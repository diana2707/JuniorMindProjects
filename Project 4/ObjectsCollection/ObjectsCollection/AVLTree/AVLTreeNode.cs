using System;

namespace ObjectsCollection
{
    internal class AVLTreeNode<T>
    {
        internal AVLTreeNode(T data)
        {
            Data = data;
            Height = 0;
        }

        internal T Data { get; set; }

        internal AVLTreeNode<T> Left { get; set; }

        internal AVLTreeNode<T> Right { get; set; }

        internal int Height { get; set; }
    }
}
