using System;
using System.Collections;

namespace ObjectsCollection
{
    public class AVLTree<T> : ICollection<T>
        where T : IComparable<T>
    {
        private AVLTreeNode<T> root;

        public AVLTree()
        {
            Count = 0;
        }

        public int Count { get; private set; }

        public bool IsReadOnly { get; }

        public int Height
        {
            get => root?.Height ?? -1;
        }

        public void Add(T item)
        {
            ValidateArgumentIsNotNull(item);

            root = Add(root, item);
        }

        public bool Contains(T item)
        {
            ValidateArgumentIsNotNull(item);

            return Contains(root, item);
        }

        public void Clear()
        {
            root = null;
            Count = 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ValidateArray(array, arrayIndex);

            foreach (var element in this)
            {
                array[arrayIndex] = element;
                arrayIndex++;
            }
        }

        public bool Remove(T item)
        {
            ValidateArgumentIsNotNull(item);

            (root, bool isRemoved) = Remove(root, item);

            return isRemoved;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (root == null)
            {
                return Enumerable.Empty<T>().GetEnumerator();
            }

            return Traverse(root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static IEnumerable<T> Traverse(AVLTreeNode<T> currentNode)
        {
            if (currentNode.Left != null)
            {
                foreach (var item in Traverse(currentNode.Left))
                {
                    yield return item;
                }
            }

            yield return currentNode.Data;

            if (currentNode.Right != null)
            {
                foreach (var item in Traverse(currentNode.Right))
                {
                    yield return item;
                }
            }
        }

        private AVLTreeNode<T> Add(AVLTreeNode<T> currentNode, T item)
        {
            if (currentNode == null)
            {
                Count++;
                return new AVLTreeNode<T>(item);
            }

            if (AreEqual(item, currentNode.Data))
            {
                throw new ArgumentException("Item already inserted");
            }

            if (IsLessThan(item, currentNode.Data))
            {
                currentNode.Left = Add(currentNode.Left, item);
            }

            if (IsLargerThan(item, currentNode.Data))
            {
                currentNode.Right = Add(currentNode.Right, item);
            }

            currentNode.UpdateHeight();

            currentNode = BalanceForInsertion(currentNode, item);

            return currentNode;
        }

        private static bool Contains(AVLTreeNode<T> currentNode, T item)
        {
            if (currentNode == null)
            {
                return false;
            }

            if (IsLessThan(item, currentNode.Data))
            {
                return Contains(currentNode.Left, item);
            }
            else if (IsLargerThan(item, currentNode.Data))
            {
                return Contains(currentNode.Right, item);
            }

            return true;
        }

        private (AVLTreeNode<T> root, bool isRemoved) Remove(AVLTreeNode<T> currentNode, T item)
        {
            bool isRemoved = false;

            if (currentNode == null)
            {
                return (currentNode, false);
            }

            if (IsLessThan(item, currentNode.Data))
            {
                (currentNode.Left, isRemoved) = Remove(currentNode.Left, item);
            }

            if (IsLargerThan(item, currentNode.Data))
            {
                (currentNode.Right, isRemoved) = Remove(currentNode.Right, item);
            }

            if (AreEqual(item, currentNode.Data))
            {
                if (currentNode.Right == null)
                {
                    Count--;
                    return (currentNode.Left, true);
                }
                else if (currentNode.Left == null)
                {
                    Count--;
                    return (currentNode.Right, true);
                }
                else
                {
                    AVLTreeNode<T> nextInOrder = currentNode.Right;
                    while (nextInOrder.Left != null)
                    {
                        nextInOrder = nextInOrder.Left;
                    }

                    currentNode.Data = nextInOrder.Data;
                    (currentNode.Right, isRemoved) = Remove(currentNode.Right, nextInOrder.Data);
                }
            }

            currentNode.UpdateHeight();

            currentNode = BalanceForRemove(currentNode);

            return (currentNode, isRemoved);
        }

        private static AVLTreeNode<T> BalanceForInsertion(AVLTreeNode<T> node, T item)
        {
            if (node.IsBalanced())
            {
                return node;
            }

            if (node.GetBalanceFactor() > 1)
            {
                if (IsLessThan(item, node.Left.Data))
                {
                    return RotateRight(node);
                }

                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if (IsLargerThan(item, node.Right.Data))
            {
                return RotateLeft(node);
            }

            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        private static AVLTreeNode<T> BalanceForRemove(AVLTreeNode<T> node)
        {
            if (node.IsBalanced())
            {
                return node;
            }

            int leftHeight = node.Left?.Height ?? -1;
            int rightHeight = node.Right?.Height ?? -1;

            if (leftHeight > rightHeight)
            {
                int leftLeftHeight = node.Left.Left?.Height ?? -1;
                int leftRightHeight = node.Left.Right?.Height ?? -1;

                if (leftLeftHeight > leftRightHeight)
                {
                    return RotateRight(node);
                }

                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            int rightRightHeight = node.Right.Right?.Height ?? -1;
            int rightLeftHeight = node.Right.Left?.Height ?? -1;

            if (rightRightHeight > rightLeftHeight)
            {
                return RotateLeft(node);
            }

            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        private static AVLTreeNode<T> RotateRight(AVLTreeNode<T> node)
        {
            AVLTreeNode<T> subtreeRoot = node.Left;
            node.Left = subtreeRoot.Right;
            subtreeRoot.Right = node;

            node.UpdateHeight();
            subtreeRoot.UpdateHeight();

            return subtreeRoot;
        }

        private static AVLTreeNode<T> RotateLeft(AVLTreeNode<T> node)
        {
            AVLTreeNode<T> subtreeRoot = node.Right;
            node.Right = subtreeRoot.Left;
            subtreeRoot.Left = node;

            node.UpdateHeight();
            subtreeRoot.UpdateHeight();

            return subtreeRoot;
        }

        private static bool AreEqual(T first, T second)
        {
            return first.CompareTo(second) == 0;
        }

        private static bool IsLessThan(T first, T second)
        {
            return first.CompareTo(second) < 0;
        }

        private static bool IsLargerThan(T first, T second)
        {
            return first.CompareTo(second) > 0;
        }

        private static void ValidateArgumentIsNotNull(T item)
        {
            ArgumentNullException.ThrowIfNull(item);
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
