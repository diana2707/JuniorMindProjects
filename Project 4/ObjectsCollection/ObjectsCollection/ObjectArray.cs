using System.Collections;

namespace ObjectsCollection
{
    internal class ObjectArray : IEnumerable
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

        public bool Contains(object element)
        {
            return IndexOf(element) != -1;
        }

        public int IndexOf(object element)
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

        public void Insert(int index, object element)
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
            if (index == -1 || index >= Count)
            {
                return;
            }

            ShiftElementsLeft(index);

            Count--;
        }

        public void Remove(object element)
        {
            int index = IndexOf(element);

            RemoveAt(index);
        }

        public IEnumerator GetEnumerator()
        {
            return new ObjectArrayEnum(this);
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
