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
    }
}
