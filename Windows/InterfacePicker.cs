using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharpPcap.WinPcap;

namespace IPTComShark
{
    public partial class InterfacePicker : Form
    {
        private List<WinPcapDevice> _captureDevices;

        public InterfacePicker()
        {
            InitializeComponent();
        }

        public InterfacePicker(List<WinPcapDevice> captureDevices)
        {
            InitializeComponent();
            _captureDevices = captureDevices;
            dataListView1.DataSource = _captureDevices;
        }

        public bool PressedYes { get; set; }
        public WinPcapDevice SelectedDevice { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            PressedYes = true;
            if (dataListView1.SelectedIndex != -1)
                SelectedDevice = _captureDevices[dataListView1.SelectedIndex];
            Close();
        }
    }
}