using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BitDataParser;

// ReSharper disable InconsistentNaming
#pragma warning disable 1591

namespace IPTComShark.DataSets
{
    /// <summary>
    /// This includes both chapter 7 and chapter 8.
    /// In general, datasets from chapter 7 are named PACKET xx, while datasets from chapter 8 are MESSAGE xx
    /// </summary>
    public static class Subset26
    {
        public static float ImplementationLevel
        {
            get
            {
                IEnumerable<FieldInfo> fieldInfos =
                    typeof(Subset26).GetFields().Where(f => f.FieldType == typeof(DataSetDefinition));

                // Packets
                // 54 Track to Train
                // 8 Train to Track

                // messages
                // 38 both

                // 100 in total!


                return fieldInfos.Count() / 100f;
            }
        }

        /// <summary>
        /// Applies Q_SCALE to a value and returns the value in meters
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="qscale"></param>
        /// <returns></returns>
        public static double ApplyQScale(uint distance, uint qscale)
        {
            if (qscale == 0)
                return distance / 10f;
            if (qscale == 1)
                return distance;
            if (qscale == 2)
                return distance * 10f;
            throw new ArgumentOutOfRangeException("Q_SCALE must be 0-2");
        }

        #region A_

        public static BitField A_NVMAXREDADH1 = new BitField
        {
            Name = "A_NVMAXREDADH1",
            BitFieldType = BitFieldType.UInt16,
            Length = 6,
            Scaling = 0.05,
            AppendString = "m/s",
            Comment = "Has been scaled by 0.05 to m/s"
        };

        public static BitField A_NVMAXREDADH2 = new BitField
        {
            Name = "A_NVMAXREDADH2",
            BitFieldType = BitFieldType.UInt16,
            Length = 6,
            Scaling = 0.05,
            AppendString = "m/s",
            Comment = "Has been scaled by 0.05 to m/s"
        };

        public static BitField A_NVMAXREDADH3 = new BitField
        {
            Name = "A_NVMAXREDADH3",
            BitFieldType = BitFieldType.UInt16,
            Length = 6,
            Scaling = 0.05,
            AppendString = "m/s",
            Comment = "Has been scaled by 0.05 to m/s"
        };

        public static BitField A_NVP12 = new BitField
        {
            Name = "A_NVP12",
            BitFieldType = BitFieldType.UInt16,
            Length = 6,
            Scaling = 0.05,
            AppendString = "m/s",
            Comment = "Has been scaled by 0.05 to m/s"
        };

        public static BitField A_NVP23 = new BitField
        {
            Name = "A_NVP23",
            BitFieldType = BitFieldType.UInt16,
            Length = 6,
            Scaling = 0.05,
            AppendString = "m/s",
            Comment = "Has been scaled by 0.05 to m/s"
        };

        #endregion

        #region D_

        public static BitField D_ADHESION = new BitField
        {
            Name = "D_ADHESION",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_CURRENT = new BitField
        {
            Name = "D_CURRENT",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_CYCLOC = new BitField
        {
            Name = "D_CYCLOC",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_DP = new BitField
        {
            Name = "D_DP",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "Q_DANGERPOINT",
                LookUpTable = new IntLookupTable
                {
                    {0, 0},
                    {1, 15}
                }
            }
        };

        public static BitField D_EMERGENCYSTOP = new BitField
        {
            Name = "D_EMERGENCYSTOP",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_ENDTIMERSTARTLOC = new BitField
        {
            Name = "D_ENDTIMERSTARTLOC",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "Q_ENDTIMER",
                LookUpTable = new IntLookupTable
                {
                    {0, 0},
                    {1, 15}
                }
            }
        };

        public static BitField D_INFILL = new BitField
        {
            Name = "D_INFILL",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_LEVELTR = new BitField
        {
            Name = "D_LEVELTR",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_LINK = new BitField
        {
            Name = "D_LINK",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_LOC = new BitField
        {
            Name = "D_LOC",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_LOOP = new BitField
        {
            Name = "D_LOOP",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_OL = new BitField
        {
            Name = "D_OL",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "Q_OVERLAP",
                LookUpTable = new IntLookupTable
                {
                    {0, 0},
                    {1, 15}
                }
            }
        };

        public static BitField D_GRADIENT = new BitField
        {
            Name = "D_GRADIENT",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_MAMODE = new BitField
        {
            Name = "D_MAMODE",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_NVOVTRP = new BitField
        {
            Name = "D_NVOVTRP",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_NVPOTRP = new BitField
        {
            Name = "D_NVPOTRP",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_NVROLL = new BitField
        {
            Name = "D_NVROLL",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_NVSTFF = new BitField
        {
            Name = "D_NVSTFF",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_LRBG = new BitField
        {
            Name = "D_LRBG",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_REF = new BitField
        {
            Name = "D_REF",
            BitFieldType = BitFieldType.Int16,
            Length = 16,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_REVERSE = new BitField
        {
            Name = "D_REVERSE",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_RBCTR = new BitField
        {
            Name = "D_RBCTR",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_SR = new BitField
        {
            Name = "D_SR",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_STATIC = new BitField
        {
            Name = "D_STATIC",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_STARTOL = new BitField
        {
            Name = "D_STARTOL",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "Q_OVERLAP",
                LookUpTable = new IntLookupTable
                {
                    {0, 0},
                    {1, 15}
                }
            }
        };

        public static BitField D_STARTREVERSE = new BitField
        {
            Name = "D_STARTREVERSE",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_TAFDISPLAY = new BitField
        {
            Name = "D_TAFDISPLAY",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_TRACKINIT = new BitField
        {
            Name = "D_TRACKINIT",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "Q_TRACKINIT",
                LookUpTable = new IntLookupTable
                {
                    {0, 0},
                    {1, 15}
                }
            }
        };

        public static BitField D_TRACKCOND = new BitField
        {
            Name = "D_TRACKCOND",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_SECTIONTIMERSTOPLOC = new BitField
        {
            Name = "D_SECTIONTIMERSTOPLOC",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "Q_SECTIONTIMER",
                LookUpTable = new IntLookupTable
                {
                    {0, 0},
                    {1, 15}
                }
            }
        };

        public static BitField D_TEXTDISPLAY = new BitField
        {
            Name = "D_TEXTDISPLAY",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_TSR = new BitField
        {
            Name = "D_TSR",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField D_VALIDNV = new BitField
        {
            Name = "D_VALIDNV",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        #endregion

        #region G_

        public static BitField G_A = new BitField
        {
            Name = "G_A",
            BitFieldType = BitFieldType.UInt16,
            Length = 8,
            Comment = "1‰"
        };

        public static BitField G_TSR = new BitField
        {
            Name = "G_TSR",
            BitFieldType = BitFieldType.UInt16,
            Length = 8,
            Comment = "1‰"
        };

        #endregion

        #region L_

        public static BitField L_ACKLEVELTR = new BitField
        {
            Name = "L_ACKLEVELTR",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField L_ACKMAMODE = new BitField
        {
            Name = "L_ACKMAMODE",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField L_ADHESION = new BitField
        {
            Name = "L_ADHESION",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField L_DOUBTOVER = new BitField
        {
            Name = "L_DOUBTOVER",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField L_DOUBTUNDER = new BitField
        {
            Name = "L_DOUBTUNDER",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField L_ENDSECTION = new BitField
        {
            Name = "L_ENDSECTION",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField L_LOOP = new BitField
        {
            Name = "L_LOOP",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField L_MAMODE = new BitField
        {
            Name = "L_MAMODE",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField L_MESSAGE = new BitField
        {
            Name = "L_MESSAGE",
            BitFieldType = BitFieldType.UInt16,
            Length = 10,
            Comment = "bytes"
        };

        public static BitField L_NVKRINT = new BitField
        {
            Name = "L_NVKRINT",
            BitFieldType = BitFieldType.UInt16,
            Length = 5,
            LookupTable = new LookupTable
            {
                {"0", "0m"},
                {"1", "25m"},
                {"2", "50m"},
                {"3", "75m"},
                {"4", "100m"},
                {"5", "150m"},
                {"6", "200m"},
                {"7", "300m"},
                {"8", "400m"},
                {"9", "500m"},
                {"10", "600m"},
                {"11", "700m"},
                {"12", "800m"},
                {"13", "900m"},
                {"14", "1000m"},
                {"15", "1100m"},
                {"16", "1200m"},
                {"17", "1300m"},
                {"18", "1400m"},
                {"19", "1500m"},
                {"20", "1600m"},
                {"21", "1700m"},
                {"22", "1800m"},
                {"23", "1900m"},
                {"24", "2000m"},
                {"25", "2100m"},
                {"26", "2200m"},
                {"27", "2300m"},
                {"28", "2400m"},
                {"29", "2500m"},
                {"30", "2600m"},
                {"31", "2700m"}
            }
        };

        public static BitField L_SECTION = new BitField
        {
            Name = "L_SECTION",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField L_PACKET = new BitField
        {
            Name = "L_PACKET",
            BitFieldType = BitFieldType.UInt16,
            Length = 13
        };

        public static BitField L_REVERSEAREA = new BitField
        {
            Name = "L_REVERSEAREA",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField L_TAFDISPLAY = new BitField
        {
            Name = "L_TAFDISPLAY",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField L_TEXT = new BitField
        {
            Name = "L_TEXT",
            BitFieldType = BitFieldType.UInt16,
            Length = 8
        };

        public static BitField L_TEXTDISPLAY = new BitField
        {
            Name = "L_TEXTDISPLAY",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField L_TRACKCOND = new BitField
        {
            Name = "L_TRACKCOND",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        public static BitField L_TRAININT = new BitField
        {
            Name = "L_TRAININT",
            BitFieldType = BitFieldType.UInt16,
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "Q_LENGTH",
                LookUpTable = new IntLookupTable
                {
                    {0, 0}, // No train integrity
                    {1, 15}, // Train integrity confirmed by device
                    {2, 15}, // Train integrity confirmed by driver
                    {3, 0} // Train integrity lost
                }
            },
            Comment = "meters"
        };

        public static BitField L_TSR = new BitField
        {
            Name = "L_TSR",
            BitFieldType = BitFieldType.UInt16,
            Length = 15,
            Comment = "10 cm, 1m or 10 m depends on Q_SCALE"
        };

        #endregion

        #region M_

        public static BitField M_ACK = new BitField
        {
            Name = "M_ACK",
            BitFieldType = BitFieldType.Bool,
            Length = 1
        };

        public static BitField M_ADHESION = new BitField
        {
            Name = "M_ADHESION",
            BitFieldType = BitFieldType.Bool,
            Length = 1
        };

        public static BitField M_COUNT = new BitField
        {
            Name = "M_COUNT",
            BitFieldType = BitFieldType.UInt16,
            Length = 8,
            Comment =
                "Message counter. To enable detection of a change of balise group message during passage of the balise group"
        };

        public static BitField M_CURRENT = new BitField
        {
            Name = "M_CURRENT",
            BitFieldType = BitFieldType.UInt16,
            Length = 10,
            Comment = "10A, 1023 = no restriction"
        };

        public static BitField M_DUP = new BitField
        {
            Name = "M_DUP",
            BitFieldType = BitFieldType.UInt16,
            Length = 2,
            Comment =
                "Used to indicate whether the information of the balise is a duplicate of the balise before or after this one"
        };

        public static BitField M_ERROR = new BitField
        {
            Name = "M_ERROR",
            BitFieldType = BitFieldType.UInt16,
            Length = 8,
            LookupTable = new LookupTable
            {
                {"0", "Balise group: linking consistency error (ref. 3.16.2.3)"},
                {"1", "Linked balise group: message consistency error(ref. 3.16.2.4.1/4)"},
                {"2", "Unlinked balise group: message consistency error (ref. 3.16.2.5)"},
                {"3", "Radio: message consistency error (ref. 3.16.3.1.1a,c)"},
                {"4", "Radio: sequence error (ref. 3.16.3.1.1b)"},
                {
                    "5",
                    "Radio: safe radio connection error (ref. 3.16.3.4, to be sent when communication links re-established)"
                },
                {"6", "Safety critical failure (ref 4.4.6.1.6 , 4.4.15.1.5)"},
                {"7", "Double linking error (3.16.2.7.1)"},
                {"8", "Double repositioning error (3.16.2.7.2)"}
            }
        };

        public static BitField M_LEVEL = new BitField
        {
            Name = "M_LEVEL",
            BitFieldType = BitFieldType.UInt16,
            Length = 3,
            LookupTable = new LookupTable
            {
                {"0", "L0"},
                {"1", "L-NTC"},
                {"2", "L1"},
                {"3", "L2"},
                {"4", "L3"},
                {"5", "Spare"},
                {"6", "Spare"},
                {"7", "Spare"}
            }
        };

        public static BitField M_LEVELTR = new BitField
        {
            Name = "M_LEVELTR",
            BitFieldType = BitFieldType.UInt16,
            Length = 3,
            LookupTable = new LookupTable
            {
                {"0", "Level 0"},
                {"1", "Level NTC specified by NID_NTC"},
                {"2", "Level 1"},
                {"3", "Level 2"},
                {"4", "Level 3"},
                {"5", "Spare"},
                {"6", "Spare"},
                {"7", "Spare"}
            }
        };

        public static BitField M_LEVELTEXTDISPLAY = new BitField
        {
            Name = "M_LEVELTEXTDISPLAY",
            BitFieldType = BitFieldType.UInt16,
            Length = 3,
            LookupTable = new LookupTable
            {
                {"0", "Level 0"},
                {"1", "Level NTC specified by NID_NTC"},
                {"2", "Level 1"},
                {"3", "Level 2"},
                {"4", "Level 3"},
                {"5", "The display of the text shall not be limited by the level"},
                {"6", "Spare"},
                {"7", "Spare"}
            }
        };

        public static BitField M_LOC = new BitField
        {
            Name = "M_LOC",
            BitFieldType = BitFieldType.UInt16,
            Length = 3,
            LookupTable = new LookupTable
            {
                {"0", "Now (The position report is sent upon receipt of the order)"},
                {"1", "Every LRBG compliant balise group."},
                {"2", "Do not send position report on passage of LRBG compliant balise group."},
                {"3", "Spare"},
                {"4", "Spare"},
                {"5", "Spare"},
                {"6", "Spare"},
                {"7", "Spare"}
            }
        };

        public static BitField M_MAMODE = new BitField
        {
            Name = "M_MAMODE",
            BitFieldType = BitFieldType.UInt16,
            Length = 2,
            LookupTable = new LookupTable
            {
                {"0", "On Sight"},
                {"1", "Shunting"},
                {"2", "Limited Supervision"},
                {"3", "Spare"}
            }
        };

        public static BitField M_MODE = new BitField
        {
            Name = "M_MODE",
            BitFieldType = BitFieldType.UInt16,
            Length = 4,
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
                {"15", "Passive Shunting"}
            }
        };

        public static BitField M_MODETEXTDISPLAY = new BitField
        {
            Name = "M_MODETEXTDISPLAY",
            BitFieldType = BitFieldType.UInt16,
            Length = 4,
            LookupTable = new LookupTable
            {
                {"0", "Full Supervision"},
                {"1", "On Sight"},
                {"2", "Staff Responsible"},
                {"3", "Spare"},
                {"4", "Unfitted"},
                {"5", "Spare"},
                {"6", "Stand By"},
                {"7", "Trip"},
                {"8", "Post Trip"},
                {"9", "Spare"},
                {"10", "Spare"},
                {"11", "Non Leading"},
                {"12", "Limited Supervision"},
                {"13", "Spare"},
                {"14", "Reversing"},
                {"15", "The display of the text shall not be limited by the mode."}
            }
        };

        public static BitField M_NVAVADH = new BitField
        {
            Name = "M_NVAVADH",
            BitFieldType = BitFieldType.UInt16,
            Length = 5,
            Scaling = 0.05,
            Comment = "Has been scaled by 0.05"
        };

        public static BitField M_NVCONTACT = new BitField
        {
            Name = "M_NVCONTACT",
            BitFieldType = BitFieldType.UInt16,
            Length = 2,
            LookupTable = new LookupTable
            {
                {"0", "Train trip"},
                {"1", "Apply service brake"},
                {"2", "No Reaction"},
                {"3", "Spare"}
            }
        };

        public static BitField M_NVDERUN = new BitField
        {
            Name = "M_NVDERUN",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "No"},
                {"1", "Yes"}
            }
        };

        public static BitField M_NVEBCL = new BitField
        {
            Name = "M_NVEBCL",
            BitFieldType = BitFieldType.UInt16,
            Length = 4,
            LookupTable = new LookupTable
            {
                {"0", "Confidence level = 50 %"},
                {"1", "Confidence level = 90 %"},
                {"2", "Confidence level = 99 %"},
                {"3", "Confidence level = 99.9 %"},
                {"4", "Confidence level = 99.99%"},
                {"5", "Confidence level = 99.999 %"},
                {"6", "Confidence level = 99.9999 %"},
                {"7", "Confidence level = 99.99999 %"},
                {"8", "Confidence level = 99.999999 %"},
                {"9", "Confidence level = 99.9999999 %"}
            }
        };

        public static BitField M_NVKRINT = new BitField
        {
            Name = "M_NVKRINT",
            BitFieldType = BitFieldType.UInt16,
            Length = 5,
            Scaling = 0.05,
            Comment = "Has been scaled by 0.05"
        };

        public static BitField M_NVKTINT = new BitField
        {
            Name = "M_NVKTINT",
            BitFieldType = BitFieldType.UInt16,
            Length = 5,
            Scaling = 0.05,
            Comment = "Has been scaled by 0.05"
        };

        public static BitField M_NVKVINT = new BitField
        {
            Name = "M_NVKVINT",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Scaling = 0.02,
            Comment = "Has been scaled by 0.02"
        };

        public static BitField M_TRACKCOND = new BitField
        {
            Name = "M_TRACKCOND",
            BitFieldType = BitFieldType.UInt16,
            Length = 4,
            LookupTable = new LookupTable
            {
                {"0", "Non stopping area. Initial state: stopping permitted"},
                {"1", "Tunnel stopping area. Initial state: no tunnel stopping area"},
                {"2", "Sound horn. Initial state: no request for sound horn"},
                {"3", "Powerless section – lower pantograph. Initial state: not powerless section"},
                {"4", "Radio hole (stop supervising T_NVCONTACT). Initial state: supervise T_NVCONTACT"},
                {"5", "Air tightness. Initial state: no request for air tightness"},
                {"6", "Switch off regenerative brake. Initial state: regenerative brake on"},
                {
                    "7",
                    "Switch off eddy current brake for service brake. Initial state: eddy current brake for service brake on"
                },
                {"8", "Switch off magnetic shoe brake. Initial state: magnetic shoe brake on"},
                {"9", "Powerless section – switch off the main power switch. Initial state: not powerless section"},
                {
                    "10",
                    "Switch off eddy current brake for emergency brake. Initial state: eddy current brake for emergency brake on"
                }
            }
        };

        public static BitField M_VERSION = new BitField
        {
            Name = "M_VERSION",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "Version of the ERTMS/ETCS system"
        };

        #endregion

        #region N_

        public static BitField N_ITER = new BitField
        {
            Name = "N_ITER",
            BitFieldType = BitFieldType.UInt16,
            Length = 5
        };

        public static BitField N_PIG = new BitField
        {
            Name = "N_PIG",
            BitFieldType = BitFieldType.UInt16,
            Length = 3,
            Comment = "Position in the group"
        };

        public static BitField N_TOTAL = new BitField
        {
            Name = "N_TOTAL",
            BitFieldType = BitFieldType.UInt16,
            Length = 3,
            Comment = "Total number of balises in the balise group"
        };

        public static BitField NC_CDDIFF = new BitField
        {
            Name = "NC_CDDIFF",
            BitFieldType = BitFieldType.UInt16,
            LookupTable = new LookupTable
            {
                {"0", "Specific SSP applicable to Cant Deficiency 80 mm"},
                {"1", "Specific SSP applicable to Cant Deficiency 100 mm"},
                {"2", "Specific SSP applicable to Cant Deficiency 130 mm"},
                {"3", "Specific SSP applicable to Cant Deficiency 150 mm"},
                {"4", "Specific SSP applicable to Cant Deficiency 165 mm"},
                {"5", "Specific SSP applicable to Cant Deficiency 180 mm"},
                {"6", "Specific SSP applicable to Cant Deficiency 210 mm"},
                {"7", "Specific SSP applicable to Cant Deficiency 225 mm"},
                {"8", "Specific SSP applicable to Cant Deficiency 245 mm"},
                {"9", "Specific SSP applicable to Cant Deficiency 275 mm"},
                {"10", "Specific SSP applicable to Cant Deficiency 300 mm"}
            },
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "Q_DIFF",
                LookUpTable = new IntLookupTable
                {
                    {0, 4},
                    {1, 0},
                    {2, 0},
                    {3, 0}
                }
            }
        };

        public static BitField NC_DIFF = new BitField
        {
            Name = "NC_DIFF",
            BitFieldType = BitFieldType.UInt16,
            LookupTable = new LookupTable
            {
                {"0", "Specific SSP applicable to Freight train braked in “P” position"},
                {"1", "Specific SSP applicable to Freight train braked in “G” position"},
                {"2", "Specific SSP applicable to Passenger train"}
            },
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "Q_DIFF",
                LookUpTable = new IntLookupTable
                {
                    {0, 0},
                    {1, 4},
                    {2, 4},
                    {3, 0}
                }
            }
        };

        public static BitField NID_BG = new BitField
        {
            Name = "NID_BG",
            BitFieldType = BitFieldType.UInt16,
            Length = 14,
            Comment = "Identity of the balise group"
        };

        public static BitField NID_C = new BitField
        {
            Name = "NID_C",
            BitFieldType = BitFieldType.UInt16,
            Length = 10,
            Comment = "Country or region"
        };

        public static BitField NID_EM = new BitField
        {
            Name = "NID_EM",
            BitFieldType = BitFieldType.UInt16,
            Length = 4
        };

        public static BitField NID_ENGINE = new BitField
        {
            Name = "NID_ENGINE",
            BitFieldType = BitFieldType.UInt32,
            Length = 24
        };

        public static BitField NID_LRBG = new BitField
        {
            Name = "NID_LRBG",
            BitFieldType = BitFieldType.UInt32,
            Length = 24
        };

        public static BitField NID_LTRBG = new BitField
        {
            Name = "NID_LTRBG",
            BitFieldType = BitFieldType.UInt32,
            Length = 24
        };

        public static BitField NID_LOOP = new BitField
        {
            Name = "NID_LOOP",
            BitFieldType = BitFieldType.UInt16,
            Length = 14
        };

        public static BitField NID_MN = new BitField
        {
            Name = "NID_MN",
            BitFieldType = BitFieldType.HexString,
            Length = 24
        };

        public static BitField NID_NTC = new BitField
        {
            Name = "NID_NTC",
            BitFieldType = BitFieldType.UInt16,
            Length = 8
        };

        public static BitField NID_OPERATIONAL = new BitField
        {
            Name = "NID_OPERATIONAL",
            BitFieldType = BitFieldType.HexString,
            Length = 32
        };

        public static BitField NID_MESSAGE = new BitField
        {
            Name = "NID_MESSAGE",
            BitFieldType = BitFieldType.UInt16,
            Length = 8
        };

        public static BitField NID_PACKET = new BitField
        {
            Name = "NID_PACKET",
            BitFieldType = BitFieldType.UInt16,
            Length = 8
        };

        public static BitField NID_PRVLRBG = new BitField
        {
            Name = "NID_PRVLRBG",
            BitFieldType = BitFieldType.UInt32,
            Length = 24
        };

        public static BitField NID_VBCMK = new BitField
        {
            Name = "NID_VBCMK",
            BitFieldType = BitFieldType.UInt16,
            Length = 6
        };

        public static BitField NID_RADIO = new BitField
        {
            Name = "NID_RADIO",
            BitFieldType = BitFieldType.HexString,
            Length = 64
        };

        public static BitField NID_RBC = new BitField
        {
            Name = "NID_RBC",
            BitFieldType = BitFieldType.UInt16,
            Length = 14,
            Comment = "16 383 = Contact last known RBC"
        };

        public static BitField NID_RIU = new BitField
        {
            Name = "NID_RIU",
            BitFieldType = BitFieldType.UInt16,
            Length = 14
        };

        public static BitField NID_TEXTMESSAGE = new BitField
        {
            Name = "NID_TEXTMESSAGE",
            BitFieldType = BitFieldType.UInt16,
            Length = 8
        };

        public static BitField NID_TSR = new BitField
        {
            Name = "NID_TSR",
            BitFieldType = BitFieldType.UInt16,
            Length = 8,
            Comment = "16 383 = Contact last known RBC"
        };

        #endregion

        #region Q_

        public static BitField Q_ASPECT = new BitField
        {
            Name = "Q_ASPECT",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "Stop if in SH mode"},
                {"1", "Go if in SH mode"}
            }
        };

        public static BitField Q_CONFTEXTDISPLAY = new BitField
        {
            Name = "Q_CONFTEXTDISPLAY",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "Driver acknowledgement always ends the text display, regardless of the end condition"},
                {"1", "Driver acknowledgement is an additional condition to end the display"}
            }
        };

        public static BitField Q_DANGERPOINT = new BitField
        {
            Name = "Q_DANGERPOINT",
            BitFieldType = BitFieldType.UInt16,
            Length = 1
        };

        public static BitField Q_DIFF = new BitField
        {
            Name = "Q_DIFF",
            BitFieldType = BitFieldType.UInt16,
            Length = 2,
            LookupTable = new LookupTable
            {
                {"0", "Cant Deficiency specific category"},
                {"1", "Other specific category, replaces the Cant Deficiency SSP"},
                {"2", "Other specific category, does not replace the Cant Deficiency SSP"},
                {"3", "Spare"}
            }
        };

        public static BitField Q_DIR = new BitField
        {
            Name = "Q_DIR",
            BitFieldType = BitFieldType.UInt16,
            Length = 2,
            LookupTable = new LookupTable
            {
                {"0", "Reverse"},
                {"1", "Nominal"},
                {"2", "Both"},
                {"3", "Spare"}
            }
        };

        public static BitField Q_DIRLRBG = new BitField
        {
            Name = "Q_DIRLRBG",
            BitFieldType = BitFieldType.UInt16,
            Length = 2,
            LookupTable = new LookupTable
            {
                {"0", "Reverse"},
                {"1", "Nominal"},
                {"2", "Unknown"},
                {"3", "Spare"}
            }
        };

        public static BitField Q_DIRTRAIN = new BitField
        {
            Name = "Q_DIRTRAIN",
            BitFieldType = BitFieldType.UInt16,
            Length = 2,
            LookupTable = new LookupTable
            {
                {"0", "Reverse"},
                {"1", "Nominal"},
                {"2", "Unknown"},
                {"3", "Spare"}
            }
        };

        public static BitField Q_DLRBG = new BitField
        {
            Name = "Q_DLRBG",
            BitFieldType = BitFieldType.UInt16,
            Length = 2,
            LookupTable = new LookupTable
            {
                {"0", "Reverse"},
                {"1", "Nominal"},
                {"2", "Unknown"},
                {"3", "Spare"}
            }
        };

        public static BitField Q_EMERGENCYSTOP = new BitField
        {
            Name = "Q_EMERGENCYSTOP",
            BitFieldType = BitFieldType.UInt16,
            Length = 2,
            LookupTable = new LookupTable
            {
                {"0", "Conditional Emergency Stop accepted, with update of EOA (ref 3.10.2.2)"},
                {"1", "Conditional Emergency Stop accepted, with no update of EOA (ref 3.10.2.2)"},
                {"2", "Not Relevant (Unconditional Emergency Stop) (ref 3.10.2.3)"},
                {
                    "3",
                    "Conditional Emergency Stop rejected because train has passed the emergency stop location (ref 3.10.2.2)"
                }
            }
        };

        public static BitField Q_ENDTIMER = new BitField
        {
            Name = "Q_ENDTIMER",
            BitFieldType = BitFieldType.UInt16,
            Length = 1
        };

        public static BitField Q_GDIR = new BitField
        {
            Name = "Q_GDIR",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "Downhill"},
                {"1", "Uphill"}
            }
        };

        public static BitField Q_FRONT = new BitField
        {
            Name = "Q_FRONT",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "Train length delay on validity end point of profile element."},
                {"1", "No train length delay on validity end point of profile element"}
            }
        };

        public static BitField Q_INFILL = new BitField
        {
            Name = "Q_INFILL",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "Enter"},
                {"1", "Exist"}
            }
        };

        public static BitField Q_LENGTH = new BitField
        {
            Name = "Q_LENGTH",
            BitFieldType = BitFieldType.UInt16,
            Length = 2,
            LookupTable = new LookupTable
            {
                {"0", "No train integrity information available"},
                {"1", "Train integrity confirmed by integrity monitoring device"},
                {"2", "Train integrity confirmed by driver"},
                {"3", "Train integrity lost"}
            }
        };

        public static BitField Q_LINK = new BitField
        {
            Name = "Q_LINK",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            Comment = "Marks the balise group as linked (Q_LINK = 1) or unlinked(Q_LINK = 0)"
        };

        public static BitField Q_LOCACC = new BitField
        {
            Name = "Q_LOCACC",
            BitFieldType = BitFieldType.UInt16,
            Length = 6,
            Comment = "meters"
        };


        public static BitField Q_LINKORIENTATION = new BitField
        {
            Name = "Q_LINKORIENTATION",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "The balise group is seen by the train in reverse direction"},
                {"1", "The balise group is seen by the train in nominal direction"}
            }
        };

        public static BitField Q_LINKREACTION = new BitField
        {
            Name = "Q_LINKREACTION",
            BitFieldType = BitFieldType.UInt16,
            Length = 2,
            LookupTable = new LookupTable
            {
                {"0", "Train trip"},
                {"1", "Apply service brake"},
                {"2", "No Reaction"},
                {"3", "Spare"}
            }
        };

        public static BitField Q_LOOPDIR = new BitField
        {
            Name = "Q_LOOPDIR",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "Opposite"},
                {"1", "Same"}
            }
        };

        public static BitField Q_LGTLOC = new BitField
        {
            Name = "Q_LGTLOC",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "Min safe rear end"},
                {"1", "Max safe front end"}
            }
        };

        public static BitField Q_LSSMA = new BitField
        {
            Name = "Q_LSSMA",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "Toggle off"},
                {"1", "Toggle on"}
            }
        };

        public static BitField Q_MAMODE = new BitField
        {
            Name = "Q_MAMODE",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "as the EOA (keeping the SvL given by the MA)"},
                {"1", "as both the EOA and SvL (instead of the EOA and SvL given by the MA)"}
            }
        };

        public static BitField Q_MARQSTREASON1 = new BitField
        {
            Name = "Q_MARQSTREASON1",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            SkipIfValue = (ushort)0,
            LookupTable = new LookupTable
            {
                {"1", "TAF up to level 2/3 transition location"}
            }
        };

        public static BitField Q_MARQSTREASON2 = new BitField
        {
            Name = "Q_MARQSTREASON2",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            SkipIfValue = (ushort)0,
            LookupTable = new LookupTable
            {
                {"1", "Track description deleted"}
            }
        };

        public static BitField Q_MARQSTREASON3 = new BitField
        {
            Name = "Q_MARQSTREASON3",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            SkipIfValue = (ushort)0,
            LookupTable = new LookupTable
            {
                {"1", "Time before a section timer/LOA speed timer expires reached"}
            }
        };

        public static BitField Q_MARQSTREASON4 = new BitField
        {
            Name = "Q_MARQSTREASON4",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            SkipIfValue = (ushort)0,
            LookupTable = new LookupTable
            {
                {"1", "Time before reaching pre-indication location for the EOA/LOA reached"}
            }
        };

        public static BitField Q_MARQSTREASON5 = new BitField
        {
            Name = "Q_MARQSTREASON5",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            SkipIfValue = (ushort)0,
            LookupTable = new LookupTable
            {
                {"1", "Start selected by driver"}
            }
        };

        public static BitField Q_MEDIA = new BitField
        {
            Name = "Q_MEDIA",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            Comment = "Defines the type of media: Balise(0) Loop(1)"
        };

        public static BitField Q_NEWCOUNTRY = new BitField
        {
            Name = "Q_NEWCOUNTRY",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "Same country / railway administration, no NID_C follows"},
                {"1", "Not the same country / railway administration, NID_C follows"}
            }
        };

        public static BitField Q_NVDRIVER_ADHES = new BitField
        {
            Name = "Q_NVDRIVER_ADHES",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "Not allowed"},
                {"1", "Allowed"}
            }
        };

        public static BitField Q_NVEMRRLS = new BitField
        {
            Name = "Q_NVEMRRLS",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "Revoke emergency brake command at standstill"},
                {"1", "Revoke emergency brake command when permitted speed supervision limit is no longer exceeded"}
            }
        };

        public static BitField Q_NVGUIPERM = new BitField
        {
            Name = "Q_NVGUIPERM",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "No"},
                {"1", "Yes"}
            }
        };

        public static BitField Q_NVINHSMICPERM = new BitField
        {
            Name = "Q_NVINHSMICPERM",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "No"},
                {"1", "Yes"}
            }
        };

        public static BitField Q_NVKINT = new BitField
        {
            Name = "Q_NVKINT",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "No integrated correction factors follow"},
                {"1", "Integrated correction factors follow"}
            }
        };

        public static BitField Q_NVKVINTSET = new BitField
        {
            Name = "Q_NVKVINTSET",
            BitFieldType = BitFieldType.UInt16,
            Length = 2,
            LookupTable = new LookupTable
            {
                {"0", "Freight trains"},
                {"1", "Conventional passenger trains"},
                {"2", "2 - Spare"},
                {"3", "3 - Spare"}
            }
        };

        public static BitField Q_NVLOCACC = new BitField
        {
            Name = "Q_NVLOCACC",
            BitFieldType = BitFieldType.UInt16,
            Length = 6,
            Comment = "meters"
        };

        public static BitField Q_NVSBFBPERM = new BitField
        {
            Name = "Q_NVSBFBPERM",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "No"},
                {"1", "Yes"}
            }
        };

        public static BitField Q_NVSBTSMPERM = new BitField
        {
            Name = "Q_NVSBTSMPERM",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "No"},
                {"1", "Yes"}
            }
        };

        public static BitField Q_ORIENTATION = new BitField
        {
            Name = "Q_ORIENTATION",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "The balise group has been passed by the train in reverse direction"},
                {"1", "The balise group has been passed by the train in nominal direction"}
            }
        };

        public static BitField Q_OVERLAP = new BitField
        {
            Name = "Q_OVERLAP",
            BitFieldType = BitFieldType.UInt16,
            Length = 1
        };

        public static BitField Q_RBC = new BitField
        {
            Name = "Q_RBC",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "Terminate communication session"},
                {"1", "Establish communication session"}
            }
        };

        public static BitField Q_RIU = new BitField
        {
            Name = "Q_RIU",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "Terminate communication session"},
                {"1", "Establish communication session"}
            }
        };

        public static BitField Q_SCALE = new BitField
        {
            Name = "Q_SCALE",
            BitFieldType = BitFieldType.UInt16,
            Length = 2,
            Comment = "0 = 10 cm scale, 1 = 1 m scale, 2 = 10 m scale, 3 = Spare"
        };

        public static BitField Q_SECTIONTIMER = new BitField
        {
            Name = "Q_SECTIONTIMER",
            BitFieldType = BitFieldType.UInt16,
            Length = 1
        };

        public static BitField Q_SLEEPSESSION = new BitField
        {
            Name = "Q_SLEEPSESSION",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "Ignore session establishment order"},
                {"1", "Execute session establishment order"}
            }
        };

        public static BitField Q_SRSTOP = new BitField
        {
            Name = "Q_SRSTOP",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "Stop if in SR mode"},
                {"1", "Go if in SR mode"}
            }
        };

        public static BitField Q_SSCODE = new BitField
        {
            Name = "Q_SSCODE",
            BitFieldType = BitFieldType.UInt16,
            Length = 4
        };

        public static BitField Q_STATUS = new BitField
        {
            Name = "Q_STATUS",
            BitFieldType = BitFieldType.UInt16,
            Length = 2,
            LookupTable = new LookupTable
            {
                {"0", "Invalid"},
                {"1", "Valid"},
                {"2", "Unknown"},
                {"3", "Spare"}
            }
        };

        public static BitField Q_TEXT => new BitField
        {
            Name = "Q_TEXT",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "“Level crossing not protected"},
                {"1", "Acknowledgement"}
            }
        };

        public static BitField Q_TEXTCLASS = new BitField
        {
            Name = "Q_TEXTCLASS",
            BitFieldType = BitFieldType.UInt16,
            Length = 2,
            LookupTable = new LookupTable
            {
                {"0", "Auxiliary Information"},
                {"1", "Important Information"}
            }
        };

        public static BitField Q_TEXTCONFIRM = new BitField
        {
            Name = "Q_TEXTCONFIRM",
            BitFieldType = BitFieldType.UInt16,
            Length = 2,
            LookupTable = new LookupTable
            {
                {"0", "No confirmation required"},
                {"1", "Confirmation required"},
                {
                    "2",
                    "Confirmation required: command application of the service brake when display end condition is fulfilled, unless the text has already been acknowledged by the driver"
                },
                {
                    "3",
                    "Confirmation required: command application of the emergency brake when display end condition is fulfilled, unless the text has already been acknowledged by the driver"
                }
            }
        };

        public static BitField Q_TEXTDISPLAY = new BitField
        {
            Name = "Q_TEXTDISPLAY",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "No, display as soon as / until one of the events is fulfilled"},
                {"1", "Yes, display as soon as / until all events are fulfilled"}
            }
        };

        public static BitField Q_TEXTREPORT = new BitField
        {
            Name = "Q_TEXTREPORT",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "No driver acknowledgement report required"},
                {"1", "Driver acknowledgement report required"}
            }
        };

        public static BitField Q_TRACKINIT = new BitField
        {
            Name = "Q_TRACKINIT",
            BitFieldType = BitFieldType.UInt16,
            Length = 1,
            LookupTable = new LookupTable
            {
                {"0", "No initial states to be resumed, profile to follow"},
                {"1", "Empty profile, initial states to be resumed"}
            }
        };

        public static BitField Q_UPDOWN = new BitField
        {
            Name = "Q_UPDOWN",
            BitFieldType = BitFieldType.Bool,
            Length = 1,
            Comment =
                "Defines the direction of the information: Down-link telegram (train to track) (0) Up-link telegram (track to train) (1)"
        };

        public static BitField Q_VBCO = new BitField
        {
            Name = "Q_VBCO",
            BitFieldType = BitFieldType.Bool,
            Length = 1
        };

        #endregion

        #region T_

        public static BitField T_CYCLOC = new BitField
        {
            Name = "T_CYCLOC",
            BitFieldType = BitFieldType.UInt16,
            Length = 8,
            Comment = "seconds"
        };

        public static BitField T_CYCRQST = new BitField
        {
            Name = "T_CYCRQST",
            BitFieldType = BitFieldType.UInt16,
            Length = 8,
            Comment = "seconds"
        };

        public static BitField T_ENDTIMER = new BitField
        {
            Name = "T_ENDTIMER",
            BitFieldType = BitFieldType.UInt16,
            Length = 10,
            Comment = "seconds",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "Q_ENDTIMER",
                LookUpTable = new IntLookupTable
                {
                    {0, 0},
                    {1, 10}
                }
            }
        };

        public static BitField T_EMA = new BitField
        {
            Name = "T_EMA",
            BitFieldType = BitFieldType.UInt16,
            Length = 10,
            Comment = "seconds"
        };

        public static BitField T_LSSMA = new BitField
        {
            Name = "T_LSSMA",
            BitFieldType = BitFieldType.UInt16,
            Length = 8,
            Comment = "seconds"
        };

        public static BitField T_OL = new BitField
        {
            Name = "T_OL",
            BitFieldType = BitFieldType.UInt16,
            Length = 10,
            Comment = "seconds",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "Q_OVERLAP",
                LookUpTable = new IntLookupTable
                {
                    {0, 0},
                    {1, 10}
                }
            }
        };

        public static BitField T_MAR = new BitField
        {
            Name = "T_MAR",
            BitFieldType = BitFieldType.UInt16,
            Length = 8,
            Comment = "seconds"
        };

        public static BitField T_NVCONTACT = new BitField
        {
            Name = "T_NVCONTACT",
            BitFieldType = BitFieldType.UInt16,
            Length = 8,
            Comment = "seconds"
        };

        public static BitField T_NVOVTRP = new BitField
        {
            Name = "T_NVOVTRP",
            BitFieldType = BitFieldType.UInt16,
            Length = 8,
            Comment = "seconds"
        };

        public static BitField T_SECTIONTIMER = new BitField
        {
            Name = "T_SECTIONTIMER",
            BitFieldType = BitFieldType.UInt16,
            Length = 10,
            Comment = "seconds",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "Q_SECTIONTIMER",
                LookUpTable = new IntLookupTable
                {
                    {0, 0},
                    {1, 10}
                }
            }
        };

        public static BitField T_TEXTDISPLAY = new BitField
        {
            Name = "T_TEXTDISPLAY",
            BitFieldType = BitFieldType.UInt16,
            Length = 10,
            Comment = "seconds"
        };

        public static BitField T_TRAIN = new BitField
        {
            Name = "T_TRAIN",
            BitFieldType = BitFieldType.UInt32,
            Length = 32,
            Comment = "10 ms"
        };

        public static BitField T_VBC = new BitField
        {
            Name = "T_VBC",
            BitFieldType = BitFieldType.UInt16,
            Length = 8,
            Comment = "days"
        };

        public static BitField T_TIMEOUTRQST = new BitField
        {
            Name = "T_TIMEOUTRQST",
            BitFieldType = BitFieldType.UInt16,
            Length = 10,
            Comment = "seconds"
        };

        #endregion

        #region V_

        public static BitField V_DIFF = new BitField
        {
            Name = "V_DIFF",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_EMA = new BitField
        {
            Name = "V_EMA",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_MAIN = new BitField
        {
            Name = "V_MAIN",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_MAMODE = new BitField
        {
            Name = "V_MAMODE",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_MAXTRAIN = new BitField
        {
            Name = "V_MAXTRAIN",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_NVALLOWOVTRP = new BitField
        {
            Name = "V_NVALLOWOVTRP",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_NVKVINT = new BitField
        {
            Name = "V_NVKVINT",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_NVLIMSUPERV = new BitField
        {
            Name = "V_NVLIMSUPERV",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_NVONSIGHT = new BitField
        {
            Name = "V_NVONSIGHT",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_NVSUPOVTRP = new BitField
        {
            Name = "V_NVSUPOVTRP",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_NVREL = new BitField
        {
            Name = "V_NVREL",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_NVSHUNT = new BitField
        {
            Name = "V_NVSHUNT",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_NVSTFF = new BitField
        {
            Name = "V_NVSTFF",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_NVUNFIT = new BitField
        {
            Name = "V_NVUNFIT",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_RELEASEDP = new BitField
        {
            Name = "V_RELEASEDP",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "Q_DANGERPOINT",
                LookUpTable = new IntLookupTable
                {
                    {0, 0},
                    {1, 7}
                }
            }
        };

        public static BitField V_REVERSE = new BitField
        {
            Name = "V_REVERSE",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_RELEASEOL = new BitField
        {
            Name = "V_RELEASEOL",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h",
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "Q_OVERLAP",
                LookUpTable = new IntLookupTable
                {
                    {0, 0},
                    {1, 7}
                }
            }
        };

        public static BitField V_STATIC = new BitField
        {
            Name = "V_STATIC",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_TRAIN = new BitField
        {
            Name = "V_TRAIN",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        public static BitField V_TSR = new BitField
        {
            Name = "V_TSR",
            BitFieldType = BitFieldType.UInt16,
            Length = 7,
            Comment = "5 km/h"
        };

        #endregion

        public static BitField X_TEXT = new BitField
        {
            Name = "X_TEXT",
            BitFieldType = BitFieldType.StringLatin,
            VariableLengthSettings = new VariableLengthSettings
            {
                Name = "L_TEXT",
                ScalingFactor = 8
            }
        };

        #region Track To Train Chapter 7

        public static DataSetDefinition Packet0VirtualBaliseCoverMarker =
            new DataSetDefinition
            {
                Name = "Virtual Balise Cover marker",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    NID_VBCMK
                }
            };

        public static DataSetDefinition Packet2SystemVersionOrder =
            new DataSetDefinition
            {
                Name = "System Version order",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    M_VERSION
                }
            };

        // TODO needs testcases
        public static DataSetDefinition Packet3NationalValues =
            new DataSetDefinition
            {
                Name = "National Values",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    D_VALIDNV,
                    NID_C,
                    N_ITER,
                    new BitField
                    {
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "N_ITER"
                        },
                        NestedDataSet = new DataSetDefinition
                        {
                            BitFields = new List<BitField>
                            {
                                NID_C
                            }
                        }
                    },
                    V_NVSHUNT,
                    V_NVSTFF,
                    V_NVONSIGHT,
                    V_NVLIMSUPERV,
                    V_NVUNFIT,
                    V_NVREL,
                    D_NVROLL,
                    Q_NVSBTSMPERM,
                    Q_NVEMRRLS,
                    Q_NVGUIPERM,
                    Q_NVSBFBPERM,
                    Q_NVINHSMICPERM,
                    V_NVALLOWOVTRP,
                    V_NVSUPOVTRP,
                    D_NVOVTRP,
                    T_NVOVTRP,
                    D_NVPOTRP,
                    M_NVCONTACT,
                    T_NVCONTACT,
                    M_NVDERUN,
                    D_NVSTFF,
                    Q_NVDRIVER_ADHES,
                    A_NVMAXREDADH1,
                    A_NVMAXREDADH2,
                    A_NVMAXREDADH3,
                    Q_NVLOCACC,
                    M_NVAVADH,
                    M_NVEBCL,
                    Q_NVKINT,
                    new BitField
                    {
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "Q_NVKINT",
                            LookUpTable = new IntLookupTable
                            {
                                {0, 0},
                                {1, 1}
                            }
                        },
                        NestedDataSet = new DataSetDefinition
                        {
                            BitFields = new List<BitField>
                            {
                                Q_NVKVINTSET,
                                new BitField
                                {
                                    VariableLengthSettings = new VariableLengthSettings
                                    {
                                        Name = "Q_NVKVINTSET",
                                        LookUpTable = new IntLookupTable
                                        {
                                            {0, 0},
                                            {1, 1},
                                            {2, 0},
                                            {3, 0}
                                        }
                                    },
                                    NestedDataSet = new DataSetDefinition
                                    {
                                        BitFields = new List<BitField>
                                        {
                                            A_NVP12,
                                            A_NVP23
                                        }
                                    }
                                },
                                V_NVKVINT,
                                M_NVKVINT,
                                new BitField
                                {
                                    VariableLengthSettings = new VariableLengthSettings
                                    {
                                        Name = "Q_NVKVINTSET",
                                        LookUpTable = new IntLookupTable
                                        {
                                            {0, 0},
                                            {1, 1},
                                            {2, 0},
                                            {3, 0}
                                        }
                                    },
                                    NestedDataSet = new DataSetDefinition
                                    {
                                        BitFields = new List<BitField>
                                        {
                                            M_NVKVINT
                                        }
                                    }
                                },

                                N_ITER,
                                new BitField
                                {
                                    VariableLengthSettings = new VariableLengthSettings
                                    {
                                        Name = "N_ITER"
                                    },
                                    NestedDataSet = new DataSetDefinition
                                    {
                                        BitFields = new List<BitField>
                                        {
                                            V_NVKVINT,
                                            M_NVKVINT,
                                            new BitField
                                            {
                                                VariableLengthSettings = new VariableLengthSettings
                                                {
                                                    Name = "Q_NVKVINTSET",
                                                    LookUpTable = new IntLookupTable
                                                    {
                                                        {0, 0},
                                                        {1, 1},
                                                        {2, 0},
                                                        {3, 0}
                                                    }
                                                },
                                                NestedDataSet = new DataSetDefinition
                                                {
                                                    BitFields = new List<BitField>
                                                    {
                                                        M_NVKVINT
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },

                                N_ITER,
                                new BitField
                                {
                                    VariableLengthSettings = new VariableLengthSettings
                                    {
                                        Name = "N_ITER"
                                    },
                                    NestedDataSet = new DataSetDefinition
                                    {
                                        BitFields = new List<BitField>
                                        {
                                            Q_NVKVINTSET,
                                            new BitField
                                            {
                                                VariableLengthSettings = new VariableLengthSettings
                                                {
                                                    Name = "Q_NVKVINTSET",
                                                    LookUpTable = new IntLookupTable
                                                    {
                                                        {0, 0},
                                                        {1, 1},
                                                        {2, 0},
                                                        {3, 0}
                                                    }
                                                },
                                                NestedDataSet = new DataSetDefinition
                                                {
                                                    BitFields = new List<BitField>
                                                    {
                                                        A_NVP12,
                                                        A_NVP23
                                                    }
                                                }
                                            },
                                            V_NVKVINT,
                                            M_NVKVINT,
                                            new BitField
                                            {
                                                VariableLengthSettings = new VariableLengthSettings
                                                {
                                                    Name = "Q_NVKVINTSET",
                                                    LookUpTable = new IntLookupTable
                                                    {
                                                        {0, 0},
                                                        {1, 1},
                                                        {2, 0},
                                                        {3, 0}
                                                    }
                                                },
                                                NestedDataSet = new DataSetDefinition
                                                {
                                                    BitFields = new List<BitField>
                                                    {
                                                        M_NVKVINT
                                                    }
                                                }
                                            },
                                            N_ITER,
                                            new BitField
                                            {
                                                VariableLengthSettings = new VariableLengthSettings
                                                {
                                                    Name = "N_ITER"
                                                },
                                                NestedDataSet = new DataSetDefinition
                                                {
                                                    BitFields = new List<BitField>
                                                    {
                                                        V_NVKVINT,
                                                        M_NVKVINT,
                                                        new BitField
                                                        {
                                                            VariableLengthSettings = new VariableLengthSettings
                                                            {
                                                                Name = "Q_NVKVINTSET",
                                                                LookUpTable = new IntLookupTable
                                                                {
                                                                    {0, 0},
                                                                    {1, 1},
                                                                    {2, 0},
                                                                    {3, 0}
                                                                }
                                                            },
                                                            NestedDataSet = new DataSetDefinition
                                                            {
                                                                BitFields = new List<BitField>
                                                                {
                                                                    M_NVKVINT
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },


                                L_NVKRINT,
                                M_NVKRINT,
                                N_ITER,
                                new BitField
                                {
                                    VariableLengthSettings = new VariableLengthSettings
                                    {
                                        Name = "N_ITER"
                                    },
                                    NestedDataSet = new DataSetDefinition
                                    {
                                        BitFields = new List<BitField>
                                        {
                                            L_NVKRINT,
                                            M_NVKRINT
                                        }
                                    }
                                },
                                M_NVKTINT
                            }
                        }
                    }
                }
            };


        public static DataSetDefinition Packet5Linking = new DataSetDefinition
        {
            Name = "Linking",
            BitFields = new List<BitField>
            {
                NID_PACKET,
                Q_DIR,
                L_PACKET,
                Q_SCALE,
                D_LINK,
                Q_NEWCOUNTRY,
                new BitField
                {
                    Name = "NID_C",
                    BitFieldType = BitFieldType.UInt16,
                    Comment = "Country or region",
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "Q_NEWCOUNTRY",
                        LookUpTable = new IntLookupTable
                        {
                            {0, 0},
                            {1, 10}
                        }
                    }
                },
                NID_BG,
                Q_LINKORIENTATION,
                Q_LINKREACTION,
                Q_LOCACC,
                N_ITER,
                new BitField
                {
                    Name = "Nested1",
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "N_ITER"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            D_LINK,
                            Q_NEWCOUNTRY,
                            new BitField
                            {
                                Name = "NID_C",
                                BitFieldType = BitFieldType.UInt16,
                                Comment = "Country or region",
                                VariableLengthSettings = new VariableLengthSettings
                                {
                                    Name = "Q_NEWCOUNTRY",
                                    LookUpTable = new IntLookupTable
                                    {
                                        {0, 0},
                                        {1, 10}
                                    }
                                }
                            },
                            NID_BG,
                            Q_LINKORIENTATION,
                            Q_LINKREACTION,
                            Q_LOCACC
                        }
                    }
                }
            }
        };


        public static DataSetDefinition Packet6VirtualBaliseCoverOrder =
            new DataSetDefinition
            {
                Name = "Virtual Balise Cover order",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_VBCO,
                    NID_VBCMK,
                    NID_C,
                    T_VBC
                }
            };


        public static DataSetDefinition Packet12Level1MovementAuthority =
            new DataSetDefinition
            {
                Name = "Level 1 Movement Authority",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    V_MAIN,
                    V_EMA,
                    T_EMA,
                    N_ITER,
                    new BitField
                    {
                        Name = "NestedSet1",
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "N_ITER"
                        },
                        NestedDataSet = new DataSetDefinition
                        {
                            BitFields = new List<BitField>
                            {
                                L_SECTION,
                                Q_SECTIONTIMER,
                                T_SECTIONTIMER,
                                D_SECTIONTIMERSTOPLOC
                            }
                        }
                    },
                    L_ENDSECTION,
                    Q_SECTIONTIMER,
                    T_SECTIONTIMER,
                    D_SECTIONTIMERSTOPLOC,
                    Q_ENDTIMER,
                    T_ENDTIMER,
                    D_ENDTIMERSTARTLOC,
                    Q_DANGERPOINT,
                    D_DP,
                    V_RELEASEDP,
                    Q_OVERLAP,
                    D_STARTOL,
                    T_OL,
                    D_OL,
                    V_RELEASEOL
                }
            };


        /* TODO implement this
    public static DataSetDefinition Packet13StaffResponsibleDistanceInformationFromLoop =
        new DataSetDefinition {BitFields = new List<BitField> { }};
        */


        public static DataSetDefinition Packet15Level23MovementAuthority = new DataSetDefinition
        {
            Name = "Level 2/3 Movement Authority",
            BitFields = new List<BitField>
            {
                NID_PACKET,
                Q_DIR,
                L_PACKET,
                Q_SCALE,
                V_EMA,
                T_EMA,
                N_ITER,
                new BitField
                {
                    Name = "NestedSet1",
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "N_ITER"
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            L_SECTION,
                            Q_SECTIONTIMER,
                            T_SECTIONTIMER,
                            D_SECTIONTIMERSTOPLOC
                        }
                    }
                },
                L_ENDSECTION,
                Q_SECTIONTIMER,
                T_SECTIONTIMER,
                D_SECTIONTIMERSTOPLOC,
                Q_ENDTIMER,
                T_ENDTIMER,
                D_ENDTIMERSTARTLOC,
                Q_DANGERPOINT,
                D_DP,
                V_RELEASEDP,
                Q_OVERLAP,
                D_STARTOL,
                T_OL,
                D_OL,
                V_RELEASEOL
            }
        };

        public static DataSetDefinition Packet16RepositioningInformation =
            new DataSetDefinition
            {
                Name = "Repositioning Information",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    L_SECTION
                }
            };


        public static DataSetDefinition Packet21GradientProfile =
            new DataSetDefinition
            {
                Name = "Gradient Profile",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    D_GRADIENT,
                    Q_GDIR,
                    G_A,
                    N_ITER,
                    new BitField
                    {
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "N_ITER"
                        },
                        NestedDataSet = new DataSetDefinition
                        {
                            BitFields = new List<BitField>
                            {
                                D_GRADIENT,
                                Q_GDIR,
                                G_A
                            }
                        }
                    }
                }
            };

        // TODO THIS BUGGER needs testcases
        public static DataSetDefinition Packet27InternationalStaticSpeedProfile =
            new DataSetDefinition
            {
                Name = "International Static Speed Profile",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    D_STATIC,
                    V_STATIC,
                    Q_FRONT,
                    N_ITER,
                    new BitField
                    {
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "N_ITER"
                        },
                        NestedDataSet = new DataSetDefinition
                        {
                            BitFields = new List<BitField>
                            {
                                Q_DIFF,
                                NC_CDDIFF,
                                NC_DIFF,
                                V_DIFF
                            }
                        }
                    },

                    N_ITER,

                    new BitField
                    {
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "N_ITER"
                        },
                        NestedDataSet = new DataSetDefinition
                        {
                            BitFields = new List<BitField>
                            {
                                D_STATIC,
                                V_STATIC,
                                Q_FRONT,
                                N_ITER,
                                new BitField
                                {
                                    VariableLengthSettings = new VariableLengthSettings
                                    {
                                        Name = "N_ITER"
                                    },
                                    NestedDataSet = new DataSetDefinition
                                    {
                                        BitFields = new List<BitField>
                                        {
                                            Q_DIFF,
                                            NC_CDDIFF,
                                            NC_DIFF,
                                            V_DIFF
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };


        /* TODO implement this
    public static DataSetDefinition Packet39TrackConditionChangeOfTractionSystem =
        new DataSetDefinition BitFields = new List<BitField> { }};
        */

        public static DataSetDefinition Packet40TrackConditionChangeOfAllowedCurrentConsumption =
            new DataSetDefinition
            {
                Name = "Track Condition Change of allowed current consumption",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    D_CURRENT,
                    M_CURRENT
                }
            };


        public static DataSetDefinition Packet41LevelTransitionOrder =
            new DataSetDefinition
            {
                Name = "Level Transition Order",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    D_LEVELTR,
                    M_LEVELTR,
                    new BitField
                    {
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "M_LEVELTR",
                            LookUpTable = new IntLookupTable
                            {
                                {0, 0},
                                {1, 1},
                                {2, 0},
                                {3, 0},
                                {4, 0},
                                {5, 0},
                                {6, 0},
                                {7, 0}
                            }
                        },
                        NestedDataSet = new DataSetDefinition
                        {
                            BitFields = new List<BitField>
                            {
                                NID_NTC
                            }
                        }
                    },
                    L_ACKLEVELTR,
                    N_ITER,
                    new BitField
                    {
                        VariableLengthSettings = new VariableLengthSettings {Name = "N_ITER"},
                        NestedDataSet = new DataSetDefinition
                        {
                            BitFields = new List<BitField>
                            {
                                M_LEVELTR,
                                new BitField
                                {
                                    VariableLengthSettings = new VariableLengthSettings
                                    {
                                        Name = "M_LEVELTR",
                                        LookUpTable = new IntLookupTable
                                        {
                                            {0, 0},
                                            {1, 1},
                                            {2, 0},
                                            {3, 0},
                                            {4, 0},
                                            {5, 0},
                                            {6, 0},
                                            {7, 0}
                                        }
                                    },
                                    NestedDataSet = new DataSetDefinition
                                    {
                                        BitFields = new List<BitField>
                                        {
                                            NID_NTC
                                        }
                                    }
                                },
                                L_ACKLEVELTR
                            }
                        }
                    }
                }
            };


        public static DataSetDefinition Packet42SessionManagement =
            new DataSetDefinition
            {
                Name = "Session Management",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_RBC,
                    NID_C,
                    NID_RBC,
                    NID_RADIO,
                    Q_SLEEPSESSION
                }
            };

        
        public static DataSetDefinition Packet44TrackToTrain =
            new DataSetDefinition {
                Name = "Packet 44",
                BitFields = new List<BitField>
            {
                NID_PACKET,
                Q_DIR,
                L_PACKET,
                new BitField{Name = "NID_XUSER", Length = 9, BitFieldType = BitFieldType.UInt16}
            }
            };
            

        public static DataSetDefinition Packet45RadioNetworkRegistration =
            new DataSetDefinition
            {
                Name = "Radio Network registration",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    NID_MN
                }
            };

        
        public static DataSetDefinition Packet46ConditionalLevelTransitionOrder =
            new DataSetDefinition {
                Name = "Conditional Level Transition Order",
                BitFields = new List<BitField>
            {
                NID_PACKET,
                Q_DIR,
                L_PACKET,
                M_LEVELTR,
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings
                    {
                        Name = "M_LEVELTR",
                        LookUpTable = new IntLookupTable
                        {
                            {0, 0},
                            {1, 1},
                            {2, 0},
                            {3, 0},
                            {4, 0},
                            {5, 0},
                            {6, 0},
                            {7, 0}
                        }
                    },
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            NID_NTC
                        }
                    }
                },
                N_ITER,
                new BitField
                {
                    VariableLengthSettings = new VariableLengthSettings {Name = "N_ITER"},
                    NestedDataSet = new DataSetDefinition
                    {
                        BitFields = new List<BitField>
                        {
                            M_LEVELTR,
                            new BitField
                            {
                                VariableLengthSettings = new VariableLengthSettings
                                {
                                    Name = "M_LEVELTR",
                                    LookUpTable = new IntLookupTable
                                    {
                                        {0, 0},
                                        {1, 1},
                                        {2, 0},
                                        {3, 0},
                                        {4, 0},
                                        {5, 0},
                                        {6, 0},
                                        {7, 0}
                                    }
                                },
                                NestedDataSet = new DataSetDefinition
                                {
                                    BitFields = new List<BitField>
                                    {
                                        NID_NTC
                                    }
                                }
                            }
                        }
                    }
                }
            }
            };
        

        /* TODO implement this
    public static DataSetDefinition Packet49ListOfBalisesForShArea =
        new DataSetDefinition {BitFields = new List<BitField> { }};


    public static DataSetDefinition Packet51AxleLoadSpeedProfile =
        new DataSetDefinition {BitFields = new List<BitField> { }};

    public static DataSetDefinition Packet52PermittedBrakingDistanceInformation =
        new DataSetDefinition {BitFields = new List<BitField> { }};
        */

        public static DataSetDefinition Packet57MovementAuthorityRequestParameters =
            new DataSetDefinition
            {
                Name = "Movement Authority Request Parameters",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    T_MAR,
                    T_TIMEOUTRQST,
                    T_CYCRQST
                }
            };


        public static DataSetDefinition Packet58PositionReportParameters =
            new DataSetDefinition
            {
                Name = "Position Report Parameters",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    T_CYCLOC,
                    D_CYCLOC,
                    M_LOC,
                    N_ITER,
                    new BitField
                    {
                        Name = "Nested1",
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "N_ITER"
                        },
                        NestedDataSet = new DataSetDefinition
                        {
                            BitFields = new List<BitField>
                            {
                                D_LOC,
                                Q_LGTLOC
                            }
                        }
                    }
                }
            };

        /* TODO Implement this
        public static DataSetDefinition Packet63ListOfBalisesInSrAuthority =
            new DataSetDefinition {BitFields = new List<BitField> { }};
            */

        public static DataSetDefinition Packet64InhibitionOfRevocableTsrsFromBalisesInL23 =
            new DataSetDefinition
            {
                Name = "Inhibition of revocable TSRs from balises in L2/3",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET
                }
            };

        public static DataSetDefinition Packet65TemporarySpeedRestriction =
            new DataSetDefinition
            {
                Name = "Temporary Speed Restriction",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    NID_TSR,
                    D_TSR,
                    L_TSR,
                    Q_FRONT,
                    V_TSR
                }
            };

        public static DataSetDefinition Packet66TemporarySpeedRestrictionRevocation =
            new DataSetDefinition
            {
                Name = "Temporary Speed Restriction Revocation",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    NID_TSR
                }
            };

        /* TODO implement this
        public static DataSetDefinition Packet67TrackConditionBigMetalMasses =
            new DataSetDefinition {BitFields = new List<BitField> { }};
            */

        public static DataSetDefinition Packet68TrackCondition =
            new DataSetDefinition
            {
                Name = "Track Condition",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    Q_TRACKINIT,
                    D_TRACKINIT,
                    new BitField
                    {
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "Q_TRACKINIT",
                            LookUpTable = new IntLookupTable
                            {
                                {0, 1}, // if 0, one iteration of dataset, not the total length
                                {1, 0}
                            }
                        },
                        NestedDataSet = new DataSetDefinition
                        {
                            BitFields = new List<BitField>
                            {
                                D_TRACKCOND,
                                L_TRACKCOND,
                                M_TRACKCOND,
                                N_ITER,
                                new BitField
                                {
                                    VariableLengthSettings = new VariableLengthSettings
                                    {
                                        Name = "N_ITER"
                                    },
                                    NestedDataSet = new DataSetDefinition
                                    {
                                        BitFields = new List<BitField>
                                        {
                                            D_TRACKCOND,
                                            L_TRACKCOND,
                                            M_TRACKCOND
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

        /* TODO implement this
    public static DataSetDefinition Packet69TrackConditionStationPlatforms =
        new DataSetDefinition {BitFields = new List<BitField> { }};

    public static DataSetDefinition Packet70RouteSuitabilityData =
        new DataSetDefinition {BitFields = new List<BitField> { }};
    */

        public static DataSetDefinition Packet71AdhesionFactor =
            new DataSetDefinition
            {
                Name = "Adhesion Factor",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    D_ADHESION,
                    L_ADHESION,
                    M_ADHESION
                }
            };


        public static DataSetDefinition Packet72PacketForSendingPlainTextMessages =
            new DataSetDefinition
            {
                Name = "Packet for sending plain text messages",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    Q_TEXTCLASS,
                    Q_TEXTDISPLAY,
                    D_TEXTDISPLAY,
                    M_MODETEXTDISPLAY.Clone("M_MODETEXTDISPLAYStart"),
                    M_LEVELTEXTDISPLAY.Clone("M_LEVELTEXTDISPLAYStart"),
                    new BitField
                    {
                        Name = "NID_NTCStart",
                        BitFieldType = BitFieldType.UInt16,
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "M_LEVELTEXTDISPLAYStart",
                            LookUpTable = new IntLookupTable
                            {
                                {0, 0},
                                {1, 8},
                                {2, 0},
                                {3, 0},
                                {4, 0},
                                {5, 0},
                                {6, 0},
                                {7, 0}
                            }
                        }
                    },
                    L_TEXTDISPLAY,
                    T_TEXTDISPLAY,
                    M_MODETEXTDISPLAY.Clone("M_MODETEXTDISPLAYEnd"),
                    M_LEVELTEXTDISPLAY.Clone("M_LEVELTEXTDISPLAYEnd"),
                    new BitField
                    {
                        Name = "NID_NTCEnd",
                        BitFieldType = BitFieldType.UInt16,
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "M_LEVELTEXTDISPLAYEnd",
                            LookUpTable = new IntLookupTable
                            {
                                {0, 0},
                                {1, 8},
                                {2, 0},
                                {3, 0},
                                {4, 0},
                                {5, 0},
                                {6, 0},
                                {7, 0}
                            }
                        }
                    },
                    Q_TEXTCONFIRM,
                    
                    
                    new BitField
                    {
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "Q_TEXTCONFIRM",
                            LookUpTable = new IntLookupTable
                            {
                                {0, 0},
                                {1, 1},
                                {2, 1},
                                {3, 1}
                            }
                        },
                        NestedDataSet = new DataSetDefinition()
                        {
                            BitFields = new List<BitField>()
                            {
                                new BitField
                                {
                                    Name = "Q_CONFTEXTDISPLAY",
                                    BitFieldType = BitFieldType.UInt16,
                                    Length = 1,
                                    LookupTable = new LookupTable
                                    {
                                        {
                                            "0",
                                            "Driver acknowledgement always ends the text display, regardless of the end condition"
                                        },
                                        {"1", "Driver acknowledgement is an additional condition to end the display"}
                                    }
                                },
                                new BitField
                                {
                                    Name = "Q_TEXTREPORT",
                                    BitFieldType = BitFieldType.UInt16,
                                    Length = 1,
                                    LookupTable = new LookupTable
                                    {
                                        {"0", "No driver acknowledgement report required"},
                                        {"1", "Driver acknowledgement report required"}
                                    }
                                },
                                new BitField()
                                {
                                    VariableLengthSettings = new VariableLengthSettings
                                    {
                                        Name = "Q_TEXTREPORT",
                                        LookUpTable = new IntLookupTable
                                        {
                                            {0, 0},
                                            {1, 1}
                                        }
                                    },
                                    NestedDataSet = new DataSetDefinition()
                                    {
                                        BitFields = new List<BitField>()
                                        {
                                            new BitField
                                            {
                                                Name = "NID_TEXTMESSAGE",
                                                BitFieldType = BitFieldType.UInt16
                                            },
                                            new BitField
                                            {
                                                Name = "NID_C",
                                                BitFieldType = BitFieldType.UInt16,
                                                Comment = "Country or region",
                                            },
                                            new BitField
                                            {
                                                Name = "NID_RBC",
                                                BitFieldType = BitFieldType.UInt16,
                                                Comment = "16 383 = Contact last known RBC",
                                            },
                                        }
                                    }
                                }
                            }
                        },
                    },
                    
                    L_TEXT,
                    X_TEXT
                }
            };

        /* TODO implement this
        public static DataSetDefinition Packet76PacketForSendingFixedTextMessages =
            new DataSetDefinition {BitFields = new List<BitField> { }};

        public static DataSetDefinition Packet79GeographicalPositionInformation =
            new DataSetDefinition {BitFields = new List<BitField> { }};
        */


        public static DataSetDefinition Packet80ModeProfile = new DataSetDefinition
            {
                Name = "Mode profile",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    D_MAMODE,
                    M_MAMODE,
                    V_MAMODE,
                    L_MAMODE,
                    L_ACKMAMODE,
                    Q_MAMODE,
                    N_ITER,
                    new BitField
                    {
                        Name = "Nested1",
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "N_ITER"
                        },
                        NestedDataSet = new DataSetDefinition
                        {
                            BitFields = new List<BitField>
                            {
                                D_MAMODE,
                                M_MAMODE,
                                V_MAMODE,
                                L_MAMODE,
                                L_ACKMAMODE,
                                Q_MAMODE
                            }
                        }
                    }
                }
            }
            ;

        /* TODO implement this
        public static DataSetDefinition Packet88LevelCrossingInformation =
            new DataSetDefinition {BitFields = new List<BitField> { }};

        public static DataSetDefinition Packet90TrackAheadFreeUpToLevel23TransitionLocation =
            new DataSetDefinition {BitFields = new List<BitField> { }};
        */

        public static DataSetDefinition Packet131RbcTransitionOrder =
            new DataSetDefinition
            {
                Name = "RBC transition order",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    D_RBCTR,
                    NID_C,
                    NID_RBC,
                    NID_RADIO,
                    Q_SLEEPSESSION
                }
            };

        public static DataSetDefinition Packet132DangerForShuntingInformation =
            new DataSetDefinition
            {
                Name = "Danger for Shunting information",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_ASPECT
                }
            };

        public static DataSetDefinition Packet133RadioInfillAreaInformation =
            new DataSetDefinition
            {
                Name = "Radio infill area information",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    Q_RIU,
                    NID_C,
                    NID_RIU,
                    NID_RADIO,
                    D_INFILL,
                    NID_C,
                    NID_BG
                }
            };

        public static DataSetDefinition Packet134EolmPacket = new DataSetDefinition
            {
                Name = "EOLM Packet",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    NID_LOOP,
                    D_LOOP,
                    L_LOOP,
                    Q_LOOPDIR,
                    Q_SSCODE
                }
            }
            ;

        public static DataSetDefinition Packet135StopShuntingOnDeskOpening =
            new DataSetDefinition
            {
                Name = "Stop Shunting on desk opening",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET
                }
            };

        public static DataSetDefinition Packet136InfillLocationReference =
            new DataSetDefinition
            {
                Name = "Infill location reference",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_NEWCOUNTRY,
                    NID_C,
                    NID_BG
                }
            };

        public static DataSetDefinition Packet137StopIfInStaffResponsible =
            new DataSetDefinition
            {
                Name = "Stop if in Staff Responsible",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SRSTOP
                }
            };

        public static DataSetDefinition Packet138ReversingAreaInformation =
            new DataSetDefinition
            {
                Name = "Reversing area information",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    D_STARTREVERSE,
                    L_REVERSEAREA
                }
            };

        public static DataSetDefinition Packet139ReversingSupervisionInformation =
            new DataSetDefinition
            {
                Name = "Reversing supervision information",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_SCALE,
                    D_REVERSE,
                    V_REVERSE
                }
            };

        public static DataSetDefinition Packet140TrainRunningNumberFromRbc =
            new DataSetDefinition
            {
                Name = "Train running number from RBC",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    NID_OPERATIONAL
                }
            };

        public static DataSetDefinition Packet141DefaultGradientForTemporarySpeedRestriction =
            new DataSetDefinition
            {
                Name = "Default Gradient for Temporary Speed Restriction",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_GDIR,
                    G_TSR
                }
            };

        public static DataSetDefinition Packet143SessionManagementWithNeighbouringRadioInfillUnit =
            new DataSetDefinition
            {
                Name = "Session Management with neighbouring Radio Infill Unit",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_RIU,
                    NID_C,
                    NID_RIU,
                    NID_RADIO
                }
            };

        public static DataSetDefinition Packet145InhibitionOfBaliseGroupMessageConsistencyReaction =
            new DataSetDefinition
            {
                Name = "Inhibition of balise group message consistency reaction",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET
                }
            };

        public static DataSetDefinition Packet180LssmaDisplayToggleOrder =
            new DataSetDefinition
            {
                Name = "LSSMA display toggle order",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET,
                    Q_LSSMA,
                    T_LSSMA
                }
            };

        public static DataSetDefinition Packet181GenericLsFunctionMarker =
            new DataSetDefinition
            {
                Name = "Generic LS function marker",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    Q_DIR,
                    L_PACKET
                }
            };

        public static DataSetDefinition Packet254DefaultBaliseLoopOrRiuInformation = new DataSetDefinition
        {
            Name = "Default balise, loop or RIU information",
            BitFields = new List<BitField>
            {
                NID_PACKET,
                Q_DIR,
                L_PACKET
            }
        };

        #endregion

        #region Train To Track Chapter 7

        public static DataSetDefinition Packet0PositionReport =
            new DataSetDefinition
            {
                Name = "Position Report",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    L_PACKET,
                    Q_SCALE,
                    NID_LRBG,
                    D_LRBG,
                    Q_DIRLRBG,
                    Q_DLRBG,
                    L_DOUBTOVER,
                    L_DOUBTUNDER,
                    Q_LENGTH,
                    L_TRAININT,
                    V_TRAIN,
                    Q_DIRTRAIN,
                    M_MODE,
                    M_LEVEL,
                    new BitField
                    {
                        Name = "NID_NTC",
                        BitFieldType = BitFieldType.UInt16,
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "M_LEVEL",
                            LookUpTable = new IntLookupTable
                            {
                                {0, 0},
                                {1, 8},
                                {2, 0},
                                {3, 0},
                                {4, 0},
                                {5, 0},
                                {6, 0},
                                {7, 0}
                            }
                        }
                    }
                }
            };

        public static DataSetDefinition Packet1PositionReportBasedOnTwoBaliseGroups =
            new DataSetDefinition
            {
                Name = "Position Report based on two balise groups",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    L_PACKET,
                    Q_SCALE,
                    NID_LRBG,
                    NID_PRVLRBG,
                    D_LRBG,
                    Q_DIRLRBG,
                    Q_DLRBG,
                    L_DOUBTOVER,
                    L_DOUBTUNDER,
                    Q_LENGTH,
                    L_TRAININT,
                    V_TRAIN,
                    Q_DIRTRAIN,
                    M_MODE,
                    M_LEVEL,
                    new BitField
                    {
                        Name = "NID_NTC",
                        BitFieldType = BitFieldType.UInt16,
                        VariableLengthSettings = new VariableLengthSettings
                        {
                            Name = "M_LEVEL",
                            LookUpTable = new IntLookupTable
                            {
                                {0, 0},
                                {1, 8},
                                {2, 0},
                                {3, 0},
                                {4, 0},
                                {5, 0},
                                {6, 0},
                                {7, 0}
                            }
                        }
                    }
                }
            };

        /* TODO implement this
        public static DataSetDefinition Packet3OnboardTelephoneNumbers =
            new DataSetDefinition {BitFields = new List<BitField> { }};
        */

        public static DataSetDefinition Packet4ErrorReporting =
            new DataSetDefinition
            {
                Name = "Error Reporting",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    L_PACKET,
                    M_ERROR
                }
            };

        public static DataSetDefinition Packet5TrainRunningNumber =
            new DataSetDefinition
            {
                Name = "Train running number",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    L_PACKET,
                    NID_OPERATIONAL
                }
            };

        public static DataSetDefinition Packet9Level23TransitionInformation =
            new DataSetDefinition
            {
                Name = "Level 2/3 transition information",
                BitFields = new List<BitField>
                {
                    NID_PACKET,
                    L_PACKET,
                    NID_LTRBG
                }
            };

        /* todo implement
        public static DataSetDefinition Packet11ValidatedTrainData =
            new DataSetDefinition {BitFields = new List<BitField> { }};
        */

        /* todo implement
    public static DataSetDefinition Packet44TrainToTrack =
        new DataSetDefinition {BitFields = new List<BitField> { }};
        */

        #endregion

        #region Both Directions Chapter 7

        public static DataSetDefinition Packet255EndOfInformation =
            new DataSetDefinition
            {
                BitFields = new List<BitField>
                {
                    NID_PACKET
                }
            };

        #endregion

        public static DataSetDefinition BaliseHeader = new DataSetDefinition
        {
            Name = "Subset26 Balise Header",
            BitFields = new List<BitField>
            {
                Q_UPDOWN,
                M_VERSION,
                Q_MEDIA,
                N_PIG,
                N_TOTAL,
                M_DUP,
                M_COUNT,
                NID_C,
                NID_BG,
                Q_LINK
            }
        };

        #region RadioMessages Chapter 8

        public static DataSetDefinition Message129ValidatedTrainData = new DataSetDefinition
        {
            Name = "Message 129: Validated Train Data",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE
            }
        };

        public static DataSetDefinition Message130RequestForShunting = new DataSetDefinition
        {
            Name = "Message 130: Request for Shunting",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE
            }
        };

        public static DataSetDefinition Message132MARequest = new DataSetDefinition
        {
            Name = "Message 132: MA Request",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE,
                Q_MARQSTREASON1,
                Q_MARQSTREASON2,
                Q_MARQSTREASON3,
                Q_MARQSTREASON4,
                Q_MARQSTREASON5
            }
        };

        public static DataSetDefinition Message136TrainPositionReport = new DataSetDefinition
        {
            Name = "Message 136: Train Position Report",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE
            }
        };

        public static DataSetDefinition Message137RequestToShortenMAIsGranted = new DataSetDefinition
        {
            Name = "Message 137: Request to Shorten MA is granted",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE,
                T_TRAIN
            }
        };

        public static DataSetDefinition Message138RequestToShortenMASsRejected = new DataSetDefinition
        {
            Name = "Message 138: Request to Shorten MA is rejected",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE,
                T_TRAIN
            }
        };

        public static DataSetDefinition Message146Acknowledgement = new DataSetDefinition
        {
            Name = "Message 146: Acknowledgement",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE,
                T_TRAIN
            }
        };

        public static DataSetDefinition Message147AcknowledgementOfEmergencyStop = new DataSetDefinition
        {
            Name = "Message 147: Acknowledgement of Emergency Stop",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE,
                NID_EM,
                Q_EMERGENCYSTOP
            }
        };

        public static DataSetDefinition Message149TrackAheadFreeGranted = new DataSetDefinition
        {
            Name = "Message 149: Track Ahead Free Granted",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE
            }
        };

        public static DataSetDefinition Message150EndOfMission = new DataSetDefinition
        {
            Name = "Message 150: End of Mission",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE
            }
        };

        public static DataSetDefinition Message153RadioInfillRequest = new DataSetDefinition
        {
            Name = "Message 153: Radio infill request",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE,
                NID_C,
                NID_BG,
                Q_INFILL
            }
        };


        public static DataSetDefinition Message154NoCompatibleVersionSupported = new DataSetDefinition
        {
            Name = "Message 154: No compatible version supported",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE
            }
        };

        public static DataSetDefinition Message155InitiationOfACommunicationSession = new DataSetDefinition
        {
            Name = "Message 155: Initiation of a communication session",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE
            }
        };

        public static DataSetDefinition Message156TerminationOfACommunicationSession = new DataSetDefinition
        {
            Name = "Message 156: Termination of a communication session",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE
            }
        };

        public static DataSetDefinition Message157SoMPositionReport = new DataSetDefinition
        {
            Name = "Message 157: SoM Position Report",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE,
                Q_STATUS
            }
        };

        public static DataSetDefinition Message158TextMessageAcknowledgedByDriver = new DataSetDefinition
        {
            Name = "Message 158: Text Message Acknowledged by Driver",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE,
                NID_TEXTMESSAGE
            }
        };

        public static DataSetDefinition Message159SessionEstablished = new DataSetDefinition
        {
            Name = "Message 159: Session established",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                NID_ENGINE
            }
        };

        public static DataSetDefinition Message2SRAuthorisation = new DataSetDefinition
        {
            Name = "Message 2: SR Authorisation",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG,
                Q_SCALE,
                D_SR
            }
        };

        public static DataSetDefinition Message3MovementAuthority = new DataSetDefinition
        {
            Name = "Message 3: Movement Authority",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG
            }
        };

        public static DataSetDefinition Message6RecognitionOfExitFromTRIPMode = new DataSetDefinition
        {
            Name = "Message 6: Recognition of exit from TRIP mode",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG
            }
        };

        public static DataSetDefinition Message8AcknowledgementOfTrainData = new DataSetDefinition
        {
            Name = "Message 8: Acknowledgement of Train Data",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG,
                T_TRAIN
            }
        };

        public static DataSetDefinition Message9RequestToShortenMA = new DataSetDefinition
        {
            Name = "Message 9: Request to Shorten MA",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG
            }
        };

        public static DataSetDefinition Message15ConditionalEmergencyStop = new DataSetDefinition
        {
            Name = "Message 15: Conditional Emergency Stop",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG,
                NID_EM,
                Q_SCALE,
                D_REF,
                Q_DIR,
                D_EMERGENCYSTOP
            }
        };

        public static DataSetDefinition Message16UnconditionalEmergencyStop = new DataSetDefinition
        {
            Name = "Message 16: Unconditional Emergency Stop",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG,
                NID_EM
            }
        };

        public static DataSetDefinition Message18RevocationOfEmergencyStop = new DataSetDefinition
        {
            Name = "Message 18: Revocation of Emergency Stop",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG,
                NID_EM
            }
        };

        public static DataSetDefinition Message24GeneralMessage = new DataSetDefinition
        {
            Name = "Message 24: General message",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG
            }
        };

        public static DataSetDefinition Message27SHRefused = new DataSetDefinition
        {
            Name = "Message 27: SH Refused",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG,
                T_TRAIN
            }
        };

        public static DataSetDefinition Message28SHAuthorised = new DataSetDefinition
        {
            Name = "Message 28: SH Authorised",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG,
                T_TRAIN
            }
        };

        public static DataSetDefinition Message32RBCRIUSystemVersion = new DataSetDefinition
        {
            Name = "Message 32: RBC/RIU System Version",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG,
                M_VERSION
            }
        };

        public static DataSetDefinition Message33MAWithShiftedLocationReference = new DataSetDefinition
        {
            Name = "Message 33: MA with Shifted Location Reference",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG,
                Q_SCALE,
                D_REF
            }
        };

        public static DataSetDefinition Message34TrackAheadFreeRequest = new DataSetDefinition
        {
            Name = "Message 34: Track Ahead Free Request",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG,
                Q_SCALE,
                D_REF,
                Q_DIR,
                D_TAFDISPLAY,
                L_TAFDISPLAY
            }
        };

        public static DataSetDefinition Message37InfillMA = new DataSetDefinition
        {
            Name = "Message 37: Infill MA",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG
            }
        };

        public static DataSetDefinition Message38InitiationOfACommunicationSession = new DataSetDefinition
        {
            Name = "Message 38: Initiation of a communication session",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG
            }
        };

        public static DataSetDefinition Message39AcknowledgementOfTerminationOfACommunicationSession =
            new DataSetDefinition
            {
                Name = "Message 39: Acknowledgement of termination of a communication session",
                BitFields = new List<BitField>
                {
                    NID_MESSAGE,
                    L_MESSAGE,
                    T_TRAIN,
                    M_ACK,
                    NID_LRBG
                }
            };

        public static DataSetDefinition Message40TrainRejected = new DataSetDefinition
        {
            Name = "Message 40: Train Rejected",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG
            }
        };

        public static DataSetDefinition Message41TrainAccepted = new DataSetDefinition
        {
            Name = "Message 41: Train Accepted",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG
            }
        };

        public static DataSetDefinition Message43SoMPositionReportConfirmedByRBC = new DataSetDefinition
        {
            Name = "Message 43: SoM position report confirmed by RBCd",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG
            }
        };

        public static DataSetDefinition Message45AssignmentOfCoordinateSystem = new DataSetDefinition
        {
            Name = "Message 45: Assignment of coordinate system",
            BitFields = new List<BitField>
            {
                NID_MESSAGE,
                L_MESSAGE,
                T_TRAIN,
                M_ACK,
                NID_LRBG,
                Q_ORIENTATION
            }
        };

        #endregion
    }
}