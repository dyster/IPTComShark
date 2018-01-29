using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Common.Classes;

namespace LiveRecorder
{
    public partial class PacketDisplay : UserControl
    {
        public PacketDisplay()
        {
            InitializeComponent();
        }

        public IPTConfigReader IptConfigReader { get; set; }


        public void SetObject(IPTWPPacket packet)
        {
            textBoxComid.Text = packet.Comid.ToString();
            textBoxRAW.Text = BitConverter.ToString(packet.IPTWPPayload);
            textBoxSize.Text = packet.IPTWPSize.ToString();
            textBoxType.Text = packet.IPTWPType;

            var dataLines = new List<DataLine>();

            Dataset datasetByComId = null;
            if (IptConfigReader != null)
                datasetByComId = IptConfigReader.GetDatasetByComId(packet.Comid);


            if (packet.DictionaryData != null) // a parser has chugged out something
                foreach (KeyValuePair<string, object> pair in packet.DictionaryData)
                {
                    string typestring = pair.Value.GetType().ToString();
                    dataLines.Add(new DataLine
                    {
                        Name = pair.Key,
                        Value = pair.Value.ToString(),
                        Type = typestring.Substring(typestring.LastIndexOf(".") + 1)
                    });
                }
            else if (datasetByComId == null)
                for (var i = 0; i < packet.IPTWPPayload.Length; i++)
                {
                    byte b = packet.IPTWPPayload[i];
                    dataLines.Add(new DataLine {Name = "Byte " + i, Type = "Byte", Value = $"0x{b:X2} = {b}"});
                }
            else
                dataLines.AddRange(ParseDataByIpt(datasetByComId, packet));


            dataListViewRight.DataSource = dataLines;
        }

        private static List<DataLine> ParseDataByIpt(Dataset datasetByComId, IPTWPPacket packet)
        {
            var dataLines = new List<DataLine>();
            try
            {
                var pointer = 0;
                foreach (ProcessVariable processVariable in datasetByComId.processvariable)
                {
                    int arraysize = int.Parse(processVariable.arraysize);

                    var dataLine = new DataLine();
                    dataLine.Name = processVariable.name;
                    if (arraysize > 1)
                        dataLine.Type = processVariable.type + " * " + arraysize;
                    else
                        dataLine.Type = processVariable.type;


                    switch (processVariable.type)
                    {
                        case "BOOLEAN8":
                        case "BOOL8":
                            var bools = new bool[arraysize];
                            for (var i = 0; i < arraysize; i++)
                                bools[i] = packet.IPTWPPayload[pointer + 1] != 0x00;
                            dataLine.Value = string.Join(",", bools);
                            pointer += arraysize;
                            break;
                        case "UINT8":
                            var uints = new uint[arraysize];
                            Array.Copy(packet.IPTWPPayload, pointer, uints, 0, arraysize);
                            dataLine.Value = string.Join(",", uints);
                            pointer += arraysize;
                            break;
                        case "INT8":
                            var ints = new int[arraysize];
                            Array.Copy(packet.IPTWPPayload, pointer, ints, 0, arraysize);
                            dataLine.Value = string.Join(",", ints);
                            pointer += arraysize;
                            break;
                        case "UINT16":
                            var uint16s = new UInt16[arraysize];
                            for (var i = 0; i < arraysize; i++)
                                uint16s[i] =
                                    BitConverter.ToUInt16(
                                        new[]
                                        {
                                            packet.IPTWPPayload[pointer + i * 2 + 1],
                                            packet.IPTWPPayload[pointer + i * 2]
                                        }, 0);
                            dataLine.Value = string.Join(",", uint16s);
                            pointer += arraysize * 2;
                            break;
                        case "INT16":
                            var int16s = new Int16[arraysize];
                            for (var i = 0; i < arraysize; i++)
                                int16s[i] = BitConverter.ToInt16(
                                    new[]
                                    {
                                        packet.IPTWPPayload[pointer + i * 2 + 1],
                                        packet.IPTWPPayload[pointer + i * 2]
                                    }, 0);
                            dataLine.Value = string.Join(",", int16s);
                            pointer += arraysize * 2;
                            break;
                        case "UINT32":
                            var uint32s = new UInt32[arraysize];
                            for (var i = 0; i < arraysize; i++)
                                uint32s[i] =
                                    BitConverter.ToUInt32(
                                        new[]
                                        {
                                            packet.IPTWPPayload[pointer + i * 2 + 3],
                                            packet.IPTWPPayload[pointer + i * 2 + 2],
                                            packet.IPTWPPayload[pointer + i * 2 + 1],
                                            packet.IPTWPPayload[pointer + i * 2]
                                        }, 0);
                            dataLine.Value = string.Join(",", uint32s);
                            pointer += arraysize * 4;
                            break;
                        case "INT32":
                            var int32s = new Int32[arraysize];
                            for (var i = 0; i < arraysize; i++)
                                int32s[i] = BitConverter.ToInt32(
                                    new[]
                                    {
                                        packet.IPTWPPayload[pointer + i * 2 + 3],
                                        packet.IPTWPPayload[pointer + i * 2 + 2],
                                        packet.IPTWPPayload[pointer + i * 2 + 1],
                                        packet.IPTWPPayload[pointer + i * 2]
                                    }, 0);
                            dataLine.Value = string.Join(",", int32s);
                            pointer += arraysize * 4;
                            break;
                        case "REAL32":
                            var floats = new float[arraysize];
                            for (var i = 0; i < arraysize; i++)
                                floats[i] = BitConverter.ToSingle(
                                    new[]
                                    {
                                        packet.IPTWPPayload[pointer + i * 2 + 3],
                                        packet.IPTWPPayload[pointer + i * 2 + 2],
                                        packet.IPTWPPayload[pointer + i * 2 + 1],
                                        packet.IPTWPPayload[pointer + i * 2]
                                    }, 0);
                            dataLine.Value = string.Join(",", floats);
                            pointer += arraysize * 4;
                            break;
                        case "CHAR8":
                            var chars = new byte[arraysize];
                            Array.Copy(packet.IPTWPPayload, pointer, chars, 0, arraysize);
                            dataLine.Value = "String: " + Encoding.ASCII.GetString(chars).Trim((char) 0) +
                                             " Bytes: " + BitConverter.ToString(chars);
                            pointer += arraysize;
                            break;
                        default:
                            dataLine.Value = "UNKNOWN TYPE";
                            pointer += arraysize;
                            break;
                    }
                    dataLines.Add(dataLine);
                }

                if (pointer != packet.IPTWPPayload.Length)
                    dataLines.Add(new DataLine
                    {
                        Name = "OH NO",
                        Type = "BEWARE",
                        Value = "DATA DOES NOT MATCH DATASET DEFINITION"
                    });
            }
            catch (Exception exception)
            {
                dataLines.Add(new DataLine {Name = "EXCEPTION", Type = "THROWN", Value = exception.Message});
            }

            return dataLines;
        }
    }
}