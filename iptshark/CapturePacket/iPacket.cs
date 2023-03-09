using System.Collections.Generic;

namespace IPTComShark
{
    public interface iPacket
    {
        public string ProtocolInfo { get; }
        public ProtocolType Protocol { get; }
        public List<DisplayField> DisplayFields { get; set; }
        public string Name { get; set; }
        
    }

    /// <summary>
    /// Has a source and a destination
    /// </summary>
    public interface iTraveller
    {
        public byte[] Source { get; set; }

        public byte[] Destination { get; set; }
    }    
}
