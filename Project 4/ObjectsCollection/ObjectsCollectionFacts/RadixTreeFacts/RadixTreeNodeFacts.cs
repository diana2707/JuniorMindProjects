namespace ObjectsCollection
{
    public class RadixTreeNodeFacts
    {
        [Fact]
        public void RadixTreeNodeClassExists()
        {
            RadixTreeNode node = new ();
            Assert.IsType<RadixTreeNode>(node);
        }

        [Fact]
        public void PrefixIsEmptyString()
        {
            RadixTreeNode node = new ();
            Assert.Equal(string.Empty, node.Prefix);
        }
    }
}
