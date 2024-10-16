using System.Collections;

namespace ObjectsCollection
{
    public class ReadOnlyListFacts
    {
        [Fact]
        public void ReadOnlyListIsWrapperForIListObject()
        {
            CustomList<int> list = new CustomList<int>();
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
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Equal(3, readOnlyList.Count);
        }

        [Fact]
        public void GetterShouldReturnElementFromList()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Equal(3, readOnlyList[2]);
        }

        [Fact]
        public void GetterShouldThrowArgumentOutOfRangeExceptionIfIndexIsNegative()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<ArgumentOutOfRangeException>(() => readOnlyList[-1]);
        }

        [Fact]
        public void GetterShouldThrowArgumentOutOfRangeExceptionIfIndexIsEqualToCount()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<ArgumentOutOfRangeException>(() => readOnlyList[3]);
        }

        [Fact]
        public void GetterShouldThrowArgumentOutOfRangeExceptionIfIndexIsGreaterThenCount()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<ArgumentOutOfRangeException>(() => readOnlyList[8]);
        }

        [Fact]
        public void SetterShouldThrowNotSuppertedException()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<NotSupportedException>(() => readOnlyList[2] = 4);
        }

        [Fact]
        public void IsReadOnlyShouldReturnTrue()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.True(readOnlyList.IsReadOnly);
        }

        [Fact]
        public void AddMethodShouldThrowNotSuppertedException()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<NotSupportedException>(() => readOnlyList.Add(5));
        }

        [Fact]
        public void GetEnumeratorMethodShouldAllowEnumerationThroughReadOnlyList()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            foreach (int item in new ReadOnlyList<int>(list))
            {
                Assert.Equal(item, item);
            }
        }

        [Fact]
        public void ContainsMethodShouldReturnTrueWhenElementExists()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.True(readOnlyList.Contains(2));
        }

        [Fact]
        public void ContainsMethodShouldReturnFalseWhenElementDoNotExists()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.False(readOnlyList.Contains(12));
        }

        [Fact]
        public void CopyToMethodShouldCopyElements()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            int[] array = new int[5];
            readOnlyList.CopyTo(array, 0);

            Assert.Equal(1, array[0]);
            Assert.Equal(2, array[1]);
            Assert.Equal(3, array[2]);
        }

        [Fact]
        public void IndexOfMethodShouldReturnIndexOfElement()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Equal(0, readOnlyList.IndexOf(1));
        }

        [Fact]
        public void IndexOfMethodShouldReturnNegativ1IfElementDoNotExists()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Equal(-1, readOnlyList.IndexOf(5));
        }

        [Fact]
        public void InsertMethodShouldThrowNotSupportedException()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<NotSupportedException>(() => readOnlyList.Insert(0, 8));
        }

        [Fact]
        public void ClearMethodShouldThrowNotSupportedException()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<NotSupportedException>(() => readOnlyList.Clear());
        }

        [Fact]
        public void RemoveAtMethodShouldThrowNotSupportedException()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<NotSupportedException>(() => readOnlyList.RemoveAt(0));
        }

        [Fact]
        public void RemoveMethodShouldThrowNotSupportedException()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            ReadOnlyList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<NotSupportedException>(() => readOnlyList.Remove(1));
        }

        [Fact]
        public void ReadOnlyListIsIListObject()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };
            IList<int> readOnlyList = new ReadOnlyList<int>(list);
            Assert.IsAssignableFrom<IList<int>>(readOnlyList);
        }
    }
}
