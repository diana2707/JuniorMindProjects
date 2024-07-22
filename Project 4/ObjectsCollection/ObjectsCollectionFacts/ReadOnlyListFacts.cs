using System.Collections;

namespace ObjectsCollection
{
    public class ReadOnlyListFacts
    {
        [Fact]
        public void ReadOnlyListIsWrapperForIListT()
        {
            IList<int> list = new List<int>();
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.IsType<ReadOnlyList<int>>(readOnlyList);
        }

        [Fact]
        public void ThrowArgumentNullExceptionIfListIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ReadOnlyList<int>(null));
        }

        [Fact]
        public void CountMethodShouldReturnNumberOfElements()
        {
            IList<int> list = new List<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Equal(3, readOnlyList.Count);
        }

        [Fact]
        public void GetterShouldReturnElementFromList()
        {
            IList<int> list = new List<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Equal(3, readOnlyList[2]);
        }

    }
}
