﻿using BitDataParser;
using System.Collections.Generic;

namespace TrainShark.Parsers
{
    public class ParseOutput
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