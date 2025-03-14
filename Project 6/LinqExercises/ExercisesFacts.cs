using System;
using System.Collections;
using System.Collections.Generic;

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

        [Fact]
        public void GetFullHouseReturnsTheFullHouseSubstringWhenMoreFullHouseAndADifferentHandsExists()
        {
            var input = "4♣ 5♦ 6♦ 7♠ 10♥;10♣ Q♥ 10♠ Q♠ 10♦;6♣ 6♥ 6♠ A♠ 6♦;2♣ 3♥ 3♠ 2♠ 2♦;2♣ 3♣ 4♣ 5♠ 6♠";

            var result = Exercises.GetFullHouse(input);

            Assert.Equal("10♣ Q♥ 10♠ Q♠ 10♦;2♣ 3♥ 3♠ 2♠ 2♦", result);
        }

        [Fact]
        public void GetChristmasDaysReturnsTheChristmasDayForCurrentYearz()
        {
            int years = 10;
            int startYear = DateTime.Now.Year;
            var result = Exercises.GetChristmasDays(years, startYear);

            Assert.Equal("Thursday, Friday, Saturday, Monday, Tuesday, Wednesday, Thursday, Saturday, Sunday, Monday", result);
        }

        [Fact]
        public void SelectAnagramsOfReturnsAnagramWhenASingleOneExists()
        {
            string input = "star";

            string dictionary = "rats";

            var result = Exercises.SelectAnagramsOf(input, dictionary);

            Assert.Equal("rats", result);
        }

        [Fact]
        public void SelectAnagramsOfReturnsAnagramWhenMultipleValidAnagramsAndInvalidOnesExist()
        {
            string input = "star";

            string dictionary = "parts,traps,arts,rats,starts,tarts,rat,art,tar,tars,stars,stray";

            var result = Exercises.SelectAnagramsOf(input, dictionary);

            Assert.Equal("arts, rats, tars", result);
        }

        [Fact]
        public void GetAllAnagramsReturnEmptyStringForEmptyInput()
        {
            var result = Exercises.GetAllAnagrams(string.Empty);

            Assert.Equal([""], result);
        }

        [Fact]
        public void GetAllAnagramsReturnSameStringFor1CharacterInput()
        {
            var result = Exercises.GetAllAnagrams("a");

            Assert.Equal(["a"], result);
        }

        [Fact]
        public void GetAllAnagramsReturnCorrectStringsFor2CharactersInput()
        {
            var result = Exercises.GetAllAnagrams("ab");

            Assert.Equal(["ab", "ba"], result);
        }

        [Fact]
        public void GetAllAnagramsReturnCorrectStringsFor3CharactersInput()
        {
            var result = Exercises.GetAllAnagrams("abc");

            Assert.Equal(["abc", "acb", "bac", "bca", "cab", "cba"], result);
        }

        [Fact]
        public void GetAllAnagramsReturnCorrectStringsFor4CharactersInput()
        {
            var result = Exercises.GetAllAnagrams("abcd");

            Assert.Equal(24, result.Count());
        }

        [Fact]
        public void GroupAnagramsReturnEmptyCollectionWhenInputIsEmpty()
        {
            var result = Exercises.GroupAnagrams([]);

            Assert.Equal([], result);
        }

        [Fact]
        public void GroupAnagramsReturnSameWordWhenInputIsOneWord()
        {
            var result = Exercises.GroupAnagrams(["abc"]);

            Assert.Equal([["abc"]], result);
        }

        [Fact]
        public void GroupAnagramsReturn2GroupsWhenInputAre2WordsNonAnagrams()
        {
            var result = Exercises.GroupAnagrams(["abc", "rat"]);

            Assert.Equal([["abc"], ["rat"]], result);
        }

        [Fact]
        public void GroupAnagramsReturn2GroupsWhenInputAre2WordsAnagramsAndOneAnagram()
        {
            var result = Exercises.GroupAnagrams(["abc", "rat", "cab"]);

            Assert.Equal([["abc", "cab"], ["rat"]], result);
        }

        [Fact]
        public void GroupAnagramsReturnCorrectGroupsWhenInputHasMultipleAnagrams()
        {
            var result = Exercises.GroupAnagrams(["listen", "silent", "enlist", "abc", "cab", "bac", "rat", "tar", "art"]);

            Assert.Equal([["listen", "silent", "enlist"], ["abc", "cab", "bac"], ["rat", "tar", "art"]], result);
        }

        [Fact]
        public void GetSubstringAnagramsReturnEmptyCollectionWhenInputIsEmpty()
        {
            var result = Exercises.GetSubstringAnagrams("");

            Assert.Equal([], result);
        }

        [Fact]
        public void GetSubstringAnagramsReturnEmptyCollectionWhenStringContains1Character()
        {
            var result = Exercises.GetSubstringAnagrams("a");

            Assert.Equal([], result);
        }

        [Fact]
        public void GetSubstringAnagramsReturnAnagramCollectionWhenStringContainse2RepeatingCharacter()
        {
            var result = Exercises.GetSubstringAnagrams("aa");

            Assert.Equal([["a", "a"]], result);
        }

        [Fact]
        public void GetSubstringAnagramsReturnAnagramCollectionWhenStringContainse2RepeatingCharactersAndADifferentOne()
        {
            var result = Exercises.GetSubstringAnagrams("aab");

            Assert.Equal([["a", "a"]], result);
        }

        [Fact]
        public void GetSubstringAnagramsReturnAnagramCollectionWhenStringContaineMultipleAnagramSubstrings()
        {
            var result = Exercises.GetSubstringAnagrams("abbac");

            Assert.Equal([["a", "a"], ["ab", "ba"], ["abb", "bba"], ["b", "b"]], result);
        }

        [Fact]
        public void GroupInitialsReturnEmptyCollectionWhenInputIsEmpty()
        {
            string input = "";

            var result = Exercises.GroupInitials(input);

            Assert.Equal([], result);
        }

        [Fact]
        public void GroupInitialsReturnSameNamesWhen2SameInitialNamesExist()
        {
            string input = "Santi Cazorla, Sol Campbell";

            var result = Exercises.GroupInitials(input);

            Assert.Equal([["Santi Cazorla", "Sol Campbell"]], result);
        }

        [Fact]
        public void GroupInitialsReturnSameInitialNamesGroupWhenFromInputThatIncludesDifferentInitialsName()
        {
            string input = "Santi Cazorla, Per Mertesacker, Sol Campbell";

            var result = Exercises.GroupInitials(input);

            Assert.Equal([["Santi Cazorla", "Sol Campbell"]], result);
        }

        [Fact]
        public void GroupInitialsReturnSameInitialNamesGroupWhenMultipleGroupsExist()
        {
            string input = "Santi Cazorla, Per Mertesacker, Alan Smith, Thierry Henry, Alex Song," +
                " Paul Merson, Alexis Sánchez, Robert Pires, Dennis Bergkamp, Sol Campbell";

            var result = Exercises.GroupInitials(input);

            Assert.Equal(
                [["Santi Cazorla", "Sol Campbell"], ["Per Mertesacker", "Paul Merson"],
                        ["Alan Smith", "Alex Song", "Alexis Sánchez"]], result);
        }

        [Fact]
        public void GetTimeIntervalsReturnsWholeIntervalWhenNoIntervalsToRemoveAreProvided()
        {
            string intervals = "";

            var result = Exercises.GetRemainingTimeIntervals(intervals);

            Assert.Equal("0:00:00-2:00:00", result);
        }

        [Fact]
        public void GetTimeIntervalsReturnsRemainingIntervalsWhenIntervalToRemoveIsProvidedAtStart()
        {
            string intervals = "0:00:00-0:00:05";

            var result = Exercises.GetRemainingTimeIntervals(intervals);

            Assert.Equal("0:00:05-2:00:00", result);
        }

        [Fact]
        public void GetTimeIntervalsReturnsRemainingIntervalsWhenIntervalToRemoveIsProvidedAtEnd()
        {
            string intervals = "1:59:55-2:00:00";

            var result = Exercises.GetRemainingTimeIntervals(intervals);

            Assert.Equal("0:00:00-1:59:55", result);
        }

        [Fact]
        public void GetTimeIntervalsReturnsRemainingIntervalsWhenIntervalToRemoveIsMiddleInterval()
        {
            string intervals = "0:55:12-1:05:02";

            var result = Exercises.GetRemainingTimeIntervals(intervals);

            Assert.Equal("0:00:00-0:55:12;1:05:02-2:00:00", result);
        }

        [Fact]
        public void GetTimeIntervalsReturnsRemainingIntervalsWhenMultipleIntervalsAreRemoved()
        {
            string intervals = "0:00:00-0:00:05;0:55:12-1:05:02;1:37:47-1:37:51";

            var result = Exercises.GetRemainingTimeIntervals(intervals);

            Assert.Equal("0:00:05-0:55:12;1:05:02-1:37:47;1:37:51-2:00:00", result);
        }

        [Fact]
        public void GetMaxSumSubarrayReturnEmptyCollectionWheninputIsEmpty()
        {
            int[] input = [];

            Assert.Equal([], Exercises.GetMaxSumSubarray(input));
        }

        [Fact]
        public void GetMaxSumSubarrayReturnCorrectCollectionWhenInputIs1Integer()
        {
            int[] input = [1];

            Assert.Equal([1], Exercises.GetMaxSumSubarray(input));
        }

        [Fact]
        public void GetMaxSumSubarrayReturnCorrectCollectionWhenInputHas1PositiveAnd1NegativeInteger()
        {
            int[] input = [-1, 1];

            Assert.Equal([1], Exercises.GetMaxSumSubarray(input));
        }

        [Fact]
        public void GetMaxSumSubarrayReturnCorrectCollectionWhenInputHasInterleavedPositiveAnNegstiveIntegers()
        {
            int[] input = [-1, 2, -1, 2];

            Assert.Equal([2, -1, 2], Exercises.GetMaxSumSubarray(input));
        }

        [Fact]
        public void GetMaxSumSubarrayReturnCorrectCollectionWhenInputHasInterleavedPositiveAnNegstiveIntegersx()
        {
            int[] input = [-2, 1, -3, 4, -1, 2, 1, -5, 4];

            Assert.Equal([4, -1, 2, 1], Exercises.GetMaxSumSubarray(input));
        }

    }
}
