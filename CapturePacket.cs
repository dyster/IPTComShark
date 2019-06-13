using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using PacketDotNet;
using sonesson_tools.BitStreamParser;
using sonesson_tools.DataParsers;
using sonesson_tools.DataSets;

namespace IPTComShark
{
    [Serializable]
    public class CapturePacket : IComparable
    {
        private readonly IPAddress _vapAddress = IPAddress.Parse("192.168.1.12");

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
            catch (Exception e)
            {
                Error = e.Message;
            }


            if (packet == null)
                return;

            if (packet.PayloadPacket is IPv4Packet)
            {
                var ipv4 = (IPv4Packet) packet.PayloadPacket;


                Source = ipv4.SourceAddress.GetAddressBytes();
                Destination = ipv4.DestinationAddress.GetAddressBytes();


                switch (ipv4.Protocol)
                {
                    case IPProtocolType.TCP:
                        var tcpPacket = (TcpPacket) ipv4.PayloadPacket;
                        Protocol = ProtocolType.TCP;

                        ProtocolInfo =
                            $"{tcpPacket.SourcePort}->{tcpPacket.DestinationPort} Seq={tcpPacket.SequenceNumber} Ack={tcpPacket.Ack} AckNo={tcpPacket.AcknowledgmentNumber}";

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
                                var ss27 = (SS27Packet) ss27Parser.ParseData(buffer);

                                var outlist = new List<ParsedField>();
                                outlist.Add(ParsedField.Create("Mode", ss27.Mode));
                                outlist.Add(ParsedField.Create("Level", ss27.Level));
                                outlist.Add(ParsedField.Create("Speed", ss27.V_TRAIN.ToString()));

                                string ev = string.Join(", ", ss27.Events);

                                if (string.IsNullOrEmpty(ev))
                                {
                                    // if there is no event, chuck some other data in there, maybe
                                    // ParsedData = new ParsedDataSet() { ParsedFields = new List<ParsedField>(ss27.Header) };
                                }
                                else
                                {
                                    // TODO FIX THIS SO IT WORKS
                                    var parsedFields = ss27.Events.Select(e =>
                                        ParsedField.Create(e.EventType.ToString(), e.Description)).ToList();
                                    ParsedData = new ParsedDataSet() {ParsedFields = parsedFields};
                                    //ParsedData = ParsedDataSet.Create("Event", ev);
                                }


                                Name = ss27.MsgType.ToString();

                                this.SS27Packet = ss27;
                            }
                            catch (Exception e)
                            {
                                Name = "ERROR";
                                ParsedData = ParsedDataSet.CreateError(e.Message);
                            }
                        }

                        if (tcpPacket.SourcePort == 50041 && tcpPacket.PayloadData.Length > 0)
                        {
                            Protocol = ProtocolType.JRU;
                            var jruload = tcpPacket.PayloadData;
                            try
                            {
                                ushort jrulen = BitConverter.ToUInt16(new byte[] {jruload[1], jruload[0]}, 0);
                                var buffer = new byte[jrulen];
                                Array.Copy(jruload, 2, buffer, 0, jrulen);
                                ParsedData = VSIS210.JRU_STATUS.Parse(buffer);
                                Name = "JRU_STATUS";
                            }
                            catch (Exception e)
                            {
                                Name = "ERROR";
                                ParsedData = ParsedDataSet.CreateError(e.Message);
                            }
                        }

                        break;

                    case IPProtocolType.UDP:

                        Protocol = ProtocolType.UDP;
                        var udp = (UdpPacket) ipv4.PayloadPacket;

                        if (udp == null)
                        {
                            ProtocolInfo = "Malformed UDP";
                            this.Error = "Malformed UDP";
                            return;
                        }

                        if (Equals(ipv4.SourceAddress, _vapAddress))
                        {
                            if (udp.DestinationPort == 50023)
                                ProtocolInfo = "VAP->ETC (TR)";
                            else if (udp.DestinationPort == 50030)
                                ProtocolInfo = "VAP->ETC (DMI1 to ETC)";
                            else if (udp.DestinationPort == 50031)
                                ProtocolInfo = "VAP->ETC (DMI2 to ETC)";
                            else if (udp.DestinationPort == 50032)
                                ProtocolInfo = "VAP->ETC (DMI1 to iSTM)";
                            else if (udp.DestinationPort == 50033)
                                ProtocolInfo = "VAP->ETC (DMI2 to iSTM)";
                            else if (udp.DestinationPort == 50051)
                                ProtocolInfo = "VAP->ETC (DMI1 to gSTM)";
                            else if (udp.DestinationPort == 50052)
                                ProtocolInfo = "VAP->ETC (DMI2 to gSTM)";
                            else if (udp.DestinationPort == 50024)
                                ProtocolInfo = "VAP->ETC (DMI1 EVC-102)";
                            else if (udp.DestinationPort == 50025)
                                ProtocolInfo = "VAP->ETC (DMI2 EVC-102)";
                            else if (udp.DestinationPort == 50039)
                                ProtocolInfo = "VAP->ETC (STM to JRU)";
                            else if (udp.DestinationPort == 50041)
                                ProtocolInfo = "VAP->ETC (JRU Status)";
                            else if (udp.DestinationPort == 50050)
                                ProtocolInfo = "VAP->ETC (VAP Status)";
                            else if (udp.DestinationPort == 50015)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                ProtocolInfo = "VAP->OPC (DMI to STM)";
                            }
                            else if (udp.DestinationPort == 5514)
                                ProtocolInfo = "VAP->BDS (VAP Diag)";
                        }
                        else if (Equals(ipv4.DestinationAddress, _vapAddress))
                        {
                            if (udp.DestinationPort == 50022)
                                ProtocolInfo = "ETC->VAP (OBU)";
                            else if (udp.DestinationPort == 50026)
                                ProtocolInfo = "ETC->VAP (ETC to DMI1)";
                            else if (udp.DestinationPort == 50027)
                                ProtocolInfo = "ETC->VAP (ETC to DMI2)";
                            else if (udp.DestinationPort == 50037)
                                ProtocolInfo = "ETC->VAP (EVC-1&7)";
                            else if (udp.DestinationPort == 50035)
                                ProtocolInfo = "ETC->VAP (EVC-1&7)";
                            else if (udp.DestinationPort == 50028)
                                ProtocolInfo = "ETC->VAP (iSTM to DMI1)";
                            else if (udp.DestinationPort == 50029)
                                ProtocolInfo = "ETC->VAP (iSTM to DMI2)";
                            else if (udp.DestinationPort == 50055)
                                ProtocolInfo = "ETC->VAP (gSTM to DMI1)";
                            else if (udp.DestinationPort == 50056)
                                ProtocolInfo = "ETC->VAP (gSTM to DMI2)";
                            else if (udp.DestinationPort == 50034)
                                ProtocolInfo = "ETC->VAP (to JRU)";
                            else if (udp.DestinationPort == 50057)
                                ProtocolInfo = "ETC->VAP (VAP COnfig)";
                            else if (udp.DestinationPort == 50014)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                ProtocolInfo = "OPC->VAP";
                            }
                            else if (udp.DestinationPort == 50036)
                                ProtocolInfo = "BDS->VAP (Diag)";
                            else if (udp.DestinationPort == 50070)
                                ProtocolInfo = "ETC->VAP (ATO)";
                            else if (udp.DestinationPort == 50068)
                                ProtocolInfo = "ETC->VAP (ATO)";
                            else if (udp.DestinationPort == 50072)
                                ProtocolInfo = "ETC->VAP (ATO)";
                        }
                        else
                        {
                            ProtocolInfo = $"{udp.SourcePort}->{udp.DestinationPort} Len={udp.Length} ChkSum={udp.Checksum}";
                        }
                        
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
                        ProtocolInfo = (ipv4.PayloadPacket as ICMPv4Packet).TypeCode.ToString();
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
            else if (packet.PayloadPacket is ARPPacket arpPacket)
            {
                //ARPPacket = (ARPPacket)packet.PayloadPacket;
                ProtocolInfo = arpPacket.Operation.ToString();

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

        public byte[] Source { get; set; }
        public byte[] Destination { get; set; }

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
                for (int i = 0; i < ParsedData.ParsedFields.Count; i++)
                {
                    if (Previous.ParsedData.ParsedFields.Count == i)
                        break;
                    ParsedField field = this.ParsedData[i];
                    ParsedField lookup = Previous.ParsedData[i];
                                        
                    if (lookup.Value.Equals(field.Value))
                        delta.Remove(field);
                    
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
        IGMP,
        UDP_SPL
    }
}