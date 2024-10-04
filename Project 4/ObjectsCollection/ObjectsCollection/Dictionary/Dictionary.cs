using System;
using System.Collections;

namespace ObjectsCollection
{
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        readonly int[] buckets;
        readonly Element<TKey, TValue>[] elements;
        readonly int capacity;
        private int freeIndex;

        public Dictionary(int capacity)
        {
            this.capacity = capacity;
            buckets = new int[capacity];
            elements = new Element<TKey, TValue>[capacity];

            for (int i = 0; i < capacity; i++)
            {
                buckets[i] = -1;
            }

            freeIndex = -1;
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public ICollection<TKey> Keys
        {
            get
            {
                TKey[] keys = new TKey[Count];

                int index = 0;
                foreach (KeyValuePair<TKey, TValue> pair in this)
                {
                    keys[index++] = pair.Key;
                }

                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                TValue[] values = new TValue[Count];

                int index = 0;
                foreach (KeyValuePair<TKey, TValue> pair in this)
                {
                    values[index++] = pair.Value;
                }

                return values;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                ValidateDictionaryKey(key);
                return GetElement(key).Value;
            }

            set
            {
                ValidateDictionaryKey(key);
                GetElement(key).Value = value;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(TKey key, TValue value)
        {
            ValidateNewKey(key);

            int bucketIndex = GetBucket(key);

            Element<TKey, TValue> newElement = new Element<TKey, TValue>(key, value);
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
            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i] == -1)
                {
                    continue;
                }

                for (Element<TKey, TValue> current = elements[buckets[i]];
                    current != null;
                    current = current.Next != -1 ? elements[current.Next] : null)
                {
                    yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            ValidateKeyNotNull(key);

            int bucketIndex = GetBucket(key);

            int currentIndex = buckets[bucketIndex];
            int previousIndex = -1;

            if (currentIndex == -1)
            {
                return false;
            }

            Element<TKey, TValue> current = elements[buckets[bucketIndex]];

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    if (previousIndex == -1)
                    {
                        buckets[bucketIndex] = current.Next;
                    }
                    else
                    {
                        elements[previousIndex].Next = current.Next;
                    }

                    current.Next = freeIndex;
                    freeIndex = currentIndex;
                    Count--;

                    return true;
                }

                previousIndex = currentIndex;
                currentIndex = current.Next;
                current = currentIndex != -1 ? elements[currentIndex] : null;
            }

            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public void Clear()
        {
            Count = 0;
            freeIndex = -1;
            for (int i = 0; i < capacity; i++)
            {
                buckets[i] = -1;
            }
        }

        public bool ContainsKey(TKey key)
        {
            return TryGetValue(key, out var value);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return TryGetValue(item.Key, out var value) && value.Equals(item.Value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ValidateArray(array, arrayIndex);

            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                array[arrayIndex++] = pair;
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            ValidateKeyNotNull(key);
            Element<TKey, TValue> element = GetElement(key);

            if (element == null)
            {
                value = default;
                return false;
            }

            value = element.Value;
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

        private Element<TKey, TValue> GetElement(TKey key)
        {
            int bucketIndex = GetBucket(key);
            Element<TKey, TValue> current = null;

            if (buckets[bucketIndex] == -1)
            {
                return current;
            }

            for (current = elements[buckets[bucketIndex]];
                current != null;
                current = current.Next != -1 ? elements[current.Next] : null)
            {
                if (current.Key.Equals(key))
                {
                    return current;
                }
            }

            return current;
        }

        private void ValidateDictionaryKey(TKey key)
        {
            if (ContainsKey(key))
            {
                return;
            }

            throw new KeyNotFoundException("The key was not found.");
        }

        private void ValidateNewKey(TKey key)
        {
            if (!ContainsKey(key))
            {
                return;
            }

            throw new ArgumentException("The key is already used.");
        }

        private static void ValidateKeyNotNull(TKey key)
        {
            ArgumentNullException.ThrowIfNull(key);
        }

        private void ValidateArray(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfLessThan(arrayIndex, 0);

            if (array.Length - arrayIndex >= Count)
            {
                return;
            }

            throw new ArgumentException("Not enough space from arrayIndex to end of array");
        }
    }
}
