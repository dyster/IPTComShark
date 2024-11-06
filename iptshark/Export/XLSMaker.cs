using OfficeOpenXml;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TrainShark.Classes;
using TrainShark.Parsers;

namespace TrainShark.Export
{
    public class XLSMaker
    {
        private bool _finalized = false;
        private ExcelPackage _package;
        private ProfiSheet _profiSheet;        
        private ExcelWorksheet worksheet;
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
                worksheet = _package.Workbook.Worksheets.Add("Packets");
                PrepEverythingSheet(worksheet);
            }

            if (ExportProfibus)
            {
                _profiSheet = new ProfiSheet(_package.Workbook.Worksheets.Add("Profibus"));
            }

            //if (ExportSAPIdleAnalysis)
            //{
            //    _idleAnalyser = new IdleAnalyser(_package.Workbook.Worksheets.Add("Idle Analysis"));
            //}
        }

        public void Push(CapturePacket packet, ParseOutput parse)
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

            //if (ExportSAPIdleAnalysis)
            //{
            //    _idleAnalyser.Push(packet, parse);
            //}

            if (ExportEverything)
            {
                PushEverythingSheet(packet, parse);
                _rows++;
            }
        }

        private int rowindex = 2;

        private void PushEverythingSheet(CapturePacket packet, ParseOutput parse)
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
            if (parse.NoParserInstalled)
            {
                parseCell.Value = "No parser installed";
            }
            else
            {
                foreach (var parsedDataSet in parse.ParsedData)
                {
                    var clean = Conversions.RemoveInvalidXMLChars(parsedDataSet.Name);

                    var richbit = parseCell.RichText.Add(clean).Bold = true;

                    parseCell.RichText.Add(" - ").Bold = false;

                    var s = string.Join(" | ", parsedDataSet.ParsedFields.Select(f => new DisplayField(f)));
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
            rowindex++;
        }

        private void PrepEverythingSheet(ExcelWorksheet worksheet)
        {
            int colindex = 0;
            //Add the headers
            worksheet.Cells[1, ++colindex].Value = "Time";
            worksheet.Columns[colindex].Width = 23;
            worksheet.Cells[1, ++colindex].Value = "Name";
            worksheet.Columns[colindex].Width = 25;
            worksheet.Cells[1, ++colindex].Value = "Displayfields";
            worksheet.Columns[colindex].Width = 100;
            worksheet.Cells[1, ++colindex].Value = "Delta";
            worksheet.Columns[colindex].Width = 25;
            worksheet.Cells[1, ++colindex].Value = "Data";
            worksheet.Columns[colindex].Width = 100;
            worksheet.Cells[1, ++colindex].Value = "Raw";
            worksheet.Columns[colindex].Width = 25;

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

            if (ExportProfibus)
            {
            }

            //if (ExportSAPIdleAnalysis)
            //{
            //    var plotModel = _idleAnalyser.Finalize();
            //
            //    SvgExporter.Export(plotModel, File.OpenWrite(Path.ChangeExtension(Outputfile, ".svg")), 10000, 10000, true);
            //}

            // set some document properties
            _package.Workbook.Properties.Title = "Parsed traffic";
            _package.Workbook.Properties.Author = "TrainShark";
            _package.Workbook.Properties.Comments = "Generated by TrainShark " +
                                                   System.Reflection.Assembly.GetExecutingAssembly().GetName()
                                                       .Version;

            // set some extended property values
            _package.Workbook.Properties.Company = "";

            foreach (var sheet in _package.Workbook.Worksheets)
            {
                sheet.Calculate();

                //sheet.Cells.AutoFitColumns(0);
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
}