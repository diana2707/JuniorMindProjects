using System;

namespace ObjectsCollection
{
    internal class IntArray
    {
        private int[] array;

        public IntArray()
        {
            array = new int[4];
            Count = 0;
        }

        public int Count { get; private set; }

        public int this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        public void Add(int element)
        {
            EnsureArrayPositions();

            array[Count] = element;
            Count++;
        }

        public bool Contains(int element)
        {
            return IndexOf(element) != -1;
        }

        public int IndexOf(int element)
        {
            for (int i = 0; i < Count; i++)
            {
                if (array[i] == element)
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, int element)
        {
            EnsureArrayPositions();

            if (index >= Count)
            {
                return;
            }

            int tempCount = Count;
            while (index < tempCount)
            {
                array[tempCount] = array[tempCount - 1];
                tempCount--;
            }

            array[index] = element;
            Count++;
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++)
            {
                array[i] = 0;
            }

            Count = 0;
        }

        public void Remove(int element)
        {
            int index = IndexOf(element);

            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (index == -1 || index >= Count)
            {
                return;
            }

            while (index < Count - 1)
            {
                array[index] = array[index + 1];
                index++;
            }

            Count--;
            array[Count] = 0;
        }

        private void EnsureArrayPositions()
        {
            if (Count != array.Length)
            {
                return;
            }

            Array.Resize(ref array, array.Length * 2);
        }
    }
}
