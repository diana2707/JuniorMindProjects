using System.Collections;

namespace ObjectsCollection
{
    internal class List<T> : IList<T>
    {
        protected T[] array;

        public List()
        {
            array = new T[4];
            Count = 0;
            IsReadOnly = false;
        }

        public bool IsReadOnly { get; }

        public int Count { get; private set; }

        public virtual T this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        public virtual void Add(T element)
        {
            EnsureArrayPositions();

            array[Count] = element;
            Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(T element)
        {
            return IndexOf(element) != -1;
        }

        public void CopyTo(T[] array, int index)
        {
            this.array.CopyTo(array, index);
        }

        public virtual int IndexOf(T element)
        {
            for (int i = 0; i < Count; i++)
            {
                if (array[i].Equals(element))
                {
                    return i;
                }
            }

            return -1;
        }

        public virtual void Insert(int index, T element)
        {
            EnsureArrayPositions();

            if (index >= Count)
            {
                return;
            }

            ShiftElementsRight(index);

            array[index] = element;
            Count++;
        }

        public void Clear()
        {
            Count = 0;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                return;
            }

            ShiftElementsLeft(index);

            Count--;
        }

        public bool Remove(T element)
        {
            int index = IndexOf(element);

            if (index == -1)
            {
                return false;
            }

            RemoveAt(index);
            return true;
        }

        private void EnsureArrayPositions()
        {
            if (Count != array.Length)
            {
                return;
            }

            Array.Resize(ref array, array.Length * 2);
        }

        private void ShiftElementsRight(int index)
        {
            int tempCount = Count;
            while (index < tempCount)
            {
                array[tempCount] = array[tempCount - 1];
                tempCount--;
            }
        }

        private void ShiftElementsLeft(int index)
        {
            while (index < Count - 1)
            {
                array[index] = array[index + 1];
                index++;
            }
        }
    }
}
