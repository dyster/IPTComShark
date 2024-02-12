using TrainShark.Classes;
using TrainShark.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TrainShark.Import
{
    class Hasler : IImporter
    {
        private readonly Regex _regex = new Regex(@"<binary length=""464"">(?<hex>[\w ]*)</binary>",
            RegexOptions.Singleline);


        public bool CanImport(string path)
        {
            string text = FileManager.FileManager.GetTextFromFile(path, 10000);
            return _regex.IsMatch(text);
        }

        private static byte[] MakeTcpBytes(byte[] bytes)
        {
            var lenBytes = BitConverter.GetBytes((ushort)bytes.Length);
            var outBytes = new byte[bytes.Length + 2];
            outBytes[0] = lenBytes[1];
            outBytes[1] = lenBytes[0];
            Array.Copy(bytes, 0, outBytes, 2, bytes.Length);
            return outBytes;
        }

        public IEnumerable<CapturePacket> Import(string path)
        {            
            string text = File.ReadAllText(path);
                        
            int sortIndex = 1;

            var ss27Parser = new SS27Parser();

            foreach (Match match in _regex.Matches(text))
            {
                string value = match.Groups["hex"].Value;
                string hexstring = value.Replace(" ", "");
                byte[] bytearray = Conversions.StringToByteArray(hexstring);


                var ss27 = (SS27Packet)ss27Parser.ParseData(bytearray);


                var capturePacket = new CapturePacket(ProtocolType.JRU, ss27.MsgType.ToString(), ss27.DateTime);

                capturePacket.No = sortIndex++;

                // add to the chain
                //if (prev != null)
                //    deviceLog.Previous = prev;
                //prev = deviceLog;


                yield return capturePacket;
            }            
        }

        public event EventHandler<int> ProgressUpdated;

        
    }
}