using System;

namespace ObjectsCollection
{
    public class DictionaryFacts
    {
        [Fact]
        public void ShouldReturnTyoeDictionary()
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            Assert.IsType<Dictionary<int, int>>(dictionary);
        }
    }
}
