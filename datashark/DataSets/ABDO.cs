using System.Collections.Generic;
using BitDataParser;

namespace TrainShark.DataSets
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
            DataSets.Add(DIA_167);
        }

        // Checked to Class 701 VSIS vF 2023-03-10 JS
        public static DataSetDefinition TR_7 => new DataSetDefinition
        {
            Name = "Tr_7 Station Info",
            Comment = "Several station information",
            Identifiers = new Identifiers { Numeric = { 230503600, 230503601 } },            
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

        // Checked to Class 701 VSIS vF 2023-03-10 JS
        public static DataSetDefinition TR_8 => new DataSetDefinition
        {
            Name = "Tr_8 SW Feedback",
            Comment = "Door release SW Feedback",
            Identifiers = new Identifiers { Numeric = { 230503602, 230503603 } },            
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

        // Checked to Class 701 VSIS vF 2023-03-10 JS
        public static DataSetDefinition TR_9 => new DataSetDefinition
        {
            Name = "Tr9 EB Feedback",
            Comment = "",
            Identifiers = new Identifiers { Numeric = { 230503604, 230503605 } },            
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

        // Corrected to Class 701 VSIS vF 2023-03-10 JS
        public static DataSetDefinition OBU_26 => new DataSetDefinition
        {
            Name = "Obu26 Doors Release Command",
            Comment = "",
            
            Identifiers = new Identifiers { Numeric = { 230503992, 230503993 }
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
                    Name = "NID_STATION_station",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 11                 
                },
                new BitField
                {
                    Name = "NID_STATION_platform",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 5
                },
                new BitField
                {
                    Name = "NID_TRACK",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16
                },
                new BitField
                {
                    Name = "D_PLATFORM",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "m"
                },
                new BitField
                {
                    Name = "L_PLATFORM",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "m"
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

        // Checked to Class 701 VSIS vF 2023-03-10 JS
        public static DataSetDefinition OBU_27 => new DataSetDefinition
        {
            Name = "Obu27",
            Comment = "Contains the brake effort value.",
            Identifiers = new Identifiers { Numeric = { 230503996, 230503997 }
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

        // Checked to Class 701 VSIS vF 2023-03-10 JS
        public static DataSetDefinition OBU_28 => new DataSetDefinition
        {
            Name = "Obu28",
            Comment = "Station list request",
            Identifiers = new Identifiers { Numeric = { 230503998, 230503999 }
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

        // Checked to Class 701 VSIS vF 2023-03-10 JS
        public static DataSetDefinition ATO_1 => new DataSetDefinition
        {
            Name = "Ato1",
            Comment = "",
            Identifiers = new Identifiers { Numeric = { 230560010, 230560011 }
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

        // Modified 2023-03-10 from Class 701 VSIS vF
        public static DataSetDefinition DIA_166 => new DataSetDefinition
        {
            Name = "DIA_166",
            Comment = "ATO Event data",
            Identifiers = new Identifiers { Numeric = { 230510307, 230511307 }
            },
            BitFields = new List<BitField>
            {
                new BitField { Name = "ATO00", Comment = "ABDO internal SW Failure", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False"  },
                new BitField { Name = "ATO01", Comment = "ABDO is pre-selected by driver", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO02", Comment = "ABDO is disabled by Driver", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO03", Comment = "Mission code is changed by Driver", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO04", Comment = "ABDO low adhesion is selected", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO05", Comment = "ABDO Normal adhesion is selected", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO06", Comment = "Acknowledgement of Automatic Brake Failure", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO07", Comment = "Acknowledgement of Automatic Door Opening Failure", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO08", Comment = "Acknowledgement of ABDO Failure", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO09", Comment = "Acknowledgement of Incorrect Stopping Position", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO0A", Comment = "ABDO Enable Indication", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO0B", Comment = "--not used--", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO0C", Comment = "--not used--", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO0D", Comment = "--not used--", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO0E", Comment = "--not used--", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO0F", Comment = "--not used--", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO10", Comment = "ABDO Disable Indication", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO11", Comment = "ABDO Active Indication", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO12", Comment = "ABDO Train Stop Indication", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO13", Comment = "--not used--", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO14", Comment = "--not used--", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO15", Comment = "--not used--", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO16", Comment = "--not used--", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO17", Comment = "--not used--", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" },
                new BitField { Name = "ATO18", Comment = "--not used--", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = "False" }                
            }
        };

        // Added 2023-03-10 from Class 701 VSIS vF
        public static DataSetDefinition DIA_167 => new DataSetDefinition
        {
            Name = "DIA_167",
            Comment = "ATO Environment data",
            Identifiers = new Identifiers { Numeric = { 230510308, 230511308 }
            },
            BitFields = new List<BitField>
            {
                new BitField { Name = "ATO_sw_error", Comment = "SW internal error code (set if > zero) to identify BDS internal diagnostic event.", BitFieldType = BitFieldType.UInt32, Length = 32, LookupTable = new LookupTable
                {
                    {"53249", "ABDO - Automatic Brake failure" },
                    {"53250", "ABDO - Automatic Door release failed" },
                    {"53251", "ABDO - Odometer failure" },
                    {"53252", "ABDO - Inhibited by TCMS" },
                    {"53253", "Train is not stopping in the correct position, Door release disabled" },
                    {"53254", "Mission code is not recognizing by ABDO" },
                    {"53255", "Adhesion information NOT received" },
                    {"53256", "Movement Detection with ADR Active" },
                    {"53257", "Invalid Packet 44 extract received (OBU-26)" },
                    {"53258", "Train platform overrun" },
                    {"53259", "Train platform underrun" },
                    {"53260", "Inconsistent ABDO trackside data" },
                    {"53261", "Missing ABDO trackside data" }
                } },
                new BitField { Name = "ATO_sw_failure_class", Comment = "SW failure class", BitFieldType = BitFieldType.UInt8, Length = 8 },
                new BitField { Name = "ATO_sw_error_level", Comment = "SW error level", BitFieldType = BitFieldType.UInt8, Length = 8 },
                new BitField { Name = "ATO_currend_speed", Comment = "Estimated train speed in km/h at the actual time, without tolerance added", BitFieldType = BitFieldType.UInt16, Length = 16 },
                new BitField { Name = "ATO_train_stop_pattern", Comment = "Train Stopping Pattern (based on the Event TRN)", BitFieldType = BitFieldType.UInt16, Length = 16  },
                new BitField { Name = "ATO_VIO_failure", Comment = "VIO Failure for: - ABDO is active - ABDO brake is applied - Release left side door - Release right side door", BitFieldType = BitFieldType.UInt16, Length = 16 },
                new BitField { Name = "ATO_curr_dist_stop", Comment = "Current Distance to Stopping Point", BitFieldType = BitFieldType.UInt16, Length = 16 },
                new BitField { Name = "ATO_calc_curr_v_target", Comment = "Calculation value for current target speed", BitFieldType = BitFieldType.UInt16, Length = 16 },
                new BitField { Name = "ATO_calc_curr_brake_effort", Comment = "Calculation value for current brake effort", BitFieldType = BitFieldType.UInt16, Length = 16 },
                new BitField { Name = "ATO_calc_curr_gradient", Comment = "Calculation value for current gradient", BitFieldType = BitFieldType.UInt16, Length = 16 },
                new BitField { Name = "ATO_calc_train_type", Comment = "Calculation value for train type", BitFieldType = BitFieldType.UInt16, Length = 16 },
                new BitField { Name = "ATO_spare1", Comment = "--not used--", BitFieldType = BitFieldType.ByteArray, Length = 80 }                
            }
        };

        // Added 2023-03-10 from ABDO JRU IFS 1.7
        public static DataSetDefinition ABDO_JRU => new DataSetDefinition
        {
            Name = "ABDO JRU Data",
            Comment = "ATO Environmane data",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "M_ABDO_1",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 6,
                    LookupTable = new LookupTable
                    {
                        {"0", "ABDO Disabled"},
                        {"1", "ABDO Available for Enabling"},
                        {"2", "ABDO Enabled"},
                        {"3", "ABDO Active" },
                        {"4", "ABDO Inhibited" },
                        {"5", "ABDO Error" }
                    }
                },
                new BitField
                {
                    Name = "M_ABDO_2",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "ABDO NOT enabled by Driver"},
                        {"1", "ABDO enabled by Driver"}
                    }
                },
                new BitField
                {
                    Name = "M_ABDO_3",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "ABDO NOT disabled by Driver"},
                        {"1", "ABDO disabled by Driver"}
                    }
                },
                new BitField
                {
                    Name = "M_ABDO_4",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "Error"},
                        {"1", "ABDO SET HW Signal disabled"},
                        {"2", "ABDO SET HW Signal Enabled"},
                        {"3", "Undefined/Default" }
                    }
                },
                new BitField
                {
                    Name = "M_ABDO_5",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "Error"},
                        {"1", "ABDO BRAKE HW Signal disabled"},
                        {"2", "ABDO BRAKE HW Signal Enabled"},
                        {"3", "Undefined/Default" }
                    }
                },
                new BitField
                {
                    Name = "M_ABDO_6",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "Train NOT Standstill"},
                        {"1", "Train at Standstill"}
                    }
                },
                new BitField
                {
                    Name = "M_ABDO_7",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "Error"},
                        {"1", "ABDO SW Door Released Left disabled"},
                        {"2", "ABDO SW Door\r\nReleased Left Enabled"},
                        {"3", "Undefined/Default" }
                    }
                },
                new BitField
                {
                    Name = "M_ABDO_8",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "Error"},
                        {"1", "ABDO SW Door Released Right disabled"},
                        {"2", "ABDO SW Door Released Right Enabled"},
                        {"3", "Undefined/Default" }
                    }
                },
                new BitField
                {
                    Name = "M_ABDO_9",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "Error"},
                        {"1", "ABDO HW Door Released Left disabled"},
                        {"2", "ABDO HW Door\r\nReleased Left Enabled"},
                        {"3", "Undefined/Default" }
                    }
                },
                new BitField
                {
                    Name = "M_ABDO_10",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "Error"},
                        {"1", "ABDO HW Door Released Right disabled"},
                        {"2", "ABDO HW Door Released Right Enabled"},
                        {"3", "Undefined/Default" }
                    }
                },
                new BitField
                {
                    Name = "M_ABDO_11",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                    LookupTable = new LookupTable
                    {
                        {"0", "Error"},
                        {"1", "Enabled"},
                        {"2", "Disabled"},
                        {"3", "Undefined/Default" }
                    }
                },
                new BitField
                {
                    Name = "M_ABDO_12",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "Train at Standstill is NOT in correct position"},
                        {"1", "Train at Standstill is in\r\nCorrect position"}
                    }
                },
                new BitField
                {
                    Name = "M_ABDO_13",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "Emergency Brake NOT applied"},
                        {"1", "Emergency Brake applied"}
                    }
                },
                new BitField
                {
                    Name = "M_ABDO_14",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 3,
                    LookupTable = new LookupTable
                    {
                        {"0", "No adhesion information"},
                        {"1", "Slippery rail"},
                        {"2", "Non-Slippery rail"}
                    }
                },
                new BitField
                {
                    Name = "M_ABDO_15",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                    LookupTable = new LookupTable
                    {
                        {"0", "No packet 44"},
                        {"1", "ABDO Packet 44 for 5Car"},
                        {"2", "ABDO Packet 44 for 10Car"},
                        {"3", "ABDO Packet 44 for 5and 10 Car" },
                        {"4", "Invalid Packet 44" }
                    }
                },
                new BitField
                {
                    Name = "M_ABDO_16",
                    BitFieldType = BitFieldType.Spare,
                    Length = 5
                }
            }
        };
    }
}