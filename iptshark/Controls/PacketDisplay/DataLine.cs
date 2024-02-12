using BitDataParser;
using System.Text.Json;

namespace TrainShark.Controls
{
    public class DataLine
    {
        private readonly ParsedField _field;

        public DataLine(uint tick)
        {
            No = tick;
        }

        public DataLine(ParsedField field, uint tick)
        {
            No = tick;

            _field = field;
            //string typestring = field.Value.GetType().ToString();

            Name = field.Name;
            Value = field.Value.ToString();
            TrueValue = field.TrueValue;
            //Type = typestring.Substring(typestring.LastIndexOf(".") + 1);
            Comment = field.Comment;
        }

        public uint No { get; set; }

        public string Name { get; set; }

        public string Type => _field?.UsedBitField.BitFieldType.ToString();

        public string Value { get; set; }
        public object TrueValue { get; set; }
        public string Comment { get; set; }
        public bool Changed { get; set; }
        public bool IsCategory { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public ParsedField GetField()
        {
            return _field;
        }
    }
}