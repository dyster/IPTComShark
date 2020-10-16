using SharpCompress.Archives.SevenZip;
using SharpCompress.Readers;
using sonesson_tools.FileReaders;
using sonesson_tools.FileWriters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace IPTComShark.FileManager
{
    public partial class FileOpener : Form
    {
        private string[] _inputstrings;

        private PCAPReader pcapReader = new PCAPReader();
        private PCAPNGReader pcapngReader = new PCAPNGReader();
        private BindingList<DataSource> _dataSources = new BindingList<DataSource>();

        public List<DataSource> DataSources { get; private set; }
        public DateTime DateTimeFrom { get; private set; }
        public DateTime DateTimeTo { get; private set; }

        public FileOpener(string[] inputs)
        {
            InitializeComponent();

            _inputstrings = inputs;

            dataListView1.DataSource = _dataSources;
            dataListView1.PrimarySortColumn = olvColumnStart;

            backgroundWorker1.RunWorkerAsync();
        }


        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateList("Gathering filenames");
            List<string> fileNames = new List<string>();

            foreach (var str in Uniquify(_inputstrings.ToList<string>()))
            {
                if (File.Exists(str))
                    fileNames.Add(str);
                else if (Directory.Exists(str))
                {
                    fileNames.AddRange(Directory.GetFiles(str, "*", SearchOption.AllDirectories));
                }
            }

            fileNames = Uniquify(fileNames);

            UpdateList("Inspecting " + fileNames.Count + " files");

            InspectFiles(fileNames);


            GC.Collect();
        }

        private void InspectFiles(List<string> fileNames)
        {
            var threads = new List<Thread>();

            foreach (var fileName in fileNames)
            {
                var finfo = new FileInfo(fileName);

                if (pcapReader.CanRead(fileName))
                {
                    var reader = new PCAPReader();
                    var fileReadObjects = reader.Read(fileName);
                    var first = (PCAPBlock) fileReadObjects.First().ReadObject;
                    var last = (PCAPBlock) fileReadObjects.Last().ReadObject;

                    var dsource = new DataSource
                    {
                        FileInfo = finfo,
                        StartTime = first.DateTime,
                        EndTime = last.DateTime,
                        SourceType = SourceType.PCAP,
                        Packets = fileReadObjects.Count
                    };
                    UpdateList(dsource);
                    continue;
                }
                else if (pcapngReader.CanRead(fileName))
                {
                    var reader = new PCAPNGReader();
                    var fileReadObjects = reader.Read(fileName);
                    var first = (PCAPNGBlock) fileReadObjects.First().ReadObject;
                    var last = (PCAPNGBlock) fileReadObjects.Last().ReadObject;
                    var dsource = new DataSource
                    {
                        FileInfo = finfo,
                        StartTime = first.Timestamp,
                        EndTime = last.Timestamp,
                        SourceType = SourceType.PCAPNG,
                        Packets = fileReadObjects.Count
                    };
                    UpdateList(dsource);
                    continue;
                }

                if (SevenZipArchive.IsSevenZipFile(fileName))
                {
                    // because of lousy performance when operating on 7zip, we launch a new thread instead of using tasks
                    var thread = new Thread(() => DoSevenZip(finfo));
                    thread.Start();
                    threads.Add(thread);


                    continue;
                }

                // zip performance is much better and we will not thread
                DoZip(finfo);
            }

            UpdateList("Waiting for all threads to finish");
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            UpdateList("All threads finished");
        }

        private void DoSevenZip(FileInfo finfo)
        {
            using (SevenZipArchive sevenZipArchive = SevenZipArchive.Open(finfo.FullName))
            {
                using (var reader = sevenZipArchive.ExtractAllEntries())
                {
                    UpdateList("Opening " + reader.ArchiveType + ": " + finfo.FullName);
                    PeekReader(reader, finfo);
                }
            }

            GC.Collect();
        }

        private void DoZip(FileInfo finfo)
        {
            try
            {
                using (var filestream = File.OpenRead(finfo.FullName))
                {
                    using (var reader = ReaderFactory.Open(filestream))
                    {
                        UpdateList("Opening " + reader.ArchiveType + ": " + finfo.FullName);
                        PeekReader(reader, finfo);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                //UpdateList(ex.ToString());
            }

            GC.Collect();
        }

        private void PeekReader(IReader reader, FileInfo finfo)
        {
            //try
            {
                while (reader.MoveToNextEntry())
                {
                    if (!reader.Entry.IsDirectory)
                    {
                        var readbytes = new byte[4];
                        using (var entryStream = reader.OpenEntryStream())
                        {
                            entryStream.Read(readbytes, 0, 4);

                            var dsource = new DataSource
                            {
                                FileInfo = finfo,
                                SourceType = SourceType.Zip,
                                ArchiveKey = reader.Entry.Key
                            };


                            if (IsPCAP(readbytes))
                            {
                                var memstream = MemStream(readbytes, entryStream);

                                var pcapreader = new PCAPReader();
                                var fileReadObjects = pcapreader.ReadStream(memstream);
                                var first = (PCAPBlock) fileReadObjects.First().ReadObject;
                                var last = (PCAPBlock) fileReadObjects.Last().ReadObject;
                                dsource.StartTime = first.DateTime;
                                dsource.EndTime = last.DateTime;
                                dsource.Packets = fileReadObjects.Count();
                                dsource.ArchiveSourceType = SourceType.PCAP;
                                UpdateList(dsource);
                            }

                            if (IsPCAPNG(readbytes))
                            {
                                var memstream = MemStream(readbytes, entryStream);

                                var pcapngreader = new PCAPNGReader();
                                var fileReadObjects = pcapngreader.ReadStream(memstream);
                                var first = (PCAPNGBlock) fileReadObjects.First().ReadObject;
                                var last = (PCAPNGBlock) fileReadObjects.Last().ReadObject;
                                dsource.StartTime = first.Timestamp;
                                dsource.EndTime = last.Timestamp;
                                dsource.Packets = fileReadObjects.Count();
                                dsource.ArchiveSourceType = SourceType.PCAPNG;
                                UpdateList(dsource);
                            }
                        }
                    }
                }
            }
            //catch(Exception e)
            {
            }
        }

        public static MemoryStream MemStream(byte[] bytes, Stream input)
        {
            var output = new MemoryStream();
            output.Write(bytes, 0, 4);
            byte[] buffer = new byte[16 * 1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);

                // TODO out of memory exception
                // \LOGS\Heathrow FLU19 20190118\Wire Shark.7z
                // memstream runs out at 1074003968
                // seen the length gone up to 1073987588
            }

            output.Position = 0;
            return output;
        }

        private delegate void VoidStringDelegate(string text);

        private delegate void VoidDSDelegate(DataSource dataSource);

        private void UpdateList(string text)
        {
            if (InvokeRequired)
                Invoke(new VoidStringDelegate(UpdateList), text);
            else
            {
                listBox1.Items.Add(text);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
        }

        private void UpdateList(DataSource source)
        {
            UpdateList(source.ToString());
            AddSource(source);
        }

        private void AddSource(DataSource dataSource)
        {
            if (InvokeRequired)
                Invoke(new VoidDSDelegate(AddSource), dataSource);
            else
            {
                _dataSources.Add(dataSource);
                var list = new List<DateTime>();
                foreach (var source in _dataSources)
                {
                    list.Add(source.StartTime);
                    list.Add(source.EndTime);
                }

                datePicker1.Update(list);
            }
        }

        private bool IsPCAP(byte[] bytes)
        {
            return bytes[0] == 0xd4 && bytes[1] == 0xc3 && bytes[2] == 0xb2 && bytes[3] == 0xa1 || bytes[0] == 0xa1 &&
                bytes[1] == 0xb2 && bytes[2] == 0xc3 && bytes[3] == 0xd4;
        }

        private bool IsPCAPNG(byte[] bytes)
        {
            return bytes[0] == '\n' && bytes[1] == '\r' && bytes[2] == '\r' && bytes[3] == '\n';
        }

        private List<string> Uniquify(List<string> strings)
        {
            var unique_items = new HashSet<string>(strings);
            var removed = strings.Count - unique_items.Count;
            if (removed > 0)
                UpdateList($"Removed {removed} duplicates");
            return unique_items.ToList<string>();
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateList("BackgroundWorker finished");
            buttonGO.Enabled = true;
            buttonMerge.Enabled = true;
        }

        private void SetData()
        {
            this.DataSources = _dataSources.Where(d => d.Use).ToList();
            this.DateTimeFrom = datePicker1.From;
            this.DateTimeTo = datePicker1.To;
        }

        private void ButtonGO_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            SetData();

            this.Close();
        }

        private void ButtonMerge_Click(object sender, EventArgs e)
        {
            SetData();

            var dialog = new SaveFileDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var fileName = dialog.FileName;

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
                        pcapWriter.LinkLayerType = (uint) raw.LinkLayer;

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
    }
}