using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharpPcap.Npcap;

namespace IPTComShark
{
    public partial class InterfacePicker : Form
    {
        private List<NpcapDevice> _captureDevices;

        public InterfacePicker()
        {
            InitializeComponent();
        }

        public InterfacePicker(List<NpcapDevice> captureDevices)
        {
            InitializeComponent();
            _captureDevices = captureDevices;
            dataListView1.DataSource = _captureDevices;

            olvColumn4.AspectGetter += rowObject =>
            {
                var device = (NpcapDevice)rowObject;
                return device.Addresses.Count; 
            };
        }

        public bool PressedYes { get; set; }
        public NpcapDevice SelectedDevice { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            PressedYes = true;
            if (dataListView1.SelectedIndex != -1)
                SelectedDevice = _captureDevices[dataListView1.SelectedIndex];
            Close();
        }

        private void InterfacePicker_Load(object sender, EventArgs e)
        {
            dataListView1.RefreshObjects(_captureDevices);
        }
    }
}