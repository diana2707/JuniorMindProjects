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

            root = InsertInternally(root, item);

            Count++;
        }

        public bool Contains(T item)
        {
            return Contains(root, item);
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
            else if (item.CompareTo(currentNode.Data) > 0)
            {
                return Contains(currentNode.Right, item);
            }

            return true;
        }

        public void Clear()
        {
            root = null;
            Count = 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var element in this)
            {
                array[arrayIndex] = element;
                arrayIndex++;
            }
        }

        public bool Remove(T item)
        {
            (root, bool isRemoved) = RemoveInternally(root, item);
            return isRemoved;
        }

        private (AVLTreeNode<T> root, bool isRemoved) RemoveInternally(AVLTreeNode<T> currentNode, T item)
        {
            bool isRemoved = false;

            if (currentNode == null)
            {
                return (currentNode, false);
            }

            if (currentNode.Data.CompareTo(item) > 0)
            {
                (currentNode.Left, isRemoved) = RemoveInternally(currentNode.Left, item);
            }

            if (currentNode.Data.CompareTo(item) < 0)
            {
                (currentNode.Right, isRemoved) = RemoveInternally(currentNode.Right, item);
            }

            if (currentNode.Data.CompareTo(item) == 0)
            {
                if (currentNode.Right == null)
                {
                    return (currentNode.Left, true);
                }
                else if (currentNode.Left == null)
                {
                    return (currentNode.Right, true);
                }
                else
                {
                    AVLTreeNode<T> next = GetNextInOrder(currentNode);
                    currentNode.Data = next.Data;
                    (currentNode.Right, isRemoved) = RemoveInternally(currentNode.Right, currentNode.Data);
                }
            }

            UpdateHeight(currentNode);

            if (!IsBalanced(currentNode))
            {
                currentNode = BalanceForRemove(currentNode);
            }

            return (currentNode, isRemoved);
        }

        private AVLTreeNode<T> GetNextInOrder(AVLTreeNode<T> node)
        {
            node = node.Right;
            while (node.Left != null)
            {
                node = node.Left;
            }

            return node;
        }

        private AVLTreeNode<T> InsertInternally(AVLTreeNode<T> currentNode, T item)
        {
            if (currentNode == null)
            {
                return new AVLTreeNode<T>(item);
            }

            if (currentNode.Data.CompareTo(item) == 0)
            {
                throw new ArgumentException("Item already inserted");
            }

            if (currentNode.Data.CompareTo(item) > 0)
            {
                currentNode.Left = InsertInternally(currentNode.Left, item);
            }

            if (currentNode.Data.CompareTo(item) < 0)
            {
                currentNode.Right = InsertInternally(currentNode.Right, item);
            }

            UpdateHeight(currentNode);

            if (!IsBalanced(currentNode))
            {
                currentNode = BalanceForInsertion(currentNode, item);
            }

            return currentNode;
        }

        private static AVLTreeNode<T> BalanceForInsertion(AVLTreeNode<T> node, T item)
        {
            if (GetBalanceFactor(node) > 1)
            {
                if (item.CompareTo(node.Left.Data) < 0)
                {
                    return RotateRight(node);
                }

                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if (item.CompareTo(node.Right.Data) > 0)
            {
                return RotateLeft(node);
            }

            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        private static AVLTreeNode<T> BalanceForRemove(AVLTreeNode<T> node)
        {
            if ((node.Left?.Height ?? -1) > (node.Right?.Height ?? -1))
            {
                if ((node.Left.Left?.Height ?? -1) > (node.Left.Right?.Height ?? -1))
                {
                    return RotateRight(node);
                }

                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if ((node.Right.Right?.Height ?? -1) > (node.Right.Left?.Height ?? -1))
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

        //private static int GetHeight(AVLTreeNode<T> node, int height = 0)
        //{
        //    if (node == null)
        //    {
        //        return height - 1;
        //    }

        //    int leftHeight = GetHeight(node.LeftChild, height + 1);

        //    int rightHeight = GetHeight(node.RightChild, height + 1);

        //    return int.Max(leftHeight, rightHeight);
        //}

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

            //de scris teoria pentru cele 3 variante
            //asta e inorder
            //la preorder curentnode sta la inceputul metodei
            //la postorder inversez left cu right child if
            if (currentNode.Left != null)
            {
                foreach (var item in Traverse(currentNode.Left))
                {
                    yield return item;
                }
            }

            //if (currentNode != null)
            //{
            yield return currentNode.Data;
            //}

            if (currentNode.Right != null)
            {
                foreach (var item in Traverse(currentNode.Right))
                {
                    yield return item;
                }
            }
        }

        private static int GetBalanceFactor(AVLTreeNode<T> node)
        {
            return (node.Left?.Height ?? -1) - (node.Right?.Height ?? -1);
        }

        //de ce nu acc null

        private static bool IsBalanced(AVLTreeNode<T> node)
        {
            if (node == null)
            {
                return true;
            }

            return Math.Abs(GetBalanceFactor(node)) <= 1;
        }

        private static void ValidateArgumentIsNotNull(T item)
        {
            ArgumentNullException.ThrowIfNull(item);
        }

        private static void UpdateHeight(AVLTreeNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            int maxHeight = Math.Max(node.Left?.Height ?? -1, node.Right?.Height ?? -1);
            node.Height = maxHeight + 1;
        }
    }
}
