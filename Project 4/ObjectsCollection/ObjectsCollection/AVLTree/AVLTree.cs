using System;

namespace ObjectsCollection
{
    public class AVLTree<T>
        where T : IComparable<T>
    {
        private AVLTreeNode<T> root;

        public AVLTree()
        {
            Count = 0;
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            root = InsertInternally(root, item);

            Count++;
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
                return currentNode;
            }

            if (currentNode.Data.CompareTo(item) < 0)
            {
                currentNode.RightChild = InsertInternally(currentNode.RightChild, item);
                return currentNode;
            }

            throw new ArgumentException("Item already inserted");
        }
    }
}
