using PacketDotNet;
using System;
using System.Collections.Generic;

namespace IPTComShark
{
    public enum IPTTypes
    {
        PD,
        MD,
        Md,
        Mg,
        MQ,
        Mq,
        MG,
        MA,
        Ma,
        MR
    }
    [Serializable]
    public class IPTWPPacket
    {
        //0x5044(‘PD’) Process Data, used for data - set transfer
        //0x4D44(‘MD’) Message Data with acknowledge
        //0x4D64(‘Md’) Message Data without acknowledge
        //0x4D67(‘Mg’) Message Data to a function redundancy group with acknowledge
        //0x4D51(‘MQ’) Message Data Request with acknowledgement
        //0x4D71(‘Mq’) Message Data Request without acknowledgement
        //0x4D47(‘MG’) Message Data Request to a function redundancy group with acknowledge
        //0x4D41(‘MA’) Message Data Acknowledgement for point to point message
        //0x4D61(‘Ma’) Message Data Acknowledgement for a message to a function redundancy group
        //0x4D52(‘MR’) Message Data Response with acknowledgement
        private static readonly Dictionary<uint, IPTTypes> MessageTypes = new Dictionary<uint, IPTTypes>
        {
            {0x5044, IPTTypes.PD},
            {0x4D44, IPTTypes.MD},
            {0x4D64, IPTTypes.Md},
            {0x4D67, IPTTypes.Mg},
            {0x4D51, IPTTypes.MQ},
            {0x4D71, IPTTypes.Mq},
            {0x4D47, IPTTypes.MG},
            {0x4D41, IPTTypes.MA},
            {0x4D61, IPTTypes.Ma},
            {0x4D52, IPTTypes.MR}
        };

        public uint Comid { get; set; }
        public IPTTypes IPTWPType { get; set; }
        public uint IPTWPSize { get; set; }
        //public byte[] IPTWPPayload { get; set; }

        public static byte[] GetIPTPayload(UdpPacket udp, IPTWPPacket packet)
        {
            byte[] payload = udp.PayloadData;
            ushort headerlength = BitConverter.ToUInt16(new[] { payload[23], payload[22] }, 0);

            var data = new byte[packet.IPTWPSize];
            ushort readpos =
                headerlength; // the first framecheck will be skipped by the i % 256 modulus when it parses 0

            for (var i = 0; i < packet.IPTWPSize; i++)
            {
                if (i % 256 == 0)
                    readpos += 4;
                data[i] = payload[readpos];
                readpos++;
            }

            return data;
        }

        public static IPTWPPacket Extract(UdpPacket udp)
        {
            if (udp == null)
                return null;

            var iptPacket = new IPTWPPacket();

            byte[] payload = udp.PayloadData;

            if (udp.DestinationPort == 20548 || udp.DestinationPort == 20550)
            {
                if (payload.Length <= 28) // the minimum length of an empty iptwp packet
                    return null;

                uint timestamp =
                    BitConverter.ToUInt32(new[] { payload[3], payload[2], payload[1], payload[0] },
                        0);
                uint protoversion =
                    BitConverter.ToUInt32(new[] { payload[7], payload[6], payload[5], payload[4] },
                        0);
                uint topocount =
                    BitConverter.ToUInt32(new[] { payload[11], payload[10], payload[9], payload[8] },
                        0);
                uint comid = BitConverter.ToUInt32(
                    new[] { payload[15], payload[14], payload[13], payload[12] }, 0);
                ushort type = BitConverter.ToUInt16(new[] { payload[17], payload[16] }, 0);
                ushort datasetlength = BitConverter.ToUInt16(new[] { payload[19], payload[18] }, 0);
                ushort userstatus =
                    BitConverter.ToUInt16(new[] { payload[21], payload[20] },
                        0); // only used in Message Data
                ushort headerlength = BitConverter.ToUInt16(new[] { payload[23], payload[22] }, 0);

                // calculate what the total size of the packet should be, header + framecheck for header + datasetlength
                int totalsize = headerlength + 4 + datasetlength;

                // calculate the number of datasets and add the number of framechecks we should have
                int datasets = (int)Math.Floor((double)(datasetlength / 256)) + 1;
                totalsize += datasets * 4;

                // calculate if there should be padding
                if (datasetlength % 4 != 0)
                {
                    int remainder = datasetlength % 4;
                    totalsize += 4 - remainder;
                }

                if (totalsize != payload.Length)
                    return null;
                if (!MessageTypes.ContainsKey(type))
                    return null;

                // we have valid IPTCom!

                iptPacket.Comid = comid;
                iptPacket.IPTWPSize = datasetlength;
                iptPacket.IPTWPType = MessageTypes[type];
            }
            else
            {
                return null;
            }

            return iptPacket;
        }
    }
}