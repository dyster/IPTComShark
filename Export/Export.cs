using OfficeOpenXml;
using sonesson_tools;
using sonesson_tools.BitStreamParser;
using sonesson_tools.Generic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPTComShark.Classes;
using PacketDotNet;
using sonesson_tools.DataParsers;

namespace IPTComShark.Export
{
    internal static class Export
    {
        public static void AnalyseChain(LinkedList<CapturePacket> packets, string outputfile, BackStore backStore)
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
                var extractParse = backStore.GetParse(packets.First.Value.No);

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

        public static void MakeXLSX(List<CapturePacket> packets, string outputfile, BackStore backStore)
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

                //Add the headers
                worksheet.Cells[1, 1].Value = "Time";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Displayfields";
                worksheet.Cells[1, 4].Value = "Delta";
                worksheet.Cells[1, 5].Value = "Data";
                worksheet.Cells[1, 6].Value = "Raw";


                for (var index = 0; index < packets.Count; index++)
                {
                    var packet = packets[index];
                    int rowindex = index + 2;

                    worksheet.Cells[rowindex, 1].Value = packet.Date.ToString("yyyy-MM-dd HH:mm:ss.fff");
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
                    var parse = backStore.GetParse(packet.No);

                    if (parse.HasValue)
                    {
                        parseCell.IsRichText = true;
                        parseCell.Style.WrapText = true;
                        foreach (var parsedDataSet in parse.Value.ParsedData)
                        {
                            parseCell.RichText.Add(parsedDataSet.Name).Bold = true;
                            parseCell.RichText.Add("\n").Bold = false;

                            var s = string.Join("\n", parsedDataSet.ParsedFields.Select(f => new DisplayField(f)));
                            parseCell.RichText.Add(s + "\n");
                        }
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

                    if (packet.Protocol == ProtocolType.IPTWP)
                    {
                        // since we have IPT, straight cast to UDP, BAM

                        // TODO temp disabled !!!!!
                        //var udp = (UdpPacket) packet.Packet.PayloadPacket.PayloadPacket;
                        //
                        //var bytes = IPTWPPacket.GetIPTPayload(udp, packet.IPTWPPacket);
                        //worksheet.Cells[rowindex, 6].Value = BitConverter.ToString(bytes);
                    }
                }

                //Ok now format the values;
                using (ExcelRange range = worksheet.Cells[1, 1, 1, 4])
                {
                    range.Style.Font.Bold = true;
                    //range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    //range.Style.Font.Color.SetColor(Color.White);
                }

                worksheet.Cells[2, 1, packets.Count + 2, 1].Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss.000";


                //Create an autofilter for the range
                worksheet.Cells[1, 1, 1, 4].AutoFilter = true;

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

        public static string MakeRTF(List<CapturePacket> packets, BackStore backStore)
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

                var parse = backStore.GetParse(packet.No);
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

                var udp = (UdpPacket) topPacket.PayloadPacket.PayloadPacket;

                var bytes = IPTWPPacket.GetIPTPayload(udp.PayloadData);
                sb.Append(@"\line\ul " + BitConverter.ToString(bytes) + @"\ulnone");


                sb.AppendLine(@"\line");
            }

            return sb.ToString();
        }


        public static string MakeCSV(List<CapturePacket> packets, BackStore backStore)
        {
            var csvExport = new CsvExport();


            foreach (CapturePacket packet in packets)
            {
                csvExport.AddRow();
                csvExport["Time"] = packet.Date; // + "." + l.Time.Millisecond;
                csvExport["ms"] = packet.Date.Millisecond;


                csvExport["Name"] = packet.Name;
                var parse = backStore.GetParse(packet.No);
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