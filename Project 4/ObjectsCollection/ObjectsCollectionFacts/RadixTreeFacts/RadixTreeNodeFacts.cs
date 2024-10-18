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
            Assert.Empty(node.Children);
        }

        [Fact]
        public void IsWordIsNullWhenTreeIsEmpty()
        {
            RadixTreeNode node = new ();
            Assert.False(node.IsWord);
        }

        [Fact]
        public void PrefixIndexIsNegative1WhenTreeIsEmpty()
        {
            RadixTreeNode node = new ();
            Assert.Equal(0, node.PrefixPosition);
        }
    }
}
