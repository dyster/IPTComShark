using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTComShark.Parsers
{
    class IPTWPParser : IParser
    {
        public Parse Extract(byte[] data)
        {
            throw new NotImplementedException();
        }

        public ProtocolType ProtocolType => ProtocolType.IPTWP;
    }
}
