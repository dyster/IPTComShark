using System.Collections.Generic;
using System.Linq;

namespace TrainShark.Parsers
{
    public class ParserFactory
    {
        public List<ParserBase> Parsers { get; private set; } = new List<ParserBase>();

        public void AddParser(ParserBase parser)
        {
            Parsers.Add(parser);
        }

        public ParseOutput DoPacket(ProtocolType protocol, byte[] data, iPacket iPacket)
        {
            if (Parsers.Exists(p => p.ProtocolType == protocol))
            {
                var parser = Parsers.First(p => p.ProtocolType == protocol);
                return parser.Extract(data, iPacket);
            }

            return new ParseOutput { NoParserInstalled = true };
        }
    }
}