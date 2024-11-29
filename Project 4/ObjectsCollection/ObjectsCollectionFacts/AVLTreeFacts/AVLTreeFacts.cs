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

            Assert.Contains(10, tree);
            Assert.Single(tree);
        }

        [Fact]
        public void AddShouldInsertSecondDataInLeftChildWhenSmaller()
        {
            var tree = new AVLTree<int>();
            tree.Add(10);
            tree.Add(5);

            Assert.Contains(5, tree);
            Assert.Contains(10, tree);
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void AddShouldInsertDataInRightChildWhenLarger()
        {
            var tree = new AVLTree<int>();
            tree.Add(10);
            tree.Add(15);

            Assert.Contains(10, tree);
            Assert.Contains(15, tree);
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

            Assert.Contains(3, tree);
            Assert.Contains(5, tree);
            Assert.Contains(10, tree);
            Assert.Contains(15, tree);
            Assert.Equal(4, tree.Count);
        }

        [Fact]
        public void EnumeratorShouldReturnAllItems()
        {
            var tree = new AVLTree<int>();
            tree.Add(10);
            tree.Add(15);
            tree.Add(5);
            tree.Add(3);

            int[] array = { 3, 5, 10, 15 };
            int index = 0;

            foreach (var item in tree)
            {
                Assert.Equal(item, array[index]);
                index++;
            }

            Assert.Equal(4, index);
        }

        [Fact]
        public void ContainsShouldReturnTrueIfItemExists()
        {
            var tree = new AVLTree<int>() { 10, 15, 3, 5 };

            Assert.True(tree.Contains(10));
            Assert.True(tree.Contains(15));
            Assert.True(tree.Contains(3));
            Assert.True(tree.Contains(5));

            Assert.False(tree.Contains(7));
        }

        [Fact]
        public void ContainsShouldReturnFalseIfItemDoNotExists()
        {
            var tree = new AVLTree<int>() { 10, 15, 3, 5 };

            Assert.False(tree.Contains(7));
        }

        [Fact]
        public void HeightShouldReturnCurrentHeightOfTree()
        {
            var tree = new AVLTree<int>() { 10, 15, 3, 5 };

            Assert.Equal(2, tree.Height);
        }

        [Fact]
        public void HeightShouldReturnCurrentHeightOfTreeInComplexTree()
        {
            var tree = new AVLTree<int>() { 16, 14, 31, 3, 29, 7, 5, 32, 40, 39 };

            Assert.Equal(3, tree.Height);
        }

        [Fact]
        public void AddShouldInsertItemAndBalanceTreeInLeftLeftCase()
        {
            var tree = new AVLTree<int>() { 16, 14, 7 };

            Assert.Equal(1, tree.Height);
        }

        [Fact]
        public void AddShouldInsertItemAndBalanceTreeInRightRightCase()
        {
            var tree = new AVLTree<int>() { 7, 14, 16 };

            Assert.Equal(1, tree.Height);
        }

        [Fact]
        public void AddShouldInsertItemAndBalanceTreeInLeftRightCase()
        {
            var tree = new AVLTree<int>() { 16, 14, 15 };

            Assert.Equal(1, tree.Height);
        }

        [Fact]
        public void AddShouldInsertItemAndBalanceTreeInRightLeftCase()
        {
            var tree = new AVLTree<int>() { 14, 16, 15 };

            Assert.Equal(1, tree.Height);
        }

        [Fact]
        public void RemoveShouldSetRootNullIfTreeIsSingleton()
        {
            var tree = new AVLTree<int>() { 16 };

            Assert.True(tree.Remove(16));
            Assert.Empty(tree);
        }

        [Fact]
        public void RemoveShouldremoveLeafNode()
        {
            var tree = new AVLTree<int>() { 16, 14 };

            Assert.True(tree.Remove(14));
            Assert.DoesNotContain(14, tree);
            Assert.Contains(16, tree);
        }

        [Fact]
        public void RemoveShouldRemoveLeafNodeInTreeWithMultipleLevels()
        {
            var tree = new AVLTree<int>() { 16, 10, 20, 9, 11, 19, 21 };

            Assert.True(tree.Remove(9));
            Assert.DoesNotContain(9, tree);
            Assert.Contains(16, tree);
            Assert.Contains(10, tree);
            Assert.Contains(20, tree);
            Assert.Contains(11, tree);
            Assert.Contains(19, tree);
            Assert.Contains(21, tree);
        }

        [Fact]
        public void RemoveShouldRemoveNodeWithSingleChild()
        {
            var tree = new AVLTree<int>() { 16, 10, 20, 9, 19, 21 };

            Assert.True(tree.Remove(10));
            Assert.DoesNotContain(10, tree);
            Assert.Contains(16, tree);
            Assert.Contains(9, tree);
            Assert.Contains(20, tree);
            Assert.Contains(19, tree);
            Assert.Contains(21, tree);
        }

        [Fact]
        public void RemoveShouldRemoveNodeWith2Children()
        {
            var tree = new AVLTree<int>() { 16, 10, 20, 9, 11, 19, 21 };

            Assert.True(tree.Remove(20));
            Assert.DoesNotContain(20, tree);
            Assert.Contains(10, tree);
            Assert.Contains(9, tree);
            Assert.Contains(16, tree);
            Assert.Contains(11, tree);
            Assert.Contains(19, tree);
            Assert.Contains(21, tree);
        }

        [Fact]
        public void RemoveShouldRemoveNodeAndBalanceTreeInLeftLeftCase()
        {
            var tree = new AVLTree<int>() { 16, 10, 20, 9, 11, 19, 21, 8 };

            Assert.True(tree.Remove(11));
            Assert.DoesNotContain(11, tree);
            Assert.Contains(10, tree);
            Assert.Contains(20, tree);
            Assert.Contains(9, tree);
            Assert.Contains(16, tree);
            Assert.Contains(19, tree);
            Assert.Contains(21, tree);
            Assert.Contains(8, tree);

            Assert.Equal(2, tree.Height);
        }
    }
}
