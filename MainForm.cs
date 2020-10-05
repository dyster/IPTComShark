using IPTComShark.Import;
using IPTComShark.Windows;
using SharpPcap;
using sonesson_tools;
using sonesson_tools.BitStreamParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using IPTComShark.Classes;
using IPTComShark.DataSets;
using SharpPcap.Npcap;
using sonesson_tools.FileReaders;

namespace IPTComShark
{
    public partial class MainForm : Form
    {
        private BackStore _backStore;


        private long _capturedData;

        private NpcapDevice _device;
        private long _discardedData;
        private long _discardedPackets;


        //private PCAPWriter _pcapWriter;

        public MainForm()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            InitializeComponent();


            _backStore = new BackStore();

            packetListView1.BackStore = _backStore;
            packetDisplay1.BackStore = _backStore;

            Text = Text += " " + Application.ProductVersion + " codename \"Roger\"";

            Logger.Instance.LogAdded += (sender, log) => UpdateStatus(log.ToString());


            packetListView1.PacketSelected += (sender, args) => packetDisplay1.SetObject(args.Packet);

            checkBoxAutoScroll.DataBindings.Add("Checked", packetListView1.Settings, "AutoScroll", true,
                DataSourceUpdateMode.OnPropertyChanged);
            checkBoxIgnoreLoopback.DataBindings.Add("Checked", packetListView1.Settings, "IgnoreLoopback", true,
                DataSourceUpdateMode.OnPropertyChanged);
            checkBoxHideDupes.DataBindings.Add("Checked", packetListView1.Settings, "IgnoreDuplicatedPD", true,
                DataSourceUpdateMode.OnPropertyChanged);
            checkBoxParserOnly.DataBindings.Add("Checked", packetListView1.Settings, "IgnoreUnknownData", true,
                DataSourceUpdateMode.OnPropertyChanged);
            textBoxIgnoreComid.DataBindings.Add("Text", packetListView1.Settings, "IgnoreComid", true,
                DataSourceUpdateMode.OnValidation);

            packetListView1.Settings.IgnoreComid = Properties.Settings.Default.IgnoredComIds;
            packetListView1.Settings.AutoScroll = Properties.Settings.Default.AutoScroll;
            packetListView1.Settings.IgnoreDuplicatedPD = Properties.Settings.Default.IgnoreDuplicatedPD;
            packetListView1.Settings.IgnoreLoopback = Properties.Settings.Default.IgnoreLoopback;
            packetListView1.Settings.IgnoreUnknownData = Properties.Settings.Default.IgnoreUnknownData;

            InitData();

            _backStore.NewCapturePacket += (sender, packet) => packetListView1.AddRange(packet);

            stopwatch.Stop();
            Logger.Log("IPTComShark started in " + stopwatch.ElapsedMilliseconds + "ms", Severity.Info);
        }

        private void InitData()
        {
            packetListView1.Clear();
            _backStore.Clear();


            _capturedData = 0;
            _discardedData = 0;
            _discardedPackets = 0;
        }

        private void UpdateStatus(string text)
        {
            if (InvokeRequired)
                Invoke(new UpdateStatusDelegate(UpdateStatus), text);
            else
            {
                statusRight.Text = text;
                List<Log> logs = Logger.Instance.GetLog();
                var lastLogs = logs.Skip(Math.Max(0, logs.Count - 10)).ToList();
                statusRight.ToolTipText = string.Join(Environment.NewLine, lastLogs);
            }
        }

        //private delegate void AddToListDelegate(CapturePacket o);


        private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            _capturedData += e.Packet.Data.Length;
            var raw = new Raw(e.Packet.Timeval.Date, e.Packet.Data, (LinkLayerType) e.Packet.LinkLayerType);

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

                List<NpcapDevice> captureDevices =
                    devices.Where(d => d is NpcapDevice).Cast<NpcapDevice>().ToList();

                captureDevices.RemoveAll(d => d.Loopback || d.Addresses.Count == 0);


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
            _device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);


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

            var storeStatus = _backStore.Status;

            var tuples = new List<Tuple<Color, string>>();
            tuples.Add(new Tuple<Color, string>(Color.Black, storeStatus + " | "));
            tuples.Add(new Tuple<Color, string>(Color.DarkRed, packetListView1.Count().ToString()));
            tuples.Add(new Tuple<Color, string>(Color.Black, " captured packets |"));
            tuples.Add(new Tuple<Color, string>(Color.DarkRed, sizestring));
            tuples.Add(new Tuple<Color, string>(Color.Black, "|"));
            tuples.Add(new Tuple<Color, string>(Color.DarkRed, _discardedPackets.ToString()));
            tuples.Add(new Tuple<Color, string>(Color.Black, "discarded packets | GC mem:"));
            tuples.Add(new Tuple<Color, string>(Color.DarkRed, Functions.PrettyPrintSize(totalMemory)));
            tuples.Add(new Tuple<Color, string>(Color.Black, "| Proc mem:"));
            tuples.Add(new Tuple<Color, string>(Color.DarkRed, Functions.PrettyPrintSize(memorySize64)));
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

            Properties.Settings.Default.IgnoredComIds = packetListView1.Settings.IgnoreComid;
            Properties.Settings.Default.AutoScroll = packetListView1.Settings.AutoScroll;
            Properties.Settings.Default.IgnoreDuplicatedPD = packetListView1.Settings.IgnoreDuplicatedPD;
            Properties.Settings.Default.IgnoreLoopback = packetListView1.Settings.IgnoreLoopback;
            Properties.Settings.Default.IgnoreUnknownData = packetListView1.Settings.IgnoreUnknownData;
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
            using (var pcapWriter = new sonesson_tools.FileWriters.PCAPWriter(saveFileDialog.FileName))
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

                    //fileManager.OpenFilesAsyncFinished.WaitOne();
                }

                GC.Collect();
            }
        }

        private void saveCurrentFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void simulateTrafficToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var trafficSim = new TrafficSim();
            trafficSim.Show(this);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitData();

            // and then go all in on the garbagecollection
            GC.Collect(2, GCCollectionMode.Forced, true, true);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            GitHubUpdateCheck.GetLatestVersionAndPromptAsync("dyster", "IPTComShark", Application.ProductVersion);
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
                    _backStore);
            }
        }

        private void exportXLSXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "xlsx"
            };
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                Export.Export.MakeXLSX(packetListView1.GetFilteredPackets(), saveFileDialog.FileName, _backStore);
            }
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

                RunBenchmark(@"c:\temp\benchmark1.pcap");

                clearToolStripMenuItem_Click(this, null);
                RunBenchmark(@"c:\temp\benchmark2.pcapng");

                clearToolStripMenuItem_Click(this, null);
                RunBenchmark(@"c:\temp\benchmark3.pcap");

                clearToolStripMenuItem_Click(this, null);
                RunBenchmark(@"c:\temp\benchmark4.pcap");

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

            var list = new List<CapturePacket>();
            var count = 0;

            var pcapReader = new PCAPReader();

            List<FileReadObject> fileReadObjects;
            if (pcapReader.CanRead(file))
            {
                fileReadObjects = pcapReader.Read(file);
            }
            else
            {
                var pcapngReader = new PCAPNGReader();
                fileReadObjects = pcapngReader.Read(file);
            }

            foreach (var fileReadObject in fileReadObjects)
            {
                Raw raw;
                if (fileReadObject.ReadObject is PCAPBlock)
                {
                    var pcapBlock = (PCAPBlock) fileReadObject.ReadObject;
                    raw = new Raw(pcapBlock.DateTime, pcapBlock.PayLoad,
                        (LinkLayerType) pcapBlock.Header.network);
                }
                else
                {
                    var pcapngBlock = (PCAPNGBlock) fileReadObject.ReadObject;
                    raw = new Raw(pcapngBlock.Timestamp, pcapngBlock.PayLoad,
                        (LinkLayerType) pcapngBlock.Interface.LinkLayerType);
                }

                list.Add(new CapturePacket(raw));
                count++;
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
            text += "GC memory increase = " + Functions.PrettyPrintSize(totalMemory2 - totalMemory) +
                    Environment.NewLine;
            text += "PrivateMemorySize64 increase = " +
                    Functions.PrettyPrintSize(privateMemorySize64_2 - privateMemorySize64) + Environment.NewLine;
            text += "WorkingSet64 increase = " + Functions.PrettyPrintSize(workingSet64_2 - workingSet64) +
                    Environment.NewLine;
            text += "PagedSystemMemorySize64 increase = " +
                    Functions.PrettyPrintSize(pagedSystemMemorySize64_2 - pagedSystemMemorySize64) +
                    Environment.NewLine;
            text += "PagedMemorySize64 increase = " +
                    Functions.PrettyPrintSize(pagedMemorySize64_2 - pagedMemorySize64) + Environment.NewLine;

            text += "UserProcessorTime increase = " + (userProcessorTime_2 - userProcessorTime) + Environment.NewLine;
            text += "PrivilegedProcessorTime increase = " + (privilegedProcessorTime_2 - privilegedProcessorTime) +
                    Environment.NewLine;
            text += "TotalProcessorTime increase = " + (totalProcessorTime_2 - totalProcessorTime) +
                    Environment.NewLine;

            text += "Time taken " + stopwatch.Elapsed + Environment.NewLine;

            //MessageBox.Show(text);

            File.AppendAllText(file + ".txt", text);
        }

        private void statusStrip1_DoubleClick(object sender, EventArgs e)
        {
            var textWindow = new TextWindow(string.Join(Environment.NewLine,
                Logger.Instance.GetLog().Select(log => log.ToString())));
            textWindow.Show(this);
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