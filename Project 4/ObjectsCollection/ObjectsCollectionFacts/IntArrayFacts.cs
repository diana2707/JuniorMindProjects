namespace ObjectsCollection
{
    public class IntArrayFacts
    {
        [Fact]
        public void AddMethodShouldAddElementAtTheEndOfArray()
        {
            IntArray array = new IntArray();
            array.Add(1);
        }
    }
}