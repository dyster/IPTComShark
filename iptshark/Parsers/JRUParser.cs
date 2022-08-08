using System;
using System.Collections.Generic;
using BitDataParser;

namespace IPTComShark.Parsers
{
    class JRUParser : IParser
    {
        public Parse Extract(byte[] jruload, iPacket iPacket)
        {
            var parse = new Parse();
            parse.DisplayFields = new List<DisplayField>();
            parse.ParsedData = new List<ParsedDataSet>();
            var ss27Parser = new SS27Parser();

            ushort jrulen = BitConverter.ToUInt16(new byte[] {jruload[1], jruload[0]}, 0);
            var buffer = new byte[jrulen];
            Array.Copy(jruload, 2, buffer, 0, jrulen);
            var ss27 = (SS27Packet) ss27Parser.ParseData(buffer);

            parse.Name = ss27.MsgType.ToString();

            if (ss27.Events.Count == 0)
            {
                // if there is no event, chuck some other data in there, maybe
                // ParsedData = new ParsedDataSet() { ParsedFields = new List<ParsedField>(ss27.Header) };
            }
            else
            {
                // TODO FIX THIS SO IT WORKS
                ss27.Events.ForEach(e =>
                    parse.DisplayFields.Add(new DisplayField(e.EventType.ToString(),
                        e.Description)));
            }

            if (ss27.Header != null) parse.ParsedData.Add(ss27.Header);
            if (ss27.SubMessage != null) parse.ParsedData.Add(ss27.SubMessage);
            if (ss27.ExtraMessages != null && ss27.ExtraMessages.Count > 0)
                parse.ParsedData.AddRange(ss27.ExtraMessages);

            parse.BackLinkIdentifier = ss27.MsgType.ToString();

            return parse;
        }

        public ProtocolType ProtocolType => ProtocolType.JRU;
    }
}