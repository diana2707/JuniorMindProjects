using System;

namespace ObjectsCollection
{
    internal class AVLTreeNode<T>
    {
        internal AVLTreeNode()
        {
            Data = default;
        }

        internal T Data { get; set; }

        internal AVLTreeNode<T> LeftChild { get; set; }

        internal AVLTreeNode<T> RightChild { get; set; }
    }
}
