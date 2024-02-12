using System.Collections.Generic;
using BitDataParser;

namespace TrainShark.DataSets
{
    public class TPWS : DataSetCollection
    {
        public DataSetDefinition JRUData = new DataSetDefinition
        {
            Identifiers = new List<string> {"230504300"},
            BitFields = new List<BitField>
            {
                new BitField {NestedDataSet = Subset27.Header, Length = 1},
                new BitField {NestedDataSet = Proprietary.PropJRU, Length = 1}
            }
        };

        public TPWS()
        {
            Name = "TPWS Standalone Data";
            Description = "";
            DataSets.Add(JRUData);
        }
    }
}