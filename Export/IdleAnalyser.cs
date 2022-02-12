using IPTComShark.Parsers;
using OfficeOpenXml;
using sonesson_toolsNETSTANDARD.DataSets;
using sonesson_toolsNETSTANDARD.Generic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
}