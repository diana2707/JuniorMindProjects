using System;

namespace Extension_methods
{
    public class StringExtensionsFacts
    {
        [Fact]
        public void CountVowelsReturnsNumberOfVowelsInString()
        {
            string source = "Alexandra";
            Assert.Equal(4, source.CountVowels());
        }

        [Fact]
        public void CountConsonantsReturnsNumberOfVowelsInString()
        {
            string source = "Alexandra";
            Assert.Equal(5, source.CountConsonants());
        }

        [Fact]
        public void FirstUniqueReturnsFirstUniqueCharInString()
        {
            string source = "ababacd";
            Assert.Equal('c', source.FirstUnique());
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
        public void MostFrequentCharReturnsTheCharThatAppearsTheMostFrequent()
        {
            string source = "cababad";
            Assert.Equal('a', source.MostFrequentChar());
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
        public void GetSubarraysReturnsAllSubarraysOfArrayWithElementsThatAddToLessOrEqualWithK()
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
    }
}
