using BitDataParser;
using System.Collections.Generic;

namespace TrainShark.Parsers
{
    internal class ProfiParser : ParserBase
    {
        public override ProtocolType ProtocolType => ProtocolType.Profibus;

        public override ParseOutput Extract(byte[] data, iPacket iPacket)
        {
            var parse = new ParseOutput();
            parse.DisplayFields = new List<DisplayField>();
            parse.ParsedData = new List<ParsedDataSet>();

            var traveller = (iTraveller)iPacket;

            // the second byte of the destination holds the SAP id
            // TODO this needs to be broken back into trainshark
            //SPLParser.SS57(data, parse, traveller.Destination[1]);

            return parse;
        }
    }
}