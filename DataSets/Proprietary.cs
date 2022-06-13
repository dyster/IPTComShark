using System.Collections.Generic;
using BitDataParser;

namespace IPTComShark.DataSets
{
    public static class Proprietary
    {
        public static DataSetDefinition PropJRU =
            new DataSetDefinition
            {
                Name = "Prop SS27-255 Message",
                BitFields = new List<BitField>
                {
                    new BitField
                    {
                        Name = "SubMsgNr",
                        BitFieldType = BitFieldType.UInt16,
                        Length = 8,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"1", "Version"},
                            {"2", "Config"},
                            {"3", "NTC_Data"},
                            {"4", "Additional_Data"},
                            {"5", "ETCS_NTC_Data"}
                        }
                    },
                    // this is a conditional field that will only happen on Version above
                    new BitField
                    {
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "SubMsgNr",
                            LookUpTable = new Dictionary<int, int>
                            {
                                {0, 0},
                                {1, 1},
                                {2, 0},
                                {3, 0},
                                {4, 0},
                                {5, 0}
                            }
                        },
                        NestedDataSet = new DataSetDefinition
                        {
                            BitFields = new List<BitField>
                            {
                                new BitField {Name = "SS027_Ver_Maj", BitFieldType = BitFieldType.UInt16, Length = 8},
                                new BitField {Name = "SS027_Ver_Mid", BitFieldType = BitFieldType.UInt16, Length = 8},
                                new BitField {Name = "SS027_Ver_Min", BitFieldType = BitFieldType.UInt16, Length = 8}
                            }
                        }
                    },
                    // this is a conditional field that will only happen on Config above
                    new BitField
                    {
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "SubMsgNr",
                            LookUpTable = new Dictionary<int, int>
                            {
                                {0, 0},
                                {1, 0},
                                {2, 1},
                                {3, 0},
                                {4, 0},
                                {5, 0}
                            }
                        },
                        NestedDataSet = new DataSetDefinition
                        {
                            BitFields = new List<BitField>
                            {
                                new BitField {Name = "Sensor1", BitFieldType = BitFieldType.Bool, Length = 1},
                                new BitField {Name = "Sensor2", BitFieldType = BitFieldType.Bool, Length = 1},
                                new BitField {Name = "WheelSize1", BitFieldType = BitFieldType.UInt16, Length = 16},
                                new BitField {Name = "WheelSize2", BitFieldType = BitFieldType.UInt16, Length = 16},
                                new BitField {Name = "PulsesRevol1", BitFieldType = BitFieldType.UInt16, Length = 8},
                                new BitField {Name = "PulsesRevol2", BitFieldType = BitFieldType.UInt16, Length = 8}
                            }
                        }
                    },
                    // this is a conditional field that will only happen on NTC_DATA above
                    new BitField
                    {
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "SubMsgNr",
                            LookUpTable = new Dictionary<int, int>
                            {
                                {0, 0},
                                {1, 0},
                                {2, 0},
                                {3, 1},
                                {4, 0},
                                {5, 0}
                            }
                        },
                        NestedDataSet = new DataSetDefinition
                        {
                            BitFields = new List<BitField>
                            {
                                new BitField {Name = "NID_STM", BitFieldType = BitFieldType.UInt16, Length = 8},
                                new BitField {Name = "L_MESSAGE", BitFieldType = BitFieldType.UInt16, Length = 8},
                                new BitField {Name = "NID_PACKET", BitFieldType = BitFieldType.UInt16, Length = 8}
                            }
                        }
                    }
                }
            };

        public static DataSetDefinition STM15 = new DataSetDefinition
        {
            Name = "STM-15",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "L_PACKET",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 13
                },
                new BitField
                {
                    Name = "NID_STMSTATE",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 4,
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Reserved (mapped to NP for consistency)"},
                        {"1", "Power On (PO)"},
                        {"2", "Configuration (CO)"},
                        {"3", "Data Entry (DE)"},
                        {"4", "Cold Standby (CS)"},
                        {"5", "Reserved (mapped to CS for consistency)"},
                        {"6", "Hot Standby (HS)"},
                        {"7", "Data Available (DA)"},
                        {"8", "Failure (FA)"}
                    }
                }
            }
        };


        public static DataSetDefinition MSUK =
            new DataSetDefinition
            {
                Name = "MSUK AWS/TPWS JRU Data",
                BitFields = new List<BitField>
                {
                    new BitField
                    {
                        Name = "Sunflower",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"False", "Black"},
                            {"True", "Black/Yellow"}
                        }
                    },
                    new BitField
                    {
                        Name = "AWS Isolation/Fault Indicator",
                        BitFieldType = BitFieldType.UInt16,
                        Length = 2,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"0", "Off"},
                            {"1", "On"},
                            {"2", "Flash"}
                        }
                    },
                    new BitField
                    {
                        Name =
                            "TPWS Temporary Isolation/Fault Indicator (0=Off,1 =On, 2=Flash)",
                        BitFieldType = BitFieldType.UInt16,
                        Length = 2,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"0", "Off"},
                            {"1", "On"},
                            {"2", "Flash"}
                        }
                    },
                    new BitField
                    {
                        Name = "SPAD Brake Demand Button Request",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "OVERSPEED Brake Demand Button Request",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "AWS Brake Demand Button Request",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Train Stop Override Button Request",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Brake Release Button Request",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "SPAD Brake Demand Button Event",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "OVERSPEED Brake Demand Button Event",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "AWS Brake Demand Button Event",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Train Stop Override Button Event",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Brake Release Button Event",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Brake Release Button Event",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "SPAD Audio Command",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "SPAD Reduced Level Audio Command",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "OVERSPEED Audio Command",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "OVERSPEED Reduced Level Audio Command",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Test Completed Audio Command",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "AWS Clear Audio Command",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "AWS Warning Audio Command",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Silence Audio Command",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "SPAD Brake Application Text Message",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "OVERSPEED Brake Application Text Message",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "AWS Brake Application Text Message",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "TPWS TEMPORARY ISOLATION Active Text Message",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "AWS / TPWS Fault Text Message",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "TRAIN STOP OVERRIDE Active Text Message",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "BRAKE RELEASE Active Text Message",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "AWS Isolated Text Message",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "AWS Clear Contact",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "AWS Warning Contact",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Brake Demand",
                        BitFieldType = BitFieldType.UInt16,
                        Length = 2,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"0", "Off"},
                            {"1", "SPAD/ TPWS Fault Not Acknowledged"},
                            {"2", "Overspeed"},
                            {"3", "AWS/ AWS Fault Not Acknowledged"}
                        }
                    },
                    new BitField
                    {
                        Name = "Rx Select Input",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"False", "Standard"},
                            {"True", "Extra"}
                        }
                    },
                    new BitField
                    {
                        Name = "Vehicle Type Input",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"False", "Freight"},
                            {"True", "Passenger"}
                        }
                    },
                    new BitField
                    {
                        Name = "AWS Brake Delay Input",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"False", "2"},
                            {"True", "2.7"}
                        }
                    },
                    new BitField
                    {
                        Name = "ETCS Isolation Input",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"False", "Off"},
                            {"True", "On"}
                        }
                    },
                    new BitField
                    {
                        Name = "AWS Isolation Input",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"False", "Off"},
                            {"True", "On"}
                        }
                    },
                    new BitField
                    {
                        Name = "TPWS Temporary Isolation Input",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"False", "Off"},
                            {"True", "On"}
                        },
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "AWS Caution Acknowledgement Device Input",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"False", "Off"},
                            {"True", "On"}
                        },
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "TPWS STM State",
                        BitFieldType = BitFieldType.UInt16,
                        Length = 3,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"0", "Configuration (CO)"},
                            {"1", "Data Entry (DE)"},
                            {"2", "Cold Standby (CS)"},
                            {"3", "Hot Standby (HS)"},
                            {"4", "Data Available (DA)"},
                            {"5", "Failure (FA)"}
                        }
                    },
                    new BitField
                    {
                        Name = "AWS South Pole Detection",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1
                    },
                    new BitField
                    {
                        Name = "AWS North Pole Detection",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1
                    },
                    new BitField
                    {
                        Name = "AWS Reset",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1
                    },
                    new BitField
                    {
                        Name = "F1_Tone",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1
                    },
                    new BitField
                    {
                        Name = "F2_Tone",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1
                    },
                    new BitField
                    {
                        Name = "F3_Tone",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1
                    },
                    new BitField
                    {
                        Name = "F4_Tone",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1
                    },
                    new BitField
                    {
                        Name = "F5_Tone",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1
                    },
                    new BitField
                    {
                        Name = "F6_Tone",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1
                    },
                    new BitField
                    {
                        Name = "Self Test Result",
                        BitFieldType = BitFieldType.UInt16,
                        Length = 2,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"0", "Fail"},
                            {"1", "Pass"},
                            {"2", "Not Tested"}
                        }
                    },
                    new BitField
                    {
                        Name = "M_Spares",
                        BitFieldType = BitFieldType.Spare,
                        Length = 7
                    }
                }
            };

        public static DataSetDefinition STM161 = new DataSetDefinition
        {
            Name = "STM-161",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "L_PACKET",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 13
                },
                new BitField
                {
                    Name = "T_JRU_FUNCTION",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
                new BitField
                {
                    Name = "N_L_ITER",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8
                },
                new BitField {NestedDataSet = MSUK, Length = 1}
            }
        };
    }
}