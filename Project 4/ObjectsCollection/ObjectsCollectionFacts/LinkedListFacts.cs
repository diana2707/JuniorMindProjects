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
            var list = new LinkedList<int>() { 1 };

            Assert.Equal(1, list.First.Value);
        }

        [Fact]
        public void AddShouldAddNodesToList()
        {
            var list = new LinkedList<int>() { 1, 2 };

            Assert.Equal(1, list.First.Value);
            Assert.Equal(2, list.Last.Value);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void ClearShouldSetCount0AndFirstAndLastToNull()
        {
            var list = new LinkedList<int>() { 1, 2 };

            list.Clear();

            Assert.Null(list.First);
            Assert.Null(list.Last);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void ContainsShouldReturnTrueIfListContainsValue()
        {
            var list = new LinkedList<int>() { 1, 2 };

            Assert.True(list.Contains(2));
            Assert.False(list.Contains(3));
        }

        [Fact]
        public void CopyTpShouldCopyNodesValuesToArray()
        {
            var list = new LinkedList<int>() { 1, 2, 3 };
            var array = new int[4];

            list.CopyTo(array, 1);

            Assert.Equal(0, array[0]);
            Assert.Equal(1, array[1]);
            Assert.Equal(2, array[2]);
            Assert.Equal(3, array[3]);
        }

        [Fact]
        public void RemoveShouldRemoveNodeFromList()
        {
            var list = new LinkedList<int>() { 1, 2, 3 };

            list.Remove(3);

            Assert.Equal(1, list.First.Value);
            Assert.Equal(2, list.Last.Value);
        }

        [Fact]
        public void ShouldAllowEnumeration()
        {
            var list = new LinkedList<int>() { 1, 2, 3 };

            LinkedListNode<int> current = list.First;
            foreach (var item in list)
            {
                Assert.Equal(current.Value, item);
                current = current.Next;
            }
        }
    }
}
