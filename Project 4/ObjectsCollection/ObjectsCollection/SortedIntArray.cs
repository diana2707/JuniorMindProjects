using System;
using System.Reflection.Metadata.Ecma335;

namespace ObjectsCollection
{
    internal class SortedIntArray : IntArray
    {
        public SortedIntArray()
            : base()
        {
        }

        public override void Add(int element)
        {
            base.Add(element);
            Sort();
        }

        public override void Insert(int index, int element)
        {
            bool isValidFirstElement = index == 0 && element <= array[0];
            bool isConsecutiveElement = index != 0 && array[index - 1] <= element && array[index] >= element;

            if (!isValidFirstElement && !isConsecutiveElement)
            {
                return;
            }

            base.Insert(index, element);
        }

        private void Sort()
        {
            for (int i = 0; i < Count; i++)
            {
                int j = i;
                int k = i - 1;

                while (k >= 0 && this[j] < this[k])
                {
                    (this[j], this[k]) = (this[k], this[j]);
                    j--;
                    k--;
                }
            }
        }
    }
}
