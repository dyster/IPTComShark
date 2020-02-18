using sonesson_tools;
using sonesson_tools.BitStreamParser;
using sonesson_tools.DataParsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace IPTComShark.Import
{
    class Hasler
    {
        private readonly Regex _regex = new Regex(@"<binary length=""464"">(?<hex>[\w ]*)</binary>",
            RegexOptions.Singleline);


        public bool CanImport(string path)
        {
            string text = Functions.GetTextFromFile(path, 10000);
            return _regex.IsMatch(text);
        }

        private static byte[] MakeTcpBytes(byte[] bytes)
        {
            var lenBytes = BitConverter.GetBytes((ushort) bytes.Length);
            var outBytes = new byte[bytes.Length + 2];
            outBytes[0] = lenBytes[1];
            outBytes[1] = lenBytes[0];
            Array.Copy(bytes, 0, outBytes, 2, bytes.Length);
            return outBytes;
        }

        public List<CapturePacket> Import(string path)
        {
            var list = new List<CapturePacket>();
            string text = File.ReadAllText(path);

            CapturePacket prev = null;
            uint sortIndex = 1;

            var ss27Parser = new SS27Parser();

            foreach (Match match in _regex.Matches(text))
            {
                string value = match.Groups["hex"].Value;
                string hexstring = value.Replace(" ", "");
                byte[] bytearray = StringToByteArray(hexstring);


                var ss27 = (SS27Packet) ss27Parser.ParseData(bytearray);


                var capturePacket = new CapturePacket(ProtocolType.JRU, ss27.MsgType.ToString(), ss27.DateTime);
                capturePacket.ParsedData = new ParsedDataSet() {ParsedFields = new List<ParsedField>(ss27.Header)};
                capturePacket.No = sortIndex++;
                capturePacket.SS27Packet = ss27;

                // add to the chain
                //if (prev != null)
                //    deviceLog.Previous = prev;
                //prev = deviceLog;


                list.Add(capturePacket);
            }

            return list;
        }

        public event EventHandler<int> ProgressUpdated;

        private static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            var bytes = new byte[NumberChars / 2];
            for (var i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}