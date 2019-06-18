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
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "DeviceID corresponding to a subsystem or function/application"
                },
                new BitField
                {
                    Name = "V_SubDeviceID",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "If a sub device ID is available, i.e. in case of sub structured Devices"
                },
                new BitField
                {
                    Name = "Number_of_Versions",
                    BitFieldType = BitFieldType.UInt16,
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
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "not used"
                }
            }
        };
    }
}