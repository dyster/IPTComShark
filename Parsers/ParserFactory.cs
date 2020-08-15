using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sonesson_tools.BitStreamParser;

namespace IPTComShark.Parsers
{
    public class ParserFactory
    {
        private List<IParser> _parsers = new List<IParser>();
        public ParserFactory()
        {
            _parsers.Add(new NTPParser());
            _parsers.Add(new SPLParser());
            _parsers.Add(new JRUParser());
        }

        public Parse DoPacket(ProtocolType protocol, byte[] data)
        {
            if (_parsers.Exists(p => p.ProtocolType == protocol))
            {
                var parser = _parsers.First(p => p.ProtocolType == protocol);
                return parser.Extract(data);
            }

            return new Parse{NoParserInstalled = true};
        }
    }

    public interface IParser
    {
        Parse Extract(byte[] data);

        ProtocolType ProtocolType { get; }
    }

    public struct Parse
    {
        public bool NoParserInstalled;
        public List<ParsedDataSet> ParsedData;
        public List<DisplayField> DisplayFields;
        public string Name;
    }
}
