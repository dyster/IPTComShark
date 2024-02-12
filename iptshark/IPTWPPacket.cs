using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TrainShark
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

        public uint IPTWPSize { get; set; }
        //public byte[] IPTWPPayload { get; set; }

        public static byte[] GetIPTPayload(byte[] udpPayload)
        {
            var size = GetDatasetLength(udpPayload);
            ushort headerlength = GetHeaderLength(udpPayload);

            var data = new byte[size];
            ushort readpos =
                headerlength; // the first framecheck will be skipped by the i % 256 modulus when it parses 0

            for (var i = 0; i < size; i++)
            {
                if (i % 256 == 0)
                    readpos += 4;
                data[i] = udpPayload[readpos];
                readpos++;
            }

            return data;
        }

        public static IPTWPPacket Extract(byte[] payload)
        {
            var iptPacket = new IPTWPPacket();

            if (payload.Length <= 28) // the minimum length of an empty iptwp packet
                return null;

            var header = ExtractHeader(payload);

            ushort datasetlength = (ushort)header["DatasetLength"];


            // calculate what the total size of the packet should be, header + framecheck for header + datasetlength
            int totalsize = ((ushort)header["HeaderLength"]) + 4 + datasetlength;

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
            if (!MessageTypes.ContainsKey((ushort)header["Type"]))
                return null;

            // we have valid IPTCom!

            iptPacket.IPTWPSize = datasetlength;

            return iptPacket;
        }

        public static uint GetComid(byte[] payload)
        {
            return BitConverter.ToUInt32(
                new[] { payload[15], payload[14], payload[13], payload[12] }, 0);
        }

        /// <summary>
        /// Gets the IPTWP Type as a unsigned integer
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static UInt16 GetType(byte[] payload)
        {
            return BitConverter.ToUInt16(new[] { payload[17], payload[16] }, 0);
        }

        /// <summary>
        /// Gets the IPTWP Type as an enum
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static IPTTypes GetIptType(byte[] payload)
        {
            var type = BitConverter.ToUInt16(new[] { payload[17], payload[16] }, 0);
            return MessageTypes[type];
        }

        public static ushort GetDatasetLength(byte[] payload)
        {
            return BitConverter.ToUInt16(new[] { payload[19], payload[18] }, 0);
        }

        public static ushort GetHeaderLength(byte[] payload)
        {
            return BitConverter.ToUInt16(new[] { payload[23], payload[22] }, 0);
        }

        public static Dictionary<string, object> ExtractHeader(byte[] payload)
        {
            var dic = new Dictionary<string, object>();
            dic.Add("TimeStamp", BitConverter.ToUInt32(new[] { payload[3], payload[2], payload[1], payload[0] },
                0));
            dic.Add("ProtocolVersion", BitConverter.ToUInt32(new[] { payload[7], payload[6], payload[5], payload[4] },
                0));
            dic.Add("TopoCount", BitConverter.ToUInt32(new[] { payload[11], payload[10], payload[9], payload[8] },
                0));
            dic.Add("ComID", GetComid(payload));
            var type = GetType(payload);
            dic.Add("Type", type);
            dic.Add("DatasetLength", GetDatasetLength(payload));

            if (type != 0x5044)
            {
                dic.Add("UserStatus", BitConverter.ToUInt16(new[] { payload[21], payload[20] },
                    0));
            }


            var headLen = GetHeaderLength(payload);
            dic.Add("HeaderLength", headLen);

            if (type != 0x5044)
            {
                dic.Add("SrcURILen", payload[24]);
                dic.Add("DestURILen", payload[25]);
                dic.Add("Index", BitConverter.ToInt16(new[] { payload[27], payload[26] }, 0));
                dic.Add("SequenceNumber", BitConverter.ToUInt16(new[] { payload[29], payload[28] },
                    0));
                dic.Add("MSGLength", BitConverter.ToUInt16(new[] { payload[31], payload[30] },
                    0));
                dic.Add("SessionId", BitConverter.ToUInt32(
                    new[] { payload[35], payload[34], payload[33], payload[32] }, 0));

                int pos = 36;
                dic.Add("SourceURI", "");
                for (int i = 0; i < Convert.ToUInt16(dic["SrcURILen"]) * 4; i++)
                {
                    if (payload[pos] != 0)
                        dic["SourceURI"] += Encoding.ASCII.GetString(new[] { payload[pos] });
                    pos++;
                }

                dic.Add("DestinationURI", "");
                for (int i = 0; i < Convert.ToUInt16(dic["DestURILen"]) * 4; i++)
                {
                    if (payload[pos] != 0)
                        dic["DestinationURI"] += Encoding.ASCII.GetString(new[] { payload[pos] });
                    pos++;
                }

                dic.Add("ResponseTimeout", BitConverter.ToUInt32(
                    new[] { payload[pos + 3], payload[pos + 2], payload[pos + 1], payload[pos] }, 0));
                pos += 4;

                var ip = new IPAddress(new[] { payload[pos], payload[pos + 1], payload[pos + 2], payload[pos + 3] });
                pos += 4;

                dic.Add("DestIPaddress", ip.ToString());
            }

            dic.Add("FrameCheckSequence", BitConverter.ToUInt32(
                new[] { payload[headLen + 3], payload[headLen + 2], payload[headLen + 1], payload[headLen] }, 0));


            return dic;
        }
    }
}