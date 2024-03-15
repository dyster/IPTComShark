using System;
using System.Collections.Generic;

namespace TrainShark.FileManager
{
    public class FinishedEventArgs
    {
        public FinishedEventArgs(DateTime start, DateTime now, int count, List<DataSource> dataSources, string[] inputs)
        {
            Start = start;
            Now = now;
            Count = count;
            DataSources = dataSources;
            Inputs = inputs;
        }

        public DateTime Start { get; }
        public DateTime Now { get; }
        public int Count { get; }
        public List<DataSource> DataSources { get; }
        public string[] Inputs { get; }

        public override string ToString()
        {
            return $"Finished reading {DataSources.Count} files containing {Count} packets in {Now - Start}";
        }
    }
}