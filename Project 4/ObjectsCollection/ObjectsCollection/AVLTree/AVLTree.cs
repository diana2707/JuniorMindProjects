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
            AVLTreeNode<T> currentNode = root;

            return Contains(item, currentNode);
        }

        private static bool Contains(T item, AVLTreeNode<T> currentNode)
        {
            while (currentNode != null)
            {
                if (item.Equals(currentNode.Data))
                {
                    return true;
                }
                else if (item.CompareTo(currentNode.Data) < 0)
                {
                    currentNode = currentNode.LeftChild;
                }
                else if (item.CompareTo(currentNode.Data) > 0)
                {
                    currentNode = currentNode.RightChild;
                }
            }

            return false;

            //if (currentNode == null)
            //{
            //    return false;
            //}
            //else if (item.Equals(currentNode.Data))
            //{
            //    return true;
            //}

            //if (item.CompareTo(currentNode.Data) < 0)
            //{
            //    Contains(item, currentNode.LeftChild);
            //}
            //else if (item.CompareTo(currentNode.Data) > 0)
            //{
            //    Contains(item, currentNode.RightChild);
            //}
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

            if (currentNode.Data.CompareTo(item) == 0)
            {
                if (currentNode.RightChild == null)
                {
                    currentNode = currentNode.LeftChild;
                }
                else if (currentNode.LeftChild == null)
                {
                    currentNode = currentNode.RightChild;
                }
                else
                {
                    AVLTreeNode<T> next = GetNextInOrder(currentNode);
                    currentNode.Data = next.Data;
                    (currentNode.RightChild, isRemoved) = RemoveInternally(currentNode.RightChild, currentNode.Data);
                }

                return (currentNode, true);
            }

            if (currentNode.Data.CompareTo(item) > 0)
            {
                (currentNode.LeftChild, isRemoved) = RemoveInternally(currentNode.LeftChild, item);
            }

            if (currentNode.Data.CompareTo(item) < 0)
            {
                (currentNode.RightChild, isRemoved) = RemoveInternally(currentNode.RightChild, item);
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
            node = node.RightChild;
            while (node.LeftChild != null)
            {
                node = node.LeftChild;
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
                currentNode.LeftChild = InsertInternally(currentNode.LeftChild, item);
            }

            if (currentNode.Data.CompareTo(item) < 0)
            {
                currentNode.RightChild = InsertInternally(currentNode.RightChild, item);
            }

            UpdateHeight(currentNode);

            if (!IsBalanced(currentNode))
            {
                currentNode = BalanceForInsertion(currentNode, item);
            }

            return currentNode;
        }

        private AVLTreeNode<T> BalanceForInsertion(AVLTreeNode<T> node, T item)
        {
            //if (node.GetBalanceFactor() > 1)
            //{
            //    if (item.CompareTo(node.LeftChild.Data) < 0)
            //    {
            //        return RotateRight(node);
            //    }
            //}

            if (GetBalanceFactor(node) > 1)
            {
                if (item.CompareTo(node.LeftChild.Data) < 0)
                {
                    return RotateRight(node, false);
                }

                node.LeftChild = RotateLeft(node.LeftChild, true);
                return RotateRight(node, false);
            }

            //if (node.GetBalanceFactor() < -1)
            //{
            if (item.CompareTo(node.RightChild.Data) > 0)
            {
                return RotateLeft(node, false);
            }

            node.RightChild = RotateRight(node.RightChild, true);
            return RotateLeft(node, false);

            //}
        }

        private AVLTreeNode<T> BalanceForRemove(AVLTreeNode<T> node)
        {
            if (node.RightChild == null || (node.LeftChild != null && node.LeftChild.Height > node.RightChild.Height))
            {
                if (node.LeftChild.RightChild == null || (node.LeftChild.LeftChild != null && node.LeftChild.LeftChild.Height > node.LeftChild.RightChild.Height))
                {
                    return RotateRight(node, false);
                }

                node.LeftChild = RotateLeft(node.LeftChild, true);
                return RotateRight(node, false);
            }

            if (node.RightChild.LeftChild == null || (node.RightChild.RightChild != null && node.RightChild.RightChild.Height > node.RightChild.LeftChild.Height))
            {
                return RotateLeft(node, false);
            }

            node.RightChild = RotateRight(node.RightChild, true);
            return RotateLeft(node, false);
        }

        //incearca sa le pui pe nod
        private static AVLTreeNode<T> RotateRight(AVLTreeNode<T> node, bool isPartial)
        {
            AVLTreeNode<T> subtreeRoot = node.LeftChild;
            node.LeftChild = subtreeRoot.RightChild;
            subtreeRoot.RightChild = node;

            if (isPartial)
            {
                //aici intra si remove
                //subtreeRoot.Height = node.Height;
                subtreeRoot.Height++;
                node.Height--;
            }
            else
            {
                node.Height -= 2;
            }

            return subtreeRoot;
        }

        private static AVLTreeNode<T> RotateLeft(AVLTreeNode<T> node, bool isPartial)
        {
            AVLTreeNode<T> subtreeRoot = node.RightChild;
            node.RightChild = subtreeRoot.LeftChild;
            subtreeRoot.LeftChild = node;

            if (isPartial)
            {
                //aici intra si Remove in situatia de Right Left si Left Right --> clarifica
                subtreeRoot.Height++;
                node.Height--;
            }
            else
            {
                node.Height -= 2;
            }

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
            if (currentNode.LeftChild != null)
            {
                foreach (var item in Traverse(currentNode.LeftChild))
                {
                    yield return item;
                }
            }

            //if (currentNode != null)
            //{
            yield return currentNode.Data;
            //}

            if (currentNode.RightChild != null)
            {
                foreach (var item in Traverse(currentNode.RightChild))
                {
                    yield return item;
                }
            }
        }

        private static int GetBalanceFactor(AVLTreeNode<T> node)
        {
            return (node.LeftChild?.Height ?? -1) - (node.RightChild?.Height ?? -1);
        }

        private static bool IsBalanced(AVLTreeNode<T> node)
        {
            return Math.Abs(GetBalanceFactor(node)) <= 1;
        }

        private static void ValidateArgumentIsNotNull(T item)
        {
            ArgumentNullException.ThrowIfNull(item);
        }

        private static void UpdateHeight(AVLTreeNode<T> node)
        {
            int maxHeight = Math.Max(node.LeftChild?.Height ?? -1, node.RightChild?.Height ?? -1);
            node.Height = maxHeight + 1;
            //if (currentNode.Height < largerHeight + 1)
            //{
            //    currentNode.Height++;
            //}
        }
    }
}
