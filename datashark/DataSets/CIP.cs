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

        public static DataSetDefinition GWtoDMI = new DataSetDefinition
        {
            Name = "GW to DMI",
            BitFields = new List<BitField>
            {
                new BitField{Name = "PD_MASTER_ATP_GTW", BitFieldType = BitFieldType.UInt8, Length = 8},
                new BitField{Name = "TRINT_IsAlive_EIP", BitFieldType = BitFieldType.UInt8, Length = 8},
                new BitField{Name = "TRINT_EVC_Isolation_NO", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "TRINT_EVC_Isolation_NC", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "Spare1", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "Spare2", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "TRINT_Current_Cab_EIP", BitFieldType = BitFieldType.UInt8, Length = 4, LookupTable = new LookupTable{ { "0","No Cab"},{ "1","Cab A"},{"2","Cab B" },{"3", "Both Cabs" } } },
                new BitField{Name = "TRINT_ATP_NID", BitFieldType = BitFieldType.Int8, Length = 8},
                new BitField{Name = "TRINT_Time_Offset", BitFieldType = BitFieldType.Int8, Length = 8},
                new BitField{Name = "Spare3", BitFieldType = BitFieldType.Spare, Length = 8},
                new BitField{Name = "TRINT_Set_Speed", BitFieldType = BitFieldType.UInt16, Length = 16},
                new BitField{Name = "Spare4", BitFieldType = BitFieldType.Spare, Length = 32},
                new BitField{Name = "TRINT_Time", BitFieldType = BitFieldType.UnixEpochUtc, Length = 48},
                new BitField{Name = "Spare5", BitFieldType = BitFieldType.Spare, Length = 1904}
                
            }
        };

        public static DataSetDefinition TPWStoGW = new DataSetDefinition
        {
            Name = "TPWS to GW",
            BitFields = new List<BitField>
            {
                new BitField{Name = "AWS_REC_FAULT", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "AWS_SELECT_FAULT", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "AWS_RESET_FAULT", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "AWS_RESET_POWER_FLT", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "AWS_CAUTION_ACK_FLT", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "AWS_SELFTEST_FAIL", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "Spare1", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "Spare2", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "TPWS_ANT_FAULT", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "TPWS_TONETEST_FAIL", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "TPWS_SELFTEST_FAIL", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "TEMP_ISO_SW_FAULT", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "Spare3", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "Spare4", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "Spare5", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "Spare6", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "BRAKE_MON_FAULT", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "12V_SUPPLY_FAULT", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "3_3V_SUPPLY_FAULT", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "INTERNAL_SW_FAILED", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "CLOCK_FAULT", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "INTERNAL_COMMS_FLT", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "Spare7", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "Spare8", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "DMI_FAILED", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "ETCS_COMMANDED_FA", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "AWS_TPWS_NOT_OP", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "INVALID_AWS_TPWS_OP", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "MISSING_FUNCTION", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "CAB_STATUS_INVALID", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "INVALID_STATE_TRANS", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "VERSION_ERROR", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "NO_SELFTEST_PASSED", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "SAP_DISCONNECT_BIU", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "SAP_DISCONNECT_DMI", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "Speech Unit FLT", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "Spare9", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "Spare10", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "Spare11", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "Spare12", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "SPAD_BRAKE_MESS", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "OSS_BRAKE_MESS", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "AWS_BRAKE_MESS", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "TEMP_ISO_MESS", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "TPWS_FAULT MESS", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "AWS_ISOLATION MESS", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "AWS_FAULT MESS", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "Confirm activation of TSO button MESS", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "Brake Release MESS", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "Train Stop Overide Active MESS", BitFieldType = BitFieldType.Bool, Length = 1},

            }
        }

        public static List<BitField> GWtoEVCdataset => new List<BitField>
            {
                new BitField{Name = "TCMS_LifeSign", BitFieldType = BitFieldType.UInt8, Length = 8},
                new BitField{Name = "Service_Brake_active_Vehicle", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "TR_OBU_Traction_Status", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "SpareC1", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "SpareC2", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "SpareC3", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "SpareC4", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "SpareC5", BitFieldType = BitFieldType.Spare, Length = 1},
                new BitField{Name = "SET_SPEED_DISPLAY_INFO", BitFieldType = BitFieldType.Bool, Length = 1},
                new BitField{Name = "SpareC6", BitFieldType = BitFieldType.Spare, Length = 8},
                new BitField{Name = "TR_OBU_TrainType", BitFieldType = BitFieldType.UInt8, Length = 8},
                new BitField{Name = "SET_TARGET_SPEED", BitFieldType = BitFieldType.Int32, Length = 32},
                new BitField{Name = "Driver_id_begin", BitFieldType = BitFieldType.UInt64, Length = 64},
                new BitField{Name = "Driver_id_end", BitFieldType = BitFieldType.UInt64, Length = 64},
                new BitField{Name = "SpareC7", BitFieldType = BitFieldType.Spare, Length = 32},
                new BitField{Name = "SpareC8", BitFieldType = BitFieldType.Spare, Length = 32},

            };

        public static DataSetDefinition GWtoEVC1 = new DataSetDefinition
        {
            Name = "GW->CPUA",
            Identifiers = new Identifiers { Numeric = new List<int> { 111 }, Source = new IPv4(192, 168, 1, 15), Destination = new IPv4(192, 168, 1, 21) },
            BitFields = GWtoEVCdataset
        };

        public static DataSetDefinition GWtoEVC2 = new DataSetDefinition
        {
            Name = "GW->CPUB",
            Identifiers = new Identifiers { Numeric = new List<int> { 124 }, Source = new IPv4(192, 168, 1, 15), Destination = new IPv4(192, 168, 1, 23) },
            BitFields = GWtoEVCdataset
        };


    }
}
