using System;

namespace ObjectsCollection
{
    public class DictionaryFacts
    {
        [Fact]
        public void ConstructorShouldReturnTypeDictionary()
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            Assert.IsType<Dictionary<int, int>>(dictionary);
        }

        [Fact]
        public void CountShouldReturn0WhenDictionaryIsEmpty()
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            Assert.Equal(0, dictionary.Count);
        }

        [Fact]
        public void IsReadOnlyShouldReturnFalse()
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            Assert.False(dictionary.IsReadOnly);
        }

        [Fact]
        public void AddMethodShouldAddAnElement()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("name", "Ana");
            Assert.Equal(1, dictionary.Count);
        }
    }
}
