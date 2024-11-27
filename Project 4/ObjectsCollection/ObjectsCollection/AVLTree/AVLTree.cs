using System;
using System.Collections;
using System.Xml.Linq;

namespace ObjectsCollection
{
    public class AVLTree<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private AVLTreeNode<T> root;

        public AVLTree()
        {
            Count = 0;
        }

        public int Count { get; private set; }

        public int Height
        {
            get => root.Height;
        }

        public void Add(T item)
        {
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
                (currentNode.LeftChild, isRemoved) = RemoveInternally(currentNode.LeftChild, item);
            }

            if (currentNode.Data.CompareTo(item) < 0)
            {
                (currentNode.RightChild, isRemoved) = RemoveInternally(currentNode.RightChild, item);
            }

            if (currentNode.Data.CompareTo(item) == 0)
            {
                currentNode = null;
                isRemoved = true;
            }

            return (currentNode, isRemoved);
        }

        private AVLTreeNode<T> InsertInternally(AVLTreeNode<T> currentNode, T item)
        {
            if (currentNode == null)
            {
                currentNode = new ();
                currentNode.Data = item;
                return currentNode;
            }

            if (currentNode.Data.CompareTo(item) > 0)
            {
                currentNode.LeftChild = InsertInternally(currentNode.LeftChild, item);

                if (!IsBalanced(currentNode))
                {
                    currentNode = Balance(currentNode, item);
                }

                currentNode.Height = currentNode.Height == currentNode.LeftChild.Height ? currentNode.Height + 1 : currentNode.Height;

                return currentNode;
            }

            if (currentNode.Data.CompareTo(item) < 0)
            {
                currentNode.RightChild = InsertInternally(currentNode.RightChild, item);

                if (!IsBalanced(currentNode))
                {
                    currentNode = Balance(currentNode, item);
                }

                //fa pe nod o metoda de UpdateHight
                currentNode.Height = currentNode.Height == currentNode.RightChild.Height ? currentNode.Height + 1 : currentNode.Height;

                return currentNode;
            }

            throw new ArgumentException("Item already inserted");
        }

        private AVLTreeNode<T> Balance(AVLTreeNode<T> node, T item)
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
                    return RotateRight(node);
                }

                node.LeftChild = RotateLeft(node.LeftChild);
                return RotateRight(node);
            }

            //if (node.GetBalanceFactor() < -1)
            //{
            if (item.CompareTo(node.RightChild.Data) > 0)
            {
                return RotateLeft(node);
            }

            node.RightChild = RotateRight(node.RightChild);
            return RotateLeft(node);

            //}
        }

        //incearca sa le pui pe nod
        private static AVLTreeNode<T> RotateRight(AVLTreeNode<T> node)
        {
            AVLTreeNode<T> subtreeRoot = node.LeftChild;
            node.LeftChild = subtreeRoot.RightChild;
            subtreeRoot.RightChild = node;

            subtreeRoot.Height = node.Height;
            node.Height--;

            return subtreeRoot;
        }

        private static AVLTreeNode<T> RotateLeft(AVLTreeNode<T> node)
        {
            AVLTreeNode<T> subtreeRoot = node.RightChild;
            node.RightChild = subtreeRoot.LeftChild;
            subtreeRoot.LeftChild = node;

            subtreeRoot.Height = node.Height;
            node.Height--;

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
    }
}
