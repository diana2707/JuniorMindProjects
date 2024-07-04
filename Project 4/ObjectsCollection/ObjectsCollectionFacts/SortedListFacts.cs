namespace ObjectsCollection
{
    public class SortedListFacts
    {
        [Fact]
        public void SortedListClassCanContainElements()
        {
            SortedList<int> sortedList = new SortedList<int> { 1, 2 };
            Assert.Equal(2, sortedList.Count);
        }

        [Fact]
        public void SortMethodShouldReturnSortedListWhenTypeIsInt()
        {
            SortedList<int> list = new SortedList<int> { 3, 2, 1 };

            Assert.Equal(1, list[0]);
            Assert.Equal(2, list[1]);
            Assert.Equal(3, list[2]);
        }

        [Fact]
        public void SortMethodShouldReturnSortedListWhenTypeIsString()
        {
            SortedList<string> list = new SortedList<string> { "z", "p", "a" };

            Assert.Equal("a", list[0]);
            Assert.Equal("p", list[1]);
            Assert.Equal("z", list[2]);
        }

        [Fact]
        public void SortMethodShouldReturnSortedListWhenTypeIsChar()
        {
            SortedList<char> list = new SortedList<char> { 'r', 'h', 'c' };

            Assert.Equal('c', list[0]);
            Assert.Equal('h', list[1]);
            Assert.Equal('r', list[2]);
        }

        [Fact]
        public void SortMethodShouldReturnSortedListWhenTypeIsBool()
        {
            SortedList<bool> list = new SortedList<bool> { true, false };

            Assert.False(list[0]);
            Assert.True(list[1]);
        }

        [Fact]
        public void AddMethodShouldAddElementAndSortArray()
        {
            SortedList<int> list = new SortedList<int> { 1, 2, 5, 8 };
            list.Add(3);

            Assert.Equal(1, list[0]);
            Assert.Equal(2, list[1]);
            Assert.Equal(3, list[2]);
            Assert.Equal(5, list[3]);
            Assert.Equal(8, list[4]);
        }

        [Fact]
        public void IndexerShouldSetElementIfIsSorted()
        {
            SortedList<int> list = new SortedList<int> { 1, 4, 5, 8 };
            list[1] = 3;
            Assert.Equal(1, list[0]);
            Assert.Equal(3, list[1]);
            Assert.Equal(5, list[2]);
            Assert.Equal(8, list[3]);
        }

        [Fact]
        public void IndexerShouldNotSetElementIfIsNotSorted()
        {
            SortedList<int> list = new SortedList<int> { 1, 4, 5, 8 };
            list[3] = 3;
            Assert.Equal(1, list[0]);
            Assert.Equal(4, list[1]);
            Assert.Equal(5, list[2]);
            Assert.Equal(8, list[3]);
        }

        [Fact]
        public void InsertMethodShouldInsertElementIfIsSorted()
        {
            SortedList<int> list = new SortedList<int> { 1, 4, 5, 8 };
            list.Insert(1, 3);
            Assert.Equal(1, list[0]);
            Assert.Equal(3, list[1]);
            Assert.Equal(4, list[2]);
            Assert.Equal(5, list[3]);
            Assert.Equal(8, list[4]);
        }

        [Fact]
        public void InsertMethodShouldNotInsertElementIfIsNotSorted()
        {
            SortedList<int> list = new SortedList<int> { 1, 4, 5, 8 };
            list.Insert(3, 3);
            Assert.Equal(1, list[0]);
            Assert.Equal(4, list[1]);
            Assert.Equal(5, list[2]);
            Assert.Equal(8, list[3]);
        }

        [Fact]
        public void IndexOfMethodShouldReturnCorrectIndex()
        {
            SortedList<int> list = new SortedList<int> { 1, 4, 5, 8 };
            Assert.Equal(2, list.IndexOf(5));
        }

        [Fact]
        public void CanBeEnumerated()
        {
            foreach (int element in new SortedList<int> { 1, 4, 5, 8, 9 })
            {
                Assert.NotNull(element);
                Assert.Equal(element, element);
            }
        }

        [Fact]
        public void ContainMethodShouldReturnTrueWhenElementExists()
        {
            SortedList<int> list = new SortedList<int> { 1, 4, 5, 8 };
            Assert.True(list.Contains(1));
        }

        [Fact]
        public void ContainMethodShouldReturnFalseWhenElementDoesNotExist()
        {
            SortedList<int> list = new SortedList<int> { 1, 4, 5, 8 };
            Assert.False(list.Contains(9));
        }

        [Fact]
        public void ClearMethodShouldClearCollection()
        {
            SortedList<int> list = new SortedList<int> { 1, 4, 5, 8 };
            list.Clear();
            Assert.Empty(list);
        }

        [Fact]
        public void RemoveAtMethodShouldRemoveElementAtIndex()
        {
            SortedList<int> list = new SortedList<int> { 1, 4, 5, 8 };
            list.RemoveAt(0);
            Assert.Equal(4, list[0]);
            Assert.Equal(5, list[1]);
            Assert.Equal(8, list[2]);
        }

        [Fact]
        public void RemoveMethodShouldRemoveElement()
        {
            SortedList<int> list = new SortedList<int> { 1, 4, 5, 8 };
            list.Remove(5);
            Assert.Equal(1, list[0]);
            Assert.Equal(4, list[1]);
            Assert.Equal(8, list[2]);
        }
    }
}
