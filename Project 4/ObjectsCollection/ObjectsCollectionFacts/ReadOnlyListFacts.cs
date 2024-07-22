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

        [Fact]
        public void GetterShouldThrowArgumentOutOfRangeExceptionIfIndexIsNegative()
        {
            IList<int> list = new List<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<ArgumentOutOfRangeException>(() => readOnlyList[-1]);
        }

        [Fact]
        public void GetterShouldThrowArgumentOutOfRangeExceptionIfIndexIsEqualToCount()
        {
            IList<int> list = new List<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<ArgumentOutOfRangeException>(() => readOnlyList[3]);
        }

        [Fact]
        public void GetterShouldThrowArgumentOutOfRangeExceptionIfIndexIsGreaterThenCount()
        {
            IList<int> list = new List<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<ArgumentOutOfRangeException>(() => readOnlyList[8]);
        }

        [Fact]
        public void SetterShouldThrowNotSuppertedException()
        {
            IList<int> list = new List<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<NotSupportedException>(() => readOnlyList[2] = 4);
        }

        [Fact]
        public void IsReadOnlyShouldReturnTrue()
        {
            IList<int> list = new List<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.True(readOnlyList.IsReadOnly);
        }

        [Fact]
        public void AddMethodShouldThrowNotSuppertedException()
        {
            IList<int> list = new List<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<NotSupportedException>(() => readOnlyList.Add(5));
        }

        [Fact]
        public void GetEnumeratorMethodShouldAllowEnumerationThroughReadOnlyList()
        {
            IList<int> list = new List<int>() { 1, 2, 3 };
            foreach (int item in new ReadOnlyList<int>(list))
            {
                Assert.Equal(item, item);
            }
        }

        [Fact]
        public void ContainsMethodShouldReturnTrueWhenElementExists()
        {
            IList<int> list = new List<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.True(readOnlyList.Contains(2));
        }

        [Fact]
        public void ContainsMethodShouldReturnFalseWhenElementDoNotExists()
        {
            IList<int> list = new List<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.False(readOnlyList.Contains(12));
        }
    }
}
