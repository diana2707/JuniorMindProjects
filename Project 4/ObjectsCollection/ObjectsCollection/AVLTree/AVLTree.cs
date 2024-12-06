using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

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

            if (item.CompareTo(currentNode.Data) == 0)
            {
                throw new ArgumentException("Item already inserted");
            }

            if (item.CompareTo(currentNode.Data) < 0)
            {
                currentNode.Left = Add(currentNode.Left, item);
            }

            if (IsGreaterThan(item, currentNode.Data))
            {
                currentNode.Right = Add(currentNode.Right, item);
            }

            UpdateHeight(currentNode);

            currentNode = BalanceForInsertion(currentNode, item);

            return currentNode;
        }

        private static bool Contains(AVLTreeNode<T> currentNode, T item)
        {
            if (currentNode == null)
            {
                return false;
            }

            if (item.CompareTo(currentNode.Data) < 0)
            {
                return Contains(currentNode.Left, item);
            }
            else if (IsGreaterThan(item, currentNode.Data))
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

            if (item.CompareTo(currentNode.Data) < 0)
            {
                (currentNode.Left, isRemoved) = Remove(currentNode.Left, item);
            }

            if (IsGreaterThan(item, currentNode.Data))
            {
                (currentNode.Right, isRemoved) = Remove(currentNode.Right, item);
            }

            if (item.CompareTo(currentNode.Data) == 0)
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
                    AVLTreeNode<T> next = GetNextInOrder(currentNode);
                    currentNode.Data = next.Data;
                    (currentNode.Right, isRemoved) = Remove(currentNode.Right, currentNode.Data);
                }
            }

            UpdateHeight(currentNode);

            currentNode = BalanceForRemove(currentNode);

            return (currentNode, isRemoved);
        }

        private static AVLTreeNode<T> GetNextInOrder(AVLTreeNode<T> node)
        {
            node = node.Right;
            while (node.Left != null)
            {
                node = node.Left;
            }

            return node;
        }

        private static AVLTreeNode<T> BalanceForInsertion(AVLTreeNode<T> node, T item)
        {
            if (IsBalanced(node))
            {
                return node;
            }

            if (GetBalanceFactor(node) > 1)
            {
                if (item.CompareTo(node.Left.Data) < 0)
                {
                    return RotateRight(node);
                }

                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if (IsGreaterThan(item, node.Right.Data))
            {
                return RotateLeft(node);
            }

            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        private static AVLTreeNode<T> BalanceForRemove(AVLTreeNode<T> node)
        {
            if (IsBalanced(node))
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

            UpdateHeight(node);
            UpdateHeight(subtreeRoot);

            return subtreeRoot;
        }

        private static AVLTreeNode<T> RotateLeft(AVLTreeNode<T> node)
        {
            AVLTreeNode<T> subtreeRoot = node.Right;
            node.Right = subtreeRoot.Left;
            subtreeRoot.Left = node;

            UpdateHeight(node);
            UpdateHeight(subtreeRoot);

            return subtreeRoot;
        }

        private static int GetBalanceFactor(AVLTreeNode<T> node)
        {
            return (node.Left?.Height ?? -1) - (node.Right?.Height ?? -1);
        }

        private static bool IsBalanced(AVLTreeNode<T> node)
        {
            return Math.Abs(GetBalanceFactor(node)) <= 1;
        }

        private static void UpdateHeight(AVLTreeNode<T> node)
        {
            int maxHeight = Math.Max(node.Left?.Height ?? -1, node.Right?.Height ?? -1);
            node.Height = maxHeight + 1;
        }

        private static bool IsGreaterThan(T first, T second)
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
