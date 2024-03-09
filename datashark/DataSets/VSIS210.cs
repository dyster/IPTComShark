using System.Collections.Generic;
using BitDataParser;

namespace TrainShark.DataSets
{
    public class VSIS210 : DataSetCollection
    {
        public VSIS210()
        {
            var dictionary = new Dictionary<int, DataSetDefinition>
            {
                {230503000, TR_1}, // cab 1
                {230503100, TR_2},
                {230503200, TR_3},
                {230503300, TR_4},
                {230503400, TR_5},
                {230503500, TR_6},
                {230503700, OBU_1},
                {230503710, OBU_2},
                {230503720, OBU_3},
                {230503800, OBU_4},
                {230503810, OBU_7},
                {230503820, OBU_8},
                {230503830, OBU_9},
                {230503840, OBU_10},
                {230503850, OBU_11},
                {230503860, OBU_12},
                {230503870, OBU_13},
                {230503880, OBU_14},
                {230503890, OBU_15},
                {230503900, OBU_16},
                {230503910, OBU_17},
                {230503920, OBU_18},
                {230503930, OBU_19},
                {230503940, OBU_20},
                {230503950, OBU_21},
                {230503960, OBU_22},
                {230503970, OBU_23},
                {230503980, OBU_24},
                {230503990, OBU_25},
                {230500110, OBU_5},
                {230500111, OBU_6},
                {230503001, TR_1}, // cab 2
                {230503101, TR_2},
                {230503201, TR_3},
                {230503301, TR_4},
                {230503401, TR_5},
                {230503501, TR_6},
                {230503701, OBU_1},
                {230503711, OBU_2},
                {230503721, OBU_3},
                {230503801, OBU_4},
                {230503811, OBU_7},
                {230503821, OBU_8},
                {230503831, OBU_9},
                {230503841, OBU_10},
                {230503851, OBU_11},
                {230503861, OBU_12},
                {230503871, OBU_13},
                {230503881, OBU_14},
                {230503891, OBU_15},
                {230503901, OBU_16},
                {230503911, OBU_17},
                {230503921, OBU_18},
                {230503931, OBU_19},
                {230503941, OBU_20},
                {230503951, OBU_21},
                {230503961, OBU_22},
                {230503971, OBU_23},
                {230503981, OBU_24},
                {230503991, OBU_25},
                {230500112, OBU_5},
                {230500113, OBU_6},
                {230520040, MMI_AUX_IN},
                // cab 1                    // cab 2
                
                
                //{230531021, EVC_102},      {230536021, EVC_102}, specified redundant telegram, I don't think this is correct (in VSIS)
               
                //{230532080, IP_STM_206},   {230532080, IP_STM_206},
                //{230532090, IP_STM_208},   {230532090, IP_STM_208},
                //{230533030, IP_STM_207},   {230533030, IP_STM_207},

                {230520000, JRU_AUX_IN},
                {230520001, JRU_AUX_IN},
                
                {230510002, DIA_4},
                {230510003, DIA_5},
                {230510004, DIA_6},
                {230510005, DIA_7},
                {230510000, DIA_128},
                {230510010, DIA_129},
                
                
                {230510040, DIA_132},
                {230510050, DIA_133},
                {230510060, DIA_134},
                {230510070, DIA_135},
                {230510080, DIA_136},
                {230510090, DIA_137},
                {230510100, DIA_138},
                {230510110, DIA_139},
                //{230510110, DIA_140},
                {230510120, DIA_141},
                {230510130, DIA_142},
                {230510140, DIA_143},
                {230510150, DIA_144},
                {230510160, DIA_145},
                {230510170, DIA_146},

                {230510240, DIA_153},
                {230510250, DIA_154},
                {230510260, DIA_155},
                {230510270, DIA_156},
                {230510280, DIA_157},
                
                {230510300, DIA_159},
                {230510301, DIA_160},
                {230510302, DIA_161},
                {230510303, DIA_162},
                {230510304, DIA_163},
                {230510305, DIA_164},
                {230510306, DIA_165},
                
                {230510390, DIA_208},
                {230510410, DIA_210},
                {230510420, DIA_211},
                {230510441, DIA_215},
                {230511002, DIA_4},
                {230511003, DIA_5},
                {230511004, DIA_6},
                {230511005, DIA_7},
                {230511000, DIA_128},
                {230511010, DIA_129},
                
                {230511040, DIA_132},
                {230511050, DIA_133},
                {230511060, DIA_134},
                {230511070, DIA_135},
                {230511080, DIA_136},
                {230511090, DIA_137},
                {230511100, DIA_138},
                //{230511110, DIA_139},
                {230511110, DIA_140},
                {230511120, DIA_141},
                {230511130, DIA_142},
                {230511140, DIA_143},
                {230511150, DIA_144},
                {230511160, DIA_145},
                {230511170, DIA_146},
                
                {230511240, DIA_153},
                {230511250, DIA_154},
                {230511260, DIA_155},
                {230511270, DIA_156},
                {230511280, DIA_157},
                {230511300, DIA_159},
                {230511301, DIA_160},
                {230511302, DIA_161},
                {230511303, DIA_162},
                {230511304, DIA_163},
                {230511305, DIA_164},
                {230511306, DIA_165},
                
                {230511390, DIA_208},
                {230511410, DIA_210},
                {230511420, DIA_211},
                {230511441, DIA_215},
                {110, IPT_ECHO}
            };

            
            this.Name = "VSIS 2.10";
            this.Description = "Based on the VSIS 2.10 ICD for ETCS";
            
            foreach(var pair in dictionary)
            {
                if (DataSets.Exists(d => d.Name == pair.Value.Name))
                {
                    DataSets.Find(d => d.Name == pair.Value.Name).Identifiers.Numeric.Add(pair.Key);
                }
                else
                {
                    if(pair.Value.Identifiers == null)
                        pair.Value.Identifiers = new Identifiers();
                    pair.Value.Identifiers.Numeric.Add(pair.Key);
                    DataSets.Add(pair.Value);
                }
                
            }


        }

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

        


        public static BitField MMI_M_XATTRIBUTE => new BitField
        {
            Name = "MMI_M_XATTRIBUTE",
            Length = 1, // fixed to one iteration
            Comment = "Attributes for text messages",
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
                        LookupTable = new LookupTable
                        {
                            {"False", "Normal"},
                            {"True", "Counter phase"}
                        }
                    },
                    new BitField
                    {
                        Name = "Flashing Speed",
                        BitFieldType = BitFieldType.UInt16,
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
                        Name = "Background Colour",
                        BitFieldType = BitFieldType.UInt16,
                        Length = 3,
                        LookupTable = new LookupTable
                        {
                            {"0", "Black text"},
                            {"1", "White text"},
                            {"2", "Red text"},
                            {"3", "Blue text"},
                            {"4", "Green text"},
                            {"5", "Yellow text"},
                            {"6", "Light red text"},
                            {"7", "Light green text"}
                        }
                    },
                    new BitField
                    {
                        Name = "Text Colour",
                        BitFieldType = BitFieldType.UInt16,
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
                    }
                }
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




        public static BitField OBU_TR_M_Mode => new BitField
        {
            Name = "OBU_TR_M_Mode",
            BitFieldType = BitFieldType.UInt16,
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
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "User Data Main Version"
                },
                new BitField
                {
                    Name = "SDT_RES3",
                    BitFieldType = BitFieldType.UInt16,
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
        public static DataSetDefinition IPT_ECHO => new DataSetDefinition
        {
            Name = "IPT_ECHO IPT_ECHO",
            Comment = "ECHO telegram structure",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "IPT_ECHO_Cmd",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "ECHO Command. Either: \r\n1 – request (sent by end device) \r\n2 – reply (sent by the IPTCom Echo server) "
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

        #region Tr/Obu Telegrams

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition TR_1 => new DataSetDefinition
        {
            Name = "TR_1 TR_Telegram_1",
            Comment = "TR Telegram 1",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "TR_OBU_TrainSleep",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Sleeping input (normal)"
                },
                new BitField
                {
                    Name = "TR_OBU_TrainSleep_Not",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Sleeping input (antivalent)"
                },
                new BitField
                {
                    Name = "TR_OBU_PassiveShunting",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Passive shunting "
                },
                new BitField
                {
                    Name = "TR_OBU_NLEnabled",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Non leading"
                },
                new BitField
                {
                    Name = "TR_OBU_DirectionFW",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Direction forward"
                },
                new BitField
                {
                    Name = "TR_OBU_DirectionBW",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Direction backward"
                },
                new BitField
                {
                    Name = "TR_OBU_CabStatusA",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Cab A status"
                },
                new BitField
                {
                    Name = "TR_OBU_CabStatusB",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Cab B status"
                },
                new BitField
                {
                    Name = "TR_OBU_TrainType",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 4,
                    Comment = "Type of train",
                    LookupTable = new LookupTable
                    {
                        {"0", "Invalid train data type"},
                        {"1", "Fixed train data entry"},
                        {"2", "Flexible train data entry"},
                        {"3", "Switchable train data entry"}
                    }
                },
                new BitField
                {
                    Name = "TR_OBU_Traction_Status",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Traction status"
                },
                new BitField
                {
                    Name = "tr1_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr1_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr1_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "TR_OBU_BrakePressure",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Brake pressure, scaled 0.1 Bar"
                },
                new BitField
                {
                    Name = "TR_OBU_NTCIsolated",
                    BitFieldType = BitFieldType.HexString,
                    Length = 8,
                    Comment = "National system isolation"
                },
                new BitField
                {
                    Name = "TR_OBU_Brake_Status",
                    BitFieldType = BitFieldType.HexString,
                    Length = 8,
                    Comment = "Special brake status according [SS34] Ch. 2.3.6"
                },
                new BitField
                {
                    Name = "tr1_spare4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr1_spare5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr1_spare6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr1_spare7",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr1_spare8",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr1_spare9",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr1_spare10",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr1_spare11",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr1_spare12",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr1_spare13",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr1_spare14",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "ValidityTrainSleep",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityTrainSleepNot",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityPS",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityNL",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityFW",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityBW",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityCabA",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityCabB",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityTrainType",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValiditySpare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 3
                },
                new BitField
                {
                    Name = "ValidityTractionStatus",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValiditySpare4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 3
                },
                new BitField
                {
                    Name = "ValidityBrakePressure",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityNTCIsolated",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValidityBrakeStatus",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1
                },
                new BitField
                {
                    Name = "ValiditySpare5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 13
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_1 => new DataSetDefinition
        {
            Name = "OBU_1 OBU_Telegram_1",
            Comment = "OBU Telegram 1",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "OBU_TR_ServiceBrake",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Service brake order"
                },
                new BitField
                {
                    Name = "OBU_TR_EB3_Cmd",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment =
                        "Emergency brake order\r\nNote: Due to ETCS product-architectural reasons the 3rd variation of the brake interface (see SS119, Ch. 5.2.3.2.9 and Ch. 4.3.1.4.1) is not supported - default value is applied."
                },
                new BitField
                {
                    Name = "OBU_TR_TCO_Cmd",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Traction cut off"
                },
                new BitField
                {
                    Name = "OBU_TR_RBInhibit_Cmd",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Regenerative brake inhibition command f1)"
                },
                new BitField
                {
                    Name = "OBU_TR_MGInhibit_Cmd",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Magnetic shoe brake inhibition command f1)\r\n"
                },
                new BitField
                {
                    Name = "OBU_TR_ECSInhibit_Cmd",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Eddy current service brake inhibition command f1)"
                },
                new BitField
                {
                    Name = "OBU_TR_ECEInhibit_Cmd",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Eddy current emergency brake inhibition command f1)"
                },
                new BitField
                {
                    Name = "OBU_TR_AirTight_Cmd",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Air tightness command f1)"
                },
                new BitField
                {
                    Name = "OBU_TR_MPS_Cmd",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Main power switch open command f1)"
                },
                new BitField
                {
                    Name = "OBU_TR_PG_Cmd",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Pantograph down command f1)"
                },
                new BitField
                {
                    Name = "obu1_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu1_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu1_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu1_spare4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu1_spare5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu1_spare6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu1_spare7",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu1_spare8",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu1_spare9",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu1_spare10",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu1_spare11",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu1_spare12",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu1_spare13",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu1_spare14",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu1_spare15",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu1_spare16",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU1_Validity1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bits (byte 0-1)\r\n0 = OBU_TR_ServiceBrake\r\n1 = OBU_TR_EB3_Cmd\r\n2 = OBU_TR_TCO_Cmd\r\n3 = OBU_TR_RBInhibit_Cmd\r\n4 = OBU_TR_MGInhibit_Cmd\r\n5 = OBU_TR_ECSInhibit_Cmd\r\n6 = OBU_TR_ECEInhibit_Cmd\r\n7 = OBU_TR_AirTight_Cmd\r\n8 = OBU_TR_MPS_Cmd\r\n9 = OBU_TR_PG_Cmd\r\n10..15 not used (set to invalid)\r\nNote: all f1) are currently not used and set to invalid. Reserved for BL3 STM implementation"
                },
                new BitField
                {
                    Name = "OBU1_Validity2",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Validity bits byte 2-21\r\n0..15 not used (set to invalid) "
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition TR_2 => new DataSetDefinition
        {
            Name = "TR_2 TR_Telegram_2",
            Comment =
                "This telegram collects miscellaneous input signals from ETCS OB that are not available in subset 119.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "TR_OBU_TCO_FB",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Traction Cut-Off Feedback"
                },
                new BitField
                {
                    Name = "TR_OBU_ETCS_IS",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS Isolation"
                },
                new BitField
                {
                    Name = "TR_OBU_SB_FB",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Service brake command feedback"
                },
                new BitField
                {
                    Name = "TR_OBU_Tilting_Status",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Status of the vehicle's tilting system"
                },
                new BitField
                {
                    Name = "TR_OBU_Coasting",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment =
                        "Coasting signal\r\nNote: This variable means: \"Coasting is active at the moment\" (not \"it is general available in TCMS\")."
                },
                new BitField
                {
                    Name = "TR_OBU_EB1Cutoff_Status",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EB1 cut off status"
                },
                new BitField
                {
                    Name = "TR_OBU_EB2Cutoff_Status",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EB2 cut off status"
                },
                new BitField
                {
                    Name = "TR_OBU_BrakeTestPossible",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Status of Vehicle ready to perform a brake test"
                },
                new BitField
                {
                    Name = "TR_OBU_BrakeTestCommand",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Remote command to perform brake test"
                },
                new BitField
                {
                    Name = "tr2_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr2_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr2_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr2_spare4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 4,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "TR2_Trans_Cmd",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Level transition command."
                },
                new BitField
                {
                    Name = "TR2_UTC_offset",
                    BitFieldType = BitFieldType.Int16,
                    Length = 8,
                    Comment = "Offset to UTC time"
                },
                new BitField
                {
                    Name = "TR2_UTC_sec",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "UTC time in seconds since 1970-01-01, 00:00:00"
                },
                new BitField
                {
                    Name = "TR2_UTC_msec",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "msec of UTC time 0..999"
                },
                new BitField
                {
                    Name = "TR2_UIC_ContryCode",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "UIC country code"
                },
                new BitField
                {
                    Name = "TR2_UICVehicleNumber",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment =
                        "National Block of the UIC Code, i.e. the Type Number and the Serial Number of the vehicle to be used by the ETCS OBU."
                },
                new BitField
                {
                    Name = "tr2_spare5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr2_spare6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "TR2_Validity1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bits\r\nFor the GPP connection,  \r\n0 = TR_OBU_TCO_FB\r\n1 = TR_OBU_ETCS_IS\r\n2 = TR_OBU_SB_FB\r\n3 = TR_OBU_Tilting_Status\r\n4 = TR_OBU_Coasting\r\n5 = TR_OBU_EB1Cutoff_Status\r\n6 = TR_OBU_EB2Cutoff_Status\r\n7 = TR_OBU_BrakeTestPossible\r\n8 = TR_OBU_BrakeTestCommand\r\n9..15 =  not used (set to invalid)"
                },
                new BitField
                {
                    Name = "TR2_Validity2",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bits \r\n0 = TR2_Trans_Cmd\r\n1 = TR2_UTC_offset\r\n2 = TR2_UTC_sec\r\n3 = TR2_UTC_msec\r\n4 = TR2_UIC_ContryCode\r\n5 = TR2_UICVehicleNumber\r\n6..15 not used (set to invalid) "
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition TR_3 => new DataSetDefinition
        {
            Name = "TR_3 TR_Telegram_3",
            Comment = "The signals in this telegram are inputs to the group STM (PZB/LZB).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "TR_OBU_Panto_Status",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Pantograph status\r\nNote: contains the Panto Status of the whole train."
                },
                new BitField
                {
                    Name = "TR_OBU_PassengEB_Status",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Passenger EB Status"
                },
                new BitField
                {
                    Name = "TR_OBU_BTButtonCab1_N",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB/LZB button \"Befehlstaste\", cab 1"
                },
                new BitField
                {
                    Name = "TR_OBU_FTButtonCab1_N",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB/LZB button \"Freitaste\", cab 1"
                },
                new BitField
                {
                    Name = "TR_OBU_LZBNothaltCab1_N",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "LZB switch \"Nothalt\", cab 1"
                },
                new BitField
                {
                    Name = "TR_OBU_WTButtonCab1_N",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB/LZB button \"Wachsamkeitstaste\", cab 1"
                },
                new BitField
                {
                    Name = "TR_OBU_BTButtonCab2_N",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB/LZB button \"Befehlstaste\" cab 2"
                },
                new BitField
                {
                    Name = "TR_OBU_FTButtonCab2_N",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB/LZB button \"Freitaste\", cab 2"
                },
                new BitField
                {
                    Name = "TR_OBU_LZBNothaltCab2_N",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "LZB switch \"Nothalt\", cab 2"
                },
                new BitField
                {
                    Name = "TR_OBU_WTButtonCab2_N",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB/LZB button \"Wachsamkeitstaste\", cab 2"
                },
                new BitField
                {
                    Name = "TR_OBU_IsoSwPZB_N",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB isolation switch"
                },
                new BitField
                {
                    Name = "TR_OBU_IsoSwLZB_N",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "LZB isolation switch"
                },
                new BitField
                {
                    Name = "tr3_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 4,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr3_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr3_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr3_spare4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr3_spare5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr3_spare6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr3_spare7",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "TR3_Validity1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bits\r\n0 = TR_OBU_Panto_Status     \r\n1 = TR_OBU_PassengEB_Status \r\n2 = TR_OBU_BTButtonCab1_N   \r\n3 = TR_OBU_FTButtonCab1_N   \r\n4 = TR_OBU_LZBNothaltCab1_N \r\n5 = TR_OBU_WTButtonCab1_N   \r\n6 = TR_OBU_BTButtonCab2_N   \r\n7 = TR_OBU_FTButtonCab2_N   \r\n8 = TR_OBU_LZBNothaltCab2_N \r\n9 =  TR_OBU_WTButtonCab2_N   \r\n10 = TR_OBU_IsoSwPZB_N       \r\n11 = TR_OBU_IsoSwLZB_N       \r\n12..15 not used (set to invalid)"
                },
                new BitField
                {
                    Name = "TR3_Validity2",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Validity bits\r\n0..15 not used (set to invalid)"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition TR_4 => new DataSetDefinition
        {
            Name = "TR_4 TR_Telegram_4",
            Comment =
                "The signals in this telegram are inputs to the group STM (PZB/LZB). The signals are antivalent. All inverted signals (I) are included in this telegram. All the normal signals (N) are included in TR Telegram 3.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "TR_OBU_BTButtonCab1_I",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB/LZB button \"Befehlstaste\", cabin 1"
                },
                new BitField
                {
                    Name = "TR_OBU_FTButtonCab1_I",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB/LZB button \"Freitaste\", cabin 1"
                },
                new BitField
                {
                    Name = "TR_OBU_LZBNothaltCab1_I",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "LZB switch \"Nothalt\", cabin 1"
                },
                new BitField
                {
                    Name = "TR_OBU_WTButtonCab1_I",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB/LZB button \"Wachsamkeitstaste\", cabin 1"
                },
                new BitField
                {
                    Name = "TR_OBU_BTButtonCab2_I",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB/LZB button \"Befehlstaste\", cabin 2"
                },
                new BitField
                {
                    Name = "TR_OBU_FTButtonCab2_I",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB/LZB button \"Freitaste\", cabin 2"
                },
                new BitField
                {
                    Name = "TR_OBU_LZBNothaltCab2_I",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "LZB switch \"Nothalt\", cabin 2"
                },
                new BitField
                {
                    Name = "TR_OBU_WTButtonCab2_I",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB/LZB button \"Wachsamkeitstaste\", cab 2"
                },
                new BitField
                {
                    Name = "TR_OBU_IsoSwPZB_I",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB isolation switch"
                },
                new BitField
                {
                    Name = "TR_OBU_IsoSwLZB_I",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "LZB isolation switch"
                },
                new BitField
                {
                    Name = "tr4_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr4_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr4_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 4,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr4_spare4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr4_spare5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr4_spare6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr4_spare7",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr4_spare8",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr4_spare9",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "TR4_Validity1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bits \r\n0 = TR_OBU_BTButtonCab1_I\r\n1 = TR_OBU_FTButtonCab1_I\r\n2 = TR_OBU_LZBNothaltCab1_I\r\n3 = TR_OBU_WTButtonCab1_I\r\n4 = TR_OBU_BTButtonCab2_I\r\n5 = TR_OBU_FTButtonCab2_I\r\n6 = TR_OBU_LZBNothaltCab2_I\r\n7 = TR_OBU_WTButtonCab2_I\r\n8 = TR_OBU_IsoSwPZB_I\r\n9 = TR_OBU_IsoSwLZB_I\r\n10..15 not used (set to invalid)"
                },
                new BitField
                {
                    Name = "TR4_Validity2",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Validity bits\r\n0..15 not used (set to invalid) "
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition TR_5 => new DataSetDefinition
        {
            Name = "TR_5 TR_Telegram_5",
            Comment = "GPS coordinates\r\nNote: This telegram does not use SDTv2.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "XGPSLat",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment =
                        "GPS Latitude\r\nBits latitude \r\n31 1=Data valid\r\n30 1=North,0 = South\r\n22-29  Degrees\r\n16-21  Minutes      \r\n10-15  Seconds\r\n  0-9    Milliseconds\r\n  0-65535"
                },
                new BitField
                {
                    Name = "XGPSLon",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment =
                        "GPS Longitude\r\nBits longitude \r\n31 1=Data valid\r\n30 1=West, 0 = East\r\n22-29  Degrees\r\n16-21  Minutes      \r\n10-15  Seconds\r\n0-  9  Milliseconds\r\n0 – 65535"
                },
                new BitField
                {
                    Name = "tr5_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr5_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr5_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr5_spare4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr5_spare5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr5_spare6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 4,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr5_spare7",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr5_spare8",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "TR5_Validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Validity of the entire dataset"
                },
                new BitField
                {
                    Name = "tr5_spare81",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare (replacement for SSW1)"
                },
                new BitField
                {
                    Name = "tr5_spare9",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare (replacement for SSW2)"
                },
                new BitField
                {
                    Name = "tr5_spare10",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare (replacement for SSW3)"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition TR_6 => new DataSetDefinition
        {
            Name = "TR_6 TR_Telegram_6",
            Comment = "TIMS",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "TIMS_IN1",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "TIMS Input signal 1"
                },
                new BitField
                {
                    Name = "TIMS_IN2",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "TIMS Input signal 2"
                },
                new BitField
                {
                    Name = "TIMS_BrPipe_IsAvailable",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Config flag: TIMS brake pressure can be used or not."
                },
                new BitField
                {
                    Name = "TIMS_BrPipe_Pressure_Hi",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment =
                        "Logic signal from TIMS if Brake Pipe pressure is \"high\", i.e. above a TIMS configurable reference level"
                },
                new BitField
                {
                    Name = "TIMS_BrPipe_Pressure_Valid",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Validity of the signal TIMS_BrPipe_Pressure_Hi (see above) \r\n"
                },
                new BitField
                {
                    Name = "tr6_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr6_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "TIMS_Battery_Capacity",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Remaining capacity of the TIMS battery "
                },
                new BitField
                {
                    Name = "TIMS_BrPipe_Pressure",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Brake Pipe Pressure measured by TIMS device sensor."
                },
                new BitField
                {
                    Name = "tr6_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr6_spare4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr6_spare5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr6_spare6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr6_spare7",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr6_spare8",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr6_spare9",
                    BitFieldType = BitFieldType.Spare,
                    Length = 4,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr6_spare10",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "tr6_spare11",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "TR6_Validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Validity of the entire dataset"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_2 => new DataSetDefinition
        {
            Name = "OBU_2 OBU_Telegram_2",
            Comment = "Reserved for future use.",
            BitFields = new List<BitField>()
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_3 => new DataSetDefinition
        {
            Name = "OBU_3 OBU_Telegram_3",
            Comment = "Reserved for future use.",
            BitFields = new List<BitField>()
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_4 => new DataSetDefinition
        {
            Name = "OBU_4 OBU_Telegram_4",
            Comment =
                "The signals in this telegram are outputs from the generic ETCS OB R4 system. This telegram collects miscellaneous output signals to the train.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "OBU_TR_ModeContact",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment =
                        "ETCS mode status\r\nNote: this variable has historical reasons. It will be set to TRUE in case OBU_TR_M_Mode = 0, which means \"FS - Full Supervision”. Use OBU_TR_M_Mode instead of this variable."
                },
                new BitField
                {
                    Name = "OBU_TR_EBTestInProgress",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EB test in progress"
                },
                new BitField
                {
                    Name = "OBU_TR_EB_Status",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EB status"
                },
                new BitField
                {
                    Name = "OBU_TR_RadioStatus",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Radio status information"
                },
                new BitField
                {
                    Name = "OBU_TR_STM_HS_ENABLED",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "STM in HS state exists"
                },
                new BitField
                {
                    Name = "OBU_TR_STM_DA_ENABLED",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "STM in DA state exists"
                },
                new BitField
                {
                    Name = "obu4_spare0",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU_TR_BrakeTest_Status",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "ETCS brake test status\r\n"
                },
                new BitField
                {
                    Name = "OBU_TR_M_Level",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "ETCS level"
                },
                OBU_TR_M_Mode,
                OBU_TR_O_TRAIN,
                new BitField
                {
                    Name = "OBU_TR_BrakeTestTimeOut",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Brake test timeout value"
                },
                new BitField
                {
                    Name = "OBU_TR_M_ADHESION",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Current applied adhesion status"
                },
                new BitField
                {
                    Name = "OBU_TR_NID_STM_HS",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "NID_STM in HS state"
                },
                new BitField
                {
                    Name = "OBU_TR_NID_STM_DA",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "NID_STM in DA state"
                },
                new BitField
                {
                    Name = "obu4_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu4_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu4_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu4_spare4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU4_Validity1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bits\r\n0 = OBU_TR_ModeContact\r\n1 = OBU_TR_EBTestInProgress\r\n2 = OBU_TR_EB_Status\r\n3 = OBU_TR_RadioStatus\r\n4 = OBU_TR_STM_HS_ENABLED\r\n5 = OBU_TR_STM_DA_ENABLED\r\n6..15 = not used (set to invalid)"
                },
                new BitField
                {
                    Name = "OBU4_Validity2",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bits\r\n0 = OBU_TR_BrakeTest_Status \r\n1 = OBU_TR_M_Level \r\n2 = OBU_TR_M_Mode \r\n3 = OBU_TR_O_TRAIN\r\n4 = OBU_TR_BrakeTestTimeOut \r\n5 = OBU_TR_M_ADHESION \r\n6 = OBU_TR_NID_STM_HS \r\n7 = OBU_TR_NID_STM_DA \r\n8..15 not used (set to invalid)"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };


        public static DataSetDefinition OBU_5 => new DataSetDefinition
        {
            Name = "OBU_5 OBU_Telegram_5",
            Comment =
                "The signals in this telegram are outputs from the generic ETCS OB R4 system. This telegram consists of packet 44 as defined by ref. [SS026]. The packet is always transferred transparently through the ETCS system to the train independent on NID_XUSER. Each received packet 44 from wayside results in a new transmission to the train.\r\nIn general the size of packet 44 must not be bigger than the maximum capacity of the balise regardless if it comes from RBC or balises.\r\nNote: The telegram uses message data for transmission on MVB. It is not for ETCS use. It can contain e. g. \"current limitation\".",
            BitFields = Subset26.Packet44TrackToTrain.BitFields
        };

        // checked 2019-11-28 to 2.13 JS
        public static DataSetDefinition OBU_6 => new DataSetDefinition
        {
            Name = "OBU_6 OBU_Telegram_6",
            Comment =
                "This packet contains trackside information concerning the Most restrictive Speed profile (MRSP) and the Gradient Profile. Whenever new information is received from trackside, the speed profile and the gradient profile shall be sent to the ATO/ AFB system of the vehicle. The Gradient Profile is used both by ATO (see [SS_130]) and by ASC.\r\nNote: The telegram uses message data for transmission on MVB.",
            BitFields = new List<BitField>
            {
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
                    VariableLengthSettings = new VariableLengthSettings()
                    {
                        Name = "MMI_N_MRSP"
                    },
                    NestedDataSet = new DataSetDefinition()
                    {
                        BitFields = new List<BitField>()
                        {
                            new BitField
                            {
                                Name = "MMI_V_MRSP",
                                BitFieldType = BitFieldType.Int16,
                                Length = 16,
                                Comment = "New speed value"
                            },
                            new BitField
                            {
                                Name = "obu6_spare1",
                                BitFieldType = BitFieldType.Spare,
                                Length = 16,
                                Comment = "Spare bits for alignment"
                            },
                            new BitField
                            {
                                Name = "MMI_O_MRSP",
                                BitFieldType = BitFieldType.Int32,
                                Length = 32,
                                Comment =
                                    "This is the position in odometer co-ordinates of the start location of a speed discontinuity in the most restrictive speed profile. This position can be adjusted depending on supervision."
                            },
                        }
                    }
                },
                
                new BitField
                {
                    Name = "MMI_G_GRADIENT_CURR",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Current gradient value"
                },
                new BitField
                {
                    Name = "MMI_N_GRADIENT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Number of grad. information fields"
                },
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings()
                    {
                        Name = "MMI_N_MRSP"
                    },
                    NestedDataSet = new DataSetDefinition()
                    {
                        BitFields = new List<BitField>()
                        {
                            new BitField
                            {
                                Name = "MMI_G_GRADIENT",
                                BitFieldType = BitFieldType.Int16,
                                Length = 16,
                                Comment = "New gradient value"
                            },
                            new BitField
                            {
                                Name = "obu6_spare2",
                                BitFieldType = BitFieldType.Spare,
                                Length = 16,
                                Comment = "Spare bits for alignment"
                            },
                            new BitField
                            {
                                Name = "MMI_O_GRADIENT",
                                BitFieldType = BitFieldType.Int32,
                                Length = 32,
                                Comment =
                                    "This is the position, in odometer co-ordinates, without tolerance correction, of the start location of a gradient value for a part of the track. The remaining distances shall be computed taking into account the estimated train front-end position."
                            }
                        }
                    }
                },
                
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_7 => new DataSetDefinition
        {
            Name = "OBU_7 OBU_Telegram_7",
            Comment = "This telegram contains the ETCS train data. Min cycle time is 128ms.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "OBU_NC_CD_TRAIN",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Cant Deficiency Train Category"
                },
                new BitField
                {
                    Name = "obu7_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU_TR_NC_TRAIN",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Other International Train Category"
                },
                new BitField
                {
                    Name = "MMI_L_TRAIN",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Train Length"
                },
                new BitField
                {
                    Name = "MMI_V_MAXTRAIN",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Max Train Speed"
                },
                Subset26.NID_OPERATIONAL,
                new BitField
                {
                    Name = "WheelDiam1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Wheel Diameter 1"
                },
                new BitField
                {
                    Name = "WheelDiam2",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Wheel Diameter 2"
                },
                new BitField
                {
                    Name = "RadarPulses",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "RadarPulses per km"
                },
                new BitField
                {
                    Name = "obu7_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU7_Validity1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Validity bits \r\n0 = OBU_NC_CD_TRAIN \r\n1..15 not used (set to invalid)"
                },
                new BitField
                {
                    Name = "OBU7_Validity2",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bits\r\n0 = OBU_TR_NC_TRAIN \r\n1 = MMI_L_TRAIN \r\n2 = MMI_V_MAXTRAIN \r\n3 = MMI_NID_OPERATION \r\n4 = WheelDiam1 \r\n5 = WheelDiam2 \r\n6 = RadarPulses \r\n7..15 not used (set to invalid) "
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_8 => new DataSetDefinition
        {
            Name = "OBU_8 OBU_Telegram_8",
            Comment =
                "ETCS MMI DYNAMIC is DMI information.\r\nNote: this packet structure is identical to EVC-1 structure.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "OBU8_MMI_M_SLIP",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Slip status"
                },
                new BitField
                {
                    Name = "OBU8_MMI_M_SLIDE",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Slide status"
                },
                new BitField
                {
                    Name = "obu8_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU8_MMI_M_WARNING",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 4,
                    Comment = "Warning status"
                },
                new BitField
                {
                    Name = "obu8_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU8_V_TRAIN",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment =
                        "Current speed of the train\r\nNote: this is the same value like in EVC1, but only used as additional information (SIL0)."
                },
                new BitField
                {
                    Name = "OBU8_A_TRAIN",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Current acceleration of the train"
                },
                new BitField
                {
                    Name = "OBU8_V_TARGET",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Speed restr. at current target"
                },
                new BitField
                {
                    Name = "OBU8_V_PERMITTED",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Current permitted speed "
                },
                new BitField
                {
                    Name = "OBU8_V_RELEASE",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Release speed applied at target"
                },
                new BitField
                {
                    Name = "OBU8_O_BRAKETARGET",
                    BitFieldType = BitFieldType.Int32,
                    Length = 32,
                    Comment =
                        "This is the position in odometer co-ordinates of the next restrictive discontinuity of the static speed profile or target, which has influence on the braking curve. This position can be adjusted depending on supervision."
                },
                new BitField
                {
                    Name = "OBU8_O_IML",
                    BitFieldType = BitFieldType.Int32,
                    Length = 32,
                    Comment =
                        "This is the location in odometer co-ordinates of the indication marker for the next brake target. This position can be adjusted depending on supervision."
                },
                new BitField
                {
                    Name = "OBU8_V_INTERVENTION",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Current intervention speed"
                },
                new BitField
                {
                    Name = "OBU8_Validity1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bits byte 0-1\r\n0 = OBU8_MMI_M_SLIP \r\n1 = OBU8_MMI_M_SLIDE\r\n2..3 = not used (set to invalid)\r\n4 = OBU8_MMI_M_WARNING \r\n5..15 not used (set to invalid)"
                },
                new BitField
                {
                    Name = "OBU8_Validity2",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bits byte 2-21\r\n0 = OBU8_V_TRAIN \r\n1 = OBU8_A_TRAIN \r\n2 = OBU8_V_TARGET \r\n3 = OBU8_V_PERMITTED \r\n4 = OBU8_V_RELEASE \r\n5 = OBU8_O_BRAKETARGET\r\n6 = OBU8_O_IML\r\n7 = OBU8_V_INTERVENTION\r\n8..15 not used (set to invalid) "
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_9 => new DataSetDefinition
        {
            Name = "OBU_9 OBU_Telegram_9",
            Comment = "STM_MMI dynamic data\r\nNote: This telegram does not use SDTv2 and no validity bits.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "Q_IND_SCALE",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Q_SCALE - Distance Scale - 2 bits [0-1]\r\nValues:\r\n0 = \"scale of target distance D_TARGET, 1 = 10cm\"\r\n1 = \"scale of target distance D_TARGET, 1 = 1m\"\r\n2 = \"scale of target distance D_TARGET, 1 = 10m\"\r\n3 = \"reserved, to be ignored\"\r\nQ_INDICATE - Indication which DMI objects to show - 12 bits [2-13]\r\nBits (1 = true, 0 = false):\r\n0 = \"unknown\"\r\n1 = \"inhibit Vpermitted\"\r\n2 = \"inhibit Vtarget\"\r\n3 = \"inhibit Dtarget\"\r\n4 = \"inhibit Vinterv\"\r\n5 = \"indicate Slippery Track\"\r\n6 = \"inhibit Vrelease\"\r\n7 = \"inhibit Warning Status\"\r\n8 = \"inhibit Ind status\"\r\n9 = \"indicate Track Data/ Condition\"\r\n10 = \"indicate Override status\"\r\n11 = \"indicate ETCS Override Req\"\r\nQ_WARNINGLIMIT - Indication whether or not to show speed warning - 1 bit [14]\r\nValues:\r\n0 = \"no warning\"\r\n1 = \"warning to be displayed\"\r\nQ_INDICATIONLIMIT - Indication whether or not to show indication status - 1 bit [15]\r\nValues:\r\n0 = \"no Indication Status to be displayed\"\r\n1 = \"Indication Status to be displayed\""
                },
                new BitField
                {
                    Name = "LifeSignal808",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Life Signal"
                },
                new BitField
                {
                    Name = "OBU9_NID_NTC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Identity of the STM that sent the data"
                },
                new BitField
                {
                    Name = "V_PERMIT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Permitted Speed"
                },
                new BitField
                {
                    Name = "D_TARGET",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Target Distance"
                },
                new BitField
                {
                    Name = "V_TARGET",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Target Speed"
                },
                new BitField
                {
                    Name = "V_RELEASE",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "V_Release"
                },
                new BitField
                {
                    Name = "V_INTERV",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Intervention Speed"
                },
                new BitField
                {
                    Name = "N_ITER_DYN",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment =
                        "Indicator for the amount of the following STM-specific information (M_SUPs). A number greater 5 indicates that additional STM-specific Info is following in the extra port for STM_MMI_DYN_SUP."
                },
                new BitField
                {
                    Name = "M_SUP_0",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "STM-specific Info"
                },
                new BitField
                {
                    Name = "M_SUP_1",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "STM-specific Info"
                },
                new BitField
                {
                    Name = "M_SUP_2",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "STM-specific Info"
                },
                new BitField
                {
                    Name = "M_SUP_3",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "STM-specific Info"
                },
                new BitField
                {
                    Name = "M_SUP_4",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "STM-specific Info"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_10 => new DataSetDefinition
        {
            Name = "OBU_10 OBU_Telegram_10",
            Comment = "STM_MMI dynamic data supplement\r\nNote: This telegram does not use SDTv2 and no validity bits.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "LifeSign",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Life Sign"
                },
                new BitField
                {
                    Name = "OBU10_NID_NTC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Identity of the STM that sent the data"
                },
                new BitField
                {
                    Name = "obu10_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "M_SUP_5",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "STM-specific Info"
                },
                new BitField
                {
                    Name = "M_SUP_6",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "STM-specific Info"
                },
                new BitField
                {
                    Name = "M_SUP_7",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "STM-specific Info"
                },
                new BitField
                {
                    Name = "M_SUP_8",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "STM-specific Info"
                },
                new BitField
                {
                    Name = "M_SUP_9",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "STM-specific Info"
                },
                new BitField
                {
                    Name = "M_SUP_10",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "STM-specific Info"
                },
                new BitField
                {
                    Name = "M_SUP_11",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "STM-specific Info"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_11 => new DataSetDefinition
        {
            Name = "OBU_11 OBU_Telegram_11",
            Comment = "ETCS Balise Passage Report",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "NID_LVBG",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "NID_C + NID_BG \r\nIdentity of the passed balise group: Country Code + Balise group"
                },
                new BitField
                {
                    Name = "D_ADJUST",
                    BitFieldType = BitFieldType.Int32,
                    Length = 32,
                    Comment = "Linking distance from previous LRBG "
                },
                new BitField
                {
                    Name = "OBU11_D_MAX",
                    BitFieldType = BitFieldType.Int32,
                    Length = 32,
                    Comment = "D_MAX value of the of the NID_LVBG\r\nRaw from the Odometer (STM-8)"
                },
                new BitField
                {
                    Name = "OBU11_D_NOM",
                    BitFieldType = BitFieldType.Int32,
                    Length = 32,
                    Comment = "Nominal odometer value of NID_LVBG \r\nRaw from the Odometer (STM-8) "
                },
                new BitField
                {
                    Name = "OBU11_D_MIN",
                    BitFieldType = BitFieldType.Int32,
                    Length = 32,
                    Comment = "D_MIN value of the of the NID_LVBG\r\nRaw from the Odometer (STM-8)"
                },
                new BitField
                {
                    Name = "O_TRAIN",
                    BitFieldType = BitFieldType.Int32,
                    Length = 32,
                    Comment = "Nominal odometer value of NID_LVBG in the ETCS BA and MMI co-ordinate system"
                },
                new BitField
                {
                    Name = "Q_LOCACC",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Location accuracy of the NID_LVBG"
                },
                new BitField
                {
                    Name = "OBU11_Validity1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Validity bits\r\n0 = Validity of the entire dataset\r\n1..7 = not used, set to invalid"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_12 => new DataSetDefinition
        {
            Name = "OBU_12 OBU_Telegram_12",
            Comment = "Odometer Data",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "T_ODO",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Timestamp"
                },
                new BitField
                {
                    Name = "OBU12_D_MAX",
                    BitFieldType = BitFieldType.Int32,
                    Length = 32,
                    Comment = "MAX_ODO, Positive direction side of confidence interval"
                },
                new BitField
                {
                    Name = "OBU12_D_NOM",
                    BitFieldType = BitFieldType.Int32,
                    Length = 32,
                    Comment = "NOM_ODO, nominal value of distance"
                },
                new BitField
                {
                    Name = "OBU12_D_MIN",
                    BitFieldType = BitFieldType.Int32,
                    Length = 32,
                    Comment = "MIN_ODO, Negative direction side of confidence interval"
                },
                new BitField
                {
                    Name = "V_MAX",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Upper bound of the measured speed"
                },
                new BitField
                {
                    Name = "V_NOM",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Nominal speed value"
                },
                new BitField
                {
                    Name = "V_MIN",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Lower bound of the measured speed"
                },
                new BitField
                {
                    Name = "D_RES",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "RES_ODO, resolution of distance measurement"
                },
                new BitField
                {
                    Name = "T_Radar_Plausible",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "remaining time of radar plausibility"
                },
                new BitField
                {
                    Name = "Q_SAFEDIR",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Ambiguous Direction"
                },
                new BitField
                {
                    Name = "Q_NOM_ODO",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Indicate if the odometer configuration is nominal or not"
                },
                new BitField
                {
                    Name = "obu12_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu12_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 4,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU12_Validity1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Validity bits\r\n0 = Validity of the entire dataset\r\n1..7 = not used, set to invalid"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_13 => new DataSetDefinition
        {
            Name = "OBU_13 OBU_Telegram_13",
            Comment = "ETCS ASC Information",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ASC_D_BRI",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Distance to start of braking curve. \r\n(Distance from nominal train front to the position where V_Permitted starts to decrease, i.e. where the permitted speed intersects with the ceiling speed) \r\nIf  the V_Permitted is derived from the EBI curve this variable will be dependent on the confidence interval"
                },
                new BitField
                {
                    Name = "ASC_V_Permitted",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Permitted Speed \r\n(as calculated by ETCS, with all gradients ahead considered)"
                },
                new BitField
                {
                    Name = "ASC_D_Target",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment =
                        "Target Distance \r\n(For Non-Zero Targets: Distance from nominal train front to the position where the curve of permitted speed crosses the curve of target speed \r\nFor Zero-Targets: Distance from nominal train front to target position)"
                },
                new BitField
                {
                    Name = "ASC_V_Target",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Target Speed \r\n(Changes when the nominal train front position passes the current target.)"
                },
                new BitField
                {
                    Name = "ASC_V_Release",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Release Speed \r\n(for the given Brake Target, as received from Movement Authority)"
                },
                new BitField
                {
                    Name = "ASC_V_TARGET_SPEED_MARGIN",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "EB/SB margin used for the current supervised target, i.e if ASC_V_PERMITTED is derived from EBI curve, EB margin is provided otherwise SB margin"
                },
                new BitField
                {
                    Name = "ASC_B_HiddenTarget",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Hidden target speed restriction beyond current target"
                },
                new BitField
                {
                    Name = "obu13_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu13_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu13_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 4,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu13_spare4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu13_spare5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu13_spare6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU13_Validity1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Validity bits\r\n0 = Validity of the entire dataset\r\n1..15 = not used, set to invalid"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_14 => new DataSetDefinition
        {
            Name = "OBU_14 OBU_Telegram_14",
            Comment = "ETCS Brake Curve Characteristics",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ASC_V_CHAR1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Speed limit for ASC_A_CHAR0"
                },
                new BitField
                {
                    Name = "ASC_V_CHAR2",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Speed limit for ASC_A_CHAR1"
                },
                new BitField
                {
                    Name = "ASC_V_CHAR3",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Speed limit for ASC_A_CHAR2"
                },
                new BitField
                {
                    Name = "ASC_V_CHAR4",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Speed limit for ASC_A_CHAR3"
                },
                new BitField
                {
                    Name = "ASC_V_CHAR5",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Speed limit for ASC_A_CHAR4"
                },
                new BitField
                {
                    Name = "ASC_V_CHAR6",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Speed limit for ASC_A_CHAR5"
                },
                new BitField
                {
                    Name = "ASC_A_CHAR0",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "SB deceleration from 0 to ASC_V_CHAR1"
                },
                new BitField
                {
                    Name = "ASC_A_CHAR1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "SB deceleration from ASC_V_CHAR1 to ASC_V_CHAR2"
                },
                new BitField
                {
                    Name = "ASC_A_CHAR2",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "SB deceleration from  ASC_V_CHAR2 to ASC_V_CHAR3"
                },
                new BitField
                {
                    Name = "ASC_A_CHAR3",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "SB deceleration from ASC_V_CHAR3 to ASC_V_CHAR4"
                },
                new BitField
                {
                    Name = "ASC_A_CHAR4",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "SB deceleration from ASC_V_CHAR4 to ASC_V_CHAR5"
                },
                new BitField
                {
                    Name = "ASC_A_CHAR5",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "SB deceleration from \r\nASC_V_CHAR5 to ASC_V_CHAR6"
                },
                new BitField
                {
                    Name = "ASC_A_CHAR6",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "SB deceleration above \r\nASC_V_CHAR6"
                },
                new BitField
                {
                    Name = "obu14_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu14_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU14_Validity1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Validity bits\r\n0 = Validity of the entire dataset\r\n1..15 = not used, set to invalid"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_15 => new DataSetDefinition
        {
            Name = "OBU_15 OBU_Telegram_15",
            Comment = "LZB ASC Information",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "F_LZB_Tuerkontrolle",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 4,
                    Comment = "Door Control Information"
                },
                new BitField
                {
                    Name = "obu15_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 4,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "LZB_Oberstromgrenzwert",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "LZB Main Current Limitation"
                },
                new BitField
                {
                    Name = "LZB_XG",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Braking distance XG"
                },
                new BitField
                {
                    Name = "LZB_BRI",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Brake information: BRI warning distance"
                },
                new BitField
                {
                    Name = "LZB_Decel",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Brake deceleration"
                },
                new BitField
                {
                    Name = "LZB_ZielDunkel",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "LZB V-target/S-target  dim light"
                },
                new BitField
                {
                    Name = "PZB_ZugartO",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB Zugart \"O\""
                },
                new BitField
                {
                    Name = "PZB_ZugartM",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB Zugart \"M\""
                },
                new BitField
                {
                    Name = "PZB_ZugartU",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB Zugart \"U\""
                },
                new BitField
                {
                    Name = "LZB_LMUe",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "LZB Leuchtmelder Ü"
                },
                new BitField
                {
                    Name = "LZB_Ende",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "LZB  ENDE Indication"
                },
                new BitField
                {
                    Name = "LZB_Servicebrake",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "LZB Zwangsbetriebsbremse"
                },
                new BitField
                {
                    Name = "LZB_VerdeckteLA",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "LZB Hidden Speed Reduction"
                },
                new BitField
                {
                    Name = "LZB_ZDStatus",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 4,
                    Comment = "LZB Zugdaten Status"
                },
                new BitField
                {
                    Name = "LZB_Betriebsart",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 4,
                    Comment = "LZB BetriebsartMaster"
                },
                new BitField
                {
                    Name = "LZB_ZD_VMZ",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "LZB Train data: Vmax"
                },
                new BitField
                {
                    Name = "LZB_ZD_BRH",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "LZB Train data: Braking percentage"
                },
                new BitField
                {
                    Name = "LZB_ZD_ZL",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "LZB Train data: Train length"
                },
                new BitField
                {
                    Name = "LZB_ZD_BRA",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "LZB Train data: Brake Type (Bremsart) "
                },
                new BitField
                {
                    Name = "LZB_Stoerschalter",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "LZB Störschalter"
                },
                new BitField
                {
                    Name = "PZB_Stoerschalter",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "PZB Störschalter"
                },
                new BitField
                {
                    Name = "obu15_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu15_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu15_spare4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 4,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu15_spare5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu15_spare6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu15_spare7",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU15_Validity1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "bit using:\r\n0 = Validity of the entire dataset\r\n1..15 = not used, set to invalid"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_16 => new DataSetDefinition
        {
            Name = "OBU_16 OBU_Telegram_16",
            Comment =
                "This telegram contains the signals for the track condition types, change of traction system and change of allowed power consumption.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "OBU_TR_CTS_MaxSFEtoStart",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to change of traction system."
                },
                new BitField
                {
                    Name = "OBU_TR_CTSNewId",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Country identifier of the new traction system (NID_CTRACTION)"
                },
                new BitField
                {
                    Name = "OBU_TR_CTSNewVoltage",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "The new Traction System voltage (M_VOLTAGE)"
                },
                new BitField
                {
                    Name = "obu16_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU_TR_ACC_MaxSFEtoStart",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to change of allowed current consumption."
                },
                new BitField
                {
                    Name = "OBU_TR_ACCLimit",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "The new Allowed current consumption (M_CURRENT)"
                },
                new BitField
                {
                    Name = "obu16_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu16_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu16_spare4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu16_spare5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu16_spare6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU16_Validity1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bits\r\n0 = \"Validity of OBU_TR_CTS_MaxSFEtoStart\"\r\n1 = \"Validity of OBU_TR_CTSNewId\"\r\n2 = \"Validity of OBU_TR_CTSNewVoltage\"\r\n3 = not used (set to invalid)\r\n4 = \"Validity of OBU_TR_ACC_MaxSFEtoStart \"\r\n5 = \"Validity of OBU_TR_ACCLimit\"\r\n6..15 not used (set to invalid)"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_17 => new DataSetDefinition
        {
            Name = "OBU_17 OBU_Telegram_17",
            Comment =
                "This telegram contains the signals for 5 different track conditions of the type pantograph to be lowered.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "OBU_TR_PG_MaxSFEtoStart_1",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to Pantograph down start location 1."
                },
                new BitField
                {
                    Name = "OBU_TR_PG_MinSFEtoEnd_1",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to Pantograph down end location 1."
                },
                new BitField
                {
                    Name = "OBU_TR_PG_MaxSFEtoStart_2",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to Pantograph down start location 2."
                },
                new BitField
                {
                    Name = "OBU_TR_PG_MinSFEtoEnd_2",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to Pantograph down end location 2."
                },
                new BitField
                {
                    Name = "OBU_TR_PG_MaxSFEtoStart_3",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to Pantograph down start location 3."
                },
                new BitField
                {
                    Name = "OBU_TR_PG_MinSFEtoEnd_3",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to Pantograph down end location 3."
                },
                new BitField
                {
                    Name = "OBU_TR_PG_MaxSFEtoStart_4",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to Pantograph down start location 4."
                },
                new BitField
                {
                    Name = "OBU_TR_PG_MinSFEtoEnd_4",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to Pantograph down end location 4."
                },
                new BitField
                {
                    Name = "OBU_TR_PG_MaxSFEtoStart_5",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to Pantograph down start location 5."
                },
                new BitField
                {
                    Name = "OBU_TR_PG_MinSFEtoEnd_5",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to Pantograph down end location 5."
                },
                new BitField
                {
                    Name = "obu17_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU17_Validity",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bit for the respectively track condition 1-5\r\n0 = \"Validity of track condition 1\"\r\n1 = \"Validity of track condition 2\"\r\n2 = \"Validity of track condition 3\"\r\n3 = \"Validity of track condition 4\"\r\n4 = \"Validity of track condition 5\"\r\n5..15 not used (set to invalid)"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_18 => new DataSetDefinition
        {
            Name = "OBU_18 OBU_Telegram_18",
            Comment =
                "This telegram contains the signals for 5 different track conditions of the type main power switch to be switched off.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "OBU_TR_MPS_MaxSFEtoStart_1",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to main power switch to be switched off start location 1."
                },
                new BitField
                {
                    Name = "OBU_TR_MPS_MinSFEtoEnd_1",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to main power switch to be switched off end location 1."
                },
                new BitField
                {
                    Name = "OBU_TR_MPS_MaxSFEtoStart_2",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to main power switch to be switched off start location 2."
                },
                new BitField
                {
                    Name = "OBU_TR_MPS_MinSFEtoEnd_2",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to main power switch to be switched off end location 2."
                },
                new BitField
                {
                    Name = "OBU_TR_MPS_MaxSFEtoStart_3",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to main power switch to be switched off start location 3."
                },
                new BitField
                {
                    Name = "OBU_TR_MPS_MinSFEtoEnd_3",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to main power switch to be switched off end location 3."
                },
                new BitField
                {
                    Name = "OBU_TR_MPS_MaxSFEtoStart_4",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to main power switch to be switched off start location 4."
                },
                new BitField
                {
                    Name = "OBU_TR_MPS_MinSFEtoEnd_4",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to main power switch to be switched off end location 4."
                },
                new BitField
                {
                    Name = "OBU_TR_MPS_MaxSFEtoStart_5",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to main power switch to be switched off start location 5."
                },
                new BitField
                {
                    Name = "OBU_TR_MPS_MinSFEtoEnd_5",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to main power switch to be switched off end location 5."
                },
                new BitField
                {
                    Name = "obu18_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU18_Validity",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bit for the respectively track condition 1-5\r\n0 = \"Validity of track condition 1\"\r\n1 = \"Validity of track condition 2\"\r\n2 = \"Validity of track condition 3\"\r\n3 = \"Validity of track condition 4\"\r\n4 = \"Validity of track condition 5\"\r\n5..15 not used (set to invalid)"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_19 => new DataSetDefinition
        {
            Name = "OBU_19 OBU_Telegram_19",
            Comment = "This telegram contains the signals for 5 different track conditions of the type air tightness.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "TC_AirTight_MaxSFEtoStart_1",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to air tightness start location 1."
                },
                new BitField
                {
                    Name = "TC_AirTight_MinSREtoEnd_1",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to air tightness end location 1."
                },
                new BitField
                {
                    Name = "TC_AirTight_MaxSFEtoStart_2",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to air tightness start location 2."
                },
                new BitField
                {
                    Name = "TC_AirTight_MinSREtoEnd_2",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to air tightness end location 2."
                },
                new BitField
                {
                    Name = "TC_AirTight_MaxSFEtoStart_3",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to air tightness start location 3."
                },
                new BitField
                {
                    Name = "TC_AirTight_MinSREtoEnd_3",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to air tightness end location 3."
                },
                new BitField
                {
                    Name = "TC_AirTight_MaxSFEtoStart_4",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to air tightness start location 4."
                },
                new BitField
                {
                    Name = "TC_AirTight_MinSREtoEnd_4",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to air tightness end location 4."
                },
                new BitField
                {
                    Name = "TC_AirTight_MaxSFEtoStart_5",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to air tightness start location 5."
                },
                new BitField
                {
                    Name = "TC_AirTight_MinSREtoEnd_5",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to air tightness end location 5."
                },
                new BitField
                {
                    Name = "obu19_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU19_Validity",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bit for the respectively track condition 1-5\r\n0 = \"Validity of track condition 1\"\r\n1 = \"Validity of track condition 2\"\r\n2 = \"Validity of track condition 3\"\r\n3 = \"Validity of track condition 4\"\r\n4 = \"Validity of track condition 5\"\r\n5..15 not used (set to invalid)"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_20 => new DataSetDefinition
        {
            Name = "OBU_20 OBU_Telegram_20",
            Comment =
                "This telegram contains the signals for 5 different track conditions of the type inhibit of regenerative brake.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "TC_RBInhibit_MaxSFEtoStart_1",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of regenerative brake start location 1."
                },
                new BitField
                {
                    Name = "TC_RBInhibit_MinSREtoEnd_1",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of regenerative brake end location 1."
                },
                new BitField
                {
                    Name = "TC_RBInhibit_MaxSFEtoStart_2",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of regenerative brake start location 2."
                },
                new BitField
                {
                    Name = "TC_RBInhibit_MinSREtoEnd_2",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of regenerative brake end location 2."
                },
                new BitField
                {
                    Name = "TC_RBInhibit_MaxSFEtoStart_3",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of regenerative brake start location 3."
                },
                new BitField
                {
                    Name = "TC_RBInhibit_MinSREtoEnd_3",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of regenerative brake end location 3."
                },
                new BitField
                {
                    Name = "TC_RBInhibit_MaxSFEtoStart_4",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of regenerative brake start location 4."
                },
                new BitField
                {
                    Name = "TC_RBInhibit_MinSREtoEnd_4",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of regenerative brake end location 4."
                },
                new BitField
                {
                    Name = "TC_RBInhibit_MaxSFEtoStart_5",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of regenerative brake start location 5."
                },
                new BitField
                {
                    Name = "TC_RBInhibit_MinSREtoEnd_5",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of regenerative brake end location 5."
                },
                new BitField
                {
                    Name = "obu20_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU20_Validity",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bit for the respectively track condition 1-5\r\n0 = \"Validity of track condition 1\"\r\n1 = \"Validity of track condition 2\"\r\n2 = \"Validity of track condition 3\"\r\n3 = \"Validity of track condition 4\"\r\n4 = \"Validity of track condition 5\"\r\n5..15 not used (set to invalid)"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_21 => new DataSetDefinition
        {
            Name = "OBU_21 OBU_Telegram_21",
            Comment =
                "This telegram contains the signals for 5 different track conditions of the type inhibit of magnetic shoe brake.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "TC_MGInhibit_MaxSFEtoStart_1",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of magnetic shoe brake start location 1."
                },
                new BitField
                {
                    Name = "TC_MGInhibit_MinSREtoEnd_1",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of magnetic shoe brake end location 1."
                },
                new BitField
                {
                    Name = "TC_MGInhibit_MaxSFEtoStart_2",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of magnetic shoe brake start location 2."
                },
                new BitField
                {
                    Name = "TC_MGInhibit_MinSREtoEnd_2",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to magnetic shoe brake end location 2."
                },
                new BitField
                {
                    Name = "TC_MGInhibit_MaxSFEtoStart_3",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to magnetic shoe brake start location 3."
                },
                new BitField
                {
                    Name = "TC_MGInhibit_MinSREtoEnd_3",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of magnetic shoe brake end location 3."
                },
                new BitField
                {
                    Name = "TC_MGInhibit_MaxSFEtoStart_4",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of magnetic shoe brake start location 4."
                },
                new BitField
                {
                    Name = "TC_MGInhibit_MinSREtoEnd_4",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of magnetic shoe brake end location 4."
                },
                new BitField
                {
                    Name = "TC_MGInhibit_MaxSFEtoStart_5",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of magnetic shoe brake start location 5."
                },
                new BitField
                {
                    Name = "TC_MGInhibit_MinSREtoEnd_5",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of magnetic shoe brake end location 5."
                },
                new BitField
                {
                    Name = "obu21_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU21_Validity",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bit for the respectively track condition 1-5\r\n0 = \"Validity of track condition 1\"\r\n1 = \"Validity of track condition 2\"\r\n2 = \"Validity of track condition 3\"\r\n3 = \"Validity of track condition 4\"\r\n4 = \"Validity of track condition 5\"\r\n5..15 not used (set to invalid)"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_22 => new DataSetDefinition
        {
            Name = "OBU_22 OBU_Telegram_22",
            Comment =
                "This telegram contains the signals for 5 different track conditions of the type inhibit of eddy current emergency brake.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "TC_ECEInhibit_MaxSFEtoStart_1",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current emergency brake start location 1."
                },
                new BitField
                {
                    Name = "TC_ECEInhibit_MinSREtoEnd_1",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current emergency brake end location 1."
                },
                new BitField
                {
                    Name = "TC_ECEInhibit_MaxSFEtoStart_2",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current emergency brake start location 2."
                },
                new BitField
                {
                    Name = "TC_ECEInhibit_MinSREtoEnd_2",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current emergency brake end location 2."
                },
                new BitField
                {
                    Name = "TC_ECEInhibit_MaxSFEtoStart_3",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current emergency brake start location 3."
                },
                new BitField
                {
                    Name = "TC_ECEInhibit_MinSREtoEnd_3",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current emergency brake end location 3."
                },
                new BitField
                {
                    Name = "TC_ECEInhibit_MaxSFEtoStart_4",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current emergency brake start location 4."
                },
                new BitField
                {
                    Name = "TC_ECEInhibit_MinSREtoEnd_4",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current emergency brake end location 4."
                },
                new BitField
                {
                    Name = "TC_ECEInhibit_MaxSFEtoStart_5",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current emergency brake start location 5."
                },
                new BitField
                {
                    Name = "TC_ECEInhibit_MinSREtoEnd_5",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current emergency brake end location 5."
                },
                new BitField
                {
                    Name = "obu22_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU22_Validity",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bit for the respectively track condition 1-5\r\n0 = \"Validity of track condition 1\"\r\n1 = \"Validity of track condition 2\"\r\n2 = \"Validity of track condition 3\"\r\n3 = \"Validity of track condition 4\"\r\n4 = \"Validity of track condition 5\"\r\n5..15 not used (set to invalid)"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_23 => new DataSetDefinition
        {
            Name = "OBU_23 OBU_Telegram_23",
            Comment =
                "This telegram contains the signals for 5 different track conditions of the type inhibit of eddy current service brake.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "TC_ECSInhibit_MaxSFEtoStart_1",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current service brake start location 1."
                },
                new BitField
                {
                    Name = "TC_ECSInhibit_MinSREtoEnd_1",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current service brake end location 1."
                },
                new BitField
                {
                    Name = "TC_ECSInhibit_MaxSFEtoStart_2",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current service brake start location 2."
                },
                new BitField
                {
                    Name = "TC_ECSInhibit_MinSREtoEnd_2",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current service brake end location 2."
                },
                new BitField
                {
                    Name = "TC_ECSInhibit_MaxSFEtoStart_3",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current service brake start location 3."
                },
                new BitField
                {
                    Name = "TC_ECSInhibit_MinSREtoEnd_3",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current service brake end location 3."
                },
                new BitField
                {
                    Name = "TC_ECSInhibit_MaxSFEtoStart_4",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current service brake start location 4."
                },
                new BitField
                {
                    Name = "TC_ECSInhibit_MinSREtoEnd_4",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current service brake end location 4."
                },
                new BitField
                {
                    Name = "TC_ECSInhibit_MaxSFEtoStart_5",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current service brake start location 5."
                },
                new BitField
                {
                    Name = "TC_ECSInhibit_MinSREtoEnd_5",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to inhibit of eddy current service brake end location 5."
                },
                new BitField
                {
                    Name = "obu23_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU23_Validity",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bit for the respectively track condition 1-5\r\n0 = \"Validity of track condition 1\"\r\n1 = \"Validity of track condition 2\"\r\n2 = \"Validity of track condition 3\"\r\n3 = \"Validity of track condition 4\"\r\n4 = \"Validity of track condition 5\"\r\n5..15 not used (set to invalid)"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_24 => new DataSetDefinition
        {
            Name = "OBU_24 OBU_Telegram_24",
            Comment =
                "This telegram contains the signals for 5 different track conditions of the type station platform.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "TC_PlatformPosition_1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 2,
                    Comment = "Platform 1 position (Q_PLATFORM)"
                },
                new BitField
                {
                    Name = "TC_PlatformPosition_2",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 2,
                    Comment = "Platform 2 position (Q_PLATFORM)"
                },
                new BitField
                {
                    Name = "TC_PlatformPosition_3",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 2,
                    Comment = "Platform 3 position (Q_PLATFORM)"
                },
                new BitField
                {
                    Name = "TC_PlatformPosition_4",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 2,
                    Comment = "Platform 4 position (Q_PLATFORM)"
                },
                new BitField
                {
                    Name = "TC_PlatformPosition_5",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 2,
                    Comment = "Platform 5 position (Q_PLATFORM)"
                },
                new BitField
                {
                    Name = "obu24_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 2,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "TC_PlatformHeight_1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 4,
                    Comment = "Height of platform 1 (M_PLATFORM)."
                },
                new BitField
                {
                    Name = "TC_PlatformHeight_2",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 4,
                    Comment = "Height of platform 2 (M_PLATFORM)."
                },
                new BitField
                {
                    Name = "TC_PlatformHeight_3",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 4,
                    Comment = "Height of platform 3 (M_PLATFORM)."
                },
                new BitField
                {
                    Name = "TC_PlatformHeight_4",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 4,
                    Comment = "Height of platform 4 (M_PLATFORM)."
                },
                new BitField
                {
                    Name = "TC_PlatformHeight_5",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 4,
                    Comment = "Height of platform 5 (M_PLATFORM)."
                },
                new BitField
                {
                    Name = "TC_Platform_MaxSFEtoStart_1",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to station platform start location 1."
                },
                new BitField
                {
                    Name = "TC_Platform_MinSFEtoEnd_1",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to station platform end location 1."
                },
                new BitField
                {
                    Name = "TC_Platform_MaxSFEtoStart_2",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to station platform start location 2."
                },
                new BitField
                {
                    Name = "TC_Platform_MinSFEtoEnd_2",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to station platform end location 2."
                },
                new BitField
                {
                    Name = "TC_Platform_MaxSFEtoStart_3",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to station platform start location 3."
                },
                new BitField
                {
                    Name = "TC_Platform_MinSFEtoEnd_3",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to station platform end location 3."
                },
                new BitField
                {
                    Name = "TC_Platform_MaxSFEtoStart_4",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to station platform start location 4."
                },
                new BitField
                {
                    Name = "TC_Platform_MinSFEtoEnd_4",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to station platform end location 4."
                },
                new BitField
                {
                    Name = "TC_Platform_MaxSFEtoStart_5",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to station platform start location 5."
                },
                new BitField
                {
                    Name = "TC_Platform_MinSFEtoEnd_5",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Remaining distance to station platform end location 5."
                },
                new BitField
                {
                    Name = "OBU24_Validity",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bits\r\n0 = PlatformPosition_1 \r\n1 = PlatformPosition_2 \r\n2 = PlatformPosition_3 \r\n3 = PlatformPosition_4 \r\n4 = PlatformPosition_5 \r\n5 = PlatformHeight_1\r\n6 = PlatformHeight_2\r\n7 = PlatformHeight_3\r\n8 = PlatformHeight_4\r\n9 = PlatformHeight_5\r\n10..15 not used (set to invalid)"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition OBU_25 => new DataSetDefinition
        {
            Name = "OBU_25 OBU_Telegram_25",
            Comment = "This telegram is dedicated for BL2 STM and other NTC outputs.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "OBU_TR_RBInhibit_BL2_Cmd",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Regenerative brake inhibition command from BL2 STM"
                },
                new BitField
                {
                    Name = "OBU_TR_MGInhibit_BL2_Cmd",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Magnetic shoe brake inhibition command from BL2 STM"
                },
                new BitField
                {
                    Name = "OBU_TR_EC_BL2_Cmd",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Eddy current inhibition command from BL2 STM"
                },
                new BitField
                {
                    Name = "OBU_TR_Airtight_BL2_Cmd",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Air tightness command from BL2 STM"
                },
                new BitField
                {
                    Name = "OBU_TR_MPS_BL2_Cmd",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Main power switch open command from BL2 STM"
                },
                new BitField
                {
                    Name = "OBU_TR_PG_BL2_Cmd",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Pantograph down command from BL2 STM"
                },
                new BitField
                {
                    Name = "OBU_TR_PassEBInhibit_BL2_Cmd",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Passenger emergency brake inhibition command from BL2 STM"
                },
                new BitField
                {
                    Name = "obu25_spare1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu25_spare2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu25_spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu25_spare4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu25_spare5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu25_spare6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu25_spare7",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "obu25_spare8",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "Spare bits for alignment"
                },
                new BitField
                {
                    Name = "OBU25_Validity",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment =
                        "Validity bits\r\n0 = OBU_TR_RBInhibit_BL2_Cmd\r\n1 = OBU_TR_MGInhibit_BL2_Cmd\r\n2 = OBU_TR_EC_BL2_Cmd\r\n3 = OBU_TR_Airtight_BL2_Cmd\r\n4 = OBU_TR_MPS_BL2_Cmd\r\n5 = OBU_TR_PG_BL2_Cmd\r\n6 = OBU_TR_PassEBInhibit_BL2_Cmd\r\n7..15 not used (set to invalid)"
                },
                SSW1,
                SSW2,
                SSW3
            }
        };

        #endregion

        // checked to current JRU spec 20200401 JS
        public static DataSetDefinition MMI_AUX_IN => new DataSetDefinition
        {
            Name = "MMI_AUX_IN MMI_AUX_IN",
            Comment =
                "From JRU/other to DMI with Fallback speed (shall be sent at least every 300 ms). \r\nNote: This is an external Device interface, not TCN aligned!",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "LifeSign",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "LifeSign"
                },
                new BitField
                {
                    Name = "Spare1",
                    BitFieldType = BitFieldType.HexString,
                    Length = 6,
                    Comment = "spare"
                },
                new BitField
                {
                    Name = "Fallback_Speed_valid",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Validity of current speed (Fallback speed)"
                },
                new BitField
                {
                    Name = "UTC Time Validity",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "spare"
                },
                new BitField
                {
                    Name = "Fallback_Speed",
                    BitFieldType = BitFieldType.Int16,
                    Length = 16,
                    Comment = "Fallback Speed given by JRU/other."
                },
                new BitField
                {
                    Name = "UTC_offset_R",
                    BitFieldType = BitFieldType.Int8,
                    Length = 8
                },
                new BitField
                {
                    Name = "Spare2",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "spare"
                },
                new BitField
                {
                    Name = "UTC_time_R",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32
                },
                new BitField
                {
                    Name = "Spare3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 40,
                    Comment = "spare"
                }
            }
        };

        #region STM Telegrams

        


        // TODO This dataset has not been checked yet!
        public static DataSetDefinition STM_206 => new DataSetDefinition
        {
            Name = "STM_206 MMI_VCM_DE_REQUEST",
            Comment =
                "This packet is used to request driver VMC (Velocità Modulo Condotta = SCMT max speed) data entry procedure by STM-SCMT. \r\n\r\nDirection of information: From STM to DMI",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "MMI_STM_NID_PACKET",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Packet identifier\r\nValue = 206"
                },
                new BitField
                {
                    Name = "MMI_STM_L_PACKET",
                    BitFieldType = BitFieldType.HexString,
                    Length = 13,
                    Comment = "Packet length"
                },
                new BitField
                {
                    Name = "MMI_STM_V_VMC_ACT",
                    BitFieldType = BitFieldType.HexString,
                    Length = 7,
                    Comment = "Actual value of the VMC speed"
                },
                new BitField
                {
                    Name = "MMI_STM_V_VMC_MIN",
                    BitFieldType = BitFieldType.HexString,
                    Length = 7,
                    Comment = "Lower bound of the VMC speed"
                },
                new BitField
                {
                    Name = "MMI_STM_V_VMC_MAX",
                    BitFieldType = BitFieldType.HexString,
                    Length = 7,
                    Comment = "Upper bound of the VMC speed"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition STM_208 => new DataSetDefinition
        {
            Name = "STM_208 MMI_STM_SCMT_TIME",
            Comment =
                "This packet is used for sending time (hour, minutes and seconds) to DMI when STM-SCMT is in DA. The packet is sent every second. \r\n\r\nDirection of information: From STM to DMI",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "MMI_STM_NID_PACKET",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Packet identifier\r\nValue = 208"
                },
                new BitField
                {
                    Name = "MMI_STM_L_PACKET",
                    BitFieldType = BitFieldType.HexString,
                    Length = 13,
                    Comment = "Packet length"
                },
                new BitField
                {
                    Name = "MMI_STM_T_HOUR",
                    BitFieldType = BitFieldType.HexString,
                    Length = 5,
                    Comment = "Official hour UTC (0 - 23)"
                },
                new BitField
                {
                    Name = "MMI_STM_T_MINUTES",
                    BitFieldType = BitFieldType.HexString,
                    Length = 6,
                    Comment = "Official minutes UTC"
                },
                new BitField
                {
                    Name = "MMI_STM_T_SECONDS",
                    BitFieldType = BitFieldType.HexString,
                    Length = 6,
                    Comment = "Official seconds UTC"
                }
            }
        };


        // TODO This dataset has not been checked yet!
        public static DataSetDefinition STM_207 => new DataSetDefinition
        {
            Name = "STM_207 MMI_VCM_DE_END",
            Comment =
                "This packet is used to send the result of driver VMC (Velocità Modulo Condotta) data entry procedure to STM-SCMT.\r\n\r\nDirection of information: From DMI to STM",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "MMI_STM_NID_PACKET",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Packet identifier\r\nValue = 207"
                },
                new BitField
                {
                    Name = "MMI_STM_L_PACKET",
                    BitFieldType = BitFieldType.HexString,
                    Length = 13,
                    Comment = "Packet length"
                },
                new BitField
                {
                    Name = "MMI_STM_V_VMC_ACT",
                    BitFieldType = BitFieldType.HexString,
                    Length = 7,
                    Comment = "Actual value of the VMC speed"
                }
            }
        };
        
        // TODO This dataset has not been checked yet!
        public static DataSetDefinition IP_STM_206 => new DataSetDefinition
        {
            Name = "IP_STM_206 MMI_VCM_DE_REQUEST_IPT",
            Comment =
                "This packet is used to request driver VMC (Velocità Modulo Condotta) data entry procedure by STM-SCMT. \r\n\r\nDirection of information: From STM to DMI",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "MMI_STM_V_VMC_ACT_IPT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Actual value of the VMC speed"
                },
                new BitField
                {
                    Name = "MMI_STM_V_VMC_MIN_IPT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Lower bound of the VMC speed"
                },
                new BitField
                {
                    Name = "MMI_STM_V_VMC_MAX_IPT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Upper bound of the VMC speed"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition IP_STM_208 => new DataSetDefinition
        {
            Name = "STM_208 MMI_STM_SCMT_TIME_IPT",
            Comment =
                "This packet is used for sending time (hour, minutes and seconds) to DMI when STM-SCMT is in DA. The packet is sent every second. \r\n\r\nDirection of information: From STM to DMI",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "MMI_STM_T_HOUR_IPT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Official hour UTC (0 - 23)"
                },
                new BitField
                {
                    Name = "MMI_STM_T_MINUTES_IPT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Official minutes UTC"
                },
                new BitField
                {
                    Name = "MMI_STM_T_SECONDS_IPT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Official seconds UTC"
                }
            }
        };

        

        

        

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition IP_STM_207 => new DataSetDefinition
        {
            Name = "STM_207 MMI_VCM_DE_END_IPT",
            Comment =
                "This packet is used to send the result of driver VMC (Velocità Modulo Condotta) data entry procedure to STM-SCMT.\r\n\r\nDirection of information: From DMI to STM",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "MMI_STM_V_VMC_ACT_IPT",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Actual value of the VMC speed"
                }
            }
        };

        #endregion
        
        

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition JRU_STATUS => new DataSetDefinition
        {
            Name = "JRU_STATUS",
            Comment = "The structure and content of JRU status information (JRU_STATUS telegram) to ETC.\r\n",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "LifeSign",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "LifeSign status port"
                },
                new BitField
                {
                    Name = "Unnamed",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "Spare"
                },
                new BitField
                {
                    Name = "FatalError",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Fatal error, Sum error flag; to be set when data recording is not possible"
                },
                new BitField
                {
                    Name = "Warning",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment =
                        "Sum warning flag; to be set when recording is disturbed, possibly some data are not recorded. \r\nE.g. recording is stopped during data download, or Short Term Memory locked, or Fallback speed sensor defect, or Battery change necessary."
                },
                new BitField
                {
                    Name = "BatteryChange",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Battery change necessary\r\n(Type: Warning)"
                },
                new BitField
                {
                    Name = "STblocked",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Short term memory blocked\r\n(Type: Warning)"
                },
                new BitField
                {
                    Name = "Unnamed",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare"
                },
                new BitField
                {
                    Name = "FatError_Int",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Fatal error internal\r\n(Type: Error)"
                },
                new BitField
                {
                    Name = "FatError_Ex",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Fatal error external\r\n(Type: Error)"
                },
                new BitField
                {
                    Name = "MemoryDownload",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Memory download active\r\n(Type: Warning)"
                },
                new BitField
                {
                    Name = "JRU24h_block",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "JRU 24h blocked\r\n(Type: Warning)"
                },
                new BitField
                {
                    Name = "Unnamed",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare"
                },
                new BitField
                {
                    Name = "CommError",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Disturbance of communication on Profibus/Ethernet\r\n(Type: Error)"
                },
                new BitField
                {
                    Name = "Vsens1_defect",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Pulse generator sensor 1 defect\r\n(Type: Warning)"
                },
                new BitField
                {
                    Name = "Vsens2_defect",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Pulse generator v sensor 2 defect\r\n(Type: Warning)"
                },
                new BitField
                {
                    Name = "Mem_FillLevel_80",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Flag to indicate if ETCS JRU memory is at or above fill level 80%."
                },
                new BitField
                {
                    Name = "Mem_FillLevel_100",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Flag to indicate if ETCS JRU memory is at or above fill level 100%."
                },
                new BitField
                {
                    Name = "Unnamed",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "Spare"
                },
                new BitField
                {
                    Name = "HW_version",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "HW Version"
                },
                new BitField
                {
                    Name = "Config_version",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Config Version"
                },
                new BitField
                {
                    Name = "SW_version",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "SW Version"
                },
                new BitField
                {
                    Name = "DeviceAddress",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "MVB Device address (if MVB used in project)"
                },
                new BitField
                {
                    Name = "ConfigIssueNo",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Config issue no."
                },
                new BitField
                {
                    Name = "OD_LTMemLoad",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Load value of the operational data long term memory 0..100 [%]"
                },
                new BitField
                {
                    Name = "DiagMemLoad",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Load value of diag memory 0..100 [%]"
                },
                new BitField
                {
                    Name = "OD_STMemLoad",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Load value of the operational data short term memory 0..100 [%]"
                },
                new BitField
                {
                    Name = "JRU8dMemLoad",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Load value of the JRU 8d memory 0..100 [%]"
                },
                new BitField
                {
                    Name = "ETCSMemLoad",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "Load value of the ETCS JRU memory 0..100 [%]."
                },
                new BitField
                {
                    Name = "Unnamed",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "spare"
                },
                new BitField
                {
                    Name = "ErrorNumber",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Contains the decimal fatal error number"
                },
                new BitField
                {
                    Name = "ConfigName",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Configuration Name"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition JRU_AUX_IN => new DataSetDefinition
        {
            Name = "JRU_AUX_IN JRU_AUX_IN",
            Comment =
                "From DMI to JRU with following structure.\r\nNote: This is an external Device interface, not 32-bit aligned!",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "LifeSign",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 8,
                    Comment = "LifeSign"
                },
                new BitField
                {
                    Name = "ETCS_IS",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS Isolation"
                },
                new BitField
                {
                    Name = "Unnamed",
                    BitFieldType = BitFieldType.Spare,
                    Length = 6,
                    Comment = "spare"
                },
                new BitField
                {
                    Name = "JIN_UTC_valid",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Validity of UTC time"
                },
                new BitField
                {
                    Name = "Unnamed",
                    BitFieldType = BitFieldType.Spare,
                    Length = 16,
                    Comment = "spare"
                },
                new BitField
                {
                    Name = "JIN_UTC_offset",
                    BitFieldType = BitFieldType.Int16,
                    Length = 8,
                    Comment = "Offset to UTC time"
                },
                new BitField
                {
                    Name = "Unnamed",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "spare"
                },
                new BitField
                {
                    Name = "JIN_UTC_time_sec",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "UTC time in Unix format (seconds after 01/01/1970)"
                },
                new BitField
                {
                    Name = "JIN_UTC_time_msec",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "UTC time part msec"
                },
                new BitField
                {
                    Name = "jru_in_spare",
                    BitFieldType = BitFieldType.Spare,
                    Length = 32,
                    Comment = "spare for alignment"
                }
            }
        };

        

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_4 => new DataSetDefinition
        {
            Name = "DIA_4 DMI1_EVENT_DIAG",
            Comment =
                "Dataset definition (defines the bit field for failure signals/events corresponding to a diagnostics ErrorCode).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_DMI1_DIAG",
                    BitFieldType = BitFieldType.HexString,
                    Length = 32,
                    Comment = "Event data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_5 => new DataSetDefinition
        {
            Name = "DIA_5 DMI1_ENVIRONMENT_DIAG",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_DMI1_DIAG",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_6 => new DataSetDefinition
        {
            Name = "DIA_6 DMI2_EVENT_DIAG",
            Comment =
                "Dataset definition (defines the bit field for failure signals/events corresponding to a diagnostics ErrorCode).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_DMI2_DIAG",
                    BitFieldType = BitFieldType.HexString,
                    Length = 32,
                    Comment = "Event data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_7 => new DataSetDefinition
        {
            Name = "DIA_7 DMI2_ENVIRONMENT_DIAG",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_DMI2_DIAG",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_100 => new DataSetDefinition
        {
            Name = "DIA_100 DIAGTOOL_CONTROL",
            Comment = "The dataset contains the input requests of the DIAG Tool (to be defined for future demands).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "DIA_100_SPARE",
                    BitFieldType = BitFieldType.Spare,
                    Length = 256,
                    Comment = "Reserved for input from the DIAG Tool."
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_128 => new DataSetDefinition
        {
            Name = "DIA_128 VAP_EVENT",
            Comment =
                "Dataset definition (defines the bit field for failure signals/events corresponding to a diagnostics ErrorCode).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_VAP",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Event data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_129 => new DataSetDefinition
        {
            Name = "DIA_129 VAP_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_VAP",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };
        
        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_132 => new DataSetDefinition
        {
            Name = "DIA_132 ODO_EVENT",
            Comment =
                "Dataset definition (defines the bit field for failure signals/events corresponding to a diagnostics ErrorCode).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_ODO",
                    BitFieldType = BitFieldType.HexString,
                    Length = 64,
                    Comment = "Event data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_133 => new DataSetDefinition
        {
            Name = "DIA_133 ODO_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_ODO",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_134 => new DataSetDefinition
        {
            Name = "DIA_134 GSTM_EVENT",
            Comment =
                "Dataset definition (defines the bit field for failure signals/events corresponding to a diagnostics ErrorCode).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_GSTM",
                    BitFieldType = BitFieldType.HexString,
                    Length = 32,
                    Comment = "Event data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_135 => new DataSetDefinition
        {
            Name = "DIA_135 GSTM_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_GSTM",
                    BitFieldType = BitFieldType.HexString,
                    Length = 128,
                    Comment = "Environment data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_136 => new DataSetDefinition
        {
            Name = "DIA_136 PZB_EVENT",
            Comment =
                "Dataset definition (defines the bit field for failure signals/events corresponding to a diagnostics ErrorCode).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_PZB",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Event data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_137 => new DataSetDefinition
        {
            Name = "DIA_137 PZB_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_PZB",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_138 => new DataSetDefinition
        {
            Name = "DIA_138 LZB_EVENT",
            Comment =
                "Dataset definition (defines the bit field for failure signals/events corresponding to a diagnostics ErrorCode).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_LZB",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Event data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_139 => new DataSetDefinition
        {
            Name = "DIA_139 LZB_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_LZB",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_140 => new DataSetDefinition
        {
            Name = "DIA_140 LZB_ES_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_LZB_ES",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_141 => new DataSetDefinition
        {
            Name = "DIA_141 ATB_EVENT",
            Comment =
                "Dataset definition (defines the bit field for failure signals/events corresponding to a diagnostics ErrorCode).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_ATB",
                    BitFieldType = BitFieldType.HexString,
                    Length = 128,
                    Comment = "Event data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_142 => new DataSetDefinition
        {
            Name = "DIA_142 ATB_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_ATB",
                    BitFieldType = BitFieldType.HexString,
                    Length = 128,
                    Comment = "Environment data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_143 => new DataSetDefinition
        {
            Name = "DIA_143 UNI_EVENT",
            Comment =
                "Dataset definition (defines the bit field for failure signals/events corresponding to a diagnostics ErrorCode).\r\nNote:\r\nThe UNI-STM can be\r\na generic product which can be realized in an external hardware generic product \r\na application product as part of the ETCS Onboard P8 generic product.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_UNI",
                    BitFieldType = BitFieldType.HexString,
                    Length = 128,
                    Comment = "Event data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_144 => new DataSetDefinition
        {
            Name = "DIA_144 UNI_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_UNI",
                    BitFieldType = BitFieldType.HexString,
                    Length = 128,
                    Comment = "Environment data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_145 => new DataSetDefinition
        {
            Name = "DIA_145 SHP_EVENT",
            Comment =
                "Dataset definition (defines the bit field for failure signals/events corresponding to a diagnostics ErrorCode).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_SHP",
                    BitFieldType = BitFieldType.HexString,
                    Length = 16,
                    Comment = "Event data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_146 => new DataSetDefinition
        {
            Name = "DIA_146 SHP_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_SHP",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };
        
        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_153 => new DataSetDefinition
        {
            Name = "DIA_153 DMI1_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_DMI1",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_154 => new DataSetDefinition
        {
            Name = "DIA_154 DMI2_EVENT",
            Comment =
                "Dataset definition (defines the bit field for failure signals/events corresponding to a diagnostics ErrorCode).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_DMI2",
                    BitFieldType = BitFieldType.HexString,
                    Length = 32,
                    Comment = "Event data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_155 => new DataSetDefinition
        {
            Name = "DIA_155 DMI2_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_DMI2",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_156 => new DataSetDefinition
        {
            Name = "DIA_156 TBL_EVENT",
            Comment =
                "Dataset definition (defines the bit field for failure signals/events corresponding to a diagnostics ErrorCode).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_TBL",
                    BitFieldType = BitFieldType.HexString,
                    Length = 16,
                    Comment = "Event data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_157 => new DataSetDefinition
        {
            Name = "DIA_157 TBL_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_TBL",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };

        

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_159 => new DataSetDefinition
        {
            Name = "DIA_159 COD_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_COD",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_160 => new DataSetDefinition
        {
            Name = "DIA_160 ATC2_EVENT",
            Comment =
                "Dataset definition (defines the bit field for failure signals/events corresponding to a diagnostics ErrorCode).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_ATC2",
                    BitFieldType = BitFieldType.HexString,
                    Length = 128,
                    Comment = "Event data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_161 => new DataSetDefinition
        {
            Name = "DIA_161 ATC2_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_ATC2",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_162 => new DataSetDefinition
        {
            Name = "DIA_162 ZUB123_EVENT",
            Comment =
                "Dataset definition (defines the bit field for failure signals/events corresponding to a diagnostics ErrorCode).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_ZUB123",
                    BitFieldType = BitFieldType.HexString,
                    Length = 128,
                    Comment = "Event data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_163 => new DataSetDefinition
        {
            Name = "DIA_163 ZUB123_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_ZUB123",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_164 => new DataSetDefinition
        {
            Name = "DIA_164 SCMT_EVENT",
            Comment =
                "Dataset definition (defines the bit field for failure signals/events corresponding to a diagnostics ErrorCode).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_SCMT",
                    BitFieldType = BitFieldType.HexString,
                    Length = 128,
                    Comment = "Event data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_165 => new DataSetDefinition
        {
            Name = "DIA_165 SCMT_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_SCMT",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };
        
        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_208 => new DataSetDefinition
        {
            Name = "DIA_208 LRU_DIAG_EVENT",
            Comment =
                "Dataset definition (defines the bit field for failure signals/events corresponding to a diagnostics ErrorCode).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_LRU",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Event data"
                }
            }
        };

        

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_210 => new DataSetDefinition
        {
            Name = "DIA_210 DIAG_STANDARD",
            Comment =
                "The following packet defines the diagnostics standardized channel interface. This packet is defined for future use.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "EVT_STD",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Event data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_211 => new DataSetDefinition
        {
            Name = "DIA_211 DIAG_STANDARD_ENVIRONMENT",
            Comment =
                "The following packet defines the diagnostics standardized channel interface. This packet is defined for future use.",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_STD",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_213 => new DataSetDefinition
        {
            Name = "DIA_213 VERSION_DATA_VRS",
            Comment =
                "The following packet defines the packet for version data representation towards the vehicle interface. The version data will be transferred on demand (future use).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "VERSION_VRS",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Version data"
                }
            }
        };

        

        // TODO This dataset has not been checked yet!
        public static DataSetDefinition DIA_215 => new DataSetDefinition
        {
            Name = "DIA_215 DIAG_WAYSIDE_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ENV_WAY",
                    BitFieldType = BitFieldType.HexString,
                    Length = 256,
                    Comment = "Environment data"
                }
            }
        };
    }
}