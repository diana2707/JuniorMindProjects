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
            List<T> key = GetAsValidList(item);

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> node) = GetLastCommonNodes(key);

            ValidateNewItem(key, node);

            (int segmentIndex, int keyIndex) = GetSplitIndexes(node, key);

            if (IsPartialMatch(node.Segment, segmentIndex))
            {
                node.Split(segmentIndex);
            }

            if (IsPartialMatch(key, keyIndex))
            {
                node = node.AddLeaf(GetRemainingPart(key, keyIndex));
            }

            node.IsEnd = true;
            Count++;
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
            List<T> itemList = GetAsValidList(item);

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> lastNode) = GetLastCommonNodes(itemList);

            return Contains(itemList, lastNode);
        }

        public List<IEnumerable<T>> FindByPrefix(IEnumerable<T> prefix)
        {
            List<IEnumerable<T>> items = [];
            List<T> key = GetAsValidList(prefix);

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> node) = GetLastCommonNodes(key);
            (int segmentIndex, int keyIndex) = GetSplitIndexes(node, key);

            if (IsPartialMatch(key, keyIndex))
            {
                return items;
            }

            key.AddRange(GetRemainingPart(node.Segment, segmentIndex));

            items.AddRange(Traverse(node, key));

            return items;
        }

        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            return Traverse(root, []).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<T> GetPrefixMatch(IEnumerable<T> item)
        {
            List<T> itemList = GetAsValidList(item);

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> currentNode) = GetLastCommonNodes(itemList);

            (int segmentIndex, int itemIndex) = GetSplitIndexes(currentNode, itemList);

            return itemList.GetRange(0, itemIndex);
        }

        public bool Remove(IEnumerable<T> item)
        {
            List<T> key = GetAsValidList(item);

            (RadixTreeNode<T> previousNode, RadixTreeNode<T> node) = GetLastCommonNodes(key);

            if (!Contains(key, node))
            {
                return false;
            }

            if (node.Children.Count == 0)
            {
                previousNode.Remove(node);
            }
            else
            {
                node.IsEnd = false;
            }

            Count--;
            return true;
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

        private static List<T> GetAsValidList(IEnumerable<T> item)
        {
            ArgumentNullException.ThrowIfNull(item);
            return item.ToList();
        }

        private static List<T> GetRemainingPart(List<T> input, int splitIndex)
        {
            return input.GetRange(splitIndex, input.Count - splitIndex);
        }

        private static bool IsPartialMatch(List<T> input, int commonPrefixLength)
        {
            return input.Count > commonPrefixLength;
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
