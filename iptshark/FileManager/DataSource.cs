using System;
using System.IO;

namespace TrainShark.FileManager
{
    public class DataSource
    {
        public FileInfo FileInfo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public SourceType SourceType { get; set; }
        public int Packets { get; set; }
        public string ArchiveKey { get; set; }

        /// <summary>
        /// Is the archive entry PCAP or PCAPNG
        /// </summary>
        public SourceType ArchiveSourceType { get; set; }

        public bool Use { get; set; } = true;

        public override string ToString()
        {
            var output = SourceType + " " + Packets + " " + FileInfo.Name;
            if (!string.IsNullOrEmpty(ArchiveKey))
                output += " " + ArchiveKey;
            return output;
        }
    }

    public enum SourceType
    {
        PCAP,
        PCAPNG,
        Zip
    }
}