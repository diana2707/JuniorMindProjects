using ObjectsCollection.ObjectsCollection.RadixTree;
using System;

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
    }
}
