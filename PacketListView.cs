using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SharpPcap;

namespace IPTComShark
{
    public partial class PacketListView : UserControl
    {
        private readonly List<CapturePacket> _list = new List<CapturePacket>();
        private readonly List<CapturePacket> _listAddBuffer = new List<CapturePacket>();
        private readonly object _listAddLock = new object();

        public PacketListView()
        {
            InitializeComponent();

            olvColumnDate.AspectGetter += rowObject =>
            {
                if (rowObject != null)
                {
                    var packet = (CapturePacket) rowObject;
                    return packet.Date.ToString() + ":" + packet.Date.Millisecond;
                }
                return null;
            };

            olvColumnDictionary.Renderer = new MultiColourTextRenderer();
        }

        public new bool AutoScroll { get; set; } = true;


        private void fastObjectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fastObjectListView1.SelectedObject != null)
            {
                if (!(fastObjectListView1.SelectedObject is IPTWPPacket))
                    return;

                var packet = (IPTWPPacket) fastObjectListView1.SelectedObject;

                OnPacketSelected(packet);
            }
        }

        public event EventHandler<PacketSelectedEventArgs> PacketSelected;

        protected virtual void OnPacketSelected(IPTWPPacket packet)
        {
            var e = new PacketSelectedEventArgs(packet);
            PacketSelected?.Invoke(this, e);
        }

        private void timerAddBuffer_Tick(object sender, EventArgs e)
        {
            if (_listAddBuffer.Count > 0)
            {
                lock (_listAddLock)
                {
                    _list.AddRange(_listAddBuffer);
                    fastObjectListView1.UpdateObjects(_listAddBuffer);
                    _listAddBuffer.Clear();
                }
                if (AutoScroll)
                    fastObjectListView1.EnsureVisible(fastObjectListView1.GetItemCount() - 1);
            }
        }

        public void Add(CapturePacket o)
        {
            lock (_listAddLock)
            {
                _listAddBuffer.Add(o);
            }
        }

        public void Clear()
        {
            lock (_listAddLock)
            {
                _listAddBuffer.Clear();
            }
            _list.Clear();
            fastObjectListView1.Objects = _list;
        }

        public int Count()
        {
            return _list.Count;
        }

        public List<RawCapture> GetRawCaptures()
        {
            return _list.Select(l => l.RawCapture).ToList();
        }
    }

    public class PacketSelectedEventArgs : EventArgs
    {
        public PacketSelectedEventArgs(IPTWPPacket packet)
        {
            Packet = packet;
        }

        public IPTWPPacket Packet { get; }
    }
}