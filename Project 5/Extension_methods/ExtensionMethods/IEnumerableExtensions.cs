using System;

namespace Extension_methods.Extension_methods
{
    public static class IEnumerableExtensions
    {
        public static bool CustomAll<TSource>(
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

        public static bool CustomAny<TSource>(
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

        public static TSource CustomFirst<TSource>(
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

        public static IEnumerable<TResult> CustomSelect<TSource, TResult>(
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

        public static IEnumerable<TResult> CustomSelectMany<TSource, TResult>(
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

        public static IEnumerable<TSource> CustomWhere<TSource>(
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

        public static Dictionary<TKey, TElement> CustomToDictionary<TSource, TKey, TElement>(
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

        public static IEnumerable<TResult> CustomZip<TFirst, TSecond, TResult>(
            this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            Func<TFirst, TSecond, TResult> resultSelector)
        {
            ArgumentNullException.ThrowIfNull(first);
            ArgumentNullException.ThrowIfNull(second);
            ArgumentNullException.ThrowIfNull(resultSelector);

            IEnumerator<TFirst> firstEnumerator = first.GetEnumerator();
            IEnumerator<TSecond> secondEnumerator = second.GetEnumerator();

            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
            {
                yield return resultSelector(firstEnumerator.Current, secondEnumerator.Current);
            }
        }

        public static TAccumulate CustomAggregate<TSource, TAccumulate>(
            this IEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> func)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(func);

            TAccumulate result = seed;
            foreach (var item in source)
            {
                result = func(result, item);
            }

            return result;
        }

        public static IEnumerable<TResult> CustomJoin<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            ArgumentNullException.ThrowIfNull(outer);
            ArgumentNullException.ThrowIfNull(inner);
            ArgumentNullException.ThrowIfNull(outerKeySelector);
            ArgumentNullException.ThrowIfNull(innerKeySelector);
            ArgumentNullException.ThrowIfNull(resultSelector);

            foreach (var outerItem in outer)
            {
                TKey outerKey = outerKeySelector(outerItem);
                foreach (var innerItem in inner)
                {
                    TKey innerKey = innerKeySelector(innerItem);

                    if (outerKey.Equals(innerKey))
                    {
                        yield return resultSelector(outerItem, innerItem);
                    }
                }
            }
        }

        public static IEnumerable<TSource> CustomDistinct<TSource>(
            this IEnumerable<TSource> source,
            IEqualityComparer<TSource> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);

            HashSet<TSource> evaluated = new(comparer);

            foreach (var item in source)
            {
                if (evaluated.Add(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TSource> CustomUnion<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            ArgumentNullException.ThrowIfNull(first);

            IEnumerable<TSource> result = first.Concat(second);

            return result.CustomDistinct(comparer);
        }

        public static IEnumerable<TSource> CustomIntersect<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            ArgumentNullException.ThrowIfNull(first);
            ArgumentNullException.ThrowIfNull(second);

            first = first.CustomDistinct(comparer);
            second = second.CustomDistinct(comparer);

            foreach (var itemFirst in first)
            {
                foreach (var itemSecond in second)
                {
                    if (comparer.Equals(itemFirst, itemSecond))
                    {
                        yield return itemFirst;
                    }
                }
            }
        }

        public static IEnumerable<TSource> CustomExcept<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            ArgumentNullException.ThrowIfNull(first);
            ArgumentNullException.ThrowIfNull(second);

            first = first.CustomDistinct(comparer);
            second = second.CustomDistinct(comparer);

            foreach (var item in first)
            {
                if (!second.Contains(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TResult> CustomGroupBy<TSource, TKey, TElement, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            Func<TKey, IEnumerable<TElement>, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(keySelector);
            ArgumentNullException.ThrowIfNull(elementSelector);
            ArgumentNullException.ThrowIfNull(resultSelector);

            Dictionary<TKey, List<TElement>> groups = new(comparer);

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
                yield return resultSelector(group.Key, group.Value);
            }
        }

        public static IOrderedEnumerable<TSource> CustomOrderBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(keySelector);

            var orderedEnumerable = new OrderedEnumerable<TSource>(source, null);
            return orderedEnumerable.CreateOrderedEnumerable(keySelector, comparer, false);
        }

        public static IOrderedEnumerable<TSource> CustomThenBy<TSource, TKey>(
            this IOrderedEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(keySelector);

            return source.CreateOrderedEnumerable(keySelector, comparer, false);
        }
    }
}