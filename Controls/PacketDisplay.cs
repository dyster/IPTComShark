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

        public void SetObject(CapturePacket originalpacket)
        {
            uint ticker = 0;

            textBoxComid.Text = string.Empty;
            textBoxRAW.Text = string.Empty;
            textBoxSize.Text = string.Empty;
            textBoxType.Text = string.Empty;

            var dataLines = new List<DataLine>();

            if (originalpacket.Protocol == ProtocolType.Virtual)
            {
                if (originalpacket.ParsedData != null)
                {
                    foreach (var parsedField in originalpacket.ParsedData.ParsedFields)
                    {
                        dataLines.Add(new DataLine(parsedField, ticker++));
                    }

                    dataListViewRight.DataSource = dataLines;
                    return;
                }
            }

            try
            {
                var extensiveData = CapturePacket.ExtractParsedData(originalpacket, out var displayfields, true);


                if (originalpacket.IPTWPPacket != null)
                {
                    var udp = (UdpPacket) originalpacket.Packet.PayloadPacket.PayloadPacket;
                    var iptPayload = IPTWPPacket.GetIPTPayload(udp, originalpacket.IPTWPPacket);
                    var iptHeader = IPTWPPacket.ExtractHeader(udp.PayloadData);

                    textBoxComid.Text = originalpacket.IPTWPPacket.Comid.ToString();

                    textBoxSize.Text = originalpacket.IPTWPPacket.IPTWPSize.ToString();
                    textBoxType.Text = originalpacket.IPTWPPacket.IPTWPType.ToString();

                    if (originalpacket.IPTWPPacket.IPTWPType == IPTTypes.MA)
                    {
                        dataLines.Add(new DataLine(ticker++)
                            {IsCategory = true, Name = "IPTCom Message Acknowledgement"});

                        var ackCode = BitConverter.ToUInt16(new[] {iptPayload[1], iptPayload[0]}, 0);
                        var ackSeq = BitConverter.ToUInt16(new[] {iptPayload[3], iptPayload[2]}, 0);


                        switch (ackCode)
                        {
                            case 0:
                                dataLines.Add(new DataLine(ticker++) {Name = "Ack Code", Value = "OK"});
                                break;
                            case 1:
                                dataLines.Add(new DataLine(ticker++)
                                    {Name = "Ack Code", Value = "NACK, wrong frame check sequence in data part"});
                                break;
                            case 2:
                                dataLines.Add(new DataLine(ticker++)
                                    {Name = "Ack Code", Value = "NACK, destination unknown / not listening"});
                                break;
                            case 3:
                                dataLines.Add(new DataLine(ticker++)
                                    {Name = "Ack Code", Value = "NACK, wrong data / configuration mismatch"});
                                break;
                            case 4:
                                dataLines.Add(new DataLine(ticker++)
                                    {Name = "Ack Code", Value = "NACK, buffer not available"});
                                break;
                            default:
                                dataLines.Add(new DataLine(ticker++)
                                    {Name = "Ack Code", Value = "Invalid code: " + ackCode});
                                break;
                        }

                        dataLines.Add(new DataLine(ticker++) {Name = "Ack Sequence", Value = ackSeq.ToString()});
                    }


                    if (extensiveData != null && extensiveData.ParsedFields.Count > 0
                    ) // a parser has chugged out something
                    {
                        dataLines.Add(new DataLine(ticker++) {IsCategory = true, Name = "IPTCom Data"});
                        foreach (var field in extensiveData.ParsedFields)
                        {
                            bool changed = false;
                            if (originalpacket.Previous != null && originalpacket.IPTWPPacket.IPTWPType == IPTTypes.PD)
                            {
                                var parsedField = originalpacket.Previous.ParsedData.GetField(field.Name);
                                if(parsedField != null)
                                    changed = !parsedField.Value.Equals(field.Value);
                            }

                            dataLines.Add(new DataLine(field, ticker++)
                            {
                                Changed = changed
                            });
                        }
                    }
                }


                if (originalpacket.SS27Packet != null)
                {
                    dataLines.Add(new DataLine(ticker++) {IsCategory = true, Name = "JRU Data"});

                    dataLines.Add(new DataLine(ticker++) {IsCategory = true, Name = "Header"});
                    dataLines.Add(new DataLine(ticker++)
                    {
                        Name = "Timestamp",
                        Value = originalpacket.SS27Packet.DateTime.ToString() + ":" +
                                originalpacket.SS27Packet.DateTime.Millisecond
                    });
                    dataLines.Add(new DataLine(ticker++) {Name = "Level", Value = originalpacket.SS27Packet.Level});
                    dataLines.Add(new DataLine(ticker++) {Name = "Mode", Value = originalpacket.SS27Packet.Mode});
                    dataLines.Add(new DataLine(ticker++)
                        {Name = "Speed", Value = originalpacket.SS27Packet.V_TRAIN.ToString()});

                    dataLines.AddRange(
                        originalpacket.SS27Packet.Header.Select(parsedField => new DataLine(parsedField, ticker++)));


                    if (originalpacket.SS27Packet.SubMessage != null)
                    {
                        dataLines.Add(new DataLine(ticker++) {IsCategory = true, Name = "SubMessage"});
                        foreach (var parsedField in originalpacket.SS27Packet.SubMessage.ParsedFields)
                        {
                            dataLines.Add(new DataLine(parsedField, ticker++));
                        }
                    }

                    foreach (var ss27PacketExtraMessage in originalpacket.SS27Packet.ExtraMessages)
                    {
                        dataLines.Add(new DataLine(ticker++)
                        {
                            IsCategory = true,
                            Name = ss27PacketExtraMessage.Name,
                            Comment = ss27PacketExtraMessage.Comment
                        });
                        foreach (var parsedField in ss27PacketExtraMessage.ParsedFields)
                        {
                            dataLines.Add(new DataLine(parsedField, ticker++));
                        }
                    }
                }

                if (originalpacket.IPTWPPacket == null && originalpacket.SS27Packet == null && extensiveData != null)
                {
                    dataLines.Add(new DataLine(ticker++) {IsCategory = true, Name = extensiveData.Name});

                    foreach (var field in extensiveData.ParsedFields)
                    {
                        dataLines.Add(new DataLine(field, ticker++));
                    }
                }
            }
            catch (Exception e)
            {
                dataLines.Add(new DataLine(ticker++) {Name = "Error", Value = e.ToString()});
            }

            try
            {
                var text = new StringBuilder(originalpacket.Packet.ToString(StringOutputType.Verbose));


                if (originalpacket.IPTWPPacket != null)
                {
                    // since we have IPT, straight cast to UDP, BAM
                    var udp = (UdpPacket) originalpacket.Packet.PayloadPacket.PayloadPacket;
                    var bytes = IPTWPPacket.GetIPTPayload(udp, originalpacket.IPTWPPacket);
                    var iptHeader = IPTWPPacket.ExtractHeader(udp.PayloadData);

                    var maxLenString = iptHeader.Max(pair => pair.Key.Length);

                    foreach (var head in iptHeader)
                    {
                        text.AppendLine("IPT:\t" + head.Key.PadLeft(maxLenString, ' ') + " = " + head.Value);
                    }

                    text.AppendLine("");

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
                var dataline = (DataLine) dataListViewRight.SelectedObject;

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

        public DataLine(uint tick)
        {
            No = tick;
        }

        public DataLine(ParsedField field, uint tick)
        {
            No = tick;

            _field = field;
            //string typestring = field.Value.GetType().ToString();

            Name = field.Name;
            Value = field.Value.ToString();
            TrueValue = field.TrueValue;
            //Type = typestring.Substring(typestring.LastIndexOf(".") + 1);
            Comment = field.Comment;
        }

        public uint No { get; set; }

        public string Name { get; set; }

        public string Type => _field?.UsedBitField.BitFieldType.ToString();

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