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

            DataSets.Add(DIA_1);
            DataSets.Add(DIA_130);
            DataSets.Add(DIA_131);
            DataSets.Add(DIA_152);
            DataSets.Add(DIA_158);

            DataSets.Add(DIA_201);
            DataSets.Add(DIA_202);
            DataSets.Add(DIA_203);
            DataSets.Add(DIA_204);
            DataSets.Add(DIA_205);
            DataSets.Add(DIA_206);
            DataSets.Add(DIA_207);
            DataSets.Add(DIA_209);
            DataSets.Add(DIA_212);
            DataSets.Add(DIA_214);
        }
        
        // checked 20191125 1.6 DIAG manual CD
        public static DataSetDefinition DIA_1 => new DataSetDefinition
        {
            Name = "DIA_1 JRU_Status",
            Comment = "Dataset definition of JRU status - event part",
            Identifiers = new List<string>
            {
                "230510450"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ijru_0",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "--not used-- (lifesign, as Environment part of DIA_1)"
                },
                new BitField
                {
                    Name = "ijru_8",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "iJRU_FatalError (-)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Fatal error, 1 = no data recording is possible",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "iJRU_Warning (-)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Warning, 1 = some data possibly not recorded",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "iJRU_BatteryChange (-)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Battery change necessary = 1",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "iJRU_STblocked (-)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Short term memory blocked = 1 (data not used)",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ijru_014",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "iJRU_FatError_Int (-)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Fatal error internal = 1",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "iJRU_FatError_Ext (-)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Fatal error external = 1",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "iJRU_MemoryDownload (-)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Memory download active = 1",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "iJRU_24h_block (-)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "JRU 24h blocked = 1",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ijru_019",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "iJRU_PBComError (-)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Profibus communication error = 1 (only in projects with PB)",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "iJRU_Vsens1_defect (-)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Pulse generator sensor 1 defect = 1",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "iJRU_Vsens2_defect (-)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Pulse generator sensor 2 defect = 1",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "iJRU_Mem_FillLevel_80 (-)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "JRU memory is above 80% filled = 1",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "iJRU_Mem_FillLevel_100 (-)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "JRU memory is above 100% filled = 1",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ijru_01F",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "ijru_020",
                    BitFieldType = BitFieldType.Spare,
                    Length = 224,
                    Comment = "--not used-- (environment part of DIA_1)"
                },
            }
        };

        // checked 20191125 1.6 DIAG manual CD
        public static DataSetDefinition DIA_130 => new DataSetDefinition
        {
            Name = "DIA_130 ETC_Events",
            Comment = "Dataset definition of ETC Events",
            Identifiers = new List<string>
            {
                "230510020",
                "230511020"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ETC_0200 (IE-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Internal SW failure (see environment data) - Code 8200  00",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0201 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC software failure, cumulative for all SW failures with system reaction - Code 8201  01",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0202 (OS-P)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS Level changed report to the diagnosis. - Code 8202  02",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0203 (OS-P)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "STM in DA state change report to the diagnosis. - Code 8203  03",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0204",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8204  04"
                },
                new BitField
                {
                    Name = "ETC_0205",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8205  05"
                },
                new BitField
                {
                    Name = "ETC_0206 (OS-P)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA Reason for TRIP detected - Code 8206  06",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0207 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA lost/no connection with SDP - Code 8207  07",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0208 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA lost/no connection with BTM 1 - Code 8208  08",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0209 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA lost/no connection with BTM 2 - Code 8209  09",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_020A (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA lost/no connection with RCF - Code 820A  0A",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_020B (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA lost/no connection with VAP - Code 820B  0B",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_020C (OS-P)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA Rollaway Protection activated - Code 820C  0C",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_020D (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA lost/no connection with JRU - Code 820D  0D",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_020E (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA lost/no connection with STM 1 - Code 820E  0E",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_020F (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA lost/no connection with STM 2 - Code 820F  0F",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0210 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA lost/no connection with STM 3 - Code 8210  10",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0211 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA lost/no connection with STM 4 - Code 8211  11",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0212 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA lost/no connection with STM 5 - Code 8212  12",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0213 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA lost/no connection with STM 6 - Code 8213  13",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0214 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA lost/no connection with STM 7 - Code 8214  14",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0215 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA lost/no connection with STM 8 - Code 8215  15",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0216 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected input data from SDP - Code 8216  16",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0217 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected input data from BTM 1 - Code 8217  17",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0218 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected input data from BTM 2 - Code 8218  18",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0219 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected feedback from BTM 1 - Code 8219  19",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_021A (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected feedback from BTM 2 - Code 821A  1A",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_021B (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected input data from RCF - Code 821B  1B",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_021C",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 821C  1C"
                },
                new BitField
                {
                    Name = "ETC_021D (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected input from TI-Client - Code 821D  1D",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_021E (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected input data from STM 1 - Code 821E  1E",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_021F (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected input data from STM 2 - Code 821F  1F",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0220 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected input data from STM 3 - Code 8220  20",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0221 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected input data from STM 4 - Code 8221  21",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0222 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected input data from STM 5 - Code 8222  22",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0223 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected input data from STM 6 - Code 8223  23",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0224 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected input data from STM 7 - Code 8224  24",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0225 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected input data from STM 8 - Code 8225  25",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0226 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected input data from MMI 1 - Code 8226  26",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0227 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: unexpected input data from MMI 2 - Code 8227  27",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0228 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA configuration data failure - Code 8228  28",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0229 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: BTM 1 or CAU 1 (unknown) failure - Code 8229  29",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_022A (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "CAU 1 failure - Code 822A  2A",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_022B (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: BTM 1 hardware failure - Code 822B  2B",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_022C (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: BTM 1 firmware failure - Code 822C  2C",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_022D (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: BTM 1 software failure - Code 822D  2D",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_022E (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: BTM 1 EVC input data failure - Code 822E  2E",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_022F (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: BTM 1 SDP input data failure - Code 822F  2F",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0230 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: BTM 2 or CAU 2 (unknown) failure - Code 8230  30",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0231 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: CAU 2 failure - Code 8231  31",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0232 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: BTM 2 hardware failure - Code 8232  32",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0233 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: BTM 2 firmware failure - Code 8233  33",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0234 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: BTM 2 software failure - Code 8234  34",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0235 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: BTM 2 EVC input data failure - Code 8235  35",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0236 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: BTM 2 SDP input data failure - Code 8236  36",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0237 (OS-P)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA Identification of the brake reason (sw_error)  - Code 8237  37",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0238 (SR-P)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA wrong TCO feedback (BI) - Code 8238  38",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0239 (SR-P)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS BA Odometer failure (Tacho1/2, Doppler) - Code 8239  39",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_023A (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "ETCS Internal SW-Event - Code 823A  3A",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_023B",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 823B  3B"
                },
                new BitField
                {
                    Name = "ETC_023C",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 823C  3C"
                },
                new BitField
                {
                    Name = "ETC_023D (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Lost communication between COM Handler A and RCF plug-in. - Code 823D  3D",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_023E (SR-P)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Modem 1 did not give the expected response to a command  - Code 823E  3E",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_023F (SR-P)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Modem 2 did not give the expected response to a command  - Code 823F  3F",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0240 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC (RCF) internal failure - Code 8240  40",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0241 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI 1 software execution failure - Code 8241  41",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0242 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI 1 unexpected input data from ETCS BA - Code 8242  42",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0243 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI 1 unexpected input data from VAP - Code 8243  43",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0244 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI 1 unexpected input data from STM - Code 8244  44",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0245 (OS-P)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: BTM reports an M_VERSION out of specification - Code 8245  45",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0246",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8246  46"
                },
                new BitField
                {
                    Name = "ETC_0247",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8247  47"
                },
                new BitField
                {
                    Name = "ETC_0248",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8248  48"
                },
                new BitField
                {
                    Name = "ETC_0249",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8249  49"
                },
                new BitField
                {
                    Name = "ETC_024A",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 824A  4A"
                },
                new BitField
                {
                    Name = "ETC_024B",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 824B  4B"
                },
                new BitField
                {
                    Name = "ETC_024C (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI1: hardware failure 1 - Code 824C  4C",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_024D (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI1: hardware failure 2 - Code 824D  4D",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_024E (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI1: hardware failure 3 - Code 824E  4E",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_024F (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI1: hardware failure 4 - Code 824F  4F",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0250 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI1: hardware failure 5 - Code 8250  50",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0251 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI1: key input failure - Code 8251  51",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0252 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI1: lost communication with ETC - Code 8252  52",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0253 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI2: ETC Interface Failure - Code 8253  53",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0254 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI2: VAP Interface Failure - Code 8254  54",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0255 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI2: STM Interface Failure - Code 8255  55",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0256",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8256  56"
                },
                new BitField
                {
                    Name = "ETC_0257",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8257  57"
                },
                new BitField
                {
                    Name = "ETC_0258",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8258  58"
                },
                new BitField
                {
                    Name = "ETC_0259",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8259  59"
                },
                new BitField
                {
                    Name = "ETC_025A",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 825A  5A"
                },
                new BitField
                {
                    Name = "ETC_025B",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 825B  5B"
                },
                new BitField
                {
                    Name = "ETC_025C",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 825C  5C"
                },
                new BitField
                {
                    Name = "ETC_025D (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI2: software failure - Code 825D  5D",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_025E (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI2: hardware failure 1 - Code 825E  5E",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_025F (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI2: hardware failure 2 - Code 825F  5F",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0260 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI2: hardware failure 3 - Code 8260  60",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0261 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI2: hardware failure 4 - Code 8261  61",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0262 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI2: hardware failure 5 - Code 8262  62",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0263 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI2: key input failure - Code 8263  63",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0264 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "MMI2: lost communication with ETC - Code 8264  64",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0265",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8265  65"
                },
                new BitField
                {
                    Name = "ETC_0266 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Waiting for reply to Cold Movement Detection Distance Telegram (no distance will be reported) - Code 8266  66",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0267 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Cold Movement Detection not configured or parameters are not valid - Code 8267  67",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0268 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Cold Movement Detection not available - Code 8268  68",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0269",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8269  69"
                },
                new BitField
                {
                    Name = "ETC_026A",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 826A  6A"
                },
                new BitField
                {
                    Name = "ETC_026B",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 826B  6B"
                },
                new BitField
                {
                    Name = "ETC_026C",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 826C  6C"
                },
                new BitField
                {
                    Name = "ETC_026D",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 826D  6D"
                },
                new BitField
                {
                    Name = "ETC_026E",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 826E  6E"
                },
                new BitField
                {
                    Name = "ETC_026F",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 826F  6F"
                },
                new BitField
                {
                    Name = "ETC_0270",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "--not used-- (8 bits) - Code 8270  70"
                },
                new BitField
                {
                    Name = "ETC_0278",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "--not used-- (8 bits) - Code 8278  78"
                },
                new BitField
                {
                    Name = "ETC_0280",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "--not used-- (8 bits) - Code 8280  80"
                },
                new BitField
                {
                    Name = "ETC_0288",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "--not used-- (8 bits) - Code 8288  88"
                },
                new BitField
                {
                    Name = "ETC_0290 (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Missing vehicle life-sign - Code 8290  90",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0291 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: TCO unexpected feedback - Code 8291  91",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0292 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Sleeping input Anti-valence failure - Code 8292  92",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_0293",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8293  93"
                },
                new BitField
                {
                    Name = "ETC_0294",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8294  94"
                },
                new BitField
                {
                    Name = "ETC_0295",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8295  95"
                },
                new BitField
                {
                    Name = "ETC_0296",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8296  96"
                },
                new BitField
                {
                    Name = "ETC_0297",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8297  97"
                },
                new BitField
                {
                    Name = "ETC_0298",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "--not used-- (8 bits) - Code 8298  98"
                },
                new BitField
                {
                    Name = "ETC_02A0",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "--not used-- (8 bits) - Code 82A0  A0"
                },
                new BitField
                {
                    Name = "ETC_02A8",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82A8  A8"
                },
                new BitField
                {
                    Name = "ETC_02A9",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82A9  A9"
                },
                new BitField
                {
                    Name = "ETC_02AA",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82AA  AA"
                },
                new BitField
                {
                    Name = "ETC_02AB (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: VDX 1 IN1 failure - Code 82AB  AB",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02AC",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82AC  AC"
                },
                new BitField
                {
                    Name = "ETC_02AD (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: VDX 1 IN2 failure - Code 82AD  AD",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02AE (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: VDX 2 IN2 failure - Code 82AE  AE",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02AF (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: VDX 1 IN3 failure - Code 82AF  AF",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02B0 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: VDX 2 IN3 failure - Code 82B0  B0",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02B1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82B1  B1"
                },
                new BitField
                {
                    Name = "ETC_02B2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82B2  B2"
                },
                new BitField
                {
                    Name = "ETC_02B3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82B3  B3"
                },
                new BitField
                {
                    Name = "ETC_02B4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82B4  B4"
                },
                new BitField
                {
                    Name = "ETC_02B5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82B5  B5"
                },
                new BitField
                {
                    Name = "ETC_02B6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82B6  B6"
                },
                new BitField
                {
                    Name = "ETC_02B7",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82B7  B7"
                },
                new BitField
                {
                    Name = "ETC_02B8",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82B8  B8"
                },
                new BitField
                {
                    Name = "ETC_02B9 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: RB relay state opposite to order - Code 82B9  B9",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02BA (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: EB relay 1 state opposite to order - Code 82BA  BA",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02BB (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: EB relay 2 state opposite to order - Code 82BB  BB",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02BC (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Bypass relay state opposite to order - Code 82BC  BC",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02BD",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82BD  BD"
                },
                new BitField
                {
                    Name = "ETC_02BE",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82BE  BE"
                },
                new BitField
                {
                    Name = "ETC_02BF (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: SB Feedback - opposite to order - Code 82BF  BF",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02C0 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: EB Feedback - opposite to order - Code 82C0  C0",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02C1 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: TCO Feedback - opposite to order - Code 82C1  C1",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02C2 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Brake Test external EB feedback failure - Code 82C2  C2",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02C3 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Brake Test external redundant Brake feedback failure - Code 82C3  C3",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02C4 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Brake Test failure (SB Feedback) - Code 82C4  C4",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02C5 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Brake Test brake relay-VDX feedback failed - Code 82C5  C5",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02C6 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Brake Test failure (Bypass) - Code 82C6  C6",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02C7 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Brake Test failure (TCO Feedback) - Code 82C7  C7",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02C8",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "--not used-- (8 bits) - Code 82C8  C8"
                },
                new BitField
                {
                    Name = "ETC_02D0",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "--not used-- (8 bits) - Code 82D0  D0"
                },
                new BitField
                {
                    Name = "ETC_02D8",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82D8  D8"
                },
                new BitField
                {
                    Name = "ETC_02D9",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82D9  D9"
                },
                new BitField
                {
                    Name = "ETC_02DA",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82DA  DA"
                },
                new BitField
                {
                    Name = "ETC_02DB",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82DB  DB"
                },
                new BitField
                {
                    Name = "ETC_02DC",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82DC  DC"
                },
                new BitField
                {
                    Name = "ETC_02DD",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82DD  DD",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02DE (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Vital Customization VDX 3 FS failure - Code 82DE  DE",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02DF (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Vital Customization VDX 3 HR1 failure - Code 82DF  DF",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02E0 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Vital Customization VDX 3 HR2 failure - Code 82E0  E0",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02E1 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Vital Customization VDX 3 MVB port failure - Code 82E1  E1",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02E2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82E2  E2"
                },
                new BitField
                {
                    Name = "ETC_02E3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82E3  E3"
                },
                new BitField
                {
                    Name = "ETC_02E4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82E4  E4"
                },
                new BitField
                {
                    Name = "ETC_02E5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82E5  E5"
                },
                new BitField
                {
                    Name = "ETC_02E6 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Vital Customization VDX 4 FS failure - Code 82E6  E6",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02E7 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Vital Customization VDX 4 HR1 failure - Code 82E7  E7",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02E8 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Vital Customization VDX 4 HR2 failure - Code 82E8  E8",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02E9 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Vital Customization VDX 4 MVB port failure - Code 82E9  E9",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02EA",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82EA  EA"
                },
                new BitField
                {
                    Name = "ETC_02EB",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82EB  EB"
                },
                new BitField
                {
                    Name = "ETC_02EC",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82EC  EC"
                },
                new BitField
                {
                    Name = "ETC_02ED",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82ED  ED"
                },
                new BitField
                {
                    Name = "ETC_02EE",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82EE  EE"
                },
                new BitField
                {
                    Name = "ETC_02EF",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82EF  EF"
                },
                new BitField
                {
                    Name = "ETC_02F0 (SR-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: No odometer service - Code 82F0  F0",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02F1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82F1  F1"
                },
                new BitField
                {
                    Name = "ETC_02F2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82F2  F2"
                },
                new BitField
                {
                    Name = "ETC_02F3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82F3  F3"
                },
                new BitField
                {
                    Name = "ETC_02F4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82F4  F4"
                },
                new BitField
                {
                    Name = "ETC_02F5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82F5  F5"
                },
                new BitField
                {
                    Name = "ETC_02F6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82F6  F6"
                },
                new BitField
                {
                    Name = "ETC_02F7",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82F7  F7"
                },
                new BitField
                {
                    Name = "ETC_02F8",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82F8  F8"
                },
                new BitField
                {
                    Name = "ETC_02F9",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 82F9  F9"
                },
                new BitField
                {
                    Name = "ETC_02FA (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Vehicle is ready for supervision - Code 82FA  FA",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02FB (IE-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Vehicle is NOT ready for supervision - Code 82FB  FB",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02FC (OS-P)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Brake due to linking reaction - Code 82FC  FC",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02FD (OS-P)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: Received MA is not sufficient for FS/OS - Code 82FD  FD",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02FE (OS-P)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: RBC Key not valid - Code 82FE  FE",
                    SkipIfValue = false
                },
                new BitField
                {
                    Name = "ETC_02FF (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "EVC: lost/no connection with VAP - Code 82FF  FF",
                    SkipIfValue = false
                },
            }
        };

        // checked 20191205 1.6 DIAG manual CD
        public static DataSetDefinition DIA_131 => new DataSetDefinition
        {
            Name = "DIA_131 ETC_Environment",
            Comment = "Dataset definition of ETC Environment" +
                      "\r\nNote: Module specific application area starts at bit position 128 (byte 16). Data that are not used are set to 0",
            Identifiers = new List<string>
            {
                "230510030",
                "230511030"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "ETC_sw_error",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "SW failure (a value greater than zero means this is an internal SW failure)"
                },
                new BitField
                {
                    Name = "ETC_sw_failure_class",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "SW failure class"
                },
                new BitField
                {
                    Name = "ETC_sw_error_level",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "SW error level"
                },
                new BitField
                {
                    Name = "ETC_currend_speed",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Estimated train speed in km/h at the actual time"
                },
                new BitField
                {
                    Name = "ETC_last_nid_c",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Identification of the last national area(s)." +
                              "\r\nLast national area(s) to which the set applies (see Subset-26)."
                },
                new BitField
                {
                    Name = "ETC_last_nid_bg",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Identity of the last Balise group"
                },
                new BitField
                {
                    Name = "ETC_distance_lrbg",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "Distance of the last relevant Balise group."
                },
                new BitField
                {
                    Name = "ETC_etcs_level",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "The current ETCS level (0-4, 255, 5-254 not used)",
                    LookupTable = new Dictionary<string, string>
                    {
                        {"0", "Level 0"},
                        {"1", "Level 1"},
                        {"2", "Level 2"},
                        {"3", "Level 3"},
                        {"20", "TPWS"},
                        {"50", "CBTC"}
                    }
                },
                new BitField
                {
                    Name = "ETC_etcs_mode",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "The current ETCS mode (0-15, 16-255 = not used)",
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
                        {"15", "Passive Shunting"}
                    }
                },
                new BitField
                {
                    Name = "ETC_app_byte1",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "ETC_sw_error specific information area begin\r\n" +
                              "Note: ETC_sw_error specific environment data (depending" +
                              "on the error code this area may have different content meaning)",
                    SkipIfValue = 0
                },
                new BitField
                {
                    Name = "ETC_app_byte2",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "ETC_sw_error specific information",
                    SkipIfValue = 0
                },
                new BitField
                {
                    Name = "ETC_app_byte3",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "ETC_sw_error specific information",
                    SkipIfValue = 0
                },
                new BitField
                {
                    Name = "ETC_app_byte4",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "ETC_sw_error specific information",
                    SkipIfValue = 0
                },
                new BitField
                {
                    Name = "ETC_app_byte5",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "ETC_sw_error specific information",
                    SkipIfValue = 0
                },
                new BitField
                {
                    Name = "ETC_app_byte6",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "ETC_sw_error specific information",
                    SkipIfValue = 0
                },
                new BitField
                {
                    Name = "ETC_app_byte7",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "ETC_sw_error specific information",
                    SkipIfValue = 0
                },
                new BitField
                {
                    Name = "ETC_app_byte8",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "ETC_sw_error specific information",
                    SkipIfValue = 0
                },
                new BitField
                {
                    Name = "ETC_app_byte9",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "ETC_sw_error specific information",
                    SkipIfValue = 0
                },
                new BitField
                {
                    Name = "ETC_app_byte10",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "ETC_sw_error specific information",
                    SkipIfValue = 0
                },
                new BitField
                {
                    Name = "ETC_app_byte11",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "ETC_sw_error specific information",
                    SkipIfValue = 0
                },
                new BitField
                {
                    Name = "ETC_app_byte12",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "ETC_sw_error specific information",
                    SkipIfValue = 0
                },
                new BitField
                {
                    Name = "ETC_app_byte13",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "ETC_sw_error specific information",
                    SkipIfValue = 0
                },
                new BitField
                {
                    Name = "ETC_app_byte14",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "ETC_sw_error specific information",
                    SkipIfValue = 0
                },
                new BitField
                {
                    Name = "ETC_app_byte15",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "ETC_sw_error specific information",
                    SkipIfValue = 0
                },
                new BitField
                {
                    Name = "ETC_app_byte16",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "ETC_sw_error specific information",
                    SkipIfValue = 0
                },
            }
        };

        // checked 20191127 1.6 DIAG manual CD
        public static DataSetDefinition DIA_152 => new DataSetDefinition
        {
            Name = "DIA_152 DMI1_Events",
            Comment = "Dataset definition of DMI1 Events",
            Identifiers = new List<string>
            {
                "230510230",
                "230511230"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "DMI1_CL_ERR (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Configuration load error - Code 8380  00"
                },
                new BitField
                {
                    Name = "DMI1_RL_ERR (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Resource load error - Code 8381  01"
                },
                new BitField
                {
                    Name = "DMI1_HW_ERR (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "HW error - Code 8382  02"
                },
                new BitField
                {
                    Name = "DMI1_SW_ERR (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "SW failure - Code 8383  03"
                },
                new BitField
                {
                    Name = "DMI1_DIS_ERR (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Display failure - Code 8384  04"
                },
                new BitField
                {
                    Name = "DMI1_CDIS_ERR (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Critical display failure - Code 8385  05"
                },
                new BitField
                {
                    Name = "DMI1_0386",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8386  06"
                },
                new BitField
                {
                    Name = "DMI1_0387",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8387  07"
                },
                new BitField
                {
                    Name = "DMI1_0388",
                    BitFieldType = BitFieldType.Spare,
                    Length = 8,
                    Comment = "--not used-- (8 bit) - Code 8388  08"
                },
                new BitField
                {
                    Name = "DMI1_0390",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8390  10"
                },
                new BitField
                {
                    Name = "DMI1_0391",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8391  11"
                },
                new BitField
                {
                    Name = "DMI1_0392",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8392  12"
                },
                new BitField
                {
                    Name = "DMI1_0393",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 8393  13"
                },
                new BitField
                {
                    Name = "DMI1_Rx_ETC_ERR (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Rx ETC packet error - Code 8394  14"
                },
                new BitField
                {
                    Name = "DMI1_Rx_STM_ERR (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Rx STM packet error - Code 8395  15"
                },
                new BitField
                {
                    Name = "DMI1_Tx_ETC_ERR (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tx ETC packet error - Code 8396  16"
                },
                new BitField
                {
                    Name = "DMI1_Tx_STM_ERR (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tx STM packet error - Code 8397  17"
                },
                new BitField
                {
                    Name = "DMI1_Rx_ETC_UNK_ERR (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Rx unknown value from ETC - Code 8398  18"
                },
                new BitField
                {
                    Name = "DMI1_Rx_STM_UNK_ERR (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Rx unknown value from STM - Code 8399  19"
                },
                new BitField
                {
                    Name = "DMI1_ITER_EXCEED (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Iterator exceeded - Code 839A  1A"
                },
                new BitField
                {
                    Name = "DMI1_039B",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 839B  1B"
                },
                new BitField
                {
                    Name = "DMI1_039C",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 839C  1C"
                },
                new BitField
                {
                    Name = "DMI1_DRV_TIME (OS-P)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Driver changed time - Code 839D  1D"
                },
                new BitField
                {
                    Name = "DMI1_STATISTICS (OS-P)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Statistics - Code 839E  1E"
                },
                new BitField
                {
                    Name = "DMI1_ATP_DOWN (OS-T)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "ATP-DOWN - Code 839F  1F"
                },
            }
        };

        // checked 20191126 1.6 DIAG manual CD
        public static DataSetDefinition DIA_158 => new DataSetDefinition
        {
            Name = "DIA_158 COD_Events",
            Comment = "Dataset definition of COD Events",
            Identifiers = new List<string>
            {
                "230510290",
                "230511290"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "COD_EVT0 (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "No service - Code 8480  00"
                },
                new BitField
                {
                    Name = "COD_EVT1 (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Internal SDP failure - Code 8481  01"
                },
                new BitField
                {
                    Name = "COD_EVT2 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "OSU agent failure - Code 8482  02"
                },
                new BitField
                {
                    Name = "COD_EVT3 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "OSU unit failure - Code 8483  03"
                },
                new BitField
                {
                    Name = "COD_EVT4 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "OSU unit failure local - Code 8484  04"
                },
                new BitField
                {
                    Name = "COD_EVT5 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "OSU unit failure remote - Code 8485  05"
                },
                new BitField
                {
                    Name = "COD_EVT6 (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Another odometer failure - Code 8486  06"
                },
                new BitField
                {
                    Name = "COD_EVT7 (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Faulty configuration data received - Code 8487  07"
                },
                new BitField
                {
                    Name = "COD_EVT8 (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Degraded mode - Code 8488  08"
                },
                new BitField
                {
                    Name = "COD_EVT9 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "SPL (GISU/CMD) failure - Code 8489  09"
                },
                new BitField
                {
                    Name = "COD_EVT10 (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "GISU communication failure - Code 848A  0A"
                },
                new BitField
                {
                    Name = "COD_EVT11 (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "GISU configuration failure - Code 848B  0B"
                },
                new BitField
                {
                    Name = "COD_EVT12 (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "CMD communication failure - Code 848C  0C"
                },
                new BitField
                {
                    Name = "COD_EVT13 (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "CMD connection down temporarily - Code 848D  0D"
                },
                new BitField
                {
                    Name = "COD_EVT14 (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "GISU failure - Code 848E  0E"
                },
                new BitField
                {
                    Name = "COD_15",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 848F  0F"
                },
                new BitField
                {
                    Name = "COD_EVT16 (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 1 invalid - OSU A/B data mismatch - Code 8490  10"
                },
                new BitField
                {
                    Name = "COD_EVT17 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 1 invalid - OSU A/B status mismatch - Code 8491  11"
                },
                new BitField
                {
                    Name = "COD_EVT18 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 1 invalid - OSU current error - Code 8492  12"
                },
                new BitField
                {
                    Name = "COD_EVT19 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 1 invalid - OSU voltage error - Code 8493  13"
                },
                new BitField
                {
                    Name = "COD_EVT20 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 1 invalid - Error counter fault - Code 8494  14"
                },
                new BitField
                {
                    Name = "COD_EVT21 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 1 invalid - Lag error counter fault - Code 8495  15"
                },
                new BitField
                {
                    Name = "COD_EVT22 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 1 invalid - Phase speed difference - Code 8496  16"
                },
                new BitField
                {
                    Name = "COD_EVT23 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 1 invalid - High wheel speed - Code 8497  17"
                },
                new BitField
                {
                    Name = "COD_EVT24 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 1 invalid - No pulses detected - Code 8498  18"
                },
                new BitField
                {
                    Name = "COD_EVT25 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 1 invalid - Permanent No pulses detected - Code 8499  19"
                },
                new BitField
                {
                    Name = "COD_EVT26 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 1 invalid - Wheel speed difference - Code 849A  1A"
                },
                new BitField
                {
                    Name = "COD_EVT27 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 1 invalid - Permanent Wheel Speed difference - Code 849B  1B"
                },
                new BitField
                {
                    Name = "COD_EVT28 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 1 invalid - Direction opposite to train direction - Code 849C  1C"
                },
                new BitField
                {
                    Name = "COD_29",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 849D  1D"
                },
                new BitField
                {
                    Name = "COD_30",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 849E  1E"
                },
                new BitField
                {
                    Name = "COD_31",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 849F  1F"
                },
                new BitField
                {
                    Name = "COD_EVT32 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 2 invalid - OSU A/B data mismatch - Code 84A0  20"
                },
                new BitField
                {
                    Name = "COD_EVT33 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 2 invalid - OSU A/B status mismatch - Code 84A1  21"
                },
                new BitField
                {
                    Name = "COD_EVT34 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 2 invalid - OSU current error - Code 84A2  22"
                },
                new BitField
                {
                    Name = "COD_EVT35 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 2 invalid - OSU voltage error - Code 84A3  23"
                },
                new BitField
                {
                    Name = "COD_EVT36 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 2 invalid - Error counter fault - Code 84A4  24"
                },
                new BitField
                {
                    Name = "COD_EVT37 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 2 invalid - Lag error counter fault - Code 84A5  25"
                },
                new BitField
                {
                    Name = "COD_EVT38 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 2 invalid - Phase speed difference - Code 84A6  26"
                },
                new BitField
                {
                    Name = "COD_EVT39 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 2 invalid - High wheel speed - Code 84A7  27"
                },
                new BitField
                {
                    Name = "COD_EVT40 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 2 invalid - No pulses detected - Code 84A8  28"
                },
                new BitField
                {
                    Name = "COD_EVT41 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 2 invalid - Permanent No pulses detected - Code 84A9  29"
                },
                new BitField
                {
                    Name = "COD_EVT42 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 2 invalid - Wheel speed difference - Code 84AA  2A"
                },
                new BitField
                {
                    Name = "COD_EVT43 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 2 invalid - Permanent Wheel Speed Difference - Code 84AB  2B"
                },
                new BitField
                {
                    Name = "COD_EVT44 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Tachometer 2 invalid - Direction opposite to train direction - Code 84AC  2C"
                },
                new BitField
                {
                    Name = "COD_45",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 84AD  2D"
                },
                new BitField
                {
                    Name = "COD_46",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 84AE  2E"
                },
                new BitField
                {
                    Name = "COD_47",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 84AF  2F"
                },
                new BitField
                {
                    Name = "COD_EVT48 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Doppler radar invalid - OSU A/B data mismatch - Code 84B0  30"
                },
                new BitField
                {
                    Name = "COD_EVT49 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Doppler radar invalid - OSU A/B status mismatch - Code 84B1  31"
                },
                new BitField
                {
                    Name = "COD_EVT50 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Doppler radar invalid - OSU current error - Code 84B2  32"
                },
                new BitField
                {
                    Name = "COD_EVT51 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Doppler radar invalid - OSU voltage error - Code 84B3  33"
                },
                new BitField
                {
                    Name = "COD_EVT52 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Doppler radar invalid - Error counter fault - Code 84B4  34"
                },
                new BitField
                {
                    Name = "COD_EVT53 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Doppler radar invalid - No pulses detected - Code 84B5  35"
                },
                new BitField
                {
                    Name = "COD_EVT54 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Doppler radar invalid - Speed is stuck - Code 84B6  36"
                },
                new BitField
                {
                    Name = "COD_EVT55 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Doppler radar invalid - High acceleration or high noise - Code 84B7  37"
                },
                new BitField
                {
                    Name = "COD_EVT56 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Doppler radar invalid - Doppler Tacho Speed difference - Code 84B8  38"
                },
                new BitField
                {
                    Name = "COD_EVT57 (SR-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Doppler radar invalid - High speed - Code 84B9  39"
                },
                new BitField
                {
                    Name = "COD_EVT58 (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "Doppler radar needs maintenance - Code 84BA  3A"
                },
                new BitField
                {
                    Name = "COD_59",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 84BB  3B"
                },
                new BitField
                {
                    Name = "COD_60",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 84BC  3C"
                },
                new BitField
                {
                    Name = "COD_61",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 84BD  3D"
                },
                new BitField
                {
                    Name = "COD_62",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used-- - Code 84BE  3E"
                },
                new BitField
                {
                    Name = "COD_EVT63 (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    SkipIfValue = false,
                    Comment = "GISU invalid - Code 84BF  3F"
                }
            }
        };

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

        // checked 20190913 1.6 DIAG manual RVV
        public static DataSetDefinition DIA_204 => new DataSetDefinition
        {
            Name = "DIA_204 GLOBAL_ENVIRONMENT_DATA_5",
            Comment = "Dataset definition of part 5 of global environment data.",
            Identifiers = new List<string>
            {
                "230510350",
                "230511350"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "STANDSTILL_POS",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Stand still position."
                },
                new BitField
                {
                    Name = "LAST_BG_POS",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last Balise group position (indicates which of stored 8 Balises the last was)."
                },
                new BitField
                {
                    Name = "D_NVSTFF",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Maximum distance for running in Staff Responsible mode. Allowed values according to [SS026]. " +
                    "D_NVSTFF considering Q_SCALE. Due to the fact that this parameter is stored in CM, but the [SS026] parameter depends on Q_SCALE, the special"
                },
                new BitField
                {
                    Name = "NID_C",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "NID_C."
                },
                new BitField
                {
                    Name = "NID_C_MAX_ITER",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Number of iterations of NID_C."
                },
                new BitField
                {
                    Name = "VALID_NV",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Valid National Values. If this parameter is set to 0, the ETCS-Core application during start-up will set the National Values " +
                    "to the default values according to requirements and values of the configuration parameters."
                },
                new BitField
                {
                    Name = "M_NVCONTACT",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Indicates the reaction to be performed when T_NVCONTACT timer elapses. Allowed values according to [SS026]."
                },
                new BitField
                {
                    Name = "M_NVDERUN",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Entry of Driver ID permitted while running. Allowed values according to [SS026]."
                },
                new BitField
                {
                    Name = "Q_NVEMRRLS",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Qualifier Emergency Brake Release. Allowed values according to [SS026]."
                },
                new BitField
                {
                    Name = "Q_NVSRBKTRG",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Permission to use service brake when braking to a target is supervised. Allowed values according to [SS026]."
                },
                new BitField
                {
                    Name = "Q_NVDRIVER_ADHES",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Qualifier for the modification of trackside adhesion factor by driver. Allowed values according to [SS026]."
                },
                new BitField
                {
                    Name = "Q_DLRBG",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Qualifier telling on which side of the LRBG the estimated front end is. Allowed values according to [SS026]."
                },
                new BitField
                {
                    Name = "QDIR_LRBG_1",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train orientation in relation to the direction of the LRBG."
                },
                new BitField
                {
                    Name = "QDIR_LRBG_2",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train orientation in relation to the direction of the LRBG."
                },
                new BitField
                {
                    Name = "QDIR_LRBG_3",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train orientation in relation to the direction of the LRBG."
                },
                new BitField
                {
                    Name = "QDIR_LRBG_4",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train orientation in relation to the direction of the LRBG."
                },
                new BitField
                {
                    Name = "QDIR_LRBG_5",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train orientation in relation to the direction of the LRBG."
                },
                new BitField
                {
                    Name = "QDIR_LRBG_6",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train orientation in relation to the direction of the LRBG."
                },
                new BitField
                {
                    Name = "QDIR_LRBG_7",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train orientation in relation to the direction of the LRBG."
                },
                new BitField
                {
                    Name = "QDIR_LRBG_8",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Train orientation in relation to the direction of the LRBG."
                },
                new BitField
                {
                    Name = "GLO5_0F0",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "--not used--"
                }
            }
        };

        // checked 20190916 1.6 DIAG manual RVV
        public static DataSetDefinition DIA_205 => new DataSetDefinition
        {
            Name = "DIA_205 GLOBAL_ENVIRONMENT_DATA_6",
            Comment = "Dataset definition of part 6 of global environment data.",
            Identifiers = new List<string>
            {
                "230510360",
                "230511360"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "NID_LRBG_1",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Country/region identity + Balise identity of last relevant Balise group (NID_C + NID_BG)."
                },
                new BitField
                {
                    Name = "NID_LRBG_2",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Country/region identity + Balise identity of last relevant Balise group (NID_C + NID_BG)."
                },
                new BitField
                {
                    Name = "NID_LRBG_3",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Country/region identity + Balise identity of last relevant Balise group (NID_C + NID_BG)."
                },
                new BitField
                {
                    Name = "NID_LRBG_4",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Country/region identity + Balise identity of last relevant Balise group (NID_C + NID_BG)."
                },
                new BitField
                {
                    Name = "NID_LRBG_5",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Country/region identity + Balise identity of last relevant Balise group (NID_C + NID_BG)."
                },
                new BitField
                {
                    Name = "NID_LRBG_6",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Country/region identity + Balise identity of last relevant Balise group (NID_C + NID_BG)."
                },
                new BitField
                {
                    Name = "NID_LRBG_7",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Country/region identity + Balise identity of last relevant Balise group (NID_C + NID_BG)."
                },
                new BitField
                {
                    Name = "NID_LRBG_8",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Country/region identity + Balise identity of last relevant Balise group (NID_C + NID_BG)."
                }
            }
        };

        // checked 20190916 1.6 DIAG manual RVV
        public static DataSetDefinition DIA_206 => new DataSetDefinition
        {
            Name = "DIA_206 GLOBAL_ENVIRONMENT_DATA_7",
            Comment = "Dataset definition of part 7 of global environment data.",
            Identifiers = new List<string>
            {
                "230510370",
                "230511370"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "LRBG_POS_1",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last relevant Balise group position."
                },
                new BitField
                {
                    Name = "LRBG_POS_2",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last relevant Balise group position."
                },
                new BitField
                {
                    Name = "LRBG_POS_3",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last relevant Balise group position."
                },
                new BitField
                {
                    Name = "LRBG_POS_4",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last relevant Balise group position."
                },
                new BitField
                {
                    Name = "LRBG_POS_5",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last relevant Balise group position."
                },
                new BitField
                {
                    Name = "LRBG_POS_6",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last relevant Balise group position."
                },
                new BitField
                {
                    Name = "LRBG_POS_7",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last relevant Balise group position."
                },
                new BitField
                {
                    Name = "LRBG_POS_8",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Last relevant Balise group position."
                }
            }
        };

        // checked 20190916 1.6 DIAG manual RVV
        public static DataSetDefinition DIA_207 => new DataSetDefinition
        {
            Name = "DIA_207 GLOBAL_ENVIRONMENT_DATA_8",
            Comment = "Dataset definition of part 8 of global environment data.",
            Identifiers = new List<string>
            {
                "230510380",
                "230511380"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "MIN_POSITION",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Min position"
                },
                new BitField
                {
                    Name = "MAX_POSITION",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Max position"
                },
                new BitField
                {
                    Name = "NID_NETWORK",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "Identity of Radio Network. Allowed values according to [SS026], NID_MN."
                },
                new BitField
                {
                    Name = "STM_ID",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Last used STM id (NID_STM). Allowed values depends on the installed STM levels. " +
                    "Not allowed are not installed NID-STMs. Value 255 means no STM used."
                },
                new BitField
                {
                    Name = "TemperatureCPU1",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Current temperature CPU1"
                },
                new BitField
                {
                    Name = "TemperatureCPU2",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Current temperature CPU2"
                },
                new BitField
                {
                    Name = "ActiveModem",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Active modem"
                },
                new BitField
                {
                    Name = "NID_ENGINE",
                    BitFieldType = BitFieldType.UInt32,
                    Length = 32,
                    Comment = "On-board ETCS identity according [ERA_VAR]. Contains information on rolling stock fleet/on-board equipment supplier. " +
                    "Reserved ranges, numbers: 0-1023  'Range for Bombardier RCS', 17051 'Range for Bombardier.' " +
                    "The project specific values will be provided by train Operator or by Product Owner within the Bombardier Transportation." +
                    "Note: According SUBSET-26-7 this is a 24 Bit variable. Due to IEC 61375 data type compatibility, this variable is internally pictured on a 32-Bit value."
                },
                new BitField
                {
                    Name = "DiagValid",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Validity of all diagnostic packets (used by vehicle diagnostics). " +
                    "0 = diagnostic data not yet valid (during initialization) " +
                    "1 = all diagnostics data are valid"
                },
                new BitField
                {
                    Name = "GLO8_0A1",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "GLO8_0A2",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "GLO8_0A3",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "GLO8_0A4",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "GLO8_0A5",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "GLO8_0A6",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "GLO8_0A7",
                    BitFieldType = BitFieldType.Spare,
                    Length = 1,
                    Comment = "--not used--"
                },
                new BitField
                {
                    Name = "GLO8_0A8",
                    BitFieldType = BitFieldType.Spare,
                    Length = 88,
                    Comment = "--not used--"
                }
            }
        };

        // checked 20190916 1.6 DIAG manual RVV
        public static DataSetDefinition DIA_209 => new DataSetDefinition
        {
            Name = "DIA_209 LRU_DIAG_ENVIRONMENT",
            Comment = "Dataset definition (container for the specific environment data to the according event).",
            Identifiers = new List<string>
            {
                "230510400",
                "230511400"
            },
            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "LRU_DeviceID",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "Device ID of the subsystem"
                },
                new BitField
                {
                    Name = "LRU_SubDeviceID",
                    BitFieldType = BitFieldType.UInt8,
                    Length = 8,
                    Comment = "If a sub device ID is available, i.e. in case of sub structured Devices"
                },
                new BitField
                {
                    Name = "LRU_DeviceID_Env_Data_1",
                    BitFieldType = BitFieldType.UInt16,
                    Length = 16,
                    Comment = "All data according to subsystem/sub product definition"
                },
                new BitField
                {
                    Name = "LRU_DeviceID_Env_Data_n",
                    BitFieldType = BitFieldType.Spare,
                    Length = 224,
                    Comment = "All data according to subsystem/sub product definition"
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

        // checked 20190920 1.6 DIAG manual RVV
        public static DataSetDefinition DIA_214 => new DataSetDefinition
        {
            Name = "DIA_214 DIAG_WAYSIDE_EVENT",
            Comment = "The Wayside diagnostic packet (including the environment data set DIA_215) contains data " +
            "delivered from the Balise transmission sub-system collected within the ETC software.",
            Identifiers = new List<string>
            {
                "230510440", "230511440"
            },

            BitFields = new List<BitField>
            {
                new BitField
                {
                    Name = "WAY_BTS_OK (IE-)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Balise OK (no BTS error detected)"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_VERSION (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Version error"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_HEADER (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Header error or Balise payload error"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_BAL_MISSING (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Balise missing in a group"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_BAL_MISSING_POS (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Max. distance of expected Balise window passed."
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_BAL_DETECT (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Balise detection error"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_BAL_UNEXPECTED (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Unexpected Balise"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_BAL_REPOS_OR_UNKNW (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Balise reposition error or Balise unknown"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_PACKET_SIZE (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Wrong packet size"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_PACKET_DUPLICATE (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Duplicate packet received"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_PACKET_UNEXPECTED (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Unexpected packet received"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_QDIR (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "QDIR error"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_INFILL (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Infill Balise error"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_BG_STATUS (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Reports the ETC IL handler internal BG-status"
                },
                new BitField
                {
                    Name = "WAY_ERR_LOOP (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Loop error detected"
                },
                new BitField
                {
                    Name = "WAY_ERR_LEU (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "LEU error detected"
                },
                new BitField
                {
                    Name = "WAY_ERR_BTS_BAL_GROUP_MISSING (OS-W)",
                    BitFieldType = BitFieldType.Bool,
                    Length = 1,
                    Comment = "Complete Balise Group missing"
                },
                new BitField
                {
                    Name = "WAY_unused",
                    BitFieldType = BitFieldType.Spare,
                    Length = 15,
                    Comment = "--not used--"
                },
            }
        };
    }
}