using System.Collections.Generic;
using BitDataParser;

namespace IPTComShark.DataSets
{
    public class Subset58 : DataSetCollection
    {
        public Subset58()
        {
            DataSets.Add(STM_1);
            DataSets.Add(STM_4);
            DataSets.Add(STM_13);
            DataSets.Add(STM_14);
            DataSets.Add(STM_15);
            DataSets.Add(STM_136);
            DataSets.Add(STM_139);
            DataSets.Add(STM_179);
            DataSets.Add(STM_181);
        }

        public static BitField NID_PACKET = new BitField()
        {
            Name = "NID_PACKET",
            Comment = "Packet identifier",
            BitFieldType = BitFieldType.UInt8,
            Length = 8
        };

        public static BitField L_PACKET = new BitField()
        {
            Name = "L_PACKET",
            Comment = "Packet Length",
            BitFieldType = BitFieldType.UInt16,
            Length = 13
        };

        public static DataSetDefinition FFFISHeader => new DataSetDefinition()
        {
            Name = "FFFIS Header",
            BitFields = new List<BitField>()
            {
                new BitField()
                {
                    Name = "NID_STM",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "The identity of the STM"
                },
                new BitField()
                {
                    Name = "L_MESSAGE",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Length of the message in bytes"
                }
            }
        };

        public static DataSetDefinition STM_1 => new DataSetDefinition()
        {
            Name = "STM/ETCS function version number",
            Comment = "This packet contains implicitly the connection request from the STM or the connection confirmation from the ERTMS/ETCS on-board function and provide also FFFIS STM version number for check.",
            Identifiers = new List<string>{"1"},
            BitFields = new List<BitField>
            {
                NID_PACKET,
                L_PACKET,
                new BitField()
                {
                    Name = "Versions",
                    BitFieldType = BitFieldType.HexString,
                    Length = 8*8,
                    Comment = "This is the baseline 2 version"
                }
            }
        };

        public static DataSetDefinition STM_4 => new DataSetDefinition()
        {
            Name = "STM parameters data and product identity",
            Identifiers = new List<string> { "4" },
            BitFields = new List<BitField>
            {
                NID_PACKET,
                L_PACKET,
                new BitField()
                {
                    Name = "NID_STMTYPE",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                },
                new BitField()
                {
                    Name = "L_TEXT",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                },
                new BitField()
                {
                    Name = "X_TEXT",
                    BitFieldType = BitFieldType.StringAscii,
                    VariableLengthSettings = new VariableLengthSettings()
                    {
                        Name = "L_TEXT",
                        ScalingFactor = 8
                    }
                }
            }
        };

        public static DataSetDefinition STM_13 => new DataSetDefinition()
        {
            Name = "State request from STM",
            Comment = "Reports a request for a state change from the STM to the ETCS On-board\r\nSTM Control Function.",
            Identifiers = new List<string>(new[] { "13" }),
            BitFields = new List<BitField>()
            {
                NID_PACKET,
                L_PACKET,
                new BitField()
                {
                    Name = "NID_STMSTATEREQUEST",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                    Comment = "Current STM state",
                    LookupTable = new LookupTable()
                    {
                        {"0","NP"},
                        {"1","PO"},
                        {"2","CO"},
                        {"3","DE"},
                        {"4","CS"},
                        {"5","CS"},
                        {"6","HS"},
                        {"7","DA"},
                        {"8","FA"}
                    }
                }
            }
        };

        public static DataSetDefinition STM_14 => new DataSetDefinition()
        {
            Name = "State order to STM",
            Comment = "State order to STM",
            Identifiers = new List<string>(new[] { "14" }),
            BitFields = new List<BitField>()
            {
                NID_PACKET,
                L_PACKET,
                new BitField()
                {
                    Name = "NID_STMSTATEORDER",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                    Comment = "State order to STM",
                    LookupTable = new LookupTable()
                    {
                        {"0","NP"},
                        {"1","PO"},
                        {"2","CO"},
                        {"3","DE"},
                        {"4","U-CS"},
                        {"5","C-CS"},
                        {"6","HS"},
                        {"7","DA"},
                        {"8","FA"}
                    }
                }
            }
        };

        public static DataSetDefinition STM_15 => new DataSetDefinition()
        {
            Name = "State report from STM",
            Comment = "Indicates to the ERTMS/ETCS on-board the STM state.",
            Identifiers = new List<string>(new[] { "15" }),
            BitFields = new List<BitField>()
            {
                NID_PACKET,
                L_PACKET,
                new BitField()
                {
                    Name = "NID_STMSTATE",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                    Comment = "Current STM state",
                    LookupTable = new LookupTable()
                    {
                        {"0","NP"},
                        {"1","PO"},
                        {"2","CO"},
                        {"3","DE"},
                        {"4","CS"},
                        {"5","CS"},
                        {"6","HS"},
                        {"7","DA"},
                        {"8","FA"}
                    }
                }
            }
        };

        public static DataSetDefinition STM_136 => new DataSetDefinition()
        {
            Name = "BIU status to STM",
            Comment = "Transmission of the brake interface status to STM",
            Identifiers = new List<string>(new[] { "136" }),
            BitFields = new List<BitField>()
            {
                NID_PACKET,
                L_PACKET,
                new BitField
                {
                    Name = "M_BIEB_STATUS",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    Comment = "EB Status",
                    LookupTable = new LookupTable()
                    {
                        {"0","Fail State"},
                        {"1","EB Applied"},
                        {"2","EB Released"},
                        {"3","Status not available"}
                    }
                },
                new BitField
                {
                    Name = "M_BISB_STATUS",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    Comment = "SB Status",
                    LookupTable = new LookupTable()
                    {
                        {"0","Fail State"},
                        {"1","SB Applied"},
                        {"2","SB Released"},
                        {"3","Status not available"}
                    }
                }
            }
        };

        public static DataSetDefinition STM_139 => new DataSetDefinition()
        {
            Name = "TIU status to STM",
            Comment = "Transmission of the train interface inputs status/availability to STM",
            Identifiers = new List<string>(new[] { "139" }),
            BitFields = new List<BitField>()
            {
                NID_PACKET,
                L_PACKET,
                new BitField
                {
                    Name = "M_TITR_C_STATUS",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    Comment = "Traction cut off train interface status",
                    LookupTable = new LookupTable()
                    {
                        {"0","Fail Status"},
                        {"1","Traction cut off"},
                        {"2","No traction cut off"},
                        {"3","Status not available"}
                    }
                },
                new BitField
                {
                    Name = "M_TIDIR_STATUS",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 3,
                    Comment = "Direction handle train interface status",
                    LookupTable = new LookupTable()
                    {
                        {"0","Fail State"},
                        {"1","Forward"},
                        {"2","Neutral"},
                        {"3","Reserved"},
                        {"4","Backward"},
                        {"5","Reserved"},
                        {"6","Reserved"},
                        {"7","Status not available"}
                    }
                },
                new BitField
                {
                    Name = "M_TICAB_STATUS",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 3,
                    Comment = "Cab train interface state",
                    LookupTable = new LookupTable()
                    {
                        {"0","Fail State"},
                        {"1","Desk A opened"},
                        {"2","Desk A & B closed"},
                        {"3","Reserved"},
                        {"4","Desk B opened"},
                        {"5","Desk A & B opened"},
                        {"6","Reserved"},
                        {"7","Status not available"}
                    }
                }
            }
        };

        public static DataSetDefinition STM_179 => new DataSetDefinition()
        {
            Name = "Specific NTC Data Entry request",
            Comment = "Request for Specific NTC Data Entry.\r\nThis packet can be grouped with other STM-179 packets by using the Q_FOLLOWING indicator in order to form one common Specific NTC Data Entry request.\r\nNote: The STM indicates the \"End of Specific NTC Data Entry\" by a packet STM-179, with N_ITER=0.",
            Identifiers = new List<string>(new[] { "179" }),
            BitFields = new List<BitField>()
            {
                NID_PACKET,
                L_PACKET,
                new BitField
                {
                    Name = "Q_FOLLOWING",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Indicate a following STM-179 packet"
                },
                new BitField
                {
                    Name = "N_ITER",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 5,
                    Comment = "Maximum iteration data\r\n=0 if there is “End of Specific NTC Data Entry”\r\nMaximum value = 15"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings(){Name = "N_ITER"},
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "NID_DATA",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Identifier of a Specific NTC Data to be entered."
                            },
                            new BitField
                            {
                                Name = "L_CAPTION",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 6,
                                Comment = "Length of X_CAPTION for data label in bytes\r\nMaximum value = 40"
                            },
                            new BitField
                            {
                                Name = "X_CAPTION",
                                BitFieldType = BitFieldType.StringLatin,
                                VariableLengthSettings = new VariableLengthSettings{Name = "L_CAPTION", ScalingFactor = 8},
                                Comment = "Data label caption text byte string"
                            },
                            new BitField
                            {
                                Name = "L_VALUE",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 5,
                                Comment = "Length of X_VALUE for default value in bytes.\r\nMaximum value = 20"
                            },
                            new BitField
                            {
                                Name = "X_VALUE",
                                BitFieldType = BitFieldType.StringLatin,
                                VariableLengthSettings = new VariableLengthSettings{Name = "L_VALUE", ScalingFactor = 8},
                                Comment = "Data value caption text byte string"
                            },
                            new BitField
                            {
                                Name = "N_ITER",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 5,
                                Comment = "Maximum iteration data\r\n=0 if there is “End of Specific NTC Data Entry”\r\nMaximum value = 15"
                            },
                            new BitField
                            {
                                VariableLengthSettings = new VariableLengthSettings(){Name = "N_ITER"},
                                NestedDataSet = new DataSetDefinition
                                {
                                    BitFields = new List<BitField>
                                    {
                                        new BitField
                                        {
                                            Name = "L_VALUE",
                                            BitFieldType = BitFieldType.UInt8,
                                            Length = 5,
                                            Comment = "Length of X_VALUE for default value in bytes.\r\nMaximum value = 20"
                                        },
                                        new BitField
                                        {
                                            Name = "X_VALUE",
                                            BitFieldType = BitFieldType.StringLatin,
                                            VariableLengthSettings = new VariableLengthSettings{Name = "L_VALUE", ScalingFactor = 8},
                                            Comment = "Data value caption text byte string"
                                        },
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        public static DataSetDefinition STM_181 => new DataSetDefinition()
        {
            Name = "Specific STM Data need",
            Comment = "STM need for Specific STM Data Entry.",
            Identifiers = new List<string>(new[] { "181" }),
            BitFields = new List<BitField>()
            {
                NID_PACKET,
                L_PACKET,
                new BitField()
                {
                    Name = "Q_DATAENTRY",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Need for Specific STM Data Entry"
                   
                },
                new BitField()
                {
                    Name = "Q_DRIVERINT",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Need for driver intervention or not"
                    
                }
            }
        };
    }
}
