using BustPCap;
using IPTComShark.BackStore;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using PCAPWriter = BustPCap.PCAPWriter;

namespace IPTComShark.FileManager
{
    public partial class FileOpener : Form
    {
        private string[] _inputstrings;

        private BindingList<DataSource> _dataSources = new BindingList<DataSource>();

        public List<DataSource> DataSources { get; private set; }
        public DateTime DateTimeFrom { get; private set; }
        public DateTime DateTimeTo { get; private set; }
        public ProcessingFilter ProcessingFilters { get; private set; }

        public FileOpener(string[] inputs)
        {
            InitializeComponent();

            _inputstrings = inputs;

            dataListView1.DataSource = _dataSources;
            dataListView1.PrimarySortColumn = olvColumnStart;

            Load += (object sender, EventArgs e) => { backgroundWorker1.RunWorkerAsync(); };

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
            var threadCount = 0;
            var threads = new List<Thread>();

            foreach (var fileName in fileNames)
            {
                var finfo = new FileInfo(fileName);

                var first4 = new byte[4];
                using (FileStream fileStream = File.OpenRead(fileName))
                {
                    fileStream.Read(first4, 0, 4);
                }




                if (BaseReader.IsPCAP(first4))
                {
                    ThreadPool.QueueUserWorkItem(x =>
                    {
                        Interlocked.Increment(ref threadCount);

                        using (var pcapFileReader = new PCAPFileReader(fileName))
                        {
                            foreach (var pcapBlock in pcapFileReader.Enumerate())
                            {
                                // enumerate to gather info
                            }

                            var dsource = new DataSource
                            {
                                FileInfo = finfo,
                                StartTime = pcapFileReader.StartTime,
                                EndTime = pcapFileReader.EndTime,
                                SourceType = SourceType.PCAP,
                                Packets = pcapFileReader.Count
                            };
                            UpdateList(dsource);
                        }

                        Interlocked.Decrement(ref threadCount);
                    });

                    continue;
                }

                if (BaseReader.IsPCAPNG(first4))
                {
                    ThreadPool.QueueUserWorkItem(x =>
                    {
                        Interlocked.Increment(ref threadCount);

                        using (var pcapFileReader = new PCAPNGFileReader(fileName))
                        {
                            foreach (var pcapBlock in pcapFileReader.Enumerate())
                            {
                                // enumerate to gather info
                            }

                            var dsource = new DataSource
                            {
                                FileInfo = finfo,
                                StartTime = pcapFileReader.StartTime,
                                EndTime = pcapFileReader.EndTime,
                                SourceType = SourceType.PCAPNG,
                                Packets = pcapFileReader.Count
                            };
                            UpdateList(dsource);




                        }

                        Interlocked.Decrement(ref threadCount);
                    });

                    continue;

                    

                }

                if (first4[3] == 0xFD && first4[2] == 0x2F && first4[1] == 0xB5 && first4[0] == 0x28)
                {
                    // Zstandard
                    continue;
                }


                if (SevenZipArchive.IsSevenZipFile(fileName))
                {
                    ThreadPool.QueueUserWorkItem(x =>
                    {
                        Interlocked.Increment(ref threadCount);

                        DoSevenZip(finfo);

                        Interlocked.Decrement(ref threadCount);
                    });

                    // because of lousy performance when operating on 7zip, we launch a new thread instead of using tasks
                    //var thread = new Thread(() => DoSevenZip(finfo));
                    //thread.Start();
                    //threads.Add(thread);

                    continue;
                }

                                
                ThreadPool.QueueUserWorkItem(x =>
                {
                    Interlocked.Increment(ref threadCount);

                    DoZip(finfo);

                    Interlocked.Decrement(ref threadCount);
                });
               
            }

            // Give all threads a chance to start
            Thread.Sleep(100);
                        
            while(threadCount > 0)
            {
                UpdateList("Processing threads: " + threadCount.ToString());
                Thread.Sleep(1000);
            }
            //foreach (Thread thread in threads)
            //{
            //    thread.Join();
            //}

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
            try
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


                            if (BaseReader.IsPCAP(readbytes))
                            {
                                var memstream = MemStream(readbytes, entryStream);

                                var pcapStreamReader = new PCAPStreamReader(memstream);

                                foreach (var pcapBlock in pcapStreamReader.Enumerate())
                                {
                                    // enumerate to gather info
                                }

                                dsource.StartTime = pcapStreamReader.StartTime;
                                dsource.EndTime = pcapStreamReader.EndTime;
                                dsource.Packets = pcapStreamReader.Count;
                                dsource.ArchiveSourceType = SourceType.PCAP;
                                UpdateList(dsource);
                            }

                            if (BaseReader.IsPCAPNG(readbytes))
                            {
                                var memstream = MemStream(readbytes, entryStream);

                                var pcapngreader = new PCAPNGStreamReader(memstream);

                                foreach (var pcapBlock in pcapngreader.Enumerate())
                                {
                                    // enumerate to gather info
                                }

                                dsource.StartTime = pcapngreader.StartTime;
                                dsource.EndTime = pcapngreader.EndTime;
                                dsource.Packets = pcapngreader.Count;
                                dsource.ArchiveSourceType = SourceType.PCAPNG;
                                UpdateList(dsource);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                UpdateList(finfo.FullName + " " + e.Message);
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

            this.ProcessingFilters = new ProcessingFilter();
            if (!string.IsNullOrWhiteSpace(textBoxExcludeIP.Text))
            {
                string[] vs = textBoxExcludeIP.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                ProcessingFilters.ExcludeIPs = new List<IPAddress>();
                foreach (var stringip in vs)
                {
                    if (IPAddress.TryParse(stringip, out var ip))
                        ProcessingFilters.ExcludeIPs.Add(ip);
                    else
                        MessageBox.Show("Discarded " + stringip);
                }
            }
            if (!string.IsNullOrWhiteSpace(textBoxIncludeIP.Text))
            {
                string[] vs = textBoxIncludeIP.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                ProcessingFilters.IncludeIPs = new List<IPAddress>();
                foreach (var stringip in vs)
                {
                    if (IPAddress.TryParse(stringip, out var ip))
                        ProcessingFilters.IncludeIPs.Add(ip);
                    else
                        MessageBox.Show("Discarded " + stringip);
                }
            }


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
    }
}