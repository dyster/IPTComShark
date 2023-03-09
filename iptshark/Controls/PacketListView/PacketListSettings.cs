using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace IPTComShark.Controls
{
    public class PacketListSettings : INotifyPropertyChanged
    {
        private bool _ignoreLoopback = false;
        private bool _autoScroll = false;
        private bool _ignoreDupePd = false;
        private bool _ignoreUnknown = false;
        private string _ignoreComid = "";
        private string[] _ignoreVariables = new string[] { "MMI_M_PACKET", "MMI_L_PACKET" };
        private List<ColumnInfo> _columnSettings;

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

                // This does not need it as it is read every time a packet is received
                //OnPropertyChanged();
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

        public string[] IgnoreVariables
        {
            get => _ignoreVariables;
            set
            {
                _ignoreVariables = value;
                OnPropertyChanged();
            }
        }

        public List<ColumnInfo> ColumnInfos
        {
            get => _columnSettings;
            set
            {
                _columnSettings = value;
                
                // does not need as only read on startup
                //OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SerializeToFile(string file)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            var bytes = JsonSerializer.SerializeToUtf8Bytes(this, this.GetType(), options);
            File.WriteAllBytes(file, bytes);
        }

        public string SerialiseToString()
        {
            return JsonSerializer.Serialize(this, this.GetType());
        }


        public static PacketListSettings DeserializeFile(string file)
        {
            using (var fileStream = File.OpenRead(file))
            {
                var settings = JsonSerializer.Deserialize<PacketListSettings>(fileStream);
                return settings;
            }
        }

        public static PacketListSettings DeserializeString(string str)            
        {
            if(string.IsNullOrWhiteSpace(str))
                return new PacketListSettings();
            return JsonSerializer.Deserialize<PacketListSettings>(str);
        }
    }
}