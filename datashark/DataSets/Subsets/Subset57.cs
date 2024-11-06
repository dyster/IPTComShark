using BitDataParser;
using System.Collections.Generic;

namespace TrainShark.DataSets
{
    public static class Subset57
    {
        public static DataSetDefinition SLLHeader => new DataSetDefinition()
        {
            Name = "SLL Header",
            BitFields = new List<BitField>()
            {
                new BitField()
                {
                    Name = "SLL Seq",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField()
                {
                    Name = "SL",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                    LookupTable = new LookupTable()
                    {
                        {"0", "2"},
                        {"1", "2"},
                        {"2", "2"},
                        {"3", "2"},
                        {"8", "4"},
                        {"9", "4"},
                        {"10", "4"},
                        {"11", "4"},
                        {"12", "0"},
                        {"13", "0"},
                        {"14", "0"},
                        {"15", "0"},
                    }
                },
                new BitField()
                {
                    Name = "Cmd",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                    LookupTable = new LookupTable()
                    {
                        {"0", "Connect Request"},
                        {"1", "Reserved"},
                        {"2", "Connect Confirm"},
                        {"3", "Authentication"},
                        {"4", "Auth Ack"},
                        {"5", "Disconnect"},
                        {"6", "Idle"},
                        {"7", "Send Disable"},
                        {"8", "Send Enable"},
                        {"9", "Upper Layer"},
                        {"10", "Redundancy Switchover"},
                        {"11", "RS Ack"},
                        {"12", "Reserved"},
                        {"13", "Multicast"},
                        {"14", "Reserved"},
                        {"15", "Upper Layer"},
                    }
                }
            }
        };

        public static DataSetDefinition Cmd0ConnectRequest = new DataSetDefinition()
        {
            Name = "Connect Request",
            BitFields = new List<BitField>()
            {
                new BitField
                {
                    Name = "Random Number",
                    BitFieldType = BitFieldType.HexString,
                    Length = 32
                },
                new BitField
                {
                    Name = "Idle_cycle_timeout",
                    BitFieldType = BitFieldType.UInt16Reverse,
                    Length = 16
                },
                new BitField
                {
                    Name = "Config X",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "Config Y",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "Config Z",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "Dual Bus",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
            }
        };

        public static DataSetDefinition Cmd5Disconnect = new DataSetDefinition()
        {
            Name = "Disconnect",
            BitFields = new List<BitField>()
            {
                new BitField
                {
                    Name = "New_setup_desired",
                    BitFieldType = BitFieldType.Bool,
                    Length = 8
                },
                new BitField
                {
                    Name = "Disconnect Reason",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    LookupTable = new LookupTable
                    {
                        {"0", "Request from application"},
                        {"1", "Bad version error"},
                        {"3", "Error during connection setup"},
                        {"4", "Existing connection"},
                        {"5", "Idle cycle time-out"},
                        {"6", "1st incorrect receive"},
                        {"7", "2nd incorrect receive"}
                    }
                },
                // this string is dynamic but no way of telling the length without reading to end
                //new BitField
                //{
                //    Name = "Disconnect Reason text",
                //    BitFieldType = BitFieldType.StringAscii,
                //    Length = 8*7
                //}
            }
        };

        public static DataSetDefinition SLLTimestamp => new DataSetDefinition()
        {
            Name = "SLL Timestamp",
            Comment = "Byte order is little endian",
            BitFields = new List<BitField>()
            {
                new BitField()
                {
                    Name = "Timestamp",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
            }
        };

        public static DataSetDefinition SL2Checksum => new DataSetDefinition()
        {
            Name = "SL2 Checksum",
            BitFields = new List<BitField>()
            {
                new BitField()
                {
                    Name = "Checksum",
                    BitFieldType = BitFieldType.HexString,
                    Length = 32
                },
            }
        };

        public static DataSetDefinition SL4Checksum => new DataSetDefinition()
        {
            Name = "SL4 Checksum",
            BitFields = new List<BitField>()
            {
                new BitField()
                {
                    Name = "Checksum",
                    BitFieldType = BitFieldType.HexString,
                    Length = 48
                },
            }
        };
    }
}