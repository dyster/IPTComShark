using BrightIdeasSoftware;
using IPTComShark.Export;
using IPTComShark.Parsers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace IPTComShark.Controls
{
    public partial class PacketListView : UserControl
    {
        private readonly List<CapturePacket> _list = new List<CapturePacket>(500000);
        private readonly List<CapturePacket> _listAddBuffer = new List<CapturePacket>();
        private readonly object _listAddLock = new object();
        private CapturePacket _selectedPacket;
        private bool _loaded = false;

        private string _searchString;

        private static readonly Color TcpColor = Color.FromArgb(231, 230, 255);
        private static readonly Color UdpColor = Color.FromArgb(218, 238, 255);
        private static readonly Color IptwpColor = Color.FromArgb(170, 223, 255);
        private static readonly Color ArpColor = Color.FromArgb(214, 232, 255);
        private static readonly Color ErrorColor = Color.Crimson;

        private static readonly IPAddress localhost = IPAddress.Parse("127.0.0.1");
        private const string EmptyText = "No files loaded, use File->Open or Drag&Drop";
        private const string EmptyFilterText = "The filter has excluded everything";
        private string _lastBackStoreStatus = "";
        private Font _statusFont = new Font("Tahoma", 20, FontStyle.Bold);
        private PacketListSettings _settings = new PacketListSettings();

        public string SearchString
        {
            get => _searchString;
            set
            {
                if (value == null)
                    return;
                var input = value.Trim();
                if (input.Equals(_searchString))
                    return;
                _searchString = input;
                UpdateFilter();
            }
        }

        public PacketListView()
        {
            InitializeComponent();





            olvColumnNo.AspectGetter += rowObject =>
            {
                var packet = (CapturePacket)rowObject;
                return packet?.No;
            };

            olvColumnDate.AspectGetter += rowObject =>
            {
                var packet = (CapturePacket)rowObject;
                return packet?.Date.ToString();
            };

            olvColumnMS.AspectGetter += rowObject =>
            {
                var packet = (CapturePacket)rowObject;
                //return packet.Date.ToString(CultureInfo.InvariantCulture) + ":" + packet.Date.Millisecond;
                return packet?.Date.Millisecond;
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

            olvColumnProtocol.AspectGetter += rowObject =>
            {
                var capturePacket = (CapturePacket)rowObject;
                return capturePacket?.Protocol;
            };

            olvColumnProtocolInfo.AspectGetter += rowObject =>
            {
                var capturePacket = (CapturePacket)rowObject;
                return capturePacket?.ProtocolInfo;
            };

            olvColumnComId.AspectGetter += rowObject =>
            {
                if (rowObject == null)
                    return null;

                var packet = (CapturePacket)rowObject;
                if (packet.Protocol == ProtocolType.IPTWP)
                    return packet.Comid;

                return null;
            };

            olvColumnIPTWPType.AspectGetter += rowObject =>
            {
                var packet = (CapturePacket)rowObject;
                if (packet != null && packet.IPTWPType.HasValue)
                    return packet.IPTWPType.Value;
                return null;
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
                var clusters = StringsToClusters(packets.Where(p => p.Protocol == ProtocolType.IPTWP)
                    .Select(p => p.IPTWPType.ToString()));
                foreach (var cluster in clusters)
                {
                    cluster.ClusterKey = Enum.Parse(typeof(IPTTypes), cluster.DisplayLabel);
                }

                return clusters;
            };

            olvColumnComId.ClusterGetter += packets =>
            {
                var clusters = StringsToClusters(packets.Where(p => p.Protocol == ProtocolType.IPTWP)
                    .Select(p => p.Comid.ToString()));
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



            olvColumnDictionary.Renderer = new MultiColourTextRenderer();

            UpdateFilter();



            fastObjectListView1.OverlayText = new TextOverlay() { Text = "If you can read this, the universe has come apart" };
        }

        public BackStore.BackStore BackStore { get; set; }
        public ParserFactory ParserFactory { get; set; }

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
            if (_loaded)
                SaveColumns();
        }

        private void FastObjectListView1_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            // we have to set it manually to have it available when we call save
            e.Header.DisplayIndex = e.NewDisplayIndex;
            SaveColumns();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.PacketListSettings = Settings.SerialiseToString();
            Properties.Settings.Default.Save();
        }

        private void SaveColumns()
        {
            var cset = new List<ColumnInfo>();
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

            Settings.ColumnInfos = cset;


            SaveSettings();
        }

        public void UpdateFilter()
        {
            var ignoreComids = new List<uint>();
            if (!string.IsNullOrEmpty(Settings.IgnoreComid))
            {
                List<string> strings = Settings.IgnoreComid.Split(',').ToList();

                foreach (string s in strings.Distinct())
                {
                    if (uint.TryParse(s.Trim(), out uint u))
                    {
                        ignoreComids.Add(u);
                    }
                }
            }

            Regex searchRegex = null;
            if (!string.IsNullOrEmpty(_searchString))
            {
                searchRegex = new Regex(Regex.Escape(_searchString), RegexOptions.IgnoreCase);
            }

            fastObjectListView1.AdditionalFilter = new ModelFilter(model =>
            {
                var capturePacket = (CapturePacket)model;


                if (Settings.IgnoreLoopback && capturePacket.Source != null && capturePacket.Destination != null &&
                    Equals(new IPAddress(capturePacket.Source), localhost) &&
                    Equals(new IPAddress(capturePacket.Destination), localhost))
                    return false;

                if (Settings.IgnoreUnknownData)
                {
                    if (capturePacket.HasData)
                    {
                        // we have data
                    }
                    else
                        return false;
                }

                foreach (var comid in ignoreComids)
                {
                    if (capturePacket.Comid == comid)
                        return false;
                }

                if (Settings.IgnoreDuplicatedPD)
                {
                    if (capturePacket.IPTWPType != null && capturePacket.IPTWPType == IPTTypes.PD)
                    {
                        // TODO can we check this directly without doing the check above?
                        if (capturePacket.IsDupe)
                            return false;
                    }
                }

                if (this.Settings.IgnoreVariables.Length > 0)
                {
                    // this block does not filter out entire packets, but it is the most efficient place to refilter the displayfields for the text renderer to only work on a subset after the basic exclusions have been made

                    foreach (var field in capturePacket.DisplayFields)
                    {
                        field.Display = !Settings.IgnoreVariables.Contains(field.Name);
                    }

                }

                if (searchRegex != null)
                {
                    if (capturePacket.Name != null && searchRegex.IsMatch(capturePacket.Name))
                        return true;
                    if (capturePacket.DisplayFields.Exists(t => searchRegex.IsMatch(t.Name)))
                        return true;

                    // TODO we still need to be able to filter on the whole dataset somehow, either generate some massive database or maybe use the dataset definitions instead of the parsed result

                    return false;
                }

                return true;
            });
        }

        public PacketListSettings Settings
        {
            get => _settings; set
            {
                _settings = value;

                if (_settings.ColumnInfos != null && _settings.ColumnInfos.Count > 0)
                {
                    foreach (var cset in _settings.ColumnInfos)
                    {
                        var column = fastObjectListView1.AllColumns.Find(col => col.Text == cset.Name);
                        column.DisplayIndex = cset.DisplayIndex;
                        column.Width = cset.Width;
                        column.IsVisible = cset.IsVisible;
                    }

                    fastObjectListView1.RebuildColumns();
                }

                _settings.PropertyChanged += _settings_PropertyChanged;
            }
        }

        private void _settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateFilter();
        }

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
            lock (_listAddLock)
            {
                _listAddBuffer.Add(o);
            }
        }

        public void AddRange(CapturePacket[] packets)
        {
            lock (_listAddLock)
            {
                _listAddBuffer.AddRange(packets);
            }
        }

        public void Clear()
        {
            lock (_listAddLock)
            {
                _listAddBuffer.Clear();
            }

            _list.Clear();

            _selectedPacket = null;
            fastObjectListView1.ClearObjects();
            fastObjectListView1.SetObjects(_list);
        }

        public int Count()
        {
            return _list.Count;
        }

        public List<CapturePacket> GetAllPackets()
        {
            return _list.ToArray().ToList();
        }

        public List<CapturePacket> GetFilteredPackets()
        {
            return fastObjectListView1.FilteredObjects.Cast<CapturePacket>().ToList();
        }

        public List<CapturePacket> GetSelectedPackets()
        {
            return fastObjectListView1.SelectedObjects.Cast<CapturePacket>().ToList();
        }

        private void PacketListView_Load(object sender, EventArgs e)
        {
            timerFlicker.Enabled = true;
            _loaded = true;
        }

        private void copyRawByteshexStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CapturePacket o = (CapturePacket)fastObjectListView1.SelectedObject;
            if (o != null)
            {
                var s = BitConverter.ToString(BackStore.GetRaw(o.No).RawData);
                Clipboard.SetText(s, TextDataFormat.Text);
            }

            Logger.Log("Hex String copied to ClipBoard", Severity.Info);
        }

        private void copyParsedDatatextStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CapturePacket o = (CapturePacket)fastObjectListView1.SelectedObject;
            if (o != null)
            {
                var payload = BackStore.GetPayload(o.No);
                Parse? parse = ParserFactory.DoPacket(o.Protocol, payload, o);
                if (parse.HasValue)
                {
                    var list = new List<DisplayField>();
                    foreach (var parsedDataSet in parse.Value.ParsedData)
                    {
                        list.AddRange(parsedDataSet.ParsedFields.Select(f => new DisplayField(f)));
                    }

                    var s = string.Join(" ", list);
                    Clipboard.SetText(s, TextDataFormat.Text);
                }
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
                    Export.Export.AnalyseChain(linked, saveFileDialog.FileName, BackStore, ParserFactory);
                }
            }
        }

        private void addToIgnoredComIDsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ignoreComids = new List<uint>();
            List<string> strings = new List<string>();

            if (!string.IsNullOrEmpty(Settings.IgnoreComid))
            {
                strings.AddRange(Settings.IgnoreComid.Split(','));
            }

            foreach (CapturePacket o in fastObjectListView1.SelectedObjects)
            {
                strings.Add(o.Comid.ToString());
            }

            foreach (string s in strings.Distinct())
            {
                if (uint.TryParse(s.Trim(), out uint u))
                {
                    ignoreComids.Add(u);
                }
            }

            Settings.IgnoreComid = string.Join(",", ignoreComids);

            UpdateFilter();
        }

        private void ContextMenuMouse_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CapturePacket o = (CapturePacket)fastObjectListView1.SelectedObject;
            if (o != null)
            {
                addToIgnoredComIDsToolStripMenuItem.Enabled = o.Protocol == ProtocolType.IPTWP;
            }
        }

        private void timerFlicker_Tick(object sender, EventArgs e)
        {
            if (BackStore != null)
            {
                var backStoreStatus = BackStore.Status;
                if (backStoreStatus != _lastBackStoreStatus)
                {
                    _lastBackStoreStatus = backStoreStatus;
                    var textOverlay = new TextOverlay() { Text = backStoreStatus };
                    textOverlay.Alignment = ContentAlignment.MiddleCenter;
                    textOverlay.Font = _statusFont;
                    textOverlay.InsetY = 100;
                    textOverlay.BackColor = Color.White;
                    textOverlay.TextColor = Color.Black;
                    textOverlay.Transparency = 200;
                    //var measureText = TextRenderer.MeasureText(backStoreStatus, textOverlay.Font);
                    //var inset = this.Width / 2 + measureText.Width / 2;
                    //textOverlay.InsetX = inset;
                    fastObjectListView1.OverlayText = textOverlay;
                }
            }

            if (_list.Count > 0)
                fastObjectListView1.EmptyListMsg = EmptyFilterText;
            else
            {
                fastObjectListView1.EmptyListMsg = EmptyText;
            }
        }

        private void sPREADSHEETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog { DefaultExt = "xlsx" };
            DialogResult dialogResult = saveFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                var exporterer = new Exporterer(this.GetAllPackets(), this.GetFilteredPackets(), this.GetSelectedPackets(), BackStore, ParserFactory);
                exporterer.ShowDialog(this);
            }

        }
    }

    public class MyOLVColumn : OLVColumn
    {
        public ClusterGetterDelegate ClusterGetter { get; set; }

        public delegate List<ICluster> ClusterGetterDelegate(IEnumerable<CapturePacket> packets);
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