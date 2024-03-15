using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TrainShark.Classes;
using TrainShark.Parsers;

namespace TrainShark.Import
{
    internal class Canape : IImporter
    {
        private readonly Regex _regex = new Regex(@"^[\d-]*T[\d:]*,\d\d\d VariableCan896 \$ (?'hex'(?>\w\w )+)",
            RegexOptions.Singleline);

        bool IImporter.CanImport(string path)
        {
            string text = FileManager.FileManager.GetTextFromFile(path, 10000);
            return _regex.IsMatch(text);
        }

        IEnumerable<CapturePacket> IImporter.Import(string fileName)
        {
            int sortIndex = 1;

            var ss27Parser = new SS27Parser();

            foreach (var line in File.ReadLines(fileName))
            {
                Match match = _regex.Match(line);

                string value = match.Groups["hex"].Value;
                string hexstring = value.Replace(" ", "");
                byte[] bytearray = Conversions.StringToByteArray(hexstring);

                if (bytearray[3] == 0x20 && bytearray[7] != 0)
                {
                    var sub = BitDataParser.Functions.SubArrayGetterX(bytearray, 77, bytearray.Length * 8 - 77);
                    var ss27 = (SS27Packet)ss27Parser.ParseData(sub);

                    var capturePacket = new CapturePacket(ProtocolType.JRU, ss27.MsgType.ToString(), ss27.DateTime);

                    capturePacket.No = sortIndex++;

                    // add to the chain
                    //if (prev != null)
                    //    deviceLog.Previous = prev;
                    //prev = deviceLog;

                    yield return capturePacket;
                }
            }
        }
    }
}