using System;

namespace Extension_methods
{
    public class ExercisesFacts
    {
        [Fact]
        public void CountVowelsReturnsNumberOfVowelsInString()
        {
            string source = "Alexandra";
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
            string source = "mmmmmmmmm";
            Assert.Equal(0, source.CountVowels());
        }

        [Fact]
        public void CountConsonantsReturnsNumberOfVowelsInString()
        {
            string source = "Alexandra";
            Assert.Equal(5, source.CountConsonants());
        }

        [Fact]
        public void CountConsonantsReturns0WhenStringHasNoConsonants()
        {
            string source = "aaaaaaa";
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
    }
}
