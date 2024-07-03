namespace ObjectsCollection
{
    public class SortedListFacts
    {
        [Fact]
        public void SortedListClassCanContainElements()
        {
            SortedList<int> list = new SortedList<int> { 1, 2 };
            Assert.Equal(2, list.Count);
        }
    }
}
