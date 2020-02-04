﻿using BrightIdeasSoftware;
using sonesson_tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace IPTComShark.Controls
{
    public partial class PacketListView : UserControl
    {
        private readonly List<CapturePacket> _list = new List<CapturePacket>();
        private readonly List<CapturePacket> _listAddBuffer = new List<CapturePacket>();
        private readonly object _listAddLock = new object();
        private CapturePacket _selectedPacket = null;

        private readonly Dictionary<Tuple<uint, IPAddress>, CapturePacket> _lastKnowns =
            new Dictionary<Tuple<uint, IPAddress>, CapturePacket>();

        private static readonly Color TcpColor = Color.FromArgb(231, 230, 255);
        private static readonly Color UdpColor = Color.FromArgb(218, 238, 255);
        private static readonly Color IptwpColor = Color.FromArgb(170, 223, 255);
        private static readonly Color ArpColor = Color.FromArgb(214, 232, 255);
        private static readonly Color ErrorColor = Color.Crimson;

        public PacketListView()
        {
            InitializeComponent();

            if (Properties.Settings.Default.ColumnSettings != null)
            {
                foreach (var cset in Properties.Settings.Default.ColumnSettings)
                {
                    var column = fastObjectListView1.AllColumns.Find(col => col.Text == cset.Name);
                    column.DisplayIndex = cset.DisplayIndex;
                    column.Width = cset.Width;
                    column.IsVisible = cset.IsVisible;
                }

                fastObjectListView1.RebuildColumns();
            }

            olvColumnMS.AspectGetter += rowObject =>
            {
                var packet = (CapturePacket)rowObject;
                //return packet.Date.ToString(CultureInfo.InvariantCulture) + ":" + packet.Date.Millisecond;
                return packet?.Date.Millisecond;
            };

            olvColumnComId.AspectGetter += rowObject =>
            {
                var packet = (CapturePacket)rowObject;
                return packet?.IPTWPPacket?.Comid;
            };

            olvColumnIPTWPType.AspectGetter += rowObject =>
            {
                var packet = (CapturePacket)rowObject;
                return packet?.IPTWPPacket?.IPTWPType;
            };

            fastObjectListView1.ColumnReordered += FastObjectListView1_ColumnReordered;
            fastObjectListView1.ColumnWidthChanged += FastObjectListView1_ColumnWidthChanged;

            fastObjectListView1.FilterMenuBuildStrategy = new MyFilterMenuBuilder();

            olvColumnFrom.ClusterGetter += packets =>
            {
                return StringsToClusters(packets.Where(p => p.Source != null)
                    .Select(p => new IPAddress(p.Source).ToString()));
            };

            olvColumnTo.ClusterGetter += packets =>
            {
                return StringsToClusters(packets.Where(p => p.Source != null)
                    .Select(p => new IPAddress(p.Destination).ToString()));
            };

            olvColumnProtocol.ClusterGetter += packets =>
            {
                var clusters = StringsToClusters(packets.Select(p => p.Protocol.ToString()));
                foreach (var cluster in clusters)
                {
                    cluster.ClusterKey = Enum.Parse(typeof(ProtocolType), cluster.DisplayLabel);
                }

                return clusters;
            };

            olvColumnName.ClusterGetter += packets => { return StringsToClusters(packets.Select(p => p.Name)); };

            olvColumnIPTWPType.ClusterGetter += packets =>
            {
                var clusters = StringsToClusters(packets.Where(p => p.IPTWPPacket != null)
                    .Select(p => p.IPTWPPacket.IPTWPType.ToString()));
                foreach (var cluster in clusters)
                {
                    cluster.ClusterKey = Enum.Parse(typeof(IPTTypes), cluster.DisplayLabel);
                }

                return clusters;

            };

            olvColumnComId.ClusterGetter += packets =>
            {
                var clusters = StringsToClusters(packets.Where(p => p.IPTWPPacket != null)
                    .Select(p => p.IPTWPPacket.Comid.ToString()));
                foreach (var cluster in clusters)
                {
                    cluster.ClusterKey = uint.Parse(cluster.DisplayLabel);
                }

                return clusters;
            };

            //olvColumnDate.ClusteringStrategy = new DateTimeClusteringStrategy(DateTimePortion.Year|DateTimePortion.Month|DateTimePortion.Hour|DateTimePortion.Minute, "yyyy-MM-dd hh:mm");

            //olvColumnDate.ClusterGetter += packets =>
            //{
            //    var cluster = new List<ICluster>();
            //    foreach (var packet in packets)
            //    {
            //        var s = packet.Date.ToString("yyyy-MM-dd hh:mm");
            //        if (cluster.Exists(c => c.DisplayLabel == s))
            //        {
            //            var target = cluster.Find(c => c.DisplayLabel == s);
            //            var clusterKey = (List<DateTime>) target.ClusterKey;
            //            clusterKey.Add(packet.Date);
            //            target.Count++;
            //        }
            //        else
            //        {
            //            cluster.Add(new Cluster(new List<DateTime>() {packet.Date}) {DisplayLabel = s, Count = 1});
            //        }
            //    }
            //
            //    foreach (var c in cluster)
            //    {
            //        var keys = (List<DateTime>)c.ClusterKey;
            //        c.ClusterKey = new object[] { keys};
            //    }
            //
            //    return cluster;
            //};


            fastObjectListView1.RowFormatter += item =>
            {
                if (item.RowObject != null)
                {
                    var packet = (CapturePacket)item.RowObject;
                    if (packet.Error != null)
                        item.BackColor = ErrorColor;
                    else
                        switch (packet.Protocol)
                        {
                            case ProtocolType.IPTWP:
                                item.BackColor = IptwpColor;
                                break;
                            case ProtocolType.ARP:
                                item.BackColor = ArpColor;
                                break;
                            case ProtocolType.TCP:
                                item.BackColor = TcpColor;
                                break;
                            case ProtocolType.JRU:
                                item.BackColor = Color.Orange;
                                break;
                            case ProtocolType.UDP:
                                item.BackColor = UdpColor;
                                break;
                            case ProtocolType.UNKNOWN:
                                item.BackColor = Color.MediumVioletRed;
                                break;
                        }
                }
            };

            olvColumnFrom.AspectGetter += rowObject =>
            {
                var capturePacket = (CapturePacket)rowObject;
                return capturePacket?.Source != null ? new IPAddress(capturePacket.Source).ToString() : null;
            };

            olvColumnTo.AspectGetter += rowObject =>
            {
                var capturePacket = (CapturePacket)rowObject;
                return capturePacket?.Destination != null ? new IPAddress(capturePacket.Destination).ToString() : null;
            };

            olvColumnDictionary.Renderer = new MultiColourTextRenderer();

            UpdateFilter();
        }

        private static List<ICluster> StringsToClusters(IEnumerable<string> strings)
        {
            var dic = new Dictionary<string, int>();
            foreach (var str in strings)
            {
                if (str == null)
                    continue;

                if (dic.ContainsKey(str))
                {
                    dic[str]++;
                }
                else
                    dic.Add(str, 1);
            }

            var list = new List<ICluster>();
            foreach (var pair in dic)
            {
                list.Add(new Cluster(pair.Key) { Count = pair.Value, DisplayLabel = pair.Key });
            }

            return list;
        }

        private void FastObjectListView1_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            SaveColumns();
        }

        private void FastObjectListView1_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            // we have to set it manually to have it available when we call save
            e.Header.DisplayIndex = e.NewDisplayIndex;
            SaveColumns();
        }

        private void SaveColumns()
        {
            var cset = new ColumnSettings();
            foreach (var column in fastObjectListView1.AllColumns)
            {
                cset.Add(new ColumnInfo()
                {
                    DisplayIndex = column.DisplayIndex,
                    Name = column.Text,
                    Width = column.Width,
                    IsVisible = column.IsVisible
                });
            }

            Properties.Settings.Default.ColumnSettings = cset;
            Properties.Settings.Default.Save();
        }

        public void UpdateFilter()
        {
            fastObjectListView1.AdditionalFilter = new ModelFilter(model =>
            {
                var capturePacket = (CapturePacket)model;

                var localhost = IPAddress.Parse("127.0.0.1");

                if (Settings.IgnoreLoopback && capturePacket.Source != null && capturePacket.Destination != null && Equals(new IPAddress(capturePacket.Source), localhost) &&
                    Equals(new IPAddress(capturePacket.Destination), localhost))
                    return false;

                if (Settings.IgnoreUnknownData)
                {
                    if (capturePacket.ParsedData != null || capturePacket.SS27Packet != null)
                    {
                        // we have data
                    }
                    else
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
                    if (capturePacket.IPTWPPacket?.IPTWPType == IPTTypes.PD)
                    {
                        if (capturePacket.Previous?.ParsedData != null && capturePacket.ParsedData != null)
                        {
                            return !capturePacket.Previous.ParsedData.Equals(capturePacket.ParsedData);
                            
                            
                            
                        }
                    }
                }

                return true;
            });
        }

        public PacketListSettings Settings { get; set; } = new PacketListSettings();


        private void fastObjectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fastObjectListView1.SelectedObject == null) return;

            var packet = (CapturePacket)fastObjectListView1.SelectedObject;
            _selectedPacket = packet;
            OnPacketSelected(packet);
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
                    //fastObjectListView1.UpdateObjects(_listAddBuffer);
                    fastObjectListView1.AddObjects(_listAddBuffer);
                    _listAddBuffer.Clear();
                }

                if (Settings.AutoScroll && fastObjectListView1.GetItemCount() > 10)
                    fastObjectListView1.EnsureVisible(fastObjectListView1.GetItemCount() - 1);
            }
        }

        public void Add(CapturePacket o)
        {
            // Connect up the chain
            if (o.IPTWPPacket != null)
            {
                var tupleKey = new Tuple<uint, IPAddress>(o.IPTWPPacket.Comid, new IPAddress(o.Source));
                if (_lastKnowns.ContainsKey(tupleKey))
                {
                    o.Previous = _lastKnowns[tupleKey];
                    _lastKnowns[tupleKey].Next = o;
                    _lastKnowns[tupleKey] = o;

                    
                }
                else
                {
                    _lastKnowns.Add(tupleKey, o);
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
            _lastKnowns.Clear();
            fastObjectListView1.ClearObjects();
            fastObjectListView1.SetObjects(_list);
            GC.Collect();
        }

        public int Count()
        {
            return _list.Count;
        }

        public List<Raw> GetAllRawCaptures()
        {
            lock (_listAddLock)
            {
                return _list.Select(l => l.ReconstructRaw()).ToList();
            }
        }

        public List<CapturePacket> GetAllPackets()
        {
            return _list.ToArray().ToList();
        }

        public List<CapturePacket> GetFilteredPackets()
        {
            return fastObjectListView1.FilteredObjects.Cast<CapturePacket>().ToList();
        }

        private void PacketListView_Load(object sender, EventArgs e)
        {
            Settings.PropertyChanged += (o, args) =>
            {
                if (args.PropertyName != "AutoScroll")
                    UpdateFilter();
            };
        }

        private void copyRawByteshexStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CapturePacket o = (CapturePacket)fastObjectListView1.SelectedObject;
            if (o != null)
            {
                var s = BitConverter.ToString(o.GetRawData());
                Clipboard.SetText(s, TextDataFormat.Text);
            }

            Logger.Log("Hex String copied to ClipBoard", Severity.Info);
        }

        private void copyParsedDatatextStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CapturePacket o = (CapturePacket)fastObjectListView1.SelectedObject;
            if (o != null)
            {
                var s = Functions.MakeCommentString(o.ParsedData.GetDataDictionary());
                Clipboard.SetText(s, TextDataFormat.Text);
            }

            Logger.Log("Parsed data copied to ClipBoard", Severity.Info);
        }

        private void analyzeChainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CapturePacket o = (CapturePacket)fastObjectListView1.SelectedObject;
            if (o != null)
            {
                var linked = new LinkedList<CapturePacket>();
                while (o.Previous != null)
                {
                    o = o.Previous;
                }

                linked.AddFirst(o);

                while (o.Next != null)
                {
                    linked.AddLast(o.Next);
                    o = o.Next;
                }

                var saveFileDialog = new SaveFileDialog { DefaultExt = "xlsx" };
                DialogResult dialogResult = saveFileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    Export.Export.AnalyseChain(linked, saveFileDialog.FileName);
                }
            }
        }

        private void addToIgnoredComIDsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CapturePacket o = (CapturePacket)fastObjectListView1.SelectedObject;
            if (o?.IPTWPPacket != null)
            {
                var s = o.IPTWPPacket.Comid.ToString();
                if (string.IsNullOrEmpty(Settings.IgnoreComid))
                    Settings.IgnoreComid = s;
                else
                    Settings.IgnoreComid += "," + s;

                UpdateFilter();
            }
        }

        private void ContextMenuMouse_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CapturePacket o = (CapturePacket)fastObjectListView1.SelectedObject;
            if (o != null)
            {
                addToIgnoredComIDsToolStripMenuItem.Enabled = o.IPTWPPacket != null;
            }
        }
    }

    public class MyOLVColumn : OLVColumn
    {
        public ClusterGetterDelegate ClusterGetter { get; set; }

        public delegate List<ICluster> ClusterGetterDelegate(IEnumerable<CapturePacket> packets);
    }

    /// <summary>
    /// Empty class to be used in Settings
    /// </summary>
    public class ColumnSettings : List<ColumnInfo>
    {
    }

    public class ColumnInfo
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int DisplayIndex { get; set; }
        public bool IsVisible { get; set; }
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