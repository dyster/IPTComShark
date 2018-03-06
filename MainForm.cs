using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using IPTComShark.Windows;
using IPTComShark.XmlFiles;
using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using SharpPcap.WinPcap;
using sonesson_tools;
using sonesson_tools.BitStreamParser;
using sonesson_tools.DataSets;
using sonesson_tools.FileWriters;
using sonesson_tools.Generic;

namespace IPTComShark
{
    public partial class MainForm : Form
    {
        private const string Iptfile = @"ECN1_ipt_config.xml";

        private static readonly IPTConfigReader IptConfigReader = new IPTConfigReader(Iptfile);

        private long _capturedData;

        private WinPcapDevice _device;
        private long _discardedData;
        private long _discaredPackets;

        //private PCAPWriter _pcapWriter;

        public MainForm()
        {
            InitializeComponent();

            this.Text = Text += " " + Application.ProductVersion;

            Logger.Instance.LogAdded += (sender, log) => UpdateStatus(log.ToString());

            packetDisplay1.IptConfigReader = IptConfigReader;

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


            InitData();

            Logger.Log("IPTComShark started", Severity.Info);
        }

        private void InitData()
        {
            packetListView1.Clear();


            _capturedData = 0;
            _discardedData = 0;
            _discaredPackets = 0;
        }

        private void UpdateStatus(string text)
        {
            if (InvokeRequired)
                Invoke(new UpdateStatusDelegate(UpdateStatus), text);
            else
            {
                statusRight.Text = text;
                List<Log> logs = Logger.Instance.GetLog();
                var lastLogs = logs.Skip(Math.Max(0, logs.Count() - 10)).ToList();
                statusRight.ToolTipText = string.Join(Environment.NewLine, lastLogs);
            }
        }

        //private delegate void AddToListDelegate(CapturePacket o);


        private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            _capturedData += e.Packet.Data.Length;
            var raw = new Raw(e.Packet.Timeval.Date, e.Packet.Data, e.Packet.LinkLayerType);
            var capturePacket = new CapturePacket(raw);

            packetListView1.Add(capturePacket);

            //_pcapWriter.WritePacket(raw.RawData, raw.TimeStamp);
        }


        public static void ParseIPTWPData(CapturePacket packet)
        {
            IPTWPPacket iptwpPacket = packet.IPTWPPacket;
            if (iptwpPacket == null || iptwpPacket.IPTWPType == "MA")
                return;

            try
            {
                DataSetDefinition dataSetDefinition = VSIS210.GetDataSetDefinition(iptwpPacket.Comid);


                if (dataSetDefinition != null)
                {
                    ParsedDataSet parsedDataSet = dataSetDefinition.Parse(iptwpPacket.IPTWPPayload);
                    packet.ParsedData = parsedDataSet;
                    packet.Name = parsedDataSet.Name;
                }
                else
                {
                    Telegram telegram = IptConfigReader.GetTelegramByComId(iptwpPacket.Comid);
                    if (telegram == null)
                        packet.Name = "Unknown ComID " + iptwpPacket.Comid;
                    else
                        packet.Name = telegram.Name;
                }
            }
            catch (Exception e)
            {
                packet.Name = e.Message;
                throw;
            }
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
            }


            UpdateStatus("Recording from " + _device.Interface.FriendlyName);

            // Register our handler function to the
            // 'packet arrival' event
            _device.OnPacketArrival +=
                device_OnPacketArrival;

            // Open the device for capturing
            var readTimeoutMilliseconds = 1000;
            _device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);


            // Start the capturing process
            _device.StartCapture();
        }

        private void timerFlicker_Tick(object sender, EventArgs e)
        {
            var sizestring = "";
            if (_capturedData > 1024 * 1024)
                sizestring = _capturedData / 1024 / 1024 + " mb";
            else if (_capturedData > 1024)
                sizestring = _capturedData / 1024 + " kb";
            else
                sizestring = _capturedData + " b";

            var sizestring2 = "";
            if (_discardedData > 1024 * 1024)
                sizestring2 = _discardedData / 1024 / 1024 + " mb";
            else if (_discardedData > 1024)
                sizestring2 = _discardedData / 1024 + " kb";
            else
                sizestring2 = _discardedData + " b";
            statusLeft.Text = packetListView1.Count() + " captured packets, " + sizestring + ". " + _discaredPackets +
                              " discarded packets, " + sizestring2 + ".";
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

            LinkLayers layer = allRawCaptures.First().LinkLayer;
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

        private void buttonCleanPcap_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                ThreadPool.QueueUserWorkItem(o => { throw new NotImplementedException(); });
            }
        }

        private delegate void UpdateStatusDelegate(string text);


        private void openFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openCapturesDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var fileManager = new FileManager.FileManager();
                List<CapturePacket> capturePackets = fileManager.OpenFiles(openCapturesDialog.FileNames);
                foreach (CapturePacket capturePacket in capturePackets)
                {
                    packetListView1.Add(capturePacket);
                }
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
            packetListView1.Clear();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            packetListView1.Settings.IgnoreComid = Properties.Settings.Default.IgnoredComIds;
            packetListView1.Settings.AutoScroll = Properties.Settings.Default.AutoScroll;
            packetListView1.Settings.IgnoreDuplicatedPD = Properties.Settings.Default.IgnoreDuplicatedPD;
            packetListView1.Settings.IgnoreLoopback = Properties.Settings.Default.IgnoreLoopback;
            packetListView1.Settings.IgnoreUnknownData = Properties.Settings.Default.IgnoreUnknownData;


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
    }

    public enum Protocol
    {
        Unknown,
        UDP,
        TCP,
        IPTWP
    }
}