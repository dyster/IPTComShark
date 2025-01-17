﻿using BitDataParser;
using System.Collections.Generic;

namespace TrainShark.DataSets
{
    public class Subset58 : DataSetCollection
    {
        public Subset58()
        {
            DataSets.Add(STM_1);
            DataSets.Add(STM_4);
            DataSets.Add(STM_5);
            DataSets.Add(STM_13);
            DataSets.Add(STM_14);
            DataSets.Add(STM_15);
            DataSets.Add(STM_32);
            DataSets.Add(STM_35);
            DataSets.Add(STM_77);
            DataSets.Add(STM_136);
            DataSets.Add(STM_139);
            DataSets.Add(STM_161);
            DataSets.Add(STM_175);
            DataSets.Add(STM_176);
            DataSets.Add(STM_177);
            DataSets.Add(STM_178);
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

        public static BitField L_CAPTION = new BitField()
        {
            Name = "L_CAPTION",
            BitFieldType = BitFieldType.UInt8,
            Length = 5
        };

        public static BitField X_CAPTION = new BitField
        {
            Name = "X_CAPTION",
            BitFieldType = BitFieldType.StringLatin,
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "L_CAPTION",
                ScalingFactor = 8
            }
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
            Identifiers = new Identifiers { Numeric = { 1 } },
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
            Identifiers = new Identifiers { Numeric = { 4 } },
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

        public static DataSetDefinition STM_5 => new DataSetDefinition()
        {
            Name = "ETCS status data",
            Comment = "This packet contains the ETCS On-board current status (ETCS technical mode\r\nand ETCS level of operation) for the STM",
            Identifiers = new Identifiers { Numeric = { 5 } },
            BitFields = new List<BitField>
            {
                NID_PACKET,
                L_PACKET,
                Subset26.M_LEVEL,
                new BitField()
                {
                    Name = "NID_STM",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    VariableLengthSettings = new VariableLengthSettings()
                    {
                        Name = "M_LEVEL",
                        LookUpTable = new IntLookupTable
                        {
                            {0,0 }, {1,8}, {2,0}, {3,0}, {4,0}
                        }
                    }
                },
                Subset26.M_MODE
            }
        };

        public static DataSetDefinition STM_13 => new DataSetDefinition()
        {
            Name = "State request from STM",
            Comment = "Reports a request for a state change from the STM to the ETCS On-board\r\nSTM Control Function.",
            Identifiers = new Identifiers { Numeric = { 13 } },
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
            Identifiers = new Identifiers { Numeric = { 14 } },
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
            Identifiers = new Identifiers { Numeric = { 15 } },
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

        public static DataSetDefinition STM_32 => new DataSetDefinition()
        {
            Name = "Button Request",
            Comment = "Create or update the visual states of buttons by STM. Only referenced buttons are updated",
            Identifiers = new Identifiers { Numeric = { 32 } },
            BitFields = new List<BitField>()
            {
                NID_PACKET,
                L_PACKET,
                Subset26.N_ITER,
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "N_ITER"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "NID_STM",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8
                            },
                            new BitField
                            {
                                Name = "NID_BUTTON",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8
                            },
                            new BitField
                            {
                                Name = "NID_BUTPOS",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 4
                            },
                            new BitField
                            {
                                Name = "NID_ICON",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8
                            },
                            new BitField
                            {
                                Name = "M_BUT_ATTRIB",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 10
                            },
                            L_CAPTION,
                            X_CAPTION
                        }
                    }
                },
            }
        };

        public static DataSetDefinition STM_35 => new DataSetDefinition()
        {
            Name = "Indicator Request",
            Comment = "Create or update the visual states of indicators by STM.\r\nOnly referenced indicators are updated",
            Identifiers = new Identifiers { Numeric = { 35 } },
            BitFields = new List<BitField>()
            {
                NID_PACKET,
                L_PACKET,
                Subset26.N_ITER,
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "N_ITER"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "NID_STM",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8
                            },
                            new BitField
                            {
                                Name = "NID_INDICATOR",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8
                            },
                            new BitField
                            {
                                Name = "NID_INDPOS",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 5
                            },
                            new BitField
                            {
                                Name = "NID_ICON",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8
                            },
                            new BitField
                            {
                                Name = "M_IND_ATTRIB",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 10
                            },
                            L_CAPTION,
                            X_CAPTION
                        }
                    }
                },
            }
        };

        public static DataSetDefinition STM_77 => new DataSetDefinition()
        {
            Name = "Diagnostic Message",
            Comment = "Packet which delivers diagnostic message.",
            Identifiers = new Identifiers { Numeric = { 77 } },
            BitFields = new List<BitField>
            {
                NID_PACKET,
                L_PACKET,
                Subset26.L_TEXT,
                Subset26.X_TEXT,
                new BitField
                {
                    Name = "N_L_ITER",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "N_L_ITER"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "M_DATA",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8
                            }
                        }
                    }
                }
            }
        };

        public static DataSetDefinition STM_136 => new DataSetDefinition()
        {
            Name = "BIU status to STM",
            Comment = "Transmission of the brake interface status to STM",
            Identifiers = new Identifiers { Numeric = { 136 } },
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
            Identifiers = new Identifiers { Numeric = { 139 } },
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

        public static DataSetDefinition STM_161 => new DataSetDefinition()
        {
            Name = "STM information to JRU",
            Comment = "National STM data transmitted to the JRU. (Structure of the data internal to each company)",
            Identifiers = new Identifiers { Numeric = { 161 } },
            BitFields = new List<BitField>
            {
                NID_PACKET,
                L_PACKET,
                new BitField
                {
                    Name = "T_JRU",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
                new BitField
                {
                    Name = "N_L_ITER",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "N_L_ITER"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "M_DATA",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8
                            }
                        }
                    }
                }
            }
        };

        public static DataSetDefinition STM_175 => new DataSetDefinition()
        {
            Name = "Train Data",
            Comment = "Validated train data",
            Identifiers = new Identifiers { Numeric = { 175 } },
            BitFields = new List<BitField>()
            {
                NID_PACKET,
                L_PACKET,
                Subset26.NID_OPERATIONAL,
                new BitField
                {
                    Name = "NC_TRAIN",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 15
                },
                new BitField
                {
                    Name = "L_TRAIN",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 12
                },
                Subset26.V_MAXTRAIN,
                new BitField
                {
                    Name = "M_LOADINGGAUGE",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "M_AXLELOAD",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 7
                },
                new BitField
                {
                    Name = "M_AIRTIGHT",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2
                },
                Subset26.N_ITER,
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "N_ITER"
                    },
                    Name = "M_TRACTION",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
            }
        };

        public static DataSetDefinition STM_176 => new DataSetDefinition()
        {
            Name = "Train Data additional brake characteristic to STM",
            Comment = "Validated train data additional braking characteristic",
            Identifiers = new Identifiers { Numeric = { 176 } },
            BitFields = new List<BitField>()
            {
                NID_PACKET,
                L_PACKET,
                new BitField
                {
                    Name = "T_BEGIN_SB_EF",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16
                },
                new BitField
                {
                    Name = "T_FULL_SB_EF",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16
                },
                Subset26.N_ITER,
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "N_ITER"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "V_SB_CHAR",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 10
                            },
                            new BitField
                            {
                                Name = "A_SB_CHAR",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8
                            }
                        }
                    }
                },
                new BitField
                {
                    Name = "T_BEGIN_EB_EF",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16
                },
                new BitField
                {
                    Name = "T_FULL_EB_EF",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16
                },
                Subset26.N_ITER,
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "N_ITER"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "V_EB_CHAR",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 10
                            },
                            new BitField
                            {
                                Name = "A_EB_CHAR",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8
                            }
                        }
                    }
                },
                new BitField
                {
                    Name = "T_TRACTION_CUT_OFF",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16
                },
                 new BitField
                {
                    Name = "A_MAX",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
            }
        };

        public static DataSetDefinition STM_177 => new DataSetDefinition()
        {
            Name = "Additional Data Values and date/time to STM",
            Identifiers = new Identifiers { Numeric = { 177 } },
            BitFields = new List<BitField>
            {
                NID_PACKET,
                L_PACKET,
                new BitField()
                {
                    Name = "NID_DRIVER",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                },
                Subset26.NID_ENGINE,
                Subset26.M_ADHESION,
                new BitField()
                {
                    Name = "T_YEAR",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 7,
                },
                new BitField()
                {
                    Name = "T_MONTH",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                },
                new BitField()
                {
                    Name = "T_DAY",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 5,
                },
                new BitField()
                {
                    Name = "T_HOUR",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 5,
                },
                new BitField()
                {
                    Name = "T_MINUTES",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 6,
                },
                new BitField()
                {
                    Name = "T_SECONDS",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 6,
                },
                new BitField()
                {
                    Name = "T_TTS",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 5,
                },
            }
        };

        public static DataSetDefinition STM_178 => new DataSetDefinition()
        {
            Name = "National values to STM",
            Comment = "Downloads a set of National Values",
            Identifiers = new Identifiers { Numeric = { 178 } },
            BitFields = new List<BitField>()
            {
                NID_PACKET,
                L_PACKET,
                Subset26.Q_SCALE,
                Subset26.V_NVSHUNT,
                Subset26.V_NVSTFF,
                Subset26.V_NVONSIGHT,
                Subset26.V_NVUNFIT,
                Subset26.V_NVREL,
                Subset26.D_NVROLL,
                Subset26.V_NVALLOWOVTRP,
                Subset26.V_NVSUPOVTRP,
                Subset26.D_NVOVTRP,
                Subset26.T_NVOVTRP,
                Subset26.D_NVPOTRP,
                Subset26.D_NVSTFF,
                Subset26.Q_NVDRIVER_ADHES
            }
        };

        public static DataSetDefinition STM_179 => new DataSetDefinition()
        {
            Name = "Specific NTC Data Entry request",
            Comment = "Request for Specific NTC Data Entry.\r\nThis packet can be grouped with other STM-179 packets by using the Q_FOLLOWING indicator in order to form one common Specific NTC Data Entry request.\r\nNote: The STM indicates the \"End of Specific NTC Data Entry\" by a packet STM-179, with N_ITER=0.",
            Identifiers = new Identifiers { Numeric = { 179 } },
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
            Identifiers = new Identifiers { Numeric = { 181 } },
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