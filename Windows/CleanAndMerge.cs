using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using sonesson_tools.FileWriters;

namespace IPTComShark.Windows
{
    public partial class CleanAndMerge : Form
    {
        public CleanAndMerge(string[] fileNames)
        {
            InitializeComponent();

            var fileManager = new FileManager.FileManager();

            //FileStream fileStream = File.OpenWrite("c:\\temp\\test.pcap");

            var pcapWriter = new PCAPWriter("c:\\temp\\test.pcap");
            bool started = false;
            

            int pos = 0;

            fileManager.RawParsed += (sender, raw) =>
            {
                if (!started)
                {
                    pcapWriter.LinkLayerType = (uint) raw.LinkLayer;
                    pcapWriter.Start();
                    started = true;
                }

                var capturePacket = new CapturePacket(raw);
                if (capturePacket.Protocol == ProtocolType.JRU)
                {
                    pcapWriter.WritePacket(raw.RawData, raw.TimeStamp);
                }
                else if (capturePacket.Protocol == ProtocolType.IPTWP && capturePacket.IPTWPPacket != null && capturePacket.IPTWPPacket.Comid != 110)
                {
                    pcapWriter.WritePacket(raw.RawData, raw.TimeStamp);
                }


            };

            fileManager.EnumerateFiles(fileNames);

            Thread.Sleep(1000);
            pcapWriter.Stop();
        }
    }
}
