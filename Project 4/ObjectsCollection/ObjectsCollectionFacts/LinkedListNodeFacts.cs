using System;

namespace ObjectsCollection
{
    public class LinkedListNodeFacts
    {
        [Fact]
        public void ShouldReturnObjectOfTypeLinkedListNode()
        {
            var node = new LinkedListNode<int>();
            Assert.IsType<LinkedListNode<int>>(node);
        }
    }
}
