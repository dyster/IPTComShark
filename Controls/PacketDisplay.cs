using IPTComShark.XmlFiles;
using PacketDotNet;
using sonesson_tools.BitStreamParser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPTComShark.Windows;

namespace IPTComShark.Controls
{
    public partial class PacketDisplay : UserControl
    {
        public PacketDisplay()
        {
            InitializeComponent();


            dataListViewRight.RowFormatter += item =>
            {
                DataLine line = (DataLine) item.RowObject;
                if (line.Changed)
                    item.BackColor = Color.LightSeaGreen;
                if (line.IsCategory)
                    item.Font = new Font(item.Font, FontStyle.Bold);
            };
        }

        public IPTConfigReader IptConfigReader { get; set; }

        public void SetObject(CapturePacket packet)
        {
            textBoxComid.Text = string.Empty;
            textBoxRAW.Text = string.Empty;
            textBoxSize.Text = string.Empty;
            textBoxType.Text = string.Empty;

             

            var dataLines = new List<DataLine>();
            if (packet.IPTWPPacket != null)
            {
                

                textBoxComid.Text = packet.IPTWPPacket.Comid.ToString();
                
                textBoxSize.Text = packet.IPTWPPacket.IPTWPSize.ToString();
                textBoxType.Text = packet.IPTWPPacket.IPTWPType.ToString();


                if (packet.ParsedData?.GetDataDictionary() != null) // a parser has chugged out something
                    foreach (var field in packet.ParsedData.ParsedFields)
                    {
                        bool changed = false;
                        if (packet.Previous != null && packet.IPTWPPacket.IPTWPType == IPTTypes.PD)
                        {
                            // not checking for null because frankly it shouldn't happen and we want an exception
                            changed = !packet.Previous.ParsedData.GetField(field.Name).Value.Equals(field.Value);
                        }

                        dataLines.Add(new DataLine(field)
                        {
                            Changed = changed
                        });
                    }
            }

            if (packet.SS27Packet != null)
            {
                dataLines.Add(new DataLine() {IsCategory = true, Name = "Header"});
                dataLines.Add(new DataLine() {Name = "Level", Value = packet.SS27Packet.Level});
                dataLines.Add(new DataLine() {Name = "Mode", Value = packet.SS27Packet.Mode});
                dataLines.Add(new DataLine() {Name = "Speed", Value = packet.SS27Packet.V_TRAIN.ToString()});

                dataLines.AddRange(packet.SS27Packet.Header.Select(parsedField => new DataLine(parsedField)));


                if (packet.SS27Packet.SubMessage != null)
                {
                    dataLines.Add(new DataLine() {IsCategory = true, Name = "SubMessage"});
                    foreach (var parsedField in packet.SS27Packet.SubMessage.ParsedFields)
                    {
                        dataLines.Add(new DataLine(parsedField));
                    }
                }

                foreach (var ss27PacketExtraMessage in packet.SS27Packet.ExtraMessages)
                {
                    dataLines.Add(new DataLine()
                    {
                        IsCategory = true,
                        Name = ss27PacketExtraMessage.Name,
                        Comment = ss27PacketExtraMessage.Comment
                    });
                    foreach (var parsedField in ss27PacketExtraMessage.ParsedFields)
                    {
                        dataLines.Add(new DataLine(parsedField));
                    }
                }
            }

            try
            {
                


                var text = new StringBuilder(packet.Packet.ToString(StringOutputType.Verbose));


                if (packet.IPTWPPacket != null)
                {
                    text.AppendLine("IPT: comid: " + packet.IPTWPPacket.Comid);
                    text.AppendLine("IPT: size: " + packet.IPTWPPacket.IPTWPSize);
                    text.AppendLine("IPT: type: " + packet.IPTWPPacket.IPTWPType);

                    // since we have IPT, straight cast to UDP, BAM
                    var udp = (UdpPacket)packet.Packet.PayloadPacket.PayloadPacket;
                    
                    var bytes = IPTWPPacket.GetIPTPayload(udp, packet.IPTWPPacket);
                    textBoxRAW.Text = BitConverter.ToString(bytes);

                    string str1 = "";
                    string str2 = "";
                    text.AppendLine("IPT:  ******* Raw Hex Output - length=" + (object) bytes.Length + " bytes");
                    text.AppendLine("IPT: Segment:                   Bytes:                              Ascii:");
                    text.AppendLine("IPT: --------------------------------------------------------------------------");
                    for (int index = 1; index <= bytes.Length; ++index)
                    {
                        str1 = str1 + bytes[index - 1].ToString("x").PadLeft(2, '0') + " ";
                        if (bytes[index - 1] < (byte) 33 || bytes[index - 1] > (byte) 126)
                            str2 += ".";
                        else
                            str2 += Encoding.ASCII.GetString(new byte[1]
                            {
                                bytes[index - 1]
                            });
                        if (index % 16 != 0 && index % 8 == 0)
                        {
                            str1 += " ";
                            str2 += " ";
                        }

                        if (index % 16 == 0)
                        {
                            string str3 = ((index - 16) / 16 * 10).ToString().PadLeft(4, '0');
                            text.AppendLine("IPT: " + str3 + "  " + str1 + "  " + str2);
                            str1 = "";
                            str2 = "";
                        }
                        else if (index == bytes.Length)
                        {
                            string str3 = (((index - 16) / 16 + 1) * 10).ToString().PadLeft(4, '0');
                            text.AppendLine("IPT: " + str3.ToString().PadLeft(4, '0') + "  " + str1.PadRight(49, ' ') +
                                            "  " + str2);
                        }
                    }
                }

                richTextBox1.Text = text.ToString();
            }
            catch (Exception e)
            {
                richTextBox1.Text = e.ToString();
            }

            dataListViewRight.DataSource = dataLines;
        }

        // TODO this is not needed anymore I believe
        /*
        private static List<DataLine> ParseDataByIpt(Dataset datasetByComId, IPTWPPacket packet)
        {
            var dataLines = new List<DataLine>();
            try
            {
                var pointer = 0;
                foreach (ProcessVariable processVariable in datasetByComId.Processvariable)
                {
                    int arraysize = int.Parse(processVariable.Arraysize);

                    var dataLine = new DataLine();
                    dataLine.Name = processVariable.Name;
                    if (arraysize > 1)
                        dataLine.Type = processVariable.Type + " * " + arraysize;
                    else
                        dataLine.Type = processVariable.Type;


                    switch (processVariable.Type)
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
                            var uint16s = new ushort[arraysize];
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
                            var int16s = new short[arraysize];
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
                            var uint32s = new uint[arraysize];
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
                            var int32s = new int[arraysize];
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
        }*/

        private void analyzeValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataListViewRight.SelectedObject != null)
            {
                var dataline = (DataLine)dataListViewRight.SelectedObject;

                var parsedField = dataline.GetField();
                if (parsedField != null)
                {
                    var textWindow = new TextWindow(parsedField.ToStringExtended());
                    textWindow.Show();
                }
            }
        }
    }

    public class DataLine
    {
        private readonly ParsedField _field;

        public DataLine()
        {
        }

        public DataLine(ParsedField field)
        {
            _field = field;
            string typestring = field.Value.GetType().ToString();

            Name = field.Name;
            Value = field.Value.ToString();
            TrueValue = field.TrueValue;
            Type = typestring.Substring(typestring.LastIndexOf(".") + 1);
            Comment = field.Comment;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public object TrueValue { get; set; }
        public string Comment { get; set; }
        public bool Changed { get; set; }
        public bool IsCategory { get; set; }

        public ParsedField GetField()
        {
            return _field;
        }
    }
}