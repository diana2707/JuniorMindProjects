namespace ObjectsCollection
{
    public class SortedIntArrayFacts
    {
        [Fact]
        public void SortMethodShouldSortTheArray()
        {
            SortedIntArray array = new SortedIntArray();
            array.Add(4);
            array.Add(5);
            array.Add(2);
            array.Add(1);
            array.Add(3);

            Assert.Equal(1, array[0]);
            Assert.Equal(2, array[1]);
            Assert.Equal(3, array[2]);
            Assert.Equal(4, array[3]);
            Assert.Equal(5, array[4]);
        }

        [Fact]
        public void AddMethodShouldAddOneElementAndPlaceInSortedOrder()
        {
            SortedIntArray array = new SortedIntArray();
            array.Add(5);
            array.Add(1);
            array.Add(8);
            array.Add(4);

            Assert.Equal(1, array[0]);
            Assert.Equal(4, array[1]);
            Assert.Equal(5, array[2]);
            Assert.Equal(8, array[3]);
        }

        [Fact]
        public void CountShouldReturnCorrectNumberOfEementsInArray()
        {
            SortedIntArray array = new SortedIntArray();
            array.Add(5);
            array.Add(1);
            array.Add(8);
            array.Add(4);

            Assert.Equal(4, array.Count);
        }

        [Fact]
        public void ContainsMethodShouldReturnTrueWhenElementExists()
        {
            SortedIntArray array = new SortedIntArray();
            array.Add(5);
            array.Add(1);
            array.Add(8);
            array.Add(4);

            Assert.True(array.Contains(4));
        }

        [Fact]
        public void ContainsMethodShouldReturnFalseWhenElementDoesNotExist()
        {
            SortedIntArray array = new SortedIntArray();
            array.Add(5);
            array.Add(1);
            array.Add(8);
            array.Add(4);

            Assert.False(array.Contains(7));
        }

        [Fact]
        public void IndexOfShouldReturnCorrectIndexInTheSortedArray()
        {
            SortedIntArray array = new SortedIntArray();
            array.Add(5);
            array.Add(1);
            array.Add(8);
            array.Add(4);

            Assert.Equal(0, array.IndexOf(1));
            Assert.Equal(1, array.IndexOf(4));
            Assert.Equal(2, array.IndexOf(5));
            Assert.Equal(3, array.IndexOf(8));
        }

        [Fact]
        public void InsertMethodShouldInsertMiddleElementIfPreservesTheArraySorted()
        {
            SortedIntArray array = new SortedIntArray();
            array.Add(5);
            array.Add(1);
            array.Add(8);
            array.Add(4);
            array.Insert(1, 2);

            Assert.Equal(0, array.IndexOf(1));
            Assert.Equal(1, array.IndexOf(2));
            Assert.Equal(2, array.IndexOf(4));
            Assert.Equal(3, array.IndexOf(5));
            Assert.Equal(4, array.IndexOf(8));
        }

        [Fact]
        public void InsertMethodShouldInsertFirstElementIfPreservesTheArraySorted()
        {
            SortedIntArray array = new SortedIntArray();
            array.Add(5);
            array.Add(1);
            array.Add(8);
            array.Add(4);
            array.Insert(0, 0);

            Assert.Equal(0, array.IndexOf(0));
            Assert.Equal(1, array.IndexOf(1));
            Assert.Equal(2, array.IndexOf(4));
            Assert.Equal(3, array.IndexOf(5));
            Assert.Equal(4, array.IndexOf(8));
        }

        [Fact]
        public void InsertMethodShouldInsertElementOnLastPositionIfPreservesTheArraySorted()
        {
            SortedIntArray array = new SortedIntArray();
            array.Add(5);
            array.Add(1);
            array.Add(8);
            array.Add(4);
            array.Insert(3, 7);

            Assert.Equal(0, array.IndexOf(1));
            Assert.Equal(1, array.IndexOf(4));
            Assert.Equal(2, array.IndexOf(5));
            Assert.Equal(3, array.IndexOf(7));
            Assert.Equal(4, array.IndexOf(8));
        }

        [Fact]
        public void InsertMethodShouldNotInsertAMiddleElementIfIsNotInSortedOrder()
        {
            SortedIntArray array = new SortedIntArray();
            array.Add(5);
            array.Add(1);
            array.Add(8);
            array.Add(4);
            array.Insert(1, 7);

            Assert.Equal(0, array.IndexOf(1));
            Assert.Equal(1, array.IndexOf(4));
            Assert.Equal(2, array.IndexOf(5));
            Assert.Equal(3, array.IndexOf(8));
        }

        [Fact]
        public void InsertMethodShouldNotInsertAFirstElementIfIsNotInSortedOrder()
        {
            SortedIntArray array = new SortedIntArray();
            array.Add(5);
            array.Add(1);
            array.Add(8);
            array.Add(4);
            array.Insert(0, 2);

            Assert.Equal(0, array.IndexOf(1));
            Assert.Equal(1, array.IndexOf(4));
            Assert.Equal(2, array.IndexOf(5));
            Assert.Equal(3, array.IndexOf(8));
        }

        [Fact]
        public void InsertMethodShouldNotInsertALastElementIfIsNotInSortedOrder()
        {
            SortedIntArray array = new SortedIntArray();
            array.Add(5);
            array.Add(1);
            array.Add(8);
            array.Add(4);
            array.Insert(4, 7);

            Assert.Equal(0, array.IndexOf(1));
            Assert.Equal(1, array.IndexOf(4));
            Assert.Equal(2, array.IndexOf(5));
            Assert.Equal(3, array.IndexOf(8));
        }

        [Fact]
        public void ClearMethodShouldClearTheArray()
        {
            SortedIntArray array = new SortedIntArray();
            array.Add(5);
            array.Add(1);
            array.Add(8);
            array.Add(4);
            array.Clear();

            Assert.Equal(0, array.Count);
        }
    }
}