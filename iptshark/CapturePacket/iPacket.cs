using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
