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
            Array.Fill(buckets, -1);

            elements = new Element<TKey, TValue>[capacity];
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
            int elementIndex = freeIndex == -1 ? Count : freeIndex;

            newElement.Next = buckets[bucketIndex];
            buckets[bucketIndex] = elementIndex;
            elements[elementIndex] = newElement;
            freeIndex = freeIndex == -1 ? -1 : elements[freeIndex].Next;

            Count++;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                for (int j = buckets[i]; j != -1; j = elements[j].Next)
                {
                    yield return new KeyValuePair<TKey, TValue>(elements[j].Key, elements[j].Value);
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
            Array.Fill(buckets, -1);
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
            Element<TKey, TValue> element;
            try
            {
                element = GetElement(key);
            }
            catch (KeyNotFoundException)
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
            int previousIndex = GetPreviousIndex(key);

            int currentIndex = previousIndex == -1 ? buckets[bucketIndex] : elements[previousIndex].Next;

            return elements[currentIndex];
        }

        private int GetPreviousIndex(TKey key)
        {
            int bucketIndex = GetBucket(key);
            int previousIndex = -1;

            for (int currentIndex = buckets[bucketIndex]; currentIndex != -1; currentIndex = elements[currentIndex].Next)
            {
                if (elements[currentIndex] == null)
                {
                    continue;
                }

                if (elements[currentIndex].Key.Equals(key))
                {
                    return previousIndex;
                }

                previousIndex = currentIndex;
            }

            throw new KeyNotFoundException("The key was not found.");
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
