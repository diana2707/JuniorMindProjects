namespace ObjectsCollection
{
    public class DictionaryFacts
    {
        [Fact]
        public void ConstructorShouldReturnTypeDictionary()
        {
            CustomDictionary<int, int> dictionary = new CustomDictionary<int, int>();
            Assert.IsType<CustomDictionary<int, int>>(dictionary);
        }

        [Fact]
        public void CountShouldReturn0WhenDictionaryIsEmpty()
        {
            CustomDictionary<int, int> dictionary = new CustomDictionary<int, int>();
            Assert.Equal(0, dictionary.Count);
        }

        [Fact]
        public void IsReadOnlyShouldReturnFalse()
        {
            CustomDictionary<int, int> dictionary = new CustomDictionary<int, int>();
            Assert.False(dictionary.IsReadOnly);
        }

        [Fact]
        public void AddMethodShouldAddAnElement()
        {
            CustomDictionary<string, string> dictionary = new CustomDictionary<string, string>();
            dictionary.Add("name", "Ana");

            Assert.Equal("Ana", dictionary["name"]);
            Assert.Equal(1, dictionary.Count);
        }

        [Fact]
        public void AddMethodShouldAddMoreElements()
        {
            CustomDictionary<string, string> dictionary = new CustomDictionary<string, string>();
            dictionary.Add("first name", "Ana");
            dictionary.Add("last name", "Popescu");

            Assert.Equal("Ana", dictionary["first name"]);
            Assert.Equal("Popescu", dictionary["last name"]);
            Assert.Equal(2, dictionary.Count);
        }

        [Fact]
        public void AddMethodShouldAddAnElementWhenHasSameHashCode()
        {
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>();
            dictionary.Add(2, "Ana");
            dictionary.Add(7, "Elena");

            Assert.Equal("Ana", dictionary[2]);
            Assert.Equal("Elena", dictionary[7]);
            Assert.Equal(2, dictionary.Count);
        }

        [Fact]
        public void AddItemMethodShouldAddKeyValuePair()
        {
            CustomDictionary<string, string> dictionary = new CustomDictionary<string, string>();
            dictionary.Add(new KeyValuePair<string, string>("name", "Ana"));

            Assert.Equal("Ana", dictionary["name"]);
            Assert.Equal(1, dictionary.Count);
        }

        [Fact]
        public void GetterShouldGetAnElementByKey()
        {
            CustomDictionary<string, string> dictionary = new CustomDictionary<string, string>()
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
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
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
            CustomDictionary<string, string> dictionary = new CustomDictionary<string, string>()
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
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
            {
                { 2, "Ana" },
                { 7, "Popescu" }
            };
            dictionary[7] = "Georgescu";

            Assert.Equal("Georgescu", dictionary[7]);
            Assert.Equal("Ana", dictionary[2]);
        }

        [Fact]
        public void RemoveShouldRemoveElementByKeyAndReturnTrue()
        {
            CustomDictionary<string, string> dictionary = new CustomDictionary<string, string>()
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
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
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
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
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
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
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
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
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
        public void EnumeratorShouldReturnOnlyActiveElements()
        {
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            dictionary.Remove(7);
            dictionary.Remove(17);

            KeyValuePair<int, string>[] array =
            {
                new KeyValuePair<int, string>(2, "Ana"),
                new KeyValuePair<int, string>(12, "Ion")
            };

            foreach (var element in dictionary)
            {
                Assert.Contains(element, array);
            }
        }

        [Fact]
        public void ClearShouldRemoveAllElements()
        {
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            dictionary.Clear();

            Assert.Empty(dictionary);
        }

        [Fact]
        public void KeysPropertyShouldReturnCollectionOfAllKeys()
        {
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
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
        public void KeysPropertyShouldReturnCollectionOfAllKeysWithoutTheOnesFromRemovedElement()
        {
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            dictionary.Remove(2);
            dictionary.Remove(17);
            dictionary.Remove(7);

            ICollection<int> collection = dictionary.Keys;
            int[] array = { 12 };
            foreach (int key in collection)
            {
                Assert.Contains(key, array);
            }

            Assert.Equal(1, collection.Count);
        }

        [Fact]
        public void KeysPropertyShouldReturnCollectionOfAllKeysWhenAreDistributedInMoreBuckets()
        {
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
            {
               { 2, "Ana" },
               { 3, "Elene" },
               { 7, "Andrei" },
               { 8, "Dan" },
               { 12, "Ion" }
            };

            dictionary.Remove(3);
            dictionary.Remove(2);
            dictionary.Remove(12);

            ICollection<int> collection = dictionary.Keys;
            int[] array = { 7, 8 };
            foreach (int key in collection)
            {
                Assert.Contains(key, array);
            }

            Assert.Equal(2, collection.Count);
        }

        [Fact]
        public void ValuesPropertyShouldReturnCollectionOfAllValues()
        {
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
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
        public void ValuesPropertyShouldReturnCollectionOfAllValuesWithoutRemovedValues()
        {
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
            {
               { 2, "Ana" },
               { 3, "Elene" },
               { 7, "Andrei" },
               { 8, "Dan" },
               { 12, "Ion" }
            };

            dictionary.Remove(3);
            dictionary.Remove(2);
            dictionary.Remove(12);

            ICollection<string> collection = dictionary.Values;
            string[] array = { "Andrei", "Dan" };
            foreach (string value in collection)
            {
                Assert.Contains(value, array);
            }

            Assert.Equal(2, collection.Count);
        }

        [Fact]
        public void ContainKeyShouldReturnTrueWhenContainsTheKey()
        {
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
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
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
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
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            string value;

            Assert.True(dictionary.TryGetValue(12, out value));
            Assert.Equal("Ion", value);
        }

        [Fact]
        public void TryGetValueShouldReturnFalseWhenTheElementWasRemoved()
        {
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };
            dictionary.Remove(12);

            string value;

            Assert.False(dictionary.TryGetValue(12, out value));
            Assert.Null(value);
        }

        [Fact]
        public void TryGetValueShouldReturnFalseWhenDoNotContainsTheKeyAndDefaultValue()
        {
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            string value;

            Assert.False(dictionary.TryGetValue(10, out value));
            Assert.Null(value);
        }

        [Fact]
        public void ContainsShouldReturnTrueWhenContainsBothKeyAndValue()
        {
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
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
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            KeyValuePair<int, string>[] array = new KeyValuePair<int, string>[dictionary.Count];
            dictionary.CopyTo(array, 0);

            foreach (KeyValuePair<int, string> pair in dictionary)
            {
                Assert.Contains(pair, array);
            }
        }

        [Fact]
        public void CopyToShouldCopyKeyValuePairsInArrayWhenIndexIsGreaterThan0()
        {
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            KeyValuePair<int, string>[] array = new KeyValuePair<int, string>[10];
            dictionary.CopyTo(array, 5);

            foreach (KeyValuePair<int, string> pair in dictionary)
            {
                Assert.Contains(pair, array);
            }
        }

        [Fact]
        public void GetterShouldThrowInvalidOperationExceptionWhenUsedWithRemovedElement()
        {
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
            {
               { 2, "Ana" },
               { 7, "Andrei" },
               { 12, "Ion" },
               { 17, "Mircea" }
            };

            dictionary.Remove(12);

            Assert.Throws<KeyNotFoundException>(() => dictionary[12]);
        }

        [Fact]
        public void CopyToShouldCopyKeyValuePairsInArrayAndExcludeRemovedElements()
        {
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
            {
                { 2, "Ana" },
                { 7, "Andrei" },
                { 12, "Ion" },
                { 17, "Mircea" }
            };

            dictionary.Remove(2);
            dictionary.Remove(17);

            KeyValuePair<int, string>[] array = new KeyValuePair<int, string>[dictionary.Count];
            dictionary.CopyTo(array, 0);

            foreach (KeyValuePair<int, string> pair in dictionary)
            {
                Assert.Contains(pair, array);
            }

            Assert.Equal(2, array.Length);
        }

        [Fact]
        public void IndexerThrowsArgumentNullExceptionWhenKeyIsNull()
        {
            CustomDictionary<string, string> dictionary = new CustomDictionary<string, string>()
            {
                { "1", "Ana" },
                { "2", "Andrei" },
                { "3", "Ion" },
                { "4", "Mircea" }
            };

            Assert.Throws<ArgumentNullException>(() => dictionary[null]);
            Assert.Throws<ArgumentNullException>(() => dictionary[null] = "5");
        }

        [Fact]
        public void SetterAddsElementWhenKeyIsNotFound()
        {
            CustomDictionary<int, string> dictionary = new CustomDictionary<int, string>()
            {
                { 1, "Ana" },
                { 2, "Andrei" },
                { 3, "Ion" },
                { 4, "Mircea" }
            };

            dictionary[5] = "Laura";

            Assert.Equal("Laura", dictionary[5]);
            Assert.Equal(5, dictionary.Count);
        }

        [Fact]
        public void AddThrowsArgumentNullExceptionWhenKeyIsNull()
        {
            CustomDictionary<string, string> dictionary = new CustomDictionary<string, string>()
            {
                { "1", "Ana" },
                { "2", "Andrei" },
                { "3", "Ion" },
                { "4", "Mircea" }
            };

            Assert.Throws<ArgumentNullException>(() => dictionary.Add(null, "aaa"));
        }

        [Fact]
        public void AddThrowsArgumentExceptionWhenKeyIsAlreadyUsed()
        {
            CustomDictionary<string, string> dictionary = new CustomDictionary<string, string>()
            {
                { "1", "Ana" },
                { "2", "Andrei" },
                { "3", "Ion" },
                { "4", "Mircea" }
            };

            Assert.Throws<ArgumentException>(() => dictionary.Add("4", "Alina"));
        }

        [Fact]
        public void ContainsThrowsArgumentNullExceptionWhenKeyIsNull()
        {
            CustomDictionary<string, string> dictionary = new CustomDictionary<string, string>()
            {
                { "1", "Ana" },
                { "2", "Andrei" },
                { "3", "Ion" },
                { "4", "Mircea" }
            };

            Assert.Throws<ArgumentNullException>(() => dictionary.ContainsKey(null));
        }

        [Fact]
        public void CopyToThrowsArgumentNullExceptionWhenArrayIsNull()
        {
            CustomDictionary<string, string> dictionary = new CustomDictionary<string, string>()
            {
                { "1", "Ana" },
                { "2", "Andrei" },
                { "3", "Ion" },
                { "4", "Mircea" }
            };

            Assert.Throws<ArgumentNullException>(() => dictionary.CopyTo(null, 0));
        }

        [Fact]
        public void CopyToThrowsArgumentOutOfExceptionWhenArrayIndexIsLessThan0()
        {
            CustomDictionary<string, string> dictionary = new CustomDictionary<string, string>()
            {
                { "1", "Ana" },
                { "2", "Andrei" },
                { "3", "Ion" },
                { "4", "Mircea" }
            };

            KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[dictionary.Count];

            Assert.Throws<ArgumentOutOfRangeException>(() => dictionary.CopyTo(array, -1));
        }

        [Fact]
        public void CopyToThrowsArgumentExceptionWhenArrayLengthIsLessThanCount()
        {
            CustomDictionary<string, string> dictionary = new CustomDictionary<string, string>()
            {
                { "1", "Ana" },
                { "2", "Andrei" },
                { "3", "Ion" },
                { "4", "Mircea" }
            };

            KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[3];

            Assert.Throws<ArgumentException>(() => dictionary.CopyTo(array, 0));
        }

        [Fact]
        public void CopyToThrowsArgumentExceptionWhenArrayLengthFromIndexIsLessThanCount()
        {
            CustomDictionary<string, string> dictionary = new CustomDictionary<string, string>()
            {
                { "1", "Ana" },
                { "2", "Andrei" },
                { "3", "Ion" },
                { "4", "Mircea" }
            };

            KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[dictionary.Count];

            Assert.Throws<ArgumentException>(() => dictionary.CopyTo(array, 1));
        }

        [Fact]
        public void RemoveThrowsArgumentNullExceptionIfKeyIsNull()
        {
            CustomDictionary<string, string> dictionary = new CustomDictionary<string, string>()
            {
                { "1", "Ana" },
                { "2", "Andrei" },
                { "3", "Ion" },
                { "4", "Mircea" }
            };

            Assert.Throws<ArgumentNullException>(() => dictionary.Remove(null));
        }

        [Fact]
        public void TryGetValueThrowsArgumentNullExceptionIfKeyIsNull()
        {
            CustomDictionary<string, string> dictionary = new CustomDictionary<string, string>()
            {
                { "1", "Ana" },
                { "2", "Andrei" },
                { "3", "Ion" },
                { "4", "Mircea" }
            };

            Assert.Throws<ArgumentNullException>(() => dictionary.TryGetValue(null, out string value));
        }
    }
}
