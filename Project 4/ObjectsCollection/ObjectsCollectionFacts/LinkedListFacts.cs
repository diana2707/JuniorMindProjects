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

        [Fact]
        public void CountShouldReturn0WhenListIsEmpty()
        {
            var list = new LinkedList<int>();
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void IsreadOnlyShouldReturnFalse()
        {
            var list = new LinkedList<int>();
            Assert.False(list.IsReadOnly);
        }

        [Fact]
        public void FirstShouldReturnNullWhenListIsEmpty()
        {
            var list = new LinkedList<int>();
            Assert.Null(list.First);
        }

        [Fact]
        public void LastShouldReturnNullWhenListIsEmpty()
        {
            var list = new LinkedList<int>();
            Assert.Null(list.Last);
        }

        [Fact]
        public void AddShouldAddFirstNodeToList()
        {
            var list = new LinkedList<int>();
            var node = new LinkedListNode<int>(1);
            list.Add(node);
            Assert.Equal(node, list.First);
        }

        [Fact]
        public void AddShouldAddNodesToList()
        {
            var list = new LinkedList<int>();
            var first = new LinkedListNode<int>(1);
            var second = new LinkedListNode<int>(2);
            list.Add(first);
            list.Add(second);
            Assert.Equal(first, list.First);
            Assert.Equal(second, list.Last);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void ClearShouldSetCount0AndFirstAndLastToNull()
        {
            var list = new LinkedList<int>();
            var first = new LinkedListNode<int>(1);
            var second = new LinkedListNode<int>(2);
            list.Add(first);
            list.Add(second);
            list.Clear();
            Assert.Null(list.First);
            Assert.Null(list.Last);
            Assert.Equal(0, list.Count);
        }
    }
}
