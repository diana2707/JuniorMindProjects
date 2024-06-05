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

        public void Add(int element)
        {
            AllocatePositions();

            array[Count] = element;
            Count++;
        }

        public int Element(int index)
        {
            if (index >= Count)
            {
                return -1;
            }

            return array[index];
        }

        public void SetElement(int index, int element)
        {
            if (index >= Count)
            {
                return;
            }

            array[index] = element;
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
            AllocatePositions();

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

        private void AllocatePositions()
        {
            if (Count != array.Length)
            {
                return;
            }

            int[] newArray = new int[array.Length * 2];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }

            array = newArray;
        }
    }
}
