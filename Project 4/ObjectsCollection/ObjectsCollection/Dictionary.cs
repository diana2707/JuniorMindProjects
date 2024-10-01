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

        //verifica tot, acum ca ai adaugat isActive

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public ICollection<TKey> Keys
        {
            get
            {
                TKey[] keys = new TKey[Count];
                int keysIndex = 0;

                for (int i = 0; i < buckets.Length; i++)
                {
                    if (buckets[i] != -1)
                    {
                        for (int current = buckets[i]; current != -1; current = elements[current].Next)
                        {
                            keys[keysIndex] = elements[current].Key;
                            keysIndex++;
                        }
                    }
                }

                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                TValue[] values = new TValue[Count];
                int valuesIndex = 0;

                for (int i = 0; i < buckets.Length; i++)
                {
                    if (buckets[i] != -1)
                    {
                        for (int current = buckets[i]; current != -1; current = elements[current].Next)
                        {
                            values[valuesIndex] = elements[current].Value;
                            valuesIndex++;
                        }
                    }
                }

                return values;
            }
        }

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
            //throw ex daca key exista deja
            int bucketIndex = GetBucket(key);

            Element<TKey, TValue> newElement = new Element<TKey, TValue>(key, value);
            newElement.IsActive = true;
            newElement.Next = buckets[bucketIndex];

            if (freeIndex == -1)
            {
                buckets[bucketIndex] = Count;
                elements[Count] = newElement;
            }
            else
            {
                buckets[bucketIndex] = freeIndex;
                freeIndex = elements[freeIndex].Next;
                elements[buckets[bucketIndex]] = newElement;
            }

            Count++;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] != null && elements[i].IsActive)
                {
                    yield return new KeyValuePair<TKey, TValue>(elements[i].Key, elements[i].Value);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            int bucketIndex = GetBucket(key);

            if (buckets[bucketIndex] == -1)
            {
                return false;
            }

            Element<TKey, TValue> current = elements[buckets[bucketIndex]];
            if (current.Key.Equals(key))
            {
                int tempIndex = buckets[bucketIndex];
                buckets[bucketIndex] = current.Next;
                current.Next = freeIndex;
                freeIndex = tempIndex;
                current.IsActive = false;
                Count--;

                return true;
            }

            while (current.Next != -1)
            {
                Element<TKey, TValue> previous = current;
                current = elements[previous.Next];

                if (current.Key.Equals(key))
                {
                    int tempIndex = current.Next;
                    current.Next = freeIndex;
                    freeIndex = previous.Next;
                    previous.Next = tempIndex;
                    current.IsActive = false;
                    Count--;

                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            Count = 0;
            freeIndex = -1;
            for (int i = 0; i < capacity; i++)
            {
                buckets[i] = -1;

                if (elements[i] != null)
                {
                    elements[i].IsActive = false;
                }
            }
        }

        public bool ContainsKey(TKey key)
        {
            return Keys.Contains(key);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return TryGetValue(item.Key, out var value) && value.Equals(item.Value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ICollection<TKey> keys = Keys;

            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i]?.IsActive != true)
                {
                    continue;
                }

                array[arrayIndex] = new KeyValuePair<TKey, TValue>(elements[i].Key, elements[i].Value);
                arrayIndex++;
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (!ContainsKey(key))
            {
                value = default;
                return false;
            }

            value = this[key];
            return true;
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
