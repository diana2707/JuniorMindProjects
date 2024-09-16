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
            int[] array = { 1 };
            var list = new LinkedList<int>(array);

            Assert.Equal(1, list.First.Value);
        }

        [Fact]
        public void AddShouldAddNodesToList()
        {
            int[] array = { 1, 2 };
            var list = new LinkedList<int>(array);
            
            Assert.Equal(1, list.First.Value);
            Assert.Equal(2, list.Last.Value);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void ClearShouldSetCount0AndFirstAndLastToNull()
        {
            int[] array = { 1, 2 };
            var list = new LinkedList<int>(array);

            list.Clear();

            Assert.Null(list.First);
            Assert.Null(list.Last);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void ContainsShouldReturnTrueIfListContainsValue()
        {
            int[] array = { 1, 2 };
            var list = new LinkedList<int>(array);

            Assert.True(list.Contains(2));
            Assert.False(list.Contains(3));
        }

        [Fact]
        public void CopyTpShouldCopyNodesValuesToArray()
        {
            int[] collection = { 1, 2, 3 };
            var list = new LinkedList<int>(collection);
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
            int[] collection = { 1, 2, 3 };
            var list = new LinkedList<int>(collection);

            Assert.True(list.Remove(3));
            Assert.Equal(1, list.First.Value);
            Assert.Equal(2, list.Last.Value);
        }

        [Fact]
        public void RemoveShouldNotRemoveNodeFromListAndReturnFalseIfValueIsMissing()
        {
            int[] collection = { 1, 2, 3 };
            var list = new LinkedList<int>(collection);

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
            int[] collection = { 1, 2, 3 };
            var list = new LinkedList<int>(collection);

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
            int[] collection = { 1, 2, 3, 2, 4 };
            var list = new LinkedList<int>(collection);

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
            int[] collection = { 1, 2, 3, 2, 4 };
            var list = new LinkedList<int>(collection);

            LinkedListNode<int> node = list.Find(5);

            Assert.Null(node);
        }

        [Fact]
        public void FindShouldReturnLastNodeThatContainsSpecificValue()
        {
            int[] collection = { 1, 2, 3, 2, 4 };
            var list = new LinkedList<int>(collection);

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
            int[] collection = { 1, 2, 3, 2, 4 };
            var list = new LinkedList<int>(collection);

            LinkedListNode<int> node = list.FindLast(5);

            Assert.Null(node);
        }

        [Fact]
        public void AddAfterShouldAddNodeAfterSpecifiedNodeUsingPassedValue()
        {
            int[] collection = { 1, 2, 3, 2, 4 };
            var list = new LinkedList<int>(collection);

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
            int[] collection = { 1, 2, 3, 2, 4 };
            var list = new LinkedList<int>(collection);

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
            int[] collection = { 1, 2, 3, 2, 4 };
            var list = new LinkedList<int>(collection);

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

        [Fact]
        public void AddBeforeShouldAddNodeAfterSpecifiedNodeUsingPassedValue()
        {
            int[] collection = { 1, 2, 3, 2, 4 };
            var list = new LinkedList<int>(collection);

            LinkedListNode<int> previousNode = list.Find(3);
            LinkedListNode<int> nextNode = list.FindLast(2);

            list.AddBefore(nextNode, 4);

            LinkedListNode<int> newNode = list.Find(4);

            Assert.Equal(4, previousNode.Next.Value);
            Assert.Equal(4, nextNode.Previous.Value);
            Assert.Equal(3, newNode.Previous.Value);
            Assert.Equal(2, newNode.Next.Value);
            Assert.Equal(6, list.Count);
        }

        [Fact]
        public void AddFirstShouldAddNewNodeOnFirstPosition()
        {
            int[] collection = { 1, 2, 3, 2, 4 };
            var list = new LinkedList<int>(collection);

            LinkedListNode<int> newNode = new LinkedListNode<int>(0);
            LinkedListNode<int> nextNode = list.Find(1);

            list.AddFirst(newNode);

            Assert.Equal(0, list.First.Value);
            Assert.Equal(1, newNode.Next.Value);
            Assert.Equal(0, nextNode.Previous.Value);
            Assert.Equal(6, list.Count);
        }

        [Fact]
        public void AddFirstShouldCreateNewNodeAndAddValueOnFirstPosition()
        {
            int[] collection = { 1, 2, 3, 2, 4 };
            var list = new LinkedList<int>(collection);

            LinkedListNode<int> nextNode = list.Find(1);

            list.AddFirst(0);

            LinkedListNode<int> newNode = list.Find(0);

            Assert.Equal(0, list.First.Value);
            Assert.Equal(1, newNode.Next.Value);
            Assert.Equal(0, nextNode.Previous.Value);
            Assert.Equal(6, list.Count);
        }

        [Fact]
        public void AddFirstShouldAddNewNodeWhenListIsEmptyUsingPassedValue()
        {
            var list = new LinkedList<int>();

            list.AddFirst(5);

            Assert.Equal(5, list.Last.Value);
            Assert.Equal(5, list.First.Value);
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void AddLastShouldAddNewNodeOnLastPosition()
        {
            int[] collection = { 1, 2, 3, 2, 4 };
            var list = new LinkedList<int>(collection);

            LinkedListNode<int> newNode = new LinkedListNode<int>(5);
            LinkedListNode<int> previousNode = list.Find(4);

            list.AddLast(newNode);

            Assert.Equal(5, list.Last.Value);
            Assert.Equal(4, newNode.Previous.Value);
            Assert.Equal(5, previousNode.Next.Value);
            Assert.Equal(6, list.Count);
        }

        [Fact]
        public void AddLastShouldAddNewNodeOnLastPositionUsingPassedValue()
        {
            int[] collection = { 1, 2, 3, 2, 4 };
            var list = new LinkedList<int>(collection);

            LinkedListNode<int> previousNode = list.Find(4);

            list.AddLast(5);

            LinkedListNode<int> newNode = list.Find(5);

            Assert.Equal(5, list.Last.Value);
            Assert.Equal(4, newNode.Previous.Value);
            Assert.Equal(5, previousNode.Next.Value);
            Assert.Equal(6, list.Count);
        }

        [Fact]
        public void AddLastShouldAddNewNodeWhenListIsEmptyUsingPassedValue()
        {
            var list = new LinkedList<int>();

            list.AddLast(5);

            Assert.Equal(5, list.Last.Value);
            Assert.Equal(5, list.First.Value);
            Assert.Equal(1, list.Count);
        }
    }
}
