using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace IPTComShark.Controls
{
    public class PacketSource : IVirtualListDataSource
    {
        private List<CapturePacket> _list = new List<CapturePacket>();

        public object GetNthObject(int n)
        {
            return _list[n];
        }

        public int GetObjectCount()
        {
            return _list.Count;
        }

        public int GetObjectIndex(object model)
        {
            var cp = (CapturePacket) model;
            return _list.IndexOf(cp, 0);
        }

        public void PrepareCache(int first, int last)
        {
            throw new NotImplementedException();
        }

        public int SearchText(string value, int first, int last, OLVColumn column)
        {
            throw new NotImplementedException();
        }

        public void Sort(OLVColumn column, SortOrder order)
        {
            throw new NotImplementedException();
        }

        public void AddObjects(ICollection modelObjects)
        {
            throw new NotImplementedException();
        }

        public void InsertObjects(int index, ICollection modelObjects)
        {
            throw new NotImplementedException();
        }

        public void RemoveObjects(ICollection modelObjects)
        {
            throw new NotImplementedException();
        }

        public void SetObjects(IEnumerable collection)
        {
            throw new NotImplementedException();
        }

        public void UpdateObject(int index, object modelObject)
        {
            throw new NotImplementedException();
        }
    }
}