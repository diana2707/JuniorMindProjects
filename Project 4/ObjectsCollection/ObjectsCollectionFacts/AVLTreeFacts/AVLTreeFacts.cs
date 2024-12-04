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
            Assert.Empty(tree);
            Assert.Equal(-1, tree.Height);
        }

        [Fact]
        public void AddShouldInsertFirstData()
        {
            var tree = new AVLTree<int>();
            tree.Add(10);

            Assert.Contains(10, tree);
            Assert.Single(tree);
            Assert.Equal(0, tree.Height);
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
            Assert.Equal(1, tree.Height);
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
            Assert.Equal(1, tree.Height);
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
            Assert.Equal(2, tree.Height);
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
        }

        [Fact]
        public void ContainsShouldReturnFalseIfItemDoNotExists()
        {
            var tree = new AVLTree<int>() { 10, 15, 3, 5 };

            Assert.False(tree.Contains(7));
        }

        [Fact]
        public void ContainsShouldReturnFalseIfNodeWasRemoved()
        {
            var tree = new AVLTree<int>() { 10, 15, 3, 5 };
            tree.Remove(10);

            Assert.False(tree.Contains(10));
            Assert.Equal(1, tree.Height);
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

            tree.Add(10);

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
            Assert.Equal(-1, tree.Height);
        }

        [Fact]
        public void RemoveShouldReturnFalseIfItemDoNotExist()
        {
            var tree = new AVLTree<int>() { 16 };

            Assert.False(tree.Remove(15));
        }

        [Fact]
        public void RemoveShouldRemoveLeafNode()
        {
            var tree = new AVLTree<int>() { 16, 14 };

            Assert.True(tree.Remove(14));
            Assert.DoesNotContain(14, tree);
            Assert.Contains(16, tree);
            Assert.Equal(0, tree.Height);
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

            Assert.Equal(2, tree.Height);
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

            Assert.Equal(2, tree.Height);
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

            Assert.Equal(2, tree.Height);
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

        [Fact]
        public void RemoveShouldRemoveNodeAndBalanceTreeInRightRightCase()
        {
            var tree = new AVLTree<int>() { 16, 10, 20, 9, 11, 21, 19, 22 };

            Assert.True(tree.Remove(19));
            Assert.DoesNotContain(19, tree);
            Assert.Contains(10, tree);
            Assert.Contains(9, tree);
            Assert.Contains(16, tree);
            Assert.Contains(11, tree);
            Assert.Contains(22, tree);
            Assert.Contains(21, tree);

            Assert.Equal(2, tree.Height);
        }

        [Fact]
        public void RemoveShouldRemoveNodeAndBalanceTreeInLeftRightCase()
        {
            var tree = new AVLTree<int>() { 16, 10, 20, 8, 11, 19, 21, 9 };

            Assert.True(tree.Remove(11));
            Assert.DoesNotContain(11, tree);
            Assert.Contains(10, tree);
            Assert.Contains(8, tree);
            Assert.Contains(9, tree);
            Assert.Contains(16, tree);
            Assert.Contains(20, tree);
            Assert.Contains(19, tree);
            Assert.Contains(21, tree);

            Assert.Equal(2, tree.Height);
        }

        [Fact]
        public void RemoveShouldRemoveNodeAndBalanceTreeInRightLeftCase()
        {
            var tree = new AVLTree<int>() { 16, 10, 20, 9, 11, 19, 22, 21 };

            Assert.True(tree.Remove(19));
            Assert.DoesNotContain(19, tree);
            Assert.Contains(16, tree);
            Assert.Contains(10, tree);
            Assert.Contains(20, tree);
            Assert.Contains(9, tree);
            Assert.Contains(11, tree);
            Assert.Contains(22, tree);
            Assert.Contains(21, tree);

            Assert.Equal(2, tree.Height);
        }

        [Fact]
        public void ClearShouldClearTree()
        {
            var tree = new AVLTree<int>() { 16, 10, 20 };

            tree.Clear();
            Assert.Empty(tree);
        }

        [Fact]
        public void CopyToShouldCopyElementsInArray()
        {
            var tree = new AVLTree<int>() { 16, 10, 20 };

            int[] array = new int[3];
            int count = 0;

            tree.CopyTo(array, 0);

            foreach (int n in tree)
            {
                Assert.Contains(n, array);
                count++;
            }

            Assert.Equal(3, count);
        }

        [Fact]
        public void CopyToShouldCopyElementsInArrayWhenIndexIsLargerThan0()
        {
            var tree = new AVLTree<int>() { 16, 10, 20 };

            int[] array = new int[5];
            int count = 0;

            tree.CopyTo(array, 2);

            foreach (int n in tree)
            {
                Assert.Contains(n, array);
                count++;
            }

            Assert.Equal(3, count);
        }

        [Fact]
        public void IsResdOnlyReturnsTrue()
        {
            var tree = new AVLTree<int>() { 16, 10, 20 };

            Assert.False(tree.IsReadOnly);
        }

        [Fact]
        public void AddThrowsArgumentNullExceptionWhenArgumentisNull()
        {
            var tree = new AVLTree<string>();

            Assert.Throws<ArgumentNullException>(() => tree.Add(null));
        }

        [Fact]
        public void AddThrowsArgumentExceptionWhenItemExists()
        {
            var tree = new AVLTree<int>() { 10, 5, 15 };

            Assert.Throws<ArgumentException>(() => tree.Add(5));
        }

        [Fact]
        public void ContainsThrowsArgumentNullExceptionWhenArgumentisNull()
        {
            var tree = new AVLTree<string>();

            Assert.Throws<ArgumentNullException>(() => tree.Contains(null));
        }

        [Fact]
        public void RemoveThrowsArgumentNullExceptionWhenArgumentisNull()
        {
            var tree = new AVLTree<string>();

            Assert.Throws<ArgumentNullException>(() => tree.Remove(null));
        }

        [Fact]
        public void AAAaaaaa()
        {
            var tree = new AVLTree<int>() { 5, 8, 42, 100, 77, 95, 12, 37, 83, 73, 29, 36, 31, 19, 89 };
            tree.Add(10);

            Assert.Equal(4, tree.Height);
        }

        [Fact]
        public void AAAaaaaaasd()
        {
            var tree = new AVLTree<int>() { 5, 8, 42, 100, 77, 95, 12, 37, 83, 73, 29, 36, 31, 19, 89 };
            tree.Remove(5);
            tree.Remove(73);

            Assert.Equal(3, tree.Height);
        }
    }
}
