using sonesson_tools.BitStreamParser;
using System.Collections.Generic;

namespace IPTComShark.Parsers
{
    public class ETCSDiag : DataSetCollection
    {
        public ETCSDiag()
        {
            this.Name = "ETCS Diagnostics";
            this.Description = "Diagnostic Events and Environment Data of the ETCS Subsystem Components";

            DataSets.Add(DIA_201);
            DataSets.Add(DIA_202);
            DataSets.Add(DIA_203);
            DataSets.Add(DIA_204);
            DataSets.Add(DIA_205);
            DataSets.Add(DIA_206);
            DataSets.Add(DIA_207);
            DataSets.Add(DIA_209);
            DataSets.Add(DIA_212);
            DataSets.Add(DIA_214);
        }

        // checked 20190602 1.6 DIAG manual JS
        public static DataSetDefinition DIA_201 => new DataSetDefinition
        {
            Name = "DIA_201 GLOBAL_ENVIRONMENT_DATA_2",
            Comment = "Dataset definition of part 2 of global environment data.",
            Identifiers = new List<string>
            {
                "230510320",
                "230511320"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "TrainNumber",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "NID_OPERATIONAL_STM according to [SS058]"
                },
                new BitField
                {
                    Name = "SDP_CurrentPosition",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Odometer position"
                },
                new BitField
                {
                    Name = "SDP_CurrentSpeed",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Odometer speed"
                },
                new BitField
                {
                    Name = "SDP_ConfidenceInterval",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Odometer confidence interval"
                },
                new BitField
                {
                    Name = "BrakePipePressure",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Pressure in bar, 1000d = 1Bar, range is 0-10 Bar"
                },
                new BitField
                {
                    Name = "EB1 Command",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "EB2 Command",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "SB Command",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "TCO Command",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "EB Feedback",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "SB Feedback",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "TCO Feedback",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Brake Test",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 1,
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Successful"},
                        {"1", "Error"}
                    }
                },
                new BitField
                {
                    Name = "Isolation",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 1,
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Not Isolated"},
                        {"1", "Isolated"}
                    }
                },
                new BitField
                {
                    Name = "EB1cutoff",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 1,
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Cut Off"},
                        {"1", "Not Cut Off"}
                    }
                },
                new BitField
                {
                    Name = "EB2cutoff",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 1,
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Cut Off"},
                        {"1", "Not Cut Off"}
                    }
                },
                new BitField
                {
                    Name = "SB Error",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Bypass relay error",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "TCO red HW failure",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2
                },
                new BitField
                {
                    Name = "Cab Status",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 2,
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "No Cab Active"},
                        {"1", "Cab 1 Active"},
                        {"2", "Cab 2 Active"},
                        {"3", "Spare"}
                    }
                },
                new BitField
                {
                    Name = "Direction Controller",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 2,
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Neutral"},
                        {"1", "Forward"},
                        {"2", "Backward"},
                        {"3", "Not Used"}
                    }
                },
                new BitField
                {
                    Name = "Sleeping",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Non Leading",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Passive Shunting",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1
                },
                new BitField
                {
                    Name = "LastDriverAction",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Last driver action (button, iso, brake)"
                },
                new BitField
                {
                    Name = "ETC Mode",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "The current ETCS Mode"
                },
                new BitField
                {
                    Name = "ETC Level",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "The current ETCS Level"
                },
                new BitField
                {
                    Name = "ETC_BrakeError",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = ""
                },
                new BitField
                {
                    Name = "SDP_SlipSlideInformation",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Odometer slip slide information"
                },
                new BitField
                {
                    Name = "SDP_DrivingDirection",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Odometer driving direction"
                },
                new BitField
                {
                    Name = "WheelDiameterCompensation",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 1,
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "No"},
                        {"1", "Compensating"}
                    }
                },
                new BitField
                {
                    Name = "ColdMovementDetection",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 1,
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "No"},
                        {"1", "Detection"}
                    }
                },
                new BitField
                {
                    Name = "Spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 6
                },
                new BitField
                {
                    Name = "NID_STM_in_DA",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "The current NID_STM of the STM which is in DA."
                },
                new BitField
                {
                    Name = "Spare4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 24
                },
            }
        };

        // checked 20190712 1.6 DIAG manual RVV
        public static DataSetDefinition DIA_202 => new DataSetDefinition
        {
            Name = "DIA_202 GLOBAL_ENVIRONMENT_DATA_3",
            Comment = "Dataset definition of part 3 of global environment data.",
            Identifiers = new List<string>
            {
                "230510330",
                "230511330"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "RBC_ID",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "RBC identification"
                },
                new BitField
                {
                    Name = "RBC_ETCS_IDENTITY",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "RBC ETCS identity number. Allowed values according to [SS026], NID_C, NID_RBC and the packet 42 description." +
                                "The 14 least significant bits contain the value of NID_RBC, the 10 next higher bits contain NID_C."
                },
                new BitField
                {
                    Name = "BTM_TestTime",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Time at last BTM test."
                },
                new BitField
                {
                    Name = "BRAKE_TestTime",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Time at last brake test"
                },
                new BitField
                {
                    Name = "NID_RADIO_1",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "NID Radio part 1. Allowed values according to [SS026], NID_RADIO."
                },
                new BitField
                {
                    Name = "NID_RADIO_2",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "NID Radio part 2. Allowed values according to [SS026], NID_RADIO."
                },
                new BitField
                {
                    Name = "GSMR_Modem1SignalQuality",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Network Signal quality on Modem 1"
                },
                new BitField
                {
                    Name = "GSMR_Modem2SignalQuality",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Network Signal quality on Modem 2"
                },
                new BitField
                {
                    Name = "V_NVSHUNT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Shunting mode (permitted) speed limit. Allowed values according to [SS026], V_NVSHUNT."
                },
                new BitField
                {
                    Name = "PARTIAL_BLOCK_FLAG",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Identifies if the system is in Partial Block Mode (PARTIAL_BLOCK = 1, FULL_BLOCK = 2)."
                },
                new BitField
                {
                    Name = "RBC_DATA_QUALIFIER",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "RBC data qualifier (0 = unknown; 1 = invalid; 2 = valid)."
                }
            }
        };

        // checked 20190913 1.6 DIAG manual RVV
        public static DataSetDefinition DIA_203 => new DataSetDefinition
        {
            Name = "DIA_203 GLOBAL_ENVIRONMENT_DATA_4",
            Comment = "Dataset definition of part 4 of global environment data.",
            Identifiers = new List<string>
            {
                "230510340",
                "230511340"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "D_NVOVTRP",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Maximum distance for overriding the train trip. Allowed values according to [SS026], D_NVOVTRP."
                },
                new BitField
                {
                    Name = "D_NVPOTRP",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Maximum distance for reversing in Post Trip mode. Allowed values according to [SS026], D_NVPOTRP."
                },
                new BitField
                {
                    Name = "D_NVROLL",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Rollaway distance limit; Allowed values according to [SS026]. D_NVROLL considering Q_SCALE. " +
                                "Due to the fact that this parameter is stored in CM, but the [SS026] parameter depends on Q_SCALE, the special value infinite is here defined."
                },
                new BitField
                {
                    Name = "T_NVOVTRP",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Maximum time for overriding the train trip. Allowed values according to [SS026], T_NVOVTRP."
                },
                new BitField
                {
                    Name = "T_NVCONTACT",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Maximal time without new safe message. Allowed values according to [SS026]. " +
                                "T_NVCONTACT considering that this document specifies ms, but the [SS026] specifies s. " +
                                "Due to the different units the special value infinite is here defined as 25."
                },
                new BitField
                {
                    Name = "V_NVSTFF",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Staff Responsible mode (permitted) speed limit. Allowed values according to [SS026], V_NVSTFF."
                },
                new BitField
                {
                    Name = "V_NVONSIGHT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "On Sight mode (permitted) speed limit. Allowed values according to [SS026], V_NVONSIGHT."
                },
                new BitField
                {
                    Name = "V_NVUNFIT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Unfitted mode (permitted) speed limit. Allowed values according to [SS026], V_NVUNFIT."
                },
                new BitField
                {
                    Name = "V_NVREL",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Release Speed (permitted) speed limit. Allowed values according to [SS026], V_NVREL."
                },
                new BitField
                {
                    Name = "V_NVALLOWOVTRP",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Maximum speed limit allowing the driver to select the override EOA function"
                },
                new BitField
                {
                    Name = "V_NVSUPOVTRP",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Permitted speed limit to be supervised when the override EOA function is active"
                }
            }
        };

        // checked 20190913 1.6 DIAG manual RVV
        public static DataSetDefinition DIA_204 => new DataSetDefinition
        {
            Name = "DIA_204 GLOBAL_ENVIRONMENT_DATA_5",
            Comment = "Dataset definition of part 5 of global environment data.",
            Identifiers = new List<string>
            {
                "230510350",
                "230511350"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "STANDSTILL_POS",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Stand still position."
                },
                new BitField
                {
                    Name = "LAST_BG_POS",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last Balise group position (indicates which of stored 8 Balises the last was)."
                },
                new BitField
                {
                    Name = "D_NVSTFF",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Maximum distance for running in Staff Responsible mode. Allowed values according to [SS026]. " +
                    "D_NVSTFF considering Q_SCALE. Due to the fact that this parameter is stored in CM, but the [SS026] parameter depends on Q_SCALE, the special"
                },
                new BitField
                {
                    Name = "NID_C",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "NID_C."
                },
                new BitField
                {
                    Name = "NID_C_MAX_ITER",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Number of iterations of NID_C."
                },
                new BitField
                {
                    Name = "VALID_NV",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Valid National Values. If this parameter is set to 0, the ETCS-Core application during start-up will set the National Values " +
                    "to the default values according to requirements and values of the configuration parameters."
                },
                new BitField
                {
                    Name = "M_NVCONTACT",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Indicates the reaction to be performed when T_NVCONTACT timer elapses. Allowed values according to [SS026]."
                },
                new BitField
                {
                    Name = "M_NVDERUN",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Entry of Driver ID permitted while running. Allowed values according to [SS026]."
                },
                new BitField
                {
                    Name = "Q_NVEMRRLS",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Qualifier Emergency Brake Release. Allowed values according to [SS026]."
                },
                new BitField
                {
                    Name = "Q_NVSRBKTRG",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Permission to use service brake when braking to a target is supervised. Allowed values according to [SS026]."
                },
                new BitField
                {
                    Name = "Q_NVDRIVER_ADHES",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Qualifier for the modification of trackside adhesion factor by driver. Allowed values according to [SS026]."
                },
                new BitField
                {
                    Name = "Q_DLRBG",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Qualifier telling on which side of the LRBG the estimated front end is. Allowed values according to [SS026]."
                },
                new BitField
                {
                    Name = "QDIR_LRBG_1",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train orientation in relation to the direction of the LRBG."
                },
                new BitField
                {
                    Name = "QDIR_LRBG_2",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train orientation in relation to the direction of the LRBG."
                },
                new BitField
                {
                    Name = "QDIR_LRBG_3",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train orientation in relation to the direction of the LRBG."
                },
                new BitField
                {
                    Name = "QDIR_LRBG_4",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train orientation in relation to the direction of the LRBG."
                },
                new BitField
                {
                    Name = "QDIR_LRBG_5",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train orientation in relation to the direction of the LRBG."
                },
                new BitField
                {
                    Name = "QDIR_LRBG_6",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train orientation in relation to the direction of the LRBG."
                },
                new BitField
                {
                    Name = "QDIR_LRBG_7",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train orientation in relation to the direction of the LRBG."
                },
                new BitField
                {
                    Name = "QDIR_LRBG_8",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train orientation in relation to the direction of the LRBG."
                },
                new BitField
                {
                    Name = "GLO5_0F0",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "--not used--"
                }
            }
        };

        // checked 20190916 1.6 DIAG manual RVV
        public static DataSetDefinition DIA_205 => new DataSetDefinition
        {
            Name = "DIA_205 GLOBAL_ENVIRONMENT_DATA_6",
            Comment = "Dataset definition of part 6 of global environment data.",
            Identifiers = new List<string>
            {
                "230510360",
                "230511360"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "NID_LRBG_1",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Country/region identity + Balise identity of last relevant Balise group (NID_C + NID_BG)."
                },
                new BitField
                {
                    Name = "NID_LRBG_2",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Country/region identity + Balise identity of last relevant Balise group (NID_C + NID_BG)."
                },
                new BitField
                {
                    Name = "NID_LRBG_3",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Country/region identity + Balise identity of last relevant Balise group (NID_C + NID_BG)."
                },
                new BitField
                {
                    Name = "NID_LRBG_4",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Country/region identity + Balise identity of last relevant Balise group (NID_C + NID_BG)."
                },
                new BitField
                {
                    Name = "NID_LRBG_5",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Country/region identity + Balise identity of last relevant Balise group (NID_C + NID_BG)."
                },
                new BitField
                {
                    Name = "NID_LRBG_6",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Country/region identity + Balise identity of last relevant Balise group (NID_C + NID_BG)."
                },
                new BitField
                {
                    Name = "NID_LRBG_7",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Country/region identity + Balise identity of last relevant Balise group (NID_C + NID_BG)."
                },
                new BitField
                {
                    Name = "NID_LRBG_8",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Country/region identity + Balise identity of last relevant Balise group (NID_C + NID_BG)."
                }
            }
        };

        // checked 20190916 1.6 DIAG manual RVV
        public static DataSetDefinition DIA_206 => new DataSetDefinition
        {
            Name = "DIA_206 GLOBAL_ENVIRONMENT_DATA_7",
            Comment = "Dataset definition of part 7 of global environment data.",
            Identifiers = new List<string>
            {
                "230510370",
                "230511370"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "LRBG_POS_1",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last relevant Balise group position."
                },
                new BitField
                {
                    Name = "LRBG_POS_2",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last relevant Balise group position."
                },
                new BitField
                {
                    Name = "LRBG_POS_3",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last relevant Balise group position."
                },
                new BitField
                {
                    Name = "LRBG_POS_4",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last relevant Balise group position."
                },
                new BitField
                {
                    Name = "LRBG_POS_5",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last relevant Balise group position."
                },
                new BitField
                {
                    Name = "LRBG_POS_6",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last relevant Balise group position."
                },
                new BitField
                {
                    Name = "LRBG_POS_7",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last relevant Balise group position."
                },
                new BitField
                {
                    Name = "LRBG_POS_8",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last relevant Balise group position."
                }
            }
        };

        // checked 20190916 1.6 DIAG manual RVV
        public static DataSetDefinition DIA_207 => new DataSetDefinition
        {
            Name = "DIA_207 GLOBAL_ENVIRONMENT_DATA_8",
            Comment = "Dataset definition of part 8 of global environment data.",
            Identifiers = new List<string>
            {
                "230510380",
                "230511380"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "MIN_POSITION",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Min position"
                },
                new BitField
                {
                    Name = "MAX_POSITION",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Max position"
                },
                new BitField
                {
                    Name = "NID_NETWORK",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Identity of Radio Network. Allowed values according to [SS026], NID_MN."
                },
                new BitField
                {
                    Name = "STM_ID",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Last used STM id (NID_STM). Allowed values depends on the installed STM levels. " +
                    "Not allowed are not installed NID-STMs. Value 255 means no STM used."
                },
                new BitField
                {
                    Name = "TemperatureCPU1",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Current temperature CPU1"
                },
                new BitField
                {
                    Name = "TemperatureCPU2",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Current temperature CPU2"
                },
                new BitField
                {
                    Name = "ActiveModem",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Active modem"
                },
                new BitField
                {
                    Name = "NID_ENGINE",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "On-board ETCS identity according [ERA_VAR]. Contains information on rolling stock fleet/on-board equipment supplier. " +
                    "Reserved ranges, numbers: 0-1023  'Range for Bombardier RCS', 17051 'Range for Bombardier.' " +
                    "The project specific values will be provided by train Operator or by Product Owner within the Bombardier Transportation." +
                    "Note: According SUBSET-26-7 this is a 24 Bit variable. Due to IEC 61375 data type compatibility, this variable is internally pictured on a 32-Bit value."
                },
                new BitField
                {
                    Name = "DiagValid",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Validity of all diagnostic packets (used by vehicle diagnostics). " +
                    "0 = diagnostic data not yet valid (during initialization) " +
                    "1 = all diagnostics data are valid"
                },
                new BitField
                {
                    Name = "GLO8_0A1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "GLO8_0A2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "GLO8_0A3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "GLO8_0A4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "GLO8_0A5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "GLO8_0A6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "GLO8_0A7",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "GLO8_0A8",
                    BitFieldType = BitFieldType.Spare,
                    Length = 88,
                    Comment = "--not used--"
                }
            }
        };

        // checked 20190916 1.6 DIAG manual RVV
        public static DataSetDefinition DIA_209 => new DataSetDefinition
        {
            Name = "DIA_209 LRU_DIAG_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            Identifiers = new List<string>
            {
                "230510400",
                "230511400"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "LRU_DeviceID",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Device ID of the subsystem"
                },
                new BitField
                {
                    Name = "LRU_SubDeviceID",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "If a sub device ID is available, i.e. in case of sub structured Devices"
                },
                new BitField
                {
                    Name = "LRU_DeviceID_Env_Data_1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "All data according to subsystem/sub product definition"
                },
                new BitField
                {
                    Name = "LRU_DeviceID_Env_Data_n",
                    BitFieldType = BitFieldType.Spare,
                    Length = 224,
                    Comment = "All data according to subsystem/sub product definition"
                }
            }
        };

        // checked 20190602 1.6 DIAG manual JS
        public static DataSetDefinition DIA_212 => new DataSetDefinition
        {
            Name = "DIA_212 VERSION_DATA",
            Comment = "Sent when all versions from the subsystems are sent to the DIA subsystem.",
            Identifiers = new List<string>
            {
                "230510430", "230511430"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "VersionNumberIndex",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Current version index (0...Number_of_Versions-1)"
                },
                new BitField
                {
                    Name = "V_DeviceID",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "DeviceID corresponding to a subsystem or function/application"
                },
                new BitField
                {
                    Name = "V_SubDeviceID",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "If a sub device ID is available, i.e. in case of sub structured Devices"
                },
                new BitField
                {
                    Name = "Number_of_Versions",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "The max. number of versions from a subsystem"
                },
                new BitField
                {
                    Name = "VersionName",
                    BitFieldType = BitFieldType.StringAscii,
                    Length = 80,
                    Comment = "Name of the Version Number, i.e. ”CommonODO”, 10 bytes maximum"
                },
                new BitField
                {
                    Name = "VersionNumber",
                    BitFieldType = BitFieldType.HexString,
                    Length = 128,
                    Comment =
                        "Version number i.e. “1.0.22.10”, 16 bytes maximum. The version format is specified by the subsystem."
                },
                new BitField
                {
                    Name = "Number_of_Versions",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "not used"
                }
            }
        };

        // checked 20190920 1.6 DIAG manual RVV
        public static DataSetDefinition DIA_214 => new DataSetDefinition
        {
            Name = "DIA_214 DIAG_WAYSIDE_EVENT",
            Comment = "The Wayside diagnostic packet (including the environment data set DIA_215) contains data " +
            "delivered from the Balise transmission sub-system collected within the ETC software.",
            Identifiers = new List<string>
            {
                "230510440", "230511440"
            },

            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "WAY_BTS_OK (IE-)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Balise OK (no BTS error detected)"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_VERSION (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Version error"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_HEADER (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Header error or Balise payload error"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_BAL_MISSING (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Balise missing in a group"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_BAL_MISSING_POS (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Max. distance of expected Balise window passed."
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_BAL_DETECT (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Balise detection error"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_BAL_UNEXPECTED (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Unexpected Balise"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_BAL_REPOS_OR_UNKNW (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Balise reposition error or Balise unknown"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_PACKET_SIZE (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Wrong packet size"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_PACKET_DUPLICATE (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Duplicate packet received"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_PACKET_UNEXPECTED (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Unexpected packet received"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_QDIR (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "QDIR error"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_INFILL (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Infill Balise error"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_BG_STATUS (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Reports the ETC IL handler internal BG-status"
                },
                new BitField
                {
                    Name = "WAY_ERR_LOOP (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Loop error detected"
                },
                new BitField
                {
                    Name = "WAY_ERR_LEU (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "LEU error detected"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_BAL_GROUP_MISSING (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Complete Balise Group missing"
                },
                new BitField
                {
                    Name = "WAY_unused",
                    BitFieldType = BitFieldType.Spare,
                    Length = 15,
                    Comment = "--not used--"
                },
            }
        };
    }
}