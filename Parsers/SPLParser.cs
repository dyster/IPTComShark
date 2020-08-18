using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPTComShark.DataSets;
using sonesson_tools;
using sonesson_tools.BitStreamParser;
using sonesson_toolsNETSTANDARD.DataParsers.Subset57;
using sonesson_toolsNETSTANDARD.DataSets;

namespace IPTComShark.Parsers
{
    class SPLParser : IParser
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
        public Parse Extract(byte[] payload)
        {
            var parse = new Parse();

            parse.DisplayFields = new List<DisplayField>();
            parse.ParsedData = new List<ParsedDataSet>();

            var seqNr = payload[0];
            var position = 9;

            int remainer = 0;

            do
            {
                // get the whole remainder of unparsed data
                payload = Functions.SubArrayGetter(payload, position);

                // reset the position to the new dataset
                position = 1;

                // parse the SPL header
                var spl = VAP.UDP_SPL.Parse(payload);
                parse.ParsedData.Add(spl);
                position += spl.BitsRead;

                // add the DSAP to displayfields
                var sapField = spl.GetField("DSAP");
                parse.DisplayFields.Add(new DisplayField(sapField));

                // get the length of the frame and the data inside the frame
                var frameLen = (ushort) spl.GetField("SPLFrameLen").Value;
                var splframeArray = Functions.SubArrayGetterX(payload, position, frameLen * 8);
                // forward the position for the next iteration
                position += frameLen * 8;

                // get the SLL header
                var header = sonesson_toolsNETSTANDARD.DataSets.Subset57.SLLHeader.Parse(splframeArray);
                parse.ParsedData.Add(header);
                
                // get the SIL level
                var sl = header.GetField("SL").Value.ToString();

                // get the SS57 Command and add it to displayfields
                var cmdField = header.GetField("Cmd");
                parse.DisplayFields.Add(new DisplayField(cmdField.Name, cmdField.Value));
                var cmd = Convert.ToByte(cmdField.TrueValue);

                ParsedDataSet checksum = null;

                int checksumlength = 0;
                int timestamplength = 0;
                int headerLength = header.BitsRead / 8;
                if (cmd == 9)
                    timestamplength = 4;

                if (sl == "2")
                {
                    checksumlength = 4;
                    
                    var checksumBytes = new byte[checksumlength];
                    Array.Copy(splframeArray, splframeArray.Length - checksumlength, checksumBytes, 0, checksumlength);
                    checksum = sonesson_toolsNETSTANDARD.DataSets.Subset57.SL2Checksum.Parse(checksumBytes);
                }
                else if (sl == "4")
                {
                    checksumlength = 6;
                    
                    var checksumBytes = new byte[checksumlength];
                    Array.Copy(splframeArray, splframeArray.Length - checksumlength, checksumBytes, 0, checksumlength);
                    checksum = sonesson_toolsNETSTANDARD.DataSets.Subset57.SL4Checksum.Parse(checksumBytes);
                }
               

                // 2 for header
                byte[] framePayload = new byte[splframeArray.Length - headerLength - checksumlength - timestamplength];
                Array.Copy(splframeArray, headerLength, framePayload, 0, framePayload.Length);

                if (timestamplength > 0)
                {
                    byte[] timestampBytes = new byte[timestamplength];
                    Array.Copy(splframeArray, splframeArray.Length - checksumlength - timestamplength, timestampBytes, 0, timestamplength);

                    //var timestamp = BitConverter.ToUInt32(timestampBytes, 0);
                    var timeStampSet = Subset57.SLLTimestamp.Parse(timestampBytes.Reverse().ToArray());
                    parse.ParsedData.Add(timeStampSet);
                }

                var framePosition = 1;
                

                //var sll = SS57Parser.Parse(splframeArray, out var outfields, Convert.ToInt32(sapField.TrueValue));

                //parse.ParsedData.AddRange(sll);
                //parse.DisplayFields.AddRange(outfields.Select(of => new DisplayField(of.Item1, of.Item2)));
                /*
                int sllread = 0;
                foreach (var parsedDataSet in sll)
                {
                    sllread += parsedDataSet.BitsRead;
                    parse.DisplayFields.AddRange(parsedDataSet.ParsedFields.Select(f => new DisplayField(f)));
                }

                if (splframeArray.Length * 8 > sllread)
                {
                    var sllPayload = Functions.SubArrayGetter(splframeArray, sllread + 1);


                    parse.DisplayFields.Add(new DisplayField("SLLPayloadRemain", BitConverter.ToString(sllPayload)));
                }*/

                var SAP = Convert.ToInt32(sapField.TrueValue);
                if (cmd == 6)
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
                            var nid_atp = BitConverter.ToUInt16(new[] {framePayload[1], framePayload[0]}, 0);
                            if (nid_atp == 266)
                            {
                                var parsedDataSet = VAP.ATPCULifeSign.Parse(framePayload);
                                parse.ParsedData.Add(parsedDataSet);
                                parse.DisplayFields.Add(new DisplayField("ATP", "Lifesign"));

                                framePosition += parsedDataSet.BitsRead;
                                
                                
                            }
                            else
                            {
                                parse.DisplayFields.Add(new DisplayField("ATP NID", nid_atp));
                            }
                            
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

                        var ffisheader = Subset58.FFFISHeader.Parse(framePayload);
                        parse.ParsedData.Add(ffisheader);
                        framePosition += ffisheader.BitsRead;

                        var lmessage = Convert.ToByte(ffisheader.GetField("L_MESSAGE").Value);
                        var ss58payloadLength = lmessage * 8 - ffisheader.BitsRead;
                        var ss58payload = Functions.SubArrayGetterX(framePayload, framePosition, ss58payloadLength);


                        var ss58 = new Subset58();

                        int ss58remainer = ss58payloadLength;
                        int ss58pos = 1;
                        do
                        {
                            var ident = Functions.FieldGetter(ss58payload, ss58pos, 8);
                            var definition = ss58.FindByIdentifier(ident.ToString());
                            parse.DisplayFields.Add(new DisplayField("STM-", ident));

                            if (definition == null)
                            {
                                parse.ParsedData.Add(ParsedDataSet.CreateError("No SS58 set found for " + ident));
                                parse.DisplayFields.Add(new DisplayField("ERROR", "NO DATASET"));
                                break;
                            }

                            var parsedDataSet = definition.Parse(ss58payload, false, ss58pos - 1);
                            parse.ParsedData.Add(parsedDataSet);

                            ss58pos += parsedDataSet.BitsRead;
                            framePosition += parsedDataSet.BitsRead;
                            ss58remainer -= parsedDataSet.BitsRead;



                        } while (ss58remainer > 24);
                    }
                    else
                    {
                        // twilight zone?
                    }


                }

                var padding = framePayload.Length * 8 - framePosition;
                if (padding > 0)
                    parse.DisplayFields.Add(new DisplayField("Padding", padding));

                // wait til end to add checksum
                if (checksum != null) parse.ParsedData.Add(checksum);

                remainer = payload.Length * 8 - (position);
            } while (remainer > 0);


            if(remainer > -1)
                parse.DisplayFields.Add(new DisplayField("Remaining bits", remainer));

            //if (remainer > 0)
            //{
            //    var subArrayGetter = Functions.SubArrayGetter(payload, position);
            //    this.DisplayFields.Add(new Tuple<string, object>("Remainer", BitConverter.ToString(subArrayGetter)));
            //}

            return parse;
        }

        public ProtocolType ProtocolType => ProtocolType.UDP_SPL;
    }
}
