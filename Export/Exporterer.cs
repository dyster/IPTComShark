using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IPTComShark.Classes;
using sonesson_tools;

namespace IPTComShark.Export
{
    public partial class Exporterer : Form
    {
        private readonly List<CapturePacket> _getAllPackets;
        private readonly List<CapturePacket> _getFilteredPackets;
        private readonly List<CapturePacket> _getSelectedPackets;
        private readonly BackStore _backStore;

        public Exporterer(List<CapturePacket> getAllPackets, List<CapturePacket> getFilteredPackets, List<CapturePacket> getSelectedPackets, BackStore backStore)
        {
            _getAllPackets = getAllPackets;
            _getFilteredPackets = getFilteredPackets;
            _getSelectedPackets = getSelectedPackets;
            _backStore = backStore;
            InitializeComponent();
            
            radioButtonSelectAll.Text = $"All packets ({getAllPackets.Count:n0})";
            radioButtonSelectFilter.Text = $"Filtered packets ({getFilteredPackets.Count:n0})";
            radioButtonSelectSelected.Text = $"Selected packets ({getSelectedPackets.Count:n0})";
        }

        public List<CapturePacket> Selection { get; set; }

        public bool ExportProfibus { get; set; }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            BackStore activeStore = _backStore;

            if (radioButtonSelectAll.Checked)
                Selection = _getAllPackets;
            else if (radioButtonSelectFilter.Checked)
                Selection = _getFilteredPackets;
            else if (radioButtonSelectSelected.Checked)
                Selection = _getSelectedPackets;
            else if (radioButtonSelectFile.Checked)
            {
                var openFileDialog = new OpenFileDialog();
                var showDialog = openFileDialog.ShowDialog(this);
                if (showDialog == DialogResult.OK)
                {
                    var openFiles = new FileManager.FileManager().OpenFiles(openFileDialog.FileNames);
                    var backStore = new BackStore();
                    Selection = new List<CapturePacket>();
                    foreach (var openFile in openFiles)
                        Selection.Add(backStore.Add(openFile, out var parsedlist));

                    activeStore = backStore;
                }
                else
                {
                    return;
                }
                
            }

            ExportEverything = checkBoxEverything.Checked;
            ExportProfibus = checkBoxProfibus.Checked;

            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "xlsx"
            };
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                IPTComShark.Export.Export.MakeXLSX(Selection, saveFileDialog.FileName, activeStore, ExportEverything, ExportProfibus);
            }
            else
            {
                
            }
        }

        public bool ExportEverything { get; set; }
    }
}
