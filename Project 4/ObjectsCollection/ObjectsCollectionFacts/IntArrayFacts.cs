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
    }
}