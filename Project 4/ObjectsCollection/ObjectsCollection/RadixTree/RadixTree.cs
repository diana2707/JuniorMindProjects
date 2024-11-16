using System.Collections;

namespace ObjectsCollection
{
    public class RadixTree<T> : IEnumerable<IEnumerable<T>>
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

        public void Add(T item)
        {
            Add([item]);
        }

        public void Add(IEnumerable<T> key)
        {
            ValidateArgumentIsNotNull(key);

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> lastNode, IEnumerable<T> prefix, IEnumerable<T> remainingKey) = GetLastNodes(key);

            ValidateNewItem(prefix, remainingKey, lastNode.IsEnd);

            int commonPrefixLength = GetCommonPrefixLength(prefix, remainingKey);

            if (IsPartial(prefix, commonPrefixLength))
            {
                lastNode = SplitPrefix(previousNode, lastNode, prefix, commonPrefixLength);
            }

            if (IsPartial(remainingKey, commonPrefixLength))
            {
                lastNode = AddRemainingKey(lastNode, remainingKey.Skip(commonPrefixLength));
            }

            lastNode.IsEnd = true;

            Count++;
        }

        public void Clear()
        {
            root.Children.Clear();
            Count = 0;
        }

        public bool Contains(IEnumerable<T> key)
        {
            ValidateArgumentIsNotNull(key);

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> lastNode, IEnumerable<T> prefix, IEnumerable<T> remainingKey) = GetLastNodes(key);

            return Contains(remainingKey, prefix, lastNode.IsEnd);
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

        public List<IEnumerable<T>> GetAllItemsByPrefix(IEnumerable<T> commonKey)
        {
            ValidateArgumentIsNotNull(commonKey);

            List<IEnumerable<T>> items = [];

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> lastNode, IEnumerable<T> prefix, IEnumerable<T> remainingKey) = GetLastNodes(commonKey);

            int commonPrefixLength = GetCommonPrefixLength(prefix, remainingKey);

            if (remainingKey.Count() > commonPrefixLength)
            {
                return items;
            }

            commonKey = commonKey.Concat(prefix.Skip(commonPrefixLength));

            items.AddRange(Traverse(lastNode, commonKey));

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

        public IEnumerable<T> GetLongestPrefixMatch(IEnumerable<T> key)
        {
            ValidateArgumentIsNotNull(key);

            (RadixTreeNode<T> parent, RadixTreeNode<T> child, IEnumerable<T> prefix, IEnumerable<T> remainingKey) = GetLastNodes(key);

            int commonPrefixLength = GetCommonPrefixLength(prefix, remainingKey);

            return key.Take(key.Count() - remainingKey.Count() + commonPrefixLength);
        }

        public bool Remove(IEnumerable<T> key)
        {
            ValidateArgumentIsNotNull(key);

            (RadixTreeNode<T> parent, RadixTreeNode<T> child, IEnumerable<T> prefix, IEnumerable<T> remainingKey) = GetLastNodes(key);

            if (!child.IsEnd || !prefix.SequenceEqual(remainingKey))
            {
                return false;
            }

            if (child.Children.Count == 0)
            {
                parent.Children.Remove(prefix);
            }
            else
            {
                child.IsEnd = false;
            }

            Count--;
            return true;
        }

        private static RadixTreeNode<T> AddRemainingKey(RadixTreeNode<T> node, IEnumerable<T> key)
        {
            RadixTreeNode<T> newNode = new ();
            node.Children.Add(key, newNode);
            return newNode;
        }

        private static bool Contains(IEnumerable<T> prefix, IEnumerable<T> key, bool isEnd)
        {
            return isEnd && prefix.SequenceEqual(key);
        }

        private static int GetCommonPrefixLength(IEnumerable<T> prefix, IEnumerable<T> key)
        {
            IEnumerator<T> prefixEnumerator = prefix.GetEnumerator();
            IEnumerator<T> keyEnumerator = key.GetEnumerator();

            int prefixLength = 0;

            while (prefixEnumerator.MoveNext() && keyEnumerator.MoveNext())
            {
                if (!prefixEnumerator.Current.Equals(keyEnumerator.Current))
                {
                    break;
                }

                prefixLength++;
            }

            return prefixLength;
        }

        private static bool HasPrefix(IEnumerable<T> prefix, IEnumerable<T> key)
        {
            return GetCommonPrefixLength(prefix, key) > 0;
        }

        private static bool IsPartial(IEnumerable<T> input, int commonPrefixLength)
        {
            return input.Count() > commonPrefixLength;
        }

        private static RadixTreeNode<T> SplitPrefix(RadixTreeNode<T> parent, RadixTreeNode<T> child, IEnumerable<T> prefix, int commonPrefixLength)
        {
            RadixTreeNode<T> newNode = new ()
            {
                IsEnd = child.IsEnd,
                Children = child.Children
            };

            RadixTreeNode<T> splitNode = new ();
            splitNode.Children.Add(prefix.Skip(commonPrefixLength), newNode);

            parent.Children.Add(prefix.Take(commonPrefixLength), splitNode);
            parent.Children.Remove(prefix);

            return splitNode;
        }

        private static IEnumerable<IEnumerable<T>> Traverse(RadixTreeNode<T> node, IEnumerable<T> key)
        {
            if (node.IsEnd)
            {
                yield return key;
            }

            foreach (var child in node.Children)
            {
                foreach (IEnumerable<T> item in Traverse(child.Value, key.Concat(child.Key)))
                {
                    yield return item;
                }
            }
        }

        private static void ValidateArgumentIsNotNull(IEnumerable<T> key)
        {
            ArgumentNullException.ThrowIfNull(key);
        }

        private static void ValidateNewItem(IEnumerable<T> prefix, IEnumerable<T> key, bool isEnd)
        {
            if (!Contains(prefix, key, isEnd))
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

        private (RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode, IEnumerable<T> prefix, IEnumerable<T> remainingKey) GetLastNodes(IEnumerable<T> key)
        {
            RadixTreeNode<T> previousNode = null;
            RadixTreeNode<T> currentNode = root;
            IEnumerable<T> prefix = [];
            IEnumerable<T> remainingKey = key;
            int consumedKeyLength = 0;

            bool matchFound = true;

            while (matchFound)
            {
                matchFound = false;

                foreach (var child in currentNode.Children)
                {
                    if (HasPrefix(child.Key, remainingKey))
                    {
                        previousNode = currentNode;
                        currentNode = child.Value;
                        prefix = child.Key;
                        remainingKey = remainingKey.Skip(prefix.Count());
                        consumedKeyLength += prefix.Count();

                        matchFound = true;
                        break;
                    }
                }
            }

            consumedKeyLength -= prefix.Count();

            return (previousNode, currentNode, prefix, key.Skip(consumedKeyLength));
        }
    }
}
