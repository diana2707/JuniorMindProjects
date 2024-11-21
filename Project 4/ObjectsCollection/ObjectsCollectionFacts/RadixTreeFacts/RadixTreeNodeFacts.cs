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
            Assert.Empty(node.Segment);
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

        [Fact]
        public void AddLeafAddsNodeToParent()
        {
            RadixTreeNode<char> parent = new ();
            List<char> segment = new () { 'a' };

            RadixTreeNode<char> leaf = parent.AddLeaf(segment);

            Assert.Single(parent.Children);
            Assert.Equal(segment, leaf.Segment);
            Assert.True(leaf.IsEnd);
        }

        [Fact]
        public void SplitCreates2Nodes()
        {
            RadixTreeNode<char> node = new ();
            node.Segment = ['a', 'b'];
            node.IsEnd = true;
            node.Children.Add(new RadixTreeNode<char>());
            node.Children.Add(new RadixTreeNode<char>());

            node.Split(1);

            RadixTreeNode<char> newNode = node.Children[0];

            Assert.Single(node.Children);
            Assert.Equal(['a'], node.Segment);
            Assert.False(node.IsEnd);

            Assert.Equal(2, newNode.Children.Count);
            Assert.Equal(['b'], newNode.Segment);
            Assert.True(newNode.IsEnd);
        }

        [Fact]
        public void RemoveChildNodeFromParent()
        {
            RadixTreeNode<char> parent = new ();
            RadixTreeNode<char> child = new ();

            parent.Children.Add(child);

            parent.Remove(child);

            Assert.Empty(parent.Children);
        }
    }
}
