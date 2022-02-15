using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacketDotNet;
using PacketDotNet.Utils;
using sonesson_tools.BitStreamParser;

namespace IPTComShark.Parsers
{
    public class ARPParser : IParser
    {
        public ProtocolType ProtocolType => ProtocolType.ARP;

        public Parse Extract(byte[] data)
        {
            var parse = new Parse();
            var segment = new ByteArraySegment(data);
            var arp = new PacketDotNet.ArpPacket(segment);
            parse.DisplayFields = new List<DisplayField>();
            parse.DisplayFields.Add(new DisplayField("Operation", arp.Operation));
            parse.DisplayFields.Add(new DisplayField("Sender", arp.SenderProtocolAddress));
            parse.DisplayFields.Add(new DisplayField("Target", arp.TargetProtocolAddress));

            parse.ParsedData = new List<ParsedDataSet>();
            var dataset = new ParsedDataSet();

            dataset.ParsedFields.Add(ParsedField.Create(nameof(arp.HardwareAddressLength), arp.HardwareAddressLength));
            dataset.ParsedFields.Add(ParsedField.Create(nameof(arp.HardwareAddressType), arp.HardwareAddressType));
            dataset.ParsedFields.Add(ParsedField.Create(nameof(arp.Operation), arp.Operation));
            dataset.ParsedFields.Add(ParsedField.Create(nameof(arp.ProtocolAddressLength), arp.ProtocolAddressLength));
            dataset.ParsedFields.Add(ParsedField.Create(nameof(arp.ProtocolAddressType), arp.ProtocolAddressType));
            dataset.ParsedFields.Add(ParsedField.Create(nameof(arp.SenderHardwareAddress), arp.SenderHardwareAddress));
            dataset.ParsedFields.Add(ParsedField.Create(nameof(arp.SenderProtocolAddress), arp.SenderProtocolAddress));
            dataset.ParsedFields.Add(ParsedField.Create(nameof(arp.TargetHardwareAddress), arp.TargetHardwareAddress));
            dataset.ParsedFields.Add(ParsedField.Create(nameof(arp.TargetProtocolAddress), arp.TargetProtocolAddress));
            
            parse.ParsedData.Add(dataset);

            return parse;
        }
    }
}
