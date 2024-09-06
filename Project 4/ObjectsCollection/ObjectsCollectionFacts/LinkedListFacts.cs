using System;

namespace ObjectsCollection
{
    public class LinkedListFacts
    {
        [Fact]
        public void ShouldReturnObjectOfTypeLinkedList()
        {
            var list = new LinkedList<int>();
            Assert.IsType<LinkedList<int>>(list);
        }
    }
}
