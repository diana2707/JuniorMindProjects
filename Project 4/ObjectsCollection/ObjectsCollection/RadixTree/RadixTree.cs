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
        }

        public int Count { get; private set; }

        public void Add(string item)
        {
            ValidateArgumentIsNotNull(item);

            (RadixTreeNode previousNode, RadixTreeNode node, int lastNodeIndex) = GetLastCommonNodes(item);
            int itemIndex = lastNodeIndex;

            ValidateNewItem(item, node);

            if (IsPartialPrefix(node, item, lastNodeIndex))
            {
                itemIndex = GetEndIndex(node.Prefix, item, lastNodeIndex);
                int prefixIndex = itemIndex - lastNodeIndex;

                RadixTreeNode splitNode = new ();
                SetNode(splitNode, node.Prefix[prefixIndex..], node.Children, node.IsWord);

                Dictionary<char, RadixTreeNode> children = new () { { splitNode.Prefix[0], splitNode } };
                SetNode(node, node.Prefix[..prefixIndex], children, false);
            }
            else
            {
                itemIndex += node.Prefix.Length;
            }

            if (item.Length != itemIndex)
            {
                RadixTreeNode newNode = new ();
                SetNode(newNode, item[itemIndex..], newNode.Children, true);
                node.Children.Add(item[itemIndex], newNode);
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

            (RadixTreeNode previousNode, RadixTreeNode node, int lastNodeIndex) = GetLastCommonNodes(item);

            return Contains(item, node);
        }

        public bool Remove(string item)
        {
            ValidateArgumentIsNotNull(item);

            (RadixTreeNode previousNode, RadixTreeNode node, int lastNodeIndex) = GetLastCommonNodes(item);

            if (!node.IsWord || !item.EndsWith(node.Prefix))
            {
                return false;
            }

            if (node.Children.Count == 0)
            {
                Count--;
                return previousNode.Children.Remove(item[lastNodeIndex]);
            }

            node.IsWord = false;
            Count--;
            return true;
        }

        public List<string> GetAllItemsByPrefix(string prefix)
        {
            ValidateArgumentIsNotNull(prefix);

            List<string> items = new List<string>();
            (RadixTreeNode previousNode, RadixTreeNode node, int index) = GetLastCommonNodes(prefix);

            //vezi daca poti refactoriza ca sa redici substringurile
            if (!prefix.EndsWith(node.Prefix) && !node.Prefix.StartsWith(prefix[index..]))
            {
                return items;
            }

            prefix = prefix[..index] + node.Prefix;
            items.AddRange(Traverse(node, prefix));

            return items;
        }

        public string GetLongestPrefixMatch(string item)
        {
            ValidateArgumentIsNotNull(item);

            (RadixTreeNode previousNode, RadixTreeNode node, int index) = GetLastCommonNodes(item);

            int itemIndex = GetEndIndex(node.Prefix, item, index);

            return item[..itemIndex];
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

        private static int GetEndIndex(string prefix, string item, int lastNodeIndex)
        {
            int minLength = Math.Min(prefix.Length, item.Length - lastNodeIndex);
            int itemIndex = lastNodeIndex;

            for (int i = 0; i < minLength; i++)
            {
                if (prefix[i] == item[itemIndex])
                {
                    itemIndex++;
                }
            }

            return itemIndex;
        }

        private static bool Contains(string item, RadixTreeNode node)
        {
            return node.IsWord && item.EndsWith(node.Prefix);
        }

        private static bool IsPartialPrefix(RadixTreeNode node, string item, int itemIndex)
        {
            return node.Prefix.Length > item.Length - itemIndex
                    || item.IndexOf(node.Prefix, itemIndex, node.Prefix.Length) != itemIndex;
        }

        private static void SetNode(RadixTreeNode node, string prefix, Dictionary<char, RadixTreeNode> children, bool isWord)
        {
            node.Prefix = prefix;
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

        private (RadixTreeNode previousNode, RadixTreeNode lastNode, int lastNodeIndex) GetLastCommonNodes(string item)
        {
            RadixTreeNode lastNode = root;
            RadixTreeNode previousNode = root;

            int index = 0;
            int lastNodeIndex = index;

            while (index < item.Length && lastNode.Children.TryGetValue(item[index], out RadixTreeNode node))
            {
                lastNodeIndex = index;
                previousNode = lastNode;
                lastNode = node;

                if (IsPartialPrefix(node, item, lastNodeIndex))
                {
                    return (previousNode, lastNode, lastNodeIndex);
                }

                index += node.Prefix.Length;
            }

            return (previousNode, lastNode, lastNodeIndex);
        }
    }
}
