namespace ObjectsCollection
{
    public class IntArrayFacts
    {
        [Fact]
        public void AddMethodShouldAddOneElement()
        {
            IntArray array = new IntArray();
            array.Add(5);

            Assert.Equal(5, array[0]);
        }

        [Fact]
        public void AddMethodShouldAddOneElementWhenArrayHasOver4Elements()
        {
            IntArray array = new IntArray();
            array.Add(1);
            array.Add(2);
            array.Add(3);
            array.Add(4);
            array.Add(5);

            Assert.Equal(5, array[4]);
        }

        [Fact]
        public void CountMethodShouldReturnCorrectNumberOfElements()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(6);

            Assert.Equal(2, array.Count);
        }

        [Fact]
        public void ArrayIndexerShouldReturnCorrectElement()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(6);

            Assert.Equal(5, array[0]);
            Assert.Equal(6, array[1]);
        }

        [Fact]
        public void ArrayIndexerShouldSetCorrectElement()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array[0] = 6;

            Assert.Equal(6, array[0]);
        }

        [Fact]
        public void ArrayIndexerShouldNotSetElementWhenIndexIsLargerThanCount()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array[0] = 6;
            array[1] = 7;

            Assert.Equal(6, array[0]);
            Assert.Equal(7, array[1]);
        }

        [Fact]
        public void ContainsMethodShouldReturnTrueWhenArrayContainsElement()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(6);

            Assert.True(array.Contains(6));
        }

        [Fact]
        public void ContainsMethodShouldReturnTrueWhenArrayDoNotContainsElement()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(6);

            Assert.False(array.Contains(7));
        }

        [Fact]
        public void IndexOfMethodShouldReturnCorrectIndexOfElementWhenElementExists()
        {
            IntArray array = new IntArray();
            array.Add(3);
            array.Add(4);
            array.Add(5);

            Assert.Equal(1, array.IndexOf(4));
        }

        [Fact]
        public void IndexOfMethodShouldReturnMinus1WhenElementDoNotExists()
        {
            IntArray array = new IntArray();
            array.Add(3);
            array.Add(4);
            array.Add(5);

            Assert.Equal(-1, array.IndexOf(7));
        }

        [Fact]
        public void InsertMethodShouldInsertCorrectElement()
        {
            IntArray array = new IntArray();
            array.Add(3);
            array.Add(5);
            array.Add(6);
            array.Insert(1, 4);

            Assert.Equal(4, array[1]);
            Assert.Equal(6, array[3]);
        }

        [Fact]
        public void InsertMethodShouldInsertCorrectElementOnLastPosition()
        {
            IntArray array = new IntArray();
            array.Add(3);
            array.Add(4);
            array.Add(6);
            array.Insert(2, 5);

            Assert.Equal(5, array[2]);
            Assert.Equal(6, array[3]);
        }

        [Fact]
        public void InsertMethodShouldInsertCorrectElementOnFirstPosition()
        {
            IntArray array = new IntArray();
            array.Add(3);
            array.Add(4);
            array.Add(6);
            array.Insert(0, 2);

            Assert.Equal(2, array[0]);
            Assert.Equal(3, array[1]);
        }

        [Fact]
        public void ClearMethodShouldSetAllElementToDefault()
        {
            IntArray array = new IntArray();
            array.Add(3);
            array.Add(4);
            array.Clear();

            Assert.Equal(0, array.Count);
        }

        [Fact]
        public void RemoveMethodShouldRemoveTheCorrectElement()
        {
            IntArray array = new IntArray();
            array.Add(3);
            array.Add(4);
            array.Add(5);
            array.Remove(4);

            Assert.Equal(3, array[0]);
            Assert.Equal(5, array[1]);
        }

        [Fact]
        public void RemoveMethodShouldRemoveTheCorrectFirstElement()
        {
            IntArray array = new IntArray();
            array.Add(3);
            array.Add(4);
            array.Add(5);
            array.Remove(3);

            Assert.Equal(4, array[0]);
            Assert.Equal(5, array[1]);
        }

        [Fact]
        public void RemoveMethodShouldNotChangeOriginalArrayWhenElementDoNotExists()
        {
            IntArray array = new IntArray();
            array.Add(3);
            array.Add(4);
            array.Add(5);
            array.Remove(7);

            Assert.Equal(3, array[0]);
            Assert.Equal(4, array[1]);
            Assert.Equal(5, array[2]);
        }

        [Fact]
        public void RemoveAtMethodShouldRemoveTheCorrectElement()
        {
            IntArray array = new IntArray();
            array.Add(3);
            array.Add(4);
            array.Add(5);
            array.RemoveAt(1);

            Assert.Equal(3, array[0]);
            Assert.Equal(5, array[1]);
        }

        [Fact]
        public void RemoveAtMethodShouldRemoveCorrectlyFirstElement()
        {
            IntArray array = new IntArray();
            array.Add(3);
            array.Add(4);
            array.Add(5);
            array.RemoveAt(0);

            Assert.Equal(4, array[0]);
            Assert.Equal(5, array[1]);
        }

        [Fact]
        public void RemoveAtMethodShouldNotChangeArrayWhenIndexDoesNotExist()
        {
            IntArray array = new IntArray();
            array.Add(3);
            array.Add(4);
            array.Add(5);
            array.RemoveAt(5);

            Assert.Equal(3, array[0]);
            Assert.Equal(4, array[1]);
            Assert.Equal(5, array[2]);
        }
    }
}