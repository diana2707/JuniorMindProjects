using System.Collections;

namespace Extension_methods
{
    //vezi daca poti evita sa repeti construirea comparerului

    public class CustomComparer<TSource> : IComparer<TSource>
    {
        private readonly IComparer<TSource> currentComparer;
        private readonly IComparer<TSource> compositeComparer;

        public CustomComparer(
            IComparer<TSource> currentComparer,
            IComparer<TSource> compositeComparer)
        {
            this.currentComparer = currentComparer;
            this.compositeComparer = compositeComparer;
        }

        public int Compare(TSource x, TSource y)
        {
            int comparison = 0;

            if (compositeComparer != null)
            {
                comparison = compositeComparer.Compare(x, y);
            }

            if (comparison == 0)
            {
                comparison = currentComparer.Compare(x, y);
            }

            return comparison;
        }
    }

    public class OrderedEnumerable<TSource> : IOrderedEnumerable<TSource>
    {
        private readonly IEnumerable<TSource> source;
        private readonly IComparer<TSource> compositeComparer;

        public OrderedEnumerable(
            IEnumerable<TSource> source,
            IComparer<TSource> compositeComparer)
        {
            this.source = source;
            this.compositeComparer = compositeComparer;
        }

        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer,
            bool descending = false)
        {
            IComparer<TSource> newComparer = Comparer<TSource>.Create((first, second) =>
                comparer.Compare(keySelector(first), keySelector(second)));

            return new OrderedEnumerable<TSource>(
                source,
                new CustomComparer<TSource>(newComparer, compositeComparer));
        }

        public IEnumerator<TSource> GetEnumerator()
        {
            List<TSource> sourceList = source.ToList();
            MergeSort(sourceList);

            foreach (var item in sourceList)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void MergeSort(
            List<TSource> source)
        {
            if (source.Count <= 1)
            {
                return;
            }

            int mid = (int)Math.Floor((double)source.Count / 2);

            List<TSource> left = source[..mid];
            List<TSource> right = source[mid..];

            MergeSort(left);
            MergeSort(right);

            Merge(source, left, right);
        }

        private void Merge(
            List<TSource> source,
            List<TSource> left,
            List<TSource> right)
        {
            int leftIndex = 0;
            int rightIndex = 0;
            int sourceIndex = 0;

            while (leftIndex < left.Count && rightIndex < right.Count)
            {
                int comparison = compositeComparer.Compare(left[leftIndex], right[rightIndex]);

                if (comparison < 0)
                {
                    source[sourceIndex++] = left[leftIndex++];
                }

                if (comparison > 0)
                {
                    source[sourceIndex++] = right[rightIndex++];
                }

                if (comparison == 0)
                {
                    source[sourceIndex++] = left[leftIndex++];
                    source[sourceIndex++] = right[rightIndex++];
                }
            }

            while (leftIndex < left.Count)
            {
                source[sourceIndex++] = left[leftIndex++];
            }

            while (rightIndex < right.Count)
            {
                source[sourceIndex++] = right[rightIndex++];
            }
        }
    }
}
