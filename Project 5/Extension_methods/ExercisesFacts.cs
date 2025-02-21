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
            List<Product> outer =
            [
               new Product() { Name = "Jacket", Quantity = 5 },
                new Product() { Name = "Coat", Quantity = 10 },
                new Product() { Name = "Shirt", Quantity = 3 },
                new Product() { Name = "Pants", Quantity = 5 },
            ];

            List<Product> inner =
            [
                new Product() { Name = "Jacket", Quantity = 20 },
                new Product() { Name = "Coat", Quantity = 15 },
                new Product() { Name = "Shirt", Quantity = 10 },
                new Product() { Name = "Pants", Quantity = 5 }
            ];

            List<Product> expected =
            [
                new Product() { Name = "Jacket", Quantity = 25 },
                new Product() { Name = "Coat", Quantity = 25 },
                new Product() { Name = "Shirt", Quantity = 13 },
                new Product() { Name = "Pants", Quantity = 10 },
            ];

            List<Product> result = Exercises.SumProductQuantity(outer, inner);

            int count = 0;
            for (var i = 0; i < result.Count; i++)
            {
                Assert.Equal(expected[i].Name, result[i].Name);
                Assert.Equal(expected[i].Quantity, result[i].Quantity);
                count++;
            }

            Assert.Equal(4, count);
        }
    }
}
