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

namespace IPTComShark.BackStore
{
    public class BackStore
    {
        private int _seed = 0;

        private BackgroundWorker _worker;
        private ParserFactory _parserFactory;

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

        /// <summary>
        /// True to disable saving of packets
        /// </summary>
        private readonly bool processingOnly;

        //private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();
        //private FileStream fileStream;

        /// <summary>
        /// Creates an instance of the BackStore to process and save packets
        /// </summary>
        /// <param name="parserFactory">The parser factory used to process incoming data</param>
        /// <param name="processingOnly">If set to true, the backstore will only process packets and return them, not save them</param>
        public BackStore(ParserFactory parserFactory, bool processingOnly = false)
        {
            //fileStream = File.Create(@"c:\temp\iptsharkstream");

            _parserFactory = parserFactory;

            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += DoWork;
            _worker.RunWorkerAsync();
            this.processingOnly = processingOnly;
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

        public byte[] GetPayload(int number)
        {
            return GetPayloadData(_packetStore[number],
                GetPacket(number));
        }

        public static byte[] GetPayloadData(CapturePacket packet, Packet topPacket)
        {
            if (!string.IsNullOrEmpty(packet.Error))
                return null;

            var actionpacket = topPacket.PayloadPacket;

            if(actionpacket is Ieee8021QPacket vlanpacket)
            {
                if (vlanpacket.PayloadPacket == null)
                    return null;

                actionpacket = vlanpacket.PayloadPacket;
            }
             
            byte[] payloadData = null;

            if(actionpacket is IPv4Packet ipv4)
            {
                if (ipv4.Protocol == PacketDotNet.ProtocolType.Udp)
                {
                    var udp = (UdpPacket)ipv4.PayloadPacket;
                    if (udp == null)
                        return null;
                    // protect against corrupted data with a try read
                    try
                    {
                        var throwaway = udp.DestinationPort + udp.SourcePort + udp.Length + udp.Checksum;
                    }
                    catch (Exception e)
                    {
                        return null;
                    }

                    payloadData = udp.PayloadData;

                }
                else if (ipv4.Protocol == PacketDotNet.ProtocolType.Tcp)
                {
                    var tcp = (TcpPacket)ipv4.PayloadPacket;
                    if (tcp == null)
                        return null;
                    // protect against corrupted data with a try read
                    try
                    {
                        var throwaway = tcp.DestinationPort + tcp.SourcePort + tcp.Checksum;
                    }
                    catch (Exception e)
                    {
                        return null;
                    }

                    payloadData = tcp.PayloadData;
                }
            }
            else if(actionpacket is ArpPacket arp)
            {
                return arp.Bytes;
            }
            
            

            return payloadData;
        }

        public bool Working { get; set; }

        /// <summary>
        /// If a filter is set, anything filtered will not be processed, i.e not saved or passed on to other functions
        /// </summary>
        public ProcessingFilter ProcessingFilters { get; set; }

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
        public CapturePacket Add(Raw raw, out Parse parse)
        {
            var seed = ++_seed;
            parse = new Parse();
            
            var topPacket = Packet.ParsePacket((LinkLayers) raw.LinkLayer, raw.RawData);

            try
            {
                // re-assemble fragments
                if (topPacket.PayloadPacket is IPv4Packet)
                {
                    var ipv4 = (IPv4Packet)topPacket.PayloadPacket;

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
            }
            catch (Exception e)
            {
                //not sure what to do with this here, the exception will be re-captured when creating the CapturePacket further down.
                //this should be optimized somehow but not sure how, at least it is an infrequent event
            }

            if (ProcessingFilters != null)
            {
                if (topPacket.PayloadPacket is IPv4Packet)
                {
                    var ipv4 = (IPv4Packet)topPacket.PayloadPacket;

                    if (ProcessingFilters.IncludeIPs != null && ProcessingFilters.IncludeIPs.Count > 0)
                    {
                        if (ProcessingFilters.IncludeIPs.Exists(ip => ip.Equals(ipv4.SourceAddress)) ||
                            ProcessingFilters.IncludeIPs.Exists(ip => ip.Equals(ipv4.DestinationAddress)))
                        {
                            // keep
                        }
                        else
                        {
                            // throw
                            this.DiscardedPackets++;
                            return null;
                        }
                    }
                    else if (ProcessingFilters.ExcludeIPs != null && ProcessingFilters.ExcludeIPs.Count > 0)
                    {
                        if (ProcessingFilters.IncludeIPs.Exists(ip => ip.Equals(ipv4.SourceAddress)) ||
                            ProcessingFilters.IncludeIPs.Exists(ip => ip.Equals(ipv4.DestinationAddress)))
                        {
                            // throw
                            this.DiscardedPackets++;
                            return null;
                        }
                        else
                        {
                            // keep
                        }
                    }

                }
            }

            // create the capturepacket
            var capturePacket = new CapturePacket(raw, topPacket);
            capturePacket.No = seed;


            // try to parse data if there is any
            var payload = GetPayloadData(capturePacket, topPacket);
            Parse? extractParsedData = _parserFactory.DoPacket(capturePacket.Protocol, payload);

            if (extractParsedData.HasValue)
            {
                parse = extractParsedData.Value;
                
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

            if(!processingOnly)
            {
                _rawStore.Add(capturePacket.No, raw);
                _packetStore.Add(capturePacket.No, capturePacket);
            }
            
            //_binaryFormatter.Serialize(fileStream, raw);
            //OnNewCapturePacket(capturePacket);
            return capturePacket;
        }

        public int DiscardedPackets { get; set; }

        public int CapturedPackets => _packetStore.Count;

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
            DiscardedPackets = 0;
        }
    }

    public class ProcessingFilter
    {
        /// <summary>
        /// If this list has items, any IP not matching one of these will be excluded
        /// </summary>
        public List<IPAddress> IncludeIPs { get; set; }

        /// <summary>
        /// If this list has items, any IP that matches on in this list will be excluded
        /// </summary>
        public List<IPAddress> ExcludeIPs { get; set; }

    }
}