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
           

            foreach(var prop in arp.GetType().GetProperties())
            {
                var value = prop.GetValue(arp);
                if (value == null)
                    continue;
                dataset.ParsedFields.Add(ParsedField.Create(prop.Name, value.ToString()));
            }            
            
            parse.ParsedData.Add(dataset);


            return parse;
        }
    }
}
