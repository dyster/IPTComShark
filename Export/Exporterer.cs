using IPTComShark.Parsers;
using System;
using System.Collections.Generic;
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
            BackStore.BackStore activeStore = _backStore;
            ExportEverything = checkBoxEverything.Checked;
            ExportProfibus = checkBoxProfibus.Checked;
            ExportSAPIdleAnalysis = checkBoxSAPIdle.Checked;

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

            if (radioButtonSelectAll.Checked)
                Selection = _getAllPackets;
            else if (radioButtonSelectFilter.Checked)
                Selection = _getFilteredPackets;
            else if (radioButtonSelectSelected.Checked)
                Selection = _getSelectedPackets;
            else if (radioButtonSelectFile.Checked)
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select source for export";
                openFileDialog.Multiselect = true;
                var showDialog = openFileDialog.ShowDialog(this);
                if (showDialog == DialogResult.OK)
                {
                    FileManager.FileManager fileManager = new FileManager.FileManager();
                    var fileRaws = fileManager.OpenFiles(openFileDialog.FileNames);
                    var backStore = new BackStore.BackStore(_parserFactory, true);
                    backStore.ProcessingFilters = fileManager.ProcessingFilters;
                    Selection = new List<CapturePacket>();

                    XLSMaker xLSMaker = new XLSMaker(saveFileDialog.FileName, ExportEverything, ExportProfibus, ExportSAPIdleAnalysis);
                    foreach (var raw in fileRaws)
                    {
                        CapturePacket capturePacket = backStore.Add(raw, out var parse);
                        xLSMaker.Push(capturePacket, parse);

                    }
                    xLSMaker.Finalize();
                                        
                }
                else
                {
                    return;
                }

                return;
            }

            XLSMaker xLSMaker2 = new XLSMaker(saveFileDialog.FileName, ExportEverything, ExportProfibus, ExportSAPIdleAnalysis);
            foreach(var packet in Selection)
            {
                var payload = _backStore.GetPayload(packet.No);
                Parsers.Parse? parse = _parserFactory.DoPacket(packet.Protocol, payload);
                if(parse.HasValue)
                    xLSMaker2.Push(packet, parse.Value);
            }
            xLSMaker2.Finalize();
                        
        }

        public bool ExportEverything { get; set; }
    }
}
