using System.Collections;

namespace ObjectsCollection
{
    public class RadixTree<T> /*ICollection<IEnumerable<T>>*/
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

            ValidateNewKey(key, result.Child);

            if (IsPartialMatch(result.Child.Segment, result.SegmentIndex))
            {
                result.Child.Split(result.SegmentIndex);
            }

            if (IsPartialMatch(key, result.KeyIndex))
            {
                result.Child = result.Child.AddLeaf(GetRemainingPart(key, result.KeyIndex));
            }

            result.Child.IsEnd = true;
            Count++;
        }

        public void Clear()
        {
            root.Children.Clear();
            Count = 0;
        }

        //public void CopyTo(IEnumerable<T>[] array, int arrayIndex)
        //{
        //    ValidateArray(array, arrayIndex);

        //    foreach (IEnumerable<T> item in this)
        //    {
        //        array[arrayIndex] = item;
        //        arrayIndex++;
        //    }
        //}

        public bool Contains(IEnumerable<T> item)
        {
            List<T> key = ValidateAndConvertToList(item);

            SearchResult result = Search(key);

            return Contains(key, result.Child);
        }

        //public List<IEnumerable<T>> FindByPrefix(IEnumerable<T> prefix)
        //{
        //    List<IEnumerable<T>> items = [];
        //    List<T> key = ValidateAndConvertToList(prefix);

        //    (RadixTreeNode<T> parent, RadixTreeNode<T> node) = GetLastCommonNodes(key);
        //    (int segmentIndex, int keyIndex) = GetSplitIndexes(node, key);

        //    if (IsPartialMatch(key, keyIndex))
        //    {
        //        return items;
        //    }

        //    key.AddRange(GetRemainingPart(node.Segment, segmentIndex));

        //    items.AddRange(Traverse(node, key));

        //    return items;
        //}

        //public IEnumerator<IEnumerable<T>> GetEnumerator()
        //{
        //    return Traverse(root, []).GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}

        //public IEnumerable<T> GetPrefixMatch(IEnumerable<T> item)
        //{
        //    List<T> key = ValidateAndConvertToList(item);

        //    (RadixTreeNode<T> parent, RadixTreeNode<T> node) = GetLastCommonNodes(key);

        //    (int segmentIndex, int keyIndex) = GetSplitIndexes(node, key);

        //    return key.GetRange(0, keyIndex);
        //}

        //public bool Remove(IEnumerable<T> item)
        //{
        //    List<T> key = ValidateAndConvertToList(item);

        //    (RadixTreeNode<T> parent, RadixTreeNode<T> node) = GetLastCommonNodes(key);

        //    if (!Contains(key, node))
        //    {
        //        return false;
        //    }

        //    if (node.Children.Count == 0)
        //    {
        //        parent.Remove(node);
        //    }
        //    else
        //    {
        //        node.IsEnd = false;
        //    }

        //    Count--;
        //    return true;
        //}

        private static bool Contains(List<T> key, RadixTreeNode<T> node)
        {
            (int segmentIndex, int keyIndex) = GetSplitIndexes(node, key);
            bool keyEndsWithSegment = segmentIndex == node.Segment.Count && keyIndex == key.Count;

            return node.IsEnd && keyEndsWithSegment;
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

        private static (int segmentIndex, int keyIndex) GetSplitIndexes(RadixTreeNode<T> node, List<T> key)
        {
            int segmentIndex = 0;
            int keyIndex = node.PathLength - node.Segment.Count;
            int minLength = Math.Min(node.Segment.Count, key.Count - keyIndex);

            for (int i = 0; i < minLength; i++)
            {
                if (!node.Segment[i].Equals(key[keyIndex]))
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
                    (int currentSegmentIndex, int currentKeyIndex) = GetSplitIndexes(node, key);
                    if (currentSegmentIndex > 0)
                    {
                        parent = child;
                        child = node;
                        segmentIndex = currentSegmentIndex;
                        keyIndex = currentKeyIndex;

                        matchFound = true;
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

        private static void ValidateNewKey(List<T> item, RadixTreeNode<T> node)
        {
            if (!Contains(item, node))
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

        private class SearchResult
        {
            public SearchResult(RadixTreeNode<T> parent, RadixTreeNode<T> child, int segmentIndex, int keyIndex)
            {
                Parent = parent;
                Child = child;
                SegmentIndex = segmentIndex;
                KeyIndex = keyIndex;
            }

            public RadixTreeNode<T> Parent { get; }

            public RadixTreeNode<T> Child { get; set; }

            public int KeyIndex { get; }

            public int SegmentIndex { get; }
        }
    }
}
