using BitDataParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace datashark.DataSets
{
    public class CIP : DataSetCollection
    {
        public CIP() 
        {
            Name = "CIP";
            Description = "";

            DataSets.Add(TPWStoDMI);
        }

        public static DataSetDefinition TPWStoDMI = new DataSetDefinition
        {
            Name = "TPWS to DMI",
            BitFields = new List<BitField>
            {
                new BitField{Name = "PD_MASTER_ATP_GTW", BitFieldType = BitFieldType.UInt8, Length = 8},
                new BitField{Name = "ATP_GTW_IsAlive", BitFieldType = BitFieldType.UInt8, Length = 8},
                new BitField{Name = "ATP_INDICATOR_5", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "ATP_INDICATOR_6", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "ATP_BUTTON_1", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "ATP_BUTTON_2", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "ATP_BUTTON_3", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "ATP_BUTTON_4", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "ATP_BUTTON_5", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "Spare1", BitFieldType = BitFieldType.UInt8, Length = 3},
                new BitField{Name = "ATP_FLASHING_INDICATOR_1", BitFieldType = BitFieldType.UInt8, Length = 2},
                new BitField{Name = "ATP_FLASHING_INDICATOR_2", BitFieldType = BitFieldType.UInt8, Length = 2},
                new BitField{Name = "ATP_FLASHING_INDICATOR_3", BitFieldType = BitFieldType.UInt8, Length = 2},
                new BitField{Name = "ATP_FLASHING_INDICATOR_4", BitFieldType = BitFieldType.UInt8, Length = 2},
                new BitField{Name = "Spare2", BitFieldType = BitFieldType.UInt8, Length = 6},
                new BitField{Name = "ATP_TEXT_MESSAGE_ACK_ID", BitFieldType = BitFieldType.Int8, Length = 8},
                new BitField{Name = "ATP_TEXT_MESSAGE_ID_1", BitFieldType = BitFieldType.Int8, Length = 8},
                new BitField{Name = "ATP_TEXT_MESSAGE_ID_2", BitFieldType = BitFieldType.Int8, Length = 8},
                new BitField{Name = "ATP_TEXT_MESSAGE_ID_3", BitFieldType = BitFieldType.Int8, Length = 8},
                new BitField{Name = "ATP_TEXT_MESSAGE_ID_4", BitFieldType = BitFieldType.Int8, Length = 8},
                new BitField{Name = "Spare3", BitFieldType = BitFieldType.Spare, Length = 8+8+32+32},
            }
        };

        public static DataSetDefinition DMItoTPWS = new DataSetDefinition
        {
            Name = "DMI to TPWS",
            BitFields = new List<BitField>
            {
                new BitField{Name = "PD_Dx_Cn_MASTER_DMI", BitFieldType = BitFieldType.UInt8, Length = 8},
                new BitField{Name = "DMI_IsAlive", BitFieldType = BitFieldType.UInt8, Length = 8},
                new BitField{Name = "ATP_REPORT_BUTTON_1", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "ATP_REPORT_BUTTON_2", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "ATP_REPORT_BUTTON_3", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "ATP_REPORT_BUTTON_4", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "ATP_REPORT_BUTTON_5", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "DMI_ACK_REPORT", BitFieldType = BitFieldType.Bool, Length = 1},
            }
        };
    }
}
