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
                if (IsUnsortedSetter(index, value))
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
            if (IsUnsortedInsert(index, element))
            {
                return;
            }

            base.Insert(index, element);
        }

        public override int IndexOf(int element)
        {
            int low = 0;
            int high = Count - 1;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;

                if (array[mid] == element)
                {
                    return mid;
                }
                else if (array[mid] > element)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            return -1;
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

        private bool IsUnsortedSetter(int index, int element)
        {
            return (index != 0 && element < array[index - 1]) ||
                (index != Count - 1 && element > array[index + 1]);
        }

        private bool IsUnsortedInsert(int index, int element)
        {
            return (index != 0 && element < array[index - 1]) ||
                element > array[index];
        }
    }
}
