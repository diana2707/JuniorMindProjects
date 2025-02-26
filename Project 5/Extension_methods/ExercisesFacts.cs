using System;

namespace Extension_methods
{
    public class ExercisesFacts
    {
        [Fact]
        public void CountVowelsReturnsNumberOfVowelsInString()
        {
            string source = " m34AeicvoNb;";
            Assert.Equal(4, source.CountVowels());
        }

        [Fact]
        public void CountVowelThrowsArgumentNullExceptionWhenSourceIsNull()
        {
            string source = null;
            Assert.Throws<ArgumentNullException>(() => source.CountVowels());
        }

        [Fact]
        public void CountVowelReturns0WhenSourceIsEmpty()
        {
            string source = string.Empty;
            Assert.Equal(0, source.CountVowels());
        }

        [Fact]
        public void CountVowelReturns0WhenStringHasNoVowels()
        {
            string source = " mmmmmmmmm,";
            Assert.Equal(0, source.CountVowels());
        }

        [Fact]
        public void CountConsonantsReturnsNumberOfVowelsInString()
        {
            string source = " m34AeicvoNb;";
            Assert.Equal(5, source.CountConsonants());
        }

        [Fact]
        public void CountConsonantsReturns0WhenStringHasNoConsonants()
        {
            string source = " aaaaaaa;";
            Assert.Equal(0, source.CountConsonants());
        }

        [Fact]
        public void CountConsonantsReturns0WhenStringIsEmpty()
        {
            string source = string.Empty;
            Assert.Equal(0, source.CountConsonants());
        }

        [Fact]
        public void CountConsonantsThrowArgumentNullExceptionWhenSourceIsNull()
        {
            string source = null;
            Assert.Throws<ArgumentNullException>(() => source.CountConsonants());
        }

        [Fact]
        public void FirstUniqueReturnsFirstUniqueCharInString()
        {
            string source = "ababacd";
            Assert.Equal('c', source.FirstUnique());
        }

        [Fact]
        public void FirstUniqueThrowsArgumentExceptionWhenNoUniqueElementExists()
        {
            string source = "ababa";
            Assert.Throws<ArgumentException>(() => source.FirstUnique());
        }

        [Fact]
        public void FirstUniqueThrowsArgumentExceptionWhenNSourceIsEmpty()
        {
            string source = string.Empty;
            Assert.Throws<ArgumentException>(() => source.FirstUnique());
        }

        [Fact]
        public void FirstUniqueThrowsArgumentNullExceptionWhenSourceIsNull()
        {
            string source = null;
            Assert.Throws<ArgumentNullException>(() => source.FirstUnique());
        }

        [Fact]
        public void ConvertToIntReturnsIntFromStringForUnits()
        {
            string source = "5";
            Assert.Equal(5, source.ConvertToInt());
        }

        [Fact]
        public void ConvertToIntReturnsIntFromStringFor2DigitNumbers()
        {
            string source = "52";
            Assert.Equal(52, source.ConvertToInt());
        }

        [Fact]
        public void ConvertToIntReturnsIntFromStringFor3DigitNumbers()
        {
            string source = "521";
            Assert.Equal(521, source.ConvertToInt());
        }

        [Fact]
        public void ConvertToIntReturnsNegativeIntFromString()
        {
            string source = "-5";
            Assert.Equal(-5, source.ConvertToInt());
        }

        [Fact]
        public void ConvertToIntReturnsIntFromStringWhenStringStartsWithPlusSign()
        {
            string source = "+5";
            Assert.Equal(5, source.ConvertToInt());
        }

        [Fact]
        public void ConvertToIntThrowsArgumentExeptionIfSourceContainsInvalidCharacters()
        {
            string source = "+5a";
            Assert.Throws<ArgumentException>(() => source.ConvertToInt());
        }

        [Fact]
        public void ConvertToIntThrowsArgumentExeptionIfSourceContainsMultipleOptionalSigns()
        {
            string source = "--5";
            Assert.Throws<ArgumentException>(() => source.ConvertToInt());
        }

        [Fact]
        public void ConvertToIntThrowsArgumentExeptionIfSourceIsEmpty()
        {
            string source = string.Empty;
            Assert.Throws<ArgumentException>(() => source.ConvertToInt());
        }

        [Fact]
        public void ConvertToIntThrowsArgumentNullExeptionIfSourceIsNull()
        {
            string source = null;
            Assert.Throws<ArgumentNullException>(() => source.ConvertToInt());
        }

        [Fact]
        public void MostFrequentCharReturnsTheCharThatAppearsTheMostFrequent()
        {
            string source = "cababad";
            Assert.Equal('a', source.MostFrequentChar());
        }

        [Fact]
        public void MostFrequentCharReturnsFirstMostFrequentCharWhenMultipleCharsAreMostFrequent()
        {
            string source = "cdbaba";
            Assert.Equal('b', source.MostFrequentChar());
        }

        [Fact]
        public void MostFrequentCharThrowsArgumentNullExceptionWhenSourceIsNull()
        {
            string source = null;
            Assert.Throws<ArgumentNullException>(() => source.MostFrequentChar());
        }

        [Fact]
        public void MostFrequentCharThrowsArgumentExceptionWhenSourceIsEmpty()
        {
            string source = string.Empty;
            Assert.Throws<ArgumentException>(() => source.MostFrequentChar());
        }

        [Fact]
        public void GetAllPalidromsReturnsAllPartitionOfStringThatArePalindroms()
        {
            string source = "aabaac";
            IEnumerable<string> expected = ["a", "a", "b", "a", "a", "c", "aa", "aa", "aba", "aabaa"];

            var result = source.GetAllPalindroms();
            int count = 0;

            foreach (var item in result)
            {
                Assert.Contains(item, result);
                count++;
            }

            Assert.Equal(10, count);
        }

        [Fact]
        public void GetAllPalidromsThrowsArgumentNullExceptionIfSourceIsNull()
        {
            string source = null;

            Assert.Throws<ArgumentNullException>(() => source.GetAllPalindroms());
        }

        [Fact]
        public void GetAllPalidromsReturnsEmptyCollectionWhenSourceIsEmpty()
        {
            string source = string.Empty;
            IEnumerable<string> expected = [];

            var result = source.GetAllPalindroms();
            int count = 0;

            foreach (var item in result)
            {
                Assert.Contains(item, result);
                count++;
            }
        }

        [Fact]
        public void GetSubarraysReturnsAllSubarraysOfArrayWithElementsThatAddToLessOrEqualWithArgument()
        {
            List<int> source = [ 1, 2, 3, 4, 5 ];

            List<List<int>> expected = [[1], [1, 2], [1, 2, 3], [1, 2, 3, 4],
                                        [2], [2, 3], [2, 3, 4],
                                        [3], [3, 4],
                                        [4], [4, 5],
                                        [5]];

            var result = source.GetSubarrays(10);

            for (int i = 0; i < result.Count; i++)
            {
                Assert.True(result[i].SequenceEqual(expected[i]));
            }
        }

        [Fact]
        public void GetSubarraysReturnsEmptyCollectionWhenNoSubarraysAreLessOrEqualWithArgument()
        {
            List<int> source = [10, 20, 30, 40, 50];

            var result = source.GetSubarrays(1);

            Assert.Empty(result);
        }

        [Fact]
        public void GetSubarraysThrowsArgumentNullExceptionWhenSourceIsNull()
        {
            List<int> source = null;

            Assert.Throws<ArgumentNullException>(() => source.GetSubarrays(5));
        }

        [Fact]
        public void GetSubarraysReturnsEmptyCollectionWhenCollectionIsEmpty()
        {
            List<int> source = [];

            List<List<int>> result = source.GetSubarrays(5);

            Assert.Empty(source);
        }

        [Fact]
        public void GetSignCombinationsReturnsAllSignCombinations()
        {
            List<string> expected = ["-1-2-3", "-1-2+3", "-1+2-3", "-1+2+3", "+1-2-3", "+1-2+3", "+1+2-3", "+1+2+3"];

            var result = Exercises.GetSignCombinations(3);

            int count = 0;
            foreach (string s in result)
            {
                Assert.Contains(s, expected);
                count++;
            }

            Assert.Equal(8, count);
        }

        [Fact]
        public void GetSignCombinationsReturnsEmptyCollectionWhenArgumentIs0()
        {
            var result = Exercises.GetSignCombinations(0);

            Assert.Empty(result);
        }

        [Fact]
        public void GetPythagoreanTripletsReturnsAllTripletsFromIntegerArrayThatArePitagoreanTriplets()
        {
            List<int> source = [ 3, 4, 5, 6, 8, 10, 9, 12, 15, 16, 20, 25 ];

            List<List<int>> expected = [[3, 4, 5], [6, 8, 10], [9, 12, 15], [12, 16, 20], [15, 20, 25]];

            List<List<int>> result = Exercises.GetPythagoreanTriplets(source);

            for (int i = 0; i < result.Count; i++)
            {
                Assert.True(result[i].SequenceEqual(expected[i]));
            }
        }

        [Fact]
        public void GetPythagoreanTripletsThrowsArgumentNullExceptionWhenSourceIsNull()
        {
            List<int> source = null;

            Assert.Throws<ArgumentNullException>(() => Exercises.GetPythagoreanTriplets(source));
        }

        [Fact]
        public void GetPythagoreanTripletsReturnsEmptyCollectionWhenSourceIsEmpty()
        {
            List<int> source = [];

            List<List<int>> result = Exercises.GetPythagoreanTriplets(source);

            Assert.Empty(result);
        }

        [Fact]
        public void FilterForAnyFeaturesReturnAllProductsWithAtListOneFeatureFromAListOfFeatures()
        {
            var jacket = new Product()
            {
                Name = "Jacket",
                Quantity = 20,
                Features = new List<Feature>
                    {
                        new Feature() { Id = 1 },
                        new Feature() { Id = 2 },
                        new Feature() { Id = 3 },
                    },
            };

            var coat = new Product()
            {
                Name = "Coat",
                Quantity = 15,
                Features = new List<Feature>
                    {
                        new Feature() { Id = 1 },
                        new Feature() { Id = 4 },
                        new Feature() { Id = 5 },
                    },
            };

            var shirt = new Product()
            {
                Name = "Shirt",
                Quantity = 25,
                Features = new List<Feature>
                {
                    new Feature() { Id = 2 },
                    new Feature() { Id = 3 },
                    new Feature() { Id = 4 },
                },
            };

            var pants = new Product()
            {
                Name = "Pants",
                Quantity = 25,
                Features = new List<Feature>
                {
                    new Feature() { Id = 5 },
                    new Feature() { Id = 6 },
                    new Feature() { Id = 7 },
                },
            };

            List<Product> products = new List<Product>() { jacket, coat, shirt, pants };

            List<Feature> features = new List<Feature>()
            {
                new Feature() { Id = 1 },
                new Feature() { Id = 5 },
            };

            List<Product> filteredProducts = Exercises.FilterByAnyFeatures(products, features);

            var expected = new List<Product> { jacket, coat, pants };

            int count = 0;
            for (var i = 0; i < filteredProducts.Count; i++)
            {
                Assert.Equal(expected[i], filteredProducts[i]);
                count++;
            }

            Assert.Equal(3, count);
        }

        [Fact]
        public void FilterForAllFeaturesReturnsProductsThatHaveAllFeaturesFromAListOfFeatures()
        {
            var jacket = new Product()
            {
                Name = "Jacket",
                Quantity = 20,
                Features = new List<Feature>
                    {
                        new Feature() { Id = 1 },
                        new Feature() { Id = 2 },
                        new Feature() { Id = 3 },
                    },
            };

            var coat = new Product()
            {
                Name = "Coat",
                Quantity = 15,
                Features = new List<Feature>
                    {
                        new Feature() { Id = 1 },
                        new Feature() { Id = 4 },
                        new Feature() { Id = 5 },
                    },
            };

            var shirt = new Product()
            {
                Name = "Shirt",
                Quantity = 25,
                Features = new List<Feature>
                {
                    new Feature() { Id = 2 },
                    new Feature() { Id = 3 },
                    new Feature() { Id = 4 },
                },
            };

            var pants = new Product()
            {
                Name = "Pants",
                Quantity = 25,
                Features = new List<Feature>
                {
                    new Feature() { Id = 5 },
                    new Feature() { Id = 6 },
                    new Feature() { Id = 7 },
                },
            };

            List<Product> products = new List<Product>() { jacket, coat, shirt, pants };

            List<Feature> features = new List<Feature>()
            {
                new Feature() { Id = 1 },
                new Feature() { Id = 5 },
            };

            List<Product> filteredProducts = Exercises.FilterByAllFeatures(products, features);

            var expected = new List<Product> { coat };

            int count = 0;
            for (var i = 0; i < filteredProducts.Count; i++)
            {
                Assert.Equal(expected[i], filteredProducts[i]);
                count++;
            }

            Assert.Equal(1, count);
        }

        [Fact]
        public void FilterForExceptFeaturesReturnsProductsThatHaveAllFeaturesFromAListOfFeatures()
        {
            var jacket = new Product()
            {
                Name = "Jacket",
                Quantity = 20,
                Features = new List<Feature>
                    {
                        new Feature() { Id = 1 },
                        new Feature() { Id = 2 },
                        new Feature() { Id = 3 },
                    },
            };

            var coat = new Product()
            {
                Name = "Coat",
                Quantity = 15,
                Features = new List<Feature>
                    {
                        new Feature() { Id = 1 },
                        new Feature() { Id = 4 },
                        new Feature() { Id = 5 },
                    },
            };

            var shirt = new Product()
            {
                Name = "Shirt",
                Quantity = 25,
                Features = new List<Feature>
                {
                    new Feature() { Id = 2 },
                    new Feature() { Id = 3 },
                    new Feature() { Id = 4 },
                },
            };

            var pants = new Product()
            {
                Name = "Pants",
                Quantity = 25,
                Features = new List<Feature>
                {
                    new Feature() { Id = 5 },
                    new Feature() { Id = 6 },
                    new Feature() { Id = 7 },
                },
            };

            List<Product> products = new List<Product>() { shirt };

            List<Feature> features = new List<Feature>()
            {
                new Feature() { Id = 1 },
                new Feature() { Id = 7 },
            };

            List<Product> filteredProducts = Exercises.FilterByExcludedFeatures(products, features);

            var expected = new List<Product> { shirt };

            int count = 0;
            for (var i = 0; i < filteredProducts.Count; i++)
            {
                Assert.Equal(expected[i], filteredProducts[i]);
                count++;
            }

            Assert.Equal(1, count);
        }

        [Fact]
        public void SumProductQuantitysReturnOneCollectionFrom2CollectionsAndQuantityReflectTotalAmount()
        {
            List<Product> first =
            [
               new Product() { Name = "Jacket", Quantity = 5 },
                new Product() { Name = "Coat", Quantity = 10 },
                new Product() { Name = "Shirt", Quantity = 3 },
                new Product() { Name = "Pants", Quantity = 5 },
                new Product() { Name = "Shirt", Quantity = 15 },
            ];

            List<Product> second =
            [
                new Product() { Name = "Jacket", Quantity = 20 },
                new Product() { Name = "Coat", Quantity = 15 },
                new Product() { Name = "Shirt", Quantity = 10 },
                new Product() { Name = "Pants", Quantity = 5 },
                new Product() { Name = "Coat", Quantity = 4 },
            ];

            List<Product> expected =
            [
                new Product() { Name = "Jacket", Quantity = 25 },
                new Product() { Name = "Coat", Quantity = 29 },
                new Product() { Name = "Shirt", Quantity = 28 },
                new Product() { Name = "Pants", Quantity = 10 },
            ];

            List<Product> result = Exercises.SumProductQuantity(first, second);

            int count = 0;
            for (var i = 0; i < result.Count; i++)
            {
                Assert.Equal(expected[i].Name, result[i].Name);
                Assert.Equal(expected[i].Quantity, result[i].Quantity);
                count++;
            }

            Assert.Equal(4, count);
        }

        [Fact]
        public void TestResultsReturnTheResultWithLargestScoreForEachFamily()
        {
            var results = new List<TestResults>()
            {
                new TestResults() { Id = "01", FamilyId = "A", Score = 10 },
                new TestResults() { Id = "01", FamilyId = "B", Score = 70 },
                new TestResults() { Id = "02", FamilyId = "A", Score = 80 },
                new TestResults() { Id = "02", FamilyId = "B", Score = 50 },
                new TestResults() { Id = "01", FamilyId = "B", Score = 70 },
            };

            var expected = new List<TestResults>()
            {
                new TestResults() { Id = "02", FamilyId = "A", Score = 80 },
                new TestResults() { Id = "01", FamilyId = "B", Score = 70 },
            };

            List<TestResults> maxResults = Exercises.GetMaxResultsByFamily(results);

            for (int i = 0; i < maxResults.Count; i++)
            {
                Assert.Equal(expected[i].Id, maxResults[i].Id);
                Assert.Equal(expected[i].FamilyId, maxResults[i].FamilyId);
                Assert.Equal(expected[i].Score, maxResults[i].Score);
            }
        }

        [Fact]
        public void MostFrequentWordsReturnsTheTop5MostFrequentWordsInAText()
        {
            //string input = "Aaa aaa mmm gg aaa gg gg tt gg mmm gg mmm mmm aaa ss ss.";

            //List<string> expected = ["gg", "mmm", "aaa",  "ss", "tt"];

            string input = "The surface salinity of the Marmara averages about 22 " +
                "parts per thousand, which is slightly more than that of the Black Sea," +
                " but only about two-thirds that of most oceans. The water is much more" +
                " saline at the bottom of the sea, averaging a salinity of around 38" +
                " parts per thousand, similar to that of the Mediterranean Sea. This " +
                "high-density saline water does not migrate to the surface as is also " +
                "the case with the Black Sea water. Water from the Susurluk, Biga Granicus," +
                " and Gönen Rivers also reduces the salinity of the sea, though with less " +
                "effect than that on the Black Sea. With little land in Thrace draining southward, " +
                "almost all of these rivers flow from Anatolia.";

            List<string> expected = ["the", "of", "sea", "that", "water" ];

            List<string> result = Exercises.MostFrequentWords(input);

            for (int i = 0; i < result.Count; i++)
            {
                Assert.Equal(expected[i], result[i]);
            }
        }

        [Fact]
        public void IsValidSudokuReturnsTrueWhenTheSudokuIsValid()
        {
            List<int> input = [5, 3, 4, 6, 7, 8, 9, 1, 2,
                               6, 7, 2, 1, 9, 5, 3, 4, 8,
                               1, 9, 8, 3, 4, 2, 5, 6, 7,
                               8, 5, 9, 7, 6, 1, 4, 2, 3,
                               4, 2, 6, 8, 5, 3, 7, 9, 1,
                               7, 1, 3, 9, 2, 4, 8, 5, 6,
                               9, 6, 1, 5, 3, 7, 2, 8, 4,
                               2, 8, 7, 4, 1, 9, 6, 3, 5,
                               3, 4, 5, 2, 8, 6, 1, 7, 9];

            Assert.True(Exercises.IsValidSudoku(input));
        }

        [Fact]
        public void IsValidSudokuReturnsFalseWhenTheSudokuIsInvalid()
        {
            List<int> input = [5, 3, 4, 6, 7, 8, 9, 1, 2,
                               6, 7, 2, 1, 9, 5, 3, 4, 8,
                               1, 9, 8, 3, 4, 2, 5, 6, 7,
                               8, 5, 9, 7, 6, 1, 4, 2, 3,
                               4, 2, 6, 8, 5, 3, 7, 9, 1,
                               7, 1, 3, 9, 2, 4, 8, 5, 6,
                               9, 6, 1, 5, 3, 7, 2, 8, 4,
                               2, 8, 7, 4, 1, 9, 6, 3, 5,
                               3, 4, 5, 2, 8, 6, 1, 7, 3];

            Assert.False(Exercises.IsValidSudoku(input));
        }

        [Fact]
        public void IsValidSudokuThrowsArgumentExceptionWhenElementIsNot1To9()
        {
            List<int> input = [5, 3, 4, 6, 7, 8, 9, 1, 2,
                               6, 7, 2, 1, 9, 5, 3, 4, 8,
                               1, 9, 8, 3, 4, 2, 5, 6, 7,
                               8, 5, 9, 7, 6, 1, 4, 2, 3,
                               4, 2, 6, 8, 5, 3, 7, 9, 1,
                               7, 1, 3, 9, 2, 4, 8, 5, 6,
                               9, 6, 1, 5, 3, 7, 2, 8, 4,
                               2, 8, 7, 4, 1, 9, 6, 3, 5,
                               3, 4, 5, 2, 8, 6, 1, 7, 19];

            Assert.Throws<ArgumentException>(() => Exercises.IsValidSudoku(input));
        }

        [Fact]
        public void CalculateRPNReturnsCorrectResult()
        {
            string input = "3 4 + 5 *";

            Assert.Equal(35, Exercises.CalculateRPN(input));
        }

        [Fact]
        public void CalculateRPNReturnsCorrectResultForMultipleDigitNumber()
        {
            string input = "3 40 + 5 *";

            Assert.Equal(215, Exercises.CalculateRPN(input));
        }

        [Fact]
        public void CalculateRPNThrowsArgumentExeptionWhenArgumentContainsInvalidElement()
        {
            string input = "3 4 + a 5 *";

            Assert.Throws<ArgumentException>(() => Exercises.CalculateRPN(input));
        }

        [Fact]
        public void CalculateRPNThrowsArgumentExeptionWhenArgumentContainsElementWithInvalidCharacter()
        {
            string input = "3 4 + a5 *";

            Assert.Throws<ArgumentException>(() => Exercises.CalculateRPN(input));
        }

        [Fact]
        public void CalculateRPNThrowsArgumentExeptionWhenArgumentIsNull()
        {
            string input = null;

            Assert.Throws<ArgumentNullException>(() => Exercises.CalculateRPN(input));
        }
    }
}
