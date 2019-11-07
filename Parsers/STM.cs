using sonesson_tools.BitStreamParser;
using System.Collections.Generic;

namespace IPTComShark.Parsers
{
    public class STM : DataSetCollection
    {
        public STM()
        {
            this.Name = "STM";
            this.Description = "VSIS 2.13 STM definitions";

            DataSets.Add(IP_STM_1);
            DataSets.Add(IP_STM_15);
            DataSets.Add(IP_STM_32);
            DataSets.Add(IP_STM_35);
            DataSets.Add(IP_STM_38);
            DataSets.Add(IP_STM_39);
            DataSets.Add(IP_STM_43);
            DataSets.Add(IP_STM_46);
            DataSets.Add(IP_STM_30);
            DataSets.Add(IP_STM_34);
            DataSets.Add(IP_STM_40);
        }

        // 2019-11-04 checked to 2.14 JS
        public static DataSetDefinition IP_STM_1 => new DataSetDefinition
        {
            Name = "STM-1 Version",
            Comment =
                "This packet contains implicitly the connection request from the STM or the connection confirm from the ETCS On-board Function and provides version number and compatibility number for check.\r\nDirection of information:\r\nFrom STM to ETCS On-board function\r\nFrom ETCS On-board function to STM",
            Identifiers = new List<string>()
            {
                "230532000",
                "230537000",
                "230533007",
                "230538007"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "NID_NTC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Identity of the STM that sent the data"
                },
                new BitField
                {
                    Name = "STM_N_058_VERMAJOR",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Application Layer compatibility number, major number: X"
                },
                new BitField
                {
                    Name = "STM_N_058_VERMID",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Application Layer compatibility number, middle number: Y"
                },
                new BitField
                {
                    Name = "STM_N_058_VERMINOR",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Application Layer compatibility number, minor number: Z"
                },
                new BitField
                {
                    Name = "STM_N_035_VERMAJOR",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "FFFIS STM Layer compatibility number, major number: X"
                },
                new BitField
                {
                    Name = "STM_N_035_VERMID",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "FFFIS STM Layer compatibility number, middle number: Y"
                },
                new BitField
                {
                    Name = "STM_N_035_VERMINOR",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "FFFIS STM Layer compatibility number, minor number: Z"
                },
                new BitField
                {
                    Name = "STM_N_SRS_VERMAJOR",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Major version number of [Ref. 1 SRS]: X"
                },
                new BitField
                {
                    Name = "STM_N_SRS_VERMINOR",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Minor version number of [Ref. 1 SRS]: Y"
                },
                new BitField
                {
                    Name = "Spare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "spare to fill n x 32 byte size"
                }
            }
        };

        // 2019-11-04 checked to 2.13 JS
        public static DataSetDefinition IP_STM_15 => new DataSetDefinition
        {
            Name = "STM-15 State Report",
            Comment =
                "Indicates to the ERTMS/ETCS the STM state",
            Identifiers = new List<string>()
            {
                "230532010",
                "230537010"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "NID_NTC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Identity of the STM that sent the data"
                },
                new BitField
                {
                    Name = "STM_NID_STMSTATE",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Actual STM state",
                    LookupTable = new Dictionary<string, string>()
                    {
                        {"0", "Reserved (NP)"},
                        {"1", "PO"},
                        {"2", "CO"},
                        {"3", "DE"},
                        {"4", "CS"},
                        {"5", "Reserved (CS)"},
                        {"6", "HS"},
                        {"7", "DA"},
                        {"8", "FA"},
                    }
                },
                new BitField
                {
                    Name = "Spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "spare to fill n x 32 byte size"
                }
            }
        };

        // 2019-11-04 checked to 2.13 JS
        public static DataSetDefinition IP_STM_32 => new DataSetDefinition
        {
            Name = "STM-32 Button Request",
            Comment =
                "Create or update the visual states of buttons by STM.\r\nOnly referenced buttons are updated.",
            Identifiers = new List<string>()
            {
                "230532020",
                "230537020"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "NID_NTC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Identity of the STM that sent the data"
                },
                new BitField
                {
                    Name = "STM_N_ITER",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Maximum value = 10"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings {Name = "STM_N_ITER"},
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "STM_NID_STM",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment =
                                    "STM identity used to point to the corresponding palette for button and icon. This NID_STM may be different from the one in the message header as the STM is allowed to re-use buttons from another STM."
                            },
                            new BitField
                            {
                                Name = "STM_NID_BUTTON",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Functional identity of button from button palette given by NID_STM"
                            },
                            new BitField
                            {
                                Name = "STM32_NID_BUTPOS",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Button position on DMI"
                            },
                            new BitField
                            {
                                Name = "STM_NID_ICON",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Identity of button icon to be displayed"
                            },
                            new BitField
                            {
                                Name = "STM32_M_BUT_ATTRIB",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 16,
                                Comment =
                                    "Attributes of the buttons\r\nused bits:\r\n0..5 = spare, not used (set to zero)\r\n6..15 = Attributes\r\n"
                            },
                            new BitField
                            {
                                Name = "stm32_spare1",
                                BitFieldType = BitFieldType.Spare,
                                Length = 16,
                                Comment = "spare for alignment"
                            },
                            new BitField
                            {
                                Name = "STM_L_CAPTION",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 16,
                                Comment = "Length of X_CAPTION (Maximum value = 12)"
                            },
                            new BitField
                            {
                                VariableLengthSettings = new VariableLengthSettings {Name = "STM_L_CAPTION"},
                                NestedDataSet = new DataSetDefinition
                                {
                                    BitFields = new List<BitField>
                                    {
                                        new BitField
                                        {
                                            Name = "STM_X_CAPTION",
                                            BitFieldType = BitFieldType.StringLatin,
                                            Length = 8,
                                            Comment = "Caption text byte string"
                                        },
                                        new BitField
                                        {
                                            Name = "stm32_spare2",
                                            BitFieldType = BitFieldType.Spare,
                                            Length = 8,
                                            Comment = "spare for alignment"
                                        },
                                        new BitField
                                        {
                                            Name = "stm32_spare3",
                                            BitFieldType = BitFieldType.Spare,
                                            Length = 16,
                                            Comment = "spare for alignment"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        //2019-11-04 checked to 2.13 JS
        public static DataSetDefinition IP_STM_35 => new DataSetDefinition
        {
            Name = "STM-35 Indicator Request",
            Comment =
                "Create or update the visual states of indicators by STM.\r\nOnly referenced indicators are updated.",
            Identifiers = new List<string>()
            {
                "230532030",
                "230537030"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "NID_NTC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Identity of the STM that sent the data"
                },
                new BitField
                {
                    Name = "STM_N_ITER",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Maximum value = 24"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings {Name = "STM_N_ITER"},
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "STM_NID_STM",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment =
                                    "STM identity used to point to the corresponding palette for indicator and icon. This NID_STM may be different from the one in the message header as the STM is allowed to re-use indicators from another STM."
                            },
                            new BitField
                            {
                                Name = "STM_NID_INDICATOR",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Functional identity of indicator from indicator palette given by NID_STM"
                            },
                            new BitField
                            {
                                Name = "STM35_NID_INDPOS",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Indicator position on DMI"
                            },
                            new BitField
                            {
                                Name = "STM_NID_ICON",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Identity of icon from palette given by icon palette given by NID_STM"
                            },
                            new BitField
                            {
                                Name = "STM35_M_IND_ATTRIB",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 16,
                                Comment =
                                    "Attributes for indicators.\r\nused bits:\r\n0..5 = spare, not used (set to zero)\r\n6..15 = Attributes\r\n"
                            },
                            new BitField
                            {
                                Name = "stm35_spare2",
                                BitFieldType = BitFieldType.Spare,
                                Length = 16,
                                Comment = "Alignment"
                            },
                            new BitField
                            {
                                Name = "STM_L_CAPTION",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 16,
                                Comment = "Length of X_CAPTION\r\nMaximum value = 12"
                            },
                            new BitField
                            {
                                Name = "STM_X_CAPTION",
                                BitFieldType = BitFieldType.StringLatin,
                                Length = 8,
                                Comment = "Caption text byte string",
                                VariableLengthSettings =
                                    new VariableLengthSettings {Name = "STM_L_CAPTION", ScalingFactor = 8}
                            }
                        }
                    }
                }
            }
        };

        // reviewed 20181005 JS
        public static DataSetDefinition IP_STM_38 => new DataSetDefinition
        {
            Name = "STM-38 Text Msg",
            Comment =
                "Text messages for the DMI, with or without acknowledgement. From STM to ETCS On-board function",
            Identifiers = new List<string>()
            {
                "230532040",
                "230537040"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "NID_NTC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Identity of the STM that sent the data"
                },
                new BitField
                {
                    Name = "STM_NID_XMESSAGE",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Sequence number of given text messages"
                },
                new BitField
                {
                    Name = "STM_Q_ACK",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Acknowledgement qualifier"
                },
                new BitField
                {
                    Name = "XATTRIBUTE Spare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 6
                },
                sonesson_tools.DataSets.VSIS210.MMI_M_XATTRIBUTE,
                new BitField
                {
                    Name = "STM_L_TEXT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Number of characters in text string. Maximum value = 40"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings {Name = "STM_L_TEXT", ScalingFactor = 8},
                    Name = "STM_X_TEXT",
                    BitFieldType = BitFieldType.StringAscii,
                    Length = 8,
                    Comment = "Text character"
                }
            }
        };

        // 2019-11-04 checked to 2.13 JS
        public static DataSetDefinition IP_STM_39 => new DataSetDefinition
        {
            Name = "STM-39 Delete Text Msg",
            Comment =
                "STM commands the deletion of text message. \r\nApplies also if driver has not given acknowledgement.",
            Identifiers = new List<string>()
            {
                "230532050",
                "230537050"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "NID_NTC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Identity of the STM that sent the data"
                },
                new BitField
                {
                    Name = "STM_NID_XMESSAGE",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Sequence number of text message to be deleted."
                },
                new BitField
                {
                    Name = "stm39_spare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "spare for 32-bit alignment"
                }
            }
        };

        // 2019-11-04 checked to 2.13 JS
        public static DataSetDefinition IP_STM_43 => new DataSetDefinition
        {
            Name = "STM-43 STM_NATIONAL_ETCS_DMI",
            Comment =
                "Data for displaying on ETCS DMI while in National mode",
            Identifiers = new List<string>()
            {
                "230532060",
                "230537060"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "NID_NTC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Identity of the STM that sent the data"
                },
                new BitField
                {
                    Name = "STM43_Q_STATUS",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Contains the following Q-states (bit using):\r\n0..1   = \"Q_SCALE, qualifier for distance scale\"\r\n2..13 = \"Q_INDICATE of Inhibition of ETCS DMI objects\"\r\n14     = \"Q_WARNLIMIT, warning limit status\"\r\n15     = \"Q_INDICATIONLIMIT status\r\n\r\nNote: value setting see chp. \"Signal Variables\" MMI_STM_xxx with xxx for Q_SCALE etc."
                },
                new BitField
                {
                    Name = "STM43_V_PERMIT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Permitted speed"
                },
                new BitField
                {
                    Name = "STM43_V_TARGET",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Target speed"
                },
                new BitField
                {
                    Name = "STM43_V_RELEASE",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Release speed"
                },
                new BitField
                {
                    Name = "STM43_V_INTERV",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Intervention speed"
                },
                new BitField
                {
                    Name = "stm43_spare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "spare for alignment"
                },
                new BitField
                {
                    Name = "STM43_D_TARGET",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Target distance"
                },
                new BitField
                {
                    Name = "STM_N_ITER",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Iterator"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings {Name = "STM_N_ITER"},
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "STM_M_SUP",
                                BitFieldType = BitFieldType.UInt32,
                                Length = 32,
                                Comment = "STM-customised supervision information"
                            }
                        }
                    }
                }
            }
        };

        // 2019-11-04 checked to 2.13 JS
        public static DataSetDefinition IP_STM_46 => new DataSetDefinition
        {
            Name = "STM-46 Sound Command",
            Comment = "Command sound",
            Identifiers = new List<string>()
            {
                "230532070",
                "230537070"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "NID_NTC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Identity of the STM that sent the data"
                },
                new BitField
                {
                    Name = "STM_N_ITER",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Number of sounds to be generated\r\nMaximum value = 2\r\nThe STM is able to request to the ETCS On-board to generate a maximum of two sounds at the same time."
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "STM_N_ITER"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "STM_NID_STM",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment =
                                    "STM identity used to point to the corresponding sound palette. This NID_STM may be different from the one in the message header as the STM is allowed to re-use sounds from another STM. "
                            },
                            new BitField
                            {
                                Name = "STM_NID_SOUND",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Functional identity of sound from sound palette given by NID_STM"
                            },
                            new BitField
                            {
                                Name = "STM46_Q_SOUND",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Continuous/ Not continuous/ Stopped",
                                LookupTable = new Dictionary<string, string>()
                                {
                                    {"0", "Stop"},
                                    {"1", "One shot"},
                                    {"2", "Continuous"},
                                    {"3", "Reserved"},
                                }
                            },
                            new BitField
                            {
                                Name = "stm46_spare1",
                                BitFieldType = BitFieldType.Spare,
                                Length = 8,
                                Comment = "spare for 32-bit alignment"
                            },
                            new BitField
                            {
                                Name = "STM_N_ITER2",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 16,
                                Comment = "Number of segments of sound"
                            },
                            new BitField
                            {
                                VariableLengthSettings = new VariableLengthSettings
                                {
                                    Name = "STM_N_ITER2"
                                },
                                NestedDataSet = new DataSetDefinition
                                {
                                    BitFields = new List<BitField>
                                    {
                                        new BitField
                                        {
                                            Name = "STM_M_FREQ",
                                            BitFieldType = BitFieldType.UInt8,
                                            Length = 8,
                                            Comment = "Frequency of a segment"
                                        },
                                        new BitField
                                        {
                                            Name = "STM_T_SOUND",
                                            BitFieldType = BitFieldType.UInt8,
                                            Length = 8,
                                            Comment = "Duration of segment"
                                        },
                                        new BitField
                                        {
                                            Name = "stm46_spare2",
                                            BitFieldType = BitFieldType.Spare,
                                            Length = 16,
                                            Comment = "spare for 32-bit alignment"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        // 2019-11-04 checked to 2.13 JS
        public static DataSetDefinition IP_STM_30 => new DataSetDefinition
        {
            Name = "STM-30 DRIVER_LANGUAGE_TRANSMISSION",
            Comment =
                "Driver language selection.",
            Identifiers = new List<string>()
            {
                "230533000",
                "230538000"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "NID_NTC_IP",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Identity of the STM that sent the data"
                },
                new BitField
                {
                    Name = "MMI_STM_NID_DRV_LANG",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Driver language selection"
                }
            }
        };

        // 2019-11-04 checked to 2.13 JS
        public static DataSetDefinition IP_STM_34 => new DataSetDefinition
        {
            Name = "STM-34 Button Event",
            Comment =
                "Report the button events.",
            Identifiers = new List<string>()
            {
                "230533010",
                "230538010"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "NID_NTC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Identity of the STM that sent the data"
                },

                new BitField
                {
                    Name = "STM_N_ITER",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Number of events being reported"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "STM_N_ITER"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "STM_NID_STM",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment =
                                    "STM identity used to point to the corresponding Button palette.\r\nThis NID_STM may be different from the one in the message header as the STM is allowed to re-use buttons from another STM."
                            },
                            new BitField
                            {
                                Name = "STM_NID_BUTTON",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Functional identity of button from button palette given by NID_STM"
                            },
                            new BitField
                            {
                                Name = "STM34_Q_BUTTON",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Button Event",
                                LookupTable = new Dictionary<string, string>()
                                {
                                    {"0", "Push"},
                                    {"1", "Release"}
                                }
                            },
                            new BitField
                            {
                                Name = "stm34_spare",
                                BitFieldType = BitFieldType.Spare,
                                Length = 8,
                                Comment = "spare for alignment"
                            },
                            new BitField
                            {
                                Name = "STM_T_BUTTONEVENT",
                                BitFieldType = BitFieldType.UInt32,
                                Length = 32,
                                Comment = "event timestamp"
                            }
                        }
                    }
                },
            }
        };

        // 2019-11-04 checked to 2.13 JS
        public static DataSetDefinition IP_STM_40 => new DataSetDefinition
        {
            Name = "STM-40 Text Ack Reply",
            Comment =
                "Report from ETCS on acknowledgement of text message",
            Identifiers = new List<string>()
            {
                "230533020",
                "230538020"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "NID_NTC_IP",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Identity of the STM that sent the data"
                },
                new BitField
                {
                    Name = "MMI_STM_NID_XMESSAGE",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Sequence number of the acknowledged message."
                },
                new BitField
                {
                    Name = "stm40_spare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "spare for 32-bit alignment"
                }
            }
        };
    }
}