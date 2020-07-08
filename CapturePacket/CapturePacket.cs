using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using IPTComShark.DataSets;
using IPTComShark.Parsers;
using PacketDotNet;
using sonesson_tools;
using sonesson_tools.BitStreamParser;
using sonesson_tools.DataParsers;
using sonesson_tools.DataSets;
using sonesson_toolsNETSTANDARD.DataParsers.Subset57;

namespace IPTComShark
{
    [Serializable]
    public class CapturePacket : IComparable
    {
        private static readonly IPAddress VapAddress = IPAddress.Parse("192.168.1.12");
        private static readonly IPAddress OpcAddress = IPAddress.Parse("192.168.1.14");

        private static readonly ParserFactory _parseFactory = new ParserFactory();

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

        public static List<ParsedDataSet> ExtractParsedData(CapturePacket packet,
            out List<DisplayField> displayfields)
        {
            return ExtractParsedData(packet, out displayfields, false);
        }

        public static List<ParsedDataSet> ExtractParsedData(CapturePacket packet,
            out List<DisplayField> displayfields, bool extensive)
        {
            displayfields = new List<DisplayField>();
            var datasets = new List<ParsedDataSet>();

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

                    


                   
                    if (packet.Protocol == ProtocolType.IPTWP)
                    {
                        var parsedDataSet = MainForm.ParseIPTWPData(packet.IPTWPPacket, udp, extensive);
                        if(parsedDataSet != null)
                            datasets.Add(parsedDataSet);
                    }

                    var parse = _parseFactory.DoPacket(packet.Protocol, udp.PayloadData);
                    if (!parse.NoParserInstalled)
                    {
                        displayfields = parse.DisplayFields;
                        datasets.AddRange(parse.ParsedData);
                    }
                    
                }
            }

            return datasets;
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

                                DisplayFields.Add( new DisplayField("time", ss27.DateTime));
                                if (ss27.Events.Count == 0)
                                {
                                    // if there is no event, chuck some other data in there, maybe
                                    // ParsedData = new ParsedDataSet() { ParsedFields = new List<ParsedField>(ss27.Header) };
                                }
                                else
                                {
                                    // TODO FIX THIS SO IT WORKS
                                    ss27.Events.ForEach(e =>
                                        DisplayFields.Add(new DisplayField(e.EventType.ToString(),
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
                                var parsedDataSet = VSIS210.JRU_STATUS.Parse(buffer);
                                if(parsedDataSet != null)
                                    ParsedData.Add(parsedDataSet);
                                else
                                {
                                    // why?
                                }
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
                        else if (Equals(ipv4.SourceAddress, OpcAddress))
                        {
                            if (udp.DestinationPort == 50010)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                _protocolinfo = "OPC->CoHP (SPL)";
                            }
                            else if (udp.DestinationPort == 50012)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                _protocolinfo = "OPC->CoHP (Profibus)";
                            }
                            else if (udp.DestinationPort == 50014)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                _protocolinfo = "OPC->CoHP (Profibus)";
                            }
                            else if (udp.DestinationPort == 50015)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                _protocolinfo = "OPC->CoHP (Profibus)";
                            }
                        }
                        else if (Equals(ipv4.DestinationAddress, OpcAddress))
                        {
                            if (udp.DestinationPort == 50011)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                _protocolinfo = "CoHP->OPC (SPL)";
                            }
                            else if (udp.DestinationPort == 50013)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                _protocolinfo = "CoHP->OPC (Profibus)";
                            }
                            else if (udp.DestinationPort == 50015)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                _protocolinfo = "CoHP->OPC (Profibus)";
                            }

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
            
            if (extractParsedData != null)
            {
                foreach (var parsedDataSet in extractParsedData)
                {
                    if (parsedDataSet == null)
                    {
                        //no!
                    }
                }

                this.ParsedData.AddRange(extractParsedData);
                this.DisplayFields.AddRange(displayfields);
            }
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
                                    $"{udp.SourcePort}->{udp.DestinationPort} Len={udp.Length}";


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

        public List<ParsedDataSet> ParsedData { get; set; } = new List<ParsedDataSet>();

        public List<DisplayField> DisplayFields { get; set; } = new List<DisplayField>();

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
                    else if (ParsedData.Count > 0)
                        return ParsedData[0].Name;
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

            if (ParsedData == null || ParsedData.Count == 0)
                return delta;



            if (Previous != null && ParsedData.Count == Previous.ParsedData.Count)
            {
                for (int i = 0; i < ParsedData.Count; i++)
                {
                    var thisdataset = ParsedData[i];
                    var thatdataset = Previous.ParsedData[i];

                    if (thatdataset.ParsedFields.Count == thisdataset.ParsedFields.Count)
                    {
                        for (int x = 0; x < thisdataset.ParsedFields.Count; x++)
                        {
                            ParsedField field = thisdataset[x];
                            ParsedField lookup = thatdataset[x];

                            if (!lookup.Value.Equals(field.Value) && !ignores.Contains(field.Name))
                            {
                                delta.Add(field);
                            }
                        }
                    }
                    else
                    {
                        foreach (var field in thatdataset.ParsedFields)
                        {
                            if (!ignores.Contains(field.Name))
                                delta.Add(field);
                        }
                    }
                }
                
            }
            else
            {
                foreach (var parsedDataSet in ParsedData)
                {
                    foreach (var field in parsedDataSet.ParsedFields)
                    {
                        if (!ignores.Contains(field.Name))
                            delta.Add(field);
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
}