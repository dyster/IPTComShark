using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BitDataParser;

namespace TrainShark.DataSets
{
    public static class Subset27
    {
        public static DataSetDefinition EBrakeCommandState = new DataSetDefinition
        {
            Name = "3 EMERGENCY BRAKE COMMAND STATE",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "M_BRAKE_COMMAND_STATE",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                }
            }
        };

        public static DataSetDefinition SBrakeCommandState = new DataSetDefinition
        {
            Name = "4 SERVICE BRAKE COMMAND STATE",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "M_BRAKE_COMMAND_STATE",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                }
            }
        };

        public static DataSetDefinition MessageToRadioInfillUnit = new DataSetDefinition
        {
            Name = "5 MESSAGE TO RADIO INFILL UNIT",
            BitFields = new List<BitField>
            {
                Subset26.NID_C,
                Subset26.NID_RIU
            }
        };

        public static DataSetDefinition MessageFromRadioInfillUnit = new DataSetDefinition
        {
            Name = "8 MESSAGE FROM RADIO INFILL UNIT",
            BitFields = new List<BitField>
            {
                Subset26.NID_C,
                Subset26.NID_RIU
            }
        };

        /// <summary>
        /// This is just the header!
        /// </summary>
        public static DataSetDefinition MessageFromRBC = new DataSetDefinition
        {
            Name = "9 MESSAGE FROM RBC",
            BitFields = new List<BitField>
            {
                Subset26.NID_C,
                Subset26.NID_RBC
            }
        };

        /// <summary>
        /// This is just the header!
        /// </summary>
        public static DataSetDefinition MessageToRBC = new DataSetDefinition
        {
            Name = "10 MESSAGE TO RBC",
            BitFields = new List<BitField>
            {
                Subset26.NID_C,
                Subset26.NID_RBC
            }
        };

        public static DataSetDefinition DriversActions = new DataSetDefinition
        {
            Name = "11 DRIVER’S ACTIONS",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "M_DRIVERACTIONS",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Driver’s actions",
                    LookupTable = new LookupTable
                    {
                        {"0", "Ack of On Sight mode"},
                        {"1", "Ack of Shunting mode"},
                        {"2", "Ack of Train Trip"},
                        {"3", "Ack of Staff Responsible mode"},
                        {"4", "Ack of Unfitted mode"},
                        {"5", "Ack of Reversing mode"},
                        {"6", "Ack level 0"},
                        {"7", "Ack level 1"},
                        {"8", "Ack level 2"},
                        {"9", "Ack level 3"},
                        {"10", "Ack level NTC"},
                        {"11", "Shunting selected"},
                        {"12", "Non Leading selected"},
                        {"13", "Ack of Limited Supervision mode"},
                        {"14", "Override selected"},
                        {"15", "“Continue Shunting on desk closure” selected"},
                        {"16", "Brake release acknowledgement"},
                        {"17", "Exit of Shunting selected"},
                        {"18", "Isolation selected"},
                        {"19", "Start selected"},
                        {"20", "Train Data Entry requested"},
                        {"21", "Validation of train data"},
                        {"22", "Confirmation of Track Ahead Free"},
                        {"23", "Ack of Plain Text information"},
                        {"24", "Ack of Fixed Text information"},
                        {"25", "Request to hide supervision limits"},
                        {"26", "Train integrity confirmation"},
                        {"27", "Request to show supervision limits"},
                        {"28", "Ack of SN mode"},
                        {"29", "Selection of Language"},
                        {"30", "Request to show geographical position"},
                        {"31", "Spare"},
                        {"32", "Spare"},
                        {"33", "Request to hide geographical position"},
                        {"34", "Level 0 selected"},
                        {"35", "Level 1 selected"},
                        {"36", "Level 2 selected"},
                        {"37", "Level 3 selected"},
                        {"38", "Level NTC selected"},
                        {"39", "Request to show tunnel stopping area information"},
                        {"40", "Request to hide tunnel stopping area information"},
                        {"41", "Scroll up button activated"},
                        {"42", "Scroll down button activated"}
                    }
                }
            }
        };

        public static DataSetDefinition BaliseGroupError = new DataSetDefinition
        {
            Name = "12 BALISE GROUP ERROR",
            BitFields = new List<BitField>
            {
                Subset26.NID_C,
                new BitField
                {
                    Name = "NID_ERRORBG",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 14
                },
                Subset26.M_ERROR
            }
        };

        public static DataSetDefinition RadioError = new DataSetDefinition
        {
            Name = "13 RADIO ERROR",
            BitFields = new List<BitField>
            {
                Subset26.NID_C,
                Subset26.NID_RBC,
                Subset26.M_ERROR
            }
        };

        public static DataSetDefinition InformationFromColdMovementDetector = new DataSetDefinition
        {
            Name = "15 INFORMATION FROM COLD MOVEMENT DETECTOR",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "M_COLD_MVT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "No cold movement occurred"},
                        {"1", "Cold movement detected"},
                        {"2", "No cold movement information available"},
                        {"3", "Spare"}
                    }
                }
            }
        };

        public static DataSetDefinition StartDisplayingFixedTextMessage = new DataSetDefinition
        {
            Name = "16 START DISPLAYING FIXED TEXT MESSAGE",
            BitFields = new List<BitField> {Subset26.Q_TEXT}
        };

        public static DataSetDefinition StopDisplayingFixedTextMessage = new DataSetDefinition
        {
            Name = "17 STOP DISPLAYING FIXED TEXT MESSAGE",
            BitFields = new List<BitField> {Subset26.Q_TEXT}
        };

        public static DataSetDefinition StartDisplayingPlainTextMessage = new DataSetDefinition
        {
            Name = "18 START DISPLAYING PLAIN TEXT MESSAGE",
            BitFields = new List<BitField>
            {
                Subset26.L_TEXT,
                Subset26.X_TEXT
            }
        };

        public static DataSetDefinition StopDisplayingPlainTextMessage = new DataSetDefinition
        {
            Name = "19 STOP DISPLAYING PLAIN TEXT MESSAGE",
            BitFields = new List<BitField>
            {
                Subset26.L_TEXT,
                Subset26.X_TEXT
            }
        };

        public static DataSetDefinition SpeedAndDistanceMonitoringInformation = new DataSetDefinition
        {
            Name = "20 SPEED AND DISTANCE MONITORING INFORMATION",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "M_SDMTYPE",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "Ceiling speed monitoring (CSM)"},
                        {"1", "Pre indication monitoring (PIM)"},
                        {"2", "Target speed monitoring (TSM)"},
                        {"3", "Release speed monitoring (RSM)"}
                    }
                },
                new BitField
                {
                    Name = "M_SDMSUPSTAT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 3,
                    LookupTable = new LookupTable
                    {
                        {"0", "Normal Status"},
                        {"1", "Indication Status"},
                        {"2", "Overspeed Status"},
                        {"3", "Warning Status"},
                        {"4", "Intervention Status"},
                        {"5", "Spare"},
                        {"6", "Spare"},
                        {"7", "Spare"}
                    }
                },
                new BitField
                {
                    Name = "V_PERM",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 10,
                    Comment = "Permitted speed in km/h"
                },
                new BitField
                {
                    Name = "V_SBI",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 10,
                    Comment = "Service brake intervention speed in km/h"
                },
                new BitField
                {
                    Name = "V_TARGET",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 10,
                    Comment = "Target speed in km/h"
                },
                new BitField
                {
                    Name = "D_TARGET",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 15,
                    Comment = "Target distance in meters"
                },
                new BitField
                {
                    Name = "V_RELEASE",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 10,
                    Comment = "Release speed in km/h"
                }
            }
        };

        public static DataSetDefinition DmiSymbolStatus = new DataSetDefinition
        {
            Name = "21 DMI SYMBOL STATUS",
            BitFields = new List<BitField>
            {
                new BitField {Name = "LS01", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "LX01", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "DR05", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "DR04", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "DR03", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "DR02", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "DR01", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC37", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC36", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC35", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC34", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC33", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC32", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC31", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC30", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC29", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC28", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC27", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC26", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC25", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC24", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC23", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC22", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC21", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC20", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC19", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC18", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC17", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC16", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC15", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC14", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC13", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC12", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC11", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC10", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC09", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC08", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC07", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC06", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC05", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC04", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC03", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC02", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "TC01", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "ST06", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "ST05", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "ST04", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "ST03", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "ST02", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "ST01", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO22", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO21", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO20", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO19", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO18", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO17", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO16", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO15", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO14", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO13", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO12", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO11", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO10", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO09", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO08", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO07", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO06", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO05", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO04", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO03", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO02", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MO01", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "LE15", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "LE14", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "LE13", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "LE12", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "LE11", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "LE10", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "LE09", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "LE08", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "LE07", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "LE06", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "LE05", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "LE04", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "LE03", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "LE02", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "LE01", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
            }
        };

        public static DataSetDefinition DmiSoundStatus = new DataSetDefinition
        {
            Name = "22 DMI SOUND STATUS",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "Sound Warning",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Sound Overspeed",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Sound InfoOnDmi",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
            }
        };

        public static DataSetDefinition DmiSystemStatusMessage = new DataSetDefinition
        {
            Name = "23 DMI SYSTEM STATUS MESSAGE",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "Radio network registration failed",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "Route unsuitable – traction system",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "Route unsuitable – loading gauge",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "Route unsuitable – axle load category",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "No track description",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "RV distance exceeded",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "Emergency stop",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "SR stop order",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "SH stop order",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "SR distance exceeded",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "No MA received at level transition",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "Unauthorized passing of EOA / LOA",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "Train is rejected",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "Train data changed",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "Trackside not compatible",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "SH request failed",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField {Name = "SH refused", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField
                {
                    Name = "Runaway movement",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField {Name = "Entering OS", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "Entering FS", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField
                {
                    Name = "Communication error",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "Trackside malfunction",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "Balise read error",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false
                },
            }
        };

        public static DataSetDefinition AdditionalData = new DataSetDefinition
        {
            Name = "24 ADDITIONAL DATA",
            BitFields = new List<BitField>
            {
                Subset26.M_ADHESION,
                Subset26.NID_MN,
                new BitField
                {
                    Name = "Q_RBCENTRY",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "Contact last known RBC"},
                        {"1", "Use short number"},
                        {"2", "Enter RBC data"},
                        {"3", "Spare"}
                    }
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "Q_RBCENTRY",
                        LookUpTable = new IntLookupTable
                        {
                            {0, 0},
                            {1, 0},
                            {2, 1},
                            {3, 0}
                        }
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            Subset26.NID_C,
                            Subset26.NID_RBC,
                            Subset26.NID_RADIO
                        }
                    }
                },
                Subset26.NID_OPERATIONAL
            }
        };

        public static DataSetDefinition NtcSelected = new DataSetDefinition
        {
            Name = "26 NTC SELECTED",
            BitFields = new List<BitField>
            {
                Subset26.NID_NTC
            }
        };

        public static DataSetDefinition SleepingInput = new DataSetDefinition
        {
            Name = "30 SLEEPING INPUT",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "SleepingInput",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    LookupTable = new LookupTable
                    {
                        {"False", "Sleeping not requested"},
                        {"True", "Sleeping requested"}
                    }
                }
            }
        };

        public static DataSetDefinition PassiveShuntingInput = new DataSetDefinition
        {
            Name = "31 PASSIVE SHUNTING INPUT",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "SleepingInput",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    LookupTable = new LookupTable
                    {
                        {"False", "Passive shunting not permitted"},
                        {"True", "Passive shunting permitted"}
                    }
                }
            }
        };

        public static DataSetDefinition NonLeadingInput = new DataSetDefinition
        {
            Name = "32 NON LEADING INPUT",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "SleepingInput",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    LookupTable = new LookupTable
                    {
                        {"False", "Non leading not permitted"},
                        {"True", "Non leading permitted"}
                    }
                }
            }
        };

        public static DataSetDefinition CabStatus = new DataSetDefinition
        {
            Name = "38 CAB STATUS",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "M_CAB_A_STATUS",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Q_CAB_B",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "M_CAB_B_STATUS",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                }
            }
        };


        public static DataSetDefinition DirectionControllerPosition = new DataSetDefinition
        {
            Name = "39 DIRECTION CONTROLLER POSITION",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "M_DIRECTION_CONTROLLER",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "Neutral"},
                        {"1", "Backward"},
                        {"2", "Forward"},
                        {"3", "Spare"}
                    }
                }
            }
        };

        public static DataSetDefinition TractionStatus = new DataSetDefinition
        {
            Name = "40 TRACTION STATUS",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "M_TRACTION_STATUS",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                }
            }
        };

        public static DataSetDefinition TypeOfTrainDataEntry = new DataSetDefinition
        {
            Name = "41 TYPE OF TRAIN DATA",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "M_TRAIN_DATA_ENTRY",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 2,
                    LookupTable = new LookupTable
                    {
                        {"0", "Fixed"},
                        {"1", "Flexible"},
                        {"2", "Switchable"},
                        {"3", "Spare"}
                    }
                }
            }
        };

        public static DataSetDefinition NationalSystemIsolation = new DataSetDefinition
        {
            Name = "42 NATIONAL SYSTEM ISOLATION",
            BitFields = new List<BitField>
            {
                Subset26.NID_NTC,
                new BitField
                {
                    Name = "M_NATIONAL_SYSTEM_ISOLATION",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 1,
                    LookupTable = new LookupTable
                    {
                        {"0", "NTC Isolated"},
                        {"1", "NTC Not Isolated"}
                    }
                }
            }
        };

        public static DataSetDefinition TCOState = new DataSetDefinition
        {
            Name = "43 TRACTION CUT OFF COMMAND STATE",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "M_TCO_COMMAND_STATE",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 1,
                    LookupTable = new LookupTable
                    {
                        {"0", "Not Commanded"},
                        {"1", "Commanded"}
                    }
                }
            }
        };

        public static float ImplementationLevel
        {
            get
            {
                IEnumerable<FieldInfo> fieldInfos =
                    typeof(Subset27).GetFields().Where(f => f.FieldType == typeof(DataSetDefinition));

                // 45 in total


                return fieldInfos.Count() / 45f;
            }
        }

        public static DataSetDefinition Header => new DataSetDefinition
        {
            Name = "Subset27 Header",
            Comment = "Contains only the header of SS27, none of the various messages",
            BitFields = new List<BitField>
            {
                new BitField {Name = "NID_MESSAGE", BitFieldType = BitFieldType.UInt8, Length = 8},
                new BitField {Name = "L_MESSAGE", BitFieldType = BitFieldType.UInt16, Length = 11},
                new BitField {Name = "Date", BitFieldType = BitFieldType.ByteArray, Length = 38},
                Subset26.Q_SCALE, //2
                Subset26.NID_LRBG, //24
                Subset26.D_LRBG, //15
                Subset26.Q_DIRLRBG, //2
                Subset26.Q_DLRBG, //2
                Subset26.L_DOUBTOVER, //15
                Subset26.L_DOUBTUNDER, //15

                new BitField {Name = "V_TRAIN", BitFieldType = BitFieldType.UInt16, Length = 10, Comment = "km/h"},
                new BitField {Name = "DRIVER_ID", BitFieldType = BitFieldType.StringAscii, Length = 128},
                Subset26.NID_ENGINE, //24
                Subset26.M_VERSION, //7
                Subset26.M_LEVEL, //3
                Subset26.M_MODE //4
            }
        };
    }
}