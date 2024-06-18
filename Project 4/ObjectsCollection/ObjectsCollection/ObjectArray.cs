using System;

namespace ObjectsCollection
{
    internal class ObjectArray
    {
        private object[] array;

        public ObjectArray()
        {
            array = new object[4];
            Count = 0;
        }

        public int Count { get; private set; }

        public object this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        public void Add(object element)
        {
            EnsureArrayPositions();

            array[Count] = element;
            Count++;
        }

        public int IndexOf(object element)
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
