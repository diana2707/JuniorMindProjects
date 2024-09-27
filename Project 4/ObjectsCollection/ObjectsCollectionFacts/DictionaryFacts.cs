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

            Assert.Equal("Ana", dictionary["name"]);
            Assert.Equal(1, dictionary.Count);
        }

        [Fact]
        public void AddMethodShouldAddAnElementWhenHasSameHashCode()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(2, "Ana");
            dictionary.Add(7, "Elena");

            Assert.Equal("Ana", dictionary[2]);
            Assert.Equal("Elena", dictionary[7]);
            Assert.Equal(1, dictionary.Count);
        }

        [Fact]
        public void IndexerShouldGetAnElementByKey()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("first name", "Ana");
            dictionary.Add("last name", "Popescu");
            Assert.Equal("Popescu", dictionary["last name"]);
            Assert.Equal("Ana", dictionary["first name"]);
        }

        [Fact]
        public void IndexerShouldSetAnElementByKey()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("first name", "Ana");
            dictionary.Add("last name", "Popescu");
            dictionary["last name"] = "Georgescu";

            Assert.Equal("Georgescu", dictionary["last name"]);
            Assert.Equal("Ana", dictionary["first name"]);
        }

        [Fact]
        public void GetterShouldThrowInvalidOperationExceptionWhenKeyDoNotExists()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("first name", "Ana");
            dictionary.Add("last name", "Popescu");

            Assert.Throws<InvalidOperationException>(() => dictionary["name"]);
        }

        [Fact]
        public void SetterShouldThrowInvalidOperationExceptionWhenKeyDoNotExists()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("first name", "Ana");
            dictionary.Add("last name", "Popescu");

            Assert.Throws<InvalidOperationException>(() => dictionary["name"] = "Elena");
        }
    }
}
