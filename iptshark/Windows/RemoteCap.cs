using TrainShark.Classes;
using Renci.SshNet;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace TrainShark.Windows
{
    public partial class RemoteCap : Form
    {
        private readonly MainForm _mainForm;
        private UInt64 _capturedData;
        private Stream _stream;
        private SshClient _client;
        private string _lastFile = null;

        public RemoteCap(MainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
        }

        private delegate void PrintDelegate(string text);

        private void Print(string text)
        {
            if (this.InvokeRequired)
                this.Invoke(new PrintDelegate(Print), text);
            else
                listBox1.Items.Add(text);
        }

        private void RemoteCap_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void ButtonGO_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            _capturedData = 0;
            var connectionInfo = new ConnectionInfo(textBoxIP.Text,
                textBoxUsername.Text,
                new PasswordAuthenticationMethod(textBoxUsername.Text, textBoxPassword.Text));


            _client = new SshClient(connectionInfo);
            _client.Connect();


            PrintCommand(_client.RunCommand("mkfifo /tmp/remcap"));


            var command = _client.CreateCommand("cat /tmp/remcap");
            command.BeginExecute();
            _stream = command.OutputStream;


            var combine = Path.Combine(textBoxFolder.Text, textBoxPrefix.Text);
            int filetick = 0;
            while (File.Exists(combine + filetick + ".pcap"))
                filetick++;
            _lastFile = combine + filetick + ".pcap";

            ThreadPool.QueueUserWorkItem(x =>
            {
                var fileStream = new FileStream(_lastFile, FileMode.Create);

                int wait = 0;
                while (_stream.CanRead)
                {
                    var readByte = _stream.ReadByte();
                    if (readByte == -1)
                    {
                        if (wait > 10)
                        {
                            if (_client.IsConnected == false)
                                break;
                        }

                        Thread.Sleep(100);
                        wait++;
                    }
                    else
                    {
                        fileStream.WriteByte((byte)readByte);
                        _capturedData++;
                        wait = 0;
                    }

                    //if (_stopCapture)
                    //{
                    //    sshClientPipe.Disconnect();
                    //    
                    //}
                }

                Print("Stream can no longer be read");

                fileStream.Flush();
                fileStream.Close();
            });

            Thread.Sleep(500);

            var tcpdumpcommand =
                _client.CreateCommand("tcpdump -s 0 -U -n -w - -i any not port " + textBoxPort.Text + " > /tmp/remcap");
            tcpdumpcommand.BeginExecute();
            PrintCommand(tcpdumpcommand);
            //PrintCommand(sshClientCommand.RunCommand("tcpdump -i any -w /tmp/remcap not port " + textBoxPort.Text + ""));


            //var stream = command.OutputStream;
            //using (var fileStream = new FileStream("c:\\temp\\tryfile", FileMode.Create))
            //{
            //    command.Execute();
            //    while (stream.CanRead)
            //    {
            //        byte[] buffer = new byte[24];
            //        stream.Read(buffer, (int) stream.Position, buffer.Length);
            //        fileStream.Write(buffer, (int) fileStream.Position, buffer.Length);
            //    }
            //}
            while (_client.IsConnected)
            {
                Thread.Sleep(1000);
            }
        }

        private void PrintCommand(SshCommand command)
        {
            Print(command.CommandText);
            if (string.IsNullOrEmpty(command.Error))
                Print(command.Result);
            else
                Print(command.Error);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            labelReadData.Text = "Data Received: " + Conversions.PrettyPrintSize(_capturedData);
            buttonGO.Enabled = !backgroundWorker1.IsBusy;
            buttonStop.Enabled = backgroundWorker1.IsBusy;
            buttonOpenLastFile.Enabled = (!string.IsNullOrEmpty(_lastFile) && !backgroundWorker1.IsBusy);
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            _client.Disconnect();
        }

        private void ButtonOpenLastFile_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_lastFile) && !backgroundWorker1.IsBusy)
            {
                _mainForm.OpenPath(new[] { _lastFile });
            }
        }
    }
}