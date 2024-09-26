namespace ObjectsCollection
{
    public class ElementFacts
    {
        [Fact]
        public void ShouldReturnIsTypeElement()
        {
            Element<int, int> element = new Element<int, int>(1, 1);
            Assert.IsType<Element<int, int>>(element);
        }

        [Fact]
        public void ShouldSetKeyAndValue()
        {
            Element<int, int> element = new Element<int, int>(1, 1);
            Assert.Equal(1, element.Key);
            Assert.Equal(1, element.Value);
        }
    }
}
