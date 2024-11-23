using System;

namespace ObjectsCollection
{
    public class AVLTreeFacts
    {
        [Fact]
        public void AVLTreeShouldReturnExists()
        {
            var tree = new AVLTree<int>();
            Assert.IsType<AVLTree<int>>(tree);
        }

        [Fact]
        public void CountShouldReturn0IfTreeIsEmpty()
        {
            var tree = new AVLTree<int>();
            Assert.Equal(0, tree.Count);
        }

        [Fact]
        public void AddShouldInsertFirstData()
        {
            var tree = new AVLTree<int>();
            tree.Add(10);

            Assert.Equal(1, tree.Count);
        }

        [Fact]
        public void AddShouldInsertSecondDataInLeftChildWhenSmaller()
        {
            var tree = new AVLTree<int>();
            tree.Add(10);
            tree.Add(5);

            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void AddShouldInsertDataInRightChildWhenLarger()
        {
            var tree = new AVLTree<int>();
            tree.Add(10);
            tree.Add(15);

            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void AddShouldTraverseAndInsertDataInFirstAvailableChild()
        {
            var tree = new AVLTree<int>();
            tree.Add(10);
            tree.Add(15);
            tree.Add(5);
            tree.Add(3);

            Assert.Equal(4, tree.Count);
        }
    }
}
