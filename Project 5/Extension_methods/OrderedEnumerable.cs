using System.Collections;

namespace Extension_methods
{
    public class CustomComparer<TSource, TKey>
    {
        Func<TSource, TKey> keySelector;
        IComparer<TKey> comparer;

        public CustomComparer(
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public int Compare(TSource leftElement, TSource rightElement)
        {
            return comparer.Compare(keySelector(leftElement), keySelector(rightElement));
        }
    }

    public abstract class OrderedEnumerable<TSource> : IOrderedEnumerable<TSource>
    {
        internal abstract OrderedEnumerable<TSource> Parent { get; set; }

        internal abstract int CompareKeys(
            TSource leftElement,
            TSource rightElement,
            OrderedEnumerable<TSource> nextLayer);

        public abstract IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer,
            bool descending);

        public abstract IEnumerator<TSource> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class OrderedEnumerable<TSource, TKey> : OrderedEnumerable<TSource>
    {
        readonly IEnumerable<TSource> source;
        readonly Func<TSource, TKey> keySelector;
        readonly IComparer<TKey> comparer;
        readonly OrderedEnumerable<TSource> parent;

        public OrderedEnumerable(
            IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer,
            OrderedEnumerable<TSource> parent = null
            )
        {
            this.source = source;
            this.keySelector = keySelector;
            this.comparer = comparer;
            Parent = parent;
        }

        internal override OrderedEnumerable<TSource> Parent { get; set; }

        public override IOrderedEnumerable<TSource> CreateOrderedEnumerable<TNewKey>(
            Func<TSource, TNewKey> keySelector,
            IComparer<TNewKey> comparer,
            bool descending = false)
        {
            return new OrderedEnumerable<TSource, TNewKey>(
                source,
                keySelector,
                comparer,
                this);
        }

        public override IEnumerator<TSource> GetEnumerator()
        {
            List<TSource> sourceList = source.ToList();
            MergeSort(sourceList);

            foreach (var item in sourceList)
            {
                yield return item;
            }
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
                int comparison = CompareKeys(left[leftIndex], right[rightIndex], this);

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

        internal override int CompareKeys(
            TSource leftElement,
            TSource rightElement,
            OrderedEnumerable<TSource> currentLayer)
        {
            int comparison = 0;

            if (currentLayer != null)
            {
                comparison = currentLayer.CompareKeys(leftElement, rightElement, currentLayer.Parent);
            }

            if (comparison == 0)
            {
                comparison = comparer.Compare(keySelector(leftElement), keySelector(rightElement));
            }

            return comparison;
        }
    }
}
