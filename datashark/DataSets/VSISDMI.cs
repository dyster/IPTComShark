using System.Collections.Generic;
using BitDataParser;

// ReSharper disable InconsistentNaming
#pragma warning disable 1591

namespace IPTComShark.DataSets
{
    public class VSISDMI : DataSetCollection
    {
        public VSISDMI()
        {
            this.Name = "VSIS 2.18";
            this.Description = "EVC Telegrams based on the VSIS v2.18";

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
            DataSets.Add(EVC_42);
            DataSets.Add(EVC_50);
            DataSets.Add(EVC_51);
            DataSets.Add(EVC_52);

            DataSets.Add(EVC_100);
            DataSets.Add(EVC_101);
            DataSets.Add(EVC_102);
            DataSets.Add(EVC_104);
            DataSets.Add(EVC_106);
            DataSets.Add(EVC_107);
            DataSets.Add(EVC_109);
            DataSets.Add(EVC_110);
            DataSets.Add(EVC_111);
            DataSets.Add(EVC_112);
            DataSets.Add(EVC_116);
            DataSets.Add(EVC_118);
            DataSets.Add(EVC_119);
            DataSets.Add(EVC_121);
            DataSets.Add(EVC_122);
            DataSets.Add(EVC_123);
            DataSets.Add(EVC_128);
            DataSets.Add(EVC_129);
            DataSets.Add(EVC_140);
            DataSets.Add(EVC_141);
            DataSets.Add(EVC_142);
            DataSets.Add(EVC_150);
            DataSets.Add(EVC_151);
            DataSets.Add(EVC_152);
            DataSets.Add(EVC_153);
            DataSets.Add(EVC_154);
            DataSets.Add(EVC_155);
        }

#region EVC-0 to EVC-99 (EVC->DMI)

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
                    LookupTable = new LookupTable
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
                    LookupTable = new LookupTable
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
                    LookupTable = new LookupTable
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
                    LookupTable = new LookupTable
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
                    LookupTable = new LookupTable
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
                    LookupTable = new LookupTable {{"-1", "Speed Unknown"}},
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
                    LookupTable = new LookupTable {{"-1", "No target speed"}},
                    AppendString = " cm/s"
                },
                new BitField
                {
                    Name = "MMI_V_PERMITTED",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Current permitted speed",
                    AppendString = " cm/s"
                },
                new BitField
                {
                    Name = "MMI_V_RELEASE",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Release speed applied at the EOA",
                    LookupTable = new LookupTable {{"-1", "No release speed"}},
                    AppendString = " cm/s"
                },
                new BitField
                {
                    Name = "MMI_O_BRAKETARGET",
                    BitFieldType = BitFieldType.Int32,
                    Length = 32,
                    Comment =
                        "This is the position in odometer co-ordinates of the next restrictive discontinuity of the static speed profile or target, which has influence on the braking curve. " +
                        "This position can be adjusted depending on supervision.",
                    LookupTable = new LookupTable
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
                    LookupTable = new LookupTable {{"-1", "Spare"}}
                },
                new BitField
                {
                    Name = "MMI_V_INTERVENTION",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Current intervention speed",
                    AppendString = " cm/s"
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
                SSW3,
                new BitField
                {
                    NestedDataSet = SDT_Trailer,
                    Length = 1
                }
            }
        };

        // checked RVV 15-11-2021 2.16
        public static DataSetDefinition EVC_2 => new DataSetDefinition
        {
            Name = "EVC_2 MMI_STATUS",
            Comment =
                "This packet contains status information for the driver and shall be sent to the MMI when­ever any of the status has changed, and at least once every 5 seconds.",
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

        // checked RVV 15-11-2021 2.16
        public static DataSetDefinition EVC_4 => new DataSetDefinition
        {
            Name = "EVC_4 MMI_TRACK_DESCRIPTION",
            Comment =
                "This packet contains trackside information to the driver. " +
                "Whenever new information is received from trackside, and at least once every 5 seconds, " +
                "the speed profile and the gradient profile shall be sent to the MMI.",
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
                                    "This is the position in odometer co-ordinates of the start location of a speed discontinuity in the most restrictive speed profile. " +
                                    "This position can be adjusted depending on supervision."
                            },
                            new BitField
                            {
                                Name = "MMI_V_MRSP",
                                BitFieldType = BitFieldType.Int16,
                                Length = 16,
                                Comment = "New speed value",
                                LookupTable = new LookupTable
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
                    LookupTable = new LookupTable {{"-255", "No current gradient profile"}}
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
                                LookupTable = new LookupTable
                                    {{"-255", "The gradient profile ends at the defined position"}}
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
                    LookupTable = new LookupTable {{"-1", "No more geo position report after this"}}
                },
                new BitField
                {
                    Name = "MMI_M_RELATIVPOS",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Train’s current geographical position given as an offset from last passed balise",
                    LookupTable = new LookupTable
                        {{"-1", "N/A (i.e. MMI shall display _ABSOLUTPOS only)"}}
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
                MMI_L_TRAIN,
                MMI_V_MAXTRAIN,
                MMI_NID_KEY_TRAIN_CAT,
                MMI_M_BRAKE_PERC,
                MMI_NID_KEY_AXLE_LOAD,
                MMI_M_AIRTIGHT,
                MMI_NID_KEY_LOAD_GAUGE,
                MMI_M_BUTTONS,
                MMI_M_TRAINSET_ID,
                MMI_M_ALT_DEM,
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
                                Comment = "Gives the content (character by character) of the caption text of a preconfigured train data set",
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
                    LookupTable = new LookupTable
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
                    LookupTable = STM_LookupTable                
                },
                new BitField
                {
                    Name = "MMI_OBU_TR_NID_STM_DA",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "NID_STM in DA state",
                    LookupTable = STM_LookupTable
                },
                new BitField
                {
                    Name = "MMI_OBU_TR_BrakeTestTimeOut",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Brake test timeout value"
                },
                OBU_TR_O_TRAIN,
                MMI_T_DMILM,
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
                    BitFieldType = BitFieldType.HexString,
                    Length = 16,
                    Comment =
                        "Validity1 bits\r\n0 = not used (set to invalid)\r\n1 = MMI_OBU_TR_EBTestInProgress\r\n2 = MMI_OBU_TR_EB_Status\r\n3 = MMI_OBU_TR_RadioStatus" +
                        "\r\n4 = MMI_OBU_TR_STM_HS_ENABLED\r\n5 = MMI_OBU_TR_STM_DA_ENABLED\r\n6..7 not used (set to invalid) \r\n8 = MMI_OBU_TR_BrakeTest_Status" +
                        "\r\n9..11 not used (set to invalid) \r\n12 = MMI_OBU_TR_M_Level\r\n13..15 not used (set to invalid) "
                },
                new BitField
                {
                    Name = "EVC7_Validity2",
                    BitFieldType = BitFieldType.HexString,
                    Length = 16,
                    Comment =
                        "Validity2 bits\r\n0 = MMI_OBU_TR_M_Mode\r\n1 = MMI_OBU_TR_M_ADHESION\r\n2 = MMI_OBU_TR_NID_STM_HS\r\n3 = MMI_OBU_TR_NID_STM_DA" +
                        "\r\n4 = MMI_OBU_TR_BrakeTestTimeOut\r\n5 = MMI_OBU_TR_O_TRAIN\r\n6..15 not used (set to invalid) "
                },
                SSW1,
                SSW2,
                SSW3,
                new BitField
                {
                    NestedDataSet = SDT_Trailer,
                    Length = 1
                }
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
                    LookupTable = new LookupTable
                    {
                        {"False", "Auxiliary Information"},
                        {"True", "Important Information"}
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
                    LookupTable = new LookupTable
                    {
                        {"0", "Add text/symbol with ack prompt, to be kept after ack"},
                        {"1", "Add text/symbol with ack prompt, to be removed after ack"},
                        {"2", "Add text with ack/nak prompt, to be removed after ack/nak"},
                        {"3", "Add informative text/symbol"},
                        {"4", "Remove text/symbol. Text/symbol to be removed is defined by MMI_I_TEXT."},
                        {"5", "Text still incomplete. Another instance of EVC-8 follows."}
                    }
                },
                MMI_I_TEXT,
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
                                            VariableLengthSettings = new VariableLengthSettings
                                                {Name = "MMI_N_CAPTION_TRAINSET_", ScalingFactor = 8},
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

        // checked RVV 22-11-2019 2.13
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
                MMI_L_STFF,
                MMI_V_STFF,
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
                            MMI_M_VBC_CODE
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
                            MMI_M_VBC_CODE,
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

        // checked RVV 26-11-2019 2.13
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
                            MMI_M_VBC_CODE,
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
                MMI_N_LEVELS,
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
                            MMI_Q_LEVEL_NTC_ID,
                            new BitField
                            {
                                Name = "MMI_M_CURRENT_LEVEL",
                                BitFieldType = BitFieldType.Bool,
                                Length = 1,
                                Comment = "Indicates if MMI_M_LEVEL_STM_ID is the latest used level",
                                SkipIfValue = false
                            },
                            MMI_M_LEVEL_FLAG,
                            MMI_M_INHIBITED_LEVEL,
                            MMI_M_INHIBITED_ENABLE,
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
                            MMI_M_LEVEL_NTC_ID
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
                    LookupTable = new LookupTable {{"65535", "no LSSMA displayed"}}
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
                    LookupTable = new LookupTable {{"0", "Unknown"}}
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
                              "\r\n11: ATO service enabled" +
                              "\r\n12: CMD installed" +
                              "\r\n13..15: Spare"
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
                                LookupTable = new LookupTable
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

        // checked RVV 22-11-2021 2.16
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
                    Comment = "Need for driver intervention or not during Specific STM Data Entry.",
                    LookupTable = new LookupTable
                    {
                        {"False", "No driver intervention requested"},
                        {"True", "Driver intervention requested"}
                    }
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
                                    "Maximum iteration data pick-up list (range: 0-16)." +
                                    "\r\nNote: Higher values are allowed, if a reduced size of caption text is used but limited by the maximum message length.",
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

        // checked RVV 22-11-2021 2.16
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

        // checked RVV 22-11-2021 2.16
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

        // checked RVV 22-11-2021 2.16
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
                            MMI_M_VBC_CODE_
                        }
                    }
                }
            }
        };

        // checked RVV 22-11-2021 2.16
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
                            MMI_M_VBC_CODE_
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
                    Comment = "Enable Start button"
                },
                new BitField
                {
                    Name = "Driver ID",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Driver ID button"
                },
                new BitField
                {
                    Name = "Train data",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Train Data button"
                },
                new BitField
                {
                    Name = "Level",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Level button"
                },
                new BitField
                {
                    Name = "Train running number",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable TRN button"
                },
                new BitField
                {
                    Name = "Shunting",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Shunting button"
                },
                new BitField
                {
                    Name = "Exit Shunting",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Exit Shunting button"
                },
                new BitField
                {
                    Name = "Non-Leading",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Non-Leading button"
                },
                new BitField
                {
                    Name = "Maintain Shunting",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Maintain Shunting button"
                },
                new BitField
                {
                    Name = "EOA",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable EOA button"
                },
                new BitField
                {
                    Name = "Adhesion",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Adhesion button"
                },
                new BitField
                {
                    Name = "SR speed / distance",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable SR speed/distance button"
                },
                new BitField
                {
                    Name = "Train integrity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Train Integrity button"
                },
                new BitField
                {
                    Name = "Language",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Language button"
                },
                new BitField
                {
                    Name = "Volume",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Volume button"
                },
                new BitField
                {
                    Name = "Brightness",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Brightness button"
                },
                new BitField
                {
                    Name = "System version",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable System Version button"
                },
                new BitField
                {
                    Name = "Set VBC",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Set VBC button"
                },
                new BitField
                {
                    Name = "Remove VBC",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Remove VBC button"
                },
                new BitField
                {
                    Name = "Contact last RBC",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Contact Last RBC button"
                },
                new BitField
                {
                    Name = "Use short number",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Use Short Number button"
                },
                new BitField
                {
                    Name = "Enter RBC data",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Enter RBC Data button"
                },
                new BitField
                {
                    Name = "Radio Network ID",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Radio Network ID button"
                },
                new BitField
                {
                    Name = "Geographical position",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Geographical Position button"
                },
                new BitField
                {
                    Name = "End of data entry (NTC)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable End of Data Entry (NTC) button"
                },
                new BitField
                {
                    Name = "Set local time, date and offset",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Set Clock button"
                },
                new BitField
                {
                    Name = "Set local offset",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Set Local Offset button"
                },
                new BitField
                {
                    Name = "Reserved",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "reserved"
                },
                new BitField
                {
                    Name = "Start Brake Test",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Start Brake Test button"
                },
                new BitField
                {
                    Name = "Enable wheel diameter",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Wheel Diameter button"
                },
                new BitField
                {
                    Name = "Enable Doppler",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Doppler Radar button"
                },
                new BitField
                {
                    Name = "Enable brake percentage",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Enable Brake Percentage button"
                },
                new BitField
                {
                    Name = "System Info",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
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
                                LookupTable = new LookupTable
                                    {{"-2147483647", "No announcement location exists or is already passed"}}
                            },
                            new BitField
                            {
                                Name = "MMI_O_TRACKCOND_START",
                                BitFieldType = BitFieldType.Int32,
                                Length = 32,
                                Comment =
                                    "Start location of track condition. This position can be adjusted depending on supervision.",
                                LookupTable = new LookupTable
                                    {{"-2147483647", "Start location already passed"}}
                            },
                            new BitField
                            {
                                Name = "MMI_O_TRACKCOND_END",
                                BitFieldType = BitFieldType.Int32,
                                Length = 32,
                                Comment =
                                    "End location of track condition. This position can be adjusted depending on supervision.",
                                LookupTable = new LookupTable
                                    {{"-2147483647", "End location already passed"}}
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
                                LookupTable = new LookupTable
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
                                LookupTable = new LookupTable
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
                    LookupTable = new LookupTable
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
                            MMI_M_PULSE_PER_KM_2_,
                            MMI_M_PULSE_PER_KM_1_,
                            MMI_M_SDU_WHEEL_SIZE_2_,
                            MMI_M_SDU_WHEEL_SIZE_1_,
                            MMI_Q_MD_DATASET_,
                            MMI_M_WHEEL_SIZE_ERR_
                        }
                    }
                }
            }
        };

        // checked RVV 6-12-2019 DMI VSIS 1.5
        public static DataSetDefinition EVC_42 => new DataSetDefinition
        {
            Name = "EVC_42 MMI_KMC_ID_RESPONSE",
            Comment =
                "This packet shall be sent in response to a request to display or update the Home KMC ID maintenance parameter by the DMI. " +
                "This packet is used in relation with EVC-142",
            Identifiers = new List<string>
            {
                "230531340",
                "230536340",
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_T_DMILM,
                MMI_NID_HOMEKMC,
                new BitField
                {
                    Name = "MMI_M_KMC_STATUS",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Status of the Home KMC ID configuration",
                    LookupTable = new LookupTable
                    {
                        {"0", "Not configured"},
                        {"1", "Stored"},
                        {"2", "Error"}
                    }
                },
                new BitField
                {
                    Name = "evc42_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare"
                },
                new BitField
                {
                    Name = "evc42_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare"
                }
            }
        };

        // checked RVV 06-12-2019 DMI VSIS 1.5
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
                MMI_M_BP_ORIG,
                MMI_M_BP_CURRENT,
                MMI_M_BP_MEASURED
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
                            MMI_M_BP_ORIG_,
                            MMI_M_BP_CURRENT_,
                            MMI_M_BP_MEASURED_
                        }
                    }
                }
            }
        };

        // checked RVV 06-12-2019 DMI VSIS 1.5
        public static DataSetDefinition EVC_52 => new DataSetDefinition
        {
            Name = "EVC_52 MMI_SETSPEED_INFORMATION",
            Comment =
                "This packet shall be sent continuously to display the Set Speed information on the DMI." +
                "\r\nNote: This packet is routed via dedicated port and thus no header nor length information is contained in the (plain) data set.",
            Identifiers = new List<string>
            {
                "230530350",
                "230535350",
                "230530351",
                "230535351"
            },
            BitFields = new List<BitField>
            {
                MMI_T_DMILM,
                new BitField
                {
                    Name = "MMI_Q_SETSPEED_DISPLAY",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Control variable to display Set Speed information or not",
                    LookupTable = new LookupTable
                    {
                        {"0", "Do not display"},
                        {"1", "Display"}
                    }
                },
                new BitField
                {
                    Name = "EVC52-spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare for alignment"
                },
                MMI_V_SETSPEED,
                new BitField
                {
                    Name = "EVC52-spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare"
                }
            }
        };

#endregion

#region EVC-100 to EVC-155 (DMI->EVC)

        // checked RVV 01-07-2020 2.14
        public static DataSetDefinition EVC_100 => new DataSetDefinition
        {
            Name = "EVC_100 MMI_START_MMI",
            Comment =
                "This packet shall be sent as the MMI's answer to MMI_START_ATP and contains version and status information.",
            Identifiers = new List<string>
            {
                "230531000",
                "230536000"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Name = "MMI_M_IF_VER",
                    BitFieldType = BitFieldType.HexString,
                    Length = 32,
                    Comment = "MMI IF version"
                },
                new BitField
                {
                    Name = "MMI_M_SW_VER",
                    BitFieldType = BitFieldType.HexString,
                    Length = 32,
                    Comment = "MMI SW version"
                },
                new BitField
                {
                    Name = "MMI_M_START_STATUS",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "This variable indicates the meaning of the MMI_START_ATP packet",
                    LookupTable = new LookupTable
                    {
                        {"0", "'MMI is OK"},
                        {"1", "'Error"}
                    }
                }
            }
        };

        // checked RVV 21-11-2019 2.11
        public static DataSetDefinition EVC_101 => new DataSetDefinition
        {
            Name = "EVC_101 MMI_DRIVER_REQUEST",
            Comment =
                "This packet shall be sent when the driver requests for an action from the ATP, typically by pressing a button.",
            Identifiers = new List<string>
            {
                "230531010",
                "230536010"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_T_BUTTONEVENT,
                new BitField
                {
                    Name = "MMI_M_REQUEST",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Request type, one request at the time",
                    LookupTable = new LookupTable
                    {
                        {"0", "Spare"},
                        {"1", "Start Shunting"},
                        {"2", "Exit Shunting"},
                        {"3", "Start Train Data Entry"},
                        {"4", "Exit Train Data Entry"},
                        {"5", "Start Non-Leading"},
                        {"6", "Exit Non-Leading"},
                        {"7", "Start Override EOA (Pass stop)"},
                        {"8", "Geographical position request"},
                        {"9", "Start"},
                        {"10", "Restore adhesion coefficient to 'non-slippery rail'"},
                        {"11", "Set adhesion coefficient to 'slippery rail'"},
                        {"12", "Exit Change SR rules"},
                        {"13", "Change SR rules"},
                        {"14", "Continue shunting on desk closure"},
                        {"15", "Spare"},
                        {"16", "Spare"},
                        {"17", "Spare"},
                        {"18", "Spare"},
                        {"19", "Spare"},
                        {"20", "Change Driver identity"},
                        {"21", "Start Train Data View"},
                        {"22", "Start Brake Test"},
                        {"23", "Start Set VBC"},
                        {"24", "Start Remove VBC"},
                        {"25", "Exit Set VBC"},
                        {"26", "Exit Remove VBC"},
                        {"27", "Change Level (or inhibit status)"},
                        {"28", "Start RBC Data Entry"},
                        {"29", "System Info request"},
                        {"30", "Change Train Running Number"},
                        {"31", "Exit Change Train Running Number"},
                        {"32", "Exit Change Level (or inhibit status)"},
                        {"33", "Exit RBC Data Entry"},
                        {"34", "Exit Driver Data Entry"},
                        {"35", "Spare"},
                        {"36", "Spare"},
                        {"37", "Spare"},
                        {"38", "Start procedure 'Train Integrity'"},
                        {"39", "Exit RBC contact"},
                        {"40", "Level entered"},
                        {"41", "start NTC 1 data entry"},
                        {"42", "start NTC 2 data entry"},
                        {"43", "start NTC 3 data entry"},
                        {"44", "start NTC 4 data entry"},
                        {"45", "start NTC 5 data entry"},
                        {"46", "start NTC 6 data entry"},
                        {"47", "start NTC 7 data entry"},
                        {"48", "start NTC 8 data entry"},
                        {"49", "Exit NTC data entry"},
                        {"50", "Exit NTC data entry selection"},
                        {"51", "Change Brake Percentage"},
                        {"52", "Change Doppler"},
                        {"53", "Change Wheel Diameter"},
                        {"54", "Exit maintenance"},
                        {"55", "System Version request"},
                        {"56", "Start Network ID"},
                        {"57", "Contact last RBC"},
                        {"58", "Settings"},
                        {"59", "Switch"},
                        {"60", "Exit brake percentage"},
                        {"61", "Exit RBC Network ID"}
                    }
                },
                MMI_Q_BUTTON,
                new BitField
                {
                    Name = "evc101_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare for byte alignment"
                },
                new BitField
                {
                    Name = "evc101_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "Spare for byte alignment"
                },
                new BitField
                {
                    Name = "evc101_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 4,
                    Comment = "Spare for byte alignment"
                }
            }
        };

        // checked RVV 21-11-2019 2.13
        public static DataSetDefinition EVC_102 => new DataSetDefinition
        {
            Name = "EVC_102 MMI_STATUS_REPORT",
            Comment =
                "This packet shall be sent cyclically as an alive and status signal." +
                "\r\nNote: This packet is routed via dedicated port and thus no header nor length information is contained in the (plain) data set. It is also protected via SDTv2.",

            Identifiers = new List<string>
            {
                "230531020",
                "230536020",
                "230531021",
                "230536021"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "MMI_M_LX_STATUS",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    Comment = "Indicates validity of shown \"LX not protected\" text",
                    LookupTable = new LookupTable
                    {
                        {"0", "'LX not protected' text has not been requested"},
                        {"1", "'LX not protected' text has been requested and shown properly"},
                        {"2", "'LX not protected' text has been requested but not shown properly"},
                        {"3", "Unknown"}
                    }
                },
                MMI_M_ACTIVE_CABIN,
                new BitField
                {
                    Name = "MMI_M_SPEED_STATUS",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    Comment = "MMI speed data consistency",
                    LookupTable = new LookupTable
                    {
                        {"0", "Speed correctly shown"},
                        {"1", "Speed wrongly shown"},
                        {"2", "Unknown"}
                    }
                },
                new BitField
                {
                    Name = "MMI_M_MODE_STATUS",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 2,
                    Comment = "Indicates validity of shown mode",
                    LookupTable = new LookupTable
                    {
                        {"0", "Mode presentation failed"},
                        {"1", "Mode presentation passed"},
                        {"2", "Unknown"}
                    }
                },
                new BitField
                {
                    Name = "MMI_M_MMI_STATUS",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                    Comment = "The current health status of MMI",
                    LookupTable = new LookupTable
                    {
                        {"0", "Unknown"},
                        {"1", "Failure"},
                        {"2", "Idle"},
                        {"3", "Active"},
                        {"4", "spare"},
                        {"5", "ATP Down, NACK"},
                        {"6", "ATP Down, ACK"}
                    }
                },
                new BitField
                {
                    Name = "evc102_Spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 4,
                    Comment = "Spare"
                },
                new BitField
                {
                    Name = "MMI_M_MODE_READBACK",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Contains the current mode as shown on the DMI"
                },
                MMI_I_TEXT,
                new BitField
                {
                    Name = "MMI_V_NEEDLESPEED",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Train speed shown as analogue information"
                },
                new BitField
                {
                    Name = "MMI_V_DIGSPEED",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Train speed shown as digital information"
                },
                new BitField
                {
                    Name = "MMI_Q_TEXT_READBACK",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Gives back a Q_TEXT for \"LX not protected\""
                },
                new BitField
                {
                    Name = "evc102_Spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare"
                },
                new BitField
                {
                    Name = "MMI_T_DMILM",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Time stamp (EVC running time in milliseconds)"
                },
                new BitField
                {
                    Name = "evc102_Spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare"
                },
                new BitField
                {
                    Name = "evc102_Spare4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare"
                },
                new BitField
                {
                    Name = "EVC102_Validity1",
                    BitFieldType = BitFieldType.HexString,
                    Length = 16,
                    Comment =
                        "Validity1 bits" +
                        "\r\n0 = MMI_M_LX_STATUS" +
                        "\r\n1 not used (set to invalid)" +
                        "\r\n2 = MMI_M_ACTIVE_CABIN" +
                        "\r\n3 not used (set to invalid)" +
                        "\r\n4 = MMI_M_SPEED_STATUS" +
                        "\r\n5 not used (set to invalid)" +
                        "\r\n6 = MMI_M_MODE_STATUS" +
                        "\r\n7 not used (set to invalid)" +
                        "\r\n8 = MMI_M_MMI_STATUS" +
                        "\r\n9..15 not used (set to invalid)"
                },
                new BitField
                {
                    Name = "EVC102_Validity2",
                    BitFieldType = BitFieldType.HexString,
                    Length = 16,
                    Comment =
                        "Validity2 bits" +
                        "\r\n0..5 not used (set to invalid)" +
                        "\r\n6 = MMI_T_DMILM" +
                        "\r\n7..15 not used (set to invalid)"
                },
                SSW1,
                SSW2,
                SSW3,
                new BitField
                {
                    NestedDataSet = SDT_Trailer,
                    Length = 1
                }
            }
        };

        // checked RVV 22-11-2019 2.13
        public static DataSetDefinition EVC_104 => new DataSetDefinition
        {
            Name = "EVC_104 MMI_NEW_DRIVER_DATA",
            Comment = "This packet shall be sent when the driver has entered or validated driver identity number.",
            Identifiers = new List<string>
            {
                "230531030",
                "230536030",
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_X_DRIVER_ID
            }
        };

        // checked RVV 22-11-2019 2.13
        public static DataSetDefinition EVC_106 => new DataSetDefinition
        {
            Name = "EVC_106 MMI_NEW_SR_RULES",
            Comment =
                "This packet shall be sent sporadically from DMI when the driver has submitted data in the 'SR speed / distance' window.",
            Identifiers = new List<string>
            {
                "230531040",
                "230536040",
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_L_STFF,
                MMI_V_STFF,
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
                            MMI_NID_DATA
                        }
                    }
                },
                MMI_M_BUTTONS
            }
        };

        // checked RVV 22-11-2019 2.13
        public static DataSetDefinition EVC_107 => new DataSetDefinition
        {
            Name = "EVC_107 MMI_NEW_TRAIN_DATA",
            Comment =
                "This packet shall be sent when the driver acts during the Train Data Entry procedure. It covers the following use cases:" +
                "\r\n1. Driver accepts a data element by pressing 'Enter'" +
                "\r\n2. Driver accepts a data element by pressing 'Enter_Delay_Type'" +
                "\r\n3. Driver completes entering a data block by pressing 'Yes'" +
                "\r\n4. Driver overrules an operational check rule by pressing 'Delay Type Yes'",
            Identifiers = new List<string>
            {
                "230531050",
                "230536050",
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_L_TRAIN,
                MMI_V_MAXTRAIN,
                MMI_NID_KEY_TRAIN_CAT,
                MMI_M_BRAKE_PERC,
                MMI_NID_KEY_AXLE_LOAD,
                MMI_M_AIRTIGHT,
                MMI_NID_KEY_LOAD_GAUGE,
                MMI_M_TRAINSET_ID,
                MMI_M_ALT_DEM,
                new BitField
                {
                    Name = "evc107_spare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "Spare for alignment"
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
                            MMI_NID_DATA
                        }
                    }
                },
                MMI_M_BUTTONS
            }
        };

        // checked RVV 22-11-2019 2.13
        public static DataSetDefinition EVC_109 => new DataSetDefinition
        {
            Name = "EVC_109 MMI_SET_TIME_MMI",
            Comment =
                "This packet shall be sent whenever the time is changed in the MMI clock function. The content is the same as in packet 3.",
            Identifiers = new List<string>
            {
                "230531060",
                "230536060",
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_T_UTC,
                MMI_T_ZONE_OFFSET
            }
        };

        // checked RVV 22-11-2019 2.13
        public static DataSetDefinition EVC_110 => new DataSetDefinition
        {
            Name = "EVC_110 MMI_CONFIRMED_TRAIN_DATA",
            Comment =
                "This packet shall be sent when the driver has confirmed the presented train data during Train Data Validation.",
            Identifiers = new List<string>
            {
                "230531070",
                "230536070",
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
                                Name = "MMI_V_MAXTRAIN_",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 16,
                                Comment = "Maximum train speed"
                            },
                            new BitField
                            {
                                Name = "MMI_L_TRAIN_",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 16,
                                Comment = "Maximum train length"
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
                                Name = "evc110_spare",
                                BitFieldType = BitFieldType.Spare,
                                Length = 2,
                                Comment = "Spare for alignment"
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
                                Name = "MMI_NID_KEY_LOAD_GAUGE_",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment =
                                    "Loading gauge type of train (coded as MMI key according to NID_KEY) of the train. For Train Category the keys number 34 to 38 are applicable."
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
                                    "Axle load category (coded as MMI key according to NID_KEY) of the train. For Axle Load Category the keys number 21 to 33 are applicable."
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
                                    "Train category (label) according to ERA_ERTMS_15560, ch. 11.3.9.9.3. Coded as ERA 'key number' according to NID_KEY. For Train Category the keys number 3 to 20 are applicable."
                            }
                        }
                    }
                },
            }
        };

        // checked RVV 25-11-2019 2.13
        public static DataSetDefinition EVC_111 => new DataSetDefinition
        {
            Name = "EVC_111 MMI_DRIVER_MESSAGE_ACK",
            Comment =
                "This packet shall be sent as a positive/negative response, if required, on the “Driver Message” packet from the ETC.",
            Identifiers = new List<string>
            {
                "230531080",
                "230536080",
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_T_BUTTONEVENT,
                MMI_I_TEXT,
                new BitField
                {
                    Name = "MMI_Q_ACK",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 4,
                    Comment = "Logical Ack or Nack",
                    LookupTable = new LookupTable
                    {
                        {"0", "Spare"},
                        {"1", "Acknowledge / YES"},
                        {"2", "Not Acknowledge / NO"}
                    }
                },
                MMI_Q_BUTTON,
                new BitField
                {
                    Name = "evc111_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare for alignment"
                },
                new BitField
                {
                    Name = "evc111_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "Spare for alignment"
                }
            }
        };

        // checked RVV 25-11-2019 2.13
        public static DataSetDefinition EVC_112 => new DataSetDefinition
        {
            Name = "EVC_112 MMI_NEW_RBC_DATA",
            Comment =
                "This packet shall be sent when the driver has entered new RBC data. It covers the following use cases:" +
                "\r\n1.) Driver accepts a data element by pressing 'Enter'" +
                "\r\n2.) Driver accepts a data element by pressing 'Enter_Delay_Type'" +
                "\r\n3.) Driver completes entering a data block by pressing 'Yes'" +
                "\r\n4.) Driver overrules an operational check rule by pressing 'Delay Type Yes'",
            Identifiers = new List<string>
            {
                "230531090",
                "230536090",
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_NID_RBC,
                MMI_NID_RADIO,
                new BitField
                {
                    Name = "MMI_NID_MN",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Selected radio Network ID index starting from 0."
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
                            MMI_NID_DATA
                        }
                    }
                },
                MMI_M_BUTTONS
            }
        };

        // checked RVV 26-11-2019 2.13
        public static DataSetDefinition EVC_116 => new DataSetDefinition
        {
            Name = "EVC_116 MMI_NEW_TRAIN_NUMBER",
            Comment = "This packet shall be sent when the driver has entered or validated train running number. ",
            Identifiers = new List<string>
            {
                "230531100",
                "230536100",
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                Subset26.NID_OPERATIONAL
            }
        };

        // checked RVV 26-11-2019 2.13
        public static DataSetDefinition EVC_118 => new DataSetDefinition
        {
            Name = "EVC_118 MMI_NEW_SET_VBC",
            Comment =
                "This packet shall be sent sporadically from DMI when the driver has submitted data in the 'Set VBC' window.",
            Identifiers = new List<string>
            {
                "230531110",
                "230536110",
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_M_VBC_CODE,
                MMI_M_BUTTONS
            }
        };

        // checked RVV 04-12-2019 2.13
        public static DataSetDefinition EVC_119 => new DataSetDefinition
        {
            Name = "EVC_119 MMI_NEW_REMOVE_VBC",
            Comment =
                "This packet shall be sent sporadically from DMI when the driver has submitted data in the 'Remove VBC' window.",
            Identifiers = new List<string>
            {
                "230531120",
                "230536120",
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_M_VBC_CODE,
                MMI_M_BUTTONS
            }
        };

        // checked RVV 04-12-2019 2.13
        public static DataSetDefinition EVC_121 => new DataSetDefinition
        {
            Name = "EVC_121 MMI_NEW_LEVEL",
            Comment =
                "This packet shall be sent when the driver has selected an ETCS or NTC level or has changed the inhibit status of an installed level.",
            Identifiers = new List<string>
            {
                "230531130",
                "230536130",
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_N_LEVELS,
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
                            MMI_Q_LEVEL_NTC_ID,
                            MMI_M_LEVEL_FLAG,
                            MMI_M_INHIBITED_LEVEL,
                            MMI_M_INHIBITED_ENABLE,
                            new BitField
                            {
                                Name = "evc121_spare",
                                BitFieldType = BitFieldType.Spare,
                                Length = 4,
                                Comment = "Spare for alignment"
                            },
                            MMI_M_LEVEL_NTC_ID
                        }
                    }
                }
            }
        };

        // checked RVV 04-12-2019 2.13
        public static DataSetDefinition EVC_122 => new DataSetDefinition
        {
            Name = "EVC_122 MMI_NEW_LANGUAGE",
            Comment = "This packet will be sent when the driver has selected a new language with the language button.",
            Identifiers = new List<string>
            {
                "230531140",
                "230536140",
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                new BitField
                {
                    Name = "MMI_NID_LANGUAGE",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Language code",
                    LookupTable = new LookupTable {{"255", "Language Unknown"}}
                }
            }
        };

        // checked RVV 05-12-2019 2.13
        public static DataSetDefinition EVC_123 => new DataSetDefinition
        {
            Name = "EVC_123 MMI_SPECIFIC_STM_DATA_TO_STM",
            Comment =
                "This packet is used when the MMI returns specific data requested from the STM. Each MMI packet contains the data of one packet STM-180 according to SUBSET-058, which in turn holds up to 5 variables. If more than 5 variables need to be presented this MMI packet will be repeated. NID_PACKET and L_PACKET of packet STM-180 are stripped.\r\nBecause the content of this packet is given by the STM-functionality the assignment, ranges, values and meaning of all variables can only be given in the project-specific documentation.",
            Identifiers = new List<string>
            {
                "230531150",
                "230536150",
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_NID_NTC,
                new BitField
                {
                    Name = "evc123_spare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
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
                            MMI_STM_L_VALUE,
                            MMI_STM_X_VALUE
                        }
                    }
                }
            }
        };

        // checked RVV 05-12-2019 2.13
        public static DataSetDefinition EVC_128 => new DataSetDefinition
        {
            Name = "EVC_128 MMI_CONFIRMED_SET_VBC",
            Comment =
                "This packet shall be sent sporadically from DMI when the driver has confirmed data in the 'Set VBC' validation window.",
            Identifiers = new List<string>
            {
                "230531160",
                "230536160"
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
                            MMI_M_VBC_CODE_
                        }
                    }
                }
            }
        };

        // checked RVV 05-12-2019 2.13
        public static DataSetDefinition EVC_129 => new DataSetDefinition
        {
            Name = "EVC_129 MMI_CONFIRMED_REMOVE_VBC",
            Comment =
                "This packet shall be sent sporadically from DMI when the driver has confirmed data in the 'Remove VBC' validation window.",
            Identifiers = new List<string>
            {
                "230531170",
                "230536170"
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
                            MMI_M_VBC_CODE_
                        }
                    }
                }
            }
        };

        // checked RVV 05-12-2019 2.13
        public static DataSetDefinition EVC_140 => new DataSetDefinition
        {
            Name = "EVC_140 MMI_NEW_MAINTENANCE_DATA",
            Comment =
                "This packet shall be sent when the presented maintenance data have been accepted. The content is the same as in packet EVC-40, “MMI_CURRENT_MAINTENANCE_DATA”. " +
                "This packet is used in relation with packets EVC-40, EVC-41 and EVC-141.",
            Identifiers = new List<string>
            {
                "230531180",
                "230536180",
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_M_SDU_WHEEL_SIZE_1,
                MMI_M_SDU_WHEEL_SIZE_2,
                MMI_M_PULSE_PER_KM_1,
                MMI_M_PULSE_PER_KM_2,
                MMI_Q_MD_DATASET,
                MMI_M_WHEEL_SIZE_ERR
            }
        };

        // checked RVV 05-12-2019 2.13
        public static DataSetDefinition EVC_141 => new DataSetDefinition
        {
            Name = "EVC_141 MMI_CONFIRMED_MAINTENANCE_DATA",
            Comment =
                "This packet shall be sent when the presented maintenance data have been confirmed. " +
                "The content is the same as in packet EVC-41, “MMI_ECHOED_MAINTENANCE_DATA”, but bit-inverted (except for MMI_M_PACKET and MMI_L_PACKET). " +
                "This packet is used in relation with packets EVC-40, EVC-41 and EVC-140.",
            Identifiers = new List<string>
            {
                "230531190",
                "230536190",
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
                            MMI_M_PULSE_PER_KM_2_,
                            MMI_M_PULSE_PER_KM_1_,
                            MMI_M_SDU_WHEEL_SIZE_2_,
                            MMI_M_SDU_WHEEL_SIZE_1_,
                            MMI_M_WHEEL_SIZE_ERR_,
                            MMI_Q_MD_DATASET_
                        }
                    }
                }
            }
        };

        // checked RVV 06-12-2019 DMI VSIS 1.5
        public static DataSetDefinition EVC_142 => new DataSetDefinition
        {
            Name = "EVC_142 MMI_KMC_ID_REQUEST",
            Comment =
                "This packet shall be sent when a request to display or update the Home KMC ID maintenance parameter is initiated by the DMI." +
                "This packet is used in relation with EVC-42.",
            Identifiers = new List<string>
            {
                "230531240",
                "230536240",
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_T_DMILM,
                MMI_NID_HOMEKMC
            }
        };

        // checked RVV 6-12-2019 DMI VSIS 1.5
        public static DataSetDefinition EVC_150 => new DataSetDefinition
        {
            Name = "EVC_150 MMI_NEW_BRAKE_PERCENTAGE",
            Comment =
                "This packet shall be sent when the presented brake percentage data have been accepted. The content is the same as in packet EVC-50, “MMI_CURRENT_BRAKE_PERCENTAGE”. " +
                "This packet is used in relation with packets EVC-50, EVC-51 and EVC-151.",
            Identifiers =
            {
                "230531200",
                "230536200"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_M_BP_ORIG,
                MMI_M_BP_CURRENT,
                MMI_M_BP_MEASURED
            }
        };

        // checked RVV 06-12-2019 DMI VSIS 1.5
        public static DataSetDefinition EVC_151 => new DataSetDefinition
        {
            Name = "EVC_151 MMI_CONFIRMED_BRAKE_PERCENTAGE",
            Comment =
                "This packet shall be sent when the presented brake percentage data have been confirmed. The content is the same as in packet EVC-51, “MMI_ECHOED_BRAKE_PERCENTAGE”. " +
                "This packet is used in relation with packets EVC-50, EVC-51 and EVC-150." +
                "\r\nNote: All variables in this packet (Exception: MMI_M_PACKET and MMI_L_PACKET) shall be the same as in packet EVC-50 but bit-inverted. Their names shall end with “_”.",
            Identifiers =
            {
                "230531210",
                "230536210"
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
                            MMI_M_BP_MEASURED_,
                            MMI_M_BP_CURRENT_,
                            MMI_M_BP_ORIG_
                        }
                    }
                }
            }
        };

        // checked RVV 15-11-2021 2.16
        public static DataSetDefinition EVC_152 => new DataSetDefinition
        {
            Name = "EVC_152 MMI_DRIVER_ACTION",
            Comment = "This packet shall be sent when the corresponding driver action is performed." +
                      "\r\nThe data is used by ETC to record the driver actions in JRU.",
            Identifiers =
            {
                "230531220",
                "230536220"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_T_DMILM,
                new BitField
                {
                    Name = "MMI_M_DRIVER_ACTION",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Driver action",
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
                        {"12", "Non-Leading selected"},
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
                        {"31", "spare"},
                        {"32", "spare"},
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

        // checked RVV 16-11-2021 2.16
		// updated by PF 09-03-2023 to correct issues
        public static DataSetDefinition EVC_153 => new DataSetDefinition
        {
            Name = "EVC_153 MMI_DISPLAY_STATUS_JRU",
            Comment =
                "This packet shall be sent when the information displayed to the driver has changed. The data is used by the ETC to record the DMI status in the JRU.",
            Identifiers =
            {
                "230531230",
                "230536230"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_T_DMILM,

                // DMI Symbol Status as per Subset 27
                #region MMI_M_SYMB_STATUS_1
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
                #endregion

                #region MMI_M_SYMB_STATUS_2
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
                #endregion

                #region MMI_M_SYMB_STATUS_3
                // Need 9 bits at the end of MMI_M_SYMB_STATUS_3 as these aren't used.
                new BitField
                {
                    Name = "MMI_M_SYMB_STATUS_3_spare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 9,
                    Comment = "Spare for alignment"
                },
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
                #endregion

                // DMI System Status as per Subset27
                #region MMI_M_SYSTEM_STATUS
                new BitField 
                {
                    Name = "DMI_SYSTEM_STATUS_spare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 9,
                    Comment = "Spare for alignment"
                },
                new BitField {Name = "Radio network registration failed", BitFieldType = BitFieldType.Bool, Length = 1,SkipIfValue = false},
                new BitField {Name = "Route unsuitable – traction system", BitFieldType = BitFieldType.Bool, Length = 1,SkipIfValue = false},
                new BitField {Name = "Route unsuitable – loading gauge", BitFieldType = BitFieldType.Bool, Length = 1,SkipIfValue = false},
                new BitField {Name = "Route unsuitable – axle load category", BitFieldType = BitFieldType.Bool, Length = 1,SkipIfValue = false},
                new BitField {Name = "No track description", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "RV distance exceeded", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "Emergency stop", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "SR stop order", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "SH stop order", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "SR distance exceeded", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "No MA received at level transition", BitFieldType = BitFieldType.Bool, Length = 1,SkipIfValue = false},
                new BitField {Name = "Unauthorized passing of EOA / LOA", BitFieldType = BitFieldType.Bool, Length = 1,SkipIfValue = false},
                new BitField {Name = "Train is rejected", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "Train data changed", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "Trackside not compatible", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "SH request failed", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "SH refused", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "Runaway movement", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "Entering OS", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "Entering FS", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "Communication error", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "Trackside malfunction", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "Balise read error", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                #endregion

                new BitField
                {
                    Name = "evc153_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare for alignment"
                },

                new BitField
                {
                    Name = "evc153_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare for alignment"
                },

                new BitField
                {
                    Name = "MMI_M_SOUND_STATUS_spare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 5,
                    Comment = "Spare for alignment"
                },
                new BitField {Name = "Sound S2 Warning", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "Sound S1 Over-speed", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "Sound Sinfo", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},

                // MMI_Q_DISPLAY_CHANGE
                new BitField {Name = "MMI_M_SYMB_STATUS changed", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MMI_M_SYSTEM_STATUS changed", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "2", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "3", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "MMI_M_SOUND_STATUS changed", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "5", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "6", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false},
                new BitField {Name = "7", BitFieldType = BitFieldType.Bool, Length = 1, SkipIfValue = false}
            }
        };

        // checked RVV 16-11-2021 2.16
        public static DataSetDefinition EVC_154 => new DataSetDefinition
        {
            Name = "EVC_154 MMI_SPEED_DISTANCE_INFORMATION_JRU",
            Comment =
                "This packet shall be sent periodically (process data) and contains all information necessary to create the " +
            "JRU messages 20 (Speed and Distance Monitoring Information) and 44 (LSSMA)." +
            "Note: Size is 32 Bytes, cycle time is 512 ms.",
            Identifiers =
            {
                "230531250",
                "230536250"
            },
            BitFields = new List<BitField>
            {
                MMI_T_DMILM,
                new BitField
                {
                    Name = "MMI_M_SDMTYPE",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Type of the speed and distance monitoring. Based on information provided in EVC-1 MMI_M_WARNING",
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
                    Name = "MMI_M_SDMSUPSTAT",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Supervision status of the speed and distance monitoring. " +
                                "Based on information provided in EVC-1 MMI_M_WARNING",
                    LookupTable = new LookupTable
                    {
                        {"0", "Normal status (NoS)"},
                        {"1", "Indication status (IndS)"},
                        {"2", "Overspeed status (OvS)"},
                        {"3", "Warning status (WaS)"},
                        {"4", "Intervention status (IntS)"}
                    }
                },
                new BitField
                {
                    Name = "MMI_V_PERM_JRU",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Permitted speed displayed to the driver. " +
                                "Based on information provided in EVC-1 MMI_V_PERMITTED",
                    LookupTable = new LookupTable
                    {
                        {"1023", "None"}
                    },
                    AppendString = " km/h"
                },
                new BitField
                {
                    Name = "MMI_V_SBI_JRU",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "SBI speed displayed to the driver. " +
                                "Based on information provided in EVC-1 MMI_V_INTERVENTION",
                    LookupTable = new LookupTable
                    {
                        {"1023", "None"}
                    },
                    AppendString = " km/h"
                },
                new BitField
                {
                    Name = "MMI_V_TARGET_JRU",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Target speed displayed to the driver. " +
                                "Based on information provided in EVC-1 MMI_V_TARGET",
                    LookupTable = new LookupTable
                    {
                        {"1023", "None"}
                    },
                    AppendString = " km/h"
                },
                new BitField
                {
                    Name = "MMI_D_TARGET_JRU",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Target distance displayed to the driver. " +
                                "Based on information provided in EVC-1 MMI_O_BRAKETARGET",
                    LookupTable = new LookupTable
                    {
                        {"32767", "None"}
                    },
                    AppendString = " m"
                },
                new BitField
                {
                    Name = "MMI_V_RELEASE_JRU",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Release speed displayed to the driver. " +
                                "Based on information provided in EVC-1 MMI_V_RELEASE",
                    LookupTable = new LookupTable
                    {
                        {"1023", "None"}
                    },
                    AppendString = " km/h"
                },
                new BitField
                {
                    Name = "evc154_spare_1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare (MMI_M_TTI in P8e / BL3R2)"
                    //new BitField
                        //{
                        //    Name = "MMI_M_TTI",
                        //    BitFieldType = BitFieldType.UInt8,
                        //    Length = 8,
                        //    Comment = "Time to Indication square size on DMI",
                        //    LookupTable = new LookupTable
                        //    {
                        //        {"0", "0 cells"},
                        //        {"1", "5 x 5 cells"},
                        //        {"2", "10 x 10 cells"},
                        //        {"3", "15 x 15 cells"},
                        //        {"4", "20 x 20 cells"},
                        //        {"5", "25 x 25 cells"},
                        //        {"6", "30 x 30 cells"},
                        //        {"7", "35 x 35 cells"},
                        //        {"8", "40 x 40 cells"},
                        //        {"9", "45 x 45 cells"},
                        //        {"10", "50 x 50 cells"},
                        //    }
                        //},
                },
                new BitField
                {
                    Name = "evc154_spare_2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare for alignment"
                },
                new BitField
                {
                    Name = "MMI_V_LSSMA_JRU",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "LSSMA speed displayed to the driver. " +
                                "Based on information provided in EVC-23 MMI_V_LSSMA",
                    LookupTable = new LookupTable
                    {
                        {"1023", "None"}
                    },
                    AppendString = " km/h"
                },
                new BitField
                {
                    Name = "evc154_spare_3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare (MMI_V_SETSPEED in P8e / BL3R2)"
                        //MMI_V_SETSPEED
                },
                new BitField
                {
                    Name = "evc154_spare_4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare for alignment"
                },
                new BitField
                {
                    Name = "evc154_spare_5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare for alignment"
                },
                new BitField
                {
                    Name = "evc154_spare_6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare for alignment"
                },
            }
        };

        // checked RVV 22-11-2021 2.16
        public static DataSetDefinition EVC_155 => new DataSetDefinition
        {
            Name = "EVC_155 MMI_TEXT_MESSAGE_STATUS_JRU",
            Comment =
                "This packet is used to inform the ETC when text messages are shown or removed on the DMI and contains all information necessary to create the " +
            "JRU messages 16, 17, 18 and 19 (Start/Stop displaying Fixed/Plain text messages). To record JRU messages 16 and 17(Fixed text), only MMI_Q_TEXT is needed. " +
            "To record JRU messages 18 and 19 (Plain text), only MMI_I_TEXT is needed." +
            "" +
            "Note: The full text needs to be recorded in JRU Messages 18 and 19. However, these plain text messages can be very large " +
            "(up to 255 characters) and such a transfer on the GPP may take several seconds. " +
            "The strategy to reduce the load on the GPP is to identify each plain text message by the identifier MMI_I_TEXT and cache it in the ETC. " +
            "The DMI can report back only this identifier.",
            Identifiers =
            {
                "230531260",
                "230536260"
            },
            BitFields = new List<BitField>
            {
                MMI_M_PACKET,
                MMI_L_PACKET,
                MMI_T_DMILM,
                new BitField
                {
                    Name = "MMI_N_TEXT_MESSAGES",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Number of text messages [0..50]"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "MMI_N_TEXT_MESSAGES"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            MMI_Q_TEXT,
                            MMI_I_TEXT,
                            new BitField
                            {
                                Name = "MMI_Q_TEXT_MESSAGE_STATUS",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Status of Text Message",
                                LookupTable = new LookupTable
                                {
                                    {"0", "Start showing Text Message to Driver"},
                                    {"1", "Stop displaying Text Message to Driver"},
                                    {"2", "Stop displaying Text Message to Driver, that was never shown"}
                                }
                            }
                        }
                    }
                }                
            }
        };

        #endregion

        #region Safe Status Words

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
                        Comment = "This value should be 1 for all attributes, 0 is Reserved",
                        SkipIfValue = false
                    },
                    new BitField
                    {
                        Name = "Flashing",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                        LookupTable = new LookupTable
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
                        LookupTable = new LookupTable
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
                        LookupTable = new LookupTable
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
                        LookupTable = new LookupTable
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
                    },
                    new BitField
                    {
                        Name = "MMI_EVC_M_XATTRIBUTE_Spare",
                        BitFieldType = BitFieldType.Spare,
                        Length = 6,
                        Comment = "Spare for alignment"
                    }
                }
            }
        };

        public static BitField MMI_I_TEXT => new BitField
        {
            Name = "MMI_I_TEXT",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment =
                "The identifier of the transmitted text. Used to identify the text for addressing, acknowledgment and removing."
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

        public static BitField MMI_L_STFF => new BitField
        {
            Name = "MMI_L_STFF",
            BitFieldType = BitFieldType.UInt32,
            Length = 32,
            Comment = "Max distance in Staff responsible",
            AppendString = " m"
        };

        public static BitField MMI_L_TRAIN => new BitField
        {
            Name = "MMI_L_TRAIN",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Maximum train length",
            LookupTable = new LookupTable {{"0", "'No default value' => Data field shall remain empty"}}
        };

        public static BitField MMI_M_ACTIVE_CABIN => new BitField
        {
            Name = "MMI_M_ACTIVE_CABIN",
            BitFieldType = BitFieldType.UInt8,
            Length = 2,
            Comment = "Defines the identity of the activated cabin",
            LookupTable = new LookupTable
            {
                {"0", "No cabin is active"},
                {"1", "Cabin 1 is active"},
                {"2", "Cabin 2 is active"},
                {"3", "Spare"}
            }
        };

        public static BitField MMI_M_AIRTIGHT => new BitField
        {
            Name = "MMI_M_AIRTIGHT",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Train equipped with airtight system",
            LookupTable = new LookupTable
            {
                {"0", "Not equipped"},
                {"1", "Equipped"},
                {"2", "'No default value' => TD entry field shall remain empty"}
            }
        };

        public static BitField MMI_M_ALT_DEM => new BitField
        {
            Name = "MMI_M_ALT_DEM",
            BitFieldType = BitFieldType.UInt8,
            Length = 2,
            Comment = "Control variable for alternative train data entry method",
            LookupTable = new LookupTable
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
        };

        public static BitField MMI_M_BP_CURRENT => new BitField
        {
            Name = "MMI_M_BP_CURRENT",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Currently used brake percentage",
            LookupTable = new LookupTable
            {
                {"251", "Technical Range Check failed"},
                {"255", "Original value exceeded (will be displayed as '++++' in grey, Data Field 'Current BP')"}
            },
            AppendString = " %"
        };

        public static BitField MMI_M_BP_CURRENT_ => new BitField
        {
            Name = "MMI_M_BP_CURRENT_",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Currently used brake percentage. Bit-inverted value",
            LookupTable = new LookupTable
            {
                {"251", "Technical Range Check failed"},
                {"255", "Original value exceeded (will be displayed as '++++' in grey, Data Field 'Current BP')"}
            },
            AppendString = " %"
        };

        public static BitField MMI_M_BP_MEASURED => new BitField
        {
            Name = "MMI_M_BP_MEASURED",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Last measured brake percentage. Range 10...250",
            LookupTable = new LookupTable
            {
                {
                    "255",
                    "No last measured brake percentage available, i.e. this will be displayed as '_ _ _ _' in grey in Data Field 'Last measured BP'."
                }
            },
            AppendString = " %"
        };

        public static BitField MMI_M_BP_MEASURED_ => new BitField
        {
            Name = "MMI_M_BP_MEASURED_",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Last measured brake percentage. Range 10...250. Bit-inverted value.",
            LookupTable = new LookupTable
            {
                {
                    "255",
                    "No last measured brake percentage available, i.e. this will be displayed as '_ _ _ _' in grey in Data Field 'Last measured BP'."
                }
            },
            AppendString = " %"
        };

        public static BitField MMI_M_BP_ORIG => new BitField
        {
            Name = "MMI_M_BP_ORIG",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Original brake percentage (from Train Data Entry procedure)",
            AppendString = " %"
        };

        public static BitField MMI_M_BP_ORIG_ => new BitField
        {
            Name = "MMI_M_BP_ORIG_",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Original brake percentage (from Train Data Entry procedure). Bit-inverted value.",
            AppendString = " %"
        };

        public static BitField MMI_M_BRAKE_PERC => new BitField
        {
            Name = "MMI_M_BRAKE_PERC",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Brake percentage",
            LookupTable = new LookupTable {{"0", "'No default value' => Data field shall remain empty"}},
            AppendString = " %"
        };

        public static BitField MMI_M_BUTTONS => new BitField
        {
            Name = "MMI_M_BUTTONS",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Identifier of MMI Buttons",
            LookupTable = new LookupTable
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
            Comment = "A bit mask that, for each variable, tells if a data value is enabled (e.g. for 'edit' in EVC-6). 1== 'enabled'." +
            "The variable supports the following use cases:" +
            "1.) Controls edit ability of related data object during TDE procedure(EVC - 6, no data view)." +
            "2.) In case of a Train Data View procedure this variable controls visibility of data items(ERA_ERTMS_015560, v3.4.0, chapter 11.5.1.5)." +
            "3.) In packet EVC - 10 this variable controls highlighting of changed data items(ERA_ERTMS_015560, v3.4.0, chapter 11.4.1.4, 10.3.3.5).",
            NestedDataSet = new DataSetDefinition
            {
                BitFields = new List<BitField>
                {
                    new BitField
                    {
                        Name = "Train Set ID",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                    },
                    new BitField
                    {
                        Name = "Train Category",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                    },
                    new BitField
                    {
                        Name = "Train Length",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                    },
                    new BitField
                    {
                        Name = "Brake Percentage",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                    },
                    new BitField
                    {
                        Name = "Max. Train Speed",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                    },
                    new BitField
                    {
                        Name = "Axle Load Category",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                    },
                    new BitField
                    {
                        Name = "Airtightness",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                    },
                    new BitField
                    {
                        Name = "Loading Gauge",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                    },
                    new BitField
                    {
                        Name = "Highlight Train Set ID",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                    },
                    new BitField
                    {
                        Name = "Highlight Train Category",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                    },
                    new BitField
                    {
                        Name = "Highlight Train Length",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                    },
                    new BitField
                    {
                        Name = "Highlight Brake Percentage",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                    },
                    new BitField
                    {
                        Name = "Highlight Max. Train Speed",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                    },
                    new BitField
                    {
                        Name = "Highlight Axle Load Category",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                    },
                    new BitField
                    {
                        Name = "Highlight Airtightness",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                    },
                    new BitField
                    {
                        Name = "Highlight Loading Gauge",
                        BitFieldType = BitFieldType.Bool,
                        Length = 1,
                    }
                }
            }
        };

        public static BitField MMI_M_INHIBITED_ENABLE => new BitField
        {
            Name = "MMI_M_INHIBIT_ENABLE",
            BitFieldType = BitFieldType.Bool,
            Length = 1,
            Comment = "Indicates if MMI_M_LEVEL_NTC_ID is allowed (configurable) for inhibiting or not",
            SkipIfValue = false
        };

        public static BitField MMI_M_INHIBITED_LEVEL => new BitField
        {
            Name = "MMI_M_INHIBITED_LEVEL",
            BitFieldType = BitFieldType.Bool,
            Length = 1,
            Comment = "Indicates if MMI_M_LEVEL_NTC_ID is currently inhibited by driver or not",
            SkipIfValue = false
        };

        public static BitField MMI_M_LEVEL_FLAG => new BitField
        {
            Name = "MMI_M_LEVEL_FLAG",
            BitFieldType = BitFieldType.Bool,
            Length = 1,
            Comment = "EVC-20: Marker to indicate if a level button is enabled or disabled." +
                      "\r\nEVC-121: Marker to indicate if the driver has selected the level.",
            LookupTable = new LookupTable
            {
                {"True", "Enabled"},
                {"False", "Disabled"}
            }
        };

        public static BitField MMI_M_LEVEL_NTC_ID => new BitField
        {
            Name = "MMI_M_LEVEL_NTC_ID",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Level number or NTC ID",
            LookupTable = new LookupTable
            {
                {"0", "Level 0"},
                {"1", "Level 1"},
                {"2", "Level 2"},
                {"3", "Level 3"},
                {"20", "TPWS"},
                {"21", "TPWS Fixed"},
                {"50", "CBTC"}
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
            LookupTable = new LookupTable
            {
                {"0", "No Radar 1 on board"},
                {"4294967290", "Technical Range Check failed"},
                {"4294967291", "Technical Resolution Check failed"},
                {"4294967292", "Technical Cross Check failed"},
                {"4294967293", "Operational Range Check failed"},
                {"4294967294", "Operational Cross Check failed"}
            }
        };

        public static BitField MMI_M_PULSE_PER_KM_1_ => new BitField
        {
            Name = "MMI_M_PULSE_PER_KM_1_",
            BitFieldType = BitFieldType.UInt32,
            Length = 32,
            Comment = "Number of pulses per km (Radar 1). Value is bit-inverted",
            LookupTable = new LookupTable
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
            LookupTable = new LookupTable
            {
                {"0", "No Radar 2 on board"},
                {"4294967290", "Technical Range Check failed"},
                {"4294967291", "Technical Resolution Check failed"},
                {"4294967292", "Technical Cross Check failed"},
                {"4294967293", "Operational Range Check failed"},
                {"4294967294", "Operational Cross Check failed"}
            }
        };

        public static BitField MMI_M_PULSE_PER_KM_2_ => new BitField
        {
            Name = "MMI_M_PULSE_PER_KM_2_",
            BitFieldType = BitFieldType.UInt32,
            Length = 32,
            Comment = "Number of pulses per km (Radar 2). Value is bit-inverted",
            LookupTable = new LookupTable
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
            LookupTable = new LookupTable
            {
                {"65530", "Technical Range Check failed"},
                {"65531", "Technical Resolution Check failed"},
                {"65532", "Technical Cross Check failed"},
                {"65533", "Operational Range Check failed"},
                {"65534", "Operational Cross Check failed"}
            }
        };

        public static BitField MMI_M_SDU_WHEEL_SIZE_1_ => new BitField
        {
            Name = "MMI_M_SDU_WHEEL_SIZE_1_",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Wheel Diameter (Tacho 1). Value is bit-inverted.",
            LookupTable = new LookupTable
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
            LookupTable = new LookupTable
            {
                {"65530", "Technical Range Check failed"},
                {"65531", "Technical Resolution Check failed"},
                {"65532", "Technical Cross Check failed"},
                {"65533", "Operational Range Check failed"},
                {"65534", "Operational Cross Check failed"}
            }
        };

        public static BitField MMI_M_SDU_WHEEL_SIZE_2_ => new BitField
        {
            Name = "MMI_M_SDU_WHEEL_SIZE_2_",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Wheel Diameter (Tacho 2). Value is bit-inverted",
            LookupTable = new LookupTable
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
            LookupTable = new LookupTable
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

        public static BitField MMI_M_TRAINSET_ID => new BitField
        {
            Name = "MMI_M_TRAINSET_ID",
            BitFieldType = BitFieldType.UInt8,
            Length = 4,
            Comment = "ID of preconfigured train data set",
            LookupTable = new LookupTable
            {
                {"0", "Train data entry method by train data set is not selected --> use 'flexible TDE'"},
                {"15", "no Train data set specified"}
            }
        };

        public static BitField MMI_M_VBC_CODE => new BitField
        {
            Name = "MMI_M_VBC_CODE",
            BitFieldType = BitFieldType.UInt32,
            Length = 32,
            Comment = "VBC Identifier"
        };

        public static BitField MMI_M_VBC_CODE_ => new BitField
        {
            Name = "MMI_M_VBC_CODE_",
            BitFieldType = BitFieldType.UInt32,
            Length = 32,
            Comment = "VBC Identifier. Bit inverted value of EVC-118 or EVC-119"
        };

        public static BitField MMI_M_WHEEL_SIZE_ERR => new BitField
        {
            Name = "MMI_M_WHEEL_SIZE_ERR",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Wheel Size Accuracy (maximum 32)",
            LookupTable = new LookupTable
            {
                {"250", "Technical Range Check failed"},
                {"251", "Technical Resolution Check failed"},
                {"252", "Technical Cross Check failed"},
                {"253", "Operational Range Check failed"},
                {"254", "Operational Cross Check failed"}
            }
        };

        public static BitField MMI_M_WHEEL_SIZE_ERR_ => new BitField
        {
            Name = "MMI_M_WHEEL_SIZE_ERR_",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Wheel Size Accuracy (maximum 32). Value is bit-inverted.",
            LookupTable = new LookupTable
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
            Comment = "Number of data elements" +
                      "\r\nEVC-6 range: 0-9" +
                      "\r\nEVC-11 & EVC-106 range: 0-2" +
                      "\r\nEVC 22, 107, 112 range: 0-8"
        };

        public static BitField MMI_N_ITER => new BitField
        {
            Name = "MMI_N_ITER",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Maximum iteration data (range: 0-5)."
        };

        public static BitField MMI_N_LEVELS => new BitField
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
            LookupTable = new LookupTable
                {{"0", "Cancel presentation of previous MMI_SELECT_LEVEL (if still shown on the MMI)."}}
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
            LookupTable = new LookupTable
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

        public static BitField MMI_NID_HOMEKMC => new BitField
        {
            Name = "MMI_NID_HOMEKMC",
            BitFieldType = BitFieldType.UInt32,
            Length = 32,
            Comment = "Home KMC ID number, provided by ETCS Onboard on request of the ETCS-MMI",
            LookupTable = new LookupTable {{"0", "Unknown"}}
        };

        public static BitField MMI_NID_KEY_AXLE_LOAD => new BitField
        {
            Name = "MMI_NID_KEY_AXLE_LOAD",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment =
                "Axle load category (coded as MMI key according to NID_KEY) of the train. " +
                "For Axle Load Category the keys number 21 to 33 are applicable. \"No dedicated key\" may be used for \"entry data entry field\".",
            LookupTable = new LookupTable {{"0", "'No default value' => Data field shall remain empty"}}
        };

        public static BitField MMI_NID_KEY_LOAD_GAUGE => new BitField
        {
            Name = "MMI_NID_KEY_LOAD_GAUGE",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment =
                "Loading gauge type of train (coded as MMI key according to NID_KEY) of the train. For Train Category the keys number 34 to 38 are applicable.",
            LookupTable = new LookupTable {{"0", "'No default value' => Data field shall remain empty"}}
        };

        public static BitField MMI_NID_KEY_TRAIN_CAT => new BitField
        {
            Name = "MMI_NID_KEY_TRAIN_CAT",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment =
                "Train category (label, coded as MMI_NID_KEY) according to ERA_ERTMS_15560, ch. 11.3.9.9.3. Coded as ERA 'key number' according to NID_KEY. " +
                "For Train Category the keys number 3 to 20 are applicable. \"No dedicated key\" may be used for \"entry data entry field\".",
            LookupTable = new LookupTable {{"0", "'No default value' => Data field shall remain empty"}}
        };

        public static BitField MMI_NID_NTC => new BitField
        {
            Name = "MMI_NID_NTC",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Identity of the NTC",
            LookupTable = STM_LookupTable
        };

        public static BitField MMI_NID_NTC2 => new BitField
        {
            Name = "MMI_NID_NTC",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment =
                "STM identity used to point to the corresponding palette of Specific STM Data variables. " +
                "This NID_STM may be different from the one in the message header as the STM is allowed to re-use Specific STM data from another STM.",
            LookupTable = STM_LookupTable
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
            LookupTable = new LookupTable
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

        public static BitField MMI_Q_BUTTON => new BitField
        {
            Name = "MMI_Q_BUTTON",
            BitFieldType = BitFieldType.UInt8,
            Length = 1,
            Comment =
                "Button Event (pressed or released)",
            LookupTable = new LookupTable
            {
                {"0", "Released"},
                {"1", "Pressed"}
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
            LookupTable = new LookupTable
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
            LookupTable = new LookupTable
            {
                {"0", "All checks have passed"},
                {"1", "Technical Range Check failed"},
                {"2", "Technical Resolution Check failed"},
                {"3", "Technical Cross Check failed"},
                {"4", "Operational Range Check failed"},
                {"5", "Operational Cross Check failed"}
            }
        };

        public static BitField MMI_Q_LEVEL_NTC_ID => new BitField
        {
            Name = "MMI_Q_LEVEL_NTC_ID",
            BitFieldType = BitFieldType.UInt8,
            Length = 1,
            Comment = "Qualifier for the variable MMI_M_LEVEL_NTC_ID",
            LookupTable = new LookupTable
            {
                {"0", "MMI_M_LEVEL_NTC_ID contains an STM ID (0-255)"},
                {"1", "MMI_M_LEVEL_NTC_ID contains a level number (0-3)"}
            }
        };

        public static BitField MMI_Q_MD_DATASET => new BitField
        {
            Name = "MMI_Q_MD_DATASET",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Indicates the content of the maintenance telegram",
            LookupTable = new LookupTable
            {
                {"0", "Wheel diameter"},
                {"1", "Doppler"}
            }
        };

        public static BitField MMI_Q_MD_DATASET_ => new BitField
        {
            Name = "MMI_Q_MD_DATASET_",
            BitFieldType = BitFieldType.UInt8,
            Length = 8,
            Comment = "Indicates the content of the maintenance telegram. Value is bit-inverted",
            LookupTable = new LookupTable
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
            LookupTable = new LookupTable
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
                {"270", "TIMS brake pressure low"},
                {"271", "Spare"},
                {"272", "Spare"},
                {"273", "Unauthorized passing of EOA / LOA"},
                {"274", "Entering FS"},
                {"275", "Entering OS"},
                {"276", "#3 LE06/LE10/LE12/LE14 (Transition to Level #4)"},
                {"277", "#3 LE08 (Transition to NTC #2)"},
                {"278", "Emergency Brake Failure"},
                {"279", "Spare"},
                {"280", "Emergency stop"},
                {"281", "Spare"},
                {"282", "#3 ST04 (Connection Lost/Set-Up failed)"},
                {"286", "#3 ST06 (Reversing is possible)"},
                {"290", "SH refused"},
                {"291", "Spare"},
                {"292", "SH request failed"},
                {"293", "Spare"},
                {"294", "Spare"},
                {"295", "Spare"},
                {"296", "Trackside not compatible"},
                {"297", "Spare"},
                {"298", "#3 DR02 (Confirm Track Ahead Free)"},
                {"299", "Train is rejected"},
                {"300", "No MA received at level transition"},
                {"301", "Spare"},
                {"302", "Spare"},
                {"303", "Spare"},
                {"304", "Spare"},
                {"305", "Train divided"},
                {"306", "Spare"},
                {"307", "Spare"},
                {"308", "Spare"},
                {"309", "Spare"},
                {"310", "Train data changed"},
                {"311", "Spare"},
                {"312", "Spare"},
                {"313", "Spare"},
                {"314", "Spare"},
                {"315", "SR distance exceeded"},
                {"316", "SR stop order"},
                {"317", "Spare"},
                {"318", "Spare"},
                {"319", "Spare"},
                {"320", "RV distance exceeded"},
                {"321", "ETCS Isolated"},
                {"325", "ADR self-test successful"},
                {"326", "ADR self-test failure - ABDO disabled"},
                {"327", "ADR self-test time out - ABDO Disabled"},
                //328...335 = Spare
                {"336", "Radar Sensor failure"},
                {"337", "Radar Sensor maintenance required in #1 hours."},
                {"338", "Radar Sensor maintenance required now!"},
                //339..513 = "Spare"
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
                {"535", "Spare"},
                {"536", "Restart ATP!"},
                {"540", "No Level available Onboard"},
                {"543", "#2 failed"},
                {"544", "Spare"},
                {"552", "Announced level(s) not supported Onboard"},
                {"553", "Spare"},
                {"554", "Reactivate the Cabin!"},
                {"555", "#3 MO20 (Ack SN Mode)"},
                {"560", "Trackside malfunction"},
                {"561", "Trackside level is inhibited"},
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
                {"577", "Default National Values (NV) applied"},
                {"580", "Procedure Brake Percentage Entry terminated by ATP"},
                {"581", "Procedure Wheel Diameter Entry terminated by ATP"},
                {"582", "Procedure Doppler Radar Entry terminated by ATP"},
                {"583", "Spare"},
                //584...605 Spare
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
                {"633", "Speed Sensor maintenance required in #1 hours."},
                {"634", "Speed Sensor maintenance required now!"},
                {"635", "Juridical Recording not available"},
                {"636", "Euroloop not available"},
                {"637", "TIMS not available"},
                {"638", "Degraded Radio service"},
                {"639", "No Radio connection possible"},
                {"640", "CMD not available"},
                {"641", "CMD configured"},
                {"649", "Critical Odometry"},
                {"650", "Brake applied due to exceeded speed confidence"},
                {"651", "Odometry fallback level active"},
                //652...699 Spare
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
                {"710", "Spare"},
                {"711", "NL-input signal is withdrawn"},
                {"712", "Wheel data settings were successfully changed"},
                {"713", "Doppler radar settings were successfully changed"},
                {"714", "Brake percentage was successfully changed"},
                {"715", "No Country Selection in LZB PB Mode"},
                {"716", "#3 Symbol ST05 (hour glass)"},
                {"717", "Brake Test request is forbidden in level NTC CBTC"},
                //718...997 Spare
                {"9998", "Remote Control"},
                {"9999", "Remote Control Failure"}
            }
        };

        public static BitField MMI_Q_TRACKCOND_STEP => new BitField
        {
            Name = "MMI_Q_TRACKCOND_STEP",
            BitFieldType = BitFieldType.UInt8,
            Length = 4,
            Comment = "Variable describing step of the track condition.",
            LookupTable = new LookupTable
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
            Comment = "Identifier of a Specific STM Data to be entered.",
            LookupTable = new LookupTable
            {
                {"1", "TPWS Start Test?"},
                {"4", "TPWS Test in Progress"}
            }
        };

        public static BitField MMI_STM_Q_FOLLOWING => new BitField
        {
            Name = "MMI_STM_Q_FOLLOWING",
            BitFieldType = BitFieldType.Bool,
            Length = 1,
            Comment = "Indicate following data to be viewed at the same time",
            LookupTable = new LookupTable {{"True", "There is a following request to be managed together with the current one"}},
            SkipIfValue = false
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
            Comment = "Data value caption text",
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
            Comment = "Data for pick-up list value caption text",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "MMI_STM_L_VALUE",
                ScalingFactor = 8
            }
        };

        public static BitField MMI_T_BUTTONEVENT => new BitField
        {
            Name = "MMI_T_BUTTONEVENT",
            BitFieldType = BitFieldType.UInt32,
            Length = 32,
            Comment = "Time stamp for button event (DMI running time in milliseconds)"
        };

        public static BitField MMI_T_DMILM => new BitField
        {
            Name = "MMI_T_DMILM",
            BitFieldType = BitFieldType.UInt32,
            Length = 32,
            Comment = "Time stamp (ETC running time in milliseconds)"
        };

        public static BitField MMI_T_UTC => new BitField
        {
            Name = "MMI_T_UTC",
            BitFieldType = BitFieldType.UnixEpochUtc,
            Length = 32,
            Comment = "UTC time as seconds since 01.01.1970, 00:00:00"
        };

        public static BitField MMI_T_ZONE_OFFSET => new BitField
        {
            Name = "MMI_T_ZONE_OFFSET",
            BitFieldType = BitFieldType.Int8,
            Length = 8,
            Comment = "Time zone offset",
            Scaling = 0.25d,
            AppendString = " h"
        };

        public static BitField MMI_V_MAXTRAIN => new BitField
        {
            Name = "MMI_V_MAXTRAIN",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Maximum train speed",
            LookupTable = new LookupTable {{"0", "'No default value' => Data field shall remain empty"}},
            AppendString = " km/h"
        };

        public static BitField MMI_V_SETSPEED => new BitField
        {
            Name = "MMI_V_SETSPEED",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Set Speed value (range: 0-600)",
            AppendString = " km/h"
        };

        public static BitField MMI_V_STFF => new BitField
        {
            Name = "MMI_V_STFF",
            BitFieldType = BitFieldType.UInt16,
            Length = 16,
            Comment = "Max speed in Staff Responsible",
            AppendString = " km/h"
        };

        public static BitField MMI_X_DRIVER_ID => new BitField
        {
            Name = "MMI_X_DRIVER_ID",
            BitFieldType = BitFieldType.StringLatin,
            Length = 128,
            Comment = "This is the driver’s identity (max 16 character long)" +
                      "0 = Empty string (null character)" +
                      "\r\nNote 1: 16 alphanumeric characters (ISO 8859-1, also known as Latin Alphabet #1)." +
                      "\r\nNote 2: If the value is unknown the table will be filled with null characters (0, not '0')." +
                      "\r\nNote 3: If Driver ID is shorter than 16 characters the free characters will be filled with null characters." +
                      "\r\nNote 4: If Driver ID is 16 characters there will be no null character in the string."
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
            LookupTable = new LookupTable
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

        // checked 25-10-2019 RVV
        public static DataSetDefinition IPT_ECHO => new DataSetDefinition
        {
            Name = "IPT_ECHO IPT_ECHO",
            Comment = "ECHO telegram structure",
            Identifiers = new List<string> {"110"},
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

        // checked 01-03-2021
        public static LookupTable STM_LookupTable => new LookupTable
        {
            {"20", "TPWS"},
            {"21", "TPWS Fixed"},
            {"50", "CBTC"},
            {"255", "exit"},
        };
    }   
}