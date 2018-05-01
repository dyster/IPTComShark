using System;
using System.Collections.Generic;
using PacketDotNet;
using sonesson_tools.BitStreamParser;
using sonesson_tools.DataParsers;
using sonesson_tools.DataSets;
using SharpPcap;

namespace IPTComShark
{
    public class CapturePacket : IComparable
    {
        public CapturePacket(RawCapture rawCapture) : this(new Raw(rawCapture.Timeval.Date, rawCapture.Data,
            rawCapture.LinkLayerType))
        {
        }

        public CapturePacket(Raw raw)
        {
            RawCapture = raw;
            Date = raw.TimeStamp;

            Packet packet = Packet.ParsePacket(raw.LinkLayer, raw.RawData);

            if (packet == null)
                return;

            if (packet.PayloadPacket is IPv4Packet)
            {
                var ipv4 = (IPv4Packet) packet.PayloadPacket;
                PacketTypes = PacketTypes.IPv4;

                Source = ipv4.SourceAddress.ToString();
                Destination = ipv4.DestinationAddress.ToString();

                // set the base protocol, may be overwritten later
                Protocol = ipv4.Protocol.ToString();

                switch (ipv4.Protocol)
                {
                    case IPProtocolType.TCP:
                        var tcpPacket = (TcpPacket) ipv4.PayloadPacket;
                        PacketTypes |= PacketTypes.TCP;
                        ProtocolInfo = $"{tcpPacket.SourcePort}->{tcpPacket.DestinationPort} Seq={tcpPacket.SequenceNumber} Ack={tcpPacket.Ack} AckNo={tcpPacket.AcknowledgmentNumber}";
                        
                        if (tcpPacket.DestinationPort == 50040 && tcpPacket.PayloadData.Length > 0)
                        {
                            Protocol = "JRU";
                            var ss27Parser = new SS27Parser();
                            var jruload = tcpPacket.PayloadData;

                            try
                            {
                                ushort jrulen = BitConverter.ToUInt16(new byte[] {jruload[1], jruload[0]}, 0);
                                var buffer = new byte[jrulen];
                                Array.Copy(jruload, 2, buffer, 0, jrulen);
                                var ss27 = (SS27Packet)ss27Parser.ParseData(buffer);
                                ParsedData = new ParsedDataSet(){ParsedFields = new List<ParsedField>(ss27.Header)};
                                Name = ss27.MsgType.ToString();
                            }
                            catch (Exception e)
                            {
                                Name = "ERROR";
                                ParsedData = ParsedDataSet.Create("ERROR", e.Message);
                            }

                        }

                        if (tcpPacket.SourcePort == 50041 && tcpPacket.PayloadData.Length > 0)
                        {
                            Protocol = "JRU";
                            var jruload = tcpPacket.PayloadData;
                            try
                            {
                                ushort jrulen = BitConverter.ToUInt16(new byte[] { jruload[1], jruload[0] }, 0);
                                var buffer = new byte[jrulen];
                                Array.Copy(jruload, 2, buffer, 0, jrulen);
                                ParsedData = VSIS210.JRU_STATUS.Parse(buffer);
                                Name = "JRU_STATUS";
                            }
                            catch (Exception e)
                            {
                                Name = "ERROR";
                                ParsedData = ParsedDataSet.Create("ERROR", e.Message);
                            }
                            

                        }
                        break;

                    case IPProtocolType.UDP:
                        PacketTypes |= PacketTypes.UDP;
                        var udp = (UdpPacket) ipv4.PayloadPacket;
                        IPTWPPacket = IPTWPPacket.Extract(udp);
                        MainForm.ParseIPTWPData(this);
                        break;

                    case IPProtocolType.NONE:
                        // dunno
                        break;

                    case IPProtocolType.ICMP:
                        // dunno
                        break;

                    case IPProtocolType.IGMP:
                        // dunno
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else if (packet.PayloadPacket is ARPPacket)
            {
                //ARPPacket = (ARPPacket)packet.PayloadPacket;
                PacketTypes = PacketTypes.ARP;
                Protocol = "ARP";
            }
            else if (packet.PayloadPacket is IPv6Packet)
            {
                PacketTypes = PacketTypes.IPv6;
                Protocol = "IPv6";
                // ignore, for now
            }
            else if (raw.LinkLayer == LinkLayers.Ethernet && packet.Header[12] == 0x88 && packet.Header[13] == 0xe1)
            {
                Protocol = "Homeplug AV";
                // ignore
            }
            else if (raw.LinkLayer == LinkLayers.Ethernet && packet.Header[12] == 0x89 && packet.Header[13] == 0x12)
            {
                Protocol = "Mediaxtream";
                // ignore
            }
            else if (raw.LinkLayer == LinkLayers.Ethernet && packet.Header[12] == 0x88 && packet.Header[13] == 0xcc)
            {
                Protocol = "LLDP";
                // ignore
            }
            else
            {
                Protocol = "UNKNOWN";
#if DEBUG
                // if we are in debug, we might want to know what is in the unknown
//                throw new NotImplementedException("Surprise data! " + BitConverter.ToString(packet.Bytes));
#endif
            }
        }

        /// <summary>
        /// If part of a chain
        /// </summary>
        public CapturePacket Previous { get; set; }

        /// <summary>
        /// If part of a chain
        /// </summary>
        public CapturePacket Next { get; set; }

        public string Source { get; set; }
        public string Destination { get; set; }

        //public IPv4Packet IPv4Packet { get; }
        //
        //public UdpPacket UDPPacket
        //{
        //    get
        //    {
        //        if (IPv4Packet != null && IPv4Packet.Protocol == IPProtocolType.UDP)
        //        {
        //            return (UdpPacket) IPv4Packet.PayloadPacket;
        //        }
        //        return null;
        //    }
        //}
        //
        //public ARPPacket ARPPacket { get; }

        public IPTWPPacket IPTWPPacket { get; }


        public string Protocol { get; }

        public string ProtocolInfo { get; }

        /// <summary>
        /// The types of packet contained inside the data
        /// </summary>
        public PacketTypes PacketTypes { get; set; }

        public uint No { get; set; }
        public DateTime Date { get; }

        public Raw RawCapture { get; }

        public ParsedDataSet ParsedData { get; set; }


        public string Name { get; set; }

        /// <summary>
        /// If this packet is part of a chain, get only the ParsedData that has changed
        /// </summary>
        public List<ParsedField> GetDelta()
        {
            List<ParsedField> delta = new List<ParsedField>(this.ParsedData.ParsedFields);

            if (Previous != null)
            {
                

                foreach (ParsedField field in this.ParsedData.ParsedFields)
                {
                    ParsedField lookup = Previous.ParsedData.GetField(field.Name);
                    if (lookup != null)
                    {
                        if (lookup.Value.Equals(field.Value))
                            delta.Remove(field);
                    }
                }
                
            }

            return delta;
        }

        public int CompareTo(object obj)
        {
            var packet = (CapturePacket) obj;
            return this.Date.CompareTo(packet.Date);
        }
    }

    [Flags]
    public enum PacketTypes
    {
        IPv4,
        IPv6,
        UDP,
        TCP,
        ARP
    }
}