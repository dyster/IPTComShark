using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using IPTComShark.Parsers;
using PacketDotNet;
using sonesson_tools;
using sonesson_tools.BitStreamParser;
using sonesson_tools.DataParsers;
using sonesson_tools.DataSets;

namespace IPTComShark
{
    [Serializable]
    public class CapturePacket : IComparable
    {
        private static readonly IPAddress VapAddress = IPAddress.Parse("192.168.1.12");
        public Packet Packet = null;

        // these could be null
        private string _protocolinfo = null;
        private LinkLayerType _linkLayerType;
        private string _customName = null;


        /// <summary>
        /// Constructor to create an artifical packet
        /// </summary>
        /// <param name="protocol"></param>
        public CapturePacket(ProtocolType protocol, string name, DateTime datetime)
        {
            Protocol = protocol;
            _customName = name;
            Date = datetime;
        }

        //public CapturePacket(RawCapture rawCapture) : this(new Raw(rawCapture.Timeval.Date, rawCapture.Data,
        //    rawCapture.LinkLayerType))
        //{
        //}

        public Raw ReconstructRaw()
        {
            return new Raw(Date, GetRawData(), _linkLayerType);
        }

        public byte[] GetRawData()
        {
            if (Packet.BytesSegment.NeedsCopyForActualBytes)
            {
                return Packet.BytesSegment.Bytes;
            }
            else
            {
                return Packet.Bytes;
            }
        }

        public static ParsedDataSet ExtractParsedData(CapturePacket packet,
            out List<Tuple<string, object>> displayfields)
        {
            return ExtractParsedData(packet, out displayfields, false);
        }

        public static ParsedDataSet ExtractParsedData(CapturePacket packet,
            out List<Tuple<string, object>> displayfields, bool extensive)
        {
            displayfields = new List<Tuple<string, object>>();

            if (packet.Packet.PayloadPacket is IPv4Packet)
            {
                var ipv4 = (IPv4Packet) packet.Packet.PayloadPacket;

                if (ipv4.Protocol == PacketDotNet.ProtocolType.Udp)
                {
                    var udp = (UdpPacket) ipv4.PayloadPacket;

                    // protect against corrupted data with a try read
                    try
                    {
                        var throwaway = udp.DestinationPort + udp.SourcePort + udp.Length + udp.Checksum;
                    }
                    catch (Exception e)
                    {
                        return null;
                    }

                    if (packet.Protocol == ProtocolType.NTP)
                    {
                        var data = NTP.NTPDataSet.Parse(udp.PayloadData);

                        var refTime = (UInt32) data.ParsedFields.First(f => f.Name == "Ref Timestamp Integer")
                            .Value;
                        var refFrac = (UInt32) data.ParsedFields.First(f => f.Name == "Ref Timestamp Fraction")
                            .Value;

                        var OrgTime = (UInt32) data.ParsedFields.First(f => f.Name == "Origin Timestamp Integer")
                            .Value;
                        var OrgFrac = (UInt32) data.ParsedFields.First(f => f.Name == "Origin Timestamp Fraction")
                            .Value;

                        var RecTime = (UInt32) data.ParsedFields.First(f => f.Name == "Receive Timestamp Integer")
                            .Value;
                        var RecFrac = (UInt32) data.ParsedFields.First(f => f.Name == "Receive Timestamp Fraction")
                            .Value;

                        var transmitTime = (UInt32) data.ParsedFields
                            .First(f => f.Name == "Transmit Timestamp Integer").Value;
                        var transmitFrac = (UInt32) data.ParsedFields
                            .First(f => f.Name == "Transmit Timestamp Fraction").Value;

                        var list = new List<ParsedField>();

                        if (refTime != 0)
                            list.Add(
                                ParsedField.Create("Reference Time", ParseNTPDate(refTime, refFrac).ToString()));
                        if (OrgTime != 0)
                            list.Add(ParsedField.Create("Origin Time", ParseNTPDate(OrgTime, OrgFrac).ToString()));
                        if (RecTime != 0)
                            list.Add(ParsedField.Create("Receive Time", ParseNTPDate(RecTime, RecFrac).ToString()));
                        if (transmitTime != 0)
                            list.Add(ParsedField.Create("Transmit Time",
                                ParseNTPDate(transmitTime, transmitFrac).ToString()));

                        if (OrgTime != 0 && RecTime != 0)
                        {
                            var rec = TimeSpan.FromSeconds(RecTime);
                            var org = TimeSpan.FromSeconds(OrgTime);
                            var timeSpan = rec.Subtract(org);
                            list.Add(ParsedField.Create("Seconds difference", timeSpan.ToString()));
                        }

                        foreach (var parsedField in list)
                        {
                            data.ParsedFields.Add(parsedField);
                            displayfields.Add(new Tuple<string, object>(parsedField.Name, parsedField.Value));
                        }

                        return data;
                    }
                    else if (packet.Protocol == ProtocolType.IPTWP)
                    {
                        return MainForm.ParseIPTWPData(packet.IPTWPPacket, udp, extensive);
                    }
                }
            }

            return null;
        }

        public CapturePacket(Raw raw)
        {
            //RawCapture = raw;
            Date = raw.TimeStamp;
            _linkLayerType = raw.LinkLayer;

            try
            {
                Packet = Packet.ParsePacket((LinkLayers) raw.LinkLayer, raw.RawData);
            }
            catch (Exception e)
            {
                Error = e.Message;
            }


            if (Packet == null)
                return;

            // protect against corrupted data with a try read
            try
            {
                var throwaway = Packet.Bytes.Length + Packet.HeaderData.Length;
            }
            catch (Exception e)
            {
                _protocolinfo = "Malformed Packet or Header";
                this.Error = "Malformed Packet or Header";
                return;
            }

            if (Packet.PayloadPacket is IPv4Packet)
            {
                var ipv4 = (IPv4Packet) Packet.PayloadPacket;

                switch (ipv4.Protocol)
                {
                    case PacketDotNet.ProtocolType.Tcp:
                        var tcpPacket = (TcpPacket) ipv4.PayloadPacket;
                        Protocol = ProtocolType.TCP;

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

                        if ((tcpPacket.DestinationPort == 50039 || tcpPacket.DestinationPort == 50040) &&
                            tcpPacket.PayloadData.Length > 0)
                        {
                            Protocol = ProtocolType.JRU;


                            try
                            {
                                var ss27 = ExtractSS27Packet(tcpPacket);

                                DisplayFields.Add(new Tuple<string, object>("time", ss27.DateTime));
                                if (ss27.Events.Count == 0)
                                {
                                    // if there is no event, chuck some other data in there, maybe
                                    // ParsedData = new ParsedDataSet() { ParsedFields = new List<ParsedField>(ss27.Header) };
                                }
                                else
                                {
                                    // TODO FIX THIS SO IT WORKS
                                    ss27.Events.ForEach(e =>
                                        DisplayFields.Add(new Tuple<string, object>(e.EventType.ToString(),
                                            e.Description)));
                                }

                                this.SS27Packet = ss27;
                            }
                            catch (Exception e)
                            {
                                Error = e.Message;
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
                            }
                            catch (Exception e)
                            {
                                Error = e.Message;
                            }
                        }

                        break;

                    case PacketDotNet.ProtocolType.Udp:

                        Protocol = ProtocolType.UDP;
                        var udp = (UdpPacket) ipv4.PayloadPacket;

                        if (udp == null)
                        {
                            _protocolinfo = "Malformed UDP";
                            this.Error = "Malformed UDP";
                            return;
                        }

                        // protect against corrupted data with a try read
                        try
                        {
                            var throwaway = udp.DestinationPort + udp.SourcePort + udp.Length + udp.Checksum;
                        }
                        catch (Exception e)
                        {
                            _protocolinfo = "Malformed UDP";
                            this.Error = "Malformed UDP";
                            return;
                        }


                        if (udp.SourcePort == 123 && udp.DestinationPort == 123)
                        {
                            Protocol = ProtocolType.NTP;
                        }
                        else if (Equals(ipv4.SourceAddress, VapAddress))
                        {
                            if (udp.DestinationPort == 50023)
                                _protocolinfo = "VAP->ETC (TR)";
                            else if (udp.DestinationPort == 50030)
                                _protocolinfo = "VAP->ETC (DMI1 to ETC)";
                            else if (udp.DestinationPort == 50031)
                                _protocolinfo = "VAP->ETC (DMI2 to ETC)";
                            else if (udp.DestinationPort == 50032)
                                _protocolinfo = "VAP->ETC (DMI1 to iSTM)";
                            else if (udp.DestinationPort == 50033)
                                _protocolinfo = "VAP->ETC (DMI2 to iSTM)";
                            else if (udp.DestinationPort == 50051)
                                _protocolinfo = "VAP->ETC (DMI1 to gSTM)";
                            else if (udp.DestinationPort == 50052)
                                _protocolinfo = "VAP->ETC (DMI2 to gSTM)";
                            else if (udp.DestinationPort == 50024)
                                _protocolinfo = "VAP->ETC (DMI1 EVC-102)";
                            else if (udp.DestinationPort == 50025)
                                _protocolinfo = "VAP->ETC (DMI2 EVC-102)";
                            else if (udp.DestinationPort == 50039)
                                _protocolinfo = "VAP->ETC (STM to JRU)";
                            else if (udp.DestinationPort == 50041)
                                _protocolinfo = "VAP->ETC (JRU Status)";
                            else if (udp.DestinationPort == 50050)
                                _protocolinfo = "VAP->ETC (VAP Status)";
                            else if (udp.DestinationPort == 50015)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                _protocolinfo = "VAP->OPC (DMI to STM)";
                            }
                            else if (udp.DestinationPort == 5514)
                                _protocolinfo = "VAP->BDS (VAP Diag)";
                        }
                        else if (Equals(ipv4.DestinationAddress, VapAddress))
                        {
                            if (udp.DestinationPort == 50022)
                                _protocolinfo = "ETC->VAP (OBU)";
                            else if (udp.DestinationPort == 50026)
                                _protocolinfo = "ETC->VAP (ETC to DMI1)";
                            else if (udp.DestinationPort == 50027)
                                _protocolinfo = "ETC->VAP (ETC to DMI2)";
                            else if (udp.DestinationPort == 50037)
                                _protocolinfo = "ETC->VAP (EVC-1&7)";
                            else if (udp.DestinationPort == 50035)
                                _protocolinfo = "ETC->VAP (EVC-1&7)";
                            else if (udp.DestinationPort == 50028)
                                _protocolinfo = "ETC->VAP (iSTM to DMI1)";
                            else if (udp.DestinationPort == 50029)
                                _protocolinfo = "ETC->VAP (iSTM to DMI2)";
                            else if (udp.DestinationPort == 50055)
                                _protocolinfo = "ETC->VAP (gSTM to DMI1)";
                            else if (udp.DestinationPort == 50056)
                                _protocolinfo = "ETC->VAP (gSTM to DMI2)";
                            else if (udp.DestinationPort == 50034)
                                _protocolinfo = "ETC->VAP (to JRU)";
                            else if (udp.DestinationPort == 50057)
                                _protocolinfo = "ETC->VAP (VAP Config)";
                            else if (udp.DestinationPort == 50014)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                _protocolinfo = "OPC->VAP";

                                var payload = udp.PayloadData;
                                var spl = VAP.UDP_SPL.Parse(payload);
                                this.DisplayFields =
                                    spl.ParsedFields.Select(f => new Tuple<string, object>(f.Name, f.Value)).ToList();

                                if (spl.ParsedFields.Last().Value.Equals("C9"))
                                {
                                    var nextBytes = Functions.SubArrayGetter(payload, 81);
                                    var stm = VAP.STM_Packet.Parse(nextBytes);
                                    DisplayFields.AddRange(stm.ParsedFields
                                        .Select(f => new Tuple<string, object>(f.Name, f.Value)).ToList());
                                }
                            }
                            else if (udp.DestinationPort == 50036)
                                _protocolinfo = "BDS->VAP (Diag)";
                            else if (udp.DestinationPort == 50070)
                                _protocolinfo = "ETC->VAP (ATO)";
                            else if (udp.DestinationPort == 50068)
                                _protocolinfo = "ETC->VAP (ATO)";
                            else if (udp.DestinationPort == 50072)
                                _protocolinfo = "ETC->VAP (ATO)";
                        }

                        if (Protocol == ProtocolType.UDP)
                        {
                            // EXPERIMENT

                            /*
                            Protocol = ProtocolType.UNKNOWN;
                            _protocolinfo = "EXPERIMENT";

                            var payload = udp.PayloadData;
                            var spl = VAP.UDP_SPL.Parse(payload);
                            this.DisplayFields =
                                spl.ParsedFields.Select(f => new Tuple<string, object>(f.Name, f.Value)).ToList();

                            if (spl.ParsedFields.Last().Value.Equals("C9"))
                            {
                                var nextBytes = Functions.SubArrayGetter(payload, 81);
                                var stm = VAP.STM_Packet.Parse(nextBytes);
                                DisplayFields.AddRange(stm.ParsedFields.Select(f => new Tuple<string, object>(f.Name, f.Value)).ToList());
                            }
                            */
                        }


                        try
                        {
                            IPTWPPacket = IPTWPPacket.Extract(udp);
                        }
                        catch (Exception e)
                        {
                            Error = e.Message;
                        }

                        if (IPTWPPacket != null)
                            Protocol = ProtocolType.IPTWP;
                        break;

                    case PacketDotNet.ProtocolType.Icmp:
                        Protocol = ProtocolType.ICMP;
                        // dunno
                        break;

                    case PacketDotNet.ProtocolType.Igmp:
                        Protocol = ProtocolType.IGMP;
                        // dunno
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else if (Packet.PayloadPacket is ArpPacket arpPacket)
            {
                Protocol = ProtocolType.ARP;
            }
            else if (Packet.PayloadPacket is IPv6Packet)
            {
                Protocol = ProtocolType.IPv6;
                // ignore, for now
            }
            else if (raw.LinkLayer == LinkLayerType.Ethernet && Packet.HeaderData[12] == 0x88 &&
                     Packet.HeaderData[13] == 0xe1)
            {
                Protocol = ProtocolType.HomeplugAV;
                // ignore
            }
            else if (raw.LinkLayer == LinkLayerType.Ethernet && Packet.HeaderData[12] == 0x89 &&
                     Packet.HeaderData[13] == 0x12)
            {
                Protocol = ProtocolType.Mediaxtream;
                // ignore
            }
            else if (raw.LinkLayer == LinkLayerType.Ethernet && Packet.HeaderData[12] == 0x88 &&
                     Packet.HeaderData[13] == 0xcc)
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

            var extractParsedData = ExtractParsedData(this, out var displayfields);
            this.DisplayFields.AddRange(displayfields);
            this.ParsedData = extractParsedData;
        }

        private static SS27Packet ExtractSS27Packet(TcpPacket tcpPacket)
        {
            var ss27Parser = new SS27Parser();
            var jruload = tcpPacket.PayloadData;

            ushort jrulen = BitConverter.ToUInt16(new byte[] {jruload[1], jruload[0]}, 0);
            var buffer = new byte[jrulen];
            Array.Copy(jruload, 2, buffer, 0, jrulen);
            var ss27 = (SS27Packet) ss27Parser.ParseData(buffer);
            return ss27;
        }

        public static DateTime ParseNTPDate(uint integer, uint fraction)
        {
            var epoc = new DateTime(1900, 1, 1, 0, 0, 0);
            var ms = (Int32) (((Double) fraction / UInt32.MaxValue) * 1000);
            return epoc.AddSeconds(integer).AddMilliseconds(ms);
        }

        /// <summary>
        /// If part of a chain
        /// </summary>
        public CapturePacket Previous { get; set; }

        /// <summary>
        /// If part of a chain
        /// </summary>
        public CapturePacket Next { get; set; }

        public byte[] Source
        {
            get
            {
                if (Packet == null)
                    return null;
                try
                {
                    if (Packet.PayloadPacket is IPv4Packet ipv4)
                    {
                        if (ipv4.SourceAddress != null) return ipv4.SourceAddress.GetAddressBytes();
                    }

                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public byte[] Destination
        {
            get
            {
                if (Packet == null)
                    return null;
                try
                {
                    if (Packet.PayloadPacket is IPv4Packet ipv4)
                    {
                        if (ipv4.DestinationAddress != null) return ipv4.DestinationAddress.GetAddressBytes();
                    }

                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

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

        //public ProcInfo ProtocolInfo { get; }
        public string ProtocolInfo
        {
            get
            {
                if (_protocolinfo == null)
                {
                    if (Packet == null)
                        return "VIRTUAL";
                    if (Packet.PayloadPacket is IPv4Packet)
                    {
                        var ipv4 = (IPv4Packet) Packet.PayloadPacket;

                        switch (ipv4.Protocol)
                        {
                            case PacketDotNet.ProtocolType.Tcp:
                                var tcpPacket = (TcpPacket) ipv4.PayloadPacket;

                                return
                                    $"{tcpPacket.SourcePort}->{tcpPacket.DestinationPort} Seq={tcpPacket.SequenceNumber} Ack={tcpPacket.Acknowledgment} AckNo={tcpPacket.AcknowledgmentNumber}";

                            case PacketDotNet.ProtocolType.Udp:

                                var udp = (UdpPacket) ipv4.PayloadPacket;


                                return
                                    $"{udp.SourcePort}->{udp.DestinationPort} Len={udp.Length} ChkSum={udp.Checksum}";


                            case PacketDotNet.ProtocolType.Icmp:

                                return (ipv4.PayloadPacket as IcmpV4Packet).TypeCode.ToString();

                                break;

                            case PacketDotNet.ProtocolType.Igmp:
                                // TODO something useful to display
                                return null;
                                break;

                            default:
                                // do nothing
                                return null;
                                break;
                        }
                    }
                    else if (Packet.PayloadPacket is ArpPacket arpPacket)
                    {
                        return arpPacket.Operation.ToString();
                    }
                    else if (Packet.PayloadPacket is IPv6Packet)
                    {
                        // ignore, for now
                        return null;
                    }
                    else
                        return null;
                }
                else
                    return _protocolinfo;
            }
        }

        public uint No { get; set; }

        public DateTime Date { get; }

        public ParsedDataSet ParsedData { get; set; }

        public List<Tuple<string, object>> DisplayFields { get; set; } = new List<Tuple<string, object>>();

        public string Error { get; set; } = null;

        public string Name
        {
            get
            {
                if (_customName == null)
                {
                    if (!string.IsNullOrEmpty(Error))
                        return "ERROR";
                    else if (SS27Packet != null)
                        return SS27Packet.MsgType.ToString();
                    else if (ParsedData != null)
                        return ParsedData.Name;
                    else if (IPTWPPacket != null)
                        return "Unknown Comid " + IPTWPPacket.Comid;

                    return null;
                }
                else
                    return _customName;
            }
        }

        /// <summary>
        /// If this packet is part of a chain, get only the ParsedData that has changed
        /// </summary>
        public List<ParsedField> GetDelta()
        {
            return GetDelta(new List<string>());
        }

        /// <summary>
        /// If this packet is part of a chain, get only the ParsedData that has changed
        /// </summary>
        public List<ParsedField> GetDelta(List<string> ignores)
        {
            List<ParsedField> delta = new List<ParsedField>();

            if (ParsedData == null)
                return delta;

            if (Previous != null && ParsedData.ParsedFields.Count == Previous.ParsedData.ParsedFields.Count)
            {
                for (int i = 0; i < ParsedData.ParsedFields.Count; i++)
                {
                    ParsedField field = this.ParsedData[i];
                    ParsedField lookup = Previous.ParsedData[i];

                    if (!lookup.Value.Equals(field.Value) && !ignores.Contains(field.Name))
                    {
                        delta.Add(field);
                    }
                }
            }
            else
            {
                foreach (var field in ParsedData.ParsedFields)
                {
                    if (!ignores.Contains(field.Name))
                        delta.Add(field);
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
}