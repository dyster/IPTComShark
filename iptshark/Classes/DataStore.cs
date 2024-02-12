using BitDataParser;
using TrainShark.DataSets;
using System.Collections.Generic;

namespace TrainShark.Classes
{
    public class DataStore
    {
        public List<DataSetCollection> DataCollections { get; set; } = new List<DataSetCollection>();
        private Dictionary<uint, DataSetDefinition> _comidIndex = new Dictionary<uint, DataSetDefinition>();

        public DataStore()
        {
            DataCollections.Add(new IPT());
            DataCollections.Add(new TPWS());
            DataCollections.Add(new STM());
            DataCollections.Add(new ETCSDiag());
            DataCollections.Add(new VSISDMI());
            DataCollections.Add(new ABDO());
            DataCollections.Add(new VSIS210());
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
                    foreach (var identifier in dataSetDefinition.Identifiers)
                    {
                        var i = uint.Parse(identifier);
                        if (_comidIndex.ContainsKey(i))
                        {
                            //Logger.Log("Conflicting identifier " + identifier, Severity.Warning); 
                        }
                        else
                            _comidIndex.Add(i, dataSetDefinition);
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