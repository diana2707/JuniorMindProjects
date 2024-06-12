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
    }
}