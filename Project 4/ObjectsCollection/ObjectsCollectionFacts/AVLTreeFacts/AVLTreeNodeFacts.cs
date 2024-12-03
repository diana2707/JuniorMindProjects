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
            Assert.Null(node.LeftChild);
        }

        [Fact]
        public void RightChildIsNullAsDefault()
        {
            var node = new AVLTreeNode<int>(5);
            Assert.Null(node.RightChild);
        }

        [Fact]
        public void HeightReturns0AsDefault()
        {
            var node = new AVLTreeNode<int>(5);
            Assert.Equal(0, node.Height);
        }
    }
}
