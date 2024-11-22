using System;

namespace ObjectsCollection
{
    public class AVLTree<T>
        where T : IComparable<T>
    {
        readonly AVLTreeNode<T> root = new ();

        public AVLTree()
        {
            Count = 0;
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            if (Count == 0)
            {
                root.Data = item;
            }

            //if (item < root.Data)
            //{
            //    root.LeftChild.Data = item; 
            //}
            
            Count++;
        }
    }
}
