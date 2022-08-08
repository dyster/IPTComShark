using IPTComShark.Parsers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace IPTComShark.Export
{
    public partial class Exporterer : Form
    {
        private readonly List<CapturePacket> _getAllPackets;
        private readonly List<CapturePacket> _getFilteredPackets;
        private readonly List<CapturePacket> _getSelectedPackets;
        private readonly BackStore.BackStore _backStore;
        private readonly ParserFactory _parserFactory;
        BackgroundWorker _worker;

        public Exporterer(List<CapturePacket> getAllPackets, List<CapturePacket> getFilteredPackets, List<CapturePacket> getSelectedPackets, BackStore.BackStore backStore, ParserFactory parserFactory)
        {
            _getAllPackets = getAllPackets;
            _getFilteredPackets = getFilteredPackets;
            _getSelectedPackets = getSelectedPackets;
            _backStore = backStore;
            _parserFactory = parserFactory;
            InitializeComponent();
            
            radioButtonSelectAll.Text = $"All packets ({getAllPackets.Count:n0})";
            radioButtonSelectFilter.Text = $"Filtered packets ({getFilteredPackets.Count:n0})";
            radioButtonSelectSelected.Text = $"Selected packets ({getSelectedPackets.Count:n0})";
        }

        public List<CapturePacket> Selection { get; set; }

        public bool ExportProfibus { get; set; }
        public bool ExportSAPIdleAnalysis { get; private set; }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (_worker != null && _worker.IsBusy)
                return;

            string[] sourceFiles = null;

            if (radioButtonSelectFile.Checked)
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select source for export";
                openFileDialog.Multiselect = true;
                var showDialog = openFileDialog.ShowDialog(this);
                if (showDialog == DialogResult.OK)
                {
                    sourceFiles = openFileDialog.FileNames;
                }
            }


                var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "xlsx",
                Title = "Select export filename"
            };
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            _worker = new BackgroundWorker();
            _worker.DoWork += (object sender, DoWorkEventArgs e) => { DOIT(saveFileDialog.FileName, sourceFiles); };
            _worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) => { ExportFinished(); };
            
            buttonOK.Enabled = false;
            progressBar1.Value = 0;

            _worker.RunWorkerAsync();

        }


        private delegate void FinishDelegate();
        private delegate void ProgressDelegate(int progress);

        private void ExportFinished()
        {
            if (this.InvokeRequired)
                this.Invoke(new FinishDelegate(ExportFinished));
            else
            {
                buttonOK.Enabled = true;
                progressBar1.Value = 0;
            }
        }

        private void ProgressUpdate(int progress)
        {
            if(this.InvokeRequired)
                this.Invoke(new ProgressDelegate(ProgressUpdate), progress);
            else
            {
                progressBar1.Value = progress;
            }
        }

        private void DOIT(string fileName, string[] sourceFiles)
        {
            BackStore.BackStore activeStore = _backStore;
            ExportEverything = checkBoxEverything.Checked;
            ExportProfibus = checkBoxProfibus.Checked;
            ExportSAPIdleAnalysis = checkBoxSAPIdle.Checked;

            

            if (radioButtonSelectAll.Checked)
                Selection = _getAllPackets;
            else if (radioButtonSelectFilter.Checked)
                Selection = _getFilteredPackets;
            else if (radioButtonSelectSelected.Checked)
                Selection = _getSelectedPackets;
            else if (radioButtonSelectFile.Checked)
            {
                
                if (sourceFiles != null)
                {                    
                    var opener = new FileManager.FileOpener(sourceFiles);
                    var dialogresult = opener.ShowDialog();

                    if (dialogresult != DialogResult.OK)
                        return;

                    var backStore = new BackStore.BackStore(_parserFactory, true);
                    backStore.ProcessingFilters = opener.ProcessingFilters;
                    Selection = new List<CapturePacket>();

                    XLSMaker xLSMaker = new XLSMaker(fileName, ExportEverything, ExportProfibus, ExportSAPIdleAnalysis);
                    int count = 0;
                    int total = opener.DataSources.Sum(d => d.Packets);

                    var fileManager = new FileManager.FileManager();
                    fileManager.FilterFrom = opener.DateTimeFrom;
                    fileManager.FilterTo = opener.DateTimeTo;
                    fileManager.ProcessingFilters = opener.ProcessingFilters;

                    foreach (var raw in fileManager.EnumerateFiles(opener.DataSources))
                    {
                        CapturePacket capturePacket = backStore.Add(raw, out var parse);
                        xLSMaker.Push(capturePacket, parse);
                        count++;
                        DoProgress(count, total);
                    }
                    xLSMaker.Finalize();

                }
                else
                {
                    return;
                }

                return;
            }

            XLSMaker xLSMaker2 = new XLSMaker(fileName, ExportEverything, ExportProfibus, ExportSAPIdleAnalysis);
            int i = 0;
            foreach (var packet in Selection)
            {
                var payload = _backStore.GetPayload(packet.No);
                Parsers.Parse? parse = _parserFactory.DoPacket(packet.Protocol, payload, packet);
                if (parse.HasValue)
                    xLSMaker2.Push(packet, parse.Value);
                i++;

                DoProgress(i, Selection.Count);
            }
            xLSMaker2.Finalize();
        }

        private int lastProgress = 0;
        private void DoProgress(int i, int total)
        {
            var perc = i * 100 / total;
            if(perc != lastProgress)
            {
                ProgressUpdate(perc);
                lastProgress = perc;
            }
        }

        public bool ExportEverything { get; set; }
    }
}
