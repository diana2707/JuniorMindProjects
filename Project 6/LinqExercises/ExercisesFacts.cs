using System;

namespace LinqExercises
{
    public class ExercisesFacts
    {
        [Fact]
        public void GetLongestDistinctElemSubarrayReturnSubarrayWith1Element()
        {
            List<int> list = [1];

            var result = Exercises.GetLongestDistinctSubarray(list);

            List<int> expected = [1];

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetLongestDistinctElemSubarrayReturnSubarrayWithDistinctElementsWhenElementsRepeat()
        {
            List<int> list = [1, 2, 2];

            var result = Exercises.GetLongestDistinctSubarray(list);

            List<int> expected = [1, 2 ];

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetLongestDistinctSubarrayReturnLongestSubarrayWhenASmallerOneExists()
        {
            List<int> list = [ 1, 2, 2, 3, 4 ];

            var result = Exercises.GetLongestDistinctSubarray(list);

            List<int> expected = [2, 3, 4];

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetLongestDistinctSubarrayReturnLongestDistinctSubarrayWhenTheSubarrayIsNotAtEnd()
        {
            List<int> list = [1, 2, 2, 3, 4, 5, 4];

            var result = Exercises.GetLongestDistinctSubarray(list);

            List<int> expected = [2, 3, 4, 5];

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GeneratePatternReturnDescendingRangeFromNTo1()
        {
            int n = 1;

            var result = Exercises.GeneratePattern(1);

            List<int> expected = [1, -1];

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GeneratePatternReturnDescendingRangesForNNumberOfRows()
        {
            int n = 2;

            var result = Exercises.GeneratePattern(n);

            List<int> expected = [2, 2, 1, 1, -1, 2, 1, -1];

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GeneratePatternReturnDescendingRangesWithNegative1AsRowSeparater()
        {
            int n = 3;

            var result = Exercises.GeneratePattern(n);

            List<int> expected = [3, 3, 3, 2, 2, 2, 1, 1, 1, -1, 3, 3, 2, 2, 1, 1, -1, 3, 2, 1, -1];

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetLongestNoSaleStreakReturnsSameSequenceWhenThereAreNoSales()
        {
            List<int> sequence = [0];

            var result = Exercises.GetLongestNoSaleStreak(sequence);

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetLongestNoSaleStreakReturnsSequenceWhenThereIsASingleNoSaleDay()
        {
            List<int> sequence = [0, 1];

            var result = Exercises.GetLongestNoSaleStreak(sequence);

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetLongestNoSaleStreakReturnsLongestSequenceWhenThereAreMoreThanOne()
        {
            List<int> sequence = [0, 1, 0, 0];

            var result = Exercises.GetLongestNoSaleStreak(sequence);

            Assert.Equal(2, result);
        }

        [Fact]
        public void GetLongestNoSaleStreakReturnsLongestSequenceWhenMultimpleSequencesExist()
        {
            List<int> sequence = [1, 2, 1, 1, 0, 3, 1, 0, 0, 2, 4, 1, 0, 0, 0, 0, 2, 1, 0, 3, 1, 0, 0, 0, 6, 1, 3, 0, 0, 0];

            var result = Exercises.GetLongestNoSaleStreak(sequence);

            Assert.Equal(4, result);
        }

        [Fact]
        public void GetFullHouseReturnsTheFullHouseSubstringWhenASingleFullHouseHandExists()
        {
            var input = "10♣ Q♥ 10♠ Q♠ 10♦";

            var result = Exercises.GetFullHouse(input);

            Assert.Equal("10♣ Q♥ 10♠ Q♠ 10♦", result);
        }

        [Fact]
        public void GetFullHouseReturnsTheFullHouseSubstringWhenAFullHouseAndADifferentHandExists()
        {
            var input = "4♣ 5♦ 6♦ 7♠ 10♥;10♣ Q♥ 10♠ Q♠ 10♦";

            var result = Exercises.GetFullHouse(input);

            Assert.Equal("10♣ Q♥ 10♠ Q♠ 10♦", result);
        }

    }
}
