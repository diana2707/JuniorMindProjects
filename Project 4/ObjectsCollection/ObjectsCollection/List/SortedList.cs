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
                if (IsUnsortedSetter(index, value))
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

        public override void Insert(int index, T element)
        {
            if (IsUnsortedInsert(index, element))
            {
                return;
            }

            base.Insert(index, element);
        }

        public override int IndexOf(T element)
        {
            int low = 0;
            int high = Count - 1;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;

                if (array[mid].CompareTo(element) == 0)
                {
                    return mid;
                }
                else if (array[mid].CompareTo(element) > 0)
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

        private bool IsUnsortedSetter(int index, T element)
        {
            return (index != 0 && element.CompareTo(array[index - 1]) < 0) ||
                (index != Count - 1 && element.CompareTo(array[index + 1]) > 0);
        }

        private bool IsUnsortedInsert(int index, T element)
        {
            return (index != 0 && element.CompareTo(array[index - 1]) < 0) ||
                element.CompareTo(array[index]) > 0;
        }
    }
}
