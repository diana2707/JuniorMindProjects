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
    }
}
