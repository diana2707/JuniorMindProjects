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

        internal int GetBalanceFactor()
        {
            return (Left?.Height ?? -1) - (Right?.Height ?? -1);
        }

        internal bool IsBalanced()
        {
            return Math.Abs(GetBalanceFactor()) <= 1;
        }

        internal void UpdateHeight()
        {
            int maxHeight = Math.Max(Left?.Height ?? -1, Right?.Height ?? -1);
            Height = maxHeight + 1;
        }

        internal AVLTreeNode<T> RotateLeft()
        {
            AVLTreeNode<T> newRoot = Right;
            Right = newRoot.Left;
            newRoot.Left = this;

            UpdateHeight();
            newRoot.UpdateHeight();

            return newRoot;
        }

        internal AVLTreeNode<T> RotateRight()
        {
            AVLTreeNode<T> newRoot = Left;
            Left = newRoot.Right;
            newRoot.Right = this;

            UpdateHeight();
            newRoot.UpdateHeight();

            return newRoot;
        }
    }
}
