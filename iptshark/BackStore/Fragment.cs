using System;
using System.Collections.Generic;

namespace TrainShark.BackStore
{
    public class Fragment
    {
        public Dictionary<int, byte[]> Fragments { get; set; } = new Dictionary<int, byte[]>();

        public byte[] Extract()
        {
            int sum = 0;
            foreach (var keyValuePair in Fragments)
            {
                sum += keyValuePair.Value.Length;
            }

            var bytes = new byte[sum];
            foreach (var keyValuePair in Fragments)
            {
                Array.Copy(keyValuePair.Value, 0, bytes, keyValuePair.Key, keyValuePair.Value.Length);
            }

            return bytes;
        }
    }
}