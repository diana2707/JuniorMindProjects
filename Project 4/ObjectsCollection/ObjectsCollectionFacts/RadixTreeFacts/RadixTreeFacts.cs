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
    }
}
