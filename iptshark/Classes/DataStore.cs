using BitDataParser;
using System.Collections.Generic;
using TrainShark.DataSets;

namespace TrainShark.Classes
{
    public class DataStore
    {
        public List<DataSetCollection> DataCollections { get; set; } = new List<DataSetCollection>();
        private Dictionary<uint, DataSetDefinition> _comidIndex = new Dictionary<uint, DataSetDefinition>();

        public DataStore()
        {
            DataCollections.Add(new IPT());            
        }

        public void Add(DataSetCollection dataSetCollection)
        {
            DataCollections.Add(dataSetCollection);
        }

        public void RebuildIndex()
        {
            foreach (var dataSetCollection in DataCollections)
            {
                foreach (var dataSetDefinition in dataSetCollection.DataSets)
                {
                    foreach (var i in dataSetDefinition.Identifiers.Numeric)
                    {
                        //TODO this currently uses numeric identifier only because it is only used by iptcom... should make generic
                        if (_comidIndex.ContainsKey((uint)i))
                        {
                            //Logger.Log("Conflicting identifier " + identifier, Severity.Warning);
                        }
                        else
                            _comidIndex.Add((uint)i, dataSetDefinition);
                    }
                }
            }
        }

        public DataSetDefinition GetByComid(uint comid)
        {
            if (_comidIndex.ContainsKey(comid))
                return _comidIndex[comid];
            return null;
        }
    }
}