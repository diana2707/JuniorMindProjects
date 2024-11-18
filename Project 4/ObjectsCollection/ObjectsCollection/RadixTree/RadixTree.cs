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

        public void Add(IEnumerable<T> item)
        {
            ValidateArgumentIsNotNull(item);

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> lastNode, List<T> remainingItem) = GetLastCommonNodes(item.ToList());

            ValidateNewItem(remainingItem, lastNode);

            Add(remainingItem, lastNode);
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
            ValidateArgumentIsNotNull(item);

            List<T> itemList = item.ToList();

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> lastNode, List<T> remainingItem) = GetLastCommonNodes(itemList);

            return Contains(remainingItem, lastNode);
        }

        public List<IEnumerable<T>> GetAllItemsByPrefix(IEnumerable<T> prefix)
        {
            ValidateArgumentIsNotNull(prefix);

            List<IEnumerable<T>> items = [];

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> lastNode, List<T> remainingPrefix) = GetLastCommonNodes(prefix.ToList());

            int commonPrefixLength = GetCommonPrefixLength(lastNode.Prefix, remainingPrefix);

            if (remainingPrefix.Count > commonPrefixLength)
            {
                return items;
            }

            prefix = prefix.Concat(lastNode.Prefix.Skip(commonPrefixLength));

            items.AddRange(Traverse(lastNode, prefix));

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

        public IEnumerable<T> GetLongestPrefixMatch(IEnumerable<T> item)
        {
            ValidateArgumentIsNotNull(item);

            List<T> itemList = item.ToList();

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode, List<T> remainingItem) = GetLastCommonNodes(itemList);

            int commonPrefixLength = GetCommonPrefixLength(currentNode.Prefix, remainingItem);

            return itemList.GetRange(0, itemList.Count - remainingItem.Count + commonPrefixLength);
        }

        public bool Remove(IEnumerable<T> item)
        {
            ValidateArgumentIsNotNull(item);

            List<T> itemList = item.ToList();

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode, List<T> remainingItem) = GetLastCommonNodes(itemList);

            if (!Contains(remainingItem, currentNode))
            {
                return false;
            }

            if (currentNode.Children.Count == 0)
            {
                previousNode.Children.Remove(currentNode);
            }
            else
            {
                currentNode.IsEnd = false;
            }

            Count--;
            return true;
        }

        private void Add(List<T> item, RadixTreeNode<T> node)
        {
            int splitIndex = GetCommonPrefixLength(node.Prefix, item);

            if (IsPartialMatch(node.Prefix, splitIndex))
            {
                SplitNode(splitIndex, node);
            }

            if (IsPartialMatch(item, splitIndex))
            {
                node = AddNode(item, splitIndex, node);
            }

            node.IsEnd = true;

            Count++;
        }

        private static RadixTreeNode<T> AddNode(List<T> prefix, int splitIndex, RadixTreeNode<T> parent)
        {
            RadixTreeNode<T> newNode = new ();
            newNode.Prefix = prefix.GetRange(splitIndex, prefix.Count - splitIndex);

            parent.Children.Add(newNode);
            return newNode;
        }

        private static bool Contains(List<T> item, RadixTreeNode<T> node)
        {
            return node.IsEnd && node.Prefix.SequenceEqual(item);
        }

        private static IEnumerable<IEnumerable<T>> Traverse(RadixTreeNode<T> node, IEnumerable<T> prefix)
        {
            if (node.IsEnd)
            {
                yield return prefix;
            }

            foreach (var child in node.Children)
            {
                foreach (IEnumerable<T> item in Traverse(child, prefix.Concat(child.Prefix)))
                {
                    yield return item;
                }
            }
        }

        private static int GetCommonPrefixLength(List<T> key, List<T> item)
        {
            int prefixLength = 0;
            int minLength = key.Count < item.Count ? key.Count : item.Count;

            for (int i = 0; i < minLength; i++)
            {
                if (!key[i].Equals(item[i]))
                {
                    break;
                }

                prefixLength++;
            }

            return prefixLength;
        }

        private (RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode, List<T> remainingItem) GetLastCommonNodes(List<T> item)
        {
            RadixTreeNode<T> previousNode = null;
            RadixTreeNode<T> currentNode = root;
            List<T> remainingItem = item;

            bool matchFound = true;

            while (matchFound)
            {
                matchFound = false;

                foreach (var child in currentNode.Children)
                {
                    if (HasCommonPrefix(child.Prefix, item))
                    {
                        previousNode = currentNode;
                        currentNode = child;
                        remainingItem = item;

                        item = child.Prefix.Count < item.Count ?
                            item.GetRange(child.Prefix.Count, item.Count - child.Prefix.Count)
                            : [];
                        matchFound = true;

                        break;
                    }
                }
            }

            return (previousNode, currentNode, remainingItem);
        }

        private static bool HasCommonPrefix(List<T> prefix, List<T> item)
        {
            return GetCommonPrefixLength(prefix, item) > 0;
        }

        private static bool IsPartialMatch(List<T> input, int commonPrefixLength)
        {
            return input.Count > commonPrefixLength;
        }

        private static void SplitNode(int splitIndex, RadixTreeNode<T> currentNode)
        {
            //RadixTreeNode<T> newNode = AddNode(currentNode.Prefix, splitIndex, currentNode);

            RadixTreeNode<T> newNode = new ()
            {
                Prefix = currentNode.Prefix.GetRange(splitIndex, currentNode.Prefix.Count - splitIndex),
                Children = currentNode.Children.ToList(),
                IsEnd = currentNode.IsEnd
            };

            currentNode.Children.Clear();
            currentNode.Children.Add(newNode);
            currentNode.Prefix = currentNode.Prefix.GetRange(0, splitIndex);
            currentNode.IsEnd = false;
        }

        private static void ValidateArgumentIsNotNull(IEnumerable<T> item)
        {
            ArgumentNullException.ThrowIfNull(item);
        }

        private static void ValidateNewItem(List<T> item, RadixTreeNode<T> node)
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
    }
}
