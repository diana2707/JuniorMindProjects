using System.Collections;
using System.Xml.Linq;

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
            List<T> itemList = GetValidList(item);

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
            List<T> itemList = GetValidList(item);

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> lastNode) = GetLastCommonNodes(itemList);

            return Contains(itemList, lastNode);
        }

        public List<IEnumerable<T>> GetAllItemsByPrefix(IEnumerable<T> prefix)
        {
            List<T> prefixList = GetValidList(prefix);

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
            List<T> itemList = GetValidList(item);

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode) = GetLastCommonNodes(itemList);

            int splitIndex = GetPrefixLength(currentNode, itemList);
            List<T> remainingItem = GetRemainingItem(itemList, splitIndex, currentNode);

            return itemList.GetRange(0, itemList.Count - remainingItem.Count);
        }

        public bool Remove(IEnumerable<T> item)
        {
            List<T> itemList = GetValidList(item);

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode) = GetLastCommonNodes(itemList);

            return Remove(itemList, previousNode, currentNode);
        }

        private void Add(List<T> item, RadixTreeNode<T> node)
        {
            int splitIndex = GetPrefixLength(node, item);
            item = GetRemainingItem(item, splitIndex, node);

            if (IsPartialMatch(node.Segment, splitIndex))
            {
                node.Split(splitIndex);
            }

            if (IsPartialMatch(item, 0 /*node.PathLength*/))
            {
                node = node.AddLeaf(item);
            }

            node.IsEnd = true;

            Count++;
        }

        private static List<T> GetRemainingItem(List<T> item, int segmentSplitIndex, RadixTreeNode<T> node)
        {
            int itemSplitIndex = node.PathLength - node.Segment.Count + segmentSplitIndex;
            return item.GetRange(itemSplitIndex, item.Count - itemSplitIndex);
        }

        private static bool Contains(List<T> item, RadixTreeNode<T> node)
        {
            return node.IsEnd && ItemEndsWithPrefix(item, node);
        }

        private static bool ItemEndsWithPrefix(List<T> item, RadixTreeNode<T> node)
        {
            return GetPrefixLength(node, item) == node.Segment.Count && item.Count == node.PathLength;
        }

        private static IEnumerable<IEnumerable<T>> Traverse(RadixTreeNode<T> node, IEnumerable<T> prefix)
        {
            if (node.IsEnd)
            {
                yield return prefix;
            }

            foreach (var child in node.Children)
            {
                foreach (IEnumerable<T> item in Traverse(child, prefix.Concat(child.Segment)))
                {
                    yield return item;
                }
            }
        }

        private static List<IEnumerable<T>> GetAllItemsByPrefix(RadixTreeNode<T> node, List<T> prefix)
        {
            List<IEnumerable<T>> items = [];

            int commonPrefixLength = GetPrefixLength(node, prefix);

            int itemSplitIndex = node.PathLength - node.Segment.Count + commonPrefixLength;

            if (IsPartialMatch(prefix, itemSplitIndex))
            {
                return items;
            }

            prefix.AddRange(node.Segment.Skip(commonPrefixLength));

            items.AddRange(Traverse(node, prefix));

            return items;
        }

        private static int GetPrefixLength(RadixTreeNode<T> node, List<T> item)
        {
            int prefixLength = 0;
            int itemIndex = node.PathLength - node.Segment.Count;
            int minLength = Math.Min(node.Segment.Count, item.Count - itemIndex);

            for (int i = 0; i < minLength; i++)
            {
                if (!node.Segment[i].Equals(item[itemIndex++]))
                {
                    break;
                }

                prefixLength++;
            }

            return prefixLength;
        }

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
                    if (GetPrefixLength(child, item) > 0)
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

        private static List<T> GetValidList(IEnumerable<T> item)
        {
            ArgumentNullException.ThrowIfNull(item);
            return item.ToList();
        }

        private static bool IsPartialMatch(List<T> input, int commonPrefixLength)
        {
            return input.Count > commonPrefixLength;
        }

        private bool Remove(List<T> item, RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode)
        {
            if (!Contains(item, currentNode))
            {
                return false;
            }

            if (currentNode.Children.Count == 0)
            {
                previousNode.Remove(currentNode);
            }
            else
            {
                currentNode.IsEnd = false;
            }

            Count--;
            return true;
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
