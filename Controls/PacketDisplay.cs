using PacketDotNet;
using sonesson_tools.BitStreamParser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPTComShark.Windows;
using IPTComShark.Parsers;

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

        public BackStore.BackStore BackStore { get; set; }
        public ParserFactory ParserFactory { get; set; }

        public void SetObject(CapturePacket originalpacket)
        {
            uint ticker = 0;

            textBoxComid.Text = string.Empty;
            textBoxRAW.Text = string.Empty;
            textBoxSize.Text = string.Empty;
            textBoxType.Text = string.Empty;

            var dataLines = new List<DataLine>();

            var topPacket = BackStore.GetPacket(originalpacket.No);

            if (originalpacket.Protocol == ProtocolType.Virtual)
            {
                // TODO Virtual is now broken! FIX!
                //foreach (var parsedField in originalpacket.ParsedData.SelectMany(dataset => dataset.ParsedFields))
                //{
                //    dataLines.Add(new DataLine(parsedField, ticker++));
                //}

                dataListViewRight.DataSource = dataLines;
                return;
            }

            try
            {
                var payload = BackStore.GetPayload(originalpacket.No);

                Packet actionPacket = topPacket.PayloadPacket;

                // if this is a vlan packet, just use the underlying packet instead
                // TODO figure out how to detect other VLAN packets
                if (topPacket.PayloadPacket is Ieee8021QPacket vlanpacket)
                {
                    if (vlanpacket.HasPayloadPacket)
                    {
                        actionPacket = vlanpacket.PayloadPacket;
                    }
                }

                
                
                Parse? parse = ParserFactory.DoPacket(originalpacket.Protocol, payload);


                if (originalpacket.Protocol == ProtocolType.IPTWP && actionPacket.PayloadPacket != null)
                {
                    var udp = (UdpPacket)actionPacket.PayloadPacket;
                    var iptPacket = IPTWPPacket.Extract(udp.PayloadData);

                    if (iptPacket != null)
                    { 

                        var iptPayload = IPTWPPacket.GetIPTPayload(udp.PayloadData);
                        var iptHeader = IPTWPPacket.ExtractHeader(udp.PayloadData);

                        textBoxComid.Text = originalpacket.Comid.ToString();

                        textBoxSize.Text = iptPacket.IPTWPSize.ToString();
                        textBoxType.Text = iptPacket.IPTWPType.ToString();

                        if (parse.HasValue && !parse.Value.NoParserInstalled && parse.Value.ParsedData.Count == 1 &&
                            originalpacket.Previous != null)
                        {
                            // if only one set we can do change detection
                            var oldpayload = BackStore.GetPayload(originalpacket.Previous.No);
                            Parse? oldparse = ParserFactory.DoPacket(originalpacket.Previous.Protocol, oldpayload);

                            dataLines.Add(new DataLine(ticker++)
                            { IsCategory = true, Name = parse.Value.ParsedData[0].Name });
                            for (var index = 0; index < parse.Value.ParsedData[0].ParsedFields.Count; index++)
                            {
                                var field = parse.Value.ParsedData[0].ParsedFields[index];
                                bool changed = false;


                                if (oldparse.HasValue && oldparse.Value.ParsedData[0].ParsedFields.Count > index)
                                {
                                    var parsedField = oldparse.Value.ParsedData[0][index];
                                    changed = !parsedField.Value.Equals(field.Value);
                                }


                                dataLines.Add(new DataLine(field, ticker++)
                                {
                                    Changed = changed
                                });
                            }
                        }
                        else if (parse.HasValue && !parse.Value.NoParserInstalled)
                        {
                            foreach (var parsedDataSet in parse.Value.ParsedData)
                            {
                                dataLines.Add(new DataLine(ticker++) { IsCategory = true, Name = parsedDataSet.Name });
                                foreach (var field in parsedDataSet.ParsedFields)
                                {
                                    dataLines.Add(new DataLine(field, ticker++));
                                }
                            }
                        }
                        else
                        {
                            // where does that leave us?
                        }
                    }
                }
                else if (parse.HasValue && !parse.Value.NoParserInstalled)
                {
                    foreach (var parsedDataSet in parse.Value.ParsedData)
                    {
                        dataLines.Add(new DataLine(ticker++) {IsCategory = true, Name = parsedDataSet.Name});
                        foreach (var field in parsedDataSet.ParsedFields)
                        {
                            dataLines.Add(new DataLine(field, ticker++));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                dataLines.Add(new DataLine(ticker++) {Name = "Error", Value = e.ToString()});
            }

            try
            {
                var text = new StringBuilder(topPacket.ToString(StringOutputType.Verbose));

                Packet actionPacket = topPacket.PayloadPacket;

                // if this is a vlan packet, just use the underlying packet instead
                // TODO figure out how to detect other VLAN packets
                if (topPacket.PayloadPacket is Ieee8021QPacket vlanpacket)
                {
                    if (vlanpacket.HasPayloadPacket)
                    {
                        actionPacket = vlanpacket.PayloadPacket;
                    }
                }

                if (originalpacket.Protocol == ProtocolType.IPTWP)
                {
                    // since we have IPT, straight cast to UDP, BAM
                    var udp = (UdpPacket)actionPacket.PayloadPacket;
                    var bytes = IPTWPPacket.GetIPTPayload(udp.PayloadData);
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