using System;
using System.Collections.Generic;
using System.Linq;
using IPTComShark.DataSets;
using sonesson_tools.BitStreamParser;

namespace IPTComShark.Parsers
{
    public class NTPParser : IParser
    {
        public Parse Extract(byte[] payload)
        {
            var pars = new Parse();

            var data = NTP.NTPDataSet.Parse(payload);

            pars.DisplayFields = new List<DisplayField>();

            var refTime = (UInt32) data.ParsedFields.First(f => f.Name == "Ref Timestamp Integer")
                .Value;
            var refFrac = (UInt32) data.ParsedFields.First(f => f.Name == "Ref Timestamp Fraction")
                .Value;

            var OrgTime = (UInt32) data.ParsedFields.First(f => f.Name == "Origin Timestamp Integer")
                .Value;
            var OrgFrac = (UInt32) data.ParsedFields.First(f => f.Name == "Origin Timestamp Fraction")
                .Value;

            var RecTime = (UInt32) data.ParsedFields.First(f => f.Name == "Receive Timestamp Integer")
                .Value;
            var RecFrac = (UInt32) data.ParsedFields.First(f => f.Name == "Receive Timestamp Fraction")
                .Value;

            var transmitTime = (UInt32) data.ParsedFields
                .First(f => f.Name == "Transmit Timestamp Integer").Value;
            var transmitFrac = (UInt32) data.ParsedFields
                .First(f => f.Name == "Transmit Timestamp Fraction").Value;

            var list = new List<ParsedField>();

            if (refTime != 0)
                list.Add(
                    ParsedField.Create("Reference Time", ParseNTPDate(refTime, refFrac).ToString()));
            if (OrgTime != 0)
                list.Add(ParsedField.Create("Origin Time", ParseNTPDate(OrgTime, OrgFrac).ToString()));
            if (RecTime != 0)
                list.Add(ParsedField.Create("Receive Time", ParseNTPDate(RecTime, RecFrac).ToString()));
            if (transmitTime != 0)
                list.Add(ParsedField.Create("Transmit Time",
                    ParseNTPDate(transmitTime, transmitFrac).ToString()));

            if (OrgTime != 0 && RecTime != 0)
            {
                var rec = TimeSpan.FromSeconds(RecTime);
                var org = TimeSpan.FromSeconds(OrgTime);
                var timeSpan = rec.Subtract(org);
                list.Add(ParsedField.Create("Seconds difference", timeSpan.ToString()));
            }

            foreach (var parsedField in list)
            {
                data.ParsedFields.Add(parsedField);
                pars.DisplayFields.Add(new DisplayField(parsedField.Name, parsedField.Value));
            }

            pars.ParsedData = new List<ParsedDataSet> {data};

            return pars;
        }


        public ProtocolType ProtocolType => ProtocolType.NTP;

        private static DateTime ParseNTPDate(uint integer, uint fraction)
        {
            var epoc = new DateTime(1900, 1, 1, 0, 0, 0);
            var ms = (Int32) (((Double) fraction / UInt32.MaxValue) * 1000);
            return epoc.AddSeconds(integer).AddMilliseconds(ms);
        }
    }
}