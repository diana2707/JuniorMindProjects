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
    }
}
