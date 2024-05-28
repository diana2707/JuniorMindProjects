using System;
using System.Reflection.Metadata.Ecma335;

namespace ObjectsCollection
{
    internal class IntArray
    {
        private int[] array;

        public IntArray()
        {
            array = new int[4];
        }

        public void Add(int element)
        {
            int lastIndex = GetLastIndex();

            AllocatePositions(lastIndex);

            array[lastIndex + 1] = element;
        }

        public int Count()
        {
            return GetLastIndex() + 1;
        }

        public int Element(int index)
        {
            if (index > GetLastIndex())
            {
                return -1;
            }

            return array[index];
        }

        public void SetElement(int index, int element)
        {
            array[index] = element;
        }

        public bool Contains(int element)
        {
            return IndexOf(element) != -1;
        }

        public int IndexOf(int element)
        {
            for (int i = 0; i < array.Length; i++)
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
            int[] newArray = new int[array.Length + 1];

            for (int i = 0; i < index; i++)
            {
                newArray[i] = array[i];
            }

            newArray[index] = element;

            for (int i = index + 1; i < newArray.Length; i++)
            {
                newArray[i] = array[i - 1];
            }

            array = newArray;
        }

        public void Clear()
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }
        }

        public void Remove(int element)
        {
            int index = IndexOf(element);

            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= array.Length)
            {
                return;
            }

            int[] newArray = new int[array.Length - 1];

            for (int i = 0; i < index; i++)
            {
                newArray[i] = array[i];
            }

            for (int i = index; i < newArray.Length; i++)
            {
                newArray[i] = array[i + 1];
            }

            array = newArray;
        }

        private int GetLastIndex()
        {
            for (int i = array.Length - 1; i >= 0; i--)
            {
                if (array[i] != 0)
                {
                    return i;
                }
            }

            return -1;
        }

        private void AllocatePositions(int lastIndex)
        {
            if (lastIndex < array.Length - 1)
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
