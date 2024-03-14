using BitDataParser;

namespace TrainShark.Parsers
{
    public abstract class ParserBase
    {
        public abstract ParseOutput Extract(byte[] data, iPacket iPacket);

        public abstract ProtocolType ProtocolType { get; }

        public DataSetCollection DataSets { get; }
    }
}