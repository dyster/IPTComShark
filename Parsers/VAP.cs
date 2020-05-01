using System.Collections.Generic;
using sonesson_tools.BitStreamParser;

namespace IPTComShark.Parsers
{
    public class VAP : DataSetCollection
    {
        public VAP()
        {
        }

        public static DataSetDefinition UDP_SPL => new DataSetDefinition()
        {
            Name = "UDP_SPL",
            BitFields = new List<BitField>()
            {
                //new BitField()
                //{
                //    Name = "SPL Seq",
                //    BitFieldType = BitFieldType.UInt8,
                //    Length = 8
                //},
                new BitField()
                {
                    Name = "SPLFrameLen",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16
                },
                new BitField()
                {
                    Name = "RecAddr",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField()
                {
                    Name = "SndAddr",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField()
                {
                    Name = "DSAP",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField()
                {
                    Name = "SSAP",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField()
                {
                    Name = "FDL mode",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
            }
        };

        public static DataSetDefinition STM_Packet => new DataSetDefinition()
        {
            Name = "STM Packet",
            BitFields = new List<BitField>()
            {
                new BitField()
                {
                    Name = "NID_STM",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField()
                {
                    Name = "Bytelength",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField()
                {
                    Name = "NID_PACKET",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField()
                {
                    Name = "L_PACKET",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 13
                },

                new BitField()
                {
                    Name = "First Niblet",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4
                },
                new BitField()
                {
                    Name = "NID_PACKET",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField()
                {
                    Name = "L_PACKET",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 13
                },
            }
        };
    }
}