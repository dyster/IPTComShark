using TrainShark.DataSets;
using TrainShark.Parsers;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainShark.Export
{
    public class ProfiSheet
    {
        private ExcelWorksheet worksheet;
        private int colindex = 1;
        private int rowindex = 1;

        private MultiArray<ushort, ushort, DateTime> idletimes = new MultiArray<ushort, ushort, DateTime>();
        private MultiArray<ushort, ushort, uint> idlereftimes = new MultiArray<ushort, ushort, uint>();
        private DateTime firstDate = default;
        private uint firstRefTime = 0;

        public ProfiSheet(ExcelWorksheet ws)
        {
            worksheet = ws;

            ProfiRow profiRow = new ProfiRow();

            foreach (var col in profiRow.Items)
                worksheet.Cells[1, colindex++].Value = col.Name;


            using (ExcelRange range = worksheet.Cells[1, 1, 1, colindex - 1])
            {
                range.Style.Font.Bold = true;
                range.AutoFilter = true;
            }
        }

        private class ProfiRow
        {
            public ProfiRow()
            {
                Items = new List<ProfiCol>
                {
                    new ProfiCol{Name = "Packet No"},
                    new ProfiCol{Name = "Packet Time"},
                    new ProfiCol{Name = "SPLFrameLen"},
                    new ProfiCol{Name = "RecAddr"},
                    new ProfiCol{Name = "SndAddr"}, //5
                    new ProfiCol{Name = "DSAP"},
                    new ProfiCol{Name = "SSAP"},
                    new ProfiCol{Name = "FDL mode"},
                    new ProfiCol{Name = "SLL Seq"},
                    new ProfiCol{Name = "SL"}, //10
                    new ProfiCol{Name = "Cmd"},
                    new ProfiCol{Name = "Timestamp"},
                    new ProfiCol{Name = "Connect: Random"},
                    new ProfiCol{Name = "Connect: Idle Timeout"},
                    new ProfiCol{Name = "Disconnect: New setup desired"}, //15
                    new ProfiCol{Name = "Disconnect: Reason"},
                    new ProfiCol{Name = "ERROR"},
                    new ProfiCol{Name = "SAP Delta time"},
                    new ProfiCol{Name = "reftime offset"},
                    new ProfiCol{Name = "SAP Delta reftime"}, //20
                };
            }

            public List<ProfiCol> Items { get; set; }
        }

        private class ProfiCol
        {
            public string Name;
            int Index;
            public object Value;

            public override string ToString()
            {
                if (Value == null)
                    return Name + ": null";
                return Name + ": " + Value.ToString();
            }
        }

        public int Push(CapturePacket packet, ParseOutput parse)
        {


            var addRows = new List<ProfiRow>();

            if (packet.Protocol == ProtocolType.UDP_SPL)
            {



                ushort sndaddr = 0;
                ushort dsap = 0;

                foreach (var parsedDataSet in parse.ParsedData)
                {
                    if (parsedDataSet.Definition == null)
                    {
                        if (parsedDataSet.ParsedFields.Count == 1 && parsedDataSet.ParsedFields[0].Name == "ERROR")
                            addRows.Last().Items[16].Value = parsedDataSet.ParsedFields[0].Value;
                        continue;
                    }


                    if (parsedDataSet.Definition.Name == DataSets.VAP.UDP_SPL.Name)
                    {

                        sndaddr = Convert.ToUInt16(parsedDataSet.ParsedFields[2].Value); // sndaddr
                        dsap = Convert.ToUInt16(parsedDataSet.ParsedFields[3].Value); // dsap

                        // SPL header means new profibus packet        
                        ProfiRow profiRow = new ProfiRow();

                        profiRow.Items[0].Value = packet.No;
                        profiRow.Items[1].Value = packet.Date.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        profiRow.Items[2].Value = parsedDataSet.ParsedFields[0].Value;
                        profiRow.Items[3].Value = parsedDataSet.ParsedFields[1].Value; //recaddr
                        profiRow.Items[4].Value = parsedDataSet.ParsedFields[2].Value; //sndaddr
                        profiRow.Items[5].Value = parsedDataSet.ParsedFields[3].Value; //dsap
                        profiRow.Items[6].Value = parsedDataSet.ParsedFields[4].Value; //ssap
                        profiRow.Items[7].Value = parsedDataSet.ParsedFields[5].Value; // fdl mode

                        addRows.Add(profiRow);







                        if (idletimes.ContainsKey(sndaddr, dsap))
                        {
                            var beforetime = idletimes[sndaddr, dsap];
                            var span = packet.Date - beforetime;

                            profiRow.Items.First(i => i.Name == "SAP Delta time").Value = span.TotalMilliseconds;
                        }

                        idletimes[sndaddr, dsap] = packet.Date;


                    }
                    else if (parsedDataSet.Definition.Name == Subset57.SLLHeader.Name)
                    {
                        addRows.Last().Items[8].Value = parsedDataSet.ParsedFields[0].Value;
                        addRows.Last().Items[9].Value = parsedDataSet.ParsedFields[1].Value;
                        addRows.Last().Items[10].Value = parsedDataSet.ParsedFields[2].Value;

                    }
                    else if (parsedDataSet.Definition.Name == Subset57.SLLTimestamp.Name)
                    {
                        var refTime = Convert.ToUInt32(parsedDataSet.ParsedFields[0].Value);
                        addRows.Last().Items[11].Value = refTime;

                        if (firstDate == default)
                        {
                            firstDate = packet.Date;
                            firstRefTime = refTime;
                        }
                        else
                        {
                            var dateDelta = (packet.Date - firstDate).TotalMilliseconds;
                            var refOffset = (refTime - firstRefTime);

                            addRows.Last().Items[18].Value = dateDelta - refOffset;
                        }

                        if (sndaddr == 0 || dsap == 0)
                            throw new ArgumentOutOfRangeException();

                        if (idlereftimes.ContainsKey(sndaddr, dsap))
                        {
                            var beforetime = idlereftimes[sndaddr, dsap];
                            var span = refTime - beforetime;

                            addRows.Last().Items[19].Value = span;
                        }

                        idlereftimes[sndaddr, dsap] = refTime;


                    }
                    else if (parsedDataSet.Definition.Name == Subset57.Cmd0ConnectRequest.Name)
                    {
                        if (parsedDataSet.ParsedFields.Count == 1)
                        {
                            addRows.Last().Items[12].Value = "PARSE ERROR";
                        }
                        else
                        {
                            addRows.Last().Items[12].Value = parsedDataSet.ParsedFields[0].Value;
                            addRows.Last().Items[13].Value = parsedDataSet.ParsedFields[1].Value;
                        }

                    }
                    else if (parsedDataSet.Definition.Name == Subset57.Cmd5Disconnect.Name)
                    {
                        if (parsedDataSet.ParsedFields.Count == 1)
                        {
                            addRows.Last().Items[14].Value = "PARSE ERROR";
                        }
                        else
                        {
                            addRows.Last().Items[14].Value = parsedDataSet.ParsedFields[0].Value;
                            addRows.Last().Items[15].Value = parsedDataSet.ParsedFields[1].Value;
                        }

                    }
                    else
                    {

                    }
                }


            }
            int news = 0;
            foreach (var addrow in addRows)
            {




                rowindex++;
                worksheet.Cells[rowindex, 2].Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss.000";
                for (int i = 0; i < addrow.Items.Count; i++)
                {
                    worksheet.Cells[rowindex, i + 1].Value = addrow.Items[i].Value;
                }
                news++;
            }

            return news;
        }
    }
}