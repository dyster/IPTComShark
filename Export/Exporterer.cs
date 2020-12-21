using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sonesson_tools;

namespace IPTComShark.Export
{
    public partial class Exporterer : Form
    {
        private readonly List<CapturePacket> _getAllPackets;
        private readonly List<CapturePacket> _getFilteredPackets;
        private readonly List<CapturePacket> _getSelectedPackets;

        public Exporterer(List<CapturePacket> getAllPackets, List<CapturePacket> getFilteredPackets, List<CapturePacket> getSelectedPackets)
        {
            _getAllPackets = getAllPackets;
            _getFilteredPackets = getFilteredPackets;
            _getSelectedPackets = getSelectedPackets;
            InitializeComponent();
            
            radioButtonSelectAll.Text = $"All packets ({getAllPackets.Count:n0})";
            radioButtonSelectFilter.Text = $"Filtered packets ({getFilteredPackets.Count:n0})";
            radioButtonSelectSelected.Text = $"Selected packets ({getSelectedPackets.Count:n0})";
        }

        public List<CapturePacket> Selection { get; set; }

        public bool Profibus { get; set; }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (radioButtonSelectAll.Checked)
                Selection = _getAllPackets;
            else if (radioButtonSelectFilter.Checked)
                Selection = _getFilteredPackets;
            else if (radioButtonSelectSelected.Checked)
                Selection = _getSelectedPackets;

            Profibus = checkBoxProfibus.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
