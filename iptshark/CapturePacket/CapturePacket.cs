﻿using BitDataParser;
using PacketDotNet;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;
using TrainShark.DataSets;
using TrainShark.Parsers;

namespace TrainShark
{
    public class CapturePacket : IComparable, iPacket, iTraveller
    {
        private static readonly IPAddress VapAddress = IPAddress.Parse("192.168.1.12");
        private static readonly IPAddress OpcAddress = IPAddress.Parse("192.168.1.14");

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
            ProtocolInfo = "VIRTUAL";
        }

        //public CapturePacket(RawCapture rawCapture) : this(new Raw(rawCapture.Timeval.Date, rawCapture.Data,
        //    rawCapture.LinkLayerType))
        //{
        //}

        public static ParseOutput? ExtractParsedData(CapturePacket packet, Packet topPacket, ParserFactory parserFactory)
        {
            return ExtractParsedData(packet, topPacket, false, parserFactory);
        }

        public static ParseOutput? ExtractParsedData(CapturePacket packet, Packet topPacket, bool extensive, ParserFactory parserFactory)
        {
            if (!string.IsNullOrEmpty(packet.Error))
                return null;

            if (topPacket.PayloadPacket is not IPv4Packet)
            {
                return null;
            }
            var ipv4 = (IPv4Packet)topPacket.PayloadPacket;

            byte[] payloadData = null;

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
                catch
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
                catch
                {
                    return null;
                }

                payloadData = tcp.PayloadData;
            }

            // TODO check payload data for null here? currently sent to parser to fail there

            var parse = parserFactory.DoPacket(packet.Protocol, payloadData, packet);
            if (!parse.NoParserInstalled)
            {
                packet.HasData = true;
                return parse;
            }

            return null;
        }

        public CapturePacket(Raw raw) : this(raw, Packet.ParsePacket((LinkLayers)raw.LinkLayer, raw.RawData))
        {
            // TODO obsolete this constructor
        }

        public CapturePacket(Raw raw, Packet originalPacket)
        {
            Date = raw.TimeStamp;

            if (originalPacket == null)
                return;

            // protect against corrupted data with a try read
            try
            {
                // TODO change this, the bytes read is really expensive as it reassambles the whole packet
                var throwaway = originalPacket.Bytes.Length + originalPacket.HeaderData.Length;
            }
            catch
            {
                ProtocolInfo = "Malformed Packet or Header";
                this.Error = "Malformed Packet or Header";
                return;
            }

            Packet actionPacket = PacketWrapper.GetActionPacket(originalPacket);

            if (actionPacket == null)
            {
                this.Protocol = ProtocolType.UNKNOWN;
                this.ProtocolInfo = "No payload";
            }
            else if (actionPacket is IPv4Packet ipv4)
            {
                if (ipv4.SourceAddress != null) Source = ipv4.SourceAddress.GetAddressBytes();
                if (ipv4.DestinationAddress != null) Destination = ipv4.DestinationAddress.GetAddressBytes();

                if ((ipv4.FragmentFlags & 0x01) == 0x01)
                {
                    // fragments are rebuilt in the backstore
                    this.ProtocolInfo = "IP Fragment";
                    return;
                }

                switch (ipv4.Protocol)
                {
                    case PacketDotNet.ProtocolType.Tcp:
                        var tcpPacket = (TcpPacket)ipv4.PayloadPacket;
                        Protocol = ProtocolType.TCP;

                        this.SourcePort = tcpPacket.SourcePort;
                        this.DestinationPort = tcpPacket.DestinationPort;

                        // catch a semi-rare error in PacketDotNet that cannot be checked against
                        try
                        {
                            ProtocolInfo =
                                $"{tcpPacket.SourcePort}->{tcpPacket.DestinationPort} Seq={tcpPacket.SequenceNumber} Ack={tcpPacket.Acknowledgment} AckNo={tcpPacket.AcknowledgmentNumber}";

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
                        }
                        else if (tcpPacket.SourcePort == 50041 && tcpPacket.PayloadData.Length > 0)
                        {
                            Protocol = ProtocolType.JRUStatus;
                            var jruload = tcpPacket.PayloadData;
                            try
                            {
                                /* not supported, remove?
                                ushort jrulen = BitConverter.ToUInt16(new byte[] { jruload[1], jruload[0] }, 0);
                                var buffer = new byte[jrulen];
                                Array.Copy(jruload, 2, buffer, 0, jrulen);
                                var parsedDataSet = VSIS210.JRU_STATUS.Parse(buffer);
                                */

                                // TODO move this to separate parser
                                //if(parsedDataSet != null)
                                //    ParsedData.Add(parsedDataSet);
                                //else
                                //{
                                // why?
                                //}
                            }
                            catch (Exception e)
                            {
                                Error = e.Message;
                            }
                        }
                        else if (tcpPacket.DestinationPort == 44818 || tcpPacket.SourcePort == 44818)
                        {
                            Protocol = ProtocolType.CIP;
                        }

                        break;

                    case PacketDotNet.ProtocolType.Udp:

                        Protocol = ProtocolType.UDP;
                        var udp = (UdpPacket)ipv4.PayloadPacket;

                        if (udp == null)
                        {
                            ProtocolInfo = "Malformed UDP";
                            this.Error = "Malformed UDP";
                            return;
                        }

                        this.SourcePort = udp.SourcePort;
                        this.DestinationPort = udp.DestinationPort;

                        ProtocolInfo = $"{udp.SourcePort}->{udp.DestinationPort} Len={udp.Length}";

                        // protect against corrupted data with a try read
                        try
                        {
                            var throwaway = udp.Length + udp.Checksum;
                        }
                        catch
                        {
                            ProtocolInfo = "Malformed UDP";
                            this.Error = "Malformed UDP";
                            return;
                        }

                        if (udp.SourcePort == 123 && udp.DestinationPort == 123)
                        {
                            Protocol = ProtocolType.NTP;
                        }
                        else if (udp.DestinationPort == 20548 || udp.DestinationPort == 20550)
                        {
                            Protocol = ProtocolType.IPTWP;

                            try
                            {
                                Comid = IPTWPPacket.GetComid(udp.PayloadData);
                                IPTWPType = IPTWPPacket.GetIptType(udp.PayloadData);
                            }
                            catch (Exception e)
                            {
                                Error = e.Message;
                            }
                        }
                        else if (udp.DestinationPort == 2222 || udp.SourcePort == 2222)
                        {
                            Protocol = ProtocolType.CIPIO;
                        }
                        else if (Equals(ipv4.SourceAddress, OpcAddress))
                        {
                            if (udp.DestinationPort == 50010)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                ProtocolInfo = "OPC->CoHP (SPL)";
                            }
                            else if (udp.DestinationPort == 50012)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                ProtocolInfo = "OPC->CoHP (Profibus)";
                            }
                            else if (udp.DestinationPort == 50014)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                ProtocolInfo = "OPC->CoHP (Profibus)";
                            }
                            else if (udp.DestinationPort == 50015)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                ProtocolInfo = "OPC->CoHP (Profibus)";
                            }
                        }
                        else if (Equals(ipv4.DestinationAddress, OpcAddress))
                        {
                            if (udp.DestinationPort == 50011)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                ProtocolInfo = "CoHP->OPC (SPL)";
                            }
                            else if (udp.DestinationPort == 50013)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                ProtocolInfo = "CoHP->OPC (Profibus)";
                            }
                            else if (udp.DestinationPort == 50015)
                            {
                                Protocol = ProtocolType.UDP_SPL;
                                ProtocolInfo = "CoHP->OPC (Profibus)";
                            }
                        }
                        else if (Equals(ipv4.SourceAddress, VapAddress))
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
                        else if (Equals(ipv4.DestinationAddress, VapAddress))
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
                                ProtocolInfo = "ETC->VAP (VAP Config)";
                            else if (udp.DestinationPort == 50036)
                                ProtocolInfo = "BDS->VAP (Diag)";
                            else if (udp.DestinationPort == 50070)
                                ProtocolInfo = "ETC->VAP (ATO)";
                            else if (udp.DestinationPort == 50068)
                                ProtocolInfo = "ETC->VAP (ATO)";
                            else if (udp.DestinationPort == 50072)
                                ProtocolInfo = "ETC->VAP (ATO)";
                        }

                        break;

                    case PacketDotNet.ProtocolType.Icmp:
                        Protocol = ProtocolType.ICMP;
                        ProtocolInfo = (ipv4.PayloadPacket as IcmpV4Packet).TypeCode.ToString();
                        // dunno
                        break;

                    case PacketDotNet.ProtocolType.Igmp:
                        Protocol = ProtocolType.IGMP;
                        // dunno
                        break;

                    default:
                        Protocol = ProtocolType.UNKNOWN;
                        //throw new ArgumentOutOfRangeException();
                        break;
                }
            }
            else if (actionPacket is ArpPacket arpPacket)
            {
                Protocol = ProtocolType.ARP;
                ProtocolInfo = arpPacket.Operation.ToString();
            }
            else if (actionPacket is IPv6Packet)
            {
                Protocol = ProtocolType.IPv6;
                // ignore, for now
            }
            else if (actionPacket is iPacket ipac)
            {
                ProtocolInfo = ipac.ProtocolInfo;
                Protocol = ipac.Protocol;
                DisplayFields = ipac.DisplayFields;
                Name = ipac.Name;
            }
            else
            {
                Protocol = ProtocolType.UNKNOWN;
                ProtocolInfo = "Type: " + actionPacket.GetType().ToString();

#if DEBUG
                // if we are in debug, we might want to know what is in the unknown
                //                throw new NotImplementedException("Surprise data! " + BitConverter.ToString(packet.Bytes));
#endif
            }

            if (actionPacket is iTraveller traveller)
            {
                Source = traveller.Source;
                Destination = traveller.Destination;
            }

            // reading headers might fail so removing
            //else if (raw.LinkLayer == LinkLayerType.Ethernet && actionPacket.HeaderData[12] == 0x88 &&
            //         actionPacket.HeaderData[13] == 0xe1)
            //{
            //    Protocol = ProtocolType.HomeplugAV;
            //    // ignore
            //}
            //else if (raw.LinkLayer == LinkLayerType.Ethernet && actionPacket.HeaderData[12] == 0x89 &&
            //         actionPacket.HeaderData[13] == 0x12)
            //{
            //    Protocol = ProtocolType.Mediaxtream;
            //    // ignore
            //}
            //else if (raw.LinkLayer == LinkLayerType.Ethernet && actionPacket.HeaderData[12] == 0x88 &&
            //         actionPacket.HeaderData[13] == 0xcc)
            //{
            //    Protocol = ProtocolType.LLDP;
            //    // ignore
            //}
        }

        /// <summary>
        /// If part of a chain
        /// </summary>
        [JsonIgnore]
        public CapturePacket Previous { get; set; }

        /// <summary>
        /// If part of a chain
        /// </summary>
        [JsonIgnore]
        public CapturePacket Next { get; set; }

        /// <summary>
        /// Indicates that some data has been parsed successfully for this packet
        /// </summary>
        [JsonIgnore]
        public bool HasData { get; set; }

        /// <summary>
        /// Indicates that the data in this packet is identical to the previous one (assuming Previous is set and HasData)
        /// </summary>
        [JsonIgnore]
        public bool IsDupe { get; set; }

        public byte[] Source { get; set; }

        public byte[] Destination { get; set; }

        [JsonIgnore]
        public ushort SourcePort { get; set; }

        [JsonIgnore]
        public ushort DestinationPort { get; set; }

        public uint Comid { get; set; }
        public IPTTypes? IPTWPType { get; set; }

        public ProtocolType Protocol { get; }

        [field: JsonIgnore] public string ProtocolInfo { get; }

        public int No { get; set; }

        public DateTime Date { get; }

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
                    else if (Protocol == ProtocolType.IPTWP)
                        return "Unknown Comid " + Comid;

                    return null;
                }
                else
                    return _customName;
            }
            set { _customName = value; }
        }

        /// <summary>
        /// If this packet is part of a chain, get only the ParsedData that has changed
        /// </summary>
        public static List<ParsedField> GetDelta(List<ParsedDataSet> oldD, List<ParsedDataSet> newD,
            List<string> ignores)
        {
            List<ParsedField> delta = new List<ParsedField>();

            if (newD == null || newD.Count == 0)
                return delta;

            if (oldD != null && newD.Count == oldD.Count)
            {
                for (int i = 0; i < newD.Count; i++)
                {
                    var thisdataset = newD[i];
                    var thatdataset = oldD[i];

                    for (int x = 0; x < thisdataset.ParsedFields.Count; x++)
                    {
                        ParsedField field = thisdataset[x];
                        if (thatdataset.ParsedFields.Count > x)
                        {
                            ParsedField lookup = thatdataset[x];

                            if (lookup.Name == field.Name && !lookup.Value.Equals(field.Value) &&
                                !ignores.Contains(field.Name))
                            {
                                delta.Add(field);
                            }
                            else if (lookup.Name != field.Name && !ignores.Contains(field.Name))
                                delta.Add(field);
                        }
                        else if (!ignores.Contains(field.Name))
                        {
                            delta.Add(field);
                        }
                    }
                }
            }
            else
            {
                foreach (var parsedDataSet in newD)
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
            var packet = (CapturePacket)obj;
            return this.Date.CompareTo(packet.Date);
        }
    }
}