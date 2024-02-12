﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainShark.Import
{
    internal interface IImporter
    {
        public bool CanImport(string path);
        IEnumerable<CapturePacket> Import(string fileName);
    }
}
