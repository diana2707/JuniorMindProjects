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
        public void PrefixIsEmptyStringWhenTreeIsEmpty()
        {
            RadixTreeNode node = new ();
            Assert.Empty(node.Prefix);
        }

        [Fact]
        public void ChildrenIsNullWhenTreeIsEmpty()
        {
            RadixTreeNode node = new ();
            Assert.Null(node.Children);
        }
    }
}
