using System.Collections.Generic;

namespace TrainShark
{
    public class MultiArray<T1, T2, Tdata>
    {
        private Dictionary<T1, Dictionary<T2, Tdata>> _upperDic = new Dictionary<T1, Dictionary<T2, Tdata>>();

        public MultiArray()
        {
        }

        public IEnumerable<T1> GetKeys()
        {
            foreach (var pair in _upperDic)
            {
                yield return pair.Key;
            }
        }

        public IEnumerable<T2> GetSecondKeys(T1 rootKey)
        {
            foreach (var pair in _upperDic[rootKey])
            {
                yield return pair.Key;
            }
        }

        public Tdata this[T1 k1, T2 k2]
        {
            get
            {
                if (!_upperDic.ContainsKey(k1))
                {
                    _upperDic.Add(k1, new Dictionary<T2, Tdata>());
                }
                return _upperDic[k1][k2];
            }
            set
            {
                if (!_upperDic.ContainsKey(k1))
                {
                    _upperDic.Add(k1, new Dictionary<T2, Tdata>());
                }

                if (_upperDic[k1].ContainsKey(k2))
                {
                    _upperDic[k1][k2] = value;
                }
                else
                {
                    _upperDic[k1].Add(k2, value);
                }
            }
        }

        public bool ContainsKey(T1 k1, T2 k2)
        {
            return _upperDic.ContainsKey(k1) && _upperDic[k1].ContainsKey(k2);
        }
    }
}