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
            var list = new LinkedList<int>();
            list.Add(1);
            list.Add(2);

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
        public void RemoveShouldRemoveNodeFromListAndReturnTrue()
        {
            var list = new LinkedList<int>() { 1, 2, 3 };

            Assert.True(list.Remove(3));
            Assert.Equal(1, list.First.Value);
            Assert.Equal(2, list.Last.Value);
        }

        [Fact]
        public void RemoveShouldNotRemoveNodeFromListAndReturnFalseIfValueIsMissing()
        {
            var list = new LinkedList<int>() { 1, 2, 3 };

            Assert.False(list.Remove(5));
        }

        [Fact]
        public void RemoveShouldReturnFalseIfListIsEmpty()
        {
            var list = new LinkedList<int>();

            Assert.False(list.Remove(5));
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

        [Fact]
        public void FindShouldReturnFirstNodeThatContainsSpecificValue()
        {
            var list = new LinkedList<int>() { 1, 2, 3, 2, 4 };
            LinkedListNode<int> node = list.Find(2);

            Assert.Equal(2, node.Value);
            Assert.Equal(1, node.Previous.Value);
            Assert.Equal(3, node.Next.Value);
        }

        [Fact]
        public void FindShouldReturnNullWhenListIsEmpty()
        {
            var list = new LinkedList<int>();
            LinkedListNode<int> node = list.Find(2);

            Assert.Null(node);
        }

        [Fact]
        public void FindShouldReturnNullWhenListDoNotContainValue()
        {
            var list = new LinkedList<int>() { 1, 2, 3, 2, 4 };
            LinkedListNode<int> node = list.Find(5);

            Assert.Null(node);
        }

        [Fact]
        public void FindShouldReturnLastNodeThatContainsSpecificValue()
        {
            var list = new LinkedList<int>() { 1, 2, 3, 2, 4 };
            LinkedListNode<int> node = list.FindLast(2);

            Assert.Equal(2, node.Value);
            Assert.Equal(3, node.Previous.Value);
            Assert.Equal(4, node.Next.Value);
        }

        [Fact]
        public void FindLastShouldReturnNullWhenListIsEmpty()
        {
            var list = new LinkedList<int>();
            LinkedListNode<int> node = list.FindLast(2);

            Assert.Null(node);
        }

        [Fact]
        public void FindLastShouldReturnNullWhenListDoNotContainValue()
        {
            var list = new LinkedList<int>() { 1, 2, 3, 2, 4 };
            LinkedListNode<int> node = list.FindLast(5);

            Assert.Null(node);
        }

        [Fact]
        public void AddAfterShouldAddNodeAfterSpecifiedNodeUsingPassedValue()
        {
            var list = new LinkedList<int>() { 1, 2, 3, 2, 4 };
            LinkedListNode<int> previousNode = list.Find(3);
            LinkedListNode<int> nextNode = list.FindLast(2);

            list.AddAfter(previousNode, 4);

            LinkedListNode<int> newNode = list.Find(4);

            Assert.Equal(4, previousNode.Next.Value);
            Assert.Equal(4, nextNode.Previous.Value);
            Assert.Equal(3, newNode.Previous.Value);
            Assert.Equal(2, newNode.Next.Value);
            Assert.Equal(6, list.Count);
        }

        [Fact]
        public void AddAfterShouldAddNodeAfterSpecifiedNodeUsingNewNode()
        {
            var list = new LinkedList<int>() { 1, 2, 3, 2, 4 };
            LinkedListNode<int> previousNode = list.Find(3);
            LinkedListNode<int> nextNode = list.FindLast(2);
            LinkedListNode<int> newNode = new LinkedListNode<int>(4);

            list.AddAfter(previousNode, newNode);

            Assert.Equal(4, previousNode.Next.Value);
            Assert.Equal(4, nextNode.Previous.Value);
            Assert.Equal(3, newNode.Previous.Value);
            Assert.Equal(2, newNode.Next.Value);
            Assert.Equal(6, list.Count);
        }

        [Fact]
        public void AddBeforeShouldAddNodeBeforeSpecifiedNodeUsingNewNode()
        {
            var list = new LinkedList<int>() { 1, 2, 3, 2, 4 };
            LinkedListNode<int> previousNode = list.Find(3);
            LinkedListNode<int> nextNode = list.FindLast(2);
            LinkedListNode<int> newNode = new LinkedListNode<int>(4);

            list.AddBefore(nextNode, newNode);

            Assert.Equal(4, previousNode.Next.Value);
            Assert.Equal(4, nextNode.Previous.Value);
            Assert.Equal(3, newNode.Previous.Value);
            Assert.Equal(2, newNode.Next.Value);
            Assert.Equal(6, list.Count);
        }
    }
}
