using System;

namespace ObjectsCollection
{
    public class Dictionary<TKey, TValue>
    {
        readonly int[] buckets;
        readonly Element<TKey, TValue>[] elements;
        readonly int initialCapacity = 7;

        public Dictionary()
        {
            buckets = new int[initialCapacity];
            elements = new Element<TKey, TValue>[initialCapacity];
            Count = 0;

            for (int i = 0; i < initialCapacity; i++)
            {
                buckets[i] = -1;
            }
        }

        public int Count { get; private set; }
    }
}
