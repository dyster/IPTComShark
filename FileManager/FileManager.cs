using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketDotNet;
using sonesson_tools.FileReaders;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Readers;

namespace IPTComShark.FileManager
{
    public class FileManager
    {
        private ZipReader zipReader = new ZipReader();
        private PCAPReader pcapReader = new PCAPReader();
        private PCAPNGReader pcapngReader = new PCAPNGReader();

        private Form _popup;
        private ProgressBar _progressbar;

        public FileManager()
        {
            pcapReader.ChunkReader += chunk =>
            {
                var pcapBlock = (PCAPBlock) chunk;
                var capturePacket = new CapturePacket(new Raw(pcapBlock.DateTime, pcapBlock.PayLoad,
                    (LinkLayers) pcapBlock.Header.network));

                return new List<FileReadObject>() {new FileReadObject(capturePacket)};
            };

            pcapngReader.ChunkReader += chunk =>
            {
                var pcapngBlock = (PCAPNGBlock) chunk;
                var capturePacket = new CapturePacket(new Raw(pcapngBlock.Timestamp, pcapngBlock.PayLoad,
                    (LinkLayers) pcapngBlock.LinkLayerType));

                return new List<FileReadObject>() {new FileReadObject(capturePacket)};
            };

            pcapReader.ProgressUpdated += (sender, i) => _progressbar.Value = i;

            pcapngReader.ProgressUpdated += (sender, i) => _progressbar.Value = i;

            _popup = new Form();
            _popup.Size = new Size(400, 70);

            _progressbar = new ProgressBar();
            _progressbar.Dock = DockStyle.Fill;

            _popup.Controls.Add(_progressbar);
        }

        ~FileManager()
        {
            _progressbar.Dispose();
            _popup.Dispose();
        }

        public List<CapturePacket> OpenFiles(string[] fileNames)
        {
            _popup.Show();
            _popup.Text = "Gathering File Info...";

            var packets = new List<CapturePacket>();

            var dic = new Dictionary<string, Func<string, List<FileReadObject>>>();

            //SevenZipExtractor.SetLibraryPath(Application.ExecutablePath + @"\7z.dll");

            foreach (string fileName in fileNames)
            {
                if (SevenZipArchive.IsSevenZipFile(fileName))
                {
                    _popup.Text = $"Reading {Path.GetFileName(fileName)}";
                    var pcaps = new List<SevenZipArchiveEntry>();
                    var pcapngs = new List<SevenZipArchiveEntry>();

                    using (SevenZipArchive sevenZipArchive = SevenZipArchive.Open(fileName))
                    {
                        foreach (SevenZipArchiveEntry entry in sevenZipArchive.Entries.Where(e => !e.IsDirectory))
                        {
                            // since these bloody streams can't seek, need to reopen every time

                            using (Stream openEntryStream = entry.OpenEntryStream())
                            {
                                if (pcapReader.CanRead(openEntryStream))
                                {
                                    pcaps.Add(entry);
                                    continue;
                                }
                            }


                            using (Stream openEntryStream = entry.OpenEntryStream())
                            {
                                if (pcapngReader.CanRead(openEntryStream))
                                    pcapngs.Add(entry);
                            }

                            // there is nothing I deplore more than extracting to temporary files, so I'm going to hold the stream in memory instead
                            // the pcap(ng) reader should be modified to deal with non-seekable streams perhaps
                        }


                        int total = pcaps.Count + pcapngs.Count;
                        int curr = 0;

                        foreach (SevenZipArchiveEntry entry in pcaps)
                        {
                            _popup.Text = $"{++curr}/{total} in {Path.GetFileName(fileName)}";

                            using (var memoryStream = new MemoryStream())
                            {
                                using (Stream openEntryStream = entry.OpenEntryStream())
                                {
                                    openEntryStream.CopyTo(memoryStream);
                                }
                                memoryStream.Position = 0;


                                List<FileReadObject> readObjects = pcapReader.ReadStream(memoryStream);
                                packets.AddRange(readObjects.Select(fro => (CapturePacket) fro.ReadObject).ToList());
                            }
                        }

                        foreach (SevenZipArchiveEntry entry in pcapngs)
                        {
                            _popup.Text = $"{++curr}/{total} in {Path.GetFileName(fileName)}";

                            using (var memoryStream = new MemoryStream())
                            {
                                using (Stream openEntryStream = entry.OpenEntryStream())
                                {
                                    openEntryStream.CopyTo(memoryStream);
                                }
                                memoryStream.Position = 0;


                                List<FileReadObject> readObjects = pcapngReader.ReadStream(memoryStream);
                                packets.AddRange(readObjects.Select(fro => (CapturePacket) fro.ReadObject).ToList());
                            }
                        }
                    }
                }
                //using (Stream stream = File.OpenRead(fileName))
                //using (var reader = ReaderFactory.Open(stream))
                //{
                //    
                //}


                /* I give up, this just can't be done properly with DeflateStreams, need to get a better zip class!
                if (zipReader.CanRead(fileName))
                {
                    ZipArchive zipArchive = ZipFile.OpenRead(fileName);
                    foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
                    {
                        // all the opening and closing of stream below is because DeflateStream is stupid
                        Stream stream = zipArchiveEntry.Open();

                        if (pcapReader.CanRead(stream))
                        {
                            
                            stream.Close();
                            dic.Add(zipArchiveEntry.Open(), pcapReader.GetReadStreamFunction());
                            continue;
                        }

                        stream.Close();
                        stream = zipArchiveEntry.Open();
                        if (pcapngReader.CanRead(stream))
                        {
                            
                            stream.Close();
                            dic.Add(zipArchiveEntry.Open(), pcapReader.GetReadStreamFunction());
                        }
                    }
                }*/

                if (pcapReader.CanRead(fileName))
                {
                    dic.Add(fileName, pcapReader.GetReadFunction());
                }
                else if (pcapngReader.CanRead(fileName))
                {
                    dic.Add(fileName, pcapngReader.GetReadFunction());
                }
            }


            uint seed = 1;
            int i = 0;
            foreach (var pair in dic)
            {
                i++;

                _popup.Text = $"Reading file {i} of {dic.Count}";
                List<FileReadObject> objects = pair.Value.Invoke(pair.Key);

                packets.AddRange(objects.Select(fro => (CapturePacket) fro.ReadObject).ToList());
            }

            packets.ForEach(p => p.No = seed++);

            _popup.Close();

            return packets;
        }
    }
}