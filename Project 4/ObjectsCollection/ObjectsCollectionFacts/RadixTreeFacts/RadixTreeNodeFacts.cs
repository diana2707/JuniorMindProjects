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
        public void PrefixIsEmptyWhenTreeIsEmpty()
        {
            RadixTreeNode<string> node = new ();
            Assert.Empty(node.Prefix);
        }

        [Fact]
        public void PathLengthIs0WhenTreeIsEmpty()
        {
            RadixTreeNode<string> node = new ();
            Assert.Equal(0, node.PathLength);
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
            Assert.False(node.IsEnd);
        }
    }
}
