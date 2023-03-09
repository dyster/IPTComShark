﻿using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using IPTComShark.Classes;
using IPTComShark.XmlFiles;

using BitDataParser;

namespace IPTComShark.Parsers
{
    class IPTWPParser : IParser
    {
        private const string Iptfile = @"ECN1_ipt_config.xml";
        private DataStore _dataStore;
        private IPTConfigReader IptConfigReader;

        public IPTWPParser(string folder)
        {
            _dataStore = new DataStore();

            var files = Directory.GetFiles(folder);

            var watch = new Stopwatch();

            foreach(var file in files)
            {
                if(file.EndsWith("PARSED"))
                {
                    continue;
                }
#if !DEBUG
                try
                {
#endif
                    Logger.Log("Parsing " + file, Severity.Info);
                    watch.Restart();
                    IptConfigReader = new IPTConfigReader(file);
                    var datasets = IptConfigReader.GetDataSetCollection();
                    _dataStore.Add(datasets);
                    watch.Stop();
                    Logger.Log(datasets.DataSets.Count + " datasets added in "+watch.ElapsedMilliseconds + "ms", Severity.Info);

                IptConfigReader.SerializeXml(file + "IN_PARSED");
                datasets.SerializeXml(file + "OUT_PARSED");
#if !DEBUG
                }
                catch(Exception e)
                {
                    var text = "Error parsing " + file + Environment.NewLine + Environment.NewLine + e.ToString();
                    var window = new TextWindow(text);
                    window.ShowDialog();                    
                }
#endif
            }



            _dataStore.RebuildIndex();
        }

        public Parse Extract(byte[] data, iPacket iPacket)
        {
            var comid = IPTWPPacket.GetComid(data);
            var type = IPTWPPacket.GetIptType(data);
            var iptPayload = IPTWPPacket.GetIPTPayload(data);

            if (type == IPTTypes.MA)
            {
                var parsedDataSet = MA.Parse(iptPayload);

                var parse = new Parse();
                parse.Name = "MA for " + comid;
                parse.ParsedData = new List<ParsedDataSet>
                {
                    parsedDataSet
                };
                parse.DisplayFields =
                    new List<DisplayField>(
                        parsedDataSet.ParsedFields.Select(pf => new DisplayField(pf.Name, pf.Value)));
                return parse;
            }
            else
            {
                var dataSetDefinition = _dataStore.GetByComid(comid);
                if (dataSetDefinition != null)
                {
                    var parsedDataSet = dataSetDefinition.Parse(iptPayload);
                    var parse = new Parse
                    {
                        BackLinkIdentifier = comid.ToString(),
                        AutoGenerateDeltaFields = true,
                        Name = parsedDataSet.Name
                    };
                    parse.ParsedData = new List<ParsedDataSet>() {parsedDataSet};
                    return parse;
                }
                else
                {
                    var parse = new Parse();
                    parse.Name = "Unknown";
                    parse.NoParserInstalled = true;
                    return parse;
                }
            }
        }

        public ProtocolType ProtocolType => ProtocolType.IPTWP;

        private DataSetDefinition MA = new DataSetDefinition()
        {
            Name = "IPT Message Acknowledgement",
            BitFields = new List<BitField>()
            {
                new BitField()
                {
                    Name = "Ack Code",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    LookupTable = new LookupTable
                    {
                        {"0", "OK"},
                        {"1", "NACK, wrong frame check sequence in data part"},
                        {"2", "NACK, destination unknown / not listening"},
                        {"3", "NACK, wrong data / configuration mismatch"},
                        {"4", "NACK, buffer not available"}
                    }
                },
                new BitField()
                {
                    Name = "Ack Sequence",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16
                }
            }
        };
    }
}