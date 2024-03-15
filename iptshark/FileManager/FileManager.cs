using BustPCap;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TrainShark.BackStore;

namespace TrainShark.FileManager
{
    public class FileManager : IDisposable
    {
        private Form _popup;
        private ProgressBar _progressbar;
        private bool _closing = false;
        private int _lastProgress = 0;

        public FileManager()
        {
            //pcapReader.ProgressUpdated += (sender, i) => _progressbar.Value = i;

            //pcapngReader.ProgressUpdated += (sender, i) => _progressbar.Value = i;

            _popup = new Form();
            _popup.Size = new Size(400, 70);

            _progressbar = new ProgressBar();
            _progressbar.Dock = DockStyle.Fill;

            _popup.Controls.Add(_progressbar);
        }

        private Raw ChunkRead(BustPCap.PCAPBlock pcapBlock)
        {
            PacketCounter++;
            UpdateProgress((PacketCounter * 100) / PacketTotal);

            var raw = new Raw(pcapBlock.DateTime, pcapBlock.PayLoad,
                (LinkLayerType)pcapBlock.Header.network);
            if (raw.TimeStamp >= FilterFrom && raw.TimeStamp <= FilterTo)
            {
                OnRawParsed(raw);
                return raw;
            }
            return null;
        }

        private Raw ChunkRead(BustPCap.PCAPNGBlock pcapBlock)
        {
            PacketCounter++;
            UpdateProgress((PacketCounter * 100) / PacketTotal);

            var raw = new Raw(pcapBlock.DateTime, pcapBlock.PayLoad,
                (LinkLayerType)pcapBlock.LinkLayerType);
            if (raw.TimeStamp >= FilterFrom && raw.TimeStamp <= FilterTo)
            {
                OnRawParsed(raw);
                return raw;
            }
            return null;
        }

        private delegate void ProgressDelegate(int i);

        private void UpdateProgress(int i)
        {
            if (i > 100)
                i = 100;
            if (_progressbar.InvokeRequired)
                _progressbar.Invoke(new ProgressDelegate(UpdateProgress), i);
            else
            {
                if (i != _lastProgress)
                {
                    _progressbar.Value = i;
                    _lastProgress = i;
                }
            }
        }

        ~FileManager()
        {
            Dispose(false);
        }

        public event EventHandler<Raw> RawParsed;

        protected virtual void OnRawParsed(Raw e)
        {
            RawParsed?.Invoke(this, e);
        }

        private int PacketCounter { get; set; }
        private int PacketTotal { get; set; }

        public IEnumerable<Raw> EnumerateFiles(List<DataSource> dataSources)
        {
            _popup.Show();

            _popup.Text = "Starting the parse";

            int i = 1;
            foreach (var source in dataSources)
            {
                PacketCounter = 0;
                PacketTotal = source.Packets;

                string pre = i + "/" + dataSources.Count + " ";
                _popup.Text = pre + "Reading from " + source.FileInfo.Name;

                if (source.SourceType == SourceType.PCAP)
                {
                    using (var pcapFileReader = new PCAPFileReader(source.FileInfo.FullName))
                    {
                        foreach (var pcapBlock in pcapFileReader.Enumerate())
                        {
                            Raw raw = ChunkRead(pcapBlock);
                            if (raw != null)
                                yield return raw;
                        }
                    }
                }
                else if (source.SourceType == SourceType.PCAPNG)
                {
                    using (var pcapFileReader = new PCAPNGFileReader(source.FileInfo.FullName))
                    {
                        foreach (var pcapBlock in pcapFileReader.Enumerate())
                        {
                            Raw raw = ChunkRead(pcapBlock);
                            if (raw != null)
                                yield return raw;
                        }
                    }
                }
                else if (source.SourceType == SourceType.Zip)
                {
                    if (SevenZipArchive.IsSevenZipFile(source.FileInfo.FullName))
                    {
                        using (SevenZipArchive sevenZipArchive = SevenZipArchive.Open(source.FileInfo.FullName))
                        {
                            using (var reader = sevenZipArchive.ExtractAllEntries())
                            {
                                foreach (var raw in ZipReader(reader, source))
                                {
                                    yield return raw;
                                }
                            }
                        }

                        GC.Collect();
                    }
                    else
                    {
                        using (var filestream = File.OpenRead(source.FileInfo.FullName))
                        {
                            using (var reader = ReaderFactory.Open(filestream))
                            {
                                foreach (var raw in ZipReader(reader, source))
                                {
                                    yield return raw;
                                }
                            }
                        }

                        GC.Collect();
                    }
                }
            }

            _popup.Close();
        }

        private IEnumerable<Raw> ZipReader(IReader reader, DataSource dataSource)
        {
            //try
            {
                while (reader.MoveToNextEntry())
                {
                    if (!reader.Entry.IsDirectory)
                    {
                        if (reader.Entry.Key != dataSource.ArchiveKey)
                            continue;
                        var memstream = new MemoryStream();
                        using (var entryStream = reader.OpenEntryStream())
                        {
                            entryStream.CopyTo(memstream);
                            memstream.Position = 0;
                        }

                        if (dataSource.ArchiveSourceType == SourceType.PCAP)
                        {
                            var pcapStreamReader = new PCAPStreamReader(memstream);
                            foreach (var pcapBlock in pcapStreamReader.Enumerate())
                            {
                                Raw raw = ChunkRead(pcapBlock);
                                if (raw != null)
                                    yield return raw;
                            }
                        }

                        if (dataSource.ArchiveSourceType == SourceType.PCAPNG)
                        {
                            var pcapngReader = new PCAPNGStreamReader(memstream);
                            foreach (var block in pcapngReader.Enumerate())
                            {
                                Raw raw = ChunkRead(block);
                                if (raw != null)
                                    yield return raw;
                            }
                        }
                    }
                }
            }
            //catch(Exception e)
            {
            }
        }

        /// <summary>
        /// Open files and folders both
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public IEnumerable<Raw> OpenFiles(string[] inputs)
        {
            List<string> fileNames = new List<string>();

            var fo = new FileOpener(inputs);
            var dialogresult = fo.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                FilterFrom = fo.DateTimeFrom;
                FilterTo = fo.DateTimeTo;
                ProcessingFilters = fo.ProcessingFilters;

                foreach (var raw in EnumerateFiles(fo.DataSources))
                {
                    yield return raw;
                }
            }
        }

        /// <summary>
        /// Starts opening data async, returns them by RawParsed event, and triggers event when finished
        /// </summary>
        /// <param name="inputs"></param>
        public async void OpenFilesAsync(string[] inputs)
        {
            var fo = new FileOpener(inputs);
            var dialogresult = fo.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                FilterFrom = fo.DateTimeFrom;
                FilterTo = fo.DateTimeTo;
                ProcessingFilters = fo.ProcessingFilters;

                var start = DateTime.Now;
                var count = 0;

                Thread thread = new Thread((object o) =>
                    {
                        foreach (var raw in EnumerateFiles(fo.DataSources))
                        {
                            // Enu merate good times come on
                            count++;
                        }

                        if (FinishedLoading != null)
                            FinishedLoading.Invoke(this, new FinishedEventArgs(start, DateTime.Now, count, fo.DataSources, inputs));
                    });

                thread.Start();
            }
        }

        public event EventHandler<FinishedEventArgs> FinishedLoading;

        public DateTime FilterTo { get; set; }
        public ProcessingFilter ProcessingFilters { get; set; }
        public DateTime FilterFrom { get; set; }

        /// <summary>
        /// Peek into text files
        /// </summary>
        /// <param name="path"></param>
        /// <param name="maxbuffer"></param>
        /// <returns></returns>
        public static string GetTextFromFile(string path, int maxbuffer)
        {
            byte[] bytes;
            using (FileStream fileStream = File.OpenRead(path))
            {
                int length;
                if (fileStream.Length > maxbuffer)
                    length = maxbuffer;
                else
                    length = (int)fileStream.Length;

                bytes = new byte[length];
                fileStream.Read(bytes, 0, length);
            }

            string text = Encoding.Default.GetString(bytes);
            return text;
        }

        private void ReleaseUnmanagedResources()
        {
            _closing = true;
        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                _popup?.Dispose();
                _progressbar?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static void SaveToFile(string fileName, List<CapturePacket> packets, BackStore.BackStore backStore)
        {
            var pcapWriter = new PCAPWriter(fileName);
            pcapWriter.Start();
            bool started = false;
            int count = 0;

            foreach (CapturePacket packet in packets)
            {
                var raw = backStore.GetRaw(packet.No);

                if (!started)
                {
                    pcapWriter.LinkLayerType = (uint)raw.LinkLayer;

                    started = true;
                }

                pcapWriter.WritePacket(raw.RawData, raw.TimeStamp);
                count++;
            }
            Thread.Sleep(1000); // just making sure
            pcapWriter.Stop();

            MessageBox.Show(count + " packets written to " + fileName);
        }

        public static void SaveToFile(DateTime DateTimeFrom, DateTime DateTimeTo, string fileName, List<DataSource> DataSources)
        {
            var fileManager = new FileManager();
            fileManager.FilterFrom = DateTimeFrom;
            fileManager.FilterTo = DateTimeTo;

            var pcapWriter = new PCAPWriter(fileName);
            bool started = false;
            int pos = 0;
            pcapWriter.Start();

            fileManager.RawParsed += (senderx, raw) =>
            {
                if (!started)
                {
                    pcapWriter.LinkLayerType = (uint)raw.LinkLayer;

                    started = true;
                }

                //var capturePacket = new CapturePacket(raw);
                pcapWriter.WritePacket(raw.RawData, raw.TimeStamp);
                //if (capturePacket.Protocol == ProtocolType.JRU)
                //{
                //    pcapWriter.WritePacket(raw.RawData, raw.TimeStamp);
                //}
                //else if (capturePacket.Protocol == ProtocolType.IPTWP && capturePacket.IPTWPPacket != null && capturePacket.IPTWPPacket.Comid != 110)
                //{
                //    pcapWriter.WritePacket(raw.RawData, raw.TimeStamp);
                //}
            };

            fileManager.EnumerateFiles(DataSources);

            Thread.Sleep(1000);
            pcapWriter.Stop();
        }
    }

    /// <summary>
    /// This class is used to avoid the confusion of using Object directly
    /// </summary>
    public class FileReadObject
    {
        public FileReadObject(object o)
        {
            ReadObject = o;
        }

        public object ReadObject { get; }
        public object Tag { get; set; }
    }
}