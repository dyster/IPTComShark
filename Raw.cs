using System;
using PacketDotNet;

namespace IPTComShark
{
    public class Raw
    {
        public Raw(DateTime timeStamp, byte[] rawData, LinkLayers layer)
        {
            TimeStamp = timeStamp;
            RawData = rawData;
            LinkLayer = layer;
        }

        public DateTime TimeStamp { get; }
        public byte[] RawData { get; }
        public LinkLayers LinkLayer { get; }
    }
}