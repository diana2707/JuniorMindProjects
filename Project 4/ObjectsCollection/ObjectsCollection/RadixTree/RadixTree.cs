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
            ValidateItemIsNotNull(item);

            (RadixTreeNode previousNode, RadixTreeNode node) = GetLastCommonNodes(item, out int itemIndex);

            ValidateNewItem(item, node);

            if (IsPartialPrefix(node, item, itemIndex))
            {
                (int prefixIndex, itemIndex) = GetEndIndexes(node.Prefix, item, itemIndex);

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
            (RadixTreeNode previousNode, RadixTreeNode node) = GetLastCommonNodes(item, out int index);

            return Contains(item, node);
        }

        public bool Remove(string item)
        {
            (RadixTreeNode penultimateNode, RadixTreeNode node) = GetLastCommonNodes(item, out int index);

            if (!node.IsWord || !item.EndsWith(node.Prefix))
            {
                return false;
            }

            if (node.Children.Count == 0)
            {
                Count--;
                return penultimateNode.Children.Remove(item[index]);
            }

            node.IsWord = false;
            Count--;
            return true;
        }

        public List<string> GetAllItemsByPrefix(string prefix)
        {
            List<string> items = new List<string>();
            (RadixTreeNode penultimateNode, RadixTreeNode node) = GetLastCommonNodes(prefix, out int index);

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
            (RadixTreeNode penultimateNode, RadixTreeNode node) = GetLastCommonNodes(item, out int index);

            (int prefixIndex, int itemIndex) = GetEndIndexes(node.Prefix, item, index);

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

        private IEnumerable<string> Traverse(RadixTreeNode node, string prefix)
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

        //private RadixTreeNode GetLastCommonNode(string item, out int itemIndex)//pot sa il numesc mai bine ca sa reflect ca iese si indexul?
        //{
        //    RadixTreeNode penultimateNode = GetPenultimateCommonNode(item, out itemIndex);

        //    //e oare corect sa returneze root si cand nu exista cuvantul(copii), si cand copii sunt imediat dupa root
        //    if (penultimateNode == null)
        //    {
        //        return root;
        //    }

        //    return penultimateNode.Children.GetValueOrDefault(item[itemIndex]);


        //    //RadixTreeNode currentNode = root;

        //    //int index = 0;
        //    //itemIndex = index;

        //    //while (index < item.Length && currentNode.Children.TryGetValue(item[index], out RadixTreeNode node))
        //    //{
        //    //    currentNode = node;
        //    //    itemIndex = index;

        //    //    if (IsPartialPrefix(currentNode, item, itemIndex))
        //    //    {
        //    //        return currentNode;
        //    //    }

        //    //    index += currentNode.Prefix.Length;
        //    //}

        //    //return currentNode;
        //}

        private (RadixTreeNode penultimateNode, RadixTreeNode lastNode) GetLastCommonNodes(string item, out int itemIndex)//face cam multe metoda asta, sa o fac sa returneze doar previous?
        {
            RadixTreeNode currentNode = root;
            RadixTreeNode penultimateNode = root;
            //string prefix = "";

            int index = 0;
            itemIndex = index;

            while (index < item.Length && currentNode.Children.TryGetValue(item[index], out RadixTreeNode node))
            {
                itemIndex = index;
                penultimateNode = currentNode;
                currentNode = node;

                if (IsPartialPrefix(node, item, itemIndex))
                {
                    return (penultimateNode, currentNode);
                }

                index += node.Prefix.Length;
            }

            return (penultimateNode, currentNode);
        }

        private (int prefixIndex, int itemIndex) GetEndIndexes(string prefix, string item, int itemIndex)
        {
            int minLength = Math.Min(prefix.Length, item.Length - itemIndex);
            int prefixIndex = 0;

            for (int i = 0; i < minLength; i++)
            {
                if (prefix[i] == item[itemIndex])
                {
                    prefixIndex++;
                    itemIndex++;
                }
            }

            return (prefixIndex, itemIndex);
        }

        private bool Contains(string item, RadixTreeNode node)
        {
            return node.IsWord && item.EndsWith(node.Prefix);
        }

        private bool IsPartialPrefix(RadixTreeNode node, string item, int itemIndex)
        {
            return node.Prefix.Length > item.Length - itemIndex
                    || item.IndexOf(node.Prefix, itemIndex, node.Prefix.Length) != itemIndex;
        }

        private void SetNode(RadixTreeNode node, string prefix, Dictionary<char, RadixTreeNode> children, bool isWord)
        {
            node.Prefix = prefix;
            node.IsWord = isWord;
            node.Children = children.ToDictionary();
        }

        private void ValidateItemIsNotNull(string item)
        {
            ArgumentNullException.ThrowIfNull(item);
        }

        private void ValidateNewItem(string item, RadixTreeNode node)
        {
            if (!Contains(item, node))
            {
                return;
            }

            throw new ArgumentException("The item is already stored in the tree.");
        }
    }
}
