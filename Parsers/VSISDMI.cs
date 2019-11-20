using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sonesson_tools.BitStreamParser;
using sonesson_tools.DataSets;

// ReSharper disable InconsistentNaming
#pragma warning disable 1591

namespace IPTComShark.Parsers
{
    public class VSISDMI : DataSetCollection
    {
        public VSISDMI()
        {
            this.Name = "VSIS 2.11";
            this.Description = "EVC Telegrams based on the VSIS v2.11";

            DataSets.Add(EVC_0);
            DataSets.Add(EVC_1);
            DataSets.Add(EVC_2);
            DataSets.Add(EVC_3);
            DataSets.Add(EVC_4);
            DataSets.Add(EVC_5);
            DataSets.Add(EVC_6);
            DataSets.Add(EVC_7);
            DataSets.Add(EVC_8);
            DataSets.Add(EVC_10);
            DataSets.Add(EVC_11);
            DataSets.Add(EVC_13);
            DataSets.Add(EVC_14);
            DataSets.Add(EVC_16);
            DataSets.Add(EVC_18);
            DataSets.Add(EVC_19);
            DataSets.Add(EVC_20);
            DataSets.Add(EVC_22);
            DataSets.Add(EVC_23);
            DataSets.Add(EVC_24);
            DataSets.Add(EVC_25);
            DataSets.Add(EVC_26);
            DataSets.Add(EVC_27);
            DataSets.Add(EVC_28);
            DataSets.Add(EVC_29);
            DataSets.Add(EVC_30);
            DataSets.Add(EVC_31);
            DataSets.Add(EVC_32);
            DataSets.Add(EVC_33);
            DataSets.Add(EVC_34);
            DataSets.Add(EVC_40);
            DataSets.Add(EVC_41);
            DataSets.Add(EVC_50);
            DataSets.Add(EVC_51);
        }
        //{230531000, EVC_100},
        //{230536000, EVC_100},
        //{230531010, EVC_101},
        //{230536010, EVC_101},
        //{230531020, EVC_102},
        //{230536020, EVC_102},
        ////{230531021, EVC_102},      {230536021, EVC_102}, specified redundant telegram, I don't think this is correct (in VSIS)
        //{230531030, EVC_104},
        //{230536030, EVC_104},
        //{230531040, EVC_106},
        //{230536040, EVC_106},
        //{230531050, EVC_107},
        //{230536050, EVC_107},
        //{230531060, EVC_109},
        //{230536060, EVC_109},
        //{230531070, EVC_110},
        //{230536070, EVC_110},
        //{230531080, EVC_111},
        //{230536080, EVC_111},
        //{230531090, EVC_112},
        //{230536090, EVC_112},
        //{230531100, EVC_116},
        //{230536100, EVC_116},
        //{230531110, EVC_118},
        //{230536110, EVC_118},
        //{230531120, EVC_119},
        //{230536120, EVC_119},
        //{230531130, EVC_121},
        //{230536130, EVC_121},
        //{230531140, EVC_122},
        //{230536140, EVC_122},
        //{230531150, EVC_123},
        //{230536150, EVC_123},
        //{230531160, EVC_128},
        //{230536160, EVC_128},
        //{230531170, EVC_129},
        //{230536170, EVC_129},
        //{230531180, EVC_140},
        //{230536180, EVC_140},
        //{230531190, EVC_141},
        //{230536190, EVC_141},
        //{230531200, EVC_150},
        //{230536200, EVC_150},
        //{230531210, EVC_151},
        //{230536210, EVC_151},
        //{230531220, EVC_152},
        //{230536220, EVC_152},

        #region EVC->DMI (EVC-0 to EVC-99)

        // checked RVV 26-10-2019 2.11
        public static DataSetDefinition EVC_0 => new DataSetDefinition
        {
            Name = "EVC_0 MMI_START_ATP",
            Comment =
                "This packet shall be sent when the ETC needs to establish contact and exchange start-up information with the MMI.",
            Identifiers = new List<string>
            {
                "230530000",
                "230535000"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Name = "MMI_M_START_REQ",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Type of request",
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Version info request"},
                        {"1", "Go to Idle state"},
                        {"2", "Error: MMI type not supported"},
                        {"3", "Error: Incompatible IF versions"},
                        {"4", "Error: Incompatible SW versions"},
                        {"5", "Spare"},
                        {"6", "Spare"},
                        {"7", "Spare"},
                        {"8", "Spare"},
                        {"9", "Spare"},
                        {"10", "DMI reboot. Indication error"}
                    }
                }
            }
        };

        // checked RVV 24-10-2019 2.11
        public static DataSetDefinition EVC_1 => new DataSetDefinition
        {
            Name = "EVC_1 MMI_DYNAMIC",
            Comment =
                "This packet contains dynamic information, such as current train speed, current position and current target data for the driver." +
                "\r\nNote: This packet is routed via dedicated port and thus no header nor length information is contained in the (plain) data set. " +
                "It is also protected via SDTv2.\r\nSome variables that were formerly contained in EVC-1 are now part of EVC-7, which has to be evaluated by the DMI as well.",
            Identifiers = new List<string>
            {
                "230530010",
                "230535010",
                "230530011",
                "230535011"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "MMI_M_SLIP",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Slip status",
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "No axle is slipping"},
                        {"1", "Any axle is slipping"}
                    }
                },
                new BitField
                {
                    Name = "MMI_M_SLIDE",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Slide status",
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "No axle is sliding"},
                        {"1", "Any axle is sliding"}
                    }
                },
                new BitField
                {
                    Name = "MMI_Q_FULLWINDOW",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Allow display of full window",
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Full Screen windows disabled"},
                        {"1", "Full Screen windows enabled"}
                    }
                },
                new BitField
                {
                    Name = "evc1_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "MMI_M_WARNING",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                    Comment = "Warning status",
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Normal Status, Ceiling Speed Monitoring"},
                        {"1", "Indication Status, Target Speed Monitoring"},
                        {"2", "Normal Status, Pre-Indication Monitoring"},
                        {"3", "Indication Status, Release Speed Monitoring"},
                        {"4", "Warning Status, Ceiling Speed Monitoring"},
                        {"5", "Warning Status and Indication Status, Target Speed Monitoring"},
                        {"6", "Warning Status, Pre-Indication Monitoring"},
                        {"7", "Spare"},
                        {"8", "Overspeed Status, Ceiling Speed Monitoring"},
                        {"9", "Overspeed Status and Indication Status, Target Speed Monitoring"},
                        {"10", "Overspeed Status, Pre-Indication Monitoring"},
                        {"11", "Normal Status, Target Speed Monitoring"},
                        {"12", "Intervention Status, Ceiling Speed Monitoring"},
                        {"13", "Intervention Status and Indication Status, Target Speed Monitoring"},
                        {"14", "Intervention Status, Pre-Indication Monitoring"},
                        {"15", "Intervention Status, Release Speed Monitoring"}
                    }
                },
                new BitField
                {
                    Name = "evc1_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "MMI_V_TRAIN",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Current speed of the train",
                    LookupTable = new Dictionary<string, string> {{"-1", "Speed Unknown"}},
                    Scaling = 0.036,
                    AppendString = " km/h"
                },
                new BitField
                {
                    Name = "MMI_A_TRAIN",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Current acceleration of the train",
                    AppendString = " cm/s2"
                },
                new BitField
                {
                    Name = "MMI_V_TARGET",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Speed restriction at current target",
                    LookupTable = new Dictionary<string, string> {{"-1", "No target speed"}}
                },
                new BitField
                {
                    Name = "MMI_V_PERMITTED",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Current permitted speed"
                },
                new BitField
                {
                    Name = "MMI_V_RELEASE",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Release speed applied at the EOA",
                    LookupTable = new Dictionary<string, string> {{"-1", "No release speed"}}
                },
                new BitField
                {
                    Name = "MMI_O_BRAKETARGET",
                    BitFieldType = BitFieldType.Int32,
                    Length = 32,
                    Comment =
                        "This is the position in odometer co-ordinates of the next restrictive discontinuity of the static speed profile or target, which has influence on the braking curve. " +
                        "This position can be adjusted depending on supervision.",
                    LookupTable = new Dictionary<string, string>
                    {
                        {"-1", "No brake target"},
                        {"2147483647", "Infinite distance in Reversing mode"}
                    }
                },
                new BitField
                {
                    Name = "MMI_O_IML",
                    BitFieldType = BitFieldType.Int32,
                    Length = 32,
                    Comment =
                        "This is the location in odometer co-ordinates of the indication marker for the next brake target. This position can be adjusted depending on supervision.",
                    LookupTable = new Dictionary<string, string> {{"-1", "Spare"}}
                },
                new BitField
                {
                    Name = "MMI_V_INTERVENTION",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Current intervention speed"
                },
                new BitField
                {
                    Name = "ValiditySlip",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValiditySlide",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityFullWindow",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValiditySpare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityWarning",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValiditySpare4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 11
                },
                new BitField
                {
                    Name = "ValidityVTrain",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityATrain",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityVTarget",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityVPermitted",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityVRelease",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityOBrakeTarget",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityOIML",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityVIntervention",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValiditySpare5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // checked RVV 26-10-2019 2.11
        public static DataSetDefinition EVC_2 => new DataSetDefinition
        {
            Name = "EVC_2 MMI_STATUS",
            Comment =
                "This packet contains status information for the driver and shall be sent to the MMI when­ever any of the status has changed.",
            Identifiers = new List<string>
            {
                "230530020",
                "230535020"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                Subset26.NID_OPERATIONAL,
                new BitField
                {
                    Name = "MMI_M_ADHESION(Driver)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Adhesion status set by driver"
                },
                new BitField
                {
                    Name = "MMI_M_ADHESION(Trackside)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Adhesion status set from trackside"
                },
                MMI_M_ACTIVE_CABIN,
                new BitField
                {
                    Name = "MMI_M_OVERRIDE_EOA",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Override EOA is activated"
                },
                new BitField
                {
                    Name = "evc2_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "spare for alignment"
                },
                new BitField
                {
                    Name = "evc2_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "spare for alignment"
                }
            }
        };

        // checked RVV 26-10-2019 2.11
        public static DataSetDefinition EVC_3 => new DataSetDefinition
        {
            Name = "EVC_3 MMI_SET_TIME_ATP",
            Comment =
                "This packet shall be sent whenever the time is changed in the ETC clock function." +
                "\r\nNote that there is a corresponding message from MMI to ETC. The issue of the message depends on who of the units are selected as “clock master”. " +
                "This may vary from system to system, but in a specific system, only the clock master is allowed to initiate the message.",
            Identifiers = new List<string>
            {
                "230530030",
                "230535030"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_T_UTC,
                MMI_T_ZONE_OFFSET
            }
        };

        // checked RVV 24-10-2019 2.11
        public static DataSetDefinition EVC_4 => new DataSetDefinition
        {
            Name = "EVC_4 MMI_TRACK_DESCRIPTION",
            Comment =
                "This packet contains trackside information to the driver. " +
                "Whenever new information is received from trackside the speed profile and the gradient profile shall be sent to the MMI.",
            Identifiers = new List<string>
            {
                "230530040",
                "230535040"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Name = "MMI_V_MRSP_CURR",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Current speed value"
                },
                new BitField
                {
                    Name = "MMI_N_MRSP",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Number of speed information fields"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_MRSP"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "MMI_O_MRSP",
                                BitFieldType = BitFieldType.Int32,
                                Length = 32,
                                Comment =
                                    "This is the position in odometer co-ordinates of the start location of a speed discontinuity in the most restrictive speed profile. This position can be adjusted depending on supervision."
                            },
                            new BitField
                            {
                                Name = "MMI_V_MRSP",
                                BitFieldType = BitFieldType.Int16,
                                Length = 16,
                                Comment = "New speed value",
                                LookupTable = new Dictionary<string, string>
                                {
                                    {"0", "Display as speed v=0 and use discontinuity symbol PL23 according to [ERA]"},
                                    {"-3", "Display as speed v=0 without any discontinuity symbol"}
                                }
                            },
                            new BitField
                            {
                                Name = "evc4_spare1",
                                BitFieldType = BitFieldType.Spare,
                                Length = 16,
                                Comment = "spare for alignment"
                            }
                        }
                    }
                },
                new BitField
                {
                    Name = "MMI_G_GRADIENT_CURR",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Current gradient value",
                    LookupTable = new Dictionary<string, string> {{"-255", "No current gradient profile"}}
                },
                new BitField
                {
                    Name = "MMI_N_GRADIENT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Number of gradient information fields"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_GRADIENT"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "MMI_O_GRADIENT",
                                BitFieldType = BitFieldType.Int32,
                                Length = 32,
                                Comment =
                                    "This is the position, in odometer co-ordinates, without tolerance correction, of the start location of a gradient value for a part of the track. " +
                                    "The remaining distances shall be computed taking into account the estimated train front-end position."
                            },
                            new BitField
                            {
                                Name = "MMI_G_GRADIENT",
                                BitFieldType = BitFieldType.Int16,
                                Length = 16,
                                Comment = "New gradient value",
                                LookupTable = new Dictionary<string, string> {{"-255", "The gradient profile ends at the defined position"}}
                            },
                            new BitField
                            {
                                Name = "evc4_spare2",
                                BitFieldType = BitFieldType.Spare,
                                Length = 16,
                                Comment = "spare for alignment"
                            }
                        }
                    }
                }
            }
        };

        // checked RVV 24-10-2019 2.11
        public static DataSetDefinition EVC_5 => new DataSetDefinition
        {
            Name = "EVC_5 MMI_GEO_POSITION",
            Comment = "This packet contains the geographical position to be presented on request by the driver.",
            Identifiers = new List<string>
            {
                "230530050",
                "230535050"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Name = "MMI_M_ABSOLUTPOS",
                    BitFieldType = BitFieldType.Int32,
                    Length = 32,
                    Comment =
                        "This is the train’s current geographical position, in absolute co-ordinates as defined by trackside. " +
                        "In case of single balises it indicates the absolute position of the last passed balise",
                    LookupTable = new Dictionary<string, string> {{"-1", "No more geo position report after this"}}
                },
                new BitField
                {
                    Name = "MMI_M_RELATIVPOS",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Train’s current geographical position given as an offset from last passed balise",
                    LookupTable = new Dictionary<string, string>{{"-1", "N/A (i.e. MMI shall display _ABSOLUTPOS only)"}}
                }
            }
        };

        // checked RVV 26-10-2019 2.11
        public static DataSetDefinition EVC_6 => new DataSetDefinition
        {
            Name = "EVC_6 MMI_CURRENT_TRAIN_DATA",
            Comment =
                "This packet is sent sporadically by ETC and is intended to support the following use cases:" +
                "\r\n1.) Display Train Data when entering TDE window" +
                "\r\n2.) Display/change echo text after data checks have been performed by ETC; this as well includes control over the allowed driver actions in case some data check has failed" +
                "\r\nIt also gives the ETC the ability to control the status/type of the \"Yes\" button, if specified by functional requirements for ETC and DMI." +
                "\r\n\r\nNote: Parameter 'MMI_N_DATA_ELEMENTS' distinguishes between use case 1.) and 2.)",
            Identifiers = new List<string>
            {
                "230530060",
                "230535060"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_M_DATA_ENABLE,
                new BitField
                {
                    Name = "MMI_L_TRAIN",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Max. train length",
                    LookupTable = new Dictionary<string, string>{{"0", "'No default value' => TD entry field shall remain empty"}}
                },
                new BitField
                {
                    Name = "MMI_V_MAXTRAIN",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Max. train speed",
                    LookupTable = new Dictionary<string, string>{{"0", "'No default value' => TD entry field shall remain empty"}}
                },
                new BitField
                {
                    Name = "MMI_NID_KEY_TRAIN_CAT",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Train category (label, coded as MMI_NID_KEY) according to ERA_ERTMS_15560, ch. 11.3.9.9.3. Coded as ERA 'key number' according to NID_KEY. " +
                        "For Train Category the keys number 3 to 20 are applicable. \"No dedicated key\" may be used for \"entry data entry field\"."
                },
                new BitField
                {
                    Name = "MMI_M_BRAKE_PERC",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Brake percentage"
                },
                new BitField
                {
                    Name = "MMI_NID_KEY_AXLE_LOAD",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Axle load category (coded as MMI key according to NID_KEY) of the train. " +
                        "For Axle Load Category the keys number 21 to 33 are applicable. \"No dedicated key\" may be used for \"entry data entry field\"."
                },
                new BitField
                {
                    Name = "MMI_M_AIRTIGHT",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train equipped with airtight system",
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Not equipped"},
                        {"1", "Equipped"},
                        {"2", "'No default value' => TD entry field shall remain empty"}
                    }
                },
                new BitField
                {
                    Name = "MMI_NID_KEY_LOAD_GAUGE",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Loading gauge type of train (coded as MMI key according to NID_KEY) of the train. " +
                        "For Train Category the keys number 34 to 38 are applicable. \"No dedicated key\" may be used for \"entry data entry field\"."
                },
                MMI_M_BUTTONS,
                new BitField
                {
                    Name = "MMI_M_TRAINSET_ID",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                    Comment = "ID of preconfigured train data set",
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Train data entry method by train data set is not selected --> use 'flexible TDE'"},
                        {"15", "no Train data set specified"}
                    }
                },
                new BitField
                {
                    Name = "MMI_M_ALT_DEM",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    Comment = "Control variable for alternative train data entry method",
                    LookupTable = new Dictionary<string, string>
                    {
                        {
                            "0",
                            "No alternative train data entry method enabled (covers 'fixed train data entry' and 'flexible train data entry' according to ERA_ERTMS_15560, v3.4.0, ch. 11.3.9.6.a+b)"
                        },
                        {
                            "15",
                            "Flexible train data entry <-> train data entry for Train Sets (covers 'switchable train data entry' according to ERA_ERTMS_15560, v3.4.0, ch. 11.3.9.6.c)"
                        }
                    }
                },
                new BitField
                {
                    Name = "evc6_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "spare for alignment"
                },
                new BitField
                {
                    Name = "evc6_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "spare for alignment"
                },
                new BitField
                {
                    Name = "MMI_N_TRAINSETS",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Number of trainsets to be shown for fixed TDE (range: 1..9)"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_TRAINSETS"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "MMI_N_CAPTION_TRAINSET",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 16,
                                Comment = "Length of caption text of train data set (max. 12)"
                            },
                            new BitField
                            {
                                Name = "MMI_X_CAPTION_TRAINSET",
                                BitFieldType = BitFieldType.StringLatin,
                                Comment = "Train data set caption text",
                                VariableLengthSettings = new VariableLengthSettings
                                {
                                    Name = "MMI_N_CAPTION_TRAINSET",
                                    ScalingFactor = 8
                                }
                            }
                        }
                    }
                },
                MMI_N_DATA_ELEMENTS,
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_DATA_ELEMENTS"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            MMI_NID_DATA,
                            MMI_Q_DATA_CHECK,
                            MMI_N_TEXT,
                            MMI_X_TEXT
                        }
                    }
                }
            }
        };

        // checked RVV 26-10-2019 2.11
        public static DataSetDefinition EVC_7 => new DataSetDefinition
        {
            Name = "EVC_7 MMI_ETCS_MISC_OUT_SIGNALS",
            Comment =
                "The signals in this telegram are outputs from the generic ETCS OB R4 system. This telegram collects miscellaneous output signals to the train." +
                "\r\nNote: This packet is routed via dedicated port and thus no header nor length information is contained in the (plain) data set. It is also protected via SDTv2.",
            Identifiers = new List<string>
            {
                "230530070",
                "230535070",
                "230530071",
                "230535071"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "evc7_spare0",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare"
                },
                new BitField
                {
                    Name = "MMI_OBU_TR_EBTestInProgress",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EB test in progress"
                },
                new BitField
                {
                    Name = "MMI_OBU_TR_EB_Status",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EB status"
                },
                new BitField
                {
                    Name = "MMI_OBU_TR_RadioStatus",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Radio status information"
                },
                new BitField
                {
                    Name = "MMI_OBU_TR_STM_HS_ENABLED",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "STM in HS state exists"
                },
                new BitField
                {
                    Name = "MMI_OBU_TR_STM_DA_ENABLED",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "STM in DA state exists"
                },
                new BitField
                {
                    Name = "evc7_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "Spare"
                },
                new BitField
                {
                    Name = "MMI_OBU_TR_BrakeTest_Status",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                    Comment = "ETCS brake test status"
                },
                new BitField
                {
                    Name = "MMI_OBU_TR_M_Level",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                    Comment = "ETCS level",
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Level 0"},
                        {"1", "Level NTC"},
                        {"2", "Level 1"},
                        {"3", "Level 2"},
                        {"4", "Level 3"},
                        {"15", "unknown"}
                    }
                },
                OBU_TR_M_Mode,
                new BitField
                {
                    Name = "MMI_OBU_TR_M_ADHESION",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Current applied adhesion coefficient"
                },
                new BitField
                {
                    Name = "MMI_OBU_TR_NID_STM_HS",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "NID_STM in HS state",
                    LookupTable = new Dictionary<string, string>
                    {
                        {"20", "TPWS"},
                        {"50", "CBTC"},
                        {"255", "none"}
                    }
                },
                new BitField
                {
                    Name = "MMI_OBU_TR_NID_STM_DA",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "NID_STM in DA state",
                    LookupTable = new Dictionary<string, string>
                    {
                        {"20", "TPWS"},
                        {"50", "CBTC"},
                        {"255", "none"}
                    }
                },
                new BitField
                {
                    Name = "MMI_OBU_TR_BrakeTestTimeOut",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Brake test timeout value"
                },
                OBU_TR_O_TRAIN,
                new BitField
                {
                    Name = "MMI_T_DMILM",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Time Stamp (ETC running time in milliseconds)"
                },
                new BitField
                {
                    Name = "evc7_Spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare"
                },
                new BitField
                {
                    Name = "evc7_Spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare"
                },
                new BitField
                {
                    Name = "EVC7_Validity1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity1 bits\r\n0 = not used (set to invalid)\r\n1 = MMI_OBU_TR_EBTestInProgress\r\n2 = MMI_OBU_TR_EB_Status\r\n3 = MMI_OBU_TR_RadioStatus" +
                        "\r\n4 = MMI_OBU_TR_STM_HS_ENABLED\r\n5 = MMI_OBU_TR_STM_DA_ENABLED\r\n6..7 not used (set to invalid) \r\n8 = MMI_OBU_TR_BrakeTest_Status" +
                        "\r\n9..11 not used (set to invalid) \r\n12 = MMI_OBU_TR_M_Level\r\n13..15 not used (set to invalid) "
                },
                new BitField
                {
                    Name = "EVC7_Validity2",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity2 bits\r\n0 = MMI_OBU_TR_M_Mode\r\n1 = MMI_OBU_TR_M_ADHESION\r\n2 = MMI_OBU_TR_NID_STM_HS\r\n3 = MMI_OBU_TR_NID_STM_DA" +
                        "\r\n4 = MMI_OBU_TR_BrakeTestTimeOut\r\n5 = MMI_OBU_TR_O_TRAIN\r\n6..15 not used (set to invalid) "
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // checked RVV 26-10-2019 2.11
        public static DataSetDefinition EVC_8 => new DataSetDefinition
        {
            Name = "EVC_8 MMI_DRIVER_MESSAGE",
            Comment =
                "This packet shall be sent when a message originating from the ETC or from wayside shall be presented to the driver. " +
                "MMI_Q_TEXT_CRITERIA indicates, how the MMI shall manage the predefined message. Some values of MMI_Q_TEXT shall result in the MMI activating a symbol according to [ERA] and not a text. " +
                "Except for the presentation, the basic principle is however the same." +
                "\r\nMMI_Q_TEXT also contains special values e.g. for deletion of message groups." +
                "\r\nRefer to the description of the Q_TEXT variable regarding the use of the free text carried by X_TEXT.",
            Identifiers = new List<string>
            {
                "230530080",
                "230535080"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Name = "MMI_Q_TEXT_CLASS",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Text class",
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Auxiliary Information"},
                        {"1", "Important Information"}
                    }
                },
                new BitField
                {
                    Name = "evc8_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare"
                },
                new BitField
                {
                    Name = "evc8_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "Spare"
                },
                new BitField
                {
                    Name = "MMI_Q_TEXT_CRITERIA",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                    Comment = "Tells MMI what to do with text",
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Add text/symbol with ack prompt, to be kept after ack"},
                        {"1", "Add text/symbol with ack prompt, to be removed after ack"},
                        {"2", "Add text with ack/nak prompt, to be removed after ack/nak"},
                        {"3", "Add informative text/symbol"},
                        {"4", "Remove text/symbol. Text/symbol to be removed is defined by MMI_I_TEXT."},
                        {"5", "Text still incomplete. Another instance of EVC-8 follows."}
                    }
                },
                new BitField
                {
                    Name = "MMI_I_TEXT",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Text identifier"
                },
                MMI_Q_TEXT,
                MMI_N_TEXT,
                MMI_X_TEXT
            }
        };

        // checked RVV 01-11-2019 2.11
        public static DataSetDefinition EVC_10 => new DataSetDefinition
        {
            Name = "EVC_10 MMI_ECHOED_TRAIN_DATA",
            Comment =
                "This packet will be sent from ETC to MMI when the driver has finished the data entry by pressing the \"Yes\" button and all checks have passed. " +
                "The packet starts the Train Data Validation window/procedure at MMI.",
            Identifiers = new List<string>
            {
                "230530090",
                "230535090"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Length = 1,
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            MMI_M_PACKET,
                            MMI_L_PACKET
                        }
                    }
                },
                new BitField
                {
                    Length = 1,
                    NestedDataSet = new DataSetDefinition
                    {
                        InvertBits = true,
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "MMI_N_TRAINSETS_",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 16,
                                Comment = "Number of trainsets to be shown for fixed TDE"
                            },
                            new BitField
                            {
                                VariableLengthSettings = new VariableLengthSettings {Name = "MMI_N_TRAINSETS_"},
                                NestedDataSet = new DataSetDefinition
                                {
                                    BitFields = new List<BitField>
                                    {
                                        new BitField
                                        {
                                            Name = "MMI_N_CAPTION_TRAINSET_",
                                            BitFieldType = BitFieldType.UInt16,
                                            Length = 16,
                                            Comment = "Length of caption text of train data set (max. 12)"
                                        },
                                        new BitField
                                        {
                                            Name = "MMI_X_CAPTION_TRAINSET_",
                                            BitFieldType = BitFieldType.StringLatin,
                                            VariableLengthSettings = new VariableLengthSettings {Name = "MMI_N_CAPTION_TRAINSET_", ScalingFactor = 8},
                                            Comment = "Train data set caption text"
                                        }
                                    }
                                }
                            },
                            new BitField
                            {
                                Name = "MMI_M_TRAINSET_ID_",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 4,
                                Comment = "ID of preconfigured train data set"
                            },
                            new BitField
                            {
                                Name = "MMI_M_ALT_DEM_",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 2,
                                Comment = "Control variable for alternative train data entry method"
                            },
                            new BitField
                            {
                                Name = "evc10_spare",
                                BitFieldType = BitFieldType.Spare,
                                Length = 2,
                                Comment = "Spare for alignment"
                            },
                            new BitField
                            {
                                Name = "MMI_NID_KEY_LOAD_GAUGE_",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment =
                                    "Loading gauge type of train (coded as MMI key according to NID_KEY) of the train. For Train Category the key numbers 34 to 38 are applicable. " +
                                    "\"No dedicated key\" may be used for \"entry data entry field\"."
                            },
                            new BitField
                            {
                                Name = "MMI_M_AIRTIGHT_",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Train equipped with airtight system"
                            },
                            new BitField
                            {
                                Name = "MMI_NID_KEY_AXLE_LOAD_",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment =
                                    "Axle load category (coded as MMI key according to NID_KEY) of the train. For Axle Load Category the key numbers 21 to 33 are applicable. " +
                                    "\"No dedicated key\" may be used for \"entry data entry field\"."
                            },
                            new BitField
                            {
                                Name = "MMI_V_MAXTRAIN_",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 16,
                                Comment = "Max train speed"
                            },
                            new BitField
                            {
                                Name = "MMI_L_TRAIN_",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 16,
                                Comment = "Max train length"
                            },
                            new BitField
                            {
                                Name = "MMI_M_BRAKE_PERC_",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Brake percentage"
                            },
                            new BitField
                            {
                                Name = "MMI_NID_KEY_TRAIN_CAT_",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment =
                                    "Train category (label) according to ERA_ERTMS_15560, ch. 11.3.9.9.3. Coded as ERA 'key number' according to NID_KEY. " +
                                    "For Train Category the keys number 3 to 20 are applicable. \"No dedicated key\" may be used for \"entry data entry field\"."
                            },
                            MMI_M_DATA_ENABLE
                        }
                    }
                }
            }
        };

        // checked RVV 28-10-2019 2.11
        public static DataSetDefinition EVC_11 => new DataSetDefinition
        {
            Name = "EVC_11 MMI_CURRENT_SR_RULES",
            Comment =
                "This packet is sent sporadically by ETC and is intended to support the following use cases:" +
                "\r\n1.) Display current SR speed / distance data when entering 'SR speed / distance' window" +
                "\r\n2.) Display/change echo text after data checks have been performed by ETC; this as well includes control over the allowed driver actions in case some data check has failed" +
                "\r\n\r\nIt also gives the ETC the ability to control the status/type of the \"Yes\" button, if specified by functional requirements for ETC and DMI." +
                "\r\n\r\nNote: Parameter 'MMI_NID_DATA_ELEMENTS' distinguishes between use case 1.) and 2.)",
            Identifiers = new List<string>
            {
                "230530100",
                "230535100"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Name = "MMI_L_STFF",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Max distance in Staff responsible"
                },
                new BitField
                {
                    Name = "MMI_V_STFF",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Max Staff Responsible speed"
                },
                MMI_N_DATA_ELEMENTS,
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_DATA_ELEMENTS"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            MMI_NID_DATA,
                            MMI_Q_DATA_CHECK,
                            MMI_N_TEXT,
                            MMI_X_TEXT
                        }
                    }
                },
                MMI_M_BUTTONS
            }
        };

        // checked RVV 12-11-2019 2.11
        public static DataSetDefinition EVC_13 => new DataSetDefinition
        {
            Name = "EVC_13 MMI_DATA_VIEW",
            Comment = "This packet shall be sent when the driver has requested to open the Data View window.",
            Identifiers = new List<string>
            {
                "230530110",
                "230535110"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_X_DRIVER_ID,
                Subset26.NID_OPERATIONAL,
                MMI_M_DATA_ENABLE,
                new BitField
                {
                    Name = "MMI_L_TRAIN",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Max. train length"
                },
                new BitField
                {
                    Name = "MMI_V_MAXTRAIN",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Max. train speed"
                },
                new BitField
                {
                    Name = "MMI_M_BRAKE_PERC",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Brake percentage"
                },
                new BitField
                {
                    Name = "MMI_NID_KEY_AXLE_LOAD",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Axle load category (coded as MMI key according to NID_KEY) of the train. For Axle Load Category the keys number 21 to 33 are applicable. " +
                        "\"No dedicated key\" may be used for \"entry data field\"."
                },
                MMI_NID_RADIO,
                MMI_NID_RBC,
                new BitField
                {
                    Name = "MMI_M_AIRTIGHT",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train equipped with airtight system"
                },
                new BitField
                {
                    Name = "MMI_NID_KEY_LOAD_GAUGE",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Loading gauge type of train (coded as MMI key according to NID_KEY) of the train. For Train Category the keys number 34 to 38 are applicable. " +
                        "\"No dedicated key\" may be used for \"entry data entry field\"."
                },
                new BitField
                {
                    Name = "MMI_N_VBC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Number of VBC elements. Value Range: 0..30 (according to SS040 max.30 is defined by 10 from trackside + 20 from driver)"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_VBC"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "MMI_M_VBC_CODE",
                                BitFieldType = BitFieldType.UInt32,
                                Length = 32,
                                Comment = "VBC Identifier"
                            }
                        }
                    }
                },
                new BitField
                {
                    Name = "MMI_N_CAPTION_TRAINSET",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Length of caption text of train data set (max. 12)"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_CAPTION_TRAINSET"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "MMI_X_CAPTION_TRAINSET",
                                BitFieldType = BitFieldType.StringLatin,
                                Length = 8,
                                Comment = "Train data set caption text"
                            },
                            new BitField
                            {
                                Name = "evc13_spare",
                                BitFieldType = BitFieldType.Spare,
                                Length = 8,
                                Comment = "spare for alignment"
                            }
                        }
                    }
                },
                MMI_N_CAPTION_NETWORK,
                MMI_X_CAPTION_NETWORK,
                new BitField
                {
                    Name = "MMI_NID_KEY_TRAIN_CAT",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Train category (label, coded as MMI_NID_KEY) according to ERA_ERTMS_15560, ch. 11.3.9.9.3. Coded as ERA 'key number' according to NID_KEY. " +
                        "For Train Category the keys number 3 to 20 are applicable. \"No dedicated key\" may be used for \"entry data view field\"."
                }
            }
        };

        // checked RVV 12-11-2019 2.11
        public static DataSetDefinition EVC_14 => new DataSetDefinition
        {
            Name = "EVC_14 MMI_CURRENT_DRIVER_ID",
            Comment =
                "This packet shall be sent when the driver is intended to enter/validate /view driver identity number.",
            Identifiers = new List<string>
            {
                "230530120",
                "230535120"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_Q_CLOSE_ENABLE,
                new BitField
                {
                    Name = "Enable TRN Button",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Enable Settings Button",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "Spares of MMI_Q_ADD_ENABLE bitmask",
                    BitFieldType = BitFieldType.Spare,
                    Length = 6
                },
                MMI_X_DRIVER_ID
            }
        };

        // checked RVV 12-11-2019 2.11
        public static DataSetDefinition EVC_16 => new DataSetDefinition
        {
            Name = "EVC_16 MMI_CURRENT_TRAIN_NUMBER",
            Comment =
                "This packet shall be sent when the driver is intended to enter/validate/view train running number",
            Identifiers = new List<string>
            {
                "230530130",
                "230535130"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                Subset26.NID_OPERATIONAL
            }
        };

        // checked 20190508 2.11
        public static DataSetDefinition EVC_18 => new DataSetDefinition
        {
            Name = "EVC_18 MMI_SET_VBC",
            Comment =
                "This packet is sent sporadically from ETC when the 'Set VBC' procedure is ongoing and is intended to support the following use cases:" +
                "\r\n1.) Prompt the driver to enter a VBC code" +
                "\r\n2.) Display/change echo text after data checks have been performed by ETC; this as well includes control over the allowed driver actions in case some data check has failed" +
                "\r\nIt also gives the ETC the ability to control the status/type of the \"Yes\" button, if specified by functional requirements for ETC and DMI." +
                "\r\n\r\nNote: Parameter 'MMI_N_VBC' distinguishes between use case 1.) and 2.)",
            Identifiers = new List<string>
            {
                "230530140",
                "230535140"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_M_BUTTONS,
                new BitField
                {
                    Name = "evc18_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare for alignment"
                },
                new BitField
                {
                    Name = "MMI_N_VBC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Number of VBC Identifiers (range: 0, 1)."
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_VBC"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "MMI_M_VBC_CODE",
                                BitFieldType = BitFieldType.UInt32,
                                Length = 32,
                                Comment = "VBC Identifier"
                            },
                            MMI_Q_DATA_CHECK,
                            new BitField
                            {
                                Name = "evc18_spare2",
                                BitFieldType = BitFieldType.Spare,
                                Length = 8,
                                Comment = "Spare for alignment"
                            },
                            MMI_N_TEXT,
                            MMI_X_TEXT
                        }
                    }
                }
            }
        };

        // checked 20190510 2.11
        public static DataSetDefinition EVC_19 => new DataSetDefinition
        {
            Name = "EVC_19 MMI_REMOVE_VBC",
            Comment =
                "This packet is sent sporadically from ETC when the 'Remove VBC' procedure is ongoing and is intended to support the following use cases:" +
                "\r\n1.) Prompt the driver to enter a VBC code" +
                "\r\n2.) Display/change echo text after data checks have been performed by ETC; this as well includes control over the allowed driver actions in case some data check has failed" +
                "\r\nIt also gives the ETC the ability to control the status/type of the \"Yes\" button, if specified by functional requirements for ETC and DMI." +
                "\r\n\r\nNote: Parameter 'MMI_N_VBC' distinguishes between use case 1.) and 2.)",
            Identifiers = new List<string>
            {
                "230530150",
                "230535150"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_M_BUTTONS,
                new BitField
                {
                    Name = "evc19_spare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare for Alignment"
                },
                new BitField
                {
                    Name = "MMI_N_VBC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Number of VBC elements (range: 0, 1)"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_VBC"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "MMI_M_VBC_CODE",
                                BitFieldType = BitFieldType.UInt32,
                                Length = 32,
                                Comment = "VBC Identifier"
                            },
                            MMI_N_TEXT,
                            MMI_X_TEXT,
                            MMI_Q_DATA_CHECK
                        }
                    }
                }
            }
        };

        // checked RVV 11-11-2019 2.11
        public static DataSetDefinition EVC_20 => new DataSetDefinition
        {
            Name = "EVC_20 MMI_SELECT_LEVEL",
            Comment =
                "This packet shall be sent when the ETC requests the driver to select level. The packet contains a list of ETCS and NTC levels and related additional status information. " +
                "Possible use cases are display of 'default level list', display of 'trackside supported level list', display of 'inhibit level list'.",
            Identifiers = new List<string>
            {
                "230530160",
                "230535160"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Name = "MMI_N_LEVELS",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Number of levels" +
                        "\r\nNote: The sequence of presentation to the driver of the ETCS levels is predefined by ERA_ERTMS_15560. " +
                        "Nevertheless, the ETC should adapt the order of levels in the list to the order desired by ERA_ERTMS_15560. " +
                        "The sequence of NTC level presentation can be chosen by generic system design. " +
                        "The DMI should use the sequence of NTC levels in MMI_M_LEVEL_NTC_ID (k=x) as input for sequence of display.",
                    LookupTable = new Dictionary<string, string> {{"0", "Cancel presentation of previous MMI_SELECT_LEVEL (if still shown on the MMI)."}}
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_LEVELS"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "MMI_Q_LEVEL_NTC_ID",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 1,
                                Comment = "Qualifier for the variable MMI_M_LEVEL_NTC_ID",
                                LookupTable = new Dictionary<string, string>
                                {
                                    {"0", "MMI_M_LEVEL_NTC_ID contains an STM ID (0-255)"},
                                    {"1", "MMI_M_LEVEL_NTC_ID contains a level number (0-3)"}
                                }
                            },
                            new BitField
                            {
                                Name = "MMI_M_CURRENT_LEVEL",
                                BitFieldType = BitFieldType.Bool,
                                Length = 1,
                                Comment = "Indicates if MMI_M_LEVEL_STM_ID is the latest used level"
                            },
                            new BitField
                            {
                                Name = "MMI_M_LEVEL_FLAG",
                                BitFieldType = BitFieldType.Bool,
                                Length = 1,
                                Comment = "Marker to indicate if a level button is enabled or disabled."
                            },
                            new BitField
                            {
                                Name = "MMI_M_INHIBITED_LEVEL",
                                BitFieldType = BitFieldType.Bool,
                                Length = 1,
                                Comment = "Indicates if MMI_M_LEVEL_NTC_ID is currently inhibited by driver or not"
                            },
                            new BitField
                            {
                                Name = "MMI_M_INHIBIT_ENABLE",
                                BitFieldType = BitFieldType.Bool,
                                Length = 1,
                                Comment = "Indicates if MMI_M_LEVEL_NTC_ID is allowed (configurable) for inhibiting or not"
                            },
                            new BitField
                            {
                                Name = "evc20_spare1",
                                BitFieldType = BitFieldType.Spare,
                                Length = 1,
                                Comment = "spare for alignment"
                            },
                            new BitField
                            {
                                Name = "evc20_spare2",
                                BitFieldType = BitFieldType.Spare,
                                Length = 2,
                                Comment = "ignored data (byte alignment)"
                            },
                            new BitField
                            {
                                Name = "MMI_M_LEVEL_NTC_ID",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Level number or NTC ID",
                                LookupTable = new Dictionary<string, string>
                                {
                                    {"0", "Level 0"},
                                    {"1", "Level 1"},
                                    {"2", "Level 2"},
                                    {"3", "Level 3"},
                                    {"20", "TPWS"},
                                    {"50", "CBTC"}
                                }
                            }
                        }
                    }
                },
                MMI_Q_CLOSE_ENABLE
            }
        };

        // checked RVV 11-11-2019 2.11
        public static DataSetDefinition EVC_22 => new DataSetDefinition
        {
            Name = "EVC_22 MMI_CURRENT_RBC_DATA",
            Comment =
                "This packet is sent sporadically by ETC and is intended to support the following use cases:" +
                "\r\n1.) Display 'RBC contact', 'RBC data' or 'Radio Network ID' when entering RBC data. Send 'RBC' data in \"data view\" procedure." +
                "\r\n2.) Display/change echo text after data checks have been performed by ETC; this as well includes control over the allowed driver actions in case some data check has failed" +
                "\r\nIt also gives the ETC the ability to control the status/type of the \"Yes\" button, if specified by functional requirements for ETC and DMI.",
            Identifiers = new List<string>
            {
                "230530170",
                "230535170"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_NID_RBC,
                MMI_NID_RADIO,
                MMI_NID_WINDOW,
                MMI_Q_CLOSE_ENABLE,
                MMI_M_BUTTONS,
                new BitField
                {
                    Name = "evc22_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare for alignment"
                },
                new BitField
                {
                    Name = "MMI_N_NETWORKS",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Number of predefined Network IDs (range: 0-9)"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_NETWORKS"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            MMI_N_CAPTION_NETWORK,
                            MMI_X_CAPTION_NETWORK
                        }
                    }
                },
                MMI_N_DATA_ELEMENTS,
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_DATA_ELEMENTS"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            MMI_NID_DATA,
                            MMI_Q_DATA_CHECK,
                            MMI_N_TEXT,
                            MMI_X_TEXT
                        }
                    }
                }
            }
        };

        // checked RVV 11-11-2019 2.11
        public static DataSetDefinition EVC_23 => new DataSetDefinition
        {
            Name = "EVC_23 MMI_LSSMA",
            Comment = "This packet is sent sporadically from ETC when LSSMA display shall be started/updated/stopped.",
            Identifiers = new List<string>
            {
                "230530180",
                "230535180"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Name = "MMI_V_LSSMA",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "LSSMA speed indication value",
                    LookupTable = new Dictionary<string, string> {{"65535", "no LSSMA displayed"}}
                }
            }
        };

        // checked RVV 11-11-2019 2.11
        public static DataSetDefinition EVC_24 => new DataSetDefinition
        {
            Name = "EVC_24 MMI_SYSTEM_INFO",
            Comment =
                "This packet shall be sent on request from the driver. The packet contains misc. system info for operational and initial maintenance purposes.",
            Identifiers = new List<string>
            {
                "230530190",
                "230535190"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Name = "MMI_NID_ENGINE_1",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Vehicle identity",
                    LookupTable = new Dictionary<string, string> {{"0", "Unknown"}}
                },
                new BitField
                {
                    Name = "MMI_T_TIMEOUT_BRAKE",
                    BitFieldType = BitFieldType.UnixEpochUtc,
                    Length = 32,
                    Comment = "Next timeout for brake test in Unix Epoch"
                },
                new BitField
                {
                    Name = "MMI_T_TIMEOUT_BTM",
                    BitFieldType = BitFieldType.UnixEpochUtc,
                    Length = 32,
                    Comment = "Next timeout for BTM test in Unix Epoch"
                },
                new BitField
                {
                    Name = "MMI_T_TIMEOUT_TBSW",
                    BitFieldType = BitFieldType.UnixEpochUtc,
                    Length = 32,
                    Comment = "Next timeout for TBSW test in Unix Epoch"
                },
                new BitField
                {
                    Name = "MMI_M_ETC_VER",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "ETC SW version" +
                              "\r\nBits:" +
                              "\r\n0..7 = X (UNSIGNED8)" +
                              "\r\n8..15 = Y (UNSIGNED8)" +
                              "\r\n16..23 = Z (UNSIGNED8)" +
                              "\r\n24..31 = spare"
                },
                new BitField
                {
                    Name = "MMI_M_AVAIL_SERVICES",
                    BitFieldType = BitFieldType.HexString,
                    Length = 16,
                    Comment = "Current HW configuration" +
                              "\r\nBits:" +
                              "\r\n0: Not used (MMI 1 always installed)" +
                              "\r\n1: Normal MMI 2 installed" +
                              "\r\n2: Redundant MMI 1 installed" +
                              "\r\n3: Redundant MMI 2 installed" +
                              "\r\n4: BTM antenna 1 installed" +
                              "\r\n5: BTM antenna 2 installed" +
                              "\r\n6: Radio modem 1 installed" +
                              "\r\n7: Radio modem 2 installed" +
                              "\r\n8: Spare" +
                              "\r\n9: DRU installed" +
                              "\r\n10: Euroloop BTM(s) installed" +
                              "\r\n11..15: Spare"
                },
                new BitField
                {
                    Name = "MMI_M_BRAKE_CONFIG",
                    BitFieldType = BitFieldType.HexString,
                    Length = 8,
                    Comment = "Current brake configuration" +
                              "\r\nBits:" +
                              "\r\n0: SB available" +
                              "\r\n1: spare" +
                              "\r\n2: 0 = EB as RTW, 1 = SB as RTW" +
                              "\r\n3: 0 = Release TCO on brake release, 1 = Release TCO when coasting" +
                              "\r\n4: TCO feedback available" +
                              "\r\n5: Soft isolation allowed" +
                              "\r\n6: Monitoring of EB1 cut-off enabled" +
                              "\r\n7: Monitoring of EB2 cut-off enabled"
                },
                new BitField
                {
                    Name = "MMI_M_LEVEL_INST",
                    BitFieldType = BitFieldType.HexString,
                    Length = 8,
                    Comment = "Installed levels" +
                              "\r\nBits:" +
                              "\r\n0: Level 0 installed" +
                              "\r\n1: Level NTC installed" +
                              "\r\n2: Level 1 installed" +
                              "\r\n3: Level 2 installed" +
                              "\r\n4: Level 3 installed" +
                              "\r\n5..7: spare"
                },
                new BitField
                {
                    Name = "MMI_N_NIDNTC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Number of installed NTC levels (range: 0-10)"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_NIDNTC"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            MMI_NID_NTC,
                            new BitField
                            {
                                Name = "MMI_NID_STMSTATE",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Current STM state",
                                LookupTable = new Dictionary<string, string>
                                {
                                    {"0", "Reserved (mapped to NP for consistency)"},
                                    {"1", "Power On (PO)"},
                                    {"2", "Configuration (CO)"},
                                    {"3", "Data Entry (DE)"},
                                    {"4", "Cold Standby (CS)"},
                                    {"5", "Reserved (mapped to CS for consistency)"},
                                    {"6", "Hot Standby"},
                                    {"7", "Data Available (DA)"},
                                    {"8", "Failure (FA)"}
                                }
                            }
                        }
                    }
                }
            }
        };

        // checked
        public static DataSetDefinition EVC_25 => new DataSetDefinition
        {
            Name = "EVC_25 MMI_SPECIFIC_STM_DE_REQUEST",
            Comment =
                "This packet is used when a STM requests specific data input from the driver. Each MMI packet contains the data of one packet STM-179 according to [FFFIS_058], which in turn holds up to 5 variables. " +
                "If more than 5 variables need to be presented this MMI packet will be repeated. NID_PACKET and L_PACKET of packet STM-179 are stripped." +
                "\r\nBecause the content of this packet is given by the STM-functionality the assignment, ranges, values and meaning of all variables can only be given in the project-specific documentation.",
            Identifiers = new List<string>
            {
                "230530200",
                "230535200"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_NID_NTC,
                new BitField
                {
                    Name = "MMI_STM_Q_DRIVERINT",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Need for driver intervention or not during Specific STM Data Entry."
                },
                MMI_STM_Q_FOLLOWING,
                new BitField
                {
                    Name = "evc25_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "Spare for byte alignment"
                },
                new BitField
                {
                    Name = "evc25_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 4,
                    Comment = "Spare for byte alignment"
                },
                MMI_N_ITER,
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_ITER"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            MMI_NID_NTC2,
                            MMI_STM_NID_DATA,
                            MMI_EVC_M_XATTRIBUTE,
                            MMI_L_CAPTION,
                            MMI_STM_X_CAPTION,
                            MMI_STM_L_VALUE,
                            MMI_STM_X_VALUE,
                            new BitField
                            {
                                Name = "MMI_N_ITER2",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 16,
                                Comment =
                                    "Maximum iteration data pick-up list (range: 0-16).\r\nNote: Higher values are allowed, if a reduced size of caption text is used but limited by the maximum message length."
                            },
                            new BitField
                            {
                                VariableLengthSettings = new VariableLengthSettings
                                {
                                    Name = "MMI_N_ITER2"
                                },
                                NestedDataSet = new DataSetDefinition
                                {
                                    BitFields = new List<BitField>
                                    {
                                        MMI_STM_L_VALUE2,
                                        MMI_STM_X_VALUE2
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        // checked
        public static DataSetDefinition EVC_26 => new DataSetDefinition
        {
            Name = "EVC_26 MMI_SPECIFIC_STM_DW_VALUES",
            Comment =
                "This packet is used when a STM presents specific data to the driver as the result of a STM specific data view request. " +
                "Each MMI packet contains the data of one packet STM-183 according to SUBSET-058, which in turn holds up to 5 variables. " +
                "If more than 5 variables need to be presented this MMI packet will be repeated. NID_PACKET and L_PACKET of packet STM-183 are stripped." +
                "\r\nBecause the content of this packet is given by the STM-functionality the assignment, ranges, values and meaning of all variables can only be given in the project-specific documentation.",
            Identifiers = new List<string>
            {
                "230530210",
                "230535210"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_NID_NTC,
                MMI_STM_Q_FOLLOWING,
                new BitField
                {
                    Name = "evc26_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare for alignment"
                },
                new BitField
                {
                    Name = "evc26_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "Spare for alignment"
                },
                new BitField
                {
                    Name = "evc26_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 4,
                    Comment = "Spare for alignment"
                },
                MMI_N_ITER,
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_ITER"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            MMI_NID_NTC2,
                            MMI_STM_NID_DATA,
                            MMI_L_CAPTION,
                            MMI_STM_X_CAPTION,
                            MMI_STM_L_VALUE,
                            MMI_STM_X_VALUE
                        }
                    }
                }
            }
        };

        // checked
        public static DataSetDefinition EVC_27 => new DataSetDefinition
        {
            Name = "EVC_27 MMI_SPECIFIC_STM_TEST_REQUEST",
            Comment =
                "This packet is sent to the MMI if a STM requests a national specific test procedure. The STM issues such a request via packet STM-19 according to [FFFIS_058]. " +
                "ETCS Onboard transfers the information of STM-19 to packet EVC-27. Packet STM-19 is not seen by the MMI.",
            Identifiers = new List<string>
            {
                "230530220",
                "230535220"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Name = "XATTRIBUTE Spare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 6
                },
                MMI_EVC_M_XATTRIBUTE,
                MMI_NID_NTC,
                new BitField
                {
                    Name = "evc27_spare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare for alignment"
                },
                new BitField
                {
                    Name = "MMI_STM_L_TEXT_IPT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Number of characters in text message (range: 0-40)." +
                        "\r\nNote: If variable = 0 the MMI will remove the STM test request message."
                },
                new BitField
                {
                    Name = "MMI_STM_X_TEXT",
                    BitFieldType = BitFieldType.HexString,
                    Comment = "Text character",
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_STM_L_TEXT_IPT",
                        ScalingFactor = 8
                    }
                }
            }
        };

        // checked 20190510 2.11
        public static DataSetDefinition EVC_28 => new DataSetDefinition
        {
            Name = "EVC_28 MMI_ECHOED_SET_VBC_DATA",
            Comment =
                "This packet shall be sent from ETC to MMI when the driver has finished the 'Set VBC' data entry by pressing the \"Yes\" button and all checks have passed. " +
                "The packet starts the 'Set VBC' Data Validation window / procedure at MMI. ",
            Identifiers = new List<string>
            {
                "230530230",
                "230535230"
            },
            BitFields = new List<BitField>
            {
               new BitField
               {
                    Length = 1,
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            MMI_M_PACKET,
                            MMI_L_PACKET,
                        }
                    }
                },
                new BitField
                {
                    Length = 1,
                    NestedDataSet = new DataSetDefinition
                    {
                        InvertBits = true,
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "MMI_M_VBC_CODE_",
                                BitFieldType = BitFieldType.UInt32,
                                Length = 32,
                                Comment = "VBC Identifier, bit inverted"
                            }
                        }
                    }
                }
            }
        };

        // checked 20190510 2.11
        public static DataSetDefinition EVC_29 => new DataSetDefinition
        {
            Name = "EVC_29 MMI_ECHOED_REMOVE_VBC_DATA",
            Comment =
                "This packet shall be sent from ETC to MMI when the driver has finished the 'Remove VBC' data entry by pressing the \"Yes\" button and all checks have passed. " +
                "The packet starts the 'Remove VBC' Data Validation window / procedure at MMI. ",
            Identifiers = new List<string>
            {
                "230530240",
                "230535240"
            },
            BitFields = new List<BitField>
            {
               new BitField
               {
                    Length = 1,
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            MMI_M_PACKET,
                            MMI_L_PACKET,
                        }
                    }
                },
                new BitField
                {
                    Length = 1,
                    NestedDataSet = new DataSetDefinition
                    {
                        InvertBits = true,
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "MMI_M_VBC_CODE_",
                                BitFieldType = BitFieldType.UInt32,
                                Length = 32,
                                Comment = "VBC Identifier, bit inverted"
                            }
                        }
                    }
                }
            }
        };

        // checked RVV 12-11-2019 2.11
        public static DataSetDefinition EVC_30 => new DataSetDefinition
        {
            Name = "EVC_30 MMI_ENABLE_REQUEST",
            Comment =
                "The packet is used by the ETC to dynamically enable/disable generic and customised (i.e. project specific) procedures on the MMI." +
                "\r\nNote: The customisable contents of the packet (variables MMI_Q_EVC_PROJECT, MMI_M_CUST_PROC_ID, MMI_Q_CUST_REQUEST_ENABLE) may be customised by projects. This has to be specified in the project's documentation.",
            Identifiers = new List<string>
            {
                "230530250",
                "230535250"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_NID_WINDOW,
                new BitField
                {
                    Name = "evc30_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "spare for alignment"
                },
                new BitField
                {
                    Name = "evc30_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "spare for alignment"
                },
                new BitField
                {
                    Name = "Start",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Start button"
                },
                new BitField
                {
                    Name = "Driver ID",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Driver ID button"
                },
                new BitField
                {
                    Name = "Train data",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Train Data button"
                },
                new BitField
                {
                    Name = "Level",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Level button"
                },
                new BitField
                {
                    Name = "Train running number",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable TRN button"
                },
                new BitField
                {
                    Name = "Shunting",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Shunting button"
                },
                new BitField
                {
                    Name = "Exit Shunting",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Exit Shunting button"
                },
                new BitField
                {
                    Name = "Non-Leading",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Non-Leading button"
                },
                new BitField
                {
                    Name = "Maintain Shunting",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Maintain Shunting button"
                },
                new BitField
                {
                    Name = "EOA",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable EOA button"
                },
                new BitField
                {
                    Name = "Adhesion",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Adhesion button"
                },
                new BitField
                {
                    Name = "SR speed / distance",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable SR speed/distance button"
                },
                new BitField
                {
                    Name = "Train integrity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Train Integrity button"
                },
                new BitField
                {
                    Name = "Language",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Language button"
                },
                new BitField
                {
                    Name = "Volume",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Volume button"
                },
                new BitField
                {
                    Name = "Brightness",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Brightness button"
                },
                new BitField
                {
                    Name = "System version",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable System Version button"
                },
                new BitField
                {
                    Name = "Set VBC",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Set VBC button"
                },
                new BitField
                {
                    Name = "Remove VBC",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Remove VBC button"
                },
                new BitField
                {
                    Name = "Contact last RBC",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Contact Last RBC button"
                },
                new BitField
                {
                    Name = "Use short number",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Use Short Number button"
                },
                new BitField
                {
                    Name = "Enter RBC data",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Enter RBC Data button"
                },
                new BitField
                {
                    Name = "Radio Network ID",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Radio Network ID button"
                },
                new BitField
                {
                    Name = "Geographical position",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Geographical Position button"
                },
                new BitField
                {
                    Name = "End of data entry (NTC)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable End of Data Entry (NTC) button"
                },
                new BitField
                {
                    Name = "Set local time, date and offset",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Set Clock button"
                },
                new BitField
                {
                    Name = "Set local offset",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Set Local Offset button"
                },
                new BitField
                {
                    Name = "Reserved",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "reserved"
                },
                new BitField
                {
                    Name = "Start Brake Test",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Start Brake Test button"
                },
                new BitField
                {
                    Name = "Enable wheel diameter",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Wheel Diameter button"
                },
                new BitField
                {
                    Name = "Enable Doppler",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Doppler Radar button"
                },
                new BitField
                {
                    Name = "Enable brake percentage",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable Brake Percentage button"
                },
                new BitField
                {
                    Name = "System Info",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Enable System Information button"
                },
                new BitField
                {
                    Name = "MMI_Q_REQUEST_ENABLE spares",
                    BitFieldType = BitFieldType.Spare,
                    Length = 31,
                    Comment = "Spare bits"
                }
            }
        };

        // checked RVV 14-11-2019 2.11
        public static DataSetDefinition EVC_31 => new DataSetDefinition
        {
            Name = "EVC_31 MMI_NTC_DE_SELECT",
            Comment = "This packet controls the display of the NTC data entry selection window (ERA ch. 12.2.1).",
            Identifiers = new List<string>
            {
                "230530260",
                "230535260"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Name = "MMI_N_NIDNTC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Number of NTC buttons to display. Range 0..8"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_NIDNTC"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                           MMI_NID_NTC,
                           new BitField
                           {
                               Name = "MMI_Q_NTC_ENABLE",
                               BitFieldType = BitFieldType.HexString,
                               Length = 8,
                               Comment =
                                   "Enable Bit mask. LSB relates to first NTC in list ('MMI_N_NIDNTC') above. Content is irrelevant if MMI_N_NIDNTC = 0."
                           }
                        }
                    }
                }
            }
        };

        // checked RVV 14-11-2019 2.11
        public static DataSetDefinition EVC_32 => new DataSetDefinition
        {
            Name = "EVC_32 MMI_TRACK_CONDITIONS",
            Comment = "Packet replacing old ATP-2 packet. This packet transmits track condition information.",
            Identifiers = new List<string>
            {
                "230530270",
                "230535270"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Name = "MMI_Q_TRACKCOND_UPDATE",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Specifies if stored data should be updated."
                },
                new BitField
                {
                    Name = "evc32_spare0",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare for byte alignment"
                },
                new BitField
                {
                    Name = "evc32_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "Spare for byte alignment"
                },
                new BitField
                {
                    Name = "evc32_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 4,
                    Comment = "Spare for byte alignment"
                },
                new BitField
                {
                    Name = "evc32_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare for byte alignment"
                },
                new BitField
                {
                    Name = "MMI_N_TRACKCONDITIONS",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Quantifier used to specify the number of variables in a list"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_TRACKCONDITIONS"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "MMI_O_TRACKCOND_ANNOUNCE",
                                BitFieldType = BitFieldType.Int32,
                                Length = 32,
                                Comment =
                                    "Track condition announcement. This position can be adjusted depending on supervision.",
                                LookupTable = new Dictionary<string, string> {{"-2147483647", "No announcement location exists or is already passed"}}
                            },
                            new BitField
                            {
                                Name = "MMI_O_TRACKCOND_START",
                                BitFieldType = BitFieldType.Int32,
                                Length = 32,
                                Comment =
                                    "Start location of track condition. This position can be adjusted depending on supervision.",
                                LookupTable = new Dictionary<string, string> {{"-2147483647", "Start location already passed" }}
                            },
                            new BitField
                            {
                                Name = "MMI_O_TRACKCOND_END",
                                BitFieldType = BitFieldType.Int32,
                                Length = 32,
                                Comment =
                                    "End location of track condition. This position can be adjusted depending on supervision.",
                                LookupTable = new Dictionary<string, string> {{"-2147483647", "End location already passed" }}
                            },
                            MMI_NID_TRACKCOND,
                            MMI_M_TRACKCOND_TYPE,
                            MMI_Q_TRACKCOND_STEP,
                            new BitField
                            {
                                Name = "MMI_Q_TRACKCOND_ACTION_START",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 1,
                                Comment = "Type of action at start location",
                                LookupTable = new Dictionary<string, string>
                                {
                                    {"0", "with driver action (manual)"},
                                    {"1", "without driver action (automatic)"}
                                }
                            },
                            new BitField
                            {
                                Name = "MMI_Q_TRACKCOND_ACTION_END",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 1,
                                Comment = "Type of action at end location",
                                LookupTable = new Dictionary<string, string>
                                {
                                    {"0", "with driver action (manual)"},
                                    {"1", "without driver action (automatic)"}
                                }
                            },
                            new BitField
                            {
                                Name = "evc32_spare4",
                                BitFieldType = BitFieldType.Spare,
                                Length = 2,
                                Comment = "Spare for alignment"
                            },
                            new BitField
                            {
                                Name = "evc32_spare5",
                                BitFieldType = BitFieldType.Spare,
                                Length = 8,
                                Comment = "Spare for alignment"
                            }
                        }
                    }
                }
            }
        };

        // checked RVV 14-11-2019 2.11
        public static DataSetDefinition EVC_33 => new DataSetDefinition
        {
            Name = "EVC_33 MMI_ADDITIONAL_ORDER",
            Comment = "Packet replacing old ATP-3 packet. This packet is the additional order or announcement.",
            Identifiers = new List<string>
            {
                "230530280",
                "230535280"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_NID_TRACKCOND,
                MMI_M_TRACKCOND_TYPE,
                MMI_Q_TRACKCOND_STEP,
                new BitField
                {
                    Name = "MMI_Q_TRACKCOND_ACTION",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 1,
                    Comment = "Required driver action",
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "with driver action (manual)"},
                        {"1", "without driver action (automatic)"}
                    }
                },
                new BitField
                {
                    Name = "evc33_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare for byte alignment"
                },
                new BitField
                {
                    Name = "evc33_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare for byte alignment"
                },
                new BitField
                {
                    Name = "evc33_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare for byte alignment"
                }
            }
        };

        // checked RVV 14-11-2019 2.11
        public static DataSetDefinition EVC_34 => new DataSetDefinition
        {
            Name = "EVC_34 MMI_SYSTEM_VERSION",
            Comment = "Packet containing operated system version",
            Identifiers = new List<string>
            {
                "230530290",
                "230535290"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Name = "MMI_M_OPERATED_SYSTEM_VERSION",
                    BitFieldType = BitFieldType.HexString,
                    Length = 16,
                    Comment = "Operated system version according to SS026 (X.Y - first byte is X)."
                }
            }
        };

        // checked RVV 14-11-2019 2.11
        public static DataSetDefinition EVC_40 => new DataSetDefinition
        {
            Name = "EVC_40 MMI_CURRENT_MAINTENANCE_DATA",
            Comment =
                "This packet shall be sent when stored maintenance data shall be presented via the MMI. " +
                "This packet is used in relation with packets EVC-41, EVC-140 and EVC-141. " +
                "The purpose of those packets is to provide vital maintenance data to the ATP.",
            Identifiers = new List<string>
            {
                "230530300",
                "230535300"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_M_PULSE_PER_KM_1,
                MMI_M_PULSE_PER_KM_2,
                MMI_M_SDU_WHEEL_SIZE_1,
                MMI_M_SDU_WHEEL_SIZE_2,
                MMI_Q_MD_DATASET,
                MMI_M_WHEEL_SIZE_ERR
            }
        };

        // checked RVV 17-11-2019 2.11
        public static DataSetDefinition EVC_41 => new DataSetDefinition
        {
            Name = "EVC_41 MMI_ECHOED_MAINTENANCE_DATA",
            Comment =
                "This packet shall be sent when new or accepted maintenance data shall be presented via the MMI for confirmation. " +
                "This packet is used in relation with packets EVC-40, EVC-140 and EVC-141." +
                "\r\nNote: All variables in this packet (Exception: MMI_M_PACKET and MMI_L_PACKET) shall be the same as in packet EVC-40 but bit-inverted and in reverse order. " +
                "Their names shall end with “_”. See train data packets EVC-6, EVC-10, EVC-110 and EVC-117 as examples of packet definitions according this rule.",
            Identifiers = new List<string>
            {
                "230530310",
                "230535310"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Length = 1,
                    NestedDataSet = new DataSetDefinition
                    {
                        InvertBits = true,
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "MMI_M_PULSE_PER_KM_2_",
                                BitFieldType = BitFieldType.UInt32,
                                Length = 32,
                                Comment = "Number of pulses per km (Radar 2)"
                            },
                            new BitField
                            {
                                Name = "MMI_M_PULSE_PER_KM_1_",
                                BitFieldType = BitFieldType.UInt32,
                                Length = 32,
                                Comment = "Number of pulses per km (Radar 1)"
                            },
                            new BitField
                            {
                                Name = "MMI_M_SDU_WHEEL_SIZE_2_",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 16,
                                Comment = "Wheel Diameter (Tacho 2)"
                            },
                            new BitField
                            {
                                Name = "MMI_M_SDU_WHEEL_SIZE_1_",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 16,
                                Comment = "Wheel Diameter (Tacho 1)"
                            },
                            new BitField
                            {
                                Name = "MMI_Q_MD_DATASET_",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Indicates the content of the maintenance telegram"
                            },
                            new BitField
                            {
                                Name = "MMI_M_WHEEL_SIZE_ERR_",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Wheel Size Accuracy"
                            }
                        }
                    }
                }
            }
        };

        // checked RVV 17-11-2019 2.11
        public static DataSetDefinition EVC_50 => new DataSetDefinition
        {
            Name = "EVC_50 MMI_CURRENT_BRAKE_PERCENTAGE",
            Comment =
                "This packet shall be sent when stored brake percentage data shall be presented via the MMI. " +
                "This packet is used in relation with packets EVC-51, EVC-150 and EVC-151. " +
                "The purpose of those packets is to allow the driver to modify the brake percentage besides the Train Data Entry procedure.",
            Identifiers = new List<string>
            {
                "230530320",
                "230535320"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Name = "MMI_M_BP_ORIG",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Original brake percentage (from Train Data Entry procedure). Range 10...250"
                },
                new BitField
                {
                    Name = "MMI_M_BP_CURRENT",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Currently used brake percentage",
                    LookupTable = new Dictionary<string, string>
                    {
                        {"251", "Technical Range Check failed"},
                        {"255", "Original value exceeded (will be displayed as '++++' in grey, Data Field 'Current BP')"}
                    }
                },
                new BitField
                {
                    Name = "MMI_M_BP_MEASURED",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Last measured brake percentage. Range 10...250",
                    LookupTable = new Dictionary<string, string> {{"255", "No last measured brake percentage available, i.e. this will be displayed as '_ _ _ _' in grey in Data Field 'Last measured BP'."}}
                }
            }
        };

        // checked RVV 18-11-2019 2.13
        public static DataSetDefinition EVC_51 => new DataSetDefinition
        {
            Name = "EVC_51 MMI_ECHOED_BRAKE_PERCENTAGE",
            Comment =
                "This packet shall be sent when new or accepted brake percentage data shall be presented via the MMI for confirmation. This packet is used in relation with packets EVC-50, EVC-150 and EVC-151." +
                "\r\nNote: All variables in this packet(Exception: MMI_M_PACKET and MMI_L_PACKET) shall be the same as in packet EVC - 50 but bit - inverted. Their names shall end with “_”. " +
                "See train data packets EVC-6, EVC-10, EVC-110 and EVC-117 as examples of packet definitions according this rule.",
            Identifiers = new List<string>
            {
                "230530330",
                "230535330"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Length = 1,
                    NestedDataSet = new DataSetDefinition
                    {
                        InvertBits = true,
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "MMI_M_BP_ORIG_",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Original brake percentage (from Train Data Entry procedure)"
                            },
                            new BitField
                            {
                                Name = "MMI_M_BP_CURRENT_",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Currently used brake percentage"
                            },
                            new BitField
                            {
                                Name = "MMI_M_BP_MEASURED_",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Last measured brake percentage"
                            }
                        }
                    }
                }
            }
        };

        #endregion

        #region DMI->EVC (EVC-100 to EVC-152)

        #endregion

        #region Safe Words

        public static BitField SSW1 => new BitField
        {
            Name = "SSW1",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Safe status word for IPTCom (variables 1-8)"
        };

        public static BitField SSW2 => new BitField
        {
            Name = "SSW2",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Safe status word for IPTCom (variables 9-16)"
        };

        public static BitField SSW3 => new BitField
        {
            Name = "SSW3",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Safe status word for IPTCom (variables > 16)"
        };

        #endregion

        #region Common MMI Variables

        public static BitField MMI_EVC_M_XATTRIBUTE => new BitField
        {
            Name = "MMI_EVC_M_XATTRIBUTE",
            Length = 1, // fixed to one iteration
            Comment = "Attribute for text string of the data label",
            NestedDataSet = new DataSetDefinition
            {
                BitFields = new List<BitField>
                {
                    new BitField
                    {
                        Name = "Reserved",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        Comment = "This value should be 1 for all attributes, 0 is Reserved"
                    },
                    new BitField
                    {
                        Name = "Flashing",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"False", "Normal"},
                            {"True", "Counter phase"}
                        }
                    },
                    new BitField
                    {
                        Name = "Flashing Speed",
                        BitFieldType = BitFieldType.UInt8,
                        Length = 2,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"0", "No Flashing"},
                            {"1", "Slow"},
                            {"2", "Fast"},
                            {"3", "Reserved"}
                        }
                    },
                    new BitField
                    {
                        Name = "Text Background Colour",
                        BitFieldType = BitFieldType.UInt8,
                        Length = 3,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"0", "Black text background"},
                            {"1", "White text background"},
                            {"2", "Red text background"},
                            {"3", "Blue text background"},
                            {"4", "Green text background"},
                            {"5", "Yellow text background"},
                            {"6", "Light red text background"},
                            {"7", "Light green text background"}
                        }
                    },
                    new BitField
                    {
                        Name = "Text Colour",
                        BitFieldType = BitFieldType.UInt8,
                        Length = 3,
                        LookupTable = new Dictionary<string, string>
                        {
                            {"0", "Black"},
                            {"1", "White"},
                            {"2", "Red"},
                            {"3", "Blue"},
                            {"4", "Green"},
                            {"5", "Yellow"},
                            {"6", "Light red"},
                            {"7", "Light green"}
                        }
                    }
                }
            }
        };

        public static BitField MMI_L_CAPTION => new BitField
        {
            Name = "MMI_L_CAPTION",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Length of X_CAPTION for data label (range: 0-20)."
        };

        public static BitField MMI_L_PACKET => new BitField
        {
            Name = "MMI_L_PACKET",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Packet Length"
        };

        public static BitField MMI_M_ACTIVE_CABIN => new BitField
        {
            Name = "MMI_M_ACTIVE_CABIN",
            BitFieldType = BitFieldType.UInt8,
            Length = 2,
            Comment = "Defines the identity of the activated cabin",
            LookupTable = new Dictionary<string, string>
            {
                {"0", "No cabin is active"},
                {"1", "Cabin 1 is active"},
                {"2", "Cabin 2 is active"},
                {"3", "Spare"}
            }
        };

        public static BitField MMI_M_BUTTONS => new BitField
        {
            Name = "MMI_M_BUTTONS",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Identifier of MMI Buttons",
            LookupTable = new Dictionary<string, string>
            {
                {"0", "BTN_MAIN"},
                {"1", "BTN_OVERRIDE"},
                {"2", "BTN_DATA_VIEW"},
                {"3", "BTN_SPECIAL"},
                {"4", "BTN_SETTINGS"},
                {"5", "BTN_START"},
                {"6", "BTN_DRIVER_ID"},
                {"7", "BTN_TRAIN_DATA"},
                {"8", "BTN_LEVEL"},
                {"9", "BTN_TRAIN_RUNNING_NUMBER"},
                {"10", "BTN_SHUNTING"},
                {"11", "BTN_EXIT_SHUNTING"},
                {"12", "BTN_NON_LEADING"},
                {"13", "BTN_MAINTAIN_SHUNTING"},
                {"14", "BTN_OVERRIDE_EOA"},
                {"15", "BTN_ADHESION"},
                {"16", "BTN_SR_SPEED_DISTANCE"},
                {"17", "BTN_TRAIN_INTEGRITY"},
                {"18", "BTN_SYSTEM_VERSION"},
                {"19", "BTN_SET_VBC"},
                {"20", "BTN_REMOVE_VBC"},
                {"21", "BTN_CONTACT_LAST_RBC"},
                {"22", "BTN_USE_SHORT_NUMBER"},
                {"23", "BTN_ENTER_RBC_DATA"},
                {"24", "BTN_RADIO_NETWORK_ID"},
                {"25", "BTN_DRIVERID_TRAIN_RUNNING_NUMBER"},
                {"26", "BTN_DRIVERID_SETTINGS"},
                {"27", "BTN_SWITCH_FIXED_TRAIN_DATA_ENTRY"},
                {"28", "BTN_SWITCH_FLEXIBLE_TRAIN_DATA_ENTRY"},
                {"29", "BTN_TOGGLE_TUNNELSTOP_AREA"},
                {"30", "BTN_TOGGLE_SPEED_DISTANCE_INFO"},
                {"31", "BTN_YES_TRACK_AHEAD_FREE"},
                {"32", "BTN_TOGGLE_GEOPOS"},
                {"33", "BTN_CLOSE"},
                {"34", "BTN_SCROLL_UP"},
                {"35", "BTN_SCROLL_DOWN"},
                {"36", "BTN_YES_DATA_ENTRY_COMPLETE"},
                {"37", "BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE"},
                {"38", "BTN_STM_DATA_ENTRY_SELECTION_POS1"},
                {"39", "BTN_STM_DATA_ENTRY_SELECTION_POS2"},
                {"40", "BTN_STM_DATA_ENTRY_SELECTION_POS3"},
                {"41", "BTN_STM_DATA_ENTRY_SELECTION_POS4"},
                {"42", "BTN_STM_DATA_ENTRY_SELECTION_POS5"},
                {"43", "BTN_STM_DATA_ENTRY_SELECTION_POS6"},
                {"44", "BTN_STM_DATA_ENTRY_SELECTION_POS7"},
                {"45", "BTN_STM_DATA_ENTRY_SELECTION_POS8"},
                {"46", "BTN_STM_END_OF_DATA_ENTRY"},
                {"253", "BTN_ENTER_DELAY_TYPE"},
                {"254", "BTN_ENTER"},
                {"255", "no button"}
            }
        };

        public static BitField MMI_M_DATA_ENABLE => new BitField
        {
            Name = "MMI_M_DATA_ENABLE",
            Length = 1, // fixed to one iteration
            Comment = "Enable Mask",
            NestedDataSet = new DataSetDefinition
            {
                BitFields = new List<BitField>
                {
                    new BitField
                    {
                        Name = "Train Set ID",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Train Category",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Train Length",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Brake Percentage",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Max. Train Speed",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Axle Load Category",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Airtightness",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Loading Gauge",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Spares",
                        BitFieldType = BitFieldType.Spare,
                        Length = 8
                    }
                }
            }
        };

        public static BitField MMI_M_PACKET => new BitField
        {
            Name = "MMI_M_PACKET",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Packet number"
        };

        public static BitField MMI_M_PULSE_PER_KM_1 => new BitField
        {
            Name = "MMI_M_PULSE_PER_KM_1",
            BitFieldType = BitFieldType.UInt32,
            Length = 32,
            Comment = "Number of pulses per km (Radar 1)",
            LookupTable = new Dictionary<string, string>
            {
                {"0", "No Radar 1 on board"},
                {"4294967290", "Technical Range Check failed"},
                {"4294967291", "Technical Resolution Check failed"},
                {"4294967292", "Technical Cross Check failed"},
                {"4294967293", "Operational Range Check failed"},
                {"4294967294", "Operational Cross Check failed"}
            }
        };

        public static BitField MMI_M_PULSE_PER_KM_2 => new BitField
        {
            Name = "MMI_M_PULSE_PER_KM_2",
            BitFieldType = BitFieldType.UInt32,
            Length = 32,
            Comment = "Number of pulses per km (Radar 2)",
            LookupTable = new Dictionary<string, string>
            {
                {"0", "No Radar 2 on board"},
                {"4294967290", "Technical Range Check failed"},
                {"4294967291", "Technical Resolution Check failed"},
                {"4294967292", "Technical Cross Check failed"},
                {"4294967293", "Operational Range Check failed"},
                {"4294967294", "Operational Cross Check failed"}
            }
        };

        public static BitField MMI_M_SDU_WHEEL_SIZE_1 => new BitField
        {
            Name = "MMI_M_SDU_WHEEL_SIZE_1",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Wheel Diameter (Tacho 1)",
            LookupTable = new Dictionary<string, string>
            {
                {"65530", "Technical Range Check failed"},
                {"65531", "Technical Resolution Check failed"},
                {"65532", "Technical Cross Check failed"},
                {"65533", "Operational Range Check failed"},
                {"65534", "Operational Cross Check failed"}
            }
        };

        public static BitField MMI_M_SDU_WHEEL_SIZE_2 => new BitField
        {
            Name = "MMI_M_SDU_WHEEL_SIZE_2",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Wheel Diameter (Tacho 2)",
            LookupTable = new Dictionary<string, string>
            {
                {"65530", "Technical Range Check failed"},
                {"65531", "Technical Resolution Check failed"},
                {"65532", "Technical Cross Check failed"},
                {"65533", "Operational Range Check failed"},
                {"65534", "Operational Cross Check failed"}
            }
        };

        public static BitField MMI_M_TRACKCOND_TYPE => new BitField
        {
            Name = "MMI_M_TRACKCOND_TYPE",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Type of track condition",
            LookupTable = new Dictionary<string, string>
            {
                {"0", "Non-Stopping Area"},
                {"1", "Tunnel Stopping Area"},
                {"2", "Sound Horn"},
                {"3", "Pantograph"},
                {"4", "Radio hole"},
                {"5", "Air tightness"},
                {"6", "Magnetic Shoe Brakes"},
                {"7", "Eddy Current Brakes"},
                {"8", "Regenerative Brakes"},
                {"9", "Main power switch/Neutral section"},
                {"10", "Change of traction system, not fitted"},
                {"11", "Change of traction system, AC 25 kV 50 Hz"},
                {"12", "Change of traction system, AC 15 kV 16.7 Hz"},
                {"13", "Change of traction system, DC 3 kV"},
                {"14", "Change of traction system, DC 1.5 kV"},
                {"15", "Change of traction system, DC 600/750 V"},
                {"16", "Level Crossing"}
            }
        };

        public static BitField MMI_M_WHEEL_SIZE_ERR => new BitField
        {
            Name = "MMI_M_WHEEL_SIZE_ERR",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Wheel Size Accuracy (maximum 32)",
            LookupTable = new Dictionary<string, string>
            {
                {"250", "Technical Range Check failed"},
                {"251", "Technical Resolution Check failed"},
                {"252", "Technical Cross Check failed"},
                {"253", "Operational Range Check failed"},
                {"254", "Operational Cross Check failed"}
            }
        };

        public static BitField MMI_N_CAPTION_NETWORK => new BitField
        {
            Name = "MMI_N_CAPTION_NETWORK",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Number of characters in Network ID caption text"
        };

        public static BitField MMI_N_DATA_ELEMENTS => new BitField
        {
            Name = "MMI_N_DATA_ELEMENTS",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Number of data elements (range: 0-8)"
        };

        public static BitField MMI_N_ITER => new BitField
        {
            Name = "MMI_N_ITER",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Maximum iteration data (range: 0-5)."
        };

        public static BitField MMI_N_TEXT => new BitField
        {
            Name = "MMI_N_TEXT",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Length of the MMI_X_TEXT that follows"
        };

        public static BitField MMI_NID_DATA => new BitField
        {
            Name = "MMI_NID_DATA",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "The identity of a data entry element",
            LookupTable = new Dictionary<string, string>
            {
                {"0", "Train running number"},
                {"1", "ERTMS/ETCS Level"},
                {"2", "Driver ID"},
                {"3", "Radio network ID"},
                {"4", "RBC ID"},
                {"5", "RBC phone number"},
                {"6", "Train Type (Train Data Set Identifier)"},
                {"7", "Train category"},
                {"8", "Length"},
                {"9", "Brake percentage"},
                {"10", "Maximum speed"},
                {"11", "Axle load category"},
                {"12", "Airtight"},
                {"13", "Loading gauge"},
                {"14", "Operated system version"},
                {"15", "SR Speed"},
                {"16", "SR Distance"},
                {"17", "Adhesion"},
                {"18", "Set VBC code"},
                {"19", "Remove VBC code"},
                {"255", "no specific data element defined"}
            }
        };

        public static BitField MMI_NID_NTC => new BitField
        {
            Name = "MMI_NID_NTC",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Identity of the NTC",
            LookupTable = new Dictionary<string, string>
            {
                {"20", "TPWS"},
                {"50", "CBTC"},
                {"255", "exit"}
            }
        };

        public static BitField MMI_NID_NTC2 => new BitField
        {
            Name = "MMI_NID_NTC[k]",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment =
                "STM identity used to point to the corresponding palette of Specific STM Data variables. " +
                "This NID_STM may be different from the one in the message header as the STM is allowed to re-use Specific STM data from another STM.",
            LookupTable = new Dictionary<string, string>
            {
                {"20", "TPWS"},
                {"50", "CBTC"},
                {"255", "exit"}
            }
        };

        public static BitField MMI_NID_RADIO => new BitField
        {
            Name = "MMI_NID_RADIO",
            BitFieldType = BitFieldType.HexString,
            Length = 64,
            Comment = "Radio subscriber number, not ASCII!"
        };

        public static BitField MMI_NID_RBC => new BitField
        {
            Name = "MMI_NID_RBC",
            BitFieldType = BitFieldType.UInt32,
            Length = 32,
            Comment = "RBC-ID, a concatenation of NID_C (10 bits) + NID_RBC (14 bits)"
        };

        public static BitField MMI_NID_TRACKCOND => new BitField
        {
            Name = "MMI_NID_TRACKCOND",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "NID assigned to track condition"
        };

        public static BitField MMI_NID_WINDOW => new BitField
        {
            Name = "MMI_NID_WINDOW",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Identifier of ETCS windows",
            LookupTable = new Dictionary<string, string>
            {
                {"0", "Default"},
                {"1", "Main"},
                {"2", "Override"},
                {"3", "Special"},
                {"4", "Settings"},
                {"5", "RBC contact"},
                {"6", "Train running number"},
                {"7", "Level"},
                {"8", "Driver ID"},
                {"9", "radio network ID"},
                {"10", "RBC data"},
                {"11", "Train data"},
                {"12", "SR speed/distance"},
                {"13", "Adhesion"},
                {"14", "Set VBC"},
                {"15", "Remove VBC"},
                {"16", "Train data validation"},
                {"17", "Set VBC validation"},
                {"18", "Remove VBC validation"},
                {"19", "Data View"},
                {"20", "System version"},
                {"21", "NTC data entry selection"},
                {"22", "NTC X data"},
                {"23", "NTC X data validation"},
                {"24", "NTC X data view"},
                {"253", "Language"},
                {"254", "close current window, return to parent"},
                {"255", "no window specified"}
            }
        };

        /// <summary>
        /// Is a bitmask but only one bit is used, so treat as Uint
        /// </summary>
        public static BitField MMI_Q_CLOSE_ENABLE => new BitField
        {
            Name = "MMI_Q_CLOSE_ENABLE",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Enabling close button in EVC-14, EVC-20 and EVC-22",
            LookupTable = new Dictionary<string, string>
            {
                {"0", "Close button disabled"},
                {"128", "Close button enabled"}
            }
        };

        public static BitField MMI_Q_DATA_CHECK => new BitField
        {
            Name = "MMI_Q_DATA_CHECK",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment =
                "This universal data check result variable provides control information how to display the related train data element in Echo Text/Data Entry Fields." +
                "\r\nAffected Echo Text/Data Entry Fields are indicated by MMI_NID_DATA.",
            LookupTable = new Dictionary<string, string>
            {
                {"0", "All checks have passed"},
                {"1", "Technical Range Check failed"},
                {"2", "Technical Resolution Check failed"},
                {"3", "Technical Cross Check failed"},
                {"4", "Operational Range Check failed"},
                {"5", "Operational Cross Check failed"}
            }
        };

        public static BitField MMI_Q_MD_DATASET => new BitField
        {
            Name = "MMI_Q_MD_DATASET",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Indicates the content of the maintenance telegram",
            LookupTable = new Dictionary<string, string>
            {
                {"0", "Wheel diameter"},
                {"1", "Doppler"}
            }
        };

        public static BitField MMI_Q_TEXT => new BitField
        {
            Name = "MMI_Q_TEXT",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Predefined text message",
            LookupTable = new Dictionary<string, string>
            {
                {"0", "Level crossing not protected"},
                {"1", "Acknowledgement"},
                {"256", "#1 (plain text only)"},
                {"257", "#3 LE07/LE11/LE13/LE15 (Ack Transition to Level #4)"},
                {"258", "#3 LE09 (Ack Transition to NTC #2)"},
                {"259", "#3 MO08 (Ack On Sight Mode)"},
                {"260", "#3 ST01 (Brake intervention)"},
                {"261", "Spare"},
                {"262", "#3 MO15 (Ack Reversing Mode)"},
                {"263", "#3 MO10 (Ack Staff Responsible Mode)"},
                {"264", "#3 MO17 (Ack Unfitted Mode)"},
                {"265", "#3 MO02 (Ack Shunting ordered by Trackside)"},
                {"266", "#3 MO05 (Ack Train Trip)"},
                {"267", "Balise read error"},
                {"268", "Communication error"},
                {"269", "Runaway movement"},
                {"273", "Unauthorized passing of EOA / LOA"},
                {"274", "Entering FS"},
                {"275", "Entering OS"},
                {"276", "#3 LE06/LE10/LE12/LE14 (Transition to Level #4)"},
                {"277", "#3 LE08 (Transition to NTC #2)"},
                {"278", "Emergency Brake Failure"},
                {"279", "Apply brakes"},
                {"280", " Emergency stop"},
                {"281", "Spare"},
                {"282", "#3 ST04 (Connection Lost/Set-Up failed)"},
                {"286", "#3 ST06 (Reversing is possible)"},
                {"290", "SH refused"},
                {"291", "Spare"},
                {"292", "SH request failed"},
                {"296", "Trackside not compatible"},
                {"297", "Spare"},
                {"298", "#3 DR02 (Confirm Track Ahead Free)"},
                {"299", "Train is rejected"},
                {"300", "No MA received at level transition"},
                {"305", "Train divided"},
                {"310", "Train data changed"},
                {"315", "SR distance exceeded"},
                {"316", "SR stop order"},
                {"320", "RV distance exceeded"},
                {"321", "ETCS Isolated"},
                {"514", "Perform Brake Test!"},
                {"515", "Unable to start Brake Test"},
                {"516", "Brake Test in Progress"},
                {"517", "Brake Test failed, perform new Test!"},
                {"520", "LZB Partial Block Mode"},
                {"521", "Override LZB Partial Block Mode"},
                {"522", "Restriction #1 km/h in Release Speed Area"},
                {"523", "Spare"},
                {"524", "Brake Test successful"},
                {"525", "Brake Test timeout in #1 Hours"},
                {"526", "Brake Test Timeout"},
                {"527", "Brake Test aborted, perform new Test?"},
                {"528", "Apply Brakes!"},
                {"531", "BTM Test in Progress"},
                {"532", "BTM Test Failure"},
                {"533", "BTM Test Timeout"},
                {"534", "BTM Test Timeout in #1 hours"},
                {"535", "ATP Restart required in #1 Hours"},
                {"536", "Restart ATP!"},
                {"540", "No Level available Onboard"},
                {"543", "#2 failed"},
                {"544", "Spare"},
                {"545", "#3 LE02A (Confirm LZB NTC)"},
                {"552", "Announced level(s) not supported Onboard"},
                {"553", "Spare"},
                {"554", "Reactivate the Cabin!"},
                {"555", "#3 MO20 (Ack SN Mode)"},
                {"560", "Trackside malfunction"},
                {"563", "Trackside Level(s) not supported Onboard"},
                {"564", "Confirm change of inhibit Level #1"},
                {"565", "Confirm change of inhibit STM #2"},
                {"568", "#3 ST03 (Connection established)"},
                {"569", "Radio network registration failed"},
                {"570", "Shunting rejected due to #2 Trip"},
                {"571", "Spare"},
                {"572", "No Track Description"},
                {"573", "#2 needs data"},
                {"574", "Cabin Reactivation required in #1 hours"},
                {"580", "Procedure Brake Percentage Entry terminated by ATP"},
                {"581", "Procedure Wheel Diameter Entry terminated by ATP"},
                {"582", "Procedure Doppler Radar Entry terminated by ATP"},
                {"583", "Spare"},
                {"606", "SH Stop Order"},
                {"609", "#3 Symbol ST100 (Network registered via one modem)"},
                {"610", "#3 Symbol ST102 (Network registered via two modems)"},
                {"613", "#3 Symbol ST103 (Connection Up) "},
                {"614", "#3 Symbol ST03B (Connection Up with two RBCs)"},
                {"615", "#3 Symbol ST03C (Connection Lost/Set-Up failed)"},
                {"621", "Unable to start Brake Test, vehicle not ready"},
                {"622", "Unblock EB"},
                {"623", "Spare"},
                {"624", "ETCS Failure"},
                {"625", "Spare"},
                {"626", "Spare"},
                {"627", "Speed Sensor failure"},
                {"628", "ETCS Service Brake not available"},
                {"629", "ETCS Traction Cut-off not available"},
                {"630", "ETCS Isolation Switch failure"},
                {"631", "#2 Isolation input not recognized"},
                {"632", "Coasting input not recognised"},
                {"633", "Spare"},
                {"634", "Spare"},
                {"635", "Juridical Recording not available"},
                {"636", "Euroloop not available"},
                {"637", "TIMS not available"},
                {"638", "Degraded Radio service"},
                {"639", "No Radio connection possible"},
                {"700", "#2 brake demand"},
                {"701", "Route unsuitable – axle load category"},
                {"702", "Route unsuitable – loading gauge"},
                {"703", "Route unsuitable – traction system"},
                {"704", "#2 is not available"},
                {"705", "New power-up required in #1 hours"},
                {"706", "No valid authentication key"},
                {"707", "Spare"},
                {"708", "Spare"},
                {"709", "#3 MO22 (Acknowledgement for Limited Supervision)"},
                {"710", "#3 (Train divided)"},
                {"711", "NL-input signal is withdrawn"},
                {"712", "Wheel data settings were successfully changed"},
                {"713", "Doppler radar settings were successfully changed"},
                {"714", "Brake percentage was successfully changed"},
                {"715", "No Country Selection in LZB PB Mode"},
                {"716", "#3 Symbol ST05 (hour glass)"}
            }
        };

        public static BitField MMI_Q_TRACKCOND_STEP => new BitField
        {
            Name = "MMI_Q_TRACKCOND_STEP",
            BitFieldType = BitFieldType.UInt8,
            Length = 4,
            Comment = "Variable describing step of the track condition.",
            LookupTable = new Dictionary<string, string>
            {
                {"0", "Approaching area"},
                {"1", "Announce Area"},
                {"2", "Inside area/active"},
                {"3", "Leave area"},
                {"4", "Remove TC"}
            }
        };

        public static BitField MMI_STM_L_VALUE => new BitField
        {
            Name = "MMI_STM_L_VALUE",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Length of X_VALUE for default value (range: 0-10)."
        };

        public static BitField MMI_STM_L_VALUE2 => new BitField
        {
            Name = "MMI_STM_L_VALUE",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Length of X_VALUE for pick-up list value (range: 0-10)."
        };

        public static BitField MMI_STM_NID_DATA => new BitField
        {
            Name = "MMI_STM_NID_DATA",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Identifier of a Specific STM Data to be entered."
        };

        public static BitField MMI_STM_Q_FOLLOWING => new BitField
        {
            Name = "MMI_STM_Q_FOLLOWING",
            BitFieldType = BitFieldType.Bool,
            Length = 1,
            Comment = "Indicate following data to be viewed at the same time"
        };

        public static BitField MMI_STM_X_CAPTION => new BitField
        {
            Name = "MMI_STM_X_CAPTION",
            BitFieldType = BitFieldType.StringLatin,
            Length = 8,
            Comment = "Data label caption text byte string",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "MMI_L_CAPTION",
                ScalingFactor = 8
            }
        };

        public static BitField MMI_STM_X_VALUE => new BitField
        {
            Name = "MMI_STM_X_VALUE",
            BitFieldType = BitFieldType.StringLatin,
            Length = 8,
            Comment = "Data value caption text byte string",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "MMI_STM_L_VALUE",
                ScalingFactor = 8
            }
        };

        public static BitField MMI_STM_X_VALUE2 => new BitField
        {
            Name = "MMI_STM_X_VALUE",
            BitFieldType = BitFieldType.StringLatin,
            Length = 8,
            Comment = "Data value caption text byte string",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "MMI_STM_L_VALUE",
                ScalingFactor = 8
            }
        };

        public static BitField MMI_T_ZONE_OFFSET => new BitField
        {
            Name = "MMI_T_ZONE_OFFSET",
            BitFieldType = BitFieldType.Int8,
            Length = 8,
            Comment = "Time zone offset",
            Scaling = 0.25d,
            AppendString = "h"
        };

        public static BitField MMI_T_UTC => new BitField
        {
            Name = "MMI_T_UTC",
            BitFieldType = BitFieldType.UnixEpochUtc,
            Length = 32,
            Comment = "UTC time as seconds since 01.01.1970, 00:00:00"
        };

        public static BitField MMI_X_DRIVER_ID => new BitField
        {
            Name = "MMI_X_DRIVER_ID",
            BitFieldType = BitFieldType.StringLatin,
            Length = 128
        };

        public static BitField MMI_X_CAPTION_NETWORK => new BitField
        {
            Name = "MMI_X_CAPTION_NETWORK",
            BitFieldType = BitFieldType.StringLatin,
            Comment = "Pre-configured Network ID",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "MMI_N_CAPTION_NETWORK",
                ScalingFactor = 8
            }
        };

        public static BitField MMI_X_TEXT => new BitField
        {
            Name = "MMI_X_TEXT",
            BitFieldType = BitFieldType.StringAscii,
            Comment = "Gives the content (character by character) of a text message to be displayed",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "MMI_N_TEXT",
                ScalingFactor = 8
            }
        };

        #endregion

        #region Common OBU Variables

        public static BitField OBU_TR_M_Mode => new BitField
        {
            Name = "OBU_TR_M_Mode",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "ETCS Mode",
            LookupTable = new Dictionary<string, string>
            {
                {"0", "Full Supervision"},
                {"1", "On Sight"},
                {"2", "Staff Responsible"},
                {"3", "Shunting"},
                {"4", "Unfitted"},
                {"5", "Sleeping"},
                {"6", "Stand By"},
                {"7", "Trip"},
                {"8", "Post Trip"},
                {"9", "System Failure"},
                {"10", "Isolation"},
                {"11", "Non Leading"},
                {"12", "Limited Supervision"},
                {"13", "National System"},
                {"14", "Reversing"},
                {"15", "Passive Shunting"},
                {"16", "No Power"},
                {"128", "Unknown"}
            }
        };

        public static BitField OBU_TR_O_TRAIN => new BitField
        {
            Name = "OBU_TR_O_TRAIN",
            BitFieldType = BitFieldType.Int32,
            Length = 32,
            Comment = "Current nominal position of the train",
            AppendString = " cm"
        };

        #endregion

        #region SDT Trailer Stuff

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition SDT_Trailer => new DataSetDefinition
        {
            Name = "SDT_2 SDT_IP_TRAILER",
            Comment = "SDTv2 structure",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "SDT_RES1",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Safe data trailer, reserved"
                },
                new BitField
                {
                    Name = "SDT_RES2",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Safe data trailer, reserved"
                },
                new BitField
                {
                    Name = "SDT_UDV_MainVersion",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "User Data Main Version"
                },
                new BitField
                {
                    Name = "SDT_RES3",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Safe data trailer, reserved"
                },
                new BitField
                {
                    Name = "SDT_SSC",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Safe Sequence Counter"
                },
                new BitField
                {
                    Name = "SDT_CRC",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Safety Code"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition MVB_SDT_Trailer => new DataSetDefinition
        {
            Name = "SDT_1 SDT_MVB_TRAILER",
            Comment = "SDTv2 structure",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "SDT_UDV",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                    Comment = "User Data Version"
                },
                new BitField
                {
                    Name = "SDT_UDV_RESERVED",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                    Comment = "Reserved for future use."
                },
                new BitField
                {
                    Name = "SDT_SSC",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Safe Sequence Counter"
                },
                new BitField
                {
                    Name = "SDT_CRC",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Safety Code"
                }
            }
        };

        #endregion

        // checked 25-10-2019 RVV
        public static DataSetDefinition IPT_ECHO => new DataSetDefinition
        {
            Name = "IPT_ECHO IPT_ECHO",
            Comment = "ECHO telegram structure",
            Identifiers = new List<string> { "110" },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "IPT_ECHO_Cmd",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "ECHO Command. Either: \r\n1 – request (sent by end device) \r\n2 – reply (sent by the IPTCom Echo server)"
                },
                new BitField
                {
                    Name = "IPT_ECHO_Reserved0",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Reserved. Set to zero."
                },
                new BitField
                {
                    Name = "IPT_ECHO_Challenge",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment =
                        "CRC32 calculated over the PAYLOAD.\r\nNote: watch the XOR modification of this value in the returned telegram [SDT_ICD] Ch. 4.10.1. Table 17."
                },
                new BitField
                {
                    Name = "IPT_ECHO_Sendtime",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Timestamp set by the safe CPU at send time."
                },
                new BitField
                {
                    Name = "IPT_ECHO_Reserved1",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Reserved. Set to zero."
                },
                new BitField
                {
                    Name = "IPT_ECHO_Uniquecrc",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "This field contains the CRC32 of the unique identifier of the sending device."
                },
                new BitField
                {
                    Name = "IPT_ECHO_Reserved20",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Reserved. Set to zero."
                },
                new BitField
                {
                    Name = "IPT_ECHO_Reserved21",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Reserved. Set to zero."
                },
                new BitField
                {
                    Name = "IPT_ECHO_Reserved22",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Reserved. Set to zero."
                },
                new BitField
                {
                    Name = "IPT_ECHO_Reserved23",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Reserved. Set to zero."
                },
                new BitField
                {
                    Name = "IPT_ECHO_Reserved24",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Reserved. Set to zero."
                }
            }
        };

    }
}