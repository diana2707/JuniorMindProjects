using System;
using System.Xml.Linq;

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

        public TValue this[TKey key]
        {
            get
            {
                int bucketIndex = GetBucket(key);
                if (buckets[bucketIndex] == -1)
                {
                    throw new InvalidOperationException("No element with this key was found");
                }

                for (Element<TKey, TValue> element = elements[buckets[bucketIndex]];
                    element.Next == -1;
                    element = elements[element.Next])
                {
                    if (element.Key.Equals(key))
                    {
                        return element.Value;
                    }
                }

                throw new InvalidOperationException("No element with this key was found");
            }

            set
            {
                int bucketIndex = GetBucket(key);
                if (buckets[bucketIndex] == -1)
                {
                    throw new InvalidOperationException("No element with this key was found");
                }

                for (Element<TKey, TValue> element = elements[buckets[bucketIndex]];
                    element.Next == -1;
                    element = elements[element.Next])
                {
                    if (element.Key.Equals(key))
                    {
                        element.Value = value;
                        return;
                    }
                }

                throw new InvalidOperationException("No element with this key was found");
            }
        }

        public void Add(TKey key, TValue value)
        {
            int bucketIndex = GetBucket(key);

            //if (!IsEmpty())
            //{

            //}
            //for (Element<TKey, TValue> element = elements[buckets[bucketIndex]];
            //        element.Next == -1;
            //        element = elements[element.Next])
            //{
            //    if (element.Key.Equals(key))
            //    {
            //        throw new InvalidOperationException("This key is already used");
            //    }
            //}

            Element<TKey, TValue> newElement = new Element<TKey, TValue>(key, value);
            newElement.Next = buckets[bucketIndex];

            buckets[bucketIndex] = Count;
            elements[Count] = newElement;

            Count++;
        }

        private int GetBucket(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % capacity;
        }

        private bool IsEmpty()
        {
            return Count == 0;
        }
    }
}
