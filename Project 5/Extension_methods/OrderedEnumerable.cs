using System.Collections;

namespace Extension_methods
{
    public class CustomComparer<TSource> : IComparer<TSource>
    {
        private readonly IComparer<TSource> comparer;

        public CustomComparer(
            IComparer<TSource> comparer,
            CustomComparer<TSource> parent)
        {
            this.comparer = comparer;
            this.Parent = parent;
        }

        public CustomComparer<TSource> Parent { get; }

        public IComparer Comparer { get; }

        public int Compare(TSource first, TSource second)
        {
            return comparer.Compare(first, second);
        }
    }

    public class OrderedEnumerable<TSource, TKey> : IOrderedEnumerable<TSource>
    {
        private readonly IEnumerable<TSource> source;
        private readonly CustomComparer<TSource> customComparer;

        public OrderedEnumerable(
            IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer,
            CustomComparer<TSource> previousComparer)
        {
            this.source = source;
            customComparer = new CustomComparer<TSource>(
                Comparer<TSource>.Create((x, y) => comparer.Compare(keySelector(x), keySelector(y))),
                previousComparer);

        }

        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TNewKey>(
            Func<TSource, TNewKey> keySelector,
            IComparer<TNewKey> comparer,
            bool descending = false)
        {
            return new OrderedEnumerable<TSource, TNewKey>(
                this.source,
                keySelector,
                comparer,
                this.customComparer);
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
                int comparison = CompareKeys(left[leftIndex], right[rightIndex], customComparer);

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

        private static int CompareKeys(
            TSource leftElement,
            TSource rightElement,
            CustomComparer<TSource> currentComparer)
        {
            if (currentComparer == null)
            {
                return 0;
            }

            int comparison = CompareKeys(leftElement, rightElement, currentComparer.Parent);

            if (comparison == 0)
            {
                comparison = currentComparer.Compare(leftElement, rightElement);
            }

            return comparison;
        }
    }
}
