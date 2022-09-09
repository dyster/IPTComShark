using System.Collections.Generic;
using BitDataParser;

namespace IPTComShark.DataSets
{
    public class ABDO : DataSetCollection
    {
        public ABDO()
        {
            Name = "ABDO";
            Description = "";

            DataSets.Add(TR_7);
            DataSets.Add(TR_8);
            DataSets.Add(TR_9);
            DataSets.Add(OBU_26);
            DataSets.Add(OBU_27);
            DataSets.Add(OBU_28);
            DataSets.Add(ATO_1);
            DataSets.Add(DIA_166);
        }

        public static DataSetDefinition TR_7 => new DataSetDefinition
        {
            Name = "Tr_7 Station Info",
            Comment = "Several station information",
            Identifiers = new List<string>
            {
                "230503600",
                "230503601"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "N_Station_To_Stop",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Number of station stops (valid bits in the following bit table, counting from MSB=bit 0)"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings {Name = "N_Station_To_Stop"},
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "Station_To_Stop_N",
                                BitFieldType = BitFieldType.Bool,
                                Length = 1,
                                LookupTable = new LookupTable
                                {
                                    {"False", "No Stop"},
                                    {"True", "Stop"}
                                }
                            }
                        }
                    }
                }
            }
        };

        public static DataSetDefinition TR_8 => new DataSetDefinition
        {
            Name = "Tr_8 SW Feedback",
            Comment = "Door release SW Feedback",
            Identifiers = new List<string>
            {
                "230503602",
                "230503603"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "Door_left_sw_feedback",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    LookupTable = new LookupTable
                    {
                        {"False", "Left door not released"},
                        {"True", "Left door released"}
                    }
                },
                new BitField
                {
                    Name = "Door_right_sw_feedback",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    LookupTable = new LookupTable
                    {
                        {"False", "Right door not released"},
                        {"True", "Right door released"}
                    }
                },
                new BitField
                {
                    Name = "spare bits",
                    BitFieldType = BitFieldType.HexString,
                    Length = 190,
                },
                new BitField
                {
                    Name = "Door_left_sw_feedback_validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Door_right_sw_feedback_validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "unused validity bits",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 14,
                },
                new BitField
                {
                    Name = "TR8_SSW1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                },
                new BitField
                {
                    Name = "TR8_SSW2",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                },
                new BitField
                {
                    Name = "TR8_SSW3",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                },
            }
        };

        public static DataSetDefinition TR_9 => new DataSetDefinition
        {
            Name = "Tr9 EB Feedback",
            Comment = "",
            Identifiers = new List<string>
            {
                "230503604",
                "230503605"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EB_feedback",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    LookupTable = new LookupTable
                    {
                        {"False", "Not applied"},
                        {"True", "Applied"}
                    }
                },
                new BitField
                {
                    Name = "spare bits",
                    BitFieldType = BitFieldType.HexString,
                    Length = 7,
                },
                new BitField
                {
                    Name = "ATO_Inhibit",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    LookupTable = new LookupTable
                    {
                        {"0", "NOT Inhibited"},
                        {"1", "Inhibited"}
                    }
                },
                new BitField
                {
                    Name = "Adhesion_Data",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    LookupTable = new LookupTable
                    {
                        {"0", "Slippery rail"},
                        {"1", "Non-slippery rail"}
                    }
                },
                new BitField
                {
                    Name = "TBC_Brake",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    LookupTable = new LookupTable
                    {
                        {"0", "Default"},
                        {"1", "Enabled"},
                        {"2", "Disabled"},
                        {"3", "Failure"},
                    }
                },
                new BitField
                {
                    Name = "Enable_ATO",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    LookupTable = new LookupTable
                    {
                        {"0", "Disabled"},
                        {"1", "Enabled"}
                    }
                },
                new BitField
                {
                    Name = "Disable_ATO",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    LookupTable = new LookupTable
                    {
                        {"0", "Disabled"},
                        {"1", "Enabled"}
                    }
                },
                new BitField
                {
                    Name = "Skip_Next_Station_ATO",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    LookupTable = new LookupTable
                    {
                        {"0", "Disabled"},
                        {"1", "Enabled"}
                    }
                },
                new BitField
                {
                    Name = "spare bits",
                    BitFieldType = BitFieldType.HexString,
                    Length = 184,
                },
                new BitField
                {
                    Name = "EB_Feedback validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ATO_Inhibit validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Adhesion_Data validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "TBC_Brake validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Enable_ATO validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Disable_ATO validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Skip_Next_Station_ATO validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "unused validity bits",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 9,
                },
            }
        };

        public static DataSetDefinition OBU_26 => new DataSetDefinition
        {
            Name = "Obu26 Doors Release Command",
            Comment = "",
            Identifiers = new List<string>
            {
                "230503992",
                "230503993"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "Door_left_release",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 1
                },
                new BitField
                {
                    Name = "Door_right_release",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 1
                },
                new BitField
                {
                    Name = "Q_DOOR_SIDE",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "doors released left side"},
                        {"1", "doors released right side"},
                        {"2", "doors released both sides"},
                        {"3", "spare"},
                    }
                },
                new BitField
                {
                    Name = "A_DIR",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "spare"},
                        {"1", "Up"},
                        {"2", "Down"},
                        {"3", "spare"},
                    }
                },
                new BitField
                {
                    Name = "spare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2
                },
                new BitField
                {
                    Name = "Q_CONFIGURATION",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    LookupTable = new LookupTable
                    {
                        {"0", "not applicable"},
                        {"1", "Class 701 5 car"},
                        {"2", "Class 701 10 car"},
                        {"3", "Class 701 5&10 car"},
                    }
                },
                new BitField
                {
                    Name = "NID_STATION",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "cm"
                },
                new BitField
                {
                    Name = "L_PLATFORM",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "cm"
                },
                new BitField
                {
                    Name = "spare bits",
                    BitFieldType = BitFieldType.HexString,
                    Length = 112
                },
                new BitField
                {
                    Name = "Door_left_release validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Door_right_release validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Q_DOOR_SIDE validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "A_DIR validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Q_CONFIGURATION validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "NID_STATION validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "NID_TRACK validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "D_PLATFORM validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "L_PLATFORM validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "unused validity bits",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 7,
                },
                new BitField
                {
                    Name = "OBU26_SSW1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                },
                new BitField
                {
                    Name = "OBU26_SSW2",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                },
                new BitField
                {
                    Name = "OBU26_SSW3",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                },
            }
        };

        public static DataSetDefinition OBU_27 => new DataSetDefinition
        {
            Name = "Obu27",
            Comment = "Contains the brake effort value.",
            Identifiers = new List<string>
            {
                "230503996",
                "230503997"
            },
            BitFields = new List<BitField>()
            {
                new BitField()
                {
                    Name = "BrakeEffort",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Brake effort in % (1 = 0.01 %)"
                },
                new BitField
                {
                    Name = "Enable_Push_Button",
                    Comment = "Enable push button indication",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    LookupTable = new LookupTable
                    {
                        {"0", "OFF"},
                        {"1", "Steady"},
                        {"2", "Flashing"}
                    }
                },
                new BitField
                {
                    Name = "Disable_Push_Button",
                    Comment = "Disable push button indication",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    LookupTable = new LookupTable
                    {
                        {"0", "OFF"},
                        {"1", "Steady"},
                        {"2", "Flashing"}
                    }
                },
            }
        };

        public static DataSetDefinition OBU_28 => new DataSetDefinition
        {
            Name = "Obu28",
            Comment = "Station list request",
            Identifiers = new List<string>
            {
                "230503998",
                "230503999"
            },
            BitFields = new List<BitField>()
            {
                new BitField()
                {
                    Name = "NID_OPERATION",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                }
            }
        };

        public static DataSetDefinition ATO_1 => new DataSetDefinition
        {
            Name = "Ato1",
            Comment = "",
            Identifiers = new List<string>
            {
                "230560010",
                "230560011"
            },
            BitFields = new List<BitField>()
            {
                new BitField()
                {
                    Name = "ATO1_reference_speed",
                    Comment = "cm/s",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16
                },
                new BitField
                {
                    Name = "ATO1_indication_type",                    
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    LookupTable = new LookupTable
                    {
                        {"0", "Available"},
                        {"1", "Active"},
                        {"2", "Enabled"}
                    }
                },
                new BitField
                {
                    Name = "ATO1_indication_attribute",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    LookupTable = new LookupTable
                    {
                        {"0", "Invisible"},
                        {"1", "Steady"},
                        {"2", "Flashing"}
                    }
                },
                new BitField
                {
                    Name = "ATO1_train_stop_indication",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    LookupTable = new LookupTable
                    {
                        {"0", "Invisible"},
                        {"1", "Steady"},
                    }
                },
                new BitField
                {
                    Name = "ATO1_Text_Criteria",
                    Comment = "DMI criteria for handling the text. Note: see MMI_Q_TEXT_CRITERIA in EVC8",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,                    
                },
                new BitField
                {
                    Name = "ATO1_I_Text",
                    Comment = "Text Identifier",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                },
                new BitField
                {
                    Name = "ATO1_L_Text",
                    Comment = "Text Length",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                },
                new BitField
                {
                    Name = "ATO1_X_Text",
                    Comment = "Text string",
                    BitFieldType = BitFieldType.StringAscii,
                    VariableLengthSettings = new VariableLengthSettings(){ Name = "ATO1_L_Text" }                    
                },
            }
        };

        public static DataSetDefinition DIA_166 => new DataSetDefinition
        {
            Name = "DIA_166",
            Comment = "",
            Identifiers = new List<string>
            {
                "230510307",
                "230510308"
            }
        };
    }
}