using System.Collections;
using System.Xml.Linq;

namespace ObjectsCollection
{
    public class RadixTree<T>
        where T : IEnumerable<char>
    {
        readonly RadixTreeNode<T> root;

        public RadixTree()
        {
            root = new RadixTreeNode<T>();
            Count = 0;
            //IsReadOnly = false;
        }

        public int Count { get; private set; }

        public bool IsReadOnly { get; }

        public void Add(T item)
        {
            string key = item.ToString();

            (RadixTreeNode<T> parent, string prefix, int keyIndex) = GetLastCommonNode(key);
            RadixTreeNode<T> child = parent == null ? root : parent.Children[prefix];
            int commonPrefixLength = GetCommonPrefixLength(prefix, key, keyIndex);

            //ValidateArgumentIsNotNull(item);

            //ValidateNewItem(item, node);

            if (IsPartialPrefix(prefix, key, keyIndex))
            {
                keyIndex += commonPrefixLength;

                RadixTreeNode<T> newNode = new ();
                newNode.IsWord = child.IsWord;
                newNode.Children = child.Children;
                newNode.Value = child.Value;

                RadixTreeNode<T> splitNewNode = new ();
                splitNewNode.Children.Add(prefix[commonPrefixLength..], newNode);

                parent.Children.Add(prefix[..commonPrefixLength], splitNewNode);
                child = parent.Children[prefix[..commonPrefixLength]];
                parent.Children.Remove(prefix);
            }
            else
            {
                keyIndex += commonPrefixLength;
            }

            if (key.Length != commonPrefixLength)
            {
                RadixTreeNode<T> newNode = new ();
                newNode.IsWord = true;
                newNode.Value = item;

                child.Children.Add(key[keyIndex..], newNode);
            }
            else
            {
                child.IsWord = true;
                child.Value = item;
            }

            Count++;
        }

        private (RadixTreeNode<T> node, string prefix, int keyIndex) GetLastCommonNode(string key)
        {
            string prefix = "";
            int keyIndex = 0;
            int currentIndex = 0;
            bool hasPrefix = true;
            RadixTreeNode<T> previousNode = null;
            RadixTreeNode<T> currentNode = root;

            while (hasPrefix)
            {
                hasPrefix = false;
                foreach (var child in currentNode.Children)
                {
                    int prefixLength = GetCommonPrefixLength(child.Key, key, currentIndex);

                    if (prefixLength > 0)
                    {
                        hasPrefix = true;
                        previousNode = currentNode;
                        keyIndex = keyIndex + prefix.Length;

                        prefix = child.Key;
                        currentIndex += prefix.Length;
                        currentNode = child.Value;
                        break;
                    }
                }
            }

            return (previousNode, prefix, keyIndex);
        }

        private static int GetCommonPrefixLength(string prefix, string key, int keyIndex)
        {
            int minLength = Math.Min(prefix.Length, key.Length - keyIndex);
            int prefixLength = 0;

            for (int i = 0; i < minLength; i++)
            {
                if (prefix[i] != key[keyIndex++])
                {
                   break;
                }

                prefixLength++;
            }

            return prefixLength;
        }

        private static bool IsPartialPrefix(string prefix, string key, int keyIndex)
        {
            return prefix.Length > key.Length - keyIndex
                    || key.IndexOf(prefix, keyIndex, prefix.Length) != keyIndex;
        }

        public bool Contains(T item)
        {
            //ValidateArgumentIsNotNull(item);

            string key = item.ToString();

            (RadixTreeNode<T> parent, string prefix, int keyIndex) = GetLastCommonNode(key);
            RadixTreeNode<T> child = parent == null ? root : parent.Children[prefix];

            return item.Equals(child.Value);
        }

        public bool Remove(T item)
        {
            //ValidateArgumentIsNotNull(item);
            string key = item.ToString();
            (RadixTreeNode<T> parent, string prefix, int keyIndex) = GetLastCommonNode(key);
            RadixTreeNode<T> child = parent == null ? root : parent.Children[prefix];

            if (!child.IsWord || !key.EndsWith(prefix))
            {
                return false;
            }

            if (child.Children.Count == 0)
            {
                parent.Children.Remove(prefix);
            }
            else
            {
                child.IsWord = false;
                child.Value = default;
            }

            Count--;
            return true;
        }

        //    public void Add(string item)
        //    {
        //        ValidateArgumentIsNotNull(item);

        //        (RadixTreeNode previousNode, RadixTreeNode node) = GetLastCommonNodes(item);
        //        int splitIndex = GetCommonPrefixEndIndex(item, node.Prefix, node.PrefixPosition);

        //        ValidateNewItem(item, node);

        //        if (IsPartialPrefix(node, item))
        //        {
        //            SplitNode(node, splitIndex);
        //        }

        //        if (IsItemLongerThanPrefix(item, splitIndex))
        //        {
        //            RadixTreeNode newNode = new ();
        //            SetNode(newNode, item[splitIndex..], splitIndex, newNode.Children, true);
        //            node.Children.Add(item[splitIndex], newNode);
        //        }
        //        else
        //        {
        //            node.IsWord = true;
        //        }

        //        Count++;
        //    }

        //    public void Clear()
        //    {
        //        root.Children.Clear();
        //        Count = 0;
        //    }

        //    public bool Contains(string item)
        //    {
        //        ValidateArgumentIsNotNull(item);

        //        (RadixTreeNode previousNode, RadixTreeNode node) = GetLastCommonNodes(item);

        //        return Contains(item, node);
        //    }

        //    public void CopyTo(string[] array, int arrayIndex)
        //    {
        //        ValidateArray(array, arrayIndex);

        //        foreach (string item in this)
        //        {
        //            array[arrayIndex] = item;
        //            arrayIndex++;
        //        }
        //    }

        //    public bool Remove(string item)
        //    {
        //        ValidateArgumentIsNotNull(item);

        //        (RadixTreeNode previousNode, RadixTreeNode node) = GetLastCommonNodes(item);

        //        if (!node.IsWord || !item.EndsWith(node.Prefix))
        //        {
        //            return false;
        //        }

        //        if (node.Children.Count == 0)
        //        {
        //            previousNode.Children.Remove(item[node.PrefixPosition]);
        //        }
        //        else
        //        {
        //            node.IsWord = false;
        //        }

        //        Count--;
        //        return true;
        //    }

        //    public List<string> GetAllItemsByPrefix(string prefix)
        //    {
        //        ValidateArgumentIsNotNull(prefix);

        //        List<string> items = [];
        //        (RadixTreeNode previousNode, RadixTreeNode node) = GetLastCommonNodes(prefix);

        //        int splitIndex = GetCommonPrefixEndIndex(prefix, node.Prefix, node.PrefixPosition);

        //        if (prefix.Length != splitIndex)
        //        {
        //            return items;
        //        }

        //        prefix = prefix[..node.PrefixPosition] + node.Prefix;
        //        items.AddRange(Traverse(node, prefix));

        //        return items;
        //    }

        //    public string GetLongestPrefixMatch(string item)
        //    {
        //        ValidateArgumentIsNotNull(item);

        //        (RadixTreeNode previousNode, RadixTreeNode node) = GetLastCommonNodes(item);

        //        int splitIndex = GetCommonPrefixEndIndex(item, node.Prefix, node.PrefixPosition);

        //        return item[..splitIndex];
        //    }

        //    public IEnumerator<string> GetEnumerator()
        //    {
        //        return Traverse(root, "").GetEnumerator();
        //    }

        //    IEnumerator IEnumerable.GetEnumerator()
        //    {
        //        return GetEnumerator();
        //    }

        //    private static IEnumerable<string> Traverse(RadixTreeNode node, string prefix)
        //    {
        //        if (node.IsWord)
        //        {
        //            yield return prefix;
        //        }

        //        foreach (var child in node.Children)
        //        {
        //            foreach (string item in Traverse(child.Value, prefix + child.Value.Prefix))
        //            {
        //                yield return item;
        //            }
        //        }
        //    }

        //    private static int GetCommonPrefixEndIndex(string item, string prefix, int prefixPosition)
        //    {
        //        int minLength = Math.Min(prefix.Length, item.Length - prefixPosition);
        //        int endIndex = prefixPosition;

        //        for (int i = 0; i < minLength; i++)
        //        {
        //            if (prefix[i] == item[endIndex])
        //            {
        //                endIndex++;
        //            }
        //        }

        //        return endIndex;
        //    }

        //    private static bool Contains(string item, RadixTreeNode node)
        //    {
        //        return node.IsWord && item.EndsWith(node.Prefix) && (item.Length - node.PrefixPosition) == node.Prefix.Length;
        //    }

        //    private static bool IsPartialPrefix(RadixTreeNode node, string item)
        //    {
        //        return node.Prefix.Length > item.Length - node.PrefixPosition
        //                || item.IndexOf(node.Prefix, node.PrefixPosition, node.Prefix.Length) != node.PrefixPosition;
        //    }

        //    private static bool IsItemLongerThanPrefix(string item, int splitIndex)
        //    {
        //        return item.Length != splitIndex;
        //    }

        //    private static void SplitNode(RadixTreeNode node, int splitIndex)
        //    {
        //        int prefixSplitIndex = splitIndex - node.PrefixPosition;

        //        RadixTreeNode newNode = new ();
        //        SetNode(newNode, node.Prefix[prefixSplitIndex..], splitIndex, node.Children, node.IsWord);

        //        node.Children.Clear();
        //        node.Children.Add(newNode.Prefix[0], newNode);
        //        SetNode(node, node.Prefix[..prefixSplitIndex], node.PrefixPosition, node.Children, false);
        //    }

        //    private static void SetNode(RadixTreeNode node, string prefix, int prefixIndex, Dictionary<char, RadixTreeNode> children, bool isWord)
        //    {
        //        node.Prefix = prefix;
        //        node.PrefixPosition = prefixIndex;
        //        node.IsWord = isWord;
        //        node.Children = children.ToDictionary();
        //    }

        //    private static void ValidateArgumentIsNotNull(string item)
        //    {
        //        ArgumentNullException.ThrowIfNull(item);
        //    }

        //    private static void ValidateNewItem(string item, RadixTreeNode node)
        //    {
        //        if (!Contains(item, node))
        //        {
        //            return;
        //        }

        //        throw new ArgumentException("The item is already stored in the tree.");
        //    }

        //    private void ValidateArray(string[] array, int index)
        //    {
        //        ArgumentNullException.ThrowIfNull(array);
        //        ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);

        //        if (array.Length - index >= Count)
        //        {
        //            return;
        //        }

        //        throw new ArgumentException("The number of items in tree is greater than the space from index to end of array.");
        //    }

        //    private (RadixTreeNode previousNode, RadixTreeNode lastNode) GetLastCommonNodes(string item)
        //    {
        //        RadixTreeNode lastNode = root;
        //        RadixTreeNode previousNode = root;

        //        int index = 0;

        //        while (index < item.Length && lastNode.Children.TryGetValue(item[index], out RadixTreeNode node))
        //        {
        //            previousNode = lastNode;
        //            lastNode = node;

        //            if (IsPartialPrefix(node, item))
        //            {
        //                return (previousNode, lastNode);
        //            }

        //            index += node.Prefix.Length;
        //        }

        //        return (previousNode, lastNode);
        //    }
        //}
    }
}
