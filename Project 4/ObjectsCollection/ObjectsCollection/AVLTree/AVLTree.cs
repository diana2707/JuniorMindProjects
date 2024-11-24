using System;
using System.Collections;

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

        public IEnumerator<T> GetEnumerator()
        {
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
    }
}
