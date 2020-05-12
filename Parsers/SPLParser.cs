using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPTComShark.DataSets;
using sonesson_tools;
using sonesson_tools.BitStreamParser;
using sonesson_toolsNETSTANDARD.DataParsers.Subset57;

namespace IPTComShark.Parsers
{
    class SPLParser : IParser
    {
        public Parse Extract(byte[] payload)
        {
            var parse = new Parse();

            parse.DisplayFields = new List<DisplayField>();
            parse.ParsedData = new List<ParsedDataSet>();

            var seqNr = payload[0];
            var position = 9;

            int remainer = 0;

            do
            {
                payload = Functions.SubArrayGetter(payload, position);
                position = 1;
                var spl = VAP.UDP_SPL.Parse(payload);
                parse.ParsedData.Add(spl);
                position += spl.BitsRead;

                parse.DisplayFields.AddRange(spl.ParsedFields.Select(f => new DisplayField(f)).ToList());

                var frameLen = (ushort)spl.GetField("SPLFrameLen").Value;
                var splframeArray = Functions.SubArrayGetter(payload, position, frameLen * 8);

                position += frameLen * 8;

                var sll = SS57Parser.Parse(splframeArray);

                parse.ParsedData.AddRange(sll);

                int sllread = 0;
                foreach (var parsedDataSet in sll)
                {
                    sllread += parsedDataSet.BitsRead;
                    parse.DisplayFields.AddRange(parsedDataSet.ParsedFields.Select(f => new DisplayField(f)));
                }

                if (splframeArray.Length * 8 > sllread)
                {
                    var sllPayload = Functions.SubArrayGetter(splframeArray, sllread + 1);


                    parse.DisplayFields.Add(new DisplayField("SLLPayloadRemain", BitConverter.ToString(sllPayload)));
                }


                remainer = payload.Length * 8 - (position);
            } while (remainer > 0);


            parse.DisplayFields.Add(new DisplayField("Remaining bits", remainer));

            //if (remainer > 0)
            //{
            //    var subArrayGetter = Functions.SubArrayGetter(payload, position);
            //    this.DisplayFields.Add(new Tuple<string, object>("Remainer", BitConverter.ToString(subArrayGetter)));
            //}

            return parse;
        }

        public ProtocolType ProtocolType => ProtocolType.UDP_SPL;
    }
}
