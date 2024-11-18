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
            //ValidateArgumentIsNotNull(item);

            List<T> itemList = item.ToList();

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> lastNode, List<T> remainingItem) = GetLastCommonNodes(itemList);

            //ValidateNewItem(key, remainingItem, lastNode.IsEnd);

            int splitIndex = GetCommonPrefixLength(lastNode.Prefix, remainingItem);

            if (IsPartialMatch(lastNode.Prefix, splitIndex))
            {
                SplitNode(splitIndex, previousNode, lastNode);
            }

            if (IsPartialMatch(remainingItem, splitIndex))
            {
                lastNode = AddNode(remainingItem, splitIndex, lastNode);
            }

            lastNode.IsEnd = true;

            Count++;
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

                        item = child.Prefix.Count < item.Count ? item.GetRange(child.Prefix.Count, item.Count - child.Prefix.Count) : []; /*item.Skip(child.Prefix.Count());*/
                        matchFound = true;

                        break;
                    }
                }
            }

            return (previousNode, currentNode, remainingItem);
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

        private static bool HasCommonPrefix(List<T> prefix, List<T> item)
        {
            return GetCommonPrefixLength(prefix, item) > 0;
        }

        private static bool IsPartialMatch(List<T> input, int commonPrefixLength)
        {
            return input.Count > commonPrefixLength;
        }

        private static void SplitNode(int splitIndex, RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode)
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

        private static RadixTreeNode<T> AddNode(List<T> prefix, int splitIndex, RadixTreeNode<T> parent)
        {
            RadixTreeNode<T> newNode = new ();
            newNode.Prefix = prefix.GetRange(splitIndex, prefix.Count - splitIndex);

            parent.Children.Add(newNode);
            return newNode;
        }

    }


    //public class RadixTree<T> : ICollection<IEnumerable<T>>
    //    where T : IEquatable<T>
    //{
    //    readonly RadixTreeNode<T> root;

    //    public RadixTree()
    //    {
    //        root = new RadixTreeNode<T>();
    //        Count = 0;
    //        IsReadOnly = false;
    //    }

    //    public int Count { get; private set; }

    //    public bool IsReadOnly { get; }

    //    public void Add(IEnumerable<T> item)
    //    {
    //        ValidateArgumentIsNotNull(item);

    //        (RadixTreeNode<T> previousNode, RadixTreeNode<T> lastNode, IEnumerable<T> key, IEnumerable<T> remainingItem) = GetLastNodes(item);

    //        ValidateNewItem(key, remainingItem, lastNode.IsEnd);

    //        int commonPrefixLength = GetCommonPrefixLength(key, remainingItem);

    //        if (IsPartialMatch(key, commonPrefixLength))
    //        {
    //            lastNode = SplitKey(key, commonPrefixLength, previousNode, lastNode);
    //        }

    //        if (IsPartialMatch(remainingItem, commonPrefixLength))
    //        {
    //            lastNode = AddRemainingItem(remainingItem, commonPrefixLength, lastNode);
    //        }

    //        lastNode.IsEnd = true;

    //        Count++;
    //    }

    //    public void Clear()
    //    {
    //        root.Children.Clear();
    //        Count = 0;
    //    }

    //    public bool Contains(IEnumerable<T> item)
    //    {
    //        ValidateArgumentIsNotNull(item);

    //        (RadixTreeNode<T> previousNode, RadixTreeNode<T> lastNode, IEnumerable<T> key, IEnumerable<T> remainingItem) = GetLastNodes(item);

    //        return Contains(key, remainingItem, lastNode.IsEnd);
    //    }

    //    public void CopyTo(IEnumerable<T>[] array, int arrayIndex)
    //    {
    //        ValidateArray(array, arrayIndex);

    //        foreach (IEnumerable<T> item in this)
    //        {
    //            array[arrayIndex] = item;
    //            arrayIndex++;
    //        }
    //    }

    //    public List<IEnumerable<T>> GetAllItemsByPrefix(IEnumerable<T> prefix)
    //    {
    //        ValidateArgumentIsNotNull(prefix);

    //        List<IEnumerable<T>> items = [];

    //        (RadixTreeNode<T> previousNode, RadixTreeNode<T> lastNode, IEnumerable<T> key, IEnumerable<T> remainingPrefix) = GetLastNodes(prefix);

    //        int commonPrefixLength = GetCommonPrefixLength(key, remainingPrefix);

    //        if (remainingPrefix.Count() > commonPrefixLength)
    //        {
    //            return items;
    //        }

    //        prefix = prefix.Concat(key.Skip(commonPrefixLength));

    //        items.AddRange(Traverse(lastNode, prefix));

    //        return items;
    //    }

    //    public IEnumerator<IEnumerable<T>> GetEnumerator()
    //    {
    //        return Traverse(root, []).GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }

    //    public IEnumerable<T> GetLongestPrefixMatch(IEnumerable<T> item)
    //    {
    //        ValidateArgumentIsNotNull(item);

    //        (RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode, IEnumerable<T> key, IEnumerable<T> remainingItem) = GetLastNodes(item);

    //        int commonPrefixLength = GetCommonPrefixLength(key, remainingItem);

    //        return item.Take(item.Count() - remainingItem.Count() + commonPrefixLength);
    //    }

    //    public bool Remove(IEnumerable<T> item)
    //    {
    //        ValidateArgumentIsNotNull(item);

    //        (RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode, IEnumerable<T> key, IEnumerable<T> remainingItem) = GetLastNodes(item);

    //        if (!Contains(key, remainingItem, currentNode.IsEnd))
    //        {
    //            return false;
    //        }

    //        if (currentNode.Children.Count == 0)
    //        {
    //            previousNode.Children.Remove(key);
    //        }
    //        else
    //        {
    //            currentNode.IsEnd = false;
    //        }

    //        Count--;
    //        return true;
    //    }

    //    private static RadixTreeNode<T> AddRemainingItem(IEnumerable<T> item, int commonPrefixLength, RadixTreeNode<T> node)
    //    {
    //        RadixTreeNode<T> newNode = new ();
    //        node.Children.Add(item.Skip(commonPrefixLength), newNode);
    //        return newNode;
    //    }

    //    private static bool Contains(IEnumerable<T> key, IEnumerable<T> item, bool isEnd)
    //    {
    //        return isEnd && key.SequenceEqual(item);
    //    }

    //    private static int GetCommonPrefixLength(IEnumerable<T> key, IEnumerable<T> item)
    //    {
    //        IEnumerator<T> keyEnumerator = key.GetEnumerator();
    //        IEnumerator<T> itemEnumerator = item.GetEnumerator();

    //        int prefixLength = 0;

    //        while (keyEnumerator.MoveNext() && itemEnumerator.MoveNext())
    //        {
    //            if (!keyEnumerator.Current.Equals(itemEnumerator.Current))
    //            {
    //                break;
    //            }

    //            prefixLength++;
    //        }

    //        return prefixLength;
    //    }

    //    private static bool HasCommonPrefix(IEnumerable<T> key, IEnumerable<T> item)
    //    {
    //        return GetCommonPrefixLength(key, item) > 0;
    //    }

    //    private static bool IsPartialMatch(IEnumerable<T> input, int commonPrefixLength)
    //    {
    //        return input.Count() > commonPrefixLength;
    //    }

    //    private static RadixTreeNode<T> SplitKey(IEnumerable<T> key, int commonPrefixLength, RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode)
    //    {
    //        RadixTreeNode<T> newNode = new ()
    //        {
    //            IsEnd = currentNode.IsEnd,
    //            Children = currentNode.Children
    //        };

    //        RadixTreeNode<T> splitNode = new ();
    //        splitNode.Children.Add(key.Skip(commonPrefixLength), newNode);

    //        previousNode.Children.Add(key.Take(commonPrefixLength), splitNode);
    //        previousNode.Children.Remove(key);

    //        return splitNode;
    //    }

    //    private static IEnumerable<IEnumerable<T>> Traverse(RadixTreeNode<T> node, IEnumerable<T> key)
    //    {
    //        if (node.IsEnd)
    //        {
    //            yield return key;
    //        }

    //        foreach (var child in node.Children)
    //        {
    //            foreach (IEnumerable<T> item in Traverse(child.Value, key.Concat(child.Key)))
    //            {
    //                yield return item;
    //            }
    //        }
    //    }

    //    private static void ValidateArgumentIsNotNull(IEnumerable<T> item)
    //    {
    //        ArgumentNullException.ThrowIfNull(item);
    //    }

    //    private static void ValidateNewItem(IEnumerable<T> key, IEnumerable<T> item, bool isEnd)
    //    {
    //        if (!Contains(key, item, isEnd))
    //        {
    //            return;
    //        }

    //        throw new ArgumentException("The item is already stored in the tree.");
    //    }

    //    private void ValidateArray(IEnumerable<T>[] array, int index)
    //    {
    //        ArgumentNullException.ThrowIfNull(array);
    //        ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);

    //        if (array.Length - index >= Count)
    //        {
    //            return;
    //        }

    //        throw new ArgumentException("The number of items in tree is greater than the space from index to end of array.");
    //    }

    //    private (RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode, IEnumerable<T> key, IEnumerable<T> remainingItem) GetLastNodes(IEnumerable<T> item)
    //    {
    //        RadixTreeNode<T> previousNode = null;
    //        RadixTreeNode<T> currentNode = root;
    //        IEnumerable<T> key = [];
    //        IEnumerable<T> remainingItem = item;

    //        bool matchFound = true;

    //        while (matchFound)
    //        {
    //            matchFound = false;

    //            foreach (var child in currentNode.Children)
    //            {
    //                if (HasCommonPrefix(child.Key, item))
    //                {
    //                    previousNode = currentNode;
    //                    currentNode = child.Value;
    //                    key = child.Key;
    //                    remainingItem = item;

    //                    item = item.Skip(key.Count());
    //                    matchFound = true;

    //                    break;
    //                }
    //            }
    //        }

    //        return (previousNode, currentNode, key, remainingItem);
    //    }
    //}
}
