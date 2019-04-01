using System;
using PacketDotNet;

namespace IPTComShark
{
    [Serializable]
    public class Raw
    {
        public Raw(DateTime timeStamp, byte[] rawData, LinkLayerType layer)
        {
            TimeStamp = timeStamp;
            RawData = rawData;
            LinkLayer = layer;
        }

        public DateTime TimeStamp { get; }
        public byte[] RawData { get; }
        public LinkLayerType LinkLayer { get; }
    }

    [Serializable]
    public enum LinkLayerType : byte
    {
        Null = 0,
        Ethernet = 1,
        ExperimentalEthernet3MB = 2,
        AmateurRadioAX25 = 3,
        ProteonProNetTokenRing = 4,
        Chaos = 5,
        Ieee802 = 6,
        ArcNet = 7,
        Slip = 8,
        Ppp = 9,
        Fddi = 10, // 0x0A
        AtmRfc1483 = 11, // 0x0B
        Raw = 12, // 0x0C
        SlipBSD = 15, // 0x0F
        PppBSD = 16, // 0x10
        AtmClip = 19, // 0x13
        PppSerial = 50, // 0x32
        CiscoHDLC = 104, // 0x68
        Ieee80211 = 105, // 0x69
        Loop = 108, // 0x6C
        LinuxSLL = 113, // 0x71
        Ieee80211_Radio = 127, // 0x7F
        PerPacketInformation = 192, // 0xC0
    }
}