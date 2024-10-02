using System;
using System.Collections;

namespace ObjectsCollection
{
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
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

        //verifica tot, acum ca ai adaugat isActive si refactorizeaza sa te folosesti de el

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public ICollection<TKey> Keys
        {
            get
            {
                Element<TKey, TValue>[] active = GetActiveElements();
                TKey[] keys = new TKey[Count];

                for (int i = 0; i < Count; i++)
                {
                    keys[i] = active[i].Key;
                }

                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                Element<TKey, TValue>[] active = GetActiveElements();
                TValue[] values = new TValue[Count];

                for (int i = 0; i < Count; i++)
                {
                    values[i] = active[i].Value;
                }

                return values;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                ValidateKeyExists(key);
                return GetElement(key).Value;
            }

            set
            {
                ValidateKeyExists(key);
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
            Element<TKey, TValue>[] active = GetActiveElements();

            for (int i = 0; i < Count; i++)
            {
                yield return new KeyValuePair<TKey, TValue>(active[i].Key, active[i].Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            ValidateKeyValue(key);

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

                if (elements[i] != null)
                {
                    elements[i].IsActive = false;
                }
            }
        }

        public bool ContainsKey(TKey key)
        {
            ValidateKeyValue(key);
            return Keys.Contains(key);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return TryGetValue(item.Key, out var value) && value.Equals(item.Value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ValidateArray(array, arrayIndex);
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
            ValidateKeyValue(key);

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

        private Element<TKey, TValue>[] GetActiveElements()
        {
            Element<TKey, TValue>[] active = new Element<TKey, TValue>[Count];
            int activeIndex = 0;

            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i]?.IsActive != true)
                {
                    continue;
                }

                active[activeIndex] = elements[i];
                activeIndex++;
            }

            return active;
        }

        private Element<TKey, TValue> GetElement(TKey key)
        {
            int bucketIndex = GetBucket(key);

            if (buckets[bucketIndex] == -1)
            {
                throw new InvalidOperationException("No element with this key was found");
            }

            for (Element<TKey, TValue> current = elements[buckets[bucketIndex]];
                current != null;
                current = current.Next != -1 ? elements[current.Next] : null)
            {
                if (current.Key.Equals(key))
                {
                    return current;
                }
            }

            throw new InvalidOperationException("No element with this key was found");
        }

        private void ValidateKeyExists(TKey key)
        {
            ValidateKeyValue(key);

            if (ContainsKey(key))
            {
                return;
            }

            throw new KeyNotFoundException("The key was not found.");
        }

        private void ValidateNewKey(TKey key)
        {
            ValidateKeyValue(key);

            if (!ContainsKey(key))
            {
                return;
            }

            throw new ArgumentException("The key is already used.");
        }

        private static void ValidateKeyValue(TKey key)
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
