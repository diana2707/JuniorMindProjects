using System.Collections;

namespace ObjectsCollection
{
    //public class RadixTree<T> : ICollection<T>
    //    /*where T : IEnumerable<char>*/
    //{
    //    readonly RadixTreeNode<T> root;
    //    readonly IEnumerable<T> collection;

    //    public RadixTree(IEnumerable<T> collection)
    //    {
    //        this.collection = collection;

    //        root = new RadixTreeNode<T>();
    //        Count = 0;
    //        IsReadOnly = false;
    //    }

    //    public int Count { get; private set; }

    //    public bool IsReadOnly { get; }

    //    public void Add(T item)
    //    {
    //        ValidateItemIsNotNull(item);

    //        string key = GetKey(item);
    //        (RadixTreeNode<T> previousNode, RadixTreeNode<T> lastNode, string prefix, int keyIndex) = GetLastNodes(key);

    //        ValidateNewItem(item, lastNode);

    //        int commonPrefixLength = GetCommonPrefixLength(prefix, key, keyIndex);

    //        if (IsPartialMatch(prefix, commonPrefixLength))
    //        {
    //            lastNode = SplitPrefix(previousNode, lastNode, prefix, commonPrefixLength);
    //        }

    //        keyIndex += commonPrefixLength;

    //        if (IsPartialMatch(key, keyIndex))
    //        {
    //            lastNode = AddRemainingKey(lastNode, key, keyIndex);
    //        }

    //        lastNode.IsWord = true;
    //        lastNode.Value = item;

    //        Count++;
    //    }

    //    public void Clear()
    //    {
    //        root.Children.Clear();
    //        Count = 0;
    //    }

    //    public bool Contains(T item)
    //    {
    //        ValidateItemIsNotNull(item);

    //        string key = GetKey(item);

    //        (RadixTreeNode<T> parent, RadixTreeNode<T> child, string prefix, int keyIndex) = GetLastNodes(key);

    //        return Contains(item, child);
    //    }

    //    public void CopyTo(T[] array, int arrayIndex)
    //    {
    //        ValidateArray(array, arrayIndex);

    //        foreach (T item in this)
    //        {
    //            array[arrayIndex] = item;
    //            arrayIndex++;
    //        }
    //    }

    //    public List<T> GetAllItemsByPrefix(string commonKey)
    //    {
    //        ValidateKeyIsNotNull(commonKey);

    //        List<T> items = [];

    //        (RadixTreeNode<T> parent, RadixTreeNode<T> child, string prefix, int keyIndex) = GetLastNodes(commonKey);

    //        int prefixLength = GetCommonPrefixLength(prefix, commonKey, keyIndex);

    //        if (commonKey.Length != keyIndex + prefixLength)
    //        {
    //            return items;
    //        }

    //        items.AddRange(Traverse(child));

    //        return items;
    //    }

    //    public IEnumerator<T> GetEnumerator()
    //    {
    //        return Traverse(root).GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }

    //    public string GetLongestPrefixMatch(T item)
    //    {
    //        ValidateItemIsNotNull(item);

    //        string key = GetKey(item);

    //        (RadixTreeNode<T> parent, RadixTreeNode<T> child, string prefix, int keyIndex) = GetLastNodes(key);

    //        int commonPrefixLength = GetCommonPrefixLength(prefix, key, keyIndex);

    //        return key[.. (keyIndex + commonPrefixLength)];
    //    }

    //    public bool Remove(T item)
    //    {
    //        ValidateItemIsNotNull(item);

    //        string key = GetKey(item);

    //        (RadixTreeNode<T> parent, RadixTreeNode<T> child, string prefix, int keyIndex) = GetLastNodes(key);

    //        if (!child.IsWord || !key.EndsWith(prefix))
    //        {
    //            return false;
    //        }

    //        if (child.Children.Count == 0)
    //        {
    //            parent.Children.Remove(prefix);
    //        }
    //        else
    //        {
    //            child.IsWord = false;
    //            child.Value = default;
    //        }

    //        Count--;
    //        return true;
    //    }

    //    private static RadixTreeNode<T> AddRemainingKey(RadixTreeNode<T> node, string key, int keyIndex)
    //    {
    //        RadixTreeNode<T> newNode = new ();
    //        node.Children.Add(key[keyIndex..], newNode);
    //        return newNode;
    //    }

    //    private static bool Contains(T item, RadixTreeNode<T> node)
    //    {
    //        return item.Equals(node.Value);
    //    }

    //    private static int GetCommonPrefixLength(string prefix, string key, int keyIndex)
    //    {
    //        int minLength = Math.Min(prefix.Length, key.Length - keyIndex);
    //        int prefixLength = 0;

    //        for (int i = 0; i < minLength; i++)
    //        {
    //            if (prefix[i] != key[keyIndex++])
    //            {
    //                break;
    //            }

    //            prefixLength++;
    //        }

    //        return prefixLength;
    //    }

    //    private static string GetKey(T item)
    //    {
    //        return new string(item.ToArray());
    //    }

    //    private static bool HasPrefix(string prefix, string key, int keyIndex)
    //    {
    //        return keyIndex < key.Length && prefix[0] == key[keyIndex];
    //    }

    //    private static bool IsPartialMatch(string input, int splitIndex)
    //    {
    //        return input.Length > splitIndex;
    //    }

    //    private static RadixTreeNode<T> SplitPrefix(RadixTreeNode<T> parent, RadixTreeNode<T> child, string prefix, int splitIndex)
    //    {
    //        RadixTreeNode<T> newNode = new ()
    //        {
    //            IsWord = child.IsWord,
    //            Children = child.Children,
    //            Value = child.Value
    //        };

    //        RadixTreeNode<T> splitNode = new ();
    //        splitNode.Children.Add(prefix[splitIndex..], newNode);

    //        parent.Children.Add(prefix[..splitIndex], splitNode);
    //        parent.Children.Remove(prefix);

    //        return splitNode;
    //    }

    //    private static IEnumerable<T> Traverse(RadixTreeNode<T> node)
    //    {
    //        if (node.IsWord)
    //        {
    //            yield return node.Value;
    //        }

    //        foreach (var child in node.Children)
    //        {
    //            foreach (T item in Traverse(child.Value))
    //            {
    //                yield return item;
    //            }
    //        }
    //    }

    //    private static void ValidateItemIsNotNull(T item)
    //    {
    //        ArgumentNullException.ThrowIfNull(item);
    //    }

    //    private static void ValidateKeyIsNotNull(string key)
    //    {
    //        ArgumentNullException.ThrowIfNull(key);
    //    }

    //    private static void ValidateNewItem(T item, RadixTreeNode<T> node)
    //    {
    //        if (!Contains(item, node))
    //        {
    //            return;
    //        }

    //        throw new ArgumentException("The item is already stored in the tree.");
    //    }

    //    private void ValidateArray(T[] array, int index)
    //    {
    //        ArgumentNullException.ThrowIfNull(array);
    //        ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);

    //        if (array.Length - index >= Count)
    //        {
    //            return;
    //        }

    //        throw new ArgumentException("The number of items in tree is greater than the space from index to end of array.");
    //    }

    //    private (RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode, string prefix, int keyIndex) GetLastNodes(string key)
    //    {
    //        RadixTreeNode<T> previousNode = null;
    //        RadixTreeNode<T> currentNode = root;
    //        string prefix = string.Empty;
    //        int keyIndex = 0;

    //        bool matchFound = true;

    //        while (matchFound)
    //        {
    //            matchFound = false;

    //            foreach (var child in currentNode.Children)
    //            {
    //                if (HasPrefix(child.Key, key, keyIndex))
    //                {
    //                    previousNode = currentNode;
    //                    currentNode = child.Value;
    //                    prefix = child.Key;
    //                    keyIndex += prefix.Length;

    //                    matchFound = true;
    //                    break;
    //                }
    //            }
    //        }

    //        keyIndex -= prefix.Length;

    //        return (previousNode, currentNode, prefix, keyIndex);
    //    }
    //}
}
