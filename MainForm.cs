using IPTComShark.Import;
using IPTComShark.Windows;
using IPTComShark.XmlFiles;
using SharpPcap;
using SharpPcap.WinPcap;
using sonesson_tools;
using sonesson_tools.BitStreamParser;
using sonesson_tools.DataSets;
using sonesson_tools.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IPTComShark.Parsers;
using PacketDotNet;

namespace IPTComShark
{
    public partial class MainForm : Form
    {
        private static readonly List<DataSetCollection> DataCollections = new List<DataSetCollection>();

        private const string Iptfile = @"ECN1_ipt_config.xml";

        private static readonly IPTConfigReader IptConfigReader = new IPTConfigReader(Iptfile);

        private long _capturedData;

        private WinPcapDevice _device;
        private long _discardedData;
        private long _discardedPackets;

        private uint _seed = 1;

        //private PCAPWriter _pcapWriter;

        public MainForm()
        {
            InitializeComponent();

            Text = Text += " " + Application.ProductVersion;

            Logger.Instance.LogAdded += (sender, log) => UpdateStatus(log.ToString());

            packetDisplay1.IptConfigReader = IptConfigReader;

            DataCollections.Add(new TPWS());
            DataCollections.Add(new STM());
            DataCollections.Add(new Parsers.ETCSDiag());
            DataCollections.Add(new VSISDMI());
            DataCollections.Add(new VSIS210());
            DataCollections.Add(IptConfigReader.GetDataSetCollection());


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

            Logger.Log("IPTComShark started", Severity.Info);
        }

        private void InitData()
        {
            packetListView1.Clear();


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
            var raw = new Raw(e.Packet.Timeval.Date, e.Packet.Data, (LinkLayerType)e.Packet.LinkLayerType);
            var capturePacket = new CapturePacket(raw);
            capturePacket.No = _seed++;
            packetListView1.Add(capturePacket);

            //_pcapWriter.WritePacket(raw.RawData, raw.TimeStamp);
        }


        public static ParsedDataSet ParseIPTWPData(IPTWPPacket iptwpPacket, UdpPacket udp, bool extensive)
        {
            if (iptwpPacket == null || iptwpPacket.IPTWPType == IPTTypes.MA)
                return null;
        
            DataSetDefinition dataSetDefinition = null;

            foreach (var collection in DataCollections)
            {
                var find = collection.FindByIdentifier(iptwpPacket.Comid.ToString());
                if (find != null)
                {
                    dataSetDefinition = find;
                    break;
                }
            }

            if (dataSetDefinition != null)
            {
                var iptPayload = IPTWPPacket.GetIPTPayload(udp, iptwpPacket);
                return dataSetDefinition.Parse(iptPayload, extensive);
                
            }

            return null;
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
                CaptureDeviceList devices = CaptureDeviceList.Instance;

                // If no devices were found print an error
                if (devices.Count < 1)
                    return;

                List<WinPcapDevice> captureDevices =
                    devices.Where(d => d is WinPcapDevice).Cast<WinPcapDevice>().ToList();

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

            var tuples = new List<Tuple<Color, string>>();
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
                _device.Close();
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
        }

        private void buttonSaveAll_Click(object sender, EventArgs e)
        {
            List<Raw> allRawCaptures = packetListView1.GetAllRawCaptures();
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
                pcapWriter.LinkLayerType = (uint)layer;
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
                using (var fileManager = new FileManager.FileManager())
                {
                    List<CapturePacket> capturePackets = fileManager.OpenFiles(paths);
                    if (capturePackets != null)
                    {
                        foreach (CapturePacket capturePacket in capturePackets)
                        {
                            packetListView1.Add(capturePacket);
                        }
                    }
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
                SeqDiagram.SeqDiagramExporter.MakeSVG(packetListView1.GetFilteredPackets(), saveFileDialog.FileName);
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
                Export.Export.MakeXLSX(packetListView1.GetFilteredPackets(), saveFileDialog.FileName);
            }
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            var dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                List<CapturePacket> capturePackets;

                using (var fileManager = new FileManager.FileManager())
                {
                    capturePackets = fileManager.OpenFiles(new[] { folderBrowserDialog.SelectedPath });
                }

                foreach (CapturePacket capturePacket in capturePackets)
                {
                    packetListView1.Add(capturePacket);
                }
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
                string[] data = (string[])e.Data.GetData(DataFormats.FileDrop);
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
                totalwidth += (int)gx.MeasureString(pair.Item2, font).Width;
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

                renderbar += (int)piecewidth;
            }
            
            return img;
        }



    
    }

    //public enum Protocol
    //{
    //    Unknown,
    //    UDP,
    //    TCP,
    //    IPTWP
    //}
}