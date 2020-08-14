using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using PacketDotNet;
using PacketDotNet.Utils;

namespace IPTComShark.Classes
{
    public class BackStore
    {
        private uint _seed = 1;

        private readonly Dictionary<uint, Raw> _rawStore = new Dictionary<uint, Raw>(500000);
        private readonly Dictionary<ushort, Fragment> _fragmentStore = new Dictionary<ushort, Fragment>();

        //private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();
        //private FileStream fileStream;

        public BackStore()
        {
            //fileStream = File.Create(@"c:\temp\iptsharkstream");
        }

        public void Add(Raw raw)
        {
            
            var topPacket = Packet.ParsePacket((LinkLayers)raw.LinkLayer, raw.RawData);

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
                    
                    if(ipv4.PayloadData != null)
                        _fragmentStore[ipv4.Id].Fragments.Add(offset, ipv4.PayloadData);
                    else
                    {
                        _fragmentStore[ipv4.Id].Fragments.Add(offset, ipv4.PayloadPacket.Bytes);
                    }

                    if (ipv4.FragmentFlags == 0)
                    {
                        var extract = _fragmentStore[ipv4.Id].Extract();

                        var array = ipv4.HeaderData.Concat(extract).ToArray();

                        var byteArraySegment = new ByteArraySegment(extract);
                        var udpPacket = new UdpPacket(byteArraySegment);
                        ipv4.PayloadPacket = udpPacket;
                    }
                }
            }

            var capturePacket = new CapturePacket(raw, topPacket);
            capturePacket.No = _seed++;

            _rawStore.Add(capturePacket.No, raw);
            //_binaryFormatter.Serialize(fileStream, raw);
            OnNewCapturePacket(capturePacket);
        }

        public event EventHandler<CapturePacket> NewCapturePacket;

        public uint Count => _seed;

        protected virtual void OnNewCapturePacket(CapturePacket e)
        {
            NewCapturePacket?.Invoke(this, e);
        }

        public void Close()
        {
            //fileStream.Flush();
            //fileStream.Close();
        }

        public void Clear()
        {
            _seed = 1;
            _rawStore.Clear();
            _fragmentStore.Clear();
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
}
