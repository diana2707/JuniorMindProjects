namespace ObjectsCollection
{
    public class RadixTreeFacts
    {
        [Fact]
        public void RadixTreeClassExists()
        {
            var tree = new RadixTree();
            Assert.IsType<RadixTree>(tree);
        }

        [Fact]
        public void CountShouldReturn0WhenTreeIsEmpty()
        {
            var tree = new RadixTree();
            Assert.Equal(0, tree.Count);
        }

        [Fact]
        public void AddShouldAddFirstElementWhenTreeIsEmpty()
        {
            var tree = new RadixTree();
            tree.Add("cat");
            Assert.Equal(1, tree.Count);
        }

        [Fact]
        public void AddShouldAddElementWhenPrefixIsCommon()
        {
            var tree = new RadixTree();
            tree.Add("cat");
            tree.Add("car");
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void AddShouldAddElementWhenNewStringHasAnotherWordAsPrefix()
        {
            var tree = new RadixTree();
            tree.Add("cat");
            tree.Add("car");
            tree.Add("cabage");
            tree.Add("candle");
            Assert.True(tree.Contains("cabage"));
            Assert.True(tree.Contains("candle"));
            Assert.Equal(4, tree.Count);
        }

        [Fact]
        public void ContainsShouldReturnTrueIfStringExistsInTree()
        {
            var tree = new RadixTree();
            tree.Add("cat");
            tree.Add("car");
            Assert.True(tree.Contains("car"));
            Assert.True(tree.Contains("cat"));
        }

        [Fact]
        public void ContainsShouldReturnFalseIfStringDoNotExistsInTree()
        {
            var tree = new RadixTree();
            tree.Add("cat");
            tree.Add("car");
            Assert.False(tree.Contains("camel"));
            Assert.False(tree.Contains("ca"));
            Assert.False(tree.Contains("container"));
            Assert.False(tree.Contains("book"));
        }
    }
}
