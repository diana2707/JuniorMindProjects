using System;

namespace ObjectsCollection
{
    public class AVLTree<T>
    {
        readonly AVLTreeNode<T> root = new ();

        public AVLTree()
        {
            Count = 0;
        }

        public int Count { get; private set; }
    }
}
