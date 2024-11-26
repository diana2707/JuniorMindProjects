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

        [Fact]
        public void HeightReturnsNegative1WhenTreeIsEmpty()
        {
            var node = new AVLTreeNode<int>();
            Assert.Equal(0, node.Height);
        }

        [Fact]
        public void GetBalanceFactorReturnsCorrectBalanceFactor()
        {
            var node = new AVLTreeNode<int>();
            node.Height = 1;
            node.RightChild = new AVLTreeNode<int>();
            node.RightChild.Height = 0;

            Assert.Equal(-1, node.GetBalanceFactor());
        }

    }
}
