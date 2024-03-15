using System.Collections.Generic;

namespace TrainShark.Import
{
    internal interface IImporter
    {
        public bool CanImport(string path);

        IEnumerable<CapturePacket> Import(string fileName);
    }
}