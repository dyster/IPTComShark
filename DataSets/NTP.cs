using System.Collections.Generic;
using sonesson_tools.BitStreamParser;

namespace IPTComShark.DataSets
{
    public class NTP : DataSetCollection
    {
        public NTP()
        {
        }

        public static DataSetDefinition NTPDataSet => new DataSetDefinition()
        {
            Name = "NTP",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "Leap Indicator",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2
                },
                new BitField
                {
                    Name = "Version Number",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 3
                },
                new BitField
                {
                    Name = "Mode",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 3
                },
                new BitField
                {
                    Name = "Stratum",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "Poll",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "Precision",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "Root Delay",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
                new BitField
                {
                    Name = "Root Dispersion",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
                new BitField
                {
                    Name = "Reference Identifier",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
                new BitField
                {
                    Name = "Ref Timestamp Integer",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
                new BitField
                {
                    Name = "Ref Timestamp Fraction",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
                new BitField
                {
                    Name = "Origin Timestamp Integer",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
                new BitField
                {
                    Name = "Origin Timestamp Fraction",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
                new BitField
                {
                    Name = "Receive Timestamp Integer",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
                new BitField
                {
                    Name = "Receive Timestamp Fraction",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
                new BitField
                {
                    Name = "Transmit Timestamp Integer",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
                new BitField
                {
                    Name = "Transmit Timestamp Fraction",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
            }
        };
    }
}