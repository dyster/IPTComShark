using BitDataParser;
using System;
using System.Collections.Generic;
using System.Linq;
using TrainShark.DataSets;
using static BitDataParser.Functions;

namespace TrainShark.Parsers
{
    // TODO why isn't this used?
    public static class SS57Parser
    {
        private static int[] SS58SAPs = {
            0b000000,
            0b000001,
            0b000010,
            0b000011,
            0b000100,
            0b000101,
            0b000110,
            0b000111,
            0b100001,
            0b100010,
            0b100011,
            0b100100,
            0b100101,
            0b100110,
            0b100111,
        };

        public static List<ParsedDataSet> Parse(byte[] data, out List<Tuple<string, object>> displayFields, int SAP)
        {
            var list = new List<ParsedDataSet>();
            displayFields = new List<Tuple<string, object>>();

            var position = 1;
            var header = DataSets.Subset57.SLLHeader.Parse(data);
            list.Add(header);
            position += header.BitsRead;

            var sl = header.GetField("SL").Value.ToString();
            var cmdField = header.GetField("Cmd");
            displayFields.Add(new Tuple<string, object>(cmdField.Name, cmdField.Value));

            //byte nibble1 = (byte)(cmd & 0x0F);
            //byte nibble2 = (byte)((cmd & 0xF0) >> 4);

            //var SL = 0;
            //
            //if (sl >= 0x8 && sl <= 0xB)
            //    SL = 4;
            //else if (sl >= 0x0 && sl <= 0x3)
            //    SL = 2;
            //else if (sl >= 0xC && sl <= 0xF)
            //    SL = 0;
            //else
            //{
            //    throw new ArgumentOutOfRangeException("SL nibble not allowed");
            //}

            var cmd = Convert.ToByte(cmdField.TrueValue);

            var middle = SubArrayGetter(data, position);

            byte[] Parse(DataSetDefinition def, byte[] bytes, ref int positionX)
            {
                var parsedDataSet = def.Parse(bytes);
                list.Add(parsedDataSet);
                positionX += parsedDataSet.BitsRead;
                return SubArrayGetter(bytes, parsedDataSet.BitsRead);
            }

            //{ "0", "Connect Request"},
            //{ "1", "Reserved"},
            //{ "2", "Connect Confirm"},
            //{ "3", "Authentication"},
            //{ "4", "Auth Ack"},
            //{ "5", "Disconnect"},
            //{ "6", "Idle"},

            if (cmd == 0)
            {
                // Connect Request
                middle = Parse(DataSets.Subset57.Cmd0ConnectRequest, middle, ref position);
            }
            else if (cmd == 1)
            {
                // Reserved
            }
            else if (cmd == 2)
            {
                // Connect Confirm
            }
            else if (cmd == 3)
            {
                // Authentication
            }
            else if (cmd == 4)
            {
                // Auth Ack
            }
            else if (cmd == 5)
            {
                // Disconnect
            }
            else if (cmd == 6)
            {
                // IDLE
            }
            else if (cmd == 9)
            {
                // upper layer

                if (SAP >= 8 && SAP <= 31)
                {
                    // on-board function

                    if (SAP == 19)
                    {
                        // 19 is BT ATP stuff
                    }
                    else
                    {
                    }
                }
                else if (SAP == 32)
                {
                    // reference time
                }
                else if (SAP >= 48 && SAP <= 62)
                {
                    // the future of stm?
                }
                else if (SAP == 63)
                {
                    // broadcast
                }
                else if (SS58SAPs.Contains(SAP))
                {
                    // STM channel!

                    var testposition = position;

                    var ffisheader = DataSets.Subset58.FFFISHeader.Parse(middle);
                    list.Add(ffisheader);
                    position += ffisheader.BitsRead;

                    var lmessage = Convert.ToByte(ffisheader.GetField("L_MESSAGE").Value);

                    var payloadLength = lmessage * 8 - ffisheader.BitsRead;
                    middle = SubArrayGetterX(data, position, payloadLength);

                    var testbytes = Parse(Subset58.FFFISHeader, middle, ref position);

                    var ss58 = new Subset58();

                    int remainer = payloadLength;
                    int middlepos = 1;
                    do
                    {
                        var ident = (int)FieldGetter(middle, middlepos, 8);
                        var definition = ss58.FindByIdentifier(new Identifiers { Numeric = { ident } });

                        if (definition == null)
                        {
                            list.Add(ParsedDataSet.CreateError("No SS58 set found for " + ident));
                            break;
                        }

                        var parsedDataSet = definition.Parse(middle, false, middlepos - 1);
                        list.Add(parsedDataSet);

                        middlepos += parsedDataSet.BitsRead;
                        remainer -= parsedDataSet.BitsRead;
                    } while (remainer > 24);

                    if (remainer > 0)
                    {
                        displayFields.Add(new Tuple<string, object>("PaddingBits", remainer));
                        var remainvalue = FieldGetter(middle, middlepos, remainer);
                        displayFields.Add(new Tuple<string, object>("PaddingBitsValue", remainvalue));
                    }

                    position += payloadLength;
                }
                else
                {
                    // twilight zone?
                }
            }

            if (sl != "0")
            {
                var remain = SubArrayGetter(data, position);
                if (sl == "2")
                    list.Add(DataSets.Subset57.SL2Checksum.Parse(remain));
                else if (sl == "4")
                    list.Add(DataSets.Subset57.SL4Checksum.Parse(remain));
            }

            return list;
        }
    }
}