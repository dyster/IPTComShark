using System;
using System.Collections.Generic;
using System.Net;
using PacketDotNet;
using sonesson_tools.BitStreamParser;
using sonesson_tools.DataParsers;
using sonesson_tools.DataSets;
using SharpPcap;

namespace IPTComShark
{
    public class CapturePacket : IComparable
    {
        /// <summary>
        /// Constructor to create an artifical packet
        /// </summary>
        /// <param name="protocol"></param>
        public CapturePacket(ProtocolType protocol, string name, DateTime datetime)
        {
            Protocol = protocol;
            Name = name;
            Date = datetime;
        }

        //public CapturePacket(RawCapture rawCapture) : this(new Raw(rawCapture.Timeval.Date, rawCapture.Data,
        //    rawCapture.LinkLayerType))
        //{
        //}

        public CapturePacket(Raw raw)
        {
            RawCapture = raw;
            Date = raw.TimeStamp;

            Packet packet = null;
            try
            {
                packet = Packet.ParsePacket((LinkLayers) raw.LinkLayer, raw.RawData);
            }
            catch(Exception e)
            {
                Error = e.Message;
            }
            

            if (packet == null)
                return;

            if (packet.PayloadPacket is IPv4Packet)
            {
                var ipv4 = (IPv4Packet) packet.PayloadPacket;
                

                Source = ipv4.SourceAddress;
                Destination = ipv4.DestinationAddress;

                
                switch (ipv4.Protocol)
                {
                    case IPProtocolType.TCP:
                        var tcpPacket = (TcpPacket) ipv4.PayloadPacket;
                        Protocol = ProtocolType.TCP;
                        
                        ProtocolInfo = $"{tcpPacket.SourcePort}->{tcpPacket.DestinationPort} Seq={tcpPacket.SequenceNumber} Ack={tcpPacket.Ack} AckNo={tcpPacket.AcknowledgmentNumber}";

                        // catch a semi-rare error in PacketDotNet that cannot be checked against
                        try
                        {
                            if (tcpPacket.PayloadData.Length == 0)
                                break;
                        }
                        catch (Exception e)
                        {
                            Error = e.Message;
                            break;
                        }

                        if (tcpPacket.DestinationPort == 50040 && tcpPacket.PayloadData.Length > 0)
                        {
                            Protocol = ProtocolType.JRU;
                            var ss27Parser = new SS27Parser();
                            var jruload = tcpPacket.PayloadData;

                            try
                            {
                                ushort jrulen = BitConverter.ToUInt16(new byte[] {jruload[1], jruload[0]}, 0);
                                var buffer = new byte[jrulen];
                                Array.Copy(jruload, 2, buffer, 0, jrulen);
                                var ss27 = (SS27Packet)ss27Parser.ParseData(buffer);

                                var outlist = new List<ParsedField>();
                                outlist.Add(new ParsedField(){Name = "Mode", Value = ss27.Mode});
                                outlist.Add(new ParsedField() { Name = "Level", Value = ss27.Level });
                                outlist.Add(new ParsedField() { Name = "Speed", Value = ss27.V_TRAIN });

                                string ev = string.Join(", ", ss27.Events);

                                if (string.IsNullOrEmpty(ev))
                                {
                                    // if there is no event, chuck some other data in there, maybe
                                    // ParsedData = new ParsedDataSet() { ParsedFields = new List<ParsedField>(ss27.Header) };
                                }
                                else
                                {
                                    ParsedData = ParsedDataSet.Create("Event", ev);
                                }

                                
                                Name = ss27.MsgType.ToString();

                                this.SS27Packet = ss27;
                            }
                            catch (Exception e)
                            {
                                Name = "ERROR";
                                ParsedData = ParsedDataSet.Create("ERROR", e.Message);
                            }

                        }

                        if (tcpPacket.SourcePort == 50041 && tcpPacket.PayloadData.Length > 0)
                        {
                            Protocol = ProtocolType.JRU;
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
                        
                        Protocol = ProtocolType.UDP;
                        var udp = (UdpPacket) ipv4.PayloadPacket;
                        IPTWPPacket = IPTWPPacket.Extract(udp);
                        MainForm.ParseIPTWPData(this);
                        if (IPTWPPacket != null)
                            Protocol = ProtocolType.IPTWP;
                        break;

                    case IPProtocolType.NONE:
                        Protocol = ProtocolType.UNKNOWN;
                        // dunno
                        break;

                    case IPProtocolType.ICMP:
                        Protocol = ProtocolType.ICMP;
                        // dunno
                        break;

                    case IPProtocolType.IGMP:
                        Protocol = ProtocolType.IGMP;
                        // dunno
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else if (packet.PayloadPacket is ARPPacket)
            {
                //ARPPacket = (ARPPacket)packet.PayloadPacket;
                
                Protocol = ProtocolType.ARP;
            }
            else if (packet.PayloadPacket is IPv6Packet)
            {
                
                Protocol = ProtocolType.IPv6;
                // ignore, for now
            }
            else if (raw.LinkLayer == LinkLayerType.Ethernet && packet.Header[12] == 0x88 && packet.Header[13] == 0xe1)
            {
                Protocol = ProtocolType.HomeplugAV;
                // ignore
            }
            else if (raw.LinkLayer == LinkLayerType.Ethernet && packet.Header[12] == 0x89 && packet.Header[13] == 0x12)
            {
                Protocol = ProtocolType.Mediaxtream;
                // ignore
            }
            else if (raw.LinkLayer == LinkLayerType.Ethernet && packet.Header[12] == 0x88 && packet.Header[13] == 0xcc)
            {
                Protocol = ProtocolType.LLDP;
                // ignore
            }
            else
            {
                Protocol = ProtocolType.UNKNOWN;
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

        public IPAddress Source { get; set; }
        public IPAddress Destination { get; set; }

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

        public SS27Packet SS27Packet { get; set; }


        public ProtocolType Protocol { get; }

        public string ProtocolInfo { get; }

        /// <summary>
        /// The types of packet contained inside the data
        /// </summary>
        //public PacketTypes PacketTypes { get; set; }

        public uint No { get; set; }
        public DateTime Date { get; }

        public Raw RawCapture { get; }

        public ParsedDataSet ParsedData { get; set; }

        public string Error { get; set; } = null;

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

    public enum ProtocolType
    {
        UNKNOWN,
        ARP,
        JRU,
        IPv6,
        HomeplugAV,
        Mediaxtream,
        LLDP,
        IPTWP,
        TCP,
        UDP,
        ICMP,
        IGMP
    }
}