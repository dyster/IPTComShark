﻿using BitDataParser;
using System.Collections.Generic;

namespace TrainShark.DataSets
{
    public class IPT : DataSetCollection
    {
        public IPT()
        {
            this.Name = "IPTCom general datasets";
            this.Description = "Built into IPTCom";

            DataSets.Add(PISRepManMsgLst);
            DataSets.Add(com100);
            DataSets.Add(com101);
            DataSets.Add(com102);
            DataSets.Add(com103);
            DataSets.Add(com222);
            DataSets.Add(com223);
            //DataSets.Add(PSCCtrlOp);
            //DataSets.Add(CCUOGlobalA);
        }

        public static DataSetDefinition PISRepManMsgLst => new DataSetDefinition
        {
            Name = "PISRepManMsgLst",
            Comment = "Request PIS Manual Message List",
            Identifiers = new Identifiers { Numeric = { 1055910100 } },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "IStatus",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "The status of this reply"
                },
                new BitField
                {
                    Name = "Reserved",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8*29,
                    Comment = "Reserved"
                },
                new BitField
                {
                    Name = "INbrOfRecords",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "The number of records in the following array. Only use when IStatus = ok"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings{Name = "INbrOfRecords"},
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField
                            {
                                Name = "IMsgId",
                                BitFieldType = BitFieldType.UInt16,
                                Length = 16,
                                Comment = "PIS identifier for the message"
                            },
                            new BitField
                            {
                                Name = "IGroupID",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment = "Indicates the group ID that the message is assigned to. 0 : No group ID assigned to the message, 1 to 255 : The group ID assigned to the message"
                            },
                            new BitField
                            {
                                Name = "Reserved",
                                BitFieldType = BitFieldType.Spare,
                                Length = 8,
                                Comment = "Reserved"
                            },
                            new BitField
                            {
                                Name = "IMsgDsc",
                                BitFieldType = BitFieldType.StringUtf8,
                                Length = 8*128,
                                Comment = "Short description of the message. UTF-8 Encoded char array, Null terminated string character to indicate end of the message"
                            },
                            new BitField
                            {
                                Name = "Reserved",
                                BitFieldType = BitFieldType.Spare,
                                Length = 8*60,
                                Comment = "Reserved"
                            }
                        }
                    }
                }
            }
        };

        public static DataSetDefinition com100 => new DataSetDefinition
        {
            Name = "IPTDir Process Data",
            Comment =
                "The IPTDir process data shall inform all end devices about the current inauguration status and changes in the train’s topology.",
            Identifiers = new Identifiers { Numeric = { 100 } },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ProtocolVersion",
                    BitFieldType = BitFieldType.HexString,
                    Length = 32,
                    Comment =
                        "The protocol version of IPTDir. The bytes repre-sent version, release, update and evolution.\r\nThe most significant byte (version) is used for protocol incompatibility.\r\nVersion = 0x02020000 (V. 2.2.0.0, default)"
                },
                new BitField
                {
                    Name = "IPT_InaugStatus",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Status of IPT train configuration:\r\n– 0: Fault on consist level  no IP communi-cation possible\r\n– 1: Invalid/Busy  Stop communication on train level\r\n– 2: OK\r\nIPT_InaugStatus = 0 is the CMS’s initialisation state that will be transmitted until the first IPT inauguration has been done.\r\nIf a transition from 2 to 1 occurs then all infor-mation contained in previous IPT Info concern-ing not the own consist have to be discarded."
                },
                new BitField
                {
                    Name = "IPT_TopoCount",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Changes if train topology changes by adding or removing consists (which changes the IP ad-dresses on train level).\r\nThe IPT_TopoCount’s range is 1…63."
                },
                new BitField
                {
                    Name = "UIC_InaugStatus",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Status of UIC train configuration:\r\n– 0: actual\r\n– 1: confirmed\r\n– 2: invalid\r\nIf a transition to 2 occurs then all information contained in previous UIC Info have to be dis-carded."
                },
                new BitField
                {
                    Name = "UIC_TopoCount",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Increases if UIC car numbering changes, com-parable to topo_count in TCN. If IPT_TopoCount changes, UIC_TopoCount will be increased, too.\r\nThe UIC_TopoCount’s range is 1…63.\r\nAfter 63, it wraps over to 1 again."
                },
                new BitField
                {
                    Name = "DynStatus",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Status of dynamic data\r\n– 0: OK\r\n– 1: invalid\r\nCurrently not used (=0)"
                },
                new BitField
                {
                    Name = "DynCount",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Dynamic count:\r\nIncreases if dynamic data change which are ir-relevant for IPT topology (currently just the posi-tion of the leading car and the car number for seat reservation). If topology changes DynCount will be increased, too.\r\nThe range is 0…255 with wraparound."
                },
                new BitField
                {
                    Name = "TBType",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Type of the used train backbone\r\n– 0: ETB\r\n– 1: WTB\r\n(see Annex 0)"
                },
                new BitField
                {
                    Name = "Reserved1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "For future extensions (set to 0)"
                },
                new BitField
                {
                    Name = "SizeOfIptInfo",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Size of payload (memory need) for IPT Info (see chapter 4.1)."
                },
                new BitField
                {
                    Name = "SizeOfUicInfo",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Size of payload (memory need) for UIC Info (see chapter 4.2)"
                },
                new BitField
                {
                    Name = "ServerIPaddress",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment =
                        "IP address of IPTDir Server. This address is re-solved by IPTDir clients to the predefined URI IPTDirServer.anyCar and used by them in re-quests (see chapter 5) and updates."
                },
                new BitField
                {
                    Name = "GatewayIPaddress",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment =
                        "IP address of the local gateway to the train backbone.\r\nThis is either the address of the active train switch (in case TBType = 0) or the address of the active TCN gateway (in case TBType = 1).\r\nNote: Usually ServerIPaddress and Gate-wayIPaddress will be the same because the IPTDir server is foreseen to be located either on the Train Switch (if TBType = 0) or on the TCN Gateway (if TBType = 1)."
                },
                new BitField
                {
                    Name = "NodeNo",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Own consist number.\r\nRange: 1…32"
                },
                new BitField
                {
                    Name = "NumConsistsInTrain",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Total number of consists in train.\r\nRange: 1…32"
                },
                new BitField
                {
                    Name = "Reserved2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Total number of consists in train.\r\nRange: 1…32"
                },
            }
        };

        public static DataSetDefinition ipt_label => new DataSetDefinition
        {
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ipt_label",
                    BitFieldType = BitFieldType.StringAscii,
                    Length = 8 * 16
                }
            }
        };

        public static DataSetDefinition IPT_McGroupDataSet => new DataSetDefinition
        {
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "McGroupLblIdx",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Index pointing into McGroupLabel string array thus defining the group label (real multicast label, starting with one of the reserved group prefixes “grp” or “frg”) for this group."
                },
                new BitField
                {
                    Name = "McGroupNo",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Number of the multicast group. Range is\r\n1..4094. This corresponds to the max. 12\r\ngroup bits in the multicast IP address of the\r\ngroup.\r\nNote:\r\nThis dataset is used on train, consist and\r\ncar level. A definition on the particular level\r\nmakes the group resolvable on that level for\r\nthe members it is defined for. Example:\r\nthere needs to be a group number assigned\r\nfor the group label “grpHMI” in car01 of\r\ncst01 to make the URI\r\n“grpHMI.car01.cst01” resolvable"
                }
            }
        };

        public static DataSetDefinition IPT_DeviceDataSet => new DataSetDefinition
        {
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "DeviceLblIdx",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Index pointing into Device_Label string array thus defining the device label (unicast label) for this de-vice."
                },
                new BitField
                {
                    Name = "DeviceNo",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Number of the device. Range is 1..4095. This cor-responds to the max. 12 host bits in the unicast IP address of the device.\r\nNote:\r\nIf the 12 available bits are split between car and host then the range for this value must be reduced accordingly."
                },
                new BitField
                {
                    Name = "NumResQWInDev",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment =
                        "Number of reserved Quad Words (4 bytes each) following. Is 0 since version 2.0.0.0 of the protocol but may be > 0 in future versions. Skip four times this number of bytes."
                }
            }
        };

        public static DataSetDefinition IPT_CarDataSet => new DataSetDefinition
        {
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "CarLbl",
                    BitFieldType = BitFieldType.StringAscii,
                    Length = 8 * 16,
                    Comment =
                        "Unique string defining the primary car label for this car. If the car has a UIC car number (written on the car body) then this number is prefixed with ‘UIC’ and the resulting string is used as car label. If not then another unique label must be defined. In this case it should be avoided to let it begin with ‘UIC’. The range in which the label must be unique must cover at least all rolling stock that can be addressed. At best the car label is globally unique as it is the case with the UIC numbers."
                },
                new BitField
                {
                    Name = "CstCarNo",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Sequence number of this car in its Consist ac-cording to the Consists reference direction. By prefixing this number with ‘car’ you will get an alias for the car label in Consist context (e.g. ‘car03’ if CstCarNo is 3)."
                },
                new BitField
                {
                    Name = "T",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 1,
                    Comment = "Orientation of car relative to IP Train: 0 = opposite 1 = same"
                },
                new BitField
                {
                    Name = "C",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 1,
                    Comment = "Orientation of car relative to Consists reference direction: 0 = opposite 1 = same"
                },
                new BitField
                {
                    Name = "spare bits",
                    BitFieldType = BitFieldType.Spare,
                    Length = 6
                },
                new BitField
                {
                    Name = "CarTypeLblIdx",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Index pointing into CarTypeLabel string array thus defining the car type for this car."
                },
                new BitField
                {
                    Length = 5,
                    NestedDataSet = new DataSetDefinition()
                    {
                        BitFields = new List<BitField>()
                        {
                            new BitField
                            {
                                Name = "UIC_Identifier",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment =
                                    "UIC vehicle identification number. This number – which also appears in the UIC Info – plays the same role for UIC as the car label does for IPT: It uniquely identifies the car in the UIC data base on the UIC inauguration and the IPT data base on an independent IPT inauguration. If there are “dead” cars, the car numbering in UIC and IPT may differ from each other. The double appearance of UIC_Identifier in UIC and IPT Info ensures that you can link the corre-sponding car records."
                            },
                        }
                    }
                },
                new BitField
                {
                    Name = "Reserved",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8 * 3
                },
                new BitField
                {
                    Name = "NumMcCarGroups",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Number of multicast groups defined in the car"
                },
                new BitField
                {
                    Name = "McCarGroupData",
                    VariableLengthSettings = new VariableLengthSettings() {Name = "NumMcCarGroups"},
                    Comment = "One entry for each group in the Car, see chapter 4.1.3",
                    NestedDataSet = IPT_McGroupDataSet
                },
                new BitField
                {
                    Name = "NumResQWInCar",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment =
                        "Number of reserved Quad Words (4 bytes each) following. Is 0 since version 2.0.0.0 of the protocol but may be > 0 in future versions. Skip four times this number of bytes."
                },
                new BitField
                {
                    Name = "ResQWInCarData",
                    VariableLengthSettings = new VariableLengthSettings() {Name = "NumResQWInCar"},
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField()
                            {
                                Name = "QuadWord",
                                BitFieldType = BitFieldType.UInt32,
                                Length = 32
                            }
                        }
                    }
                },
                new BitField
                {
                    Name = "NumDevices",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Number of devices in car"
                },
                new BitField
                {
                    Name = "DeviceDataSet",
                    VariableLengthSettings = new VariableLengthSettings() {Name = "NumDevices"},
                    NestedDataSet = IPT_DeviceDataSet
                },
            }
        };

        public static DataSetDefinition IPT_ConsistDataSet => new DataSetDefinition
        {
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ConsistLbl",
                    BitFieldType = BitFieldType.StringAscii,
                    Length = 8 * 16,
                    Comment =
                        "Unique string defining the primary label for the Consist in the IPT URI. If a Consist has a unique UIC number this number will be prefixed with ‘UIC’ and the resulting string be used as the Consist label.\r\nIf such a unique identifier is not existing, the car label of the first Consist car will be used. I.e. a Consist label may be identical to one of the car labels."
                },
                new BitField
                {
                    Name = "TrnCstNo",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Sequence number of Consist in IPT train direction. By prefixing this number with ‘cst’ you will get an alias for the Consist label in the train (e.g. ‘cst02’ if TrnCstNo is 2)"
                },
                new BitField
                {
                    Name = "isLocal",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Is this the local consist?\r\n– 0 = no\r\n– 1 = yes"
                },
                new BitField
                {
                    Name = "Orientation",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Orientation of Consist in train.\r\n– 0 = Consist reference direction 1 is op-posite to IP train reference direction.\r\n– 1 = Consist reference direction 1 matches IP train reference direction."
                },
                new BitField
                {
                    Name = "Reserved",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = ""
                },
                new BitField
                {
                    Name = "NumMcCstGroups",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Number of multicast groups defined in the Consist"
                },
                new BitField
                {
                    Name = "McCstGroupData",
                    VariableLengthSettings = new VariableLengthSettings() {Name = "NumMcCstGroups"},
                    Comment = "One entry for each group in the Consist, see chapter 4.1.3",
                    NestedDataSet = IPT_McGroupDataSet
                },
                new BitField
                {
                    Name = "NumResQWInTrain",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment =
                        "Number of reserved Quad Words (4 bytes each) following. Is 0 since version 2.0.0.0 of the protocol but may be > 0 in future versions. Skip four times this number of bytes."
                },
                new BitField
                {
                    Name = "ResQWInTrainData",
                    VariableLengthSettings = new VariableLengthSettings() {Name = "NumResQWInTrain"},
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField()
                            {
                                Name = "QuadWord",
                                BitFieldType = BitFieldType.UInt32,
                                Length = 32
                            }
                        }
                    }
                },
                new BitField
                {
                    Name = "NumControlledCars",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Number of cars in the Consist"
                },
                new BitField
                {
                    Name = "CarDataSet",
                    VariableLengthSettings = new VariableLengthSettings() {Name = "NumControlledCars"},
                    NestedDataSet = IPT_CarDataSet
                },
            }
        };

        public static DataSetDefinition com101 => new DataSetDefinition
        {
            Name = "IPTDir Message Data",
            Comment =
                "The IPTDir Server distributes one or both of the message data types presented in this chapter whenever the topology or the dynamic data changes. This is indicated by a change of one of the TopoCounters or the DynCounter contained in the IPTDir Process Data.",
            Identifiers = new Identifiers { Numeric = { 101 } },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ProtocolVersion",
                    BitFieldType = BitFieldType.HexString,
                    Length = 32,
                    Comment =
                        "The protocol version of IPTDir. The bytes represent version, release, update and evolution.\r\nThe most significant byte (version) is used\r\nfor protocol incompatibility.\r\nVersion = 0x02020000 (V. 2.2.0.0, default)"
                },
                new BitField
                {
                    Name = "IPT_InaugStatus",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Status of IPT train configuration:\r\n– 0: Fault on consist level  no IP communi-cation possible\r\n– 1: Invalid/Busy  Stop communication on train level\r\n– 2: OK\r\nIPT_InaugStatus = 0 is the CMS’s initialisation state that will be transmitted until the first IPT inauguration has been done.\r\nIf a transition from 2 to 1 occurs then all infor-mation contained in previous IPT Info concern-ing not the own consist have to be discarded."
                },
                new BitField
                {
                    Name = "IPT_TopoCount",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Changes if train topology changes by adding or removing consists (which changes the IP ad-dresses on train level). The IPT_TopoCount’s range is 1…63."
                },
                new BitField
                {
                    Name = "Reserved1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "For future extensions (set to 0)"
                },
                new BitField
                {
                    Name = "NumCarTypesInTrain",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Total number of car types in train (number of different CarTypeLabels)"
                },
                new BitField
                {
                    Name = "CarTypeDataSet",
                    VariableLengthSettings = new VariableLengthSettings() {Name = "NumCarTypesInTrain"},
                    Comment =
                        "Car types allow to group cars which belong to the same family of vehicles. They have the same main characteristics (mechanical, power, electrical, architecture, functions and equipment). Nevertheless they can still differ from each other on minor features (e.g. seat layout, interior etc.).\r\nCar types are – as labels – denoted by a string of up to 15 characters. However, they are not used in addressing (not part of the URI). This is the reason why they are – in contrary to the labels – case sensitive.\r\nCarTypeDataSet is a list of the car types present in the train. The strings in the list are unique but may be assigned to more than one car.",
                    NestedDataSet = ipt_label
                },
                new BitField
                {
                    Name = "NumDeviceLabelsInTrain",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Total number of (different) DeviceLabels in train"
                },
                new BitField
                {
                    Name = "DeviceLabelDataSet",
                    VariableLengthSettings = new VariableLengthSettings() {Name = "NumDeviceLabelsInTrain"},
                    Comment =
                        "List of device labels (for the unicast case of the device label) present in train. These are concatenations of the device type (e.g. ‘door_ctrl’) and perhaps a location postfix (e.g. ‘front_left’) in the form type_location (e.g. ‘door_ctrl_front_left’). The labels in the list are unique but may be assigned to more than one device (once per car). Device labels must not start with the reserved group prefixes (see chapter 4.1.3).",
                    NestedDataSet = ipt_label
                },
                new BitField
                {
                    Name = "NumGroupLabelsInTrain",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Total number of (different) Group Labels in train"
                },
                new BitField
                {
                    Name = "GroupLabelDataSet",
                    VariableLengthSettings = new VariableLengthSettings() {Name = "NumGroupLabelsInTrain"},
                    Comment =
                        "List of group labels (for the multicast case of the device label, see [04]) present in train.\r\nThe labels in the list are unique but may be assigned multiple times (once per train, once per each consist and once per each car).",
                    NestedDataSet = ipt_label
                },
                new BitField
                {
                    Name = "NumMcTrnGroups",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Number of multicast groups defined on train level"
                },
                new BitField
                {
                    Name = "McTrnGroupData",
                    VariableLengthSettings = new VariableLengthSettings() {Name = "NumMcTrnGroups"},
                    Comment = "One entry for each group on train level, see chapter 4.1.3",
                    NestedDataSet = IPT_McGroupDataSet
                },
                new BitField
                {
                    Name = "NumResQWInTrain",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment =
                        "Number of reserved Quad Words (4 bytes each) following. Is 0 since version 2.0.0.0 of the protocol but may be > 0 in future versions. Skip four times this number of bytes."
                },
                new BitField
                {
                    Name = "ResQWInTrainData",
                    VariableLengthSettings = new VariableLengthSettings() {Name = "NumResQWInTrain"},
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            new BitField()
                            {
                                Name = "QuadWord",
                                BitFieldType = BitFieldType.UInt32,
                                Length = 32
                            }
                        }
                    }
                },
                new BitField
                {
                    Name = "NumConsistsInTrain",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Total number of Consists in train"
                },
                new BitField
                {
                    Name = "ConsistDataSet",
                    VariableLengthSettings = new VariableLengthSettings() {Name = "NumConsistsInTrain"},
                    NestedDataSet = IPT_ConsistDataSet
                },
            }
        };

        public static DataSetDefinition UIC_CarDataSet => new DataSetDefinition
        {
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "reserved",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "For future extensions"
                },
                new BitField
                {
                    Name = "UIC_ConsistNo",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Consist number in train ( = sequence number of the active IP-TS in the train. Always = 1 if the consist does not have an IP-TS but an DR) Corresponds to WTB sequence number in UIC556"
                },
                new BitField
                {
                    Name = "NumControlledCars",
                    BitFieldType = BitFieldType.Int8,
                    Length = 8,
                    Comment =
                        "Number of cars in the Consist (UIC Leaflet 556 allows negative values. For definition see UIC Leaflet 556 Annex A)"
                },
                new BitField
                {
                    Name = "UIC_CarSeqNum",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Car sequence number in train in UIC reference direction. In UIC: uic_address (see chapter 2.3 on page 7 for ex-planation of car numbering)"
                },
                new BitField
                {
                    Name = "Operator",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Optional: Operator of the Consist (as defined in UIC556)"
                },
                new BitField
                {
                    Name = "Owner",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Optional: Owner of the Consist (as defined in UIC556)"
                },
                new BitField
                {
                    Name = "NationalAppl",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Optional: National application type not used (= 0) (as defined in UIC556)"
                },
                new BitField
                {
                    Name = "NationalVer",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Optional: National application version not used (= 0) (as defined in UIC556)"
                },
                new BitField
                {
                    Name = "UIC_CstProperties",
                    BitFieldType = BitFieldType.HexString,
                    Length = 8 * 22,
                    Comment = "Optional: Consist properties as bit map (as defined in UIC556), see Annex A on page 30."
                },
                new BitField
                {
                    Name = "reserved",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "For future extensions"
                },
                new BitField
                {
                    Name = "UIC_Identifier",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "UIC vehicle identification number"
                },
                new BitField
                {
                    Name = "UIC_Identifier",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "UIC vehicle identification number"
                },
                new BitField
                {
                    Name = "UIC_Identifier",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "UIC vehicle identification number"
                },
                new BitField
                {
                    Name = "UIC_Identifier",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "UIC vehicle identification number"
                },
                new BitField
                {
                    Name = "UIC_Identifier",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "UIC vehicle identification number"
                },
                new BitField
                {
                    Name = "UIC_CarProperties",
                    BitFieldType = BitFieldType.HexString,
                    Length = 8 * 6,
                    Comment =
                        "Optional: List of car properties as bitmap (as defined in UIC556), see Annex B on page 33."
                },
                new BitField
                {
                    Name = "reserved",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment =
                        "Optional: List of car properties as bitmap (as defined in UIC556), see Annex B on page 33."
                },
                new BitField
                {
                    Name = "T",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 1,
                    Comment =
                        "Orientation of car relative to UIC reference direction of Train (dynamic data which is changed if leading car changes, see chapter 2.3 for explanation of directions) 0: opposite 1: same"
                },
                new BitField
                {
                    Name = "C",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 1,
                    Comment = "Orientation of car relative to orientation of Consist (static) 0: opposite 1: same"
                },
                new BitField
                {
                    Name = "L",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 1,
                    Comment =
                        "Leading (dynamic data set by application request) 0: Car is not leading 1: Car is leading"
                },
                new BitField
                {
                    Name = "R",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 1,
                    Comment =
                        "Leading Request (dynamic data set by application request) 0: No leading request 1: Car requests leading"
                },
                new BitField
                {
                    Name = "reserved",
                    BitFieldType = BitFieldType.Spare,
                    Length = 4,
                    Comment = "reserved = 0"
                },
                new BitField
                {
                    Name = "SeatResNo",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Optional: Car number for seat reservation (SeatRes-No=0 it is not yet defined)"
                },
                new BitField
                {
                    Name = "NumTrnSwInCar",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Number of Train Switches in car"
                },
            }
        };

        public static DataSetDefinition com102 => new DataSetDefinition
        {
            Name = "UIC Info",
            Comment = "",
            Identifiers = new Identifiers { Numeric = { 102 } },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ProtocolVersion",
                    BitFieldType = BitFieldType.HexString,
                    Length = 32,
                    Comment =
                        "The protocol version of IPTDir. The bytes represent version, release, update and evolution.\r\nThe most significant byte (version) is used\r\nfor protocol incompatibility.\r\nVersion = 0x02020000 (V. 2.2.0.0, default)"
                },
                new BitField
                {
                    Name = "OptionsPresent",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment =
                        "The structure of the IPTDir UIC_Message Data payload is designed to be as close as possible to the UIC NADI. However, some of the data in this structure may just optionally be filled with values. OptionsPresent is a bit array denoting which of these optional parts actually contain usable values. The following table lists pairs of bit positions n and the content (names of variables) that con-tains data if the bit at position 2n in OptionsPresent is set. If the bit is not set then these data will be zero and shall be ignored by the receiver.\r\nn affected content\r\n– 0 ConfirmedPos\r\n– 1 Operator, Owner\r\n– 2 NationalAppl, NationalVer\r\n– 3 UIC_CstProperties\r\n– 5 UIC_CarProperties\r\n– 6 SeatResNo"
                },
                new BitField
                {
                    Name = "InaugFrameVers",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Version of the supported inauguration frame as defined in UIC Leaflet 556 Annex C.3"
                },
                new BitField
                {
                    Name = "RDataVers",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Version of the supported R-data telegram as de-fined in UIC Leaflet 556 Annex A"
                },
                new BitField
                {
                    Name = "UIC_InaugStatus",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Status of train configuration: 0: actual 1: confirmed 2: invalid"
                },
                new BitField
                {
                    Name = "UIC_TopoCount",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment =
                        "Increases if train topology changes, comparable to topo_count in TCN. The TopoCount’s range is 1..31. After 31 it wraps over to 1 again."
                },
                new BitField
                {
                    Name = "Reserved",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8 * 3
                },
                new BitField
                {
                    Name = "O",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 1,
                    Comment =
                        "Orientation: UIC reference direction relative to IP-Train reference direction (see chapter 2.3 for expla-nation of directions)– 0: opposite– 1: same"
                },
                new BitField
                {
                    Name = "A",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 1,
                    Comment =
                        "NotAllConfirmed: At least 1 train bus node available without confirmed UIC address\r\n– 0: feature not available\r\n– 1: feature available"
                },
                new BitField
                {
                    Name = "C",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 1,
                    Comment =
                        "ConfirmedCancelled: At least 1 train bus node with confirmed UIC address cancelled\r\n– 0: feature not available\r\n– 1: feature available"
                },
                new BitField
                {
                    Name = "spare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 5
                },
                new BitField
                {
                    Length = 8,
                    NestedDataSet = new DataSetDefinition()
                    {
                        BitFields = new List<BitField>()
                        {
                            new BitField
                            {
                                Name = "ConfirmedPos",
                                BitFieldType = BitFieldType.UInt8,
                                Length = 8,
                                Comment =
                                    "Optional: Confirmed position of unreachable cars; an array of 64 bits. Example: Bit 0 placed in octet(=1) a non addressable vehicle with UIC address 1"
                            },
                        }
                    }
                },
                new BitField
                {
                    Name = "NumCarsInTrain",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Total number of cars in train (all Consists)"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings() {Name = "NumCarsInTrain"},
                    NestedDataSet = UIC_CarDataSet
                },
            }
        };

        public static DataSetDefinition com103 => new DataSetDefinition
        {
            Name = "IPTDir Request",
            Comment =
                "If a device misses the transmission of one of the message data it may explicitly request it by sending the following message data to the IPTDir server. The response will be the re-quested message data as described in the previous chapters.",
            Identifiers = new Identifiers { Numeric = { 103 } },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ProtocolVersion",
                    BitFieldType = BitFieldType.HexString,
                    Length = 32,
                    Comment =
                        "The protocol version of IPTDir. The bytes represent version, release, update and evolution. The most significant byte (version) is used for protocol incompatibility. Version = 0x02020000 (V. 2.2.0.0, default)"
                },
                new BitField
                {
                    Name = "RequestType",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Type of request: 1 = Send IPT Info; response: see chapter 4.1 , 2 = Send UIC Info; response: see chapter 4.2",
                    LookupTable = new LookupTable
                    {
                        {"0", "INVALID"},
                        {"1", "Send IPT Info"},
                        {"2", "Send UIC Info"}
                    }
                },
                new BitField
                {
                    Name = "reserved",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "For future extensions (set to 0)"
                }
            }
        };

        public static DataSetDefinition com222 => new DataSetDefinition
        {
            Name = "Vehicle Name Output",
            Identifiers = new Identifiers { Numeric = { 222 } },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "VehicleName",
                    BitFieldType = BitFieldType.StringBigEndUtf16,
                    Length = 16 * 16
                }
            }
        };

        public static DataSetDefinition com223 => new DataSetDefinition
        {
            Name = "Vehicle Name Input",
            Identifiers = new Identifiers { Numeric = { 223 } },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "VehicleName",
                    BitFieldType = BitFieldType.StringBigEndUtf16,
                    Length = 16 * 16
                }
            }
        };

        /* this is not generic iptcom
        public static DataSetDefinition PSCCtrlOp => new DataSetDefinition
        {
            Name = "PSCCtrlOp",
            Identifiers = new Identifiers { Numeric = { 251200100 }
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "Reserved",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "CMessageLength",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
                new BitField
                {
                    Name = "CPrmMessage",
                    BitFieldType = BitFieldType.StringUtf8,
                    VariableLengthSettings = new VariableLengthSettings {Name = "CMessageLength", ScalingFactor = 8}
                }
            }
        };*/

        /* this is not generic iptcom
         * public static DataSetDefinition CCUOGlobalA => new DataSetDefinition
        {
            Name = "CCUOGlobalA",
            Identifiers = new Identifiers { Numeric = { 201100100 }
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ICcuLifeSign",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16
                },
                new BitField
                {
                    Name = "Reserved1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16
                },
                new BitField
                {
                    Name = "ITrainSpeed",
                    BitFieldType = BitFieldType.UInt16,
                    Length= 16
                },
                new BitField
                {
                    Name = "Reserved2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16
                },
                new BitField
                {
                    Name = "ISystemUtcTime",
                    BitFieldType = BitFieldType.UnixEpochUtc,
                    Length= 32
                },
                new BitField
                {
                    Name = "Reserved3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32
                },
                new BitField
                {
                    Name = "IDaylightTime",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "0 : No daylight saving time (Winter time) 1 : Daylight saving time present (Summer time) 255 : Invalid"
                },
                new BitField
                {
                    Name = "ITimeZone",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "Reserved4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16
                },
                new BitField
                {
                    Name = "IProjectID",
                    BitFieldType = BitFieldType.UInt16,
                    Length= 16
                },
                new BitField
                {
                    Name = "IProjectVariant",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "Reserved5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8
                },
                new BitField
                {
                    Name = "ITrainMode",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "0 : Unknown (after power on before any other conditions are met) 1 : In Service (Driver logged in and PIS trip is running) 2 : Parked / Depot (when ITrainLocation = 2 or 3) 3 : Maintenance  (when maintainer is logged in to active cab) 4 : Out of Service (When no PIS trip is running, and ITrainMode not equal to 2 5 ... 255  : Reserved "
                },
                new BitField
                {
                    Name = "Reserved6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8*3
                },
                new BitField
                {
                    Name = "IRemSwDownLdMode",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "Reserved7",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8*15
                },
                new BitField
                {
                    Name = "ITempOutside",
                    BitFieldType = BitFieldType.Int16,
                    Length= 16
                },
                new BitField
                {
                    Name = "Reserved8",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8*2
                },
                new BitField
                {
                    Name = "IMissionCode",
                    BitFieldType = BitFieldType.StringAscii,
                    Length= 8*4
                },
                new BitField
                {
                    Name = "IContainer1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8*112
                },
                new BitField
                {
                    Name = "Reserved9",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8*4
                },
                new BitField
                {
                    Name = "IHeading",
                    BitFieldType = BitFieldType.Float32,
                    Length = 32
                },
                new BitField
                {
                    Name = "ILongitude",
                    BitFieldType = BitFieldType.Float32,
                    Length = 32
                },
                new BitField
                {
                    Name = "ILatitude",
                    BitFieldType = BitFieldType.Float32,
                    Length = 32
                },
                new BitField
                {
                    Name = "IAltitude",
                    BitFieldType = BitFieldType.Float32,
                    Length = 32
                },
                new BitField
                {
                    Name = "IDistanceTravelled",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
                new BitField
                {
                    Name = "ITrainSpeedStatus",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "ITrainLocation",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "ILineDir",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "IGPSRxrOffset",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "IGPSPositionFix",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "IDoorReleaseStatus",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "ILinePlatformID",
                    BitFieldType = BitFieldType.StringAscii,
                    Length= 8*8
                },
                new BitField
                {
                    Name = "IPrevStationID",
                    BitFieldType = BitFieldType.StringAscii,
                    Length= 8*8
                },
                new BitField
                {
                    Name = "INextStationID",
                    BitFieldType = BitFieldType.StringAscii,
                    Length= 8*8
                },
                new BitField
                {
                    Name = "IDestStationID",
                    BitFieldType = BitFieldType.StringAscii,
                    Length= 8*8
                },
                new BitField
                {
                    Name = "IContainer2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8*85
                },
                new BitField
                {
                    Name = "Reserved10",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8
                },
                new BitField
                {
                    Name = "IDrivDeskxOccup",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "IDrivDesk1Occup",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "IDrivDesk2Occup",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "Reserved11",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8
                },
                new BitField
                {
                    Name = "ITunnelPosition",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8
                },
                new BitField
                {
                    Name = "Reserved12",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8*3
                },
            }
        };*/
    }
}