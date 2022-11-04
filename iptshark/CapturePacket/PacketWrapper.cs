using PacketDotNet;

namespace IPTComShark
{
    internal class PacketWrapper
    {
        public static Packet Parse(Raw raw)
        {
            if (raw.LinkLayer == LinkLayerType.BDS || raw.LinkLayer == LinkLayerType.BDS2)
                return new BDSPacket(raw.RawData);
            else if(raw.LinkLayer == LinkLayerType.Profibus)
            {
                // todo 
                return new ProfiPacket(raw.RawData);
                
            }
            else
                return Packet.ParsePacket((LinkLayers)raw.LinkLayer, raw.RawData);
        }

        internal static Packet GetActionPacket(Packet topPacket)
        {
            var actionpacket = topPacket.PayloadPacket;

            if (actionpacket is Ieee8021QPacket vlanpacket)
            {
                if (vlanpacket.PayloadPacket == null)
                    return null;

                actionpacket = vlanpacket.PayloadPacket;
            }
            else if (topPacket is BDSPacket bdspacket)
                actionpacket = bdspacket;
            else if(topPacket is ProfiPacket profipacket)
                actionpacket = profipacket;            

            return actionpacket;
        }
    }
}
