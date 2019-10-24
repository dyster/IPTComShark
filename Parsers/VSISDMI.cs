using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sonesson_tools.BitStreamParser;

namespace IPTComShark.Parsers
{
    public class VSISDMI : DataSetCollection
    {
        public VSISDMI()
        {
            this.Name = "VSIS DMI";
            this.Description = "EVC Telegrams";
        }
    }
}
