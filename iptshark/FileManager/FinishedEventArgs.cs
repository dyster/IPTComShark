using System;
using System.Collections.Generic;

namespace IPTComShark.FileManager
{
    public class FinishedEventArgs
    {
        public FinishedEventArgs(DateTime start, DateTime now, int count, List<DataSource> dataSources)
        {
            Start = start;
            Now = now;
            Count = count;
            DataSources = dataSources;            
        }

        public DateTime Start { get; }
        public DateTime Now { get; }
        public int Count { get; }
        public List<DataSource> DataSources { get; }

        public override string ToString()
        {
            return $"Finished reading {DataSources.Count} files containing {Count} packets in {Now - Start}";
        }
    }

}