using System;

namespace ObjectsCollection
{
    internal class SortedIntArray : IntArray
    {
        public SortedIntArray()
            : base()
        {
        }

        public override int this[int index]
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

        public override void Add(int element)
        {
            base.Add(element);
            Sort();
        }

        public override void Insert(int index, int element)
        {
            if (!IsSorted(index, element))
            {
                return;
            }

            base.Insert(index, element);
        }

        private void Sort()
        {
                int j = Count - 1;
                int k = j - 1;

                while (k >= 0 && array[j] < array[k])
                {
                    (array[j], array[k]) = (array[k], array[j]);
                    j--;
                    k--;
                }
        }

        private bool IsSorted(int index, int element)
        {
            bool isValidFirstElement = index == 0 && element <= array[0];
            bool isConsecutiveElement = index != 0 && array[index - 1] <= element && array[index] >= element;

            return isValidFirstElement || isConsecutiveElement;
        }
    }
}
