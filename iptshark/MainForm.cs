using IPTComShark.Import;
using IPTComShark.Windows;
using SharpPcap;
using BitDataParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using IPTComShark.DataSets;
using IPTComShark.Export;
using SharpPcap.LibPcap;
using IPTComShark.Parsers;
using BustPCap;
using static IPTComShark.Classes.Conversions;
using System.Text.Json;
using System.Numerics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using IPTComShark.FileManager;

namespace IPTComShark
{
    public partial class MainForm : Form
    {
        private BackStore.BackStore _backStore;
        private static ParserFactory _parserFactory;

        private long _capturedData;

        private PcapDevice _device;
        
        //private PCAPWriter _pcapWriter;

        public MainForm()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            InitializeComponent();

            _parserFactory = GenerateParserFactory();

            _backStore = new BackStore.BackStore(_parserFactory);

            packetListView1.BackStore = _backStore;
            packetListView1.ParserFactory = _parserFactory;
            packetDisplay1.BackStore = _backStore;
            packetDisplay1.ParserFactory = _parserFactory;

            Text = Text += " " + Application.ProductVersion + " Cliff's Conundrum";

            Logger.Instance.LogAdded += (sender, log) => UpdateStatus(log.ToString());


            packetListView1.PacketSelected += (sender, args) => packetDisplay1.SetObject(args.Packet);

            var packetSettings = IPTComShark.Controls.PacketListSettings.DeserializeString(Properties.Settings.Default.PacketListSettings);

            checkBoxAutoScroll.DataBindings.Add("Checked", packetSettings, "AutoScroll", true,
                DataSourceUpdateMode.OnPropertyChanged);
            checkBoxIgnoreLoopback.DataBindings.Add("Checked", packetSettings, "IgnoreLoopback", true,
                DataSourceUpdateMode.OnPropertyChanged);
            checkBoxHideDupes.DataBindings.Add("Checked", packetSettings, "IgnoreDuplicatedPD", true,
                DataSourceUpdateMode.OnPropertyChanged);
            checkBoxParserOnly.DataBindings.Add("Checked", packetSettings, "IgnoreUnknownData", true,
                DataSourceUpdateMode.OnPropertyChanged);
            textBoxIgnoreComid.DataBindings.Add("Text", packetSettings, "IgnoreComid", true,
                DataSourceUpdateMode.OnValidation);
            textBoxIgnoreVars.DataBindings.Add("Lines", packetSettings, "IgnoreVariables", true, 
                DataSourceUpdateMode.OnValidation);
            
            packetListView1.Settings = packetSettings;

            InitData();

            _backStore.NewCapturePacket += (sender, packet) => packetListView1.AddRange(packet);

            stopwatch.Stop();
            Logger.Log("IPTComShark started in " + stopwatch.ElapsedMilliseconds + "ms", Severity.Info);
        }

        public static ParserFactory GenerateParserFactory()
        {
            var parserFactory = new ParserFactory();
            parserFactory.AddParser(new NTPParser());
            parserFactory.AddParser(new SPLParser());
            parserFactory.AddParser(new JRUParser());
            parserFactory.AddParser(new ARPParser());
            parserFactory.AddParser(new ProfiParser());

            parserFactory.AddParser(new IPTWPParser(Path.Combine(Environment.CurrentDirectory, "IPTXMLFiles")));

            return parserFactory;
        }

        private void InitData()
        {
            packetListView1.Clear();
            _backStore.Clear();


            _capturedData = 0;            
        }

        private void UpdateStatus(string text)
        {
            if (InvokeRequired)
                Invoke(new UpdateStatusDelegate(UpdateStatus), text);
            else
            {
                statusRight.Text = text;
                List<LogRoll> logs = Logger.Instance.GetLog();
                var lastLogs = logs.Skip(Math.Max(0, logs.Count - 10)).ToList();
                statusRight.ToolTipText = string.Join(Environment.NewLine, lastLogs);
            }
        }

        //private delegate void AddToListDelegate(CapturePacket o);


        private void device_OnPacketArrival(object sender, PacketCapture pc)
        {
            var packet = pc.GetPacket();
            _capturedData += packet.Data.Length;
            var raw = new Raw(packet.Timeval.Date, packet.Data, (LinkLayerType) packet.LinkLayerType);

            _backStore.AddAsync(raw);

            //_pcapWriter.WritePacket(raw.RawData, raw.TimeStamp);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void Start()
        {
            fileToolStripMenuItem.Enabled = false;

            startToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = true;

            //_pcapWriter = new PCAPWriter(@"c:\temp", "testfile");
            //_pcapWriter.RotationTime = 30;
            //_pcapWriter.Start();


            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Stop();
            //_pcapWriter.Stop();
        }

        private void Stop()
        {
            fileToolStripMenuItem.Enabled = true;

            startToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = false;

            try
            {
                _device.StopCapture();
            }
            catch (Exception exception)
            {
                Logger.Log(exception.Message, Severity.Error);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_device == null)
            {
                // Retrieve the device list
                var devices = CaptureDeviceList.Instance.ToList();

                // If no devices were found print an error
                if (devices.Count < 1)
                    return;

                List<PcapDevice> captureDevices =
                    devices.Where(d => d is PcapDevice).Cast<PcapDevice>().ToList();

                // TODO try and restore this old behaviour somehow
                //captureDevices.RemoveAll(d => d.Loopback || d.Addresses.Count == 0);


                var interfacePicker = new InterfacePicker(captureDevices);
                interfacePicker.ShowDialog();

                if (!interfacePicker.PressedYes)
                    return;
                if (interfacePicker.SelectedDevice == null)
                    return;

                // Print out the available network devices
                _device = interfacePicker.SelectedDevice;

                // Register our handler function to the
                // 'packet arrival' event
                _device.OnPacketArrival +=
                    device_OnPacketArrival;
            }


            UpdateStatus("Recording from " + _device.Interface.FriendlyName);


            // Open the device for capturing
            var readTimeoutMilliseconds = 1000;
            _device.Open(DeviceModes.Promiscuous, readTimeoutMilliseconds);


            // Start the capturing process
            _device.StartCapture();
        }

        private void timerFlicker_Tick(object sender, EventArgs e)
        {
            var totalMemory = GC.GetTotalMemory(false);

            Process proc = Process.GetCurrentProcess();
            var memorySize64 = proc.PrivateMemorySize64;


            var sizestring = "";
            if (_capturedData > 1024 * 1024)
                sizestring = _capturedData / 1024 / 1024 + " mb";
            else if (_capturedData > 1024)
                sizestring = _capturedData / 1024 + " kb";
            else
                sizestring = _capturedData + " b";

            //var sizestring2 = "";
            //if (_discardedData > 1024 * 1024)
            //    sizestring2 = _discardedData / 1024 / 1024 + " mb";
            //else if (_discardedData > 1024)
            //    sizestring2 = _discardedData / 1024 + " kb";
            //else
            //    sizestring2 = _discardedData + " b";
            //statusLeft.Text = packetListView1.Count() + " captured packets, " + sizestring + ". " + _discaredPackets +
            //                  " discarded packets, " + sizestring2 + ". " + memorystring + ".";

            

            var tuples = new List<Tuple<Color, string>>();
            tuples.Add(new Tuple<Color, string>(Color.DarkRed, _backStore.CapturedPackets.ToString()));
            tuples.Add(new Tuple<Color, string>(Color.Black, " captured packets |"));
            tuples.Add(new Tuple<Color, string>(Color.DarkRed, sizestring));
            tuples.Add(new Tuple<Color, string>(Color.Black, "|"));
            tuples.Add(new Tuple<Color, string>(Color.DarkRed, _backStore.DiscardedPackets.ToString()));
            tuples.Add(new Tuple<Color, string>(Color.Black, "discarded packets | GC mem:"));
            tuples.Add(new Tuple<Color, string>(Color.DarkRed, PrettyPrintSize(totalMemory)));
            tuples.Add(new Tuple<Color, string>(Color.Black, "| Proc mem:"));
            tuples.Add(new Tuple<Color, string>(Color.DarkRed, PrettyPrintSize(memorySize64)));
            tuples.Add(new Tuple<Color, string>(Color.Black, "|"));


            var renderStatusText = RenderStatusText(statusLeft.BackColor, statusLeft.Height, statusLeft.Font, tuples);

            statusLeft.Image = renderStatusText;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Close the pcap device
                _device?.Close();
            }
            catch (Exception exception)
            {
                Logger.Log(exception.Message, Severity.Error);
            }

            Properties.Settings.Default.FormState = (int)this.WindowState;
            if (this.WindowState == FormWindowState.Normal)
            {
                // save location and size if the state is normal
                Properties.Settings.Default.FormLocation = this.Location;
                Properties.Settings.Default.FormSize = this.Size;
            }
            else
            {
                // save the RestoreBounds if the form is minimized or maximized!
                Properties.Settings.Default.FormLocation = this.RestoreBounds.Location;
                Properties.Settings.Default.FormSize = this.RestoreBounds.Size;
            }

            Properties.Settings.Default.PacketListSettings = packetListView1.Settings.SerialiseToString();
                        
            Properties.Settings.Default.Save();

            _backStore.Close();
        }

        private void buttonSaveAll_Click(object sender, EventArgs e)
        {
            List<Raw> allRawCaptures = _backStore.GetAllRaws();
            if (allRawCaptures.Count == 0)
                return;

            LinkLayerType layer = allRawCaptures.First().LinkLayer;
            if (!allRawCaptures.TrueForAll(raw => raw.LinkLayer == layer))
            {
                MessageBox.Show(
                    "The contained data consists of different Link Layer types, please use PCAPNG format instead");
            }

            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "pcap"
            };
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK) return;
            using (var pcapWriter = new BustPCap.PCAPWriter(saveFileDialog.FileName))
            {
                pcapWriter.LinkLayerType = (uint) layer;
                pcapWriter.Start();
                foreach (Raw raw in allRawCaptures)
                {
                    pcapWriter.WritePacket(raw.RawData, raw.TimeStamp);
                }
            }

            // TODO FOR TEST ONLY
            //using (var fstream = new FileStream(saveFileDialog.FileName + ".binary", FileMode.OpenOrCreate, FileAccess.Write))
            //{
            //    var bWriter = new BinaryFormatter();
            //    bWriter.Serialize(fstream, allRawCaptures);
            //}
            //
            //using (var fstream2 = new FileStream(saveFileDialog.FileName + ".binary2", FileMode.OpenOrCreate, FileAccess.Write))
            //{
            //    var bWriter2 = new BinaryFormatter();
            //    var capturePacket = packetListView1.GetAllPackets().First(p => p.ParsedData != null);
            //    bWriter2.Serialize(fstream2, packetListView1.GetAllPackets());
            //}
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            Stop();
            InitData();
            Start();
        }

        private void checkBoxAutoScroll_CheckedChanged(object sender, EventArgs e)
        {
        }


        private delegate void UpdateStatusDelegate(string text);


        private void openFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openCapturesDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                OpenPath(openCapturesDialog.FileNames);
            }
        }

        public delegate void OpenPathDelegate(string[] paths);

        public void OpenPath(string[] paths)
        {
            if (InvokeRequired)
                Invoke(new OpenPathDelegate(OpenPath), paths);
            else
            {
                {
                    var fileManager = new FileManager.FileManager();
                    fileManager.RawParsed += (sender, raw) => _backStore.AddAsync(raw);

                    fileManager.OpenFilesAsync(paths);
                    _backStore.ProcessingFilters = fileManager.ProcessingFilters;
                    //fileManager.OpenFilesAsyncFinished.WaitOne();
                }

                GC.Collect();
            }
        }

        private void saveCurrentFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filtered = packetListView1.GetFilteredPackets();

            var dialog = new SaveFileDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var fileName = dialog.FileName;

                FileManager.FileManager.SaveToFile(fileName, filtered, _backStore);
            }
        }

        private void simulateTrafficToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var trafficSim = new TrafficSim();
            trafficSim.Show(this);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitData();

            packetDisplay1.Clear();

            // and then go all in on the garbagecollection
            GC.Collect(2, GCCollectionMode.Forced, true, true);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //GitHubUpdateCheck.GetLatestVersionAndPromptAsync("dyster", "IPTComShark", Application.ProductVersion);

            if (Properties.Settings.Default.FormSize.Width == 0 || Properties.Settings.Default.FormSize.Height == 0)
            {
                // first start
                // optional: add default values
            }
            else
            {
                this.WindowState = (FormWindowState)Properties.Settings.Default.FormState;

                // we don't want a minimized window at startup
                if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;

                this.Location = Properties.Settings.Default.FormLocation;
                this.Size = Properties.Settings.Default.FormSize;
            }

            timerFlicker.Enabled = true;
        }

        private void exportSVGSequenceDiagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "svg"
            };
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                SeqDiagram.SeqDiagramExporter.MakeSVG(packetListView1.GetFilteredPackets(), saveFileDialog.FileName,
                    _backStore, _parserFactory);
            }
        }

        private void exportXLSXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
                var exporterer = new Exporterer(packetListView1.GetAllPackets(), packetListView1.GetFilteredPackets(), packetListView1.GetSelectedPackets(), _backStore, _parserFactory);
                var showDialog = exporterer.ShowDialog(this);
            
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            var dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                OpenPath(new[] {folderBrowserDialog.SelectedPath});
            }
        }

        private void eVA2XMLExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var hasler = new Hasler();
                if (hasler.CanImport(openFileDialog.FileName))
                {
                    foreach (var capturePacket in hasler.Import(openFileDialog.FileName))
                    {
                        packetListView1.Add(capturePacket);
                    }
                }
                else
                {
                    MessageBox.Show("Can't import the selected file!");
                }
            }
        }

        private void reportAnIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/dyster/IPTComShark/issues");
        }

        private void packetListView1_DragEnter(object sender, DragEventArgs e)
        {
            var formats = e.Data.GetFormats();
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void packetListView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] data = (string[]) e.Data.GetData(DataFormats.FileDrop);
                OpenPath(data);
            }
        }

        private void RemoteCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new RemoteCap(this).Show();
        }

        public static Image RenderStatusText(Color backcolor, int height, Font font, List<Tuple<Color, string>> texts)
        {
            var totalMemory = GC.GetTotalMemory(false);

            var img = new Bitmap(200, height);
            Graphics gx = Graphics.FromImage(img);


            int totalwidth = 0;
            foreach (var pair in texts)
            {
                totalwidth += (int) gx.MeasureString(pair.Item2, font).Width;
            }


            img = new Bitmap(totalwidth, height);
            gx = Graphics.FromImage(img);
            SolidBrush brush = new SolidBrush(backcolor);
            gx.FillRectangle(brush, 0, 0, img.Width, img.Height);

            int renderbar = 0;

            foreach (var pair in texts)
            {
                var piecewidth = gx.MeasureString(pair.Item2, font).Width;
                var piecebrush = new SolidBrush(pair.Item1);
                gx.DrawString(pair.Item2, font, piecebrush, renderbar, 0);

                renderbar += (int) piecewidth;
            }

            return img;
        }

        private void bDSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileDropper = new FileDropper();
            fileDropper.ShowDialog(this);
            var files = fileDropper.DroppedFiles;

            var regex = new Regex(
                @"^(LOG|ERR|WRN|FTL),(TXT|BIN|BDS|ELG),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),([\d:_; ]+),\s*(\d+),\s*(\d+),\s*(\d+),(0x\d+),(0x\d+),(.*)$");

            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file);
                foreach (var line in lines)
                {
                    var match = regex.Match(line);

                    if (match.Groups.Count != 15)
                    {
                        // stuff we missed
                    }
                    else
                    {
                        var NC_SEVERITY = match.Groups[1].Value;
                        var N_COMMAND = match.Groups[2].Value;
                        var L_PACKET = int.Parse(match.Groups[3].Value);
                        var N_VER = int.Parse(match.Groups[4].Value);
                        var ServiceID = int.Parse(match.Groups[5].Value);
                        var deviceID = int.Parse(match.Groups[6].Value);
                        var N_SEQ = int.Parse(match.Groups[7].Value);
                        var UTC_REFTIME = match.Groups[8].Value;
                        var UTC_OFFSET = int.Parse(match.Groups[9].Value);
                        var T_REFTIME = int.Parse(match.Groups[10].Value);
                        var Receive_Time = int.Parse(match.Groups[11].Value);
                        var Classification = match.Groups[12].Value;
                        var MessageId = match.Groups[13].Value;
                        var M_MSG = match.Groups[14].Value;

                        if (ServiceID == 208 && deviceID == 200)
                        {
                            continue;
                            var hexes = M_MSG.Split(',');
                            var bytes = new byte[hexes.Length];
                            for (var index = 0; index < hexes.Length; index++)
                            {
                                var hex = hexes[index];
                                bytes[index] = Convert.ToByte(hex.Substring(2, 2), 16);
                            }

                            var action = new byte[32];
                            Array.Copy(bytes, 14, action, 0, 32);

                            var parsedDataSet = ETCSDiag.DIA_130.Parse(action);

                            var capturePacket = new CapturePacket(ProtocolType.Virtual, "BDS 130", DateTime.Now);
                            // TODO no idea if this works now
                            capturePacket.DisplayFields.AddRange(
                                parsedDataSet.ParsedFields.Select(pf => new DisplayField(pf)));
                            packetListView1.Add(capturePacket);
                        }
                        else if (ServiceID == 205 && deviceID == 3)
                        {
                            var hexes = M_MSG.Split(',');
                            var bytes = new byte[hexes.Length];
                            for (var index = 0; index < hexes.Length; index++)
                            {
                                var hex = hexes[index];
                                bytes[index] = Convert.ToByte(hex.Substring(2, 2), 16);
                            }

                            var V_NOM = BitConverter.ToInt16(new byte[] {bytes[13], bytes[12]}, 0);

                            var capturePacket = new CapturePacket(ProtocolType.Virtual, "BDS ODO", DateTime.Now);
                            var parsedDataSet = ParsedDataSet.CreateError("V_NOM is " + V_NOM);
                            // TODO no idea if this works either now
                            capturePacket.DisplayFields.AddRange(
                                parsedDataSet.ParsedFields.Select(pf => new DisplayField(pf)));

                            packetListView1.Add(capturePacket);
                        }
                    }
                }
            }
        }

        private void textBoxIgnoreVars_TypingFinished(object sender, EventArgs e)
        {
            packetListView1.UpdateFilter();
        }

        private void typeDelayTextBox1_TypingFinished(object sender, EventArgs e)
        {
            packetListView1.SearchString = textBoxSearch.Text;
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.Control && e.KeyCode == Keys.B)
            {
                GC.Collect();

                RunBenchmark(@"C:\Users\dyste\OneDrive - Alstom\07_Problems\07_mr9 random crash\1\vap 1820-1830.pcap");

                return;
                RunBenchmark(@"c:\temp\benchmark1.pcap");

                clearToolStripMenuItem_Click(this, null);
                RunBenchmark(@"c:\temp\benchmark2.pcapng");

                clearToolStripMenuItem_Click(this, null);
                RunBenchmark(@"c:\temp\benchmark3.pcap");

                //clearToolStripMenuItem_Click(this, null);
                //RunBenchmark(@"c:\temp\benchmark4.pcap");

                clearToolStripMenuItem_Click(this, null);
                RunBenchmark(@"c:\temp\benchmark5.pcap");
            }
        }


        private static void RunBenchmark(string file)
        {
            var totalMemory = GC.GetTotalMemory(false);

            Process myProcess = Process.GetCurrentProcess();

            var privateMemorySize64 = myProcess.PrivateMemorySize64;
            var workingSet64 = myProcess.WorkingSet64;
            var pagedSystemMemorySize64 = myProcess.PagedSystemMemorySize64;
            var pagedMemorySize64 = myProcess.PagedMemorySize64;

            var userProcessorTime = myProcess.UserProcessorTime;
            var privilegedProcessorTime = myProcess.PrivilegedProcessorTime;
            var totalProcessorTime = myProcess.TotalProcessorTime;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var list = new List<Tuple<CapturePacket, Parse>>();
            var count = 0;

            var processor = new BackStore.BackStore(_parserFactory, true);

            var format = BaseReader.CanRead(file);
            if (format == Format.PCAP)
            {
                var pcapReader = new PCAPFileReader(file);
                foreach (var block in pcapReader.Enumerate())
                {
                    var packet = processor.Add(new Raw(block.DateTime, block.PayLoad, (LinkLayerType)block.Header.network), out var notused);
                    list.Add(new Tuple<CapturePacket, Parse>(packet, notused));
                    count++;
                }
            }
            else if(format == Format.PCAPNG)
            {
                var pcapReader = new PCAPNGFileReader(file);
                foreach (var block in pcapReader.Enumerate())
                {
                    if(block.Header == PCAPNGHeader.EnhancedPacket)
                    {
                        var packet = processor.Add(new Raw(block.DateTime, block.PayLoad, (LinkLayerType)block.LinkLayerType), out var notused);
                        list.Add(new Tuple<CapturePacket, Parse>(packet, notused));
                        count++;
                    }
                    
                }
            }
                       

            stopwatch.Stop();

            myProcess = Process.GetCurrentProcess();

            var totalMemory2 = GC.GetTotalMemory(false);

            var privateMemorySize64_2 = myProcess.PrivateMemorySize64;
            var workingSet64_2 = myProcess.WorkingSet64;
            var pagedSystemMemorySize64_2 = myProcess.PagedSystemMemorySize64;
            var pagedMemorySize64_2 = myProcess.PagedMemorySize64;

            var userProcessorTime_2 = myProcess.UserProcessorTime;
            var privilegedProcessorTime_2 = myProcess.PrivilegedProcessorTime;
            var totalProcessorTime_2 = myProcess.TotalProcessorTime;


            var peakPagedMem = myProcess.PeakPagedMemorySize64;
            var peakVirtualMem = myProcess.PeakVirtualMemorySize64;
            var peakWorkingSet = myProcess.PeakWorkingSet64;

            var text = "-------------------------------------" + Environment.NewLine;
            text += DateTime.Now.ToString() + "  " + Application.ProductVersion + Environment.NewLine;
            text += "GC memory increase = " + PrettyPrintSize(totalMemory2 - totalMemory) +
                    Environment.NewLine;
            text += "PrivateMemorySize64 increase = " +
                    PrettyPrintSize(privateMemorySize64_2 - privateMemorySize64) + Environment.NewLine;
            text += "WorkingSet64 increase = " + PrettyPrintSize(workingSet64_2 - workingSet64) +
                    Environment.NewLine;
            text += "PagedSystemMemorySize64 increase = " +
                    PrettyPrintSize(pagedSystemMemorySize64_2 - pagedSystemMemorySize64) +
                    Environment.NewLine;
            text += "PagedMemorySize64 increase = " +
                    PrettyPrintSize(pagedMemorySize64_2 - pagedMemorySize64) + Environment.NewLine;

            text += "UserProcessorTime increase = " + (userProcessorTime_2 - userProcessorTime) + Environment.NewLine;
            text += "PrivilegedProcessorTime increase = " + (privilegedProcessorTime_2 - privilegedProcessorTime) +
                    Environment.NewLine;
            text += "TotalProcessorTime increase = " + (totalProcessorTime_2 - totalProcessorTime) +
                    Environment.NewLine;

            text += "Time taken " + stopwatch.Elapsed + Environment.NewLine;

            //MessageBox.Show(text);

            File.AppendAllText(file + ".txt", text);

            var options = new JsonSerializerOptions() { 
                WriteIndented = true, 
                Converters = { new Classes.IPJsonConverter()}
            };
            var json = JsonSerializer.Serialize(list, typeof(List<Tuple<CapturePacket, Parse>>), options);
            File.WriteAllText(file + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".json", json);
        }

        private void statusStrip1_DoubleClick(object sender, EventArgs e)
        {
            var textWindow = new TextWindow(string.Join(Environment.NewLine,
                Logger.Instance.GetLog().Select(log => log.ToString())));
            textWindow.Show(this);
        }

        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            bool topWindow = this.TopMost;

            alwaysOnTopToolStripMenuItem.Checked = topWindow;
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool allowedKey;

        private void textBoxIgnoreComid_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg = String.Empty;

            if (!String.IsNullOrEmpty(textBoxIgnoreComid.Text))
            {
                if (!ValidateComIDs(textBoxIgnoreComid.Text, out errorMsg))
                {
                    // Cancel the event and select the text to be corrected by the user.
                    e.Cancel = true;
                    textBoxIgnoreComid.Select(0, textBoxIgnoreComid.Text.Length);

                    // Set the ErrorProvider error with the text to display. 
                    errorProvider1.SetError(textBoxIgnoreComid, errorMsg);
                }
                else
                {
                    errorProvider1.SetError(textBoxIgnoreComid, "");
                }
            }
            else
            {
                errorProvider1.SetError(textBoxIgnoreComid, "");
            }
        }

        private void textBoxIgnoreComid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                // Allow digits and separator chars.
                allowedKey = true;
            }
            else
                allowedKey = false;
        }

        private void textBoxIgnoreComid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || (e.KeyChar == ',') || allowedKey)
            {
                e.Handled = false;
            }
            else
            {
                // Allow digits and separator chars.
                e.Handled = true;
            }
        }

        private bool ValidateComIDs(string comIDs, out string errorMessage)
        {
            char[] separatorChars = { ',' };

            string[] tokens = comIDs.Split(separatorChars);

            // Format of the ComIDs string is numeric tokens (UINT32) separated by commas.
            // Should be able to use a regular expression to parse the tokens. Alternatively, use Split and TryParse() on the substrings.
            // Filter out any chars other than digits and comma.
            foreach (var t in tokens)
            {
                if (String.IsNullOrEmpty(t))
                {
                    errorMessage = "An empty Com ID is invalid";
                    return false;
                }
                else
                {
                    BigInteger tVal;
                    if (BigInteger.TryParse(t, out tVal))
                    {
                        if (tVal > UInt32.MaxValue)
                        {
                            errorMessage = "ComID: " + t + " is greater than UInt32.MaxValue";
                            return false;
                        }
                    }
                    else
                    {
                        // Parse failed.
                        errorMessage = "Token: " + t + " is not a valid ComID";
                        return false;
                    }

                }
            }
            errorMessage = String.Empty;
            return true;
        }


        //public enum Protocol
        //{
        //    Unknown,
        //    UDP,
        //    TCP,
        //    IPTWP
        //}
    }
}