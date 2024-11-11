using System.Collections;

namespace ObjectsCollection
{
    public class RadixTree<T> : IEnumerable<T>
        where T : IEnumerable<char>
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
            ValidateItemIsNotNull(item);

            string key = GetKey(item);

            (RadixTreeNode<T> parent, string prefix, int keyIndex) = GetLastCommonNode(key);
            RadixTreeNode<T> child = parent == null ? root : parent.Children[prefix];

            int commonPrefixLength = GetCommonPrefixLength(prefix, key, keyIndex);

            ValidateNewItem(item, child);

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

        public bool Contains(T item)
        {
            ValidateItemIsNotNull(item);

            string key = GetKey(item);

            (RadixTreeNode<T> parent, string prefix, int keyIndex) = GetLastCommonNode(key);
            RadixTreeNode<T> child = parent == null ? root : parent.Children[prefix];

            return Contains(item, child);
        }

        public bool Remove(T item)
        {
            ValidateItemIsNotNull(item);

            string key = GetKey(item);

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

        public IEnumerator<T> GetEnumerator()
        {
            return Traverse(root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<T> GetAllItemsByPrefix(string commonKey)
        {
            ValidateKeyIsNotNull(commonKey);

            List<T> items = [];

            (RadixTreeNode<T> parent, string prefix, int keyIndex) = GetLastCommonNode(commonKey);
            RadixTreeNode<T> child = parent == null ? root : parent.Children[prefix];

            int prefixLength = GetCommonPrefixLength(prefix, commonKey, keyIndex);

            if (commonKey.Length != keyIndex + prefixLength)
            {
                return items;
            }

            items.AddRange(Traverse(child));

            return items;
        }

        public string GetLongestPrefixMatch(T item)
        {
            ValidateItemIsNotNull(item);

            string key = GetKey(item);

            (RadixTreeNode<T> parent, string prefix, int keyIndex) = GetLastCommonNode(key);

            int commonPrefixLength = GetCommonPrefixLength(prefix, key, keyIndex);

            return key[.. (keyIndex + commonPrefixLength)];
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ValidateArray(array, arrayIndex);

            foreach (T item in this)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        public void Clear()
        {
            root.Children.Clear();
            Count = 0;
        }

        private static bool Contains(T item, RadixTreeNode<T> node)
        {
            return item.Equals(node.Value);
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

        private static string GetKey(T item)
        {
            return item.ToString();
        }

        private (RadixTreeNode<T> node, string prefix, int keyIndex) GetLastCommonNode(string key)
        {
            string prefix = "";
            int keyIndex = 0;
            int currentIndex = 0;
            bool hasChildren = true;
            RadixTreeNode<T> previousNode = null;
            RadixTreeNode<T> currentNode = root;

            while (hasChildren)
            {
                hasChildren = false;
                foreach (var child in currentNode.Children)
                {
                    if (HasPrefix(child.Key, key, currentIndex))
                    {
                        hasChildren = true;
                        previousNode = currentNode;
                        keyIndex += prefix.Length;

                        prefix = child.Key;
                        currentIndex += prefix.Length;
                        currentNode = child.Value;
                        break;
                    }
                }
            }

            return (previousNode, prefix, keyIndex);
        }

        private static bool IsPartialPrefix(string prefix, string key, int keyIndex)
        {
            return prefix.Length > key.Length - keyIndex
                    || key.IndexOf(prefix, keyIndex, prefix.Length) == -1;
        }

        private static bool HasPrefix(string prefix, string key, int keyIndex)
        {
            return keyIndex < key.Length && prefix[0] == key[keyIndex];
        }

        private static IEnumerable<T> Traverse(RadixTreeNode<T> node)
        {
            if (node.IsWord)
            {
                yield return node.Value;
            }

            foreach (var child in node.Children)
            {
                foreach (T item in Traverse(child.Value))
                {
                    yield return item;
                }
            }
        }

        private static void ValidateItemIsNotNull(T item)
        {
            ArgumentNullException.ThrowIfNull(item);
        }

        private static void ValidateKeyIsNotNull(string key)
        {
            ArgumentNullException.ThrowIfNull(key);
        }

        private static void ValidateNewItem(T item, RadixTreeNode<T> node)
        {
            if (!Contains(item, node))
            {
                return;
            }

            throw new ArgumentException("The item is already stored in the tree.");
        }

        private void ValidateArray(T[] array, int index)
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
