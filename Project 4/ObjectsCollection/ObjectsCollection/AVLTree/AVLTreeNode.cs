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
    }
}
