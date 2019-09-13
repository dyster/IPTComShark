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
            DataSets.Add(DIA_212);
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
    }
}