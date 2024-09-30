using System;
using System.Collections;

namespace ObjectsCollection
{
    public class Dictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        readonly int[] buckets;
        readonly Element<TKey, TValue>[] elements;
        readonly int capacity = 5;
        private int freeIndex = -1;

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

                Element<TKey, TValue> current = elements[buckets[bucketIndex]];

                if (current.Key.Equals(key))
                {
                    return current.Value;
                }

                while (current.Next != -1)
                {
                    current = elements[current.Next];

                    if (current.Key.Equals(key))
                    {
                        return current.Value;
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

                Element<TKey, TValue> current = elements[buckets[bucketIndex]];

                if (current.Key.Equals(key))
                {
                    current.Value = value;
                    return;
                }

                while (current.Next != -1)
                {
                    current = elements[current.Next];

                    if (current.Key.Equals(key))
                    {
                        current.Value = value;
                        return;
                    }
                }

                throw new InvalidOperationException("No element with this key was found");
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(TKey key, TValue value)
        {
            int bucketIndex = GetBucket(key);

            Element<TKey, TValue> newElement = new Element<TKey, TValue>(key, value);
            newElement.Next = buckets[bucketIndex];

            buckets[bucketIndex] = Count;
            elements[Count] = newElement;

            Count++;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return new KeyValuePair<TKey, TValue>(elements[i].Key, elements[i].Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            //nu uita sa updatezi add sa foloseasca spatiile libere

            int bucketIndex = GetBucket(key);

            if (buckets[bucketIndex] == -1)
            {
                return false;
            }

            Element<TKey, TValue> current = elements[buckets[bucketIndex]];
            if (current.Key.Equals(key))
            {
                buckets[bucketIndex] = current.Next;
                current.Next = freeIndex;
                freeIndex = buckets[bucketIndex];
                Count--;

                return true;
            }

            while (current.Next != -1)
            {
                Element<TKey, TValue> previous = current;
                current = elements[previous.Next];

                if (current.Key.Equals(key))
                {
                    int nextIndex = current.Next;
                    current.Next = freeIndex;
                    freeIndex = previous.Next;
                    previous.Next = nextIndex;
                    Count--;

                    return true;
                }
            }

            return false;
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
