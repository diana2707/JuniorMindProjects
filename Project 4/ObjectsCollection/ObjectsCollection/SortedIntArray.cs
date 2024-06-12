using System;

namespace ObjectsCollection
{
    internal class SortedIntArray : IntArray
    {
        public SortedIntArray()
            : base()
        {
        }

        public void Sort()
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
