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

    }
}
