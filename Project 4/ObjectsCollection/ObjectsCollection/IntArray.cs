using System;

namespace ObjectsCollection
{
    internal class IntArray
    {
        protected int[] array;

        public IntArray()
        {
            array = new int[4];
            Count = 0;
        }

        public int Count { get; private set; }

        public virtual int this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        public virtual void Add(int element)
        {
            EnsureArrayPositions();

            array[Count] = element;
            Count++;
        }

        public virtual bool Contains(int element)
        {
            return IndexOf(element) != -1;
        }

        public virtual int IndexOf(int element)
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

        public virtual void Insert(int index, int element)
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

        public virtual void Remove(int element)
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

            ShiftElementsLeft(index);

            Count--;
        }

        private void EnsureArrayPositions()
        {
            if (Count != array.Length)
            {
                return;
            }

            Array.Resize(ref array, array.Length * 2);
        }

        private void ShiftElementsLeft(int index)
        {
            while (index < Count - 1)
            {
                array[index] = array[index + 1];
                index++;
            }
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
    }
}
