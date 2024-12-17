using System;

namespace ObjectsCollection
{
    public class AVLTreeNodeFacts
    {
        [Fact]
        public void AVLTreeNodeClassIsEmpty()
        {
            var node = new AVLTreeNode<int>(5);
            Assert.IsType<AVLTreeNode<int>>(node);
        }

        [Fact]
        public void DataTakesTheParameterValue()
        {
            var node = new AVLTreeNode<int>(5);
            Assert.Equal(5, node.Data);
        }

        [Fact]
        public void LeftChildIsNullAsDefault()
        {
            var node = new AVLTreeNode<int>(5);
            Assert.Null(node.Left);
        }

        [Fact]
        public void RightChildIsNullAsDefault()
        {
            var node = new AVLTreeNode<int>(5);
            Assert.Null(node.Right);
        }

        [Fact]
        public void HeightReturns0AsDefault()
        {
            var node = new AVLTreeNode<int>(5);
            Assert.Equal(0, node.Height);
        }

        [Fact]
        public void UpdateHeightShouldCalculateCurrentHeight()
        {
            var left = new AVLTreeNode<int>(1);
            left.Height = 1;
            var right = new AVLTreeNode<int>(10);
            right.Height = 0;

            var node = new AVLTreeNode<int>(5);
            node.Left = left;
            node.Right = right;

            node.UpdateHeight();

            Assert.Equal(2, node.Height);
        }

        [Fact]
        public void GetBalanceFactorShouldReturnBalanceFactor()
        {
            var left = new AVLTreeNode<int>(1);
            left.Height = 1;
            var right = new AVLTreeNode<int>(10);
            right.Height = 0;

            var node = new AVLTreeNode<int>(5);
            node.Left = left;
            node.Right = right;

            Assert.Equal(1, node.GetBalanceFactor());
        }

        [Fact]
        public void IsBalancedFactorShouldReturnTrueWhenIsBalanced()
        {
            var left = new AVLTreeNode<int>(1);
            left.Height = 1;
            var right = new AVLTreeNode<int>(10);
            right.Height = 0;

            var node = new AVLTreeNode<int>(5);
            node.Left = left;
            node.Right = right;

            Assert.True(node.IsBalanced());
        }

        [Fact]
        public void IsBalancedFactorShouldReturnFalseWhenIsNotBalanced()
        {
            var left = new AVLTreeNode<int>(1);
            left.Height = 2;
            var right = new AVLTreeNode<int>(10);
            right.Height = 0;

            var node = new AVLTreeNode<int>(5);
            node.Left = left;
            node.Right = right;

            Assert.False(node.IsBalanced());
        }

        [Fact]
        public void RotateLeftShouldReturnRightNodeAsNewRoot()
        {
            var node = new AVLTreeNode<int>(3);
            node.Height = 2;

            node.Left = new AVLTreeNode<int>(2);
            node.Left.Height = 0;

            node.Right = new AVLTreeNode<int>(5);
            node.Right.Height = 1;

            node.Right.Left = new AVLTreeNode<int>(4);
            node.Right.Left.Height = 0;

            node.Right.Right = new AVLTreeNode<int>(7);
            node.Right.Right.Height = 0;

            var newRoot = node.RotateLeft();

            Assert.Equal(5, newRoot.Data);
            Assert.Equal(3, newRoot.Left.Data);
            Assert.Equal(4, newRoot.Left.Right.Data);
        }

        [Fact]
        public void RotateRightShouldReturnLeftNodeAsNewRoot()
        {
            var node = new AVLTreeNode<int>(7);
            node.Height = 2;

            node.Left = new AVLTreeNode<int>(5);
            node.Left.Height = 1;

            node.Left.Left = new AVLTreeNode<int>(3);
            node.Left.Left.Height = 0;

            node.Left.Right = new AVLTreeNode<int>(6);
            node.Left.Right.Height = 0;

            node.Right = new AVLTreeNode<int>(8);
            node.Right.Height = 0;

            var newRoot = node.RotateRight();

            Assert.Equal(5, newRoot.Data);
            Assert.Equal(7, newRoot.Right.Data);
            Assert.Equal(6, newRoot.Right.Left.Data);
        }
    }
}
