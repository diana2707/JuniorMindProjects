using System.Collections;

namespace ObjectsCollection
{
    public class RadixTree : IEnumerable<string>
    {
        readonly RadixTreeNode root;

        public RadixTree()
        {
            root = new RadixTreeNode();
            Count = 0;
            IsReadOnly = false;
        }

        public int Count { get; private set; }

        public bool IsReadOnly { get; }

        public void Add(string item)
        {
            ValidateArgumentIsNotNull(item);

            (RadixTreeNode previousNode, RadixTreeNode node) = GetLastCommonNodes(item);
            int splitIndex = GetCommonPrefixEndIndex(item, node.Prefix, node.PrefixPosition);

            ValidateNewItem(item, node);

            if (IsPartialPrefix(node, item))
            {
                SplitNode(node, splitIndex);
            }

            if (IsItemLongerThanPrefix(item, splitIndex))
            {
                RadixTreeNode newNode = new ();
                SetNode(newNode, item[splitIndex..], splitIndex, newNode.Children, true);
                node.Children.Add(item[splitIndex], newNode);
            }
            else
            {
                node.IsWord = true;
            }

            Count++;
        }

        public bool Contains(string item)
        {
            ValidateArgumentIsNotNull(item);

            (RadixTreeNode previousNode, RadixTreeNode node) = GetLastCommonNodes(item);

            return Contains(item, node);
        }

        public bool Remove(string item)
        {
            ValidateArgumentIsNotNull(item);

            (RadixTreeNode previousNode, RadixTreeNode node) = GetLastCommonNodes(item);

            if (!node.IsWord || !item.EndsWith(node.Prefix))
            {
                return false;
            }

            if (node.Children.Count == 0)
            {
                previousNode.Children.Remove(item[node.PrefixPosition]);
            }
            else
            {
                node.IsWord = false;
            }

            Count--;
            return true;
        }

        public List<string> GetAllItemsByPrefix(string prefix)
        {
            ValidateArgumentIsNotNull(prefix);

            List<string> items = [];
            (RadixTreeNode previousNode, RadixTreeNode node) = GetLastCommonNodes(prefix);

            int splitIndex = GetCommonPrefixEndIndex(prefix, node.Prefix, node.PrefixPosition);

            if (prefix.Length != splitIndex)
            {
                return items;
            }

            prefix = prefix[..node.PrefixPosition] + node.Prefix;
            items.AddRange(Traverse(node, prefix));

            return items;
        }

        public string GetLongestPrefixMatch(string item)
        {
            ValidateArgumentIsNotNull(item);

            (RadixTreeNode previousNode, RadixTreeNode node) = GetLastCommonNodes(item);

            int splitIndex = GetCommonPrefixEndIndex(item, node.Prefix, node.PrefixPosition);

            return item[..splitIndex];
        }

        public IEnumerator<string> GetEnumerator()
        {
            return Traverse(root, "").GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static IEnumerable<string> Traverse(RadixTreeNode node, string prefix)
        {
            if (node.IsWord)
            {
                yield return prefix;
            }

            foreach (var child in node.Children)
            {
                foreach (string item in Traverse(child.Value, prefix + child.Value.Prefix))
                {
                    yield return item;
                }
            }
        }

        private static int GetCommonPrefixEndIndex(string item, string prefix, int prefixPosition)
        {
            int minLength = Math.Min(prefix.Length, item.Length - prefixPosition);
            int endIndex = prefixPosition;

            for (int i = 0; i < minLength; i++)
            {
                if (prefix[i] == item[endIndex])
                {
                    endIndex++;
                }
            }

            return endIndex;
        }

        private static bool Contains(string item, RadixTreeNode node)
        {
            return node.IsWord && item.EndsWith(node.Prefix);
        }

        private static bool IsPartialPrefix(RadixTreeNode node, string item)
        {
            return node.Prefix.Length > item.Length - node.PrefixPosition
                    || item.IndexOf(node.Prefix, node.PrefixPosition, node.Prefix.Length) != node.PrefixPosition;
        }

        private static bool IsItemLongerThanPrefix(string item, int splitIndex)
        {
            return item.Length != splitIndex;
        }

        private static void SplitNode(RadixTreeNode node, int splitIndex)
        {
            int prefixSplitIndex = splitIndex - node.PrefixPosition;

            RadixTreeNode newNode = new ();
            SetNode(newNode, node.Prefix[prefixSplitIndex..], splitIndex, node.Children, node.IsWord);

            node.Children.Clear();
            node.Children.Add(newNode.Prefix[0], newNode);
            SetNode(node, node.Prefix[..prefixSplitIndex], node.PrefixPosition, node.Children, false);
        }

        private static void SetNode(RadixTreeNode node, string prefix, int prefixIndex, Dictionary<char, RadixTreeNode> children, bool isWord)
        {
            node.Prefix = prefix;
            node.PrefixPosition = prefixIndex;
            node.IsWord = isWord;
            node.Children = children.ToDictionary();
        }

        private static void ValidateArgumentIsNotNull(string item)
        {
            ArgumentNullException.ThrowIfNull(item);
        }

        private static void ValidateNewItem(string item, RadixTreeNode node)
        {
            if (!Contains(item, node))
            {
                return;
            }

            throw new ArgumentException("The item is already stored in the tree.");
        }

        private (RadixTreeNode previousNode, RadixTreeNode lastNode) GetLastCommonNodes(string item)
        {
            RadixTreeNode lastNode = root;
            RadixTreeNode previousNode = root;

            int index = 0;

            while (index < item.Length && lastNode.Children.TryGetValue(item[index], out RadixTreeNode node))
            {
                previousNode = lastNode;
                lastNode = node;

                if (IsPartialPrefix(node, item))
                {
                    return (previousNode, lastNode);
                }

                index += node.Prefix.Length;
            }

            return (previousNode, lastNode);
        }
    }
}
