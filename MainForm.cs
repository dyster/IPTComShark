using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace IPTComShark
{
    public partial class MainForm : Form
    {
        private const string Iptfile = @"ECN1_ipt_config.xml";

        private static readonly IPTConfigReader IptConfigReader = new IPTConfigReader(Iptfile);
        private readonly List<RawCapture> _rawCaptureList = new List<RawCapture>();
        private long _capturedData;

        private WinPcapDevice _device;
        private long _discardedData;
        private long _discaredPackets;

        public MainForm()
        {
            InitializeComponent();

            Logger.Instance.LogAdded += (sender, log) => UpdateStatus(log.LogTimeString + ": " + log.Message);

            packetDisplay1.IptConfigReader = IptConfigReader;

            packetListView1.PacketSelected += (sender, args) => packetDisplay1.SetObject(args.Packet);

            InitData();
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
                statusRight.Text = text;
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
                
                _rawCaptureList.Add(e.Packet);
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
            var pack = new CapturePacket
            {
                Data = rawCapture.Data,
                Date = rawCapture.Timeval.Date,
                MS = rawCapture.Timeval.MicroSeconds,
                RawCapture = rawCapture
            };

            discard = false;
            filter = false;

            Packet packet = Packet.ParsePacket(rawCapture.LinkLayerType, rawCapture.Data);
            if (packet != null)
                if (packet.PayloadPacket is IPv4Packet)
                {
                    var ipv4 = (IPv4Packet) packet.PayloadPacket;
                    pack.Protocol = ipv4.Protocol.ToString();
                    pack.Data = ipv4.PayloadData;

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
                            DataSetDefinition dataSetDefinition = VSIS210.GetDataSetDefinition(iptwpPacket.Comid);

                            if (dataSetDefinition != null)
                            {
                                ParsedDataSet parsedDataSet = dataSetDefinition.Parse(iptwpPacket.IPTWPPayload);
                                iptwpPacket.Name = parsedDataSet.Name;
                                iptwpPacket.DictionaryData = parsedDataSet.GetDataDictionary();


                            }
                            
                            

                            if (iptwpPacket.DictionaryData == null)
                            {
                                if (parserOnly)
                                {
                                    filter = true;
                                }
                                else
                                {
                                    Telegram telegram = IptConfigReader.GetTelegramByComId(iptwpPacket.Comid);
                                    if (telegram == null)
                                        iptwpPacket.Name = "Unknown ComID " + iptwpPacket.Comid;
                                    else
                                        iptwpPacket.Name = telegram.Name;
                                }
                            }
                            else if (iptwpPacket.DictionaryData.Count == 0)
                            {
                                if (hideDupes)
                                {
                                    filter = true;
                                }
                            }


                            // copy over the time values to the new packet
                            iptwpPacket.Date = rawCapture.Timeval.Date;
                            iptwpPacket.MS = rawCapture.Timeval.MicroSeconds;
                            iptwpPacket.RawCapture = rawCapture;

                            // and write over the old one
                            pack = iptwpPacket;
                        }
                    }
                }
                else if (packet.PayloadPacket is ARPPacket)
                {
                    pack.Protocol = "ARP";
                    discard = true;
                }
                else
                {
                    if (packet.PayloadPacket != null) pack.Protocol = packet.PayloadPacket.GetType().ToString();
                    discard = true;
                }
            return pack;
        }

        

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void Start()
        {
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            buttonSaveFiltered.Enabled = false;
            buttonSaveAll.Enabled = false;
            //buttonTest.Enabled = false;

            backgroundWorker1.RunWorkerAsync();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void Stop()
        {
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            buttonSaveFiltered.Enabled = true;
            buttonSaveAll.Enabled = true;
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

        /// <summary>
        ///     Save Filtered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SavePCAP(packetListView1.GetRawCaptures());
        }

        private void SavePCAP(List<RawCapture> picks)
        {
            var dialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "pcap"
            };

            DialogResult dialogResult = dialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    var captureFileWriter = new CaptureFileWriterDevice(dialog.FileName, FileMode.Create);
                    foreach (RawCapture rawCapture in picks)
                        captureFileWriter.Write(rawCapture);
                }
                catch (Exception e)
                {
                    Logger.Log(e.Message, Severity.Error);
                    MessageBox.Show(e.ToString());
                }
            }
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

        private void buttonTest_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                ThreadPool.QueueUserWorkItem(o =>
                {
                    var captureFileWriter =
                        //new CaptureFileReaderDevice(@"C:\Users\Johan\Downloads\ricklog.pcap");
                        new CaptureFileReaderDevice(openFileDialog.FileName);
                    captureFileWriter.OnPacketArrival += device_OnPacketArrival;
                    captureFileWriter.Open();
                    captureFileWriter.Capture();
                    captureFileWriter.Close();
                });
            }
        }


        private void buttonSaveAll_Click(object sender, EventArgs e)
        {
            SavePCAP(_rawCaptureList);
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
                            captures.Add(capturePacket.RawCapture);
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

        private void buttonSimulate_Click(object sender, EventArgs e)
        {
            var trafficSim = new TrafficSim();
            trafficSim.Show(this);
        }
    }

    public class CapturePacket : ParsedDataObject
    {
        public int No { get; set; }
        public ulong MS { get; set; }
        public byte[] Data { get; set; }
        public DateTime Date { get; set; }
        public ulong Seconds { get; set; }
        public string Protocol { get; set; }
        public RawCapture RawCapture { get; set; }
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