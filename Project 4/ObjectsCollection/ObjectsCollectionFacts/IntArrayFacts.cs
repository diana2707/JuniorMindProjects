namespace ObjectsCollection
{
    public class IntArrayFacts
    {
        [Fact]
        public void AddMethodShouldAddOneElement()
        {
            IntArray array = new IntArray();
            array.Add(5);
            Assert.Equal(1, array.Count());
        }

        [Fact]
        public void CountMethodShouldReturnCorrectNumberOfElements()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(6);
            Assert.Equal(2, array.Count());
        }

        [Fact]
        public void ElementMethodShouldReturnCorrectElement()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(6);
            Assert.Equal(5, array.Element(0));
            Assert.Equal(6, array.Element(1));
        }

        [Fact]
        public void SetElementMethodShoulSetCorrectElement()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.SetElement(0, 6);
            Assert.Equal(6, array.Element(0));
        }

        [Fact]
        public void ContainsMethodShoulReturnTrueWhenArrayContainsElement()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(6);
            Assert.True(array.Contains(6));
        }

        [Fact]
        public void ContainsMethodShoulReturnTrueWhenArrayDoNotContainsElement()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(6);
            Assert.False(array.Contains(7));
        }

        [Fact]
        public void IndexOfMethodShoulReturnCorrectIndexOfElementWhenElementExists()
        {
            IntArray array = new IntArray();
            array.Add(3);
            array.Add(4);
            array.Add(5);
            Assert.Equal(1, array.IndexOf(4));
        }

        [Fact]
        public void IndexOfMethodShoulReturnMinus1WhenElementDoNotExists()
        {
            IntArray array = new IntArray();
            array.Add(3);
            array.Add(4);
            array.Add(5);
            Assert.Equal(-1, array.IndexOf(7));
        }

        [Fact]
        public void InsertMethodShoulShouldInsertCorrectElement()
        {
            IntArray array = new IntArray();
            array.Add(3);
            array.Add(5);
            array.Add(6);
            array.Insert(1, 4);
            Assert.Equal(4, array.Element(1));
            Assert.Equal(6, array.Element(3));
        }

        [Fact]
        public void CleanMethodShoulShouldSetAllElementToDefault()
        {
            IntArray array = new IntArray();
            array.Add(3);
            array.Add(4);
            array.Clear();
            Assert.Equal(0, array.Element(0));
            Assert.Equal(0, array.Element(1));
        }
    }
}