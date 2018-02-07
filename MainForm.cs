using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using IPTComShark.XmlFiles;
using LiveRecorder;
using PacketDotNet;
using sonesson_tools.DataParsers;
using SharpPcap;
using SharpPcap.LibPcap;
using SharpPcap.WinPcap;
using sonesson_tools;
using sonesson_tools.BitStreamParser;
using sonesson_tools.DataSets;
using sonesson_tools.Generic;

namespace IPTComShark
{
    public partial class MainForm : Form
    {
        private const string Iptfile = @"ECN1_ipt_config.xml";

        private static readonly IPTConfigReader IptConfigReader = new IPTConfigReader(Iptfile);
        private readonly List<Raw> _rawCaptureList = new List<Raw>();
        private long _capturedData;

        private WinPcapDevice _device;
        private long _discardedData;
        private long _discaredPackets;

        public MainForm()
        {
            InitializeComponent();

            this.Text = Text += " " + Application.ProductVersion;

            Logger.Instance.LogAdded += (sender, log) => UpdateStatus(log.LogTimeString + ": " + log.Message);

            packetDisplay1.IptConfigReader = IptConfigReader;

            packetListView1.PacketSelected += (sender, args) => packetDisplay1.SetObject(args.Packet);

            checkBoxAutoScroll.DataBindings.Add("Checked", packetListView1.Settings, "AutoScroll", true, DataSourceUpdateMode.OnPropertyChanged);
            checkBoxIgnoreLoopback.DataBindings.Add("Checked", packetListView1.Settings, "IgnoreLoopback", true, DataSourceUpdateMode.OnPropertyChanged);
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
            _rawCaptureList.Clear();

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

        private void AddToList(CapturePacket o)
        {
            //if (InvokeRequired)
            //    Invoke(new AddToListDelegate(AddToList), o);
            //else

           
            

            packetListView1.Add(o);
        }

        private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            bool hideDupes = checkBoxHideDupes.Checked;
            bool parserOnly = checkBoxParserOnly.Checked;

            bool discard;
            bool filter;
            CapturePacket pack = CapturePacket(e.Packet, parserOnly, hideDupes, out discard, out filter);


            if (discard)
            {
                _discardedData += e.Packet.Data.Length;
                _discaredPackets++;
            }
            else
            {
                
                _rawCaptureList.Add(pack.RawCapture);
                pack.No = _rawCaptureList.Count;

                if (!filter)
                    AddToList(pack);

                _capturedData += e.Packet.Data.Length;
            }
        }

        private static CapturePacket CapturePacket(RawCapture rawCapture, bool parserOnly, bool hideDupes,
            out bool discard,
            out bool filter)
        {
            var pack = new CapturePacket(rawCapture);

            discard = false;
            filter = false;

            Packet packet = Packet.ParsePacket(rawCapture.LinkLayerType, rawCapture.Data);
            if (packet != null)
                if (packet.PayloadPacket is IPv4Packet)
                {
                    var ipv4 = (IPv4Packet) packet.PayloadPacket;
                    
                    

                    if (ipv4.Protocol != IPProtocolType.UDP)
                    {
                        discard = true;
                    }
                    else
                    {
                        // see if it is IPTWP


                        var udp = (UdpPacket) ipv4.Extract(typeof(UdpPacket));

                        IPTWPPacket iptwpPacket = IPTWPPacket.Extract(udp);
                        if (iptwpPacket == null || iptwpPacket.IPTWPType == "MA") // discard acks for now
                        {
                            discard = true;
                        }
                        else
                        {
                            ParseIPTWPData(pack);


                            
                            
                        }
                    }
                }
                else if (packet.PayloadPacket is ARPPacket)
                {
                    
                    discard = true;
                }
                else
                {
                    
                    discard = true;
                }
            return pack;
        }
        
        public static Stopwatch parsingWatch = new Stopwatch();

        public static void ParseIPTWPData(CapturePacket packet)
        {
            IPTWPPacket iptwpPacket = packet.IPTWPPacket;
            if(iptwpPacket == null || iptwpPacket.IPTWPType == "MA")
                return;
            parsingWatch.Start();
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
            parsingWatch.Stop();
        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void Start()
        {
            fileToolStripMenuItem.Enabled = false;
            
            
            //buttonTest.Enabled = false;

            backgroundWorker1.RunWorkerAsync();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void Stop()
        {
            fileToolStripMenuItem.Enabled = true;
            
            
            //buttonTest.Enabled = true;
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
        }

        private void buttonSaveAll_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            Stop();
            InitData();
            Start();
        }

        private void checkBoxAutoScroll_CheckedChanged(object sender, EventArgs e)
        {
            packetListView1.AutoScroll = checkBoxAutoScroll.Checked;
        }

        private void buttonCleanPcap_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                ThreadPool.QueueUserWorkItem(o =>
                {
                    List<RawCapture> captures = new List<RawCapture>();
                    var captureFileWriter =
                        //new CaptureFileReaderDevice(@"C:\Users\Johan\Downloads\ricklog.pcap");
                        new CaptureFileReaderDevice(openFileDialog.FileName);
                    captureFileWriter.OnPacketArrival += (sender1, args) =>
                    {
                        CapturePacket capturePacket =
                            CapturePacket(args.Packet, true, true, out bool discard, out bool filter);
                        if (!discard && !filter)
                            captures.Add(args.Packet);
                    };
                    captureFileWriter.Open();
                    captureFileWriter.Capture();
                    captureFileWriter.Close();


                    try
                    {
                        var captureFileWriter2 = new CaptureFileWriterDevice(openFileDialog.FileName + " filtered.pcap",
                            FileMode.Create);
                        foreach (RawCapture rawCapture in captures)
                            captureFileWriter2.Write(rawCapture);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.Message, Severity.Error);
                        MessageBox.Show(e.ToString());
                    }
                });
            }
        }

        private delegate void UpdateStatusDelegate(string text);

        

        private void openFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openCapturesDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                parsingWatch.Reset();
                var fileManager = new FileManager.FileManager();
                List<CapturePacket> capturePackets = fileManager.OpenFiles(openCapturesDialog.FileNames);
                foreach (CapturePacket capturePacket in capturePackets)
                {
                    AddToList(capturePacket);
                }

                this.statusRight.Text = parsingWatch.ElapsedMilliseconds + "ms spent on data parsing";
                
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
            GitHubUpdateCheck.GetLatestVersionAndPromptAsync("dyster", "IPTComShark", Application.ProductVersion);
        }
    }

    public class CapturePacket : ParsedDataObject, IComparable
    {
        public CapturePacket(RawCapture rawCapture) : this(new Raw(rawCapture.Timeval.Date, rawCapture.Data, rawCapture.LinkLayerType))
        {
            
        }

        public CapturePacket(Raw raw)
        {
            RawCapture = raw;
            Date = raw.TimeStamp;

            Packet packet = Packet.ParsePacket(raw.LinkLayer, raw.RawData);
            
            if (packet == null)
                return;

            if (packet.PayloadPacket is IPv4Packet)
            {
                this.IPv4Packet = (IPv4Packet) packet.PayloadPacket;
               

                switch (IPv4Packet.Protocol)
                {
                    case IPProtocolType.TCP:
                        break;
                    
                    case IPProtocolType.UDP:
                        var udp = (UdpPacket)IPv4Packet.PayloadPacket;
                        this.UDPPacket = udp;
                        IPTWPPacket = IPTWPPacket.Extract(udp);
                        break;
                    
                    case IPProtocolType.NONE:
                        // dunno
                        break;

                    case IPProtocolType.ICMP:
                        // dunno
                        break;

                    case IPProtocolType.IGMP:
                        // dunno
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// If part of a chain
        /// </summary>
        public CapturePacket Previous { get; set; }

        /// <summary>
        /// If part of a chain
        /// </summary>
        public CapturePacket Next { get; set; }

        public IPv4Packet IPv4Packet { get; }
        public UdpPacket UDPPacket { get; }
        public IPTWPPacket IPTWPPacket { get; set; }


        public int No { get; set; }
        public DateTime Date { get; set; }
        
        public Raw RawCapture { get; }
        public ParsedDataSet ParsedData { get; set; }
        public int CompareTo(object obj)
        {
            var packet = (CapturePacket)obj;
            return this.Date.CompareTo(packet.Date);

        }
    }

    public class Raw
    {
        public Raw(DateTime timeStamp, byte[] rawData, LinkLayers layer)
        {
            TimeStamp = timeStamp;
            RawData = rawData;
            LinkLayer = layer;
        }

        public DateTime TimeStamp { get;  }
        public byte[] RawData { get;  }
        public LinkLayers LinkLayer { get; set; }
    }

    public enum Protocol
    {
        Unknown,
        UDP,
        TCP,
        IPTWP
    }

    public class DataLine
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}