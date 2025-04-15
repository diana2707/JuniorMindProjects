using System;
using System.Runtime.CompilerServices;

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
        public void GetLongestDistinctElemSubarrayReturnSubarrayWithDistinctElementsWhenSequenceIsAtBeginning()
        {
            List<int> list = [1, 2, 2];

            var result = Exercises.GetLongestDistinctSubarray(list);

            List<int> expected = [1, 2 ];

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetLongestDistinctElemSubarrayReturnSubarrayWithDistinctElementsWhenSequenceIsAtEnd()
        {
            List<int> list = [1, 1, 2];

            var result = Exercises.GetLongestDistinctSubarray(list);

            List<int> expected = [1, 2];

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetLongestDistinctSubarrayReturnLongestSubarrayWhenASmallerOneExists()
        {
            List<int> list = [1, 2, 2, 3, 4];

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
        public void GetLongestDistinctSubarrayReturnLongestDistinctSubarrayWhenDuplicatesAreNotConsequtive()
        {
            List<int> list = [1, 2, 3, 1, 2, 4, 5];

            var result = Exercises.GetLongestDistinctSubarray(list);

            List<int> expected = [3, 1, 2, 4, 5];

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetLongestDistinctSubarrayThrowsArgumentNullExceptionWhenArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => Exercises.GetLongestDistinctSubarray(null));
        }

        [Fact]
        public void GeneratePatternReturnEmptyCollectionWhenNIs0()
        {
            int n = 0;

            var result = Exercises.GeneratePattern(0);

            Assert.Equal([], result);
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
        public void GetLongestNoSaleStreakThrowsArgumentNullExceptionWhenArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => Exercises.GetLongestNoSaleStreak(null));
        }

        [Fact]
        public void GetLongestNoSaleStreakReturnsSameSequenceWhenThereAreNoSalesc()
        {
            List<int> sequence = [];

            var result = Exercises.GetLongestNoSaleStreak(sequence);

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetFullHouseThrowsArgumentNullExceptionWhenInputIsENull()
        {
            Assert.Throws<ArgumentNullException>(() => Exercises.GetFullHouse(null));
        }

        [Fact]
        public void GetFullHouseReturnsEmptyStringWhenInputIsEmpty()
        {
            var input = string.Empty;

            var result = Exercises.GetFullHouse(input);

            Assert.Equal(string.Empty, result);
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
            var input = "4♣ 5♦ 6♦ 7♠ 10♥;9♣ Q♥ 9♠ Q♠ 9♦;6♣ 6♥ 6♠ A♠ 6♦;2♣ 3♥ 3♠ 2♠ 2♦;2♣ 3♣ 4♣ 5♠ 6♠;10♣ 8♥ 10♠ 8♠ 10♦;J♣ Q♥ J♠ Q♠ J♦";

            var result = Exercises.GetFullHouse(input);

            Assert.Equal("9♣ Q♥ 9♠ Q♠ 9♦;2♣ 3♥ 3♠ 2♠ 2♦;10♣ 8♥ 10♠ 8♠ 10♦;J♣ Q♥ J♠ Q♠ J♦", result);
        }

        [Fact]
        public void GetChristmasDaysReturnsTheChristmasDayForCurrentYearz()
        {
            int years = 0;
            int startYear = DateTime.Now.Year;
            var result = Exercises.GetChristmasDays(years, startYear);

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void GetChristmasDaysReturnsTheChristmasDayStartingWithCurrentYearAndForMultipleYers()
        {
            int years = 10;
            int startYear = DateTime.Now.Year;
            var result = Exercises.GetChristmasDays(years, startYear);

            Assert.Equal("Thursday, Friday, Saturday, Monday, Tuesday, Wednesday, Thursday, Saturday, Sunday, Monday", result);
        }

        [Fact]
        public void SelectAnagramsOfReturnsEmptyStringWhenInputIsEmpty()
        {
            string input = string.Empty;

            string dictionary = "rats";

            var result = Exercises.SelectAnagramsOf(input, dictionary);

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void SelectAnagramsOfReturnsEmptyStringWhendictionaryIsEmpty()
        {
            string input = "star";

            string dictionary = string.Empty;

            var result = Exercises.SelectAnagramsOf(input, dictionary);

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void SelectAnagramsOfThrowsArgumentNUllExceptionWhenArgumentsAreNull()
        {
            Assert.Throws<ArgumentNullException>(() => Exercises.SelectAnagramsOf(null, null));
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
        public void GetAllAnagramsThrowsArgumentNullExceptionWhenArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => Exercises.GetAllAnagrams(null));
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
        public void GroupAnagramsThrowsArgumentNullExceptionWhenArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => Exercises.GroupAnagrams(null));
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
        public void GetSubstringAnagramsThrowsArgumentNullExceptionWhenInputIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => Exercises.GetSubstringAnagrams(null));
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
        public void GroupInitialsThrowsArgumentNullExceptionWhenArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => Exercises.GroupInitials(null));
        }

        [Fact]
        public void GroupInitialsReturnEmptyCollectionWhenInputIsEmpty()
        {
            string input = string.Empty;

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
        public void GetTimeIntervalsThrowsArgumentNullExceptionWhenArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => Exercises.GetRemainingTimeIntervals(null));
        }

        [Fact]
        public void GetTimeIntervalsReturnsWholeIntervalWhenNoIntervalsToRemoveAreProvided()
        {
            string intervals = string.Empty;

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
        public void GetMaxSumSubarrayThrowsArgumentNullExceptionWhenArgumentisNull()
        {
            Assert.Throws<ArgumentNullException>(() => Exercises.GetMaxSumSubarray(null));
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

        [Fact]
        public void GetQuadrupletsThrowsArgumentNullExceptionWhenArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => Exercises.GetQuadruplets(null, 0));
        }

        [Fact]
        public void GetQuadrupletsReturnEmptyCollectionWhenInputIsEmpty()
        {
            int[] input = [];

            var result = Exercises.GetQuadruplets(input, 0);

            Assert.Equal([], result);
        }

        [Fact]
        public void GetQuadrupletsReturnEmptyCollectionWhenInputContainsLessThan4Integers()
        {
            int[] input = [1];

            var result = Exercises.GetQuadruplets(input, 0);

            Assert.Equal([], result);
        }

        [Fact]
        public void GetQuadrupletsReturnQuadrupletWhenInputHasSingleQuadrupletThatSumsToGivenValue()
        {
            int[] input = [1, 2, 3, 4];

            var result = Exercises.GetQuadruplets(input, 10);

            Assert.Equal([[1, 2, 3, 4]], result);
        }

        [Fact]
        public void GetQuadrupletsReturnQuadrupletWhenMoreThan4IntegersExist()
        {
            int[] input = [0, 1, 2, 3, 4];

            var result = Exercises.GetQuadruplets(input, 10);

            Assert.Equal([[1, 2, 3, 4]], result);
        }

        [Fact]
        public void GetQuadrupletsReturnQuadrupletWhenMultipleQuadrupletsExistt()
        {
            int[] input = [1, -1, -2, 2, -3, 3];

            var result = Exercises.GetQuadruplets(input, 0);

            Assert.Equal([[1, -1, -2, 2],[1, -1, -3, 3], [-2, 2, -3, 3]], result);
        }

        [Fact]
        public void GetMaxSumCountiguousSubarrayReturnsEmptyCollectionWhenInputIsEmpty()
        {
            Assert.Equal([], Exercises.GetMaxSumContiguousSubarray([]));
        }

        [Fact]
        public void GetMaxSumCountiguousSubarrayReturnsCorrectSubarrayWhenInputIs2Characters()
        {
            Assert.Equal([1], Exercises.GetMaxSumContiguousSubarray([-1, 1]));
        }

        [Fact]
        public void GetMaxSumCountiguousSubarrayReturnsCorrectSubarrayWhenInputIsMultipleCharacters()
        {
            Assert.Equal([4, -1, 5, 6], Exercises.GetMaxSumContiguousSubarray([-5, 3, -6, 4, -1, 5, 6]));
        }

        [Fact]
        public void GetMaxSumCountiguousSubarrayThrowsArgumentNullExceptionWhenArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => Exercises.GetMaxSumContiguousSubarray(null));
        }

        [Fact]
        public void FindPairWithSumReturnsTupleWithNegative1WhenSourceIsLessThan2()
        {
            Assert.Equal((-1, -1), Exercises.FindPairWithSum(1, [1]));
        }

        [Fact]
        public void FindPairWithSumReturnsIndexesWhenSourceContains2IntegersThatSumToTarget()
        {
            Assert.Equal((0, 1), Exercises.FindPairWithSum(3, [1, 2]));
        }

        [Fact]
        public void FindPairWithSumReturnsNegative1IndexesWhenSourceContains2IntegersThatNotSumToTarget()
        {
            Assert.Equal((-1, -1), Exercises.FindPairWithSum(3, [1, 1]));
        }

        [Fact]
        public void FindPairWithSumReturnsIndexesWhenSourceContains3IntegersAndFirstAndLast2SumToTarget()
        {
            Assert.Equal((0, 2), Exercises.FindPairWithSum(4, [1, 2, 3]));
        }

        [Fact]
        public void FindPairWithSumReturnsIndexesWhenSourceContains3IntegersAndLast2SumToTarget()
        {
            Assert.Equal((1, 2), Exercises.FindPairWithSum(5, [1, 2, 3]));
        }

        [Fact]
        public void GetNextGreaterElementReturnsEmptyCollectionWhenSourceisEmpty()
        {
            Assert.Equal([], Exercises.GetNextGreaterElement([]));
        }

        [Fact]
        public void GetNextGreaterElementReturnsNegativeOneWhenNoGreterElementExists()
        {
            Assert.Equal([-1], Exercises.GetNextGreaterElement([1]));
        }

        [Fact]
        public void GetNextGreaterElementReturnsNextGreaterElementWhenSourceHasOrderedElements()
        {
            Assert.Equal([2, -1], Exercises.GetNextGreaterElement([1, 2]));
        }

        [Fact]
        public void GetNextGreaterElementReturnsNextGreaterElementWhenSourceHasUnorderedElements()
        {
            Assert.Equal([3, 3, -1], Exercises.GetNextGreaterElement([2, 1, 3]));
        }

        [Fact]
        public void GetNextGreaterElementReturnsNextGreaterElementWhenSourceHasMultipleUnorderedElemets()
        {
            Assert.Equal([5, 5, 6, 4, 6, -1, -1], Exercises.GetNextGreaterElement([4, 1, 5, 2, 4, 6, 3]));
        }

        [Fact]
        public void GetNextGreaterElementReturnsNextGreaterElementWhenSourceHasMultipleUnorderedElemetszzzz()
        {
            Assert.Equal([8, 8, -1, 4, 6, -1, -1], Exercises.GetNextGreaterElement([4, 1, 8, 2, 4, 6, 3]));
        }

        [Fact]
        public void GetMaximumElementsReturnEmptyCollectionWhenSourceIsEmpty()
        {
            Assert.Equal([], Exercises.GetMaximumElements([], 1));
        }

        [Fact]
        public void GetMaximumElementsReturnIntegerWhenSourceContains1IntegerIntervalSize1()
        {
            Assert.Equal([1], Exercises.GetMaximumElements([1], 1));
        }

        [Fact]
        public void GetMaximumElementsReturnIntegerWhenSourceContains2IntegerIntervalSize2()
        {
            Assert.Equal([2], Exercises.GetMaximumElements([1, 2], 2));
        }

        [Fact]
        public void GetMaximumElementsReturnIntegerWhenSourceContains2IntervalsOf1Integer()
        {
            Assert.Equal([1, 2], Exercises.GetMaximumElements([1, 2], 1));
        }

        [Fact]
        public void GetMaximumElementsReturnIntegerWhenSOurceElementsAreDecreasing()
        {
            Assert.Equal([2, 1], Exercises.GetMaximumElements([2, 1], 1));
        }

        [Fact]
        public void GetMaximumElementsReturnIntegerWhenSourceContains3IntervalsOfSize4()
        {
            Assert.Equal([2, 3, 4], Exercises.GetMaximumElements([1, 2, 3, 4], 2));
        }

        [Fact]
        public void GetMaximumElementsReturnIntegerWhenSourceContainsElementsInDecreasingOrder()
        {
            Assert.Equal([4, 3, 2], Exercises.GetMaximumElements([4, 3, 2, 1], 2));
        }

        [Fact]
        public void GetMaximumElementsReturnIntegerWhenSourceIsUnordered()
        {
            Assert.Equal([5, 6, 6, 6], Exercises.GetMaximumElements([2, 5, 3, 6, 2, 1], 3));
        }

        [Fact]
        public void GetMaximumElementsReturnIntegerWhenSourceContainsNegativeElements()
        {
            Assert.Equal([3, 3, 5, 5, 6, 7], Exercises.GetMaximumElements([1, 3, -1, -3, 5, 3, 6, 7], 3));
        }

        [Fact]
        public void GetMaximumElementsReturnIntegerWhenMaxElementInFirstIntervalIsLast()
        {
            Assert.Equal([5, 6], Exercises.GetMaximumElements([3, 1, 5, 6 ], 3));
        }

        [Fact]
        public void GetMaximumElementsReturnIntegerWhenSourceContainsRepeatingElements()
        {
            Assert.Equal([4, 4, 4], Exercises.GetMaximumElements([4, 4, 4, 4, 4], 3));
        }

        [Fact]
        public void GetMaximumElementsReturnIntegerWhenSourceContains()
        {
            Assert.Equal([10, 10, 9, 2], Exercises.GetMaximumElements([9, 10, 9, -7, -4, -8, 2, -6], 5));
        }

        [Fact]
        public void GetMaximumElementsReturnIntegerWhenSizeIsGreaterThansource()
        {
            Assert.Equal([], Exercises.GetMaximumElements([4, 2, 1], 5));
        }

        [Fact]
        public void GetFirstUniqueCharacterReturnDefaultCharacterWhenStringIsEmpty()
        {
            Assert.Equal('\0', Exercises.GetFirstUniqueCharacter(string.Empty));
        }

        [Fact]
        public void GetFirstUniqueCharacterReturnSameCharWhenInputContainsSingleChar()
        {
            Assert.Equal('a', Exercises.GetFirstUniqueCharacter("a"));
        }

        [Fact]
        public void GetFirstUniqueCharacterReturnFirstCharWhenStringContains2UniqueChars()
        {
            Assert.Equal('a', Exercises.GetFirstUniqueCharacter("ab"));
        }

        [Fact]
        public void GetFirstUniqueCharacterReturnFirstUniqueCharWhebInputContainsRepeatingCharacters()
        {
            Assert.Equal('a', Exercises.GetFirstUniqueCharacter("bab"));
        }

        [Fact]
        public void GetFirstUniqueCharacterReturnDefaultValueWhenNoUniqueCharsExist()
        {
            Assert.Equal('\0', Exercises.GetFirstUniqueCharacter("baba"));
        }

        [Fact]
        public void GetMaxNonOverlapingIntervalsReturnEmptyCollectionWhenSourceIsEmpty()
        {
            Assert.Equal([], Exercises.GetMaxNonOverlapingIntervals([]));
        }

        [Fact]
        public void GetMaxNonOverlapingIntervalsReturnSameIntervalWhenSourceContains1Interval()
        {
            Assert.Equal([(1, 2)], Exercises.GetMaxNonOverlapingIntervals([(1, 2)]));
        }

        [Fact]
        public void GetMaxNonOverlapingIntervalsReturnOneIntervalsWhen2IntervalsOverlap()
        {
            Assert.Equal([(1, 3)], Exercises.GetMaxNonOverlapingIntervals([(1, 2), (1, 3)]));
        }

        [Fact]
        public void GetMaxNonOverlapingIntervalsReturnCorrectIntervalsWhenMultipleIntervalsOverlap()
        {
            Assert.Equal([(1, 2), (2, 3), (3, 4), (5, 6)], Exercises.GetMaxNonOverlapingIntervals([(1, 2), (2, 3), (3, 4), (1, 3), (2, 5), (5, 6)]));
        }

        [Fact]
        public void GetMaxNonOverlapingIntervalsReturnCorrectIntervalsWhen1IntarvalOverlapsWholeOtherIntervals()
        {
            Assert.Equal([(2, 3), (4, 5), (6, 7), (8, 9)], Exercises.GetMaxNonOverlapingIntervals([(1, 10), (2, 3), (4, 5), (6, 7), (8, 9)]));
        }

        [Fact]
        public void GetMaxNonOverlapingIntervalsReturnCorrectIntervalsWhenIntervalsAreUnordered()
        {
            Assert.Equal([(2, 3), (3, 4), (1, 2), (5, 6)], Exercises.GetMaxNonOverlapingIntervals([(2, 5), (2, 3), (3, 4), (1, 3), (1, 2), (5, 6)]));
        }

        [Fact]
        public void GetMaxNonOverlapingIntervalsReturnCorrectIntervalsWhenSmallerIntervalOverlaps2LargerIntervals()
        {
            Assert.Equal([(1, 10), (10, 20)], Exercises.GetMaxNonOverlapingIntervals([(1, 10), (9, 11), (10, 20)]));
        }

        [Fact]
        public void GetMaxNonOverlapingIntervalsReturnCorrectIntervalsWhenEveryIntervalOverlapsTheNext()
        {
            Assert.Equal([(1, 3), (4, 7), (8, 10)], Exercises.GetMaxNonOverlapingIntervals([(1, 3), (2, 5), (4, 7), (6, 9), (8, 10)]));
        }

        [Fact]
        public void GetMaxNonOverlapingIntervalsReturnCorrectIntervalsWhenNoIntervalsOverlap()
        {
            Assert.Equal([(1, 2), (2, 3), (3, 4), (4, 5), (5, 6)], Exercises.GetMaxNonOverlapingIntervals([(1, 2), (2, 3), (3, 4), (4, 5), (5, 6)]));
        }

        [Fact]
        public void GetMinInsertsToPalindromReturn0WhenSourceIsEmpty()
        {
            Assert.Equal(0, Exercises.GetMinInsertsToPalindrom(string.Empty));
        }

        [Fact]
        public void GetMinInsertsToPalindromReturn0WhenSourceIs1Character()
        {
            Assert.Equal(0, Exercises.GetMinInsertsToPalindrom("a"));
        }

        [Fact]
        public void GetMinInsertsToPalindromReturn1WhenSourceIs2Characters()
        {
            Assert.Equal(1, Exercises.GetMinInsertsToPalindrom("ab"));
        }

        [Fact]
        public void GetMinInsertsToPalindromReturn1WhenSourceIs3Characters()
        {
            Assert.Equal(2, Exercises.GetMinInsertsToPalindrom("abc"));
        }

        [Fact]
        public void GetMinInsertsToPalindromReturn1WhenPalindromicPrefixIs2Longer()
        {
            Assert.Equal(1, Exercises.GetMinInsertsToPalindrom("aab"));
        }

        [Fact]
        public void GetMinInsertsToPalindromReturn1WhenPalindromicSufixIsLonger()
        {
            Assert.Equal(1, Exercises.GetMinInsertsToPalindrom("abb"));
        }

        [Fact]
        public void GetMinInsertsToPalindromReturn1WhenPalindromicSufixAndPrefixMatch()
        {
            Assert.Equal(2, Exercises.GetMinInsertsToPalindrom("abcda"));
        }

        [Fact]
        public void GetSmallestSubarrayWithSumResturnEmptyCollectionWhenSourceisEmpty()
        {
            Assert.Equal([], Exercises.GetSmallestSubarrayWithSumAbove([], 0));
        }

        [Fact]
        public void GetSmallestSubarrayWithSumResturnCorrectSubarrayWhenSourceContains1Int()
        {
            Assert.Equal([1], Exercises.GetSmallestSubarrayWithSumAbove([1], 1));
        }

        [Fact]
        public void GetSmallestSubarrayWithSumResturnCorrectSubarrayWhenSourceAddsToLimit()
        {
            Assert.Equal([1, 2], Exercises.GetSmallestSubarrayWithSumAbove([1, 2], 3));
        }

        [Fact]
        public void GetSmallestSubarrayWithSumResturncorrectSubarrayWhenMultipleSubarraysAddEqualWithLimit()
        {
            Assert.Equal([1], Exercises.GetSmallestSubarrayWithSumAbove([0, 1], 1));
        }

        [Fact]
        public void GetSmallestSubarrayWithSumResturnCorrectSubarrayWhenMultipleSubarraysAddAboveTheLimit()
        {
            Assert.Equal([3], Exercises.GetSmallestSubarrayWithSumAbove([0, 1, 1, 3], 2));
        }

        [Fact]
        public void GetSmallestSubarrayWithSumResturnEmptyCollectionWhenNoSubarrayExist()
        {
            Assert.Equal([], Exercises.GetSmallestSubarrayWithSumAbove([0, 1, 1, 3], 10));
        }

        [Fact]
        public void GetSmallestSubarrayWithSumRestursCorrectSubarrayWhenIsAtEndOfSource()
        {
            Assert.Equal([5, 10], Exercises.GetSmallestSubarrayWithSumAbove([5, 1, 3, 5, 10], 15));
        }

        [Fact]
        public void GetSumOfSumSeriesReturn0WhenNIs0()
        {
            Assert.Equal(0, Exercises.GetSumOfSumSeries(0));
        }

        [Fact]
        public void GetSumOfSumSeriesReturn1WhenNIs1()
        {
            Assert.Equal(1, Exercises.GetSumOfSumSeries(1));
        }

        [Fact]
        public void GetSumOfSumSeriesReturn4WhenNIs2()
        {
            Assert.Equal(4, Exercises.GetSumOfSumSeries(2));
        }

        [Fact]
        public void GetSumOfSumSeriesReturn10WhenNIs3()
        {
            Assert.Equal(10, Exercises.GetSumOfSumSeries(3));
        }
    }
}
