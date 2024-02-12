using PacketDotNet;
using System;

namespace TrainShark
{
    internal class PacketWrapper
    {
        public static Packet Parse(Raw raw)
        {
            if (raw.LinkLayer == LinkLayerType.BDS || raw.LinkLayer == LinkLayerType.BDS2)
                return new BDSPacket(raw.RawData);
            else if (raw.LinkLayer == LinkLayerType.Profibus)
            {
                // todo 
                return new ProfiPacket(raw.RawData);

            }
            else if (raw.LinkLayer == LinkLayerType.IEEE8023br)
            {
                // this is not supported by packetdotnet, but since it is just framing a standard ethernet frame in most cases and has a static format
                // lets just chop it up and send it in as a standard Ethernet frame and hope for the best
                var buffer = new byte[raw.RawData.Length - 12];
                Buffer.BlockCopy(raw.RawData, 8, buffer, 0, buffer.Length);
                return Packet.ParsePacket(LinkLayers.Ethernet, buffer);
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
            else if (topPacket is ProfiPacket profipacket)
                actionpacket = profipacket;

            return actionpacket;
        }
    }
}
