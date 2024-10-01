using System;
using System.Collections;
using System.Linq;

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
        public void AddMethodShouldAddMoreElements()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("first name", "Ana");
            dictionary.Add("last name", "Popescu");

            Assert.Equal("Ana", dictionary["first name"]);
            Assert.Equal("Popescu", dictionary["last name"]);
            Assert.Equal(2, dictionary.Count);
        }

        [Fact]
        public void AddMethodShouldAddAnElementWhenHasSameHashCode()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(2, "Ana");
            dictionary.Add(7, "Elena");

            Assert.Equal("Ana", dictionary[2]);
            Assert.Equal("Elena", dictionary[7]);
            Assert.Equal(2, dictionary.Count);
        }

        [Fact]
        public void AddItemMethodShouldAddKeyValuePair()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(new KeyValuePair<string, string>("name", "Ana"));

            Assert.Equal("Ana", dictionary["name"]);
            Assert.Equal(1, dictionary.Count);
        }

        [Fact]
        public void GetterShouldGetAnElementByKey()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                { "first name", "Ana" },
                { "last name", "Popescu" }
            };

            Assert.Equal("Popescu", dictionary["last name"]);
            Assert.Equal("Ana", dictionary["first name"]);
        }

        [Fact]
        public void GetterShouldGetAnElementByKeyWhenElementsAreInTheSameBucket()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
                { 2, "Ana" },
                { 7, "Popescu" }
            };

            Assert.Equal("Popescu", dictionary[7]);
            Assert.Equal("Ana", dictionary[2]);
        }

        [Fact]
        public void SetterShouldSetAnElementByKey()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                { "first name", "Ana" },
                { "last name", "Popescu" }
            };
            dictionary["last name"] = "Georgescu";

            Assert.Equal("Georgescu", dictionary["last name"]);
            Assert.Equal("Ana", dictionary["first name"]);
        }

        [Fact]
        public void SetterShouldSetAnElementByKeyWhenTheElementsAreInTheSameBucket()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
                { 2, "Ana" },
                { 7, "Popescu" }
            };
            dictionary[7] = "Georgescu";

            Assert.Equal("Georgescu", dictionary[7]);
            Assert.Equal("Ana", dictionary[2]);
        }

        [Fact]
        public void GetterShouldThrowInvalidOperationExceptionWhenKeyDoNotExists()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                { "first name", "Ana" },
                { "last name", "Popescu" }
            };

            Assert.Throws<InvalidOperationException>(() => dictionary["name"]);
        }

        [Fact]
        public void SetterShouldThrowInvalidOperationExceptionWhenKeyDoNotExists()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                { "first name", "Ana" },
                { "last name", "Popescu" }
            };

            Assert.Throws<InvalidOperationException>(() => dictionary["name"] = "Elena");
        }

        [Fact]
        public void RemoveShouldRemoveElementByKeyAndReturnTrue()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                { "first name", "Ana" },
                { "last name", "Popescu" }
            };

            Assert.True(dictionary.Remove("first name"));
            Assert.Equal("Popescu", dictionary["last name"]);
            Assert.Equal(1, dictionary.Count);
        }

        [Fact]
        public void RemoveShouldRemoveElementByKeyAndReturnTrueWhenElementsAreInTheSameBucket()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" }
            };

            Assert.True(dictionary.Remove(2));
            Assert.Equal("Andrei", dictionary[7]);
            Assert.Equal(2, dictionary.Count);
        }

        [Fact]
        public void RemoveShouldRemoveElementByKeyAndReturnTrueWhenElementsAreInTheSameBucketAndExistsNextElement()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" },
               { 22, "Maria" }
            };

            Assert.True(dictionary.Remove(22));
            Assert.True(dictionary.Remove(7));
            Assert.True(dictionary.Remove(17));
            Assert.Equal("Ana", dictionary[2]);
            Assert.Equal("Ion", dictionary[12]);
            Assert.Equal(2, dictionary.Count);
        }

        [Fact]
        public void RemoveShouldReturnFalseWhenKeyIsNotFound()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            Assert.False(dictionary.Remove(1));
            Assert.Equal(4, dictionary.Count);
        }

        [Fact]
        public void AddShouldAddElementsInEmptySpaces()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            dictionary.Remove(7);
            dictionary.Remove(17);
            dictionary.Add(22, "Mihaela");

            Assert.Equal("Ana", dictionary[2]);
            Assert.Equal("Mihaela", dictionary[22]);
            Assert.Equal("Ion", dictionary[12]);
            Assert.Equal(3, dictionary.Count);
        }

        [Fact]
        public void ClearShouldRemoveAllElements()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            dictionary.Clear();

            Assert.Equal(0, dictionary.Count);
        }

        [Fact]
        public void KeysPropertyShouldReturnCollectionOfAllKeys()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            ICollection<int> collection = dictionary.Keys;
            int[] array = { 2, 7, 12, 17 };
            foreach (int key in collection)
            {
                Assert.Contains(key, array);
            }
        }

        [Fact]
        public void ValuesPropertyShouldReturnCollectionOfAllValues()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            ICollection<string> collection = dictionary.Values;
            string[] array = { "Ana", "Andrei", "Ion", "Mircea" };
            foreach (string key in collection)
            {
                Assert.Contains(key, array);
            }
        }

        [Fact]
        public void ContainKeyShouldReturnTrueWhenContainsTheKey()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            Assert.True(dictionary.ContainsKey(12));
        }

        [Fact]
        public void ContainKeyShouldReturnFalseWhenDoNotContainsTheKey()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            Assert.False(dictionary.ContainsKey(1));
        }

        [Fact]
        public void TryGetValueShouldReturnTrueWhenContainsTheKeyAndItsValue()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            string value;

            Assert.True(dictionary.TryGetvalue(12, out value));
            Assert.Equal("Ion", value);
        }

        [Fact]
        public void TryGetValueShouldReturnFalseWhenDoNotContainsTheKeyAndDefaultValue()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            string value;

            Assert.False(dictionary.TryGetvalue(10, out value));
            Assert.Null(value);
        }

        [Fact]
        public void ContainShouldReturnTrueWhenContainsBothKeyAndValue()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            Assert.True(dictionary.Contains(new KeyValuePair<int, string>(7, "Andrei")));
        }

        [Fact]
        public void CopyToShouldCopyKeyValuePairsInArrayWhenIndexStartsAt0()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            KeyValuePair<int, string>[] array = new KeyValuePair<int, string>[dictionary.Count];
            dictionary.CopyTo(array, 0);

            int index = 0;
            foreach (KeyValuePair<int, string> pair in dictionary)
            {
                Assert.Equal(pair.Key, array[index].Key);
                Assert.Equal(pair.Value, array[index].Value);
                index++;
            }
        }

        [Fact]
        public void CopyToShouldCopyKeyValuePairsInArrayWhenIndexIsGreaterThan0()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            KeyValuePair<int, string>[] array = new KeyValuePair<int, string>[10];
            dictionary.CopyTo(array, 5);

            int index = 5;
            foreach (KeyValuePair<int, string> pair in dictionary)
            {
                Assert.Equal(pair.Key, array[index].Key);
                Assert.Equal(pair.Value, array[index].Value);
                index++;
            }
        }

        //[Fact]
        //public void CopyToShouldCopyKeyValuePairsInArrayAndExcludeRemovedElements()
        //{
        //    Dictionary<int, string> dictionary = new Dictionary<int, string>()
        //    {
        //        { 2, "Ana" },
        //        { 7, "Andrei" },
        //        { 12, "Ion" },
        //        { 17, "Mircea" }
        //    };

        //    dictionary.Remove(2);

        //    KeyValuePair<int, string>[] array = new KeyValuePair<int, string>[dictionary.Count];
        //    dictionary.CopyTo(array, 0);

        //    int index = 5;
        //    foreach (KeyValuePair<int, string> pair in dictionary)
        //    {
        //        Assert.Equal(pair.Key, array[index].Key);
        //        Assert.Equal(pair.Value, array[index].Value);
        //        index++;
        //    }

        //    Assert.Equal(3, array.Length);
        //}
    }
}
