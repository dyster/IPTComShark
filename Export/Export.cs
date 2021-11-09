using IPTComShark.Parsers;
using OfficeOpenXml;
using PacketDotNet;
using sonesson_tools;
using sonesson_tools.BitStreamParser;
using sonesson_tools.DataParsers;
using sonesson_tools.Generic;
using sonesson_toolsNETSTANDARD.DataSets;
using sonesson_toolsNETSTANDARD.Generic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Legends;

namespace IPTComShark.Export
{
    public class IdleAnalyser
    {
        private ExcelWorksheet worksheet;
        private int colindex = 1;
        private int rowindex = 1;

        private MultiArray<ushort, ushort, List<double>> idleTimes = new MultiArray<ushort, ushort, List<double>>();
        private MultiArray<ushort, ushort, DateTime> lastReceivedMsg = new MultiArray<ushort, ushort, DateTime>();
        
        private DateTime firstDate = default;
        private uint firstRefTime = 0;

        public IdleAnalyser(ExcelWorksheet ws)
        {
            worksheet = ws;

            
        }

        public PlotModel Finalize()
        {
            worksheet.Cells[1, colindex].Value = "Snd Addr";
            worksheet.Cells[2, colindex].Value = "SAP";

            worksheet.Cells[4, colindex].Value = "Samples";
            worksheet.Cells[5, colindex].Value = "Average";
            worksheet.Cells[6, colindex].Value = "Min";
            worksheet.Cells[7, colindex].Value = "Max";

            worksheet.Cells[9, colindex].Value = "Sample Distribution";

            var minDistro = 3700;
            var maxDistro = 5100;
            var stepDistro = 20;
            var steps = ((maxDistro - minDistro) / stepDistro);
            worksheet.Cells[10, colindex].Value = "<" + minDistro;
            for (int index = 0; index < steps; index++)
            {
                var min = (minDistro + (index * stepDistro));
                var max = min + stepDistro - 1;
                worksheet.Cells[11 + index, colindex].Value = min + "-" + (max);
            }
            worksheet.Cells[11 + steps, colindex].Value = ">=" + maxDistro;

            colindex++;

            List<ushort> sndaddrs = idleTimes.GetKeys().ToList();
            sndaddrs.Sort();

            foreach (var sndaddr in sndaddrs)
            {
                List<ushort> saps = idleTimes.GetSecondKeys(sndaddr).ToList();
                saps.Sort();

                foreach (var sap in saps)
                {
                    List<double> idles = idleTimes[sndaddr, sap];

                    worksheet.Cells[1, colindex].Value = sndaddr;
                    worksheet.Cells[2, colindex].Value = sap;

                    worksheet.Cells[4, colindex].Value = idles.Count();
                    worksheet.Cells[5, colindex].Value = idles.Average();
                    worksheet.Cells[6, colindex].Value = idles.Min();
                    worksheet.Cells[7, colindex].Value = idles.Max();

                    worksheet.Cells[10, colindex].Value = Convert.ToDouble(idles.Count(i => i < minDistro)) / idles.Count;

                    for (int index = 0; index < steps; index++)
                    {
                        var min = (minDistro + (index * stepDistro));
                        var max = min + stepDistro - 1;
                        var no = Convert.ToDouble(idles.Count(i => i >= min && i <= max));
                        worksheet.Cells[11 + index, colindex].Value = no / idles.Count;
                    }

                    worksheet.Cells[11 + steps, colindex].Value = Convert.ToDouble(idles.Count(i => i >= maxDistro)) / idles.Count;

                    worksheet.Cells[10, colindex, 11 + steps, colindex].Style.Numberformat.Format = "#0.00%";
                    worksheet.Cells[10, colindex, 11 + steps, colindex].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[10, colindex, 11 + steps, colindex].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

                    colindex++;

                    int startrow = 4;
                    for (int i = 0; i < idles.Count(); i++)
                    {
                        var idle = idles[i];
                        worksheet.Cells[startrow + i, colindex].Value = idle;
                    }

                    var cfRule = worksheet.Cells[startrow, colindex, startrow + idles.Count(), colindex].ConditionalFormatting.AddThreeColorScale();

                    cfRule.LowValue.Type = OfficeOpenXml.ConditionalFormatting.eExcelConditionalFormattingValueObjectType.Num;
                    cfRule.LowValue.Value = 4750;
                    cfRule.LowValue.Color = Color.Green;
                    cfRule.MiddleValue.Type = OfficeOpenXml.ConditionalFormatting.eExcelConditionalFormattingValueObjectType.Num;
                    cfRule.MiddleValue.Value = 4900;
                    cfRule.MiddleValue.Color = Color.Yellow;
                    cfRule.HighValue.Type = OfficeOpenXml.ConditionalFormatting.eExcelConditionalFormattingValueObjectType.Num;
                    cfRule.HighValue.Value = 5000;
                    cfRule.HighValue.Color = Color.Red;

                    colindex++;
                }
            }

            var plotModel = new PlotModel();
            plotModel.Title = "Idle ScatterPlot";
            var legend = new Legend();
            legend.LegendTitle = "Legend";
            legend.LegendPosition = LegendPosition.LeftTop;
            plotModel.Legends.Add(legend);

            foreach (var sndaddr in sndaddrs.Where(addr => addr.Equals(90)))
            {
                List<ushort> saps = idleTimes.GetSecondKeys(sndaddr).ToList();
                saps.Sort();

                foreach (var sap in saps)
                {
                    List<double> idles = idleTimes[sndaddr, sap];

                    var scatSeries = new ScatterSeries();
                    scatSeries.Title = "SAP " + sap;
                    plotModel.Series.Add(scatSeries);
                    for (int i = 0; i < idles.Count; i++)
                    {
                        double idle = (double)idles[i];

                        scatSeries.Points.Add(new ScatterPoint(i, idle));
                    }
                }
            }

            return plotModel;
            
        }

        public void Push(CapturePacket packet, Parse parse)
        {

            if (packet.Protocol != ProtocolType.UDP_SPL)
            {
                return;
            }


            ushort recaddr = 0;
            ushort sndaddr = 0;
            ushort dsap = 0;
            bool hasSpan = false;
            double currentSpan = 0;

            foreach (var parsedDataSet in parse.ParsedData)
            {
                if (parsedDataSet.Definition == null)
                {                    
                    continue;
                }


                if (parsedDataSet.Definition.Name == DataSets.VAP.UDP_SPL.Name)
                {

                    recaddr = Convert.ToUInt16(parsedDataSet.ParsedFields[1].Value); // sndaddr
                    sndaddr = Convert.ToUInt16(parsedDataSet.ParsedFields[2].Value); // sndaddr
                    dsap = Convert.ToUInt16(parsedDataSet.ParsedFields[3].Value); // dsap

                    // SPL header means new profibus packet        
                   
                    if (lastReceivedMsg.ContainsKey(sndaddr, dsap))
                    {
                        var beforetime = lastReceivedMsg[sndaddr, dsap];
                        var span = packet.Date - beforetime;

                        currentSpan = span.TotalMilliseconds;
                        hasSpan = true;
                    }
                    else
                        hasSpan = false;

                    lastReceivedMsg[sndaddr, dsap] = packet.Date;


                }
                else if (parsedDataSet.Definition.Name == Subset57.SLLHeader.Name)
                {
                    if (sndaddr == 0 || dsap == 0)
                        throw new ArgumentOutOfRangeException();

                    if (parsedDataSet.ParsedFields[2].Value.ToString() == "Idle")
                    {
                        if (hasSpan && currentSpan > 5d)
                        {


                            if (idleTimes.ContainsKey(sndaddr, dsap))
                                idleTimes[sndaddr, dsap].Add(currentSpan);
                            else
                                idleTimes[sndaddr, dsap] = new List<double> { currentSpan };
                        }
                    }

                }
                else if (parsedDataSet.Definition.Name == Subset57.SLLTimestamp.Name)
                {
                    var refTime = Convert.ToUInt32(parsedDataSet.ParsedFields[0].Value);

                    if (firstDate == default)
                    {
                        firstDate = packet.Date;
                        firstRefTime = refTime;
                    }
                    else
                    {
                        var dateDelta = (packet.Date - firstDate).TotalMilliseconds;
                        var refOffset = (refTime - firstRefTime);
                    }
                }
            }

        }
    }
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

        public int Push(CapturePacket packet, Parse parse)
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
            foreach(var addrow in addRows)
            {
                



                rowindex++;
                worksheet.Cells[rowindex, 2].Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss.000";
                for(int i = 0; i < addrow.Items.Count; i++)
                {
                    worksheet.Cells[rowindex, i + 1].Value = addrow.Items[i].Value;
                }
                news++;
            }

            return news;
        }
    }
    public class XLSMaker
    {
        private bool _finalized = false;
        private ExcelPackage _package;
        private ExcelWorksheet worksheet;
        private ProfiSheet _profiSheet;
        private IdleAnalyser _idleAnalyser;
        private long _rows = 0;
        private int _rotation = 0;

        public XLSMaker(string outputfile, bool exportEverything, bool exportProfibus, bool exportSAPIdleAnalysis)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            Outputfile = outputfile;
            ExportEverything = exportEverything;
            ExportProfibus = exportProfibus;
            ExportSAPIdleAnalysis = exportSAPIdleAnalysis;
            var newFile = new FileInfo(Outputfile);
            if (newFile.Exists)
            {
                newFile.Delete(); // ensures we create a new workbook
                newFile = new FileInfo(Outputfile);
            }
            _package = new ExcelPackage(newFile);

            InitSheets();


        }

        private void InitSheets()
        {
            if (ExportEverything)
            {

                PrepEverythingSheet(_package.Workbook.Worksheets.Add("Packets"));
                
            }

            if (ExportProfibus)
            {
                _profiSheet = new ProfiSheet(_package.Workbook.Worksheets.Add("Profibus"));
                
            }

            if(ExportSAPIdleAnalysis)
            {
                _idleAnalyser = new IdleAnalyser(_package.Workbook.Worksheets.Add("Idle Analysis"));
            }
        }

        public void Push(CapturePacket packet, Parse parse)
        {
            if (_rows > 1000000)
            {
                // row limit reached, need to rotate package
                _rotation++;

                Finalize();
                _finalized = false;
                string pathNoExt = System.IO.Path.ChangeExtension(Outputfile, null);
                FileInfo fileInfo = new FileInfo(pathNoExt + "_" + _rotation + ".xlsx");
                _package = new ExcelPackage(fileInfo);
                InitSheets();
                _rows = 0;
            }


            if (ExportProfibus)
            {
                int v = _profiSheet.Push(packet, parse);
                _rows += v;
            }

            if (ExportSAPIdleAnalysis)
            {
                _idleAnalyser.Push(packet, parse);
            }

            if (ExportEverything)
            {
                PushEverythingSheet(packet, parse);
                _rows++;
            }
        }

        private int rowindex = 2;
        private void PushEverythingSheet(CapturePacket packet, Parse parse)
        {
            if (rowindex > 1000000)
            {
                MessageBox.Show("Row index has reached 1,000,000, aborting");
                return;
            }

            worksheet.Cells[rowindex, 1].Value = packet.Date.ToString("yyyy-MM-dd HH:mm:ss.fff");
            worksheet.Cells[rowindex, 1].Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss.000";
            worksheet.Cells[rowindex, 2].Value = packet.Name;
            var dfCell = worksheet.Cells[rowindex, 3];
            dfCell.IsRichText = true;
            foreach (var df in packet.DisplayFields)
            {
                var richText = dfCell.RichText.Add(df.Name);
                richText.Bold = true;
                if (df.Name == "ERROR")
                    richText.Color = Color.Red;
                else
                    richText.Color = Color.Black;

                var richText2 = dfCell.RichText.Add(": " + df.Val + " ");
                richText2.Bold = false;
                richText2.Color = Color.Black;
            }
            //worksheet.Cells[rowindex, 3].Value = string.Join(" ", packet.DisplayFields.Select(df => df.Name + ": " + df.Val));

            if (packet.Previous != null)
            {
                // TODO temp disabled for now !!!!!
                //worksheet.Cells[rowindex, 4].Value =
                //    string.Join(" ", packet.GetDelta().Select(d => d.Name+": "+d.Value)); 
            }

            var parseCell = worksheet.Cells[rowindex, 5];



            parseCell.IsRichText = true;
            parseCell.Style.WrapText = true;
            foreach (var parsedDataSet in parse.ParsedData)
            {
                parseCell.RichText.Add(parsedDataSet.Name).Bold = true;
                parseCell.RichText.Add(" - ").Bold = false;

                var s = string.Join(" | ", parsedDataSet.ParsedFields.Select(f => new DisplayField(f)));
                parseCell.RichText.Add(s + "\n");
            }


            //if (parse.HasValue)
            //    worksheet.Cells[rowindex, 5].Value =
            //        string.Join(" ",
            //            parse.Value.ParsedData.SelectMany(dataset =>
            //                dataset.ParsedFields.Select(f => new DisplayField(f))));

            //worksheet.Cells[rowindex, 5].IsRichText = true;
            //ExcelRichTextCollection rtfCollection = worksheet.Cells[rowindex, 5].RichText;
            //
            //foreach (KeyValuePair<string, object> pair in dictionaryDataObject.DictionaryData)
            //{
            //    ExcelRichText excelRichText = rtfCollection.Add(pair.Key + ": ");
            //    excelRichText.Bold = false;
            //    excelRichText.UnderLine = false;
            //    excelRichText = rtfCollection.Add(pair.Value.ToString() + ' ');
            //    excelRichText.Bold = true;
            //    excelRichText.UnderLine = true;
            //}

            // RAW
            if (packet.Protocol == ProtocolType.IPTWP)
            {
                // since we have IPT, straight cast to UDP, BAM

                // TODO temp disabled !!!!!
                //var udp = (UdpPacket) packet.Packet.PayloadPacket.PayloadPacket;
                //
                //var bytes = IPTWPPacket.GetIPTPayload(udp, packet.IPTWPPacket);
                //worksheet.Cells[rowindex, 6].Value = BitConverter.ToString(bytes);
            }

            int colIndex = 7;
        }

        private void PrepEverythingSheet(ExcelWorksheet worksheet)
        {
            int colindex = 1;
            //Add the headers
            worksheet.Cells[1, colindex++].Value = "Time";
            worksheet.Cells[1, colindex++].Value = "Name";
            worksheet.Cells[1, colindex++].Value = "Displayfields";
            worksheet.Cells[1, colindex++].Value = "Delta";
            worksheet.Cells[1, colindex++].Value = "Data";
            worksheet.Cells[1, colindex++].Value = "Raw";

            using (ExcelRange range = worksheet.Cells[1, 1, 1, colindex - 1])
            {
                range.Style.Font.Bold = true;
                range.AutoFilter = true;
            }
        }


        public void Finalize()
        {
            if (_finalized)
                return;

            if(ExportProfibus)
            {
                
            }

            if(ExportSAPIdleAnalysis)
            {
                var plotModel = _idleAnalyser.Finalize();

                SvgExporter.Export(plotModel, File.OpenWrite(Path.ChangeExtension(Outputfile, ".svg")), 10000, 10000, true);
            }

            // set some document properties
            _package.Workbook.Properties.Title = "Parsed traffic";
            _package.Workbook.Properties.Author = "Johan Sonesson";
            _package.Workbook.Properties.Comments = "Generated by IPTComShark " +
                                                   System.Reflection.Assembly.GetExecutingAssembly().GetName()
                                                       .Version;

            // set some extended property values
            _package.Workbook.Properties.Company = "";

            foreach (var sheet in _package.Workbook.Worksheets)
            {
                sheet.Calculate();

                sheet.Cells.AutoFitColumns(0);
            }


            // save our new workbook and we are done!
            _package.Save();

            _package.Dispose();
            _finalized = true;
        }

        public string Outputfile { get; }
        public bool ExportEverything { get; }
        public bool ExportProfibus { get; }
        public bool ExportSAPIdleAnalysis { get; }
    }
    internal static class Export
    {
        public static void AnalyseChain(LinkedList<CapturePacket> packets, string outputfile, BackStore.BackStore backStore, ParserFactory parserFactory)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            var newFile = new FileInfo(outputfile);
            if (newFile.Exists)
            {
                newFile.Delete(); // ensures we create a new workbook
                newFile = new FileInfo(outputfile);
            }

            using (var package = new ExcelPackage(newFile))
            {
                // Add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Worksheet1");

                worksheet.Cells[1, 1].Value = packets.First.Value.Name;
                worksheet.Cells[1, 4].Merge = true;

                //Add the headers
                worksheet.Cells[3, 1].Value = "No";
                worksheet.Cells[3, 2].Value = "Time";
                worksheet.Cells[3, 3].Value = "Delta ms";
                worksheet.Cells[3, 4].Value = "Raw";

                int topcol = 4;
                // TODO fix so it uses the whole list
                var payload = backStore.GetPayload(packets.First.Value.No);
                Parse? extractParse = parserFactory.DoPacket(packets.First.Value.Protocol, payload);

                if (extractParse.HasValue)
                {
                    foreach (ParsedField field in extractParse.Value.ParsedData[0].ParsedFields)
                    {
                        worksheet.Cells[3, topcol++].Value = field.Name;
                    }
                }


                //Ok now format the values;
                using (ExcelRange range = worksheet.Cells[3, 1, 3, 4])
                {
                    range.Style.Font.Bold = true;
                    range.AutoFilter = true;
                }

                int rowindex = 4;
                foreach (var packet in packets)
                {
                    worksheet.Cells[rowindex, 1].Value = packet.No;
                    worksheet.Cells[rowindex, 2].Value = packet.Date.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    worksheet.Cells[rowindex, 2].Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss.000";
                    if (packet.Previous != null)
                    {
                        worksheet.Cells[rowindex, 3].Value =
                            packet.Date.Subtract(packet.Previous.Date).TotalMilliseconds;
                        //worksheet.Cells[rowindex, 3].Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss.000";
                    }

                    // TODO temp disabled for now!!!
                    //List<ParsedField> deltaFields = packet.GetDelta();
                    //
                    //int col = 4;
                    //foreach (ParsedField field in packet.ParsedData[0].ParsedFields)
                    //{
                    //    worksheet.Cells[rowindex, col].Value = field.Value;
                    //
                    //    if (deltaFields.Exists(f => f.Name == field.Name))
                    //    {
                    //        worksheet.Cells[rowindex, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //        worksheet.Cells[rowindex, col].Style.Fill.BackgroundColor.SetColor(Color.LightSeaGreen);
                    //    }
                    //
                    //    col++;
                    //}

                    rowindex++;
                }


                //There is actually no need to calculate, Excel will do it for you, but in some cases it might be useful. 
                //For example if you link to this workbook from another workbook or you will open the workbook in a program that hasn't a calculation engine or 
                //you want to use the result of a formula in your program.
                worksheet.Calculate();

                worksheet.Cells.AutoFitColumns(0); //Autofit columns for all cells

                // lets set the header text 
                worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\" Parsed Traffic";
                // add the page number to the footer plus the total number of pages
                worksheet.HeaderFooter.OddFooter.RightAlignedText =
                    $"Page {ExcelHeaderFooter.PageNumber} of {ExcelHeaderFooter.NumberOfPages}";
                // add the sheet name to the footer
                worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;
                // add the file path to the footer
                worksheet.HeaderFooter.OddFooter.LeftAlignedText =
                    ExcelHeaderFooter.FilePath + ExcelHeaderFooter.FileName;

                worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:2"];
                worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:G"];

                // Change the sheet view to show it in page layout mode
                //worksheet.View.PageLayoutView = true;

                // set some document properties
                package.Workbook.Properties.Title = "Parsed traffic";
                package.Workbook.Properties.Author = "Johan Sonesson";
                package.Workbook.Properties.Comments = "Generated by IPTComShark " +
                                                       System.Reflection.Assembly.GetExecutingAssembly().GetName()
                                                           .Version;

                // set some extended property values
                package.Workbook.Properties.Company = "";


                // save our new workbook and we are done!
                package.Save();
            }
        }





        private static string SS27tostring(SS27Packet packet)
        {
            var sb = new StringBuilder(Functions.MakeCommentString(
                packet.Header.GetStringDictionary()));

            if (packet.SubMessage != null)
            {
                sb.Append(Environment.NewLine);
                sb.Append(Functions.MakeCommentString(
                    packet.SubMessage.GetDataDictionary()));
            }

            foreach (var ss27PacketExtraMessage in packet.ExtraMessages)
            {
                sb.Append(Environment.NewLine);
                sb.AppendLine(Functions.MakeCommentString(ss27PacketExtraMessage.GetDataDictionary()));
            }

            return sb.ToString();
        }

        private static List<List<Tuple<string, string>>> SS27tolist(SS27Packet packet)
        {
            var list = new List<List<Tuple<string, string>>>();

            list.Add(packet.Header.ParsedFields.Select(h => new Tuple<string, string>(h.Name, h.Value.ToString()))
                .ToList());
            if (packet.SubMessage != null)
            {
                list.Add(packet.SubMessage.ParsedFields
                    .Select(f => new Tuple<string, string>(f.Name, f.Value.ToString())).ToList());
            }

            foreach (var ss27PacketExtraMessage in packet.ExtraMessages)
            {
                list.Add(ss27PacketExtraMessage.ParsedFields
                    .Select(f => new Tuple<string, string>(f.Name, f.Value.ToString())).ToList());
            }

            return list;
        }

        public static string MakeRTF(List<CapturePacket> packets, BackStore.BackStore backStore, ParserFactory parserFactory)
        {
            var sb = new StringBuilder(
                @"{\rtf1\ansi\ansicpg1252\deff0\deflang2057{\fonttbl{\f0\fnil\fcharset0 Calibri;}}
{\*\generator Msftedit 5.41.21.2510;}\viewkind4\uc1\pard\sa200\sl276\slmult1\lang29\f0\fs20 ");
            sb.AppendLine("\r\n");
            foreach (CapturePacket packet in packets)
            {
                sb.Append(packet.Date);
                sb.Append(@"\tab ");


                sb.Append(packet.Name);
                sb.Append(@" ");

                var payload = backStore.GetPayload(packet.No);
                Parse? parse = parserFactory.DoPacket(packet.Protocol, payload);
                if (parse.HasValue)
                {
                    var displayFields = parse.Value.ParsedData.SelectMany(dataset =>
                        dataset.ParsedFields.Select(f => new DisplayField(f)));
                    foreach (var pair in displayFields)
                    {
                        sb.Append(" ");
                        sb.Append(pair.Name);
                        sb.Append(@" \b ");
                        sb.Append(pair.Val);
                        sb.Append(@"\b0 ");
                    }
                }


                // since we have IPT, straight cast to UDP, BAM
                // TODO temp disabled !!!
                var topPacket = backStore.GetPacket(packet.No);

                var udp = (UdpPacket)topPacket.PayloadPacket.PayloadPacket;

                var bytes = IPTWPPacket.GetIPTPayload(udp.PayloadData);
                sb.Append(@"\line\ul " + BitConverter.ToString(bytes) + @"\ulnone");


                sb.AppendLine(@"\line");
            }

            return sb.ToString();
        }


        public static string MakeCSV(List<CapturePacket> packets, BackStore.BackStore backStore, ParserFactory parserFactory)
        {
            var csvExport = new CsvExport();


            foreach (CapturePacket packet in packets)
            {
                csvExport.AddRow();
                csvExport["Time"] = packet.Date; // + "." + l.Time.Millisecond;
                csvExport["ms"] = packet.Date.Millisecond;


                csvExport["Name"] = packet.Name;
                var payload = backStore.GetPayload(packet.No);
                Parse? parse = parserFactory.DoPacket(packet.Protocol, payload);
                if (parse.HasValue)
                {
                    var displayFields = parse.Value.ParsedData.SelectMany(dataset =>
                        dataset.ParsedFields.Select(f => new DisplayField(f)));
                    csvExport["Data"] = string.Join(" ", displayFields);
                }


                //csvExport["Raw"] = BitConverter.ToString(packet.GetRawData());
            }

            string export = csvExport.Export();
            return export;
        }
    }
}