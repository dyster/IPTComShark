using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using BitDataParser;

namespace IPTComShark.Parsers
{
    [Serializable]
    [DataContract]
    [KnownType(typeof(SS26RadioMessageType))]
    [KnownType(typeof(SS26PacketTrackToTrain))]
    [KnownType(typeof(SS26PacketTrainToTrack))]
    [KnownType(typeof(SS27MsgType))]
    public class SS27Packet
    {
        public SS27Packet()
        {
            ExtraMessages = new List<ParsedDataSet>();
        }

        [DataMember(Order = 0)] public DateTime DateTime { get; set; }

        [DataMember(Order = 1)] public SS27MsgType MsgType { get; set; }

        public string Level { get; set; }


        public string Mode { get; set; }


        public ushort V_TRAIN { get; set; }

        [XmlIgnore] public ParsedDataSet SubMessage { get; set; }

        [XmlIgnore] public ParsedDataSet Header { get; set; }

        [XmlIgnore] public List<ParsedDataSet> ExtraMessages { get; set; }


        public byte[] PayLoad { get; set; }

        /// <summary>
        /// A short description of the event that took place
        /// </summary>
        public List<ETCSEvent> Events { get; set; } = new List<ETCSEvent>();

        public string Name { get; set; }

        public byte[] RawData { get; set; }
    }

    [Serializable]
    public class ETCSEvent
    {
        public ETCSEvent(string description)
        {
            Description = description;
            EventType = ETCSEventType.Info;
        }

        public ETCSEvent(string description, ETCSEventType eventType)
        {
            Description = description;
            EventType = eventType;
        }

        public string Description { get; }
        public ETCSEventType EventType { get; } = ETCSEventType.Info;

        public override string ToString()
        {
            return Description;
        }
    }

    [Serializable]
    public enum ETCSEventType
    {
        Info,
        Wayside,
        Main,
        Failure
    }
}
