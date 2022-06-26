using BitDataParser;
using System.Text.Json.Serialization;

namespace IPTComShark
{
    public class DisplayField
    {
        public string Name { get; }
        public object Val { get; }

        /// <summary>
        /// True to display, false to hide
        /// </summary>
        [JsonIgnore]
        public bool Display { get; set; }

        public DisplayField(string name, object val)
        {
            Name = name;
            Val = val;
        }

        public DisplayField(ParsedField field)
        {
            Name = field.Name;
            Val = field.Value;
        }

        public override string ToString()
        {
            return $"{Name}: {Val}";
        }
    }
}