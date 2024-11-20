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

            (int segmentIndex, int itemIndex) = GetSplitIndexes(currentNode, itemList);

            return itemList.GetRange(0, itemIndex);
        }

        public bool Remove(IEnumerable<T> item)
        {
            List<T> itemList = GetValidList(item);

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode) = GetLastCommonNodes(itemList);

            return Remove(itemList, previousNode, currentNode);
        }

        private void Add(List<T> item, RadixTreeNode<T> node)
        {
            (int segmentIndex, int itemIndex) = GetSplitIndexes(node, item);
            //item = GetRemainingItem(item, segmentIndex, node);

            if (IsPartialMatch(node.Segment, segmentIndex))
            {
                node.Split(segmentIndex);
            }

            if (IsPartialMatch(item, itemIndex /*node.PathLength*/))
            {
                item = GetRemainingItem(item, itemIndex);
                node = node.AddLeaf(item);
            }

            node.IsEnd = true;

            Count++;
        }

        //vezi daca e mai bine sa o inlocuiesti cu functia care iti da indexul pt item (GetSplitIndexes? si o metoda intermediara pentru GetNodes?)
        private static List<T> GetRemainingItem(List<T> item, int splitIndex)
        {
            return item.GetRange(splitIndex, item.Count - splitIndex);
            //int itemSplitIndex = node.PathLength - node.Segment.Count + segmentSplitIndex;
            //return item.GetRange(itemSplitIndex, item.Count - itemSplitIndex);
        }

        private static bool Contains(List<T> item, RadixTreeNode<T> node)
        {
            (int segmentIndex, int itemIndex) = GetSplitIndexes(node, item);
            bool itemEndsWithSegment = segmentIndex == node.Segment.Count && itemIndex == item.Count;

            return node.IsEnd && itemEndsWithSegment;
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

            (int segmentIndex, int prefixIndex) = GetSplitIndexes(node, prefix);

            List<T> remainingPrefix = GetRemainingItem(prefix, prefixIndex);

            //int itemSplitIndex = node.PathLength - node.Segment.Count + splitIndex;

            if (IsPartialMatch(prefix, prefix.Count - remainingPrefix.Count))
            {
                return items;
            }

            prefix.AddRange(node.Segment.Skip(segmentIndex));

            items.AddRange(Traverse(node, prefix));

            return items;
        }

        private static (int segmentIndex, int itemIndex) GetSplitIndexes(RadixTreeNode<T> node, List<T> item)
        {
            int segmentIndex = 0;
            int itemIndex = node.PathLength - node.Segment.Count;
            int minLength = Math.Min(node.Segment.Count, item.Count - itemIndex);

            for (int i = 0; i < minLength; i++)
            {
                if (!node.Segment[i].Equals(item[itemIndex]))
                {
                    break;
                }

                segmentIndex++;
                itemIndex++;
            }

            return (segmentIndex, itemIndex);
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
                    (int segmentIndex, int itemIndex) = GetSplitIndexes(child, item);
                    if (segmentIndex > 0)
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
