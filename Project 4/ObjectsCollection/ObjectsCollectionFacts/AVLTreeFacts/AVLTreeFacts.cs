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

        //[Fact]
        //public void AddShouldInsertFirstData()
        //{
        //    var tree = new AVLTree<int>();
        //    tree.Add(10);

        //    Assert.Equal

        //}

    }
}
