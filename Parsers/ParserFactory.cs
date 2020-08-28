﻿using System;
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
        /// <summary>
        /// True if no parser was found
        /// </summary>
        public bool NoParserInstalled;

        /// <summary>
        /// The parsed data
        /// </summary>
        public List<ParsedDataSet> ParsedData;

        /// <summary>
        /// What will be displayed in the listview
        /// </summary>
        public List<DisplayField> DisplayFields;

        /// <summary>
        /// Custom name, optional
        /// </summary>
        public string Name;

        /// <summary>
        /// An identifier that will be linked with protocol and IP to find "previous" packet
        /// </summary>
        public string BackLinkIdentifier;

        /// <summary>
        /// If this is set to true and there is a backlink, DisplayFields will be generated from what has changed from previous parse
        /// </summary>
        public bool AutoGenerateDeltaFields;
    }
}
