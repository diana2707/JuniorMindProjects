using System;

namespace ObjectsCollection
{
    internal class IntArray
    {
        private int[] array;
        private int count;

        public IntArray()
        {
            array = new int[4];
            count = 0;
        }

        public int Count { get; }

        public void Add(int element)
        {
            AllocatePositions();

            array[count] = element;
            count++;
        }

        public int Element(int index)
        {
            if (index >= count)
            {
                return -1;
            }

            return array[index];
        }

        public void SetElement(int index, int element)
        {
            if (index >= count)
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
            for (int i = 0; i < count; i++)
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

            if (index >= count)
            {
                return;
            }

            int tempCount = count;
            while (index < tempCount)
            {
                array[tempCount] = array[tempCount - 1];
                tempCount--;
            }

            array[index] = element;
            count++;
        }

        public void Clear()
        {
            for (int i = 0; i < count; i++)
            {
                array[i] = 0;
            }

            count = 0;
        }

        public void Remove(int element)
        {
            int index = IndexOf(element);

            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (index == -1 || index >= count)
            {
                return;
            }

            while (index < count - 1)
            {
                array[index] = array[index + 1];
                index++;
            }

            count--;
            array[count] = 0;
        }

        private void AllocatePositions()
        {
            if (count != array.Length)
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
