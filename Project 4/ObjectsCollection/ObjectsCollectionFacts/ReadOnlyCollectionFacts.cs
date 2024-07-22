using System.Collections;

namespace ObjectsCollection
{
    public class ReadOnlyCollectionFacts
    {
        [Fact]
        public void EmptyReadOnlyCollectionExists()
        {
            ReadOnlyCollection<int> readOnlyCollection = new ReadOnlyCollection<int>();
            Assert.IsType<ReadOnlyCollection<int>>(readOnlyCollection);
        }
    }
}
