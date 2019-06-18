using BrightIdeasSoftware;
using System.Collections.Generic;
using System.Linq;

namespace IPTComShark.Controls
{
    public class MyFilterMenuBuilder : FilterMenuBuilder
    {
        protected override List<ICluster> Cluster(IClusteringStrategy strategy, ObjectListView listView,
            OLVColumn column)
        {
            if (column is MyOLVColumn mycolumn && mycolumn.ClusterGetter != null)
            {
                var list = mycolumn.ClusterGetter.Invoke(listView.ObjectsForClustering.Cast<CapturePacket>());
                if (strategy is ClusteringStrategy cstrategy)
                {
                    foreach (var c in list)
                    {
                        string format = (c.Count == 1)
                            ? cstrategy.DisplayLabelFormatSingular
                            : cstrategy.DisplayLabelFormatPlural;
                        c.DisplayLabel = string.IsNullOrEmpty(format)
                            ? c.DisplayLabel
                            : string.Format(format, c.DisplayLabel, c.Count);
                    }
                }

                return list;
            }

            return base.Cluster(strategy, listView, column);
        }
    }
}