using System;
using System.Collections.Generic;
using System.Net;
using BitDataParser;

namespace IPTComShark.BackStore
{
    public class LastKnownStore
    {
        private List<Bygones> _bygoneses = new List<Bygones>();

        public Tuple<CapturePacket, List<ParsedDataSet>> Find(ProtocolType pt, string identifier, IPAddress ip)
        {
            foreach (var bygonese in _bygoneses)
            {
                if (bygonese.PT == pt)
                {
                    if (bygonese.Id == identifier)
                    {
                        if (bygonese.IP.Equals(ip))
                        {
                            // YES
                            return new Tuple<CapturePacket, List<ParsedDataSet>>(bygonese.Packet, bygonese.Data);
                        }
                    }
                }
            }

            return null;
        }

        public void Add(ProtocolType pt, string identifier, IPAddress ip, CapturePacket packet,
            List<ParsedDataSet> data)
        {
            _bygoneses.Add(new Bygones(pt, identifier, ip, packet, data));
        }

        public void Set(ProtocolType pt, string identifier, IPAddress ip, CapturePacket packet,
            List<ParsedDataSet> data)
        {
            _bygoneses.RemoveAll(b => b.PT == pt && b.Id == identifier && b.IP.Equals(ip));

            _bygoneses.Add(new Bygones(pt, identifier, ip, packet, data));
        }

        private struct Bygones
        {
            public ProtocolType PT;
            public string Id;
            public IPAddress IP;
            public CapturePacket Packet;
            public List<ParsedDataSet> Data;

            public Bygones(ProtocolType pt, string id, IPAddress ip, CapturePacket packet, List<ParsedDataSet> data)
            {
                PT = pt;
                Id = id;
                IP = ip;
                Packet = packet;
                Data = data;
            }
        }
    }
}