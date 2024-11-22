using System;

namespace ObjectsCollection
{
    public class AVLTreeNodeFacts
    {
        [Fact]
        public void AVLTreeNodeClassIsEmpty()
        {
            var node = new AVLTreeNode<int>();
            Assert.IsType<AVLTreeNode<int>>(node);
        }

        [Fact]
        public void DataIsDefaultWhenTreeIsEmpty()
        {
            var node = new AVLTreeNode<int>();
            Assert.Equal(0, node.Data);
        }

        [Fact]
        public void LeftChildIsNullWhenTreeIsEmpty()
        {
            var node = new AVLTreeNode<int>();
            Assert.Null(node.LeftChild);
        }

        [Fact]
        public void RightChildIsNullWhenTreeIsEmpty()
        {
            var node = new AVLTreeNode<int>();
            Assert.Null(node.RightChild);
        }

    }
}
