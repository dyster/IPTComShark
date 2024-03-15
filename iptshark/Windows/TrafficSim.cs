using PacketDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace TrainShark.Windows
{
    public partial class TrafficSim : Form
    {
        private Dictionary<int, IPv4Packet> _packets = new Dictionary<int, IPv4Packet>();
        private Dictionary<int, DateTime> _packetTimes = new Dictionary<int, DateTime>();
        private List<IPAddress> _ips = new List<IPAddress>();
        private IPAddress _ip;
        //private readonly BackStore.BackStore _backStore;

        public TrafficSim()
        {
            InitializeComponent();
            //this._backStore = new BackStore.BackStore(new Parsers.ParserFactory());
        }

        private class SendPacket
        {
            public SendPacket(DateTime dateTime, IPv4Packet ipv4)
            {
                Ipv4Packet = ipv4;
                //UdpPacket udp = (UdpPacket) ipv4.PayloadPacket;
                Date = dateTime;
                //PayLoad = udp.PayloadData;
                //Destination = new IPEndPoint(ipv4.DestinationAddress, udp.DestinationPort);
            }

            public DateTime Date { get; }
            public byte[] PayLoad { get; }
            public IPEndPoint Destination { get; set; }

            public IPv4Packet Ipv4Packet { get; set; }

            public TimeSpan TimeSpan { get; set; }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var udpClient = new UdpClient();

            var que = new Queue<SendPacket>();
            foreach (var kvp in _packets)
            {
                var packet = kvp.Value;
                if (packet.SourceAddress.ToString() == _ip.ToString())
                {
                    var sendPacket = new SendPacket(_packetTimes[kvp.Key], packet);
                    que.Enqueue(sendPacket);
                }
            }

            DateTime startTime = que.Peek().Date;
            DateTime endTime = que.Last().Date;

            var totalMS = (endTime - startTime).TotalMilliseconds;

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            while (!backgroundWorker1.CancellationPending)
            {
                if (que.Count == 0)
                    return;

                var nextSpan = que.Peek().Date - startTime;

                while (true)
                {
                    TimeSpan diff = nextSpan.Subtract(stopwatch.Elapsed);

                    double msdiff = diff.TotalMilliseconds;

                    if (msdiff <= 0f)
                        break;

                    if (msdiff < 1f)
                        Thread.SpinWait(10);
                    else if (msdiff < 5f)
                        Thread.SpinWait(100);
                    else if (msdiff < 15f)
                        Thread.Sleep(1);
                    else
                        Thread.Sleep(10);

                    if (backgroundWorker1.CancellationPending)
                        return;
                }

                SendPacket dequeue = que.Dequeue();

                ThreadPool.QueueUserWorkItem(thing =>
                {
                    if (dequeue.Ipv4Packet.PayloadPacket is UdpPacket udp)
                    {
                        udpClient.SendAsync(udp.PayloadData,
                        udp.PayloadData.Length, new IPEndPoint(dequeue.Ipv4Packet.DestinationAddress, udp.DestinationPort));
                    }
                    else if (dequeue.Ipv4Packet.PayloadPacket is TcpPacket tcp)
                    {
                    }
                });

                if (que.Count > 0)
                {
                    double perc = (dequeue.Date - startTime).TotalMilliseconds / totalMS;

                    backgroundWorker1.ReportProgress((int)(perc * 100));
                }
                else
                {
                    backgroundWorker1.ReportProgress(0);
                }
            }

            stopwatch.Stop();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            IPAddress ipAddress = IPAddress.Parse(comboBox1.SelectedItem.ToString());
            _ip = ipAddress;

            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ThreadPool.GetMaxThreads(out int maxworkerThreads, out int maxcompletionPortThreads);

            ThreadPool.GetAvailableThreads(out int workerThreads, out int completionPortThreads);

            label2.Text = "Threads = " + (maxworkerThreads - workerThreads);
        }

        private void TrafficSim_Load(object sender, EventArgs e)
        {
            int seed = 0;
            var openFileDialog = new OpenFileDialog { Multiselect = true };
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var fileManager = new FileManager.FileManager();
                var raws = fileManager.OpenFiles(openFileDialog.FileNames);
                foreach (var raw in raws)
                {
                    //_backStore.Add(raw, out var unused);
                    var parse = PacketWrapper.Parse(raw);
                    if (parse.HasPayloadPacket && parse.PayloadPacket is IPv4Packet ipv4)
                    {
                        _packets.Add(seed, ipv4);
                        _packetTimes.Add(seed, raw.TimeStamp);
                        seed++;
                    }
                }
            }
            else
            {
                this.Close();
                return;
            }

            // remove all non IPTCom packets
            //_packets.RemoveAll(packet => packet.Protocol != ProtocolType.IPTWP);

            foreach (IPv4Packet packet1 in _packets.Values)
            {
                IPAddress ip = packet1.SourceAddress;

                if (!_ips.Contains(ip))
                    _ips.Add(ip);
            }

            //_packets.Sort();

            //var firstPack = _packets.First();
            //var startTime = DateTime.Now;

            comboBox1.DataSource = _ips;

            labelStart.Text += " " + _packetTimes.First().Value.ToString();
            labelEnd.Text += " " + _packets.Last().Value.ToString();
        }
    }
}