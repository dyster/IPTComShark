using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading;
using IPTComShark.Parsers;
using PacketDotNet;
using PacketDotNet.Utils;
using sonesson_tools.BitStreamParser;

namespace IPTComShark.Classes
{
    public class BackStore
    {
        private int _seed = 0;

        private BackgroundWorker _worker;

        private ConcurrentQueue<Raw> _addBuffer = new ConcurrentQueue<Raw>();


        // is this a bit complicated maybe? maybe a little class? lol
        //private Dictionary<ProtocolType, Dictionary<string, Dictionary<IPAddress, CapturePacket>>> _lastKnowns = new Dictionary<ProtocolType, Dictionary<string, Dictionary<IPAddress, CapturePacket>>>();
        private LastKnownStore _lastKnowns = new LastKnownStore();

        private readonly Dictionary<int, Raw> _rawStore = new Dictionary<int, Raw>(500000);
        private readonly Dictionary<int, CapturePacket> _packetStore = new Dictionary<int, CapturePacket>(500000);
        private readonly Dictionary<ushort, Fragment> _fragmentStore = new Dictionary<ushort, Fragment>();

        /// <summary>
        /// Cache for re-assembled packets
        /// </summary>
        private readonly Dictionary<int, Packet> _topPacketStore = new Dictionary<int, Packet>();

        //private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();
        //private FileStream fileStream;

        public BackStore()
        {
            //fileStream = File.Create(@"c:\temp\iptsharkstream");

            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += DoWork;
            _worker.RunWorkerAsync();
        }

        public List<Raw> GetAllRaws()
        {
            return _rawStore.Values.ToList();
        }

        public Packet GetPacket(int number)
        {
            if (_topPacketStore.ContainsKey(number))
                return _topPacketStore[number];
            else
            {
                var raw = GetRaw(number);
                return Packet.ParsePacket((LinkLayers) raw.LinkLayer, raw.RawData);
            }
        }

        public Raw GetRaw(int number)
        {
            return _rawStore[number];
        }

        public Parse? GetParse(int number)
        {
            var extractParsedData = CapturePacket.ExtractParsedData(_packetStore[number],
                GetPacket(number));
            return extractParsedData;
        }

        public bool Working { get; set; }

        public string Status
        {
            get
            {
                if (Working)
                    return $"Processing {_addBuffer.Count} Packets";
                return null;
            }
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            bool updatePending = false;
            DateTime lastUpdate = DateTime.Now;
            TimeSpan minUpdateTime = TimeSpan.FromSeconds(1);

            var list = new List<CapturePacket>();

            while (!_worker.CancellationPending)
            {
                if (!_addBuffer.IsEmpty)
                {
                    Working = true;


                    while (!_addBuffer.IsEmpty)
                    {
                        //Raw tryraw;
                        var tryDequeue = _addBuffer.TryDequeue(out Raw tryRaw);
                        if (tryDequeue)
                        {
                            var capturePacket = Add(tryRaw, out var notused);

                            // this list is only to hold updates, the Parse function adds them to the main store
                            list.Add(capturePacket);
                        }
                    }


                    updatePending = true;
                    Working = false;
                }

                if (updatePending && DateTime.Now - lastUpdate > minUpdateTime)
                {
                    OnNewCapturePacket(list.ToArray());
                    list.Clear();

                    updatePending = false;
                    lastUpdate = DateTime.Now;
                }

                Thread.Sleep(100);
            }
        }

        public void AddAsync(Raw raw)
        {
            _addBuffer.Enqueue(raw);
        }

        /// <summary>
        /// Parse raw packet data and insert into main store.
        /// This is a blocking function, for async use AddAsync
        /// </summary>
        /// <param name="raw">Raw ethernet data</param>
        /// <returns>The parsed packet</returns>
        public CapturePacket Add(Raw raw, out List<ParsedDataSet> parsedData)
        {
            var seed = ++_seed;
            parsedData = new List<ParsedDataSet>();

            var topPacket = Packet.ParsePacket((LinkLayers) raw.LinkLayer, raw.RawData);

            // re-assemble fragments
            if (topPacket.PayloadPacket is IPv4Packet)
            {
                var ipv4 = (IPv4Packet) topPacket.PayloadPacket;

                if ((ipv4.FragmentFlags & 0x01) == 0x01 || ipv4.FragmentOffset != 0)
                {
                    var offset = ipv4.FragmentOffset * 8;
                    if (!_fragmentStore.ContainsKey(ipv4.Id))
                    {
                        _fragmentStore.Add(ipv4.Id, new Fragment());
                    }

                    if (ipv4.PayloadData != null)
                        _fragmentStore[ipv4.Id].Fragments.Add(offset, ipv4.PayloadData);
                    else
                    {
                        _fragmentStore[ipv4.Id].Fragments.Add(offset, ipv4.PayloadPacket.Bytes);
                    }

                    if (ipv4.FragmentFlags == 0)
                    {
                        var extract = _fragmentStore[ipv4.Id].Extract();
                        _fragmentStore.Remove(ipv4.Id);

                        //var array = ipv4.HeaderData.Concat(extract).ToArray();

                        var byteArraySegment = new ByteArraySegment(extract);
                        var udpPacket = new UdpPacket(byteArraySegment);
                        ipv4.PayloadPacket = udpPacket;
                        _topPacketStore.Add(seed, topPacket);
                    }
                }
            }

            // create the capturepacket
            var capturePacket = new CapturePacket(raw, topPacket);
            capturePacket.No = seed;


            // try to parse data if there is any
            var extractParsedData = CapturePacket.ExtractParsedData(capturePacket, topPacket);

            if (extractParsedData.HasValue)
            {
                var parse = extractParsedData.Value;
                parsedData = parse.ParsedData;

                // add all available displayfields for now
                if (parse.DisplayFields != null) capturePacket.DisplayFields.AddRange(parse.DisplayFields);
                if (!string.IsNullOrEmpty(parse.Name))
                    capturePacket.Name = parse.Name;

                if (!string.IsNullOrEmpty(parse.BackLinkIdentifier))
                {
                    // now we try to connect up the chain


                    var ipAddress = new IPAddress(capturePacket.Source);

                    var tuple = _lastKnowns.Find(capturePacket.Protocol, parse.BackLinkIdentifier, ipAddress);
                    if (tuple == null)
                    {
                        _lastKnowns.Add(capturePacket.Protocol, parse.BackLinkIdentifier, ipAddress, capturePacket,
                            parse.ParsedData);

                        // since no previous message is known there is no delta, we will fill displayfields with everything
                        if (parse.AutoGenerateDeltaFields)
                        {
                            // when null is passed for "old" it should release everything
                            var parsedFields = CapturePacket.GetDelta(null, parse.ParsedData, new List<string>());

                            capturePacket.DisplayFields = parsedFields.Select(p => new DisplayField(p)).ToList();
                        }
                    }
                    else
                    {
                        var previousPacket = tuple.Item1;
                        capturePacket.Previous = previousPacket;
                        previousPacket.Next = capturePacket;
                        _lastKnowns.Set(capturePacket.Protocol, parse.BackLinkIdentifier, ipAddress, capturePacket,
                            parse.ParsedData);


                        // generate fields by the difference from previous message
                        if (parse.AutoGenerateDeltaFields)
                        {
                            var parsedFields =
                                CapturePacket.GetDelta(tuple.Item2, parse.ParsedData, new List<string>());

                            // with all of this checking we should be fairly sure that this is a dupe, but really this should be more robust
                            // TODO make a more proper comparison, and chuck the old GetDelta method
                            if (parsedFields.Count == 0 && tuple.Item2.Count > 0 &&
                                tuple.Item2[0].ParsedFields.Count > 0 && parse.ParsedData.Count > 0 &&
                                parse.ParsedData[0].ParsedFields.Count > 0)
                                capturePacket.IsDupe = true;

                            capturePacket.DisplayFields = parsedFields.Select(p => new DisplayField(p)).ToList();
                        }
                    }
                }
            }
            else
            {
                // what?
            }


            _rawStore.Add(capturePacket.No, raw);
            _packetStore.Add(capturePacket.No, capturePacket);
            //_binaryFormatter.Serialize(fileStream, raw);
            //OnNewCapturePacket(capturePacket);
            return capturePacket;
        }

        public event EventHandler<CapturePacket[]> NewCapturePacket;


        protected virtual void OnNewCapturePacket(CapturePacket[] e)
        {
            NewCapturePacket?.Invoke(this, e);
        }

        public void Close()
        {
            Clear();
            _worker.CancelAsync();
            //fileStream.Flush();
            //fileStream.Close();
        }

        public void Clear()
        {
            _addBuffer = new ConcurrentQueue<Raw>();


            _seed = 0;
            _rawStore.Clear();
            _fragmentStore.Clear();
            _packetStore.Clear();
            _lastKnowns = new LastKnownStore();
        }
    }

    public class Fragment
    {
        public Dictionary<int, byte[]> Fragments { get; set; } = new Dictionary<int, byte[]>();

        public byte[] Extract()
        {
            int sum = 0;
            foreach (var keyValuePair in Fragments)
            {
                sum += keyValuePair.Value.Length;
            }

            var bytes = new byte[sum];
            foreach (var keyValuePair in Fragments)
            {
                Array.Copy(keyValuePair.Value, 0, bytes, keyValuePair.Key, keyValuePair.Value.Length);
            }

            return bytes;
        }
    }

    public class LastKnownStore
    {
        private List<Bygones> _bygoneses = new List<Bygones>();

        public Tuple<CapturePacket, List<ParsedDataSet>> Find(ProtocolType pt, string identifier, IPAddress ip)
        {
            foreach (var bygonese in _bygoneses)
            {
                if (bygonese.PT == pt)
                {
                    if (bygonese.Id == identifier)
                    {
                        if (bygonese.IP.Equals(ip))
                        {
                            // YES
                            return new Tuple<CapturePacket, List<ParsedDataSet>>(bygonese.Packet, bygonese.Data);
                        }
                    }
                }
            }

            return null;
        }

        public void Add(ProtocolType pt, string identifier, IPAddress ip, CapturePacket packet,
            List<ParsedDataSet> data)
        {
            _bygoneses.Add(new Bygones(pt, identifier, ip, packet, data));
        }

        public void Set(ProtocolType pt, string identifier, IPAddress ip, CapturePacket packet,
            List<ParsedDataSet> data)
        {
            _bygoneses.RemoveAll(b => b.PT == pt && b.Id == identifier && b.IP.Equals(ip));

            _bygoneses.Add(new Bygones(pt, identifier, ip, packet, data));
        }

        private struct Bygones
        {
            public ProtocolType PT;
            public string Id;
            public IPAddress IP;
            public CapturePacket Packet;
            public List<ParsedDataSet> Data;

            public Bygones(ProtocolType pt, string id, IPAddress ip, CapturePacket packet, List<ParsedDataSet> data)
            {
                PT = pt;
                Id = id;
                IP = ip;
                Packet = packet;
                Data = data;
            }
        }
    }
}