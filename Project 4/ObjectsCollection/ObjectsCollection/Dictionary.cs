using System;

namespace ObjectsCollection
{
    public class Dictionary<TKey, TValue>
    {
        readonly int[] buckets;
        readonly Element<TKey, TValue>[] elements;
        readonly int capacity = 5;

        public Dictionary()
        {
            buckets = new int[capacity];
            elements = new Element<TKey, TValue>[capacity];

            for (int i = 0; i < capacity; i++)
            {
                buckets[i] = -1;
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            int hashCode = Math.Abs(key.GetHashCode()) % capacity;
            buckets[hashCode] = Count;

            elements[Count] = new Element<TKey, TValue>(key, value);
            Count++;
        }
    }
}
