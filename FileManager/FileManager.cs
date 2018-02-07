using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketDotNet;
using sonesson_tools.FileReaders;

namespace IPTComShark.FileManager
{
    public class FileManager
    {
        private ZipReader zipReader = new ZipReader();
        private PCAPReader pcapReader = new PCAPReader();
        private PCAPNGReader pcapngReader = new PCAPNGReader();

        private Form _popup;
        private ProgressBar _progressbar;

        public FileManager()
        {
            pcapReader.ChunkReader += chunk =>
            {

                var pcapBlock = (PCAPBlock) chunk;
                var capturePacket = new CapturePacket(new Raw(pcapBlock.DateTime, pcapBlock.PayLoad, (LinkLayers) pcapBlock.Header.network));
                if (capturePacket.IPv4Packet == null)
                    return null;
                
                MainForm.ParseIPTWPData(capturePacket);
                return new List<FileReadObject>(){new FileReadObject(capturePacket) };
            };

            pcapngReader.ChunkReader += chunk =>
            {
                var pcapngBlock = (PCAPNGBlock) chunk;
                var capturePacket = new CapturePacket(new Raw(pcapngBlock.Timestamp, pcapngBlock.PayLoad, (LinkLayers)pcapngBlock.LinkLayerType));
                if (capturePacket.IPv4Packet == null)
                    return null;

                MainForm.ParseIPTWPData(capturePacket);
                return new List<FileReadObject>() { new FileReadObject(capturePacket) };
            };

            pcapReader.ProgressUpdated += (sender, i) => _progressbar.Value = i;

            pcapngReader.ProgressUpdated += (sender, i) => _progressbar.Value = i;

            _popup = new Form();
            _popup.Size = new Size(400, 70);

            _progressbar = new ProgressBar();
            _progressbar.Dock = DockStyle.Fill;

            _popup.Controls.Add(_progressbar);

            

        }

        ~FileManager()
        {
            _progressbar.Dispose();
            _popup.Dispose();
        }

        public List<CapturePacket> OpenFiles(string[] fileNames)
        {
            _popup.Show();
            _popup.Text = "Gathering File Info...";

            var dic = new Dictionary<string, Func<string, List<FileReadObject>>>();

            foreach (string fileName in fileNames)
            {
                /* I give up, this just can't be done properly with DeflateStreams, need to get a better zip class!
                if (zipReader.CanRead(fileName))
                {
                    ZipArchive zipArchive = ZipFile.OpenRead(fileName);
                    foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
                    {
                        // all the opening and closing of stream below is because DeflateStream is stupid
                        Stream stream = zipArchiveEntry.Open();

                        if (pcapReader.CanRead(stream))
                        {
                            
                            stream.Close();
                            dic.Add(zipArchiveEntry.Open(), pcapReader.GetReadStreamFunction());
                            continue;
                        }

                        stream.Close();
                        stream = zipArchiveEntry.Open();
                        if (pcapngReader.CanRead(stream))
                        {
                            
                            stream.Close();
                            dic.Add(zipArchiveEntry.Open(), pcapReader.GetReadStreamFunction());
                        }
                    }
                }*/

                if (pcapReader.CanRead(fileName))
                {
                    dic.Add(fileName, pcapReader.GetReadFunction());
                }
                else if (pcapngReader.CanRead(fileName))
                {
                    dic.Add(fileName, pcapngReader.GetReadFunction());
                }
            }

            var packets = new List<CapturePacket>();

            int i = 0;
            foreach (var pair in dic)
            {
                i++;

                _popup.Text = $"Reading file {i} of {dic.Count}";
                List<FileReadObject> objects = pair.Value.Invoke(pair.Key);

                packets.AddRange(objects.Select(fro => (CapturePacket)fro.ReadObject).ToList());
            }

            _popup.Close();

            return packets;
        }

        
    }

    
}
