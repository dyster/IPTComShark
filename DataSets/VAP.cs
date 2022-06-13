using System.Collections.Generic;
using BitDataParser;

namespace IPTComShark.DataSets
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

        public static DataSetDefinition ATPCULifeSign => new DataSetDefinition()
        {
            Name = "ATPCU LifeSign",
            BitFields = new List<BitField>()
            {
                new BitField
                {
                    Name = "NID_ATP_PACKET",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16
                },
                new BitField()
                {
                    Name = "L_ATP_PACKET",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16
                },
                new BitField()
                {
                    Name = "Lifesign_unit",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Error"},
                        {"1", "ETCS Core"},
                        {"2", "OPC"},
                    }
                },
                new BitField()
                {
                    Name = "Lifesign_status",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Error"},
                        {"1", "Idle"},
                        {"2", "Running"},
                        {"3", "Stopping Failure"},
                        {"4", "Uncondit. Stopping Failure"},
                        {"5", "Halt, Fatal Failure"},
                    }
                },
                new BitField()
                {
                    Name = "TimeStamp",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },

                new BitField()
                {
                    Name = "RefTimeOffset",
                    BitFieldType = BitFieldType.Int32,
                    Length = 32
                },
            }
        };
    }
}