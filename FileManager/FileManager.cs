using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IPTComShark.Windows;
using PacketDotNet;
using sonesson_tools.FileReaders;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Readers;
using System.Threading;

namespace IPTComShark.FileManager
{
    public class FileManager :IDisposable
    {
        private ZipReader zipReader = new ZipReader();
        private PCAPReader pcapReader = new PCAPReader();
        private PCAPNGReader pcapngReader = new PCAPNGReader();
        
        private Form _popup;
        private ProgressBar _progressbar;
        private bool _closing = false;
        private int _lastProgress = 0;

        public FileManager()
        {
            pcapReader.ChunkReader += chunk =>
            {
                PacketCounter++;
                UpdateProgress((PacketCounter * 100) / PacketTotal);
                var pcapBlock = (PCAPBlock) chunk;
                var raw = new Raw(pcapBlock.DateTime, pcapBlock.PayLoad,
                    (LinkLayerType) pcapBlock.Header.network);
                if(raw.TimeStamp >= FilterFrom && raw.TimeStamp <= FilterTo)
                    OnRawParsed(raw);
                

                return new List<FileReadObject>() ;
            };

            pcapngReader.ChunkReader += chunk =>
            {
                PacketCounter++;
                UpdateProgress((PacketCounter * 100) / PacketTotal);
                var pcapngBlock = (PCAPNGBlock) chunk;
                var raw = new Raw(pcapngBlock.Timestamp, pcapngBlock.PayLoad,
                    (LinkLayerType) pcapngBlock.Interface.LinkLayerType);
                if (raw.TimeStamp >= FilterFrom && raw.TimeStamp <= FilterTo)
                    OnRawParsed(raw);
                

                return new List<FileReadObject>();
            };

            //pcapReader.ProgressUpdated += (sender, i) => _progressbar.Value = i;

            //pcapngReader.ProgressUpdated += (sender, i) => _progressbar.Value = i;

            _popup = new Form();
            _popup.Size = new Size(400, 70);

            _progressbar = new ProgressBar();
            _progressbar.Dock = DockStyle.Fill;

            _popup.Controls.Add(_progressbar);

            
            
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
                if(i != _lastProgress)
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

        public void EnumerateFiles(List<DataSource> dataSources)
        {
            _popup.Show();
            
            _popup.Text = "Starting the parse";


            var dic = new Dictionary<string, Func<string, List<FileReadObject>>>();

            //SevenZipExtractor.SetLibraryPath(Application.ExecutablePath + @"\7z.dll");

            int i = 1;
            foreach (var source in dataSources)
            {
                PacketCounter = 0;
                PacketTotal = source.Packets;

                string pre = i + "/" + dataSources.Count + " ";
                _popup.Text = pre + "Reading from " + source.FileInfo.Name;

                if(source.SourceType == SourceType.PCAP)
                {            
                 
                    pcapReader.Read(source.FileInfo.FullName);
                }
                else if(source.SourceType == SourceType.PCAPNG)
                {
                    pcapngReader.Read(source.FileInfo.FullName);
                }
                else if(source.SourceType == SourceType.Zip)
                {
                    if (SevenZipArchive.IsSevenZipFile(source.FileInfo.FullName))
                    {
                        using (SevenZipArchive sevenZipArchive = SevenZipArchive.Open(source.FileInfo.FullName))
                        {
                            using (var reader = sevenZipArchive.ExtractAllEntries())
                            {                                
                                ZipReader(reader, source);
                            }
                        }
                        GC.Collect();
                    }
                    else
                    {
                        try
                        {

                            using (var filestream = File.OpenRead(source.FileInfo.FullName))
                            {
                                using (var reader = ReaderFactory.Open(filestream))
                                {                                    
                                    ZipReader(reader, source);

                                }
                            }
                        }
                        catch (InvalidOperationException ex)
                        {
                            
                        }
                        GC.Collect();
                    }
                }
                
            }
            _popup.Close();
        }

        private void ZipReader(IReader reader, DataSource dataSource)
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

                            pcapReader.ReadStream(memstream);
                        }

                        if (dataSource.ArchiveSourceType == SourceType.PCAPNG)
                        {
                            pcapngReader.ReadStream(memstream);


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
        public List<CapturePacket> OpenFiles(string[] inputs)
        {
            

            
            
            List<string> fileNames = new List<string>();

            var fo = new FileOpener(inputs);
            var dialogresult = fo.ShowDialog();
            if(dialogresult == DialogResult.OK)
            {
                this.FilterFrom = fo.DateTimeFrom;
                this.FilterTo = fo.DateTimeTo;
                
                var packets = new List<CapturePacket>();

                RawParsed += (sender, raw) =>
                {
                    packets.Add(new CapturePacket(raw));
                };

                EnumerateFiles(fo.DataSources);



                uint seed = 1;


                packets.ForEach(p => p.No = seed++);
                
                return packets;
            }

            return null;
        }

        public DateTime FilterTo { get; set; }

        public DateTime FilterFrom { get; set; }


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
    }
}