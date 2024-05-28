namespace ObjectsCollection
{
    public class IntArrayFacts
    {
        [Fact]
        public void AddMethodShouldAddOneElement()
        {
            IntArray array = new IntArray();
            array.Add(5);
            Assert.Equal(2, array.Count());
        }

        [Fact]
        public void AddMethodShouldAddMultipleElements()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(6);
            Assert.Equal(3, array.Count());
        }
    }
}