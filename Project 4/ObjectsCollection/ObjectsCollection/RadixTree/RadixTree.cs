using System.Collections;

namespace ObjectsCollection
{
    public class RadixTree<T> : ICollection<IEnumerable<T>>
        where T : IEquatable<T>
    {
        readonly RadixTreeNode<T> root;

        public RadixTree()
        {
            root = new RadixTreeNode<T>();
            Count = 0;
            IsReadOnly = false;
        }

        public int Count { get; private set; }

        public bool IsReadOnly { get; }

        public void Add(IEnumerable<T> item)
        {
            List<T> key = ValidateAndConvertToList(item);

            SearchResult result = Search(key);

            ValidateNewKey(key, result);

            if (IsPartialMatch(result.Child.Segment, result.SegmentIndex))
            {
                result.Child.Split(result.SegmentIndex);
            }

            if (IsPartialMatch(key, result.KeyIndex))
            {
                result.Child.AddLeaf(GetRemainingPart(key, result.KeyIndex));
            }
            else
            {
                result.Child.IsEnd = true;
            }

            Count++;
        }

        public void Clear()
        {
            root.Children.Clear();
            Count = 0;
        }

        public void CopyTo(IEnumerable<T>[] array, int arrayIndex)
        {
            ValidateArray(array, arrayIndex);

            foreach (IEnumerable<T> item in this)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        public bool Contains(IEnumerable<T> item)
        {
            List<T> key = ValidateAndConvertToList(item);

            SearchResult result = Search(key);

            return Contains(key, result);
        }

        public List<IEnumerable<T>> FindByPrefix(IEnumerable<T> prefix)
        {
            List<IEnumerable<T>> items = [];
            List<T> key = ValidateAndConvertToList(prefix);

            SearchResult result = Search(key);

            if (IsPartialMatch(key, result.KeyIndex))
            {
                return items;
            }

            key.AddRange(GetRemainingPart(result.Child.Segment, result.SegmentIndex));

            items.AddRange(Traverse(result.Child, key));

            return items;
        }

        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            return Traverse(root, []).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<T> GetPrefixMatch(IEnumerable<T> item)
        {
            List<T> key = ValidateAndConvertToList(item);

            SearchResult result = Search(key);

            return key.GetRange(0, result.KeyIndex);
        }

        public bool Remove(IEnumerable<T> item)
        {
            List<T> key = ValidateAndConvertToList(item);

            SearchResult result = Search(key);

            if (!Contains(key, result))
            {
                return false;
            }

            if (result.Child.Children.Count == 0)
            {
                result.Parent.Remove(result.Child);
            }
            else
            {
                result.Child.IsEnd = false;
            }

            Count--;
            return true;
        }

        private static bool Contains(List<T> key, SearchResult result)
        {
            bool keyEndsWithSegment = result.SegmentIndex == result.Child.Segment.Count
                && result.KeyIndex == key.Count;

            return result.Child.IsEnd && keyEndsWithSegment;
        }

        private static IEnumerable<IEnumerable<T>> Traverse(RadixTreeNode<T> node, IEnumerable<T> key)
        {
            if (node.IsEnd)
            {
                yield return key;
            }

            foreach (var child in node.Children)
            {
                foreach (IEnumerable<T> item in Traverse(child, key.Concat(child.Segment)))
                {
                    yield return item;
                }
            }
        }

        private static (int segmentIndex, int keyIndex) GetSplitIndexes(List<T> segment, List<T> key, int keyIndex)
        {
            int segmentIndex = 0;
            int minLength = Math.Min(segment.Count, key.Count - keyIndex);

            for (int i = 0; i < minLength; i++)
            {
                if (!segment[i].Equals(key[keyIndex]))
                {
                    break;
                }

                segmentIndex++;
                keyIndex++;
            }

            return (segmentIndex, keyIndex);
        }

        private SearchResult Search(List<T> key)
        {
            RadixTreeNode<T> parent = null;
            RadixTreeNode<T> child = root;
            int segmentIndex = 0;
            int keyIndex = 0;

            bool matchFound = true;

            while (matchFound)
            {
                matchFound = false;

                foreach (var node in child.Children)
                {
                    (int currentSegmentIndex, int currentKeyIndex) = GetSplitIndexes(node.Segment, key, keyIndex);
                    if (currentSegmentIndex > 0)
                    {
                        parent = child;
                        child = node;
                        segmentIndex = currentSegmentIndex;
                        keyIndex = currentKeyIndex;

                        matchFound = segmentIndex == node.Segment.Count;
                        break;
                    }
                }
            }

            return new SearchResult(parent, child, segmentIndex, keyIndex);
        }

        private static List<T> GetRemainingPart(List<T> input, int splitIndex)
        {
            return input.GetRange(splitIndex, input.Count - splitIndex);
        }

        private static bool IsPartialMatch(List<T> input, int commonPrefixLength)
        {
            return input.Count > commonPrefixLength;
        }

        private static List<T> ValidateAndConvertToList(IEnumerable<T> item)
        {
            ArgumentNullException.ThrowIfNull(item);
            return item.ToList();
        }

        private static void ValidateNewKey(List<T> key, SearchResult result)
        {
            if (!Contains(key, result))
            {
                return;
            }

            throw new ArgumentException("The item is already stored in the tree.");
        }

        private void ValidateArray(IEnumerable<T>[] array, int index)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);

            if (array.Length - index >= Count)
            {
                return;
            }

            throw new ArgumentException("The number of items in tree is greater than the space from index to end of array.");
        }

        readonly struct SearchResult
        {
            public SearchResult(RadixTreeNode<T> parent, RadixTreeNode<T> child, int segmentIndex, int keyIndex)
            {
                Parent = parent;
                Child = child;
                SegmentIndex = segmentIndex;
                KeyIndex = keyIndex;
            }

            public RadixTreeNode<T> Parent { get; }

            public RadixTreeNode<T> Child { get; }

            public int KeyIndex { get; }

            public int SegmentIndex { get; }
        }
    }
}
