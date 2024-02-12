using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TrainShark
{
    public partial class InterfacePicker : Form
    {
        private List<NetworkCard> _captureDevices;

        public InterfacePicker()
        {
            InitializeComponent();
        }

        public InterfacePicker(List<PcapDevice> captureDevices)
        {
            InitializeComponent();
            _captureDevices = captureDevices.Select(d => new NetworkCard(d)).ToList();
        }

        public bool PressedYes { get; set; }
        public PcapDevice SelectedDevice { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            PressedYes = true;
            if (dataListView1.SelectedIndex != -1)
                SelectedDevice = _captureDevices[dataListView1.SelectedIndex].NpcapDevice;
            Close();
        }

        private void InterfacePicker_Load(object sender, EventArgs e)
        {
            dataListView1.DataSource = _captureDevices;
            //dataListView1.RefreshObjects(_captureDevices);
        }
    }

    public class NetworkCard
    {
        private readonly PcapDevice _device;

        public NetworkCard(PcapDevice device)
        {
            this._device = device;
        }

        public string FriendlyName => _device.Interface.FriendlyName;
        public string Description => _device.Description;

        public string Name => _device.Name;

        public int Addresses => _device.Interface.Addresses.Count;

        // TODO restore this? does it exist anymore in sharpcap?
        //public bool Loopback => _device.Loopback;

        public PcapDevice NpcapDevice => _device;
    }
}