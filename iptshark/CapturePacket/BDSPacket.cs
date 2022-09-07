using PacketDotNet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTComShark
{
    internal class BDSPacket : PacketDotNet.Packet, iPacket
    {
        public BDSPacket(byte[] data)
        {
            var headerlength = 27;
            byte[] header = new byte[headerlength];
            byte[] payload = new byte[data.Length - headerlength];

            Buffer.BlockCopy(data, 0, header, 0, headerlength);
            Buffer.BlockCopy(data, headerlength, payload, 0, data.Length - headerlength);

            this.PayloadData = payload;
            this.Header = new ByteArraySegment(header, 0, headerlength);

            switch(header[0])
            {
                case 0x8A:
                    Name = "BDS_BIN";
                    DisplayFields.Add(new DisplayField("binary", BitConverter.ToString(payload)));
                    break;
                case 0x8B:
                    Name = "BDS_BDS";
                    DisplayFields.Add(new DisplayField("txt", ASCIIEncoding.ASCII.GetString(payload)));
                    break;
                case 0x89:
                    Name = "BDS_TXT";
                    DisplayFields.Add(new DisplayField("txt", ASCIIEncoding.ASCII.GetString(payload)));
                    break;
                default:
                    Name = header[0]+"_???";
                    break;
            }
            var bdsheader = new BDSHeader(header);
            BDSHeader = bdsheader;

            

            ProtocolInfo = bdsheader.ToString();
        }

        public BDSHeader BDSHeader { get; set; }

        public string Name { get; set; }

        public string ProtocolInfo { get; set; }

        public List<DisplayField> DisplayFields { get; set; } = new List<DisplayField>();

        public ProtocolType Protocol { get => ProtocolType.BDS; }

        public string ASCII()
        {
            //return BitConverter.ToString(Header.ActualBytes()) + " || " +  ASCIIEncoding.ASCII.GetString(this.PayloadData);
            return ASCIIEncoding.ASCII.GetString(this.PayloadData);
        }
    }

    public class BDSHeader
    {
        public BDSHeader(byte[] data)
        {
            
            length = BitConverter.ToUInt16(new byte[] { data[2], data[1] }, 0);
            n_ver = (ushort)data[3];
            device = (ushort)data[4];
            service_id = (ushort)data[5];
            n_seq = (ushort)data[6];
            utc_reftime = BitConverter.ToUInt32(new byte[] { data[10], data[9], data[8], data[7] }, 0);
            utc_ticks = BitConverter.ToUInt16(new byte[] { data[12], data[11] }, 0);
            //var utc_offset = (ushort)data[13]; not sure why the gap here is so big
            t_reftime = BitConverter.ToUInt32(new byte[] { data[21], data[20], data[19], data[18] }, 0);
            classy = BitConverter.ToUInt16(new byte[] { data[23], data[22] }, 0);
            msg_id = BitConverter.ToUInt16(new byte[] { data[25], data[24] }, 0);
            severity = (ushort)data[26];
        }

        public ushort length;
        public ushort n_ver;
        public ushort device;
        public ushort service_id;
        public ushort n_seq;
        public uint utc_reftime;
        public ushort utc_ticks;
        public uint t_reftime;
        public ushort classy;
        public ushort msg_id;
        public ushort severity;

        public override string ToString()
        {
            return "Len:" + length + " DeviceID:" + device + " UTC:" + utc_reftime + " ticks:" + utc_ticks + " t_reftime:" + t_reftime + " class:" + classy + " msg_id:" + msg_id;
        }
    }
}
