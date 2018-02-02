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
using PacketDotNet;
using sonesson_tools.FileReaders;

namespace LiveRecorder
{
    public partial class TrafficSim : Form
    {
        private List<Packet> _packets = new List<Packet>();
        private List<string> _ips = new List<string>();
        private string _ip;

        public TrafficSim()
        {
            InitializeComponent();

            var openFileDialog = new OpenFileDialog {Multiselect = true};
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                foreach (string fileName in openFileDialog.FileNames)
                {
                    var pcapReader = new PCAPReader();
                    if (pcapReader.CanRead(fileName))
                    {
                        List<FileReadObject> fileReadObjects = pcapReader.Read(fileName);
                        foreach (FileReadObject fileReadObject in fileReadObjects)
                        {
                            var block = (PCAPReader.PCAPBlock)fileReadObject.ReadObject;
                            _packets.Add(new Packet(block.DateTime, block.PayLoad));
                        }
                        continue;
                    }

                    var pcapngReader = new PCAPNGReader();
                    if (pcapngReader.CanRead(fileName))
                    {
                        List<FileReadObject> fileReadObjects = pcapngReader.Read(fileName);
                        foreach (FileReadObject fileReadObject in fileReadObjects)
                        {
                            var block = (PCAPNGBlock)fileReadObject.ReadObject;
                            _packets.Add(new Packet(block.Timestamp, block.PayLoad));
                        }
                        continue;
                    }

                    MessageBox.Show("Could not read " + fileName);
                }
            }
            else
            {
                this.Close();
            }

            

            foreach (Packet packet in _packets)
            {
                
                IPTWPPacket iptwpPacket = IPTWPPacket.Extract(packet.PayLoad);
                if (iptwpPacket != null)
                {
                    packet.IPTWPPacket = iptwpPacket;
                }

            }

            _packets.RemoveAll(packet => packet.IPTWPPacket == null);

            foreach (Packet packet1 in _packets)
            {
                var ipv4 = (IPv4Packet)packet1.IPTWPPacket.UdpPacket.ParentPacket;
                string ip = ipv4.SourceAddress.ToString();

                if(!_ips.Contains(ip))
                    _ips.Add(ip);
            }

            _packets.Sort();
            
            var startTime = _packets.First().TimeStamp;

            foreach (Packet packet in _packets)
            {
                packet.TimeSpan = packet.TimeStamp.Subtract(startTime);
            }

           

            comboBox1.DataSource = _ips;

            labelStart.Text += " " + _packets.First().TimeStamp;
            labelEnd.Text += " " + _packets.Last().TimeStamp;
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


            //var packets = _packets.Where(packet => ((IPv4Packet) packet.IPTWPPacket.UdpPacket.ParentPacket).SourceAddress.ToString() == _ip);
            var packets = _packets;

            var que = new Queue<Packet>(packets);

            Stopwatch stopwatch = new Stopwatch();

            

            stopwatch.Start();

            while (!backgroundWorker1.CancellationPending)
            {
                if(que.Count == 0)
                    return;

                var nextSpan = que.Peek().TimeSpan;

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

                Packet dequeue = que.Dequeue();

                ThreadPool.QueueUserWorkItem(thing =>
                {
                    var ipv4 = (IPv4Packet)dequeue.IPTWPPacket.UdpPacket.ParentPacket;
                    var ipEndPoint = new IPEndPoint(ipv4.DestinationAddress, dequeue.IPTWPPacket.UdpPacket.DestinationPort);
                    udpClient.SendAsync(dequeue.IPTWPPacket.UdpPacket.PayloadData,
                        dequeue.IPTWPPacket.UdpPacket.PayloadData.Length, ipEndPoint);
                });

                if (que.Count > 0)
                {
                    double perc = dequeue.TimeSpan.TotalMilliseconds / que.Last().TimeSpan.TotalMilliseconds;

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
