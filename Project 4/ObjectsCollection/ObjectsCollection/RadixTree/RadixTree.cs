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

            List<T> itemList = item.ToList();

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> lastNode) = GetLastCommonNodes(itemList);

            ValidateNewItem(itemList, lastNode);

            Add(itemList, lastNode);
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

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> lastNode) = GetLastCommonNodes(itemList);

            return Contains(itemList, lastNode);
        }

        public List<IEnumerable<T>> GetAllItemsByPrefix(IEnumerable<T> prefix)
        {
            ValidateArgumentIsNotNull(prefix);

            List<T> prefixList = prefix.ToList();

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> lastNode) = GetLastCommonNodes(prefixList);

            return GetAllItemsByPrefix(lastNode, prefixList);
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

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode) = GetLastCommonNodes(itemList);

            int commonPrefixLength = GetCommonPrefixLength(currentNode, itemList);

            int remainingItemLength = GetRemainingItemLength(itemList, commonPrefixLength, currentNode);

            return itemList.GetRange(0, itemList.Count - remainingItemLength);
        }

        public bool Remove(IEnumerable<T> item)
        {
            ValidateArgumentIsNotNull(item);

            List<T> itemList = item.ToList();

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode) = GetLastCommonNodes(itemList);

            if (!Contains(itemList, currentNode))
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

        private int GetRemainingItemLength(List<T> item, int commonPrefixLength, RadixTreeNode<T> node)
        {
            return item.Count - node.PathLength + node.Prefix.Count - commonPrefixLength;
        }

        private void Add(List<T> item, RadixTreeNode<T> node)
        {
            int splitIndex = GetCommonPrefixLength(node, item);

            if (IsPartialMatch(node.Prefix, splitIndex))
            {
                SplitNode(splitIndex, node);
            }

            //ae corect pt ca node.Path a fost actualizat in if-ul anterior
            if (IsPartialMatch(item, node.PathLength))
            {
                node = AddNode(item, node);
            }

            node.IsEnd = true;

            Count++;
        }

        private static RadixTreeNode<T> AddNode(List<T> item, RadixTreeNode<T> parent)
        {
            RadixTreeNode<T> newNode = new ();
            newNode.Prefix = item.GetRange(parent.PathLength, item.Count - parent.PathLength);
            newNode.PathLength = item.Count;

            parent.Children.Add(newNode);
            return newNode;

            //RadixTreeNode<T> newNode = new ();
            //newNode.Prefix = prefix.GetRange(splitIndex, prefix.Count - splitIndex);
            //newNode.PathLength += prefix.Count;

            //parent.Children.Add(newNode);
            //return newNode;
        }

        private static bool Contains(List<T> item, RadixTreeNode<T> node)
        {
            //sa introduc un GetRemainingItem? sau un EndsWith?

            //item = item.GetRange(node.PathLength - node.Prefix.Count, node.Prefix.Count);
            //return node.IsEnd && node.Prefix.SequenceEqual(item);

            return node.IsEnd && item.Count == node.PathLength && GetCommonPrefixLength(node, item) == node.Prefix.Count;
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

        private List<IEnumerable<T>> GetAllItemsByPrefix(RadixTreeNode<T> node, List<T> prefix)
        {
            List<IEnumerable<T>> items = [];

            int commonPrefixLength = GetCommonPrefixLength(node, prefix);

            if (GetRemainingItemLength(prefix, commonPrefixLength, node) > 0)
            {
                return items;
            }

            prefix.AddRange(node.Prefix.Skip(commonPrefixLength));
            //prefix = prefix.Concat(node.Prefix.Skip(commonPrefixLength));

            items.AddRange(Traverse(node, prefix));

            return items;
        }

        private static int GetCommonPrefixLength(RadixTreeNode<T> node, List<T> item)
        {
            int prefixLength = 0;
            int itemIndex = node.PathLength - node.Prefix.Count;
            int minLength = Math.Min(node.Prefix.Count, item.Count - itemIndex);

            for (int i = 0; i < minLength; i++)
            {
                if (!node.Prefix[i].Equals(item[itemIndex++]))
                {
                    break;
                }

                prefixLength++;
            }

            return prefixLength;
        }

        //private static int GetCommonPrefixLength(List<T> key, List<T> item)
        //{
        //    int prefixLength = 0;
        //    int minLength = key.Count < item.Count ? key.Count : item.Count;

        //    for (int i = 0; i < minLength; i++)
        //    {
        //        if (!key[i].Equals(item[i]))
        //        {
        //            break;
        //        }

        //        prefixLength++;
        //    }

        //    return prefixLength;
        //}

        private (RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode) GetLastCommonNodes(List<T> item)
        {
            RadixTreeNode<T> previousNode = null;
            RadixTreeNode<T> currentNode = root;

            bool matchFound = true;

            while (matchFound)
            {
                matchFound = false;

                foreach (var child in currentNode.Children)
                {
                    if (HasCommonPrefix(child, item))
                    {
                        previousNode = currentNode;
                        currentNode = child;

                        matchFound = true;
                        break;
                    }
                }
            }

            return (previousNode, currentNode);
        }

        private static bool HasCommonPrefix(RadixTreeNode<T> node, List<T> item)
        {
            return GetCommonPrefixLength(node, item) > 0;
        }

        private static bool IsPartialMatch(List<T> input, int commonPrefixLength)
        {
            return input.Count > commonPrefixLength;
        }

        private static void SplitNode(int splitIndex, RadixTreeNode<T> currentNode)
        {
            // sa extrag o functie de AddMiddleNode, vs AddLeafNode
            RadixTreeNode<T> newNode = new ()
            {
                Prefix = currentNode.Prefix.GetRange(splitIndex, currentNode.Prefix.Count - splitIndex),
                PathLength = currentNode.PathLength,
                Children = currentNode.Children.ToList(),
                IsEnd = currentNode.IsEnd
            };

            currentNode.Children.Clear();
            currentNode.Children.Add(newNode);
            currentNode.Prefix = currentNode.Prefix.GetRange(0, splitIndex);
            currentNode.PathLength -= newNode.Prefix.Count;
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
