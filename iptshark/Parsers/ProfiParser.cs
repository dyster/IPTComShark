using BitDataParser;
using System.Collections.Generic;

namespace IPTComShark.Parsers
{
    internal class ProfiParser : IParser
    {
        public ProtocolType ProtocolType => ProtocolType.Profibus;

        public Parse Extract(byte[] data, iPacket iPacket)
        {
            var parse = new Parse();
            parse.DisplayFields = new List<DisplayField>();
            parse.ParsedData = new List<ParsedDataSet>();

            var traveller = (iTraveller)iPacket;

            // the second byte of the destination holds the SAP id
            SPLParser.SS57(data, parse, traveller.Destination[1]);

            return parse;
        }
    }
}
