using System;
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
            ArgumentNullException.ThrowIfNull(resultSelector);

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
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(func);

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

        public static IEnumerable<TSource> Distinct<TSource>(
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

        public static IEnumerable<TSource> Union<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            ArgumentNullException.ThrowIfNull(first);

            IEnumerable<TSource> result = first.Concat(second);

            return result.Distinct(comparer);
        }

        public static IEnumerable<TSource> Intersect<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            ArgumentNullException.ThrowIfNull(first);
            ArgumentNullException.ThrowIfNull(second);

            first = first.Distinct(comparer);
            second = second.Distinct(comparer);

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

        public static IEnumerable<TSource> Except<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            ArgumentNullException.ThrowIfNull(first);
            ArgumentNullException.ThrowIfNull(second);

            first = first.Distinct(comparer);
            second = second.Distinct(comparer);

            foreach (var item in first)
            {
                if (!second.Contains(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(
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

        //-------------------------------------------------------------------------------------------------------------

        //pastreaza Order si Then la fel
        //partreaza OrederedEnumerable<TSource> ca si clasa abstracta
        //pastreaza OrderedEnumerable<TSource, TKey> ca implementare contreta, dar parent se stocheaza ca base class
        //cand este apelat ORderBy apoi ThenBy, in var result se stocheza ultimul obiect OrderedEnumerable creat de ThenBy,
                //deci cel mai lowest level

        //enumeratorul (cand este apelat) intra in ultimul obiect OrderedEnumerable (lowest level)
        //este apelat GetEnumerableSorter(tot in OrderedEnumerable) care primeste el insusi un sorter ca parametru
                //la prima apelare parametrul sorter este null (pentru ca se creaza in lowest level, nu exista un sorter mai low decat asta)
        //GetEnumerableSorter este recursiv. 
        //creaza sorterul pentru levelul curent (lowest) cu parametrul sorter pe null(sorterul trimis ca parametru este deja null)
        //returneaza un apel catre parent.GetEnumerableSorter si foloseste ca parametru sorterul din acest level
        //deci recursiv creaza si linkuieste sorterele pana cand parentul este null si atunci returneaza sorterul curent
        //astfel a creat tot lantul de sortere buttom-up apoi s-a intors si a returnat sorterul din lowes level 

        //GetEnumerator va folosi acum sorterul ca sa sorteze
        //metoda Sort din sorter intoarce un arei de indecsi sortati care reprezinta ordinea elementelor
        //deci este apelat sorter.Sorter(SourceColection) care primete ca parametru colectia sursa 

        //EnumerableSorter<TSource, TKey> class are la randul sau o base class EunumerableSorter<TSource> care este tipul fieldului Next (referitor la sorter) 






        public static IOrderedEnumerable<TSource> CustomOrderBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);

            return new OrderedEnumerable<TSource, TKey>(
                source,
                keySelector,
                comparer,
                null);
        }

        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(
            this IOrderedEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            return source.CreateOrderedEnumerable(keySelector, comparer, false);
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
}