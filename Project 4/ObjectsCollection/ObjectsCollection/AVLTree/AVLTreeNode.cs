using System;

namespace ObjectsCollection
{
    internal class AVLTreeNode<T>
    {
        internal AVLTreeNode()
        {
            Data = default;
            Height = 0;
        }

        internal T Data { get; set; }

        internal AVLTreeNode<T> LeftChild { get; set; }

        internal AVLTreeNode<T> RightChild { get; set; }

        internal int Height { get; set; }

        internal int GetBalanceFactor()
        {
            return (LeftChild?.Height ?? -1) - (RightChild?.Height ?? -1);
        }

        internal bool IsBalanced()
        {
            return Math.Abs(GetBalanceFactor()) <= 1;
        }
    }
}
