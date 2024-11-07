namespace ObjectsCollection
{
    public class RadixTreeNodeFacts
    {
        [Fact]
        public void RadixTreeNodeClassExists()
        {
            RadixTreeNode<string> node = new ();
            Assert.IsType<RadixTreeNode<string>>(node);
        }

        [Fact]
        public void ChildrenIsNullWhenTreeIsEmpty()
        {
            RadixTreeNode<string> node = new ();
            Assert.Empty(node.Children);
        }

        [Fact]
        public void IsWordIsNullWhenTreeIsEmpty()
        {
            RadixTreeNode<string> node = new ();
            Assert.False(node.IsWord);
        }
    }
}
