﻿using BitDataParser;
using System.Collections.Generic;
using System.Linq;

namespace IPTComShark.Parsers
{
    public class ParserFactory
    {
        public List<IParser> Parsers { get; private set; } = new List<IParser>();

        public void AddParser(IParser parser)
        {
            Parsers.Add(parser);
        }

        public Parse DoPacket(ProtocolType protocol, byte[] data, iPacket iPacket)
        {
            if (Parsers.Exists(p => p.ProtocolType == protocol))
            {
                var parser = Parsers.First(p => p.ProtocolType == protocol);
                return parser.Extract(data, iPacket);
            }

            return new Parse { NoParserInstalled = true };
        }
    }

    public interface IParser
    {
        Parse Extract(byte[] data, iPacket iPacket);

        ProtocolType ProtocolType { get; }
    }

    public class Parse
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