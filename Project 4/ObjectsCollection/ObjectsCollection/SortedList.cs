using System;

namespace ObjectsCollection
{
    internal class SortedList<T> : List<T>
        where T : IComparable<T>
    {
        public SortedList()
            : base()
        {
        }

        public override T this[int index]
        {
            get => array[index];
            set
            {
                if (!IsSorted(index, value))
                {
                    return;
                }

                array[index] = value;
            }
        }

        public override void Add(T element)
        {
            base.Add(element);
            Sort();
        }

        public void Sort()
        {
            for (int i = 1; i < Count; i++)
            {
                int j = i;
                int k = i - 1;

                while (k >= 0 && array[j].CompareTo(array[k]) < 0)
                {
                    (array[j], array[k]) = (array[k], array[j]);
                    j--;
                    k--;
                }
            }
        }

        public bool IsSorted(int index, T element)
        {
            bool isValidFirstElement = index == 0 && element.CompareTo(array[0]) <= 0;
            bool isConsecutiveElement = index != 0 && array[index - 1].CompareTo(element) <= 0 && array[index].CompareTo(element) >= 0;

            return isValidFirstElement || isConsecutiveElement;
        }
    }
}
