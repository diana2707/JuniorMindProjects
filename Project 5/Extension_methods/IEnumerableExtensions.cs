using System.Collections.Generic;
using Xunit;

namespace Extension_methods
{
    public static class IEnumerableExtensions
    {
        //see if you can use more yield return
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var item in source)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            throw new InvalidOperationException("No element that matches the predicate was found.");
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            List<TResult> selection = [];
            foreach (var item in source)
            {
                selection.Add(selector(item));
            }

            return selection;
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            List<TResult> selection = [];

            foreach (var item in source)
            {
                selection.AddRange(selector(item));
            }

            return selection;
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            List<TSource> result = [];
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            Dictionary<TKey, TElement> result = [];

            foreach (var item in source)
            {
                result.Add(keySelector(item), elementSelector(item));
            }

            return result;
        }

        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(
            this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            Func<TFirst, TSecond, TResult> resultSelector)
        {
            List<TResult> result = [];

            IEnumerator<TFirst> firstEnumerator = first.GetEnumerator();
            IEnumerator<TSecond> secondEnumerator = second.GetEnumerator();

            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
            {
                result.Add(resultSelector(firstEnumerator.Current, secondEnumerator.Current));
            }

            return result;
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
            HashSet<TSource> evaluated = new HashSet<TSource>(comparer);

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
            HashSet<TSource> evaluated = new HashSet<TSource>(comparer);

            foreach (var item in first)
            {
                if (evaluated.Add(item))
                {
                    yield return item;
                }
            }

            foreach (var item in second)
            {
                if (evaluated.Add(item))
                {
                    yield return item;
                }
            }
        }
    }
}