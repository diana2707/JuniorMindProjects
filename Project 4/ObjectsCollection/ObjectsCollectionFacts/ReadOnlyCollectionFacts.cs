using System.Collections;

namespace ObjectsCollection
{
    public class ReadOnlyCollectionFacts
    {
        [Fact]
        public void ReadOnlyCollectionIsWrapperForIListT()
        {
            IList<int> list = new List<int>();
            ReadOnlyCollection<int> readOnlyCollection = new ReadOnlyCollection<int>(list);
            Assert.IsType<ReadOnlyCollection<int>>(readOnlyCollection);
        }

        [Fact]
        public void ThrowArgumentNullExceptionIfListIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ReadOnlyCollection<int>(null));
        }
    }
}
