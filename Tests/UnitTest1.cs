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
        [InlineData("validate1.zip", true, "5B-EB-69-CA-93-71-F9-F1-AF-F4-67-D3-FB-57-4E-AA")]
        [InlineData("validate2.zip", false, "A5-07-69-EE-70-B6-85-FC-6A-F9-09-D4-07-7A-39-44")]
        [InlineData("validate3.zip", false, "16-7F-3F-16-40-D9-E0-AF-36-E5-9F-1F-60-96-32-FE")]
        [InlineData("validate4.zip", true, "CF-E1-5D-74-86-A1-5B-94-61-56-3E-EC-CC-90-CD-32")]
        [InlineData("validate5.zip", false, "EC-9C-98-95-7A-06-5B-53-4E-16-D9-7E-A0-63-D3-8E")]
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