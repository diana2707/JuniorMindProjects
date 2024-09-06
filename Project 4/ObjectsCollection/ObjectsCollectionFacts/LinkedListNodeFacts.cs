using System;

namespace ObjectsCollection
{
    public class LinkedListNodeFacts
    {
        [Fact]
        public void ShouldReturnObjectOfTypeLinkedListNode()
        {
            var node = new LinkedListNode<int>(5);
            Assert.IsType<LinkedListNode<int>>(node);
        }

        [Fact]
        public void ShouldReturnAssignedValue()
        {
            var node = new LinkedListNode<int>(5);
            Assert.Equal(5, node.Value);
        }
    }
}
