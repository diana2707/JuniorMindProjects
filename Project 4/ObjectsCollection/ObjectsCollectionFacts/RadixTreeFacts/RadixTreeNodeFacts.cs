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
        public void ChildrenIsEmptyWhenTreeIsEmpty()
        {
            RadixTreeNode<string> node = new ();
            Assert.Empty(node.Children);
        }

        [Fact]
        public void IsWordIsFalseWhenTreeIsEmpty()
        {
            RadixTreeNode<string> node = new ();
            Assert.False(node.IsWord);
        }

        [Fact]
        public void ValueIsNullWhenTreeIsEmpty()
        {
            RadixTreeNode<string> node = new ();
            Assert.Null(node.Value);
        }
    }
}
