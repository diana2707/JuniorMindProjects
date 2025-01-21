using System.Collections;

namespace Extension_methods
{
    public static class IEnumerableExtensions
    {
        //empty collection, null element in collection, null selector function,
        //Changes to the source collection after the Select call but before enumeration should be reflected.
        public static bool All<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            ArgumentNullException.ThrowIfNull(source);

            foreach (var item in source)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Any<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }

        public static TSource First<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            throw new InvalidOperationException(
                "No element to match the predicate was found.");
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            foreach (var item in source)
            {
                yield return selector(item);
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TResult>> selector)
        {
             ArgumentNullException.ThrowIfNull(source);
             ArgumentNullException.ThrowIfNull(selector);

             foreach (var item in source)
            {
                foreach (var subItem in selector(item))
                {
                    yield return subItem;
                }
            }
        }

        public static IEnumerable<TSource> Where<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(keySelector);
            ArgumentNullException.ThrowIfNull(elementSelector);

            Dictionary<TKey, TElement> result = [];

            foreach (var item in source)
            {
                TKey key = keySelector(item);

                if (result.ContainsKey(key))
                {
                    throw new ArgumentException("Key selector generated duplicate key.");
                }

                result.Add(keySelector(item), elementSelector(item));
            }

            return result;
        }

        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(
            this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            Func<TFirst, TSecond, TResult> resultSelector)
        {
            ArgumentNullException.ThrowIfNull(first);
            ArgumentNullException.ThrowIfNull(second);

            IEnumerator<TFirst> firstEnumerator = first.GetEnumerator();
            IEnumerator<TSecond> secondEnumerator = second.GetEnumerator();

            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
            {
                yield return resultSelector(firstEnumerator.Current, secondEnumerator.Current);
            }
        }

        public static TAccumulate Aggregate<TSource, TAccumulate>(
            this IEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> func)
        {
            TAccumulate result = seed;
            foreach (var item in source)
            {
                result = func(result, item);
            }

            return result;
        }

        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            List<TResult> result = [];

            foreach (var outerItem in outer)
            {
                TKey outerKey = outerKeySelector(outerItem);
                foreach (var innerItem in inner)
                {
                    TKey innerKey = innerKeySelector(innerItem);

                    if (outerKey.Equals(innerKey))
                    {
                        result.Add(resultSelector(outerItem, innerItem));
                    }
                }
            }

            return result;
        }

        public static IEnumerable<TSource> Distinct<TSource>(
            this IEnumerable<TSource> source,
            IEqualityComparer<TSource> comparer)
        {
            HashSet<TSource> evaluated = new (comparer);

            foreach (var item in source)
            {
                if (evaluated.Add(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TSource> Union<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            IEnumerable<TSource> result = first.Concat(second);

            return result.Distinct(comparer);
        }

        public static IEnumerable<TSource> Intersect<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            HashSet<TSource> result = new ();

            foreach (var itemFirst in first)
            {
                foreach (var itemSecond in second)
                {
                    if (comparer.Equals(itemFirst, itemSecond))
                    {
                        result.Add(itemFirst);
                    }
                }
            }

            return result;
        }

        public static IEnumerable<TSource> Except<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            HashSet<TSource> result = new (comparer);

            foreach (var item in first)
            {
                if (!second.Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            Func<TKey, IEnumerable<TElement>, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            List<TResult> result = [];
            Dictionary<TKey, List<TElement>> groups = new (comparer);

            foreach (var item in source)
            {
                TKey key = keySelector(item);
                if (groups.TryGetValue(key, out List<TElement> elementsList))
                {
                    elementsList.Add(elementSelector(item));
                }
                else
                {
                    groups.Add(key, [elementSelector(item)]);
                }
            }

            foreach (var group in groups)
            {
                result.Add(resultSelector(group.Key, group.Value));
            }

            return result;
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            Func<TSource, object> generalKeySelector = x => keySelector(x);
            IComparer<object> generalComparer = Comparer<object>.Create((x, y) => comparer.Compare((TKey)x, (TKey)y));

            return new OrderedEnumerable<TSource>(
                source,
                generalKeySelector,
                generalComparer);
        }

        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(
            this IOrderedEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            return source.CreateOrderedEnumerable(keySelector, comparer, false);
        }

        private class OrderedEnumerable<TSource> : IOrderedEnumerable<TSource>
        {
            private readonly IEnumerable<TSource> source;
            private readonly Func<TSource, object> keySelector;
            private readonly IComparer<object> comparer;
            private OrderedEnumerable<TSource> parent;

            public OrderedEnumerable(
                IEnumerable<TSource> source,
                Func<TSource, object> keySelector,
                IComparer<object> comparer,
                OrderedEnumerable<TSource> parent = null)
            {
                this.source = source;
                this.keySelector = keySelector;
                this.comparer = comparer;
                this.parent = parent;
            }

            public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(
                Func<TSource, TKey> keySelector,
                IComparer<TKey> comparer,
                bool descending)
            {
                return new OrderedEnumerable<TSource>(
                    this.source,
                    x => keySelector(x),
                    Comparer<object>.Create((x, y) => comparer.Compare((TKey)x, (TKey)y)),
                    this);
            }

            public IEnumerator<TSource> GetEnumerator()
            {
                List<TSource> list = this.source.ToList();
                Stack<(Func<TSource, object> keySelector, IComparer<object> comparer)> sortLayers = new ();
                OrderedEnumerable<TSource> current = this;

                while (current != null)
                {
                    sortLayers.Push((current.keySelector, current.comparer));
                    current = current.parent;
                }

                MergeSort(list, sortLayers);
                return list.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private static void MergeSort<TSource, TKey>(
            List<TSource> source,
            Stack<(Func<TSource, TKey> keySelector, IComparer<TKey> comparer)> sortLayers)
        {
            if (source.Count <= 1)
            {
                return;
            }

            int mid = (int)Math.Floor((double)source.Count / 2);

            List<TSource> left = source[..mid];
            List<TSource> right = source[mid..];

            MergeSort(left, sortLayers);
            MergeSort(right, sortLayers);

            Merge(source, left, right, sortLayers);
        }

        private static void Merge<TSource, TKey>(
            List<TSource> source,
            List<TSource> left,
            List<TSource> right,
            Stack<(Func<TSource, TKey> keySelector, IComparer<TKey> comparer)> sortLayers)
        {
            int leftIndex = 0;
            int rightIndex = 0;
            int sourceIndex = 0;

            while (leftIndex < left.Count && rightIndex < right.Count)
            {
                bool areEqual = true;

                foreach (var (keySelector, comparer) in sortLayers)
                {
                    int comparison = comparer.Compare(keySelector(left[leftIndex]), keySelector(right[rightIndex]));

                    if (comparison < 0)
                    {
                        source[sourceIndex++] = left[leftIndex++];
                        areEqual = false;
                        break;
                    }

                    if (comparison > 0)
                    {
                        source[sourceIndex++] = right[rightIndex++];
                        areEqual = false;
                        break;
                    }
                }

                if (areEqual)
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