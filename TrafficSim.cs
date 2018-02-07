using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IPTComShark;
using IPTComShark.FileManager;
using PacketDotNet;
using sonesson_tools.FileReaders;

namespace LiveRecorder
{
    public partial class TrafficSim : Form
    {
        private List<CapturePacket> _packets;
        private List<string> _ips = new List<string>();
        private string _ip;

        public TrafficSim()
        {
            InitializeComponent();

            var openFileDialog = new OpenFileDialog {Multiselect = true};
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var fileManager = new FileManager();
                _packets = fileManager.OpenFiles(openFileDialog.FileNames);
            }
            else
            {
                this.Close();
            }
            
            // remove all non IPTCom packets
            _packets.RemoveAll(packet => packet.IPTWPPacket == null);

            foreach (var packet1 in _packets)
            {
                string ip = packet1.IPv4Packet.SourceAddress.ToString();

                if(!_ips.Contains(ip))
                    _ips.Add(ip);
            }

            _packets.Sort();
            
            var startTime = _packets.First().Date;

            
            comboBox1.DataSource = _ips;

            labelStart.Text += " " + _packets.First().Date;
            labelEnd.Text += " " + _packets.Last().Date;
        }

        private class Packet : IComparable
        {
            public Packet(DateTime dateTime, byte[] payload)
            {
                TimeStamp = dateTime;
                PayLoad = payload;
            }
            public DateTime TimeStamp { get; }
            public byte[] PayLoad { get; }
            public IPTWPPacket IPTWPPacket { get; set; }
            public TimeSpan TimeSpan { get; set; }

            public int CompareTo(object obj)
            {
                var packet = (Packet) obj;
                return this.TimeStamp.CompareTo(packet.TimeStamp);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            

            var udpClient = new UdpClient();


            var packets = _packets.Where(packet =>  packet.IPv4Packet.SourceAddress.ToString() == _ip);
            //var packets = _packets;

            var que = new Queue<CapturePacket>(packets);

            DateTime startTime = que.Peek().Date;
            DateTime endTime = que.Last().Date;

            var totalMS = (endTime - startTime).TotalMilliseconds;

            Stopwatch stopwatch = new Stopwatch();

            

            stopwatch.Start();

            while (!backgroundWorker1.CancellationPending)
            {
                if(que.Count == 0)
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

                CapturePacket dequeue = que.Dequeue();

                ThreadPool.QueueUserWorkItem(thing =>
                {
                    var ipv4 = dequeue.IPv4Packet;
                    var ipEndPoint = new IPEndPoint(ipv4.DestinationAddress, dequeue.UDPPacket.DestinationPort);
                    udpClient.SendAsync(dequeue.UDPPacket.PayloadData,
                        dequeue.UDPPacket.PayloadData.Length, ipEndPoint);
                });

                if (que.Count > 0)
                {
                    

                    double perc = (dequeue.Date - startTime).TotalMilliseconds / totalMS;

                    backgroundWorker1.ReportProgress((int) (perc * 100));
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
            _ip = ipAddress.ToString();

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
