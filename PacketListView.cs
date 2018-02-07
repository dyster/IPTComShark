using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using BrightIdeasSoftware;
using sonesson_tools;
using SharpPcap;

namespace IPTComShark
{
    public partial class PacketListView : UserControl
    {
        private readonly List<CapturePacket> _list = new List<CapturePacket>();
        private readonly List<CapturePacket> _listAddBuffer = new List<CapturePacket>();
        private readonly object _listAddLock = new object();
        private readonly Dictionary<uint, CapturePacket> _lastKnowns = new Dictionary<uint, CapturePacket>();

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

            olvColumnDictionary.AspectGetter += rowObject =>
            {
                var capturePacket = (CapturePacket) rowObject;
                if (capturePacket?.ParsedData == null)
                    return "";
                return capturePacket.ParsedData;
            };

            olvColumnDictionary.Renderer = new MultiColourTextRenderer();

            UpdateFilter();
            
            
        }

        public void UpdateFilter()
        {
            fastObjectListView1.ModelFilter = new ModelFilter(model =>
            {
                var capturePacket = (CapturePacket)model;

                if (Settings.IgnoreLoopback && capturePacket.IPv4Packet.SourceAddress.ToString() == "127.0.0.1" &&
                    capturePacket.IPv4Packet.DestinationAddress.ToString() == "127.0.0.1")
                    return false;

                if (Settings.IgnoreUnknownData)
                {
                    if (capturePacket.ParsedData == null)
                        return false;
                }

                if (!string.IsNullOrEmpty(Settings.IgnoreComid))
                {
                    string[] strings = Settings.IgnoreComid.Split(',');
                    foreach (string s in strings)
                    {
                        uint u = uint.Parse(s.Trim());
                        if (capturePacket.IPTWPPacket != null && capturePacket.IPTWPPacket.Comid == u)
                            return false;
                    }
                }

                if (Settings.IgnoreDuplicatedPD)
                {
                    if (capturePacket.Previous?.ParsedData != null && capturePacket.ParsedData != null)
                    {
                        var before = capturePacket.Previous.ParsedData.GetStringDictionary();
                        var now = capturePacket.ParsedData.GetStringDictionary();

                        if (before.Count != now.Count)
                            return true;

                        foreach (KeyValuePair<string, string> pair in now)
                        {
                            if (before.ContainsKey(pair.Key))
                            {
                                if (pair.Value != before[pair.Key])
                                    return true;
                            }
                            else
                            {
                                return true;
                            }

                            return false;
                        }
                    }
                }

                return true;
            });
        }

        public PacketListSettings Settings { get; set; } = new PacketListSettings();


        private void fastObjectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fastObjectListView1.SelectedObject != null)
            {
               

                var packet = (CapturePacket) fastObjectListView1.SelectedObject;

                OnPacketSelected(packet);
            }
        }

        public event EventHandler<PacketSelectedEventArgs> PacketSelected;

        protected virtual void OnPacketSelected(CapturePacket packet)
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
                if (AutoScroll && fastObjectListView1.GetItemCount() > 10)
                    fastObjectListView1.EnsureVisible(fastObjectListView1.GetItemCount() - 1);
            }
        }

        public void Add(CapturePacket o)
        {
            // Connect up the chain
            if (o.IPTWPPacket != null)
            {
                var comid = o.IPTWPPacket.Comid;
                if (_lastKnowns.ContainsKey(comid))
                {
                    o.Previous = _lastKnowns[comid];
                    _lastKnowns[comid].Next = o;
                    _lastKnowns[comid] = o;
                }
                else
                {
                    _lastKnowns.Add(comid, o);
                }
            }
            

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

        public List<Raw> GetAllRawCaptures()
        {
            lock (_listAddLock)
            {
                return _list.Select(l => l.RawCapture).ToList();
            }
        }

        private void PacketListView_Load(object sender, EventArgs e)
        {
            Settings.PropertyChanged += (o, args) =>
            {
                UpdateFilter();
            };
        }
    }

    public class PacketListSettings : INotifyPropertyChanged
    {
        private bool _ignoreLoopback = true;
        private bool _autoScroll = true;
        private bool _ignoreDupePd = true;
        private bool _ignoreUnknown = true;
        private string _ignoreComid;

        public string IgnoreComid
        {
            get => _ignoreComid;
            set
            {
                _ignoreComid = value; 
                OnPropertyChanged();
            }
        }

        public bool AutoScroll
        {
            get => _autoScroll;
            set
            {
                _autoScroll = value;
                OnPropertyChanged();
            }
        }

        public bool IgnoreUnknownData
        {
            get => _ignoreUnknown;
            set
            {
                _ignoreUnknown = value;
                OnPropertyChanged();
            }
        }

        public bool IgnoreDuplicatedPD
        {
            get => _ignoreDupePd;
            set
            {
                _ignoreDupePd = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Ignore any loopback traffic (localhost to localhost)
        /// </summary>
        public bool IgnoreLoopback
        {
            get => _ignoreLoopback;
            set
            {
                _ignoreLoopback = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class PacketSelectedEventArgs : EventArgs
    {
        public PacketSelectedEventArgs(CapturePacket packet)
        {
            Packet = packet;
        }

        public CapturePacket Packet { get; }
    }
}