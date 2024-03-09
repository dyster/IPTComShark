using BitDataParser;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using TrainShark;
using TrainShark.BackStore;
using Xunit;

namespace Tests
{

    public class MainFormTests
    {
        private string _temp = @"D:\OneDrive - Alstom\09_GIT\TrainSharkTestFiles\";

        [Theory()]
        [InlineData("validate1.zip", true, "18-F7-24-79-F1-B7-14-C1-7E-9E-2B-81-39-21-06-2E")]
        [InlineData("validate2.zip", false, "D9-3D-75-DD-A2-76-A9-1D-B4-75-14-6B-C6-2B-FA-BA")]
        [InlineData("validate3.zip", false, "56-28-9D-E5-03-FD-1C-20-E5-08-D0-C3-44-13-82-88")]
        [InlineData("validate4.zip", true, "4B-21-BD-2E-D1-9D-67-9E-75-A0-1D-B2-20-50-E8-0B")]
        [InlineData("validate5.zip", false, "23-A3-80-F2-CF-3A-8D-E7-78-A1-F2-41-C0-B1-25-25")]
        public void ValidateTest(string filename, bool isNewGen, string hash)
        {
            var file = _temp + filename;
            var list = new List<Tuple<CapturePacket, List<ParsedDataSet>>>();

            var parserfactory = MainForm.GenerateParserFactory();
            var backStore = new BackStore(parserfactory);

            using (var filestream = File.OpenRead(file))
            {
                using (var zip = new ZipArchive(filestream, ZipArchiveMode.Read))
                {
                    using (var zipstream = zip.Entries[0].Open())
                    {
                        if (isNewGen)
                        {
                            var pcapstream = new BustPCap.PCAPNGStreamReader(zipstream);
                            foreach (var pcapBlock in pcapstream.Enumerate())
                            {
                                if (pcapBlock.Header != BustPCap.PCAPNGHeader.EnhancedPacket)
                                    continue;

                                var raw = new Raw(pcapBlock.DateTime, pcapBlock.PayLoad,
                                        (LinkLayerType)pcapBlock.LinkLayerType);

                                var capturePacket = backStore.Add(raw, out var parse);

                                list.Add(new Tuple<CapturePacket, List<ParsedDataSet>>(capturePacket, parse.ParsedData));
                            }
                        }
                        else
                        {
                            var pcapstream = new BustPCap.PCAPStreamReader(zipstream);
                            foreach (var pcapBlock in pcapstream.Enumerate())
                            {
                                var raw = new Raw(pcapBlock.DateTime, pcapBlock.PayLoad,
                                        (LinkLayerType)pcapBlock.Header.network);

                                var capturePacket = backStore.Add(raw, out var parse);

                                list.Add(new Tuple<CapturePacket, List<ParsedDataSet>>(capturePacket, parse.ParsedData));
                            }
                        }
                    }
                }
            }


            if (BustPCap.BaseReader.CanRead(file) == BustPCap.Format.PCAP)
            {
                using (var openRead = File.OpenRead(file))
                {
                    BustPCap.PCAPFileReader pCAPFileReader = new BustPCap.PCAPFileReader(openRead);
                    foreach (var pcapBlock in pCAPFileReader.Enumerate())
                    {

                    }


                }
            }
            else
            {

                var pcapngReader = new BustPCap.PCAPNGFileReader(file);
                foreach (var pcapBlock in pcapngReader.Enumerate())
                {

                }


            }
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
            options.Converters.Add(new IPAddressConverter());


            backStore.Close();

            using (var fileStream = File.Create(file + ".validate.json"))
            {
                //using (var streamWriter = new StreamWriter(fileStream))
                {

                    foreach (var tuple in list)
                    {
                        JsonSerializer.Serialize(fileStream, tuple, tuple.GetType(), options);


                    }
                }
            }

            byte[] computeHash;
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(file + ".validate.json"))
                {
                    computeHash = md5.ComputeHash(stream);
                    File.WriteAllText(file + ".validate.md5", BitConverter.ToString(computeHash));
                }
            }

            var validate = BitConverter.ToString(computeHash);

            Assert.Equal(hash, validate);
            //Assert.True(hash == validate);
        }
    }

    class IPAddressConverter : JsonConverter<IPAddress>
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IPAddress));
        }

        public override IPAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return IPAddress.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}