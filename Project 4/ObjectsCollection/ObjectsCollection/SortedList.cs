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

        public void Sort()
        {
            for (int i = 1; i < Count; i++)
            {
                int j = i;
                int k = i - 1;

                int value = array[j].CompareTo(array[k]);
                while (k >= 0 && value < 0)
                {
                    (array[j], array[k]) = (array[k], array[j]);
                    j--;
                    k--;
                }
            }
        }
    }
}
