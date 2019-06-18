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

namespace IPTComShark.Windows
{
    public partial class TrafficSim : Form
    {
        private List<CapturePacket> _packets;
        private List<IPAddress> _ips = new List<IPAddress>();
        private IPAddress _ip;

        public TrafficSim()
        {
            InitializeComponent();

            var openFileDialog = new OpenFileDialog { Multiselect = true };
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var fileManager = new FileManager.FileManager();
                _packets = fileManager.OpenFiles(openFileDialog.FileNames);
            }
            else
            {
                this.Close();
                return;
            }

            // remove all non IPTCom packets
            _packets.RemoveAll(packet => packet.IPTWPPacket == null);

            foreach (var packet1 in _packets)
            {
                IPAddress ip = new IPAddress(packet1.Source);

                if (!_ips.Contains(ip))
                    _ips.Add(ip);
            }

            _packets.Sort();

            var startTime = _packets.First().Date;


            comboBox1.DataSource = _ips;

            labelStart.Text += " " + _packets.First().Date;
            labelEnd.Text += " " + _packets.Last().Date;
        }

        private class SendPacket
        {
            public SendPacket(DateTime dateTime, IPv4Packet ipv4)
            {
                UdpPacket udp = (UdpPacket)ipv4.PayloadPacket;
                Date = dateTime;
                PayLoad = udp.PayloadData;
                Destination = new IPEndPoint(ipv4.DestinationAddress, udp.DestinationPort);
            }

            public DateTime Date { get; }
            public byte[] PayLoad { get; }
            public IPEndPoint Destination { get; set; }

            public TimeSpan TimeSpan { get; set; }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var udpClient = new UdpClient();


            var que = new Queue<SendPacket>();
            foreach (CapturePacket capturePacket in _packets.Where(packet => packet.Source.ToString() == _ip.ToString())
            )
            {
                Packet packet =
                    Packet.ParsePacket((LinkLayers)capturePacket.RawCapture.LinkLayer,
                        capturePacket.RawCapture.RawData);
                IPv4Packet ipv4 = (IPv4Packet)packet.PayloadPacket;


                var sendPacket = new SendPacket(capturePacket.Date, ipv4);
                que.Enqueue(sendPacket);
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
                    udpClient.SendAsync(dequeue.PayLoad,
                        dequeue.PayLoad.Length, dequeue.Destination);
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
    }
}