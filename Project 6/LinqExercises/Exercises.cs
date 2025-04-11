
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;

namespace LinqExercises
{
    public static class Exercises
    {

        //  Exercise 1
        //
        //  Given a collection return the longest subarray with distinct elements

        public static IEnumerable<int> GetLongestDistinctSubarray(IEnumerable<int> input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var inputArray = input.ToArray();
            int[] result = [];
            HashSet<int> verified = [];

            int start = 0;
            int end = 0;

            while (start < inputArray.Length && end < inputArray.Length)
            {
                if (verified.Contains(inputArray[end]))
                {
                    verified.Remove(inputArray[start]);
                    start++;
                }
                else
                {
                    if (result.Length < (end + 1) - start)
                    {
                        result = inputArray[start..(end + 1)];
                    }

                    verified.Add(inputArray[end]);
                    end++;
                }
            }

            return result;
        }

        //  Exercise 2
        //
        //  You are given a number n. You need to generate and print a pattern based on the given value of n.
        //  For each row, starting from the first, print numbers in descending order from n down to 1.
        //  Each number in a row is repeated as many times as the current row index(starting from n).
        //  Instead of a newline at the end of each row, print -1 to indicate row separation.
        //  After printing the entire pattern, end the output with -1.
        //  Example: Input => 3; Output => [3, 3, 3, 2, 2, 2, 1, 1, 1, -1, 3, 3, 2, 2, 1, 1, -1, 3, 2, 1, -1]

        public static IEnumerable<int> GeneratePattern(int n)
        {
            return Enumerable.Range(1, n)
                .Select((_, index) => Enumerable.Range(1, n)
                    .Select(e => Enumerable.Repeat(e, n - index))
                            .SelectMany(e => e)
                            .OrderByDescending(e => e).Concat([-1]))
                .SelectMany(e => e);
        }

        //Exercise 3
        //
        //  The following string contains number of sales made per day in a month:
        //  1,2,1,1,0,3,1,0,0,2,4,1,0,0,0,0,2,1,0,3,1,0,0,0,6,1,3,0,0,0
        //  How long is the longest sequence of days without a sale? (in this example it's 4)

        public static int GetLongestNoSaleStreak(IEnumerable<int> input)
        {
            ArgumentNullException.ThrowIfNull(input);

            if (!input.Any())
            {
                return 0;
            }

            var inputArray = input.ToArray();
            int maxLength = 0;

            return inputArray.SelectMany((_, start) => Enumerable.Range(start, input.Count() - start)
                            .Select(end => inputArray[start..(end + 1)]))
                .Last(subarray =>
                {
                    if (!subarray.All(e => e == 0))
                    {
                        return false;
                    }

                    if (maxLength >= subarray.Length)
                    {
                        return false;
                    }

                    maxLength = subarray.Length;
                    return true;
                })
                .Count();
        }

        //  Exercise 4
        //
        //  In poker a hand is a "full house" if it contains three cards of one sign and two of another sign.
        //  The following string defines five poker hands, separated by a semicolon:
        //  "4? 5? 6? 7? 10?;10? Q? 10? Q? 10?;6? 6? 6? A? 6?;2? 3? 3? 2? 2?;2? 3? 4? 5? 6?".
        //  Write a LINQ expression that returns a sequence containing only the "full house" hands.

        public static string GetFullHouse(string input)
        {
            ArgumentNullException.ThrowIfNull(input);

            return input.Split(';')
                        .Where(hand =>
                        {
                            var groups = hand.Split(['♣', '♦', '♥', '♠'], StringSplitOptions.RemoveEmptyEntries)
                                              .GroupBy(rank => rank.Trim());

                            return groups.Count() == 2
                                && groups.Any(group => group.Count() == 2)
                                && groups.Any(group => group.Count() == 3);
                        })
                        .Aggregate(string.Empty, (acc, fullHand) => acc + (acc.Length > 0 ? ";" : string.Empty) + fullHand);
        }

        //  Exercise 5
        //
        //What day of the week is Christmas day (25th December) on for the next 10 years (starting with 2025)?
        //The answer should be a string (or sequence of strings)

        public static string GetChristmasDays(int years, int startYear)
        {
            return Enumerable.Range(0, years)
                .Select(n => new DateTime(startYear + n, 12, 25).DayOfWeek.ToString())
                .Aggregate("", (acc, day) => acc + (acc.Length > 0 ? ", " : string.Empty) + day);
        }

        //  Exercise 6
        //
        //  From the following dictionary of words,
        //  "parts,traps,arts,rats,starts,tarts,rat,art,tar,tars,stars,stray"
        //  return all words that are an anagram of star(no leftover letters allowed).

        public static string SelectAnagramsOf(string input, string dictionary)
        {
            ArgumentNullException.ThrowIfNull(input);
            ArgumentNullException.ThrowIfNull(dictionary);

            IEnumerable<char> orderedInput = input.Order();

            return dictionary.Split(',')
                             .Where(word => word.Trim()
                                                .Order()
                                                .SequenceEqual(orderedInput))
                             .Aggregate(string.Empty, (acc, word) => acc + (acc.Length > 0 ? ", " : string.Empty) + word);
        }

        //  Exercise 7
        //
        //  Generate all anagrams of a string

        public static IEnumerable<string> GetAllAnagrams(string input)
        {
            ArgumentNullException.ThrowIfNull(input);

            if (input.Length <= 1)
            {
                return [input];
            }

            return input.SelectMany((_, index) =>
                             GetAllAnagrams(input.Substring(0, index) + input.Substring(index + 1, input.Length - index - 1))
                                   .Select(anagram => input.Substring(index, 1) + anagram));
        }

        // Exercise 7
        //
        //  Given an array of words arr[], the task is to groups strings that are anagrams
        //  Input: arr[] = [“listen”, “silent”, “enlist”, “abc”, “cab”, “bac”, “rat”, “tar”, “art”]
        //  Output: [[“abc”, “cab”, “bac”], [“listen”, “silent”, “enlist”],[“rat”, “tar”, “art”]]

        public static IEnumerable<IEnumerable<string>> GroupAnagrams(IEnumerable<string> input)
        {
            ArgumentNullException.ThrowIfNull(input);

            return input.GroupBy(word => new string(word.Order().ToArray()));
        }

        //  Exercise 8
        //
        //  Find substrings of a string that are anagram of each other

        public static IEnumerable<IEnumerable<string>> GetSubstringAnagrams(string input)
        {
            ArgumentNullException.ThrowIfNull(input);

            if (input.Length <= 1)
            {
                return [];
            }

            return input.SelectMany((e, start) => Enumerable.Range(start, input.Length - start)
                                .Select(end => input[start.. (end + 1)]))
                        .GroupBy(substring => new string(substring.Order().ToArray()))
                        .Where(group => group.Count() > 1);
        }

        //  Exercise 9
        //
        //  From the following list of names
        //  "Santi Cazorla, Per Mertesacker, Alan Smith, Thierry Henry, Alex Song, Paul Merson,
        //  Alexis Sánchez, Robert Pires, Dennis Bergkamp, Sol Campbell"
        //  find any groups of people who share the same initials as each other.

        public static IEnumerable<IEnumerable<string>> GroupInitials(string input)
        {
            ArgumentNullException.ThrowIfNull(input);

            return input.Split(", ")
                        .GroupBy(name => name.Split(' ')
                              .Aggregate(string.Empty, (acc, name) => acc + name.FirstOrDefault()))
                        .Where(group => group.Count() > 1);
        }

        //  Problem 10
        //
        //  A video is two hours long exactly, and we want to make some edits,
        //  cutting out the following time ranges (expressed in H:MM:SS):
        //  "0:00:00-0:00:05;0:55:12-1:05:02;1:37:47-1:37:51".
        //  (You can assume that the input ranges are in order and contain no overlapping portions)
        //  We would like to turn this into a sequence of time-ranges to keep.
        //  So in this example, the output should be: "0:00:05-0:55:12;1:05:02-1:37:47;1:37:51-2:00:00"

        public static string GetRemainingTimeIntervals(string toRemove)
        {
            ArgumentNullException.ThrowIfNull(toRemove);

            string result = string.Empty;

            TimeOnly start = new (0, 0, 0);
            TimeOnly end = new (2, 0, 0);

            if (toRemove.Length == 0)
            {
                return $"{start:H:mm:ss}-{end:H:mm:ss}";
            }

            (TimeOnly start, TimeOnly end)[] intervalsToRemove = toRemove.Split(";")
                                                .Select(interval => interval.Split("-"))
                                                .Select(intervalArray => (TimeOnly.Parse(intervalArray[0]),
                                                                          TimeOnly.Parse(intervalArray[1])))
                                                .ToArray();

            TimeOnly currentStart = start;

            foreach (var intervalToRemove in intervalsToRemove)
            {
                if (intervalToRemove.start > currentStart)
                {
                    result += $"{currentStart:H:mm:ss}-{intervalToRemove.start:H:mm:s}"
                                + (intervalToRemove.end.Equals(end) ? string.Empty : ";");
                }

                currentStart = intervalToRemove.end;
            }

            if (currentStart < end)
            {
                result += $"{currentStart:H:mm:ss}-{end:H:mm:ss}";
            }

            return result;
        }

        // Problem 11
        //
        //  Find the maximum sum subarray
        //  Input: [-2, 1, -3, 4, -1, 2, 1, -5, 4]
        //  Output: [4, -1, 2, 1] with sum = 6

        public static IEnumerable<int> GetMaxSumSubarray(IEnumerable<int> input)
        {
            ArgumentNullException.ThrowIfNull(input);

            if (!input.Any())
            {
                return [];
            }

            var inputArray = input.ToArray();

            return inputArray.SelectMany((_, start) => Enumerable.Range(start, inputArray.Length - start)
                                .Select(end => inputArray[start..(end + 1)]))
                        .GroupBy(subarray => subarray.Sum(e => e))
                        .MaxBy(group => group?.Key) 
                        .MinBy(subarray => subarray.Length);
        }

        //  Problem 12
        //
        //  Given a list of integers and a terget sum, find all unique quadruplets (a, b, c, d)
        //  such that a + b + c + d = targetValue
        //  Each number in the list can be used only once per quadruplet
        //  The quadruplets should be unique (different order of same numbers are not counted as unique)

        public static IEnumerable<IEnumerable<int>> GetQuadruplets(int[] input, int sum)
        {
            ArgumentNullException.ThrowIfNull(input);

            if (input.Length < 4)
            {
                return [];
            }

            return input.SelectMany((_, i) => Enumerable.Range(i + 1, input.Length - i - 1)
                            .SelectMany(j => Enumerable.Range(j + 1, input.Length - j - 1)
                                    .SelectMany(k => Enumerable.Range(k + 1, input.Length - k - 1)
                                            .Select(l => new List<int> { input[i], input[j], input[k], input[l] }))))
                        .Where(list => list.Distinct()
                                           .Count() == 4 && list.Sum(e => e) == sum);
        }

        //  Problema 13
        //
        //  Se dă un șir de numere întregi. Sarcina este să găsești un subșir contiguu
        //  (o secvență de elemente consecutive) din acest șir care are suma elementelor cea mai mare posibilă.

        public static IEnumerable<int> GetMaxSumContiguousSubarray(IEnumerable<int> input)
        {
            ArgumentNullException.ThrowIfNull(input);

            if (!input.Any())
            {
                return input;
            }

            int[] inputArray = input.ToArray();

            int maxSum = int.MinValue;
            int currentSum = 0;

            int start = 0;
            int tempStart = 0;
            int end = 0;

            for (int i = 0; i < inputArray.Length; i++)
            {
                currentSum += inputArray[i];
                end = i;

                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    start = tempStart;
                    end = i;
                }

                if (currentSum < 0)
                {
                    currentSum = 0;
                    tempStart = i + 1;
                }
            }

            return inputArray[start..(end + 1)];
        }

        //  Problema 14
        //  Dată o listă de numere întregi și un număr întreg target, găsiți indecsii a două
        //  numere din listă astfel încât suma lor să fie egală cu target.

        public static (int, int) FindPairWithSum(int target, IEnumerable<int> source)
        {
            int[] sourceArray = source.ToArray();

            if (sourceArray.Length < 2)
            {
                return (-1, -1);
            }

            int index = 0;
            int complement = 0;
            Dictionary<int, int> verified = [];

            while (index < sourceArray.Length)
            {
                complement = target - sourceArray[index];

                if (verified.TryGetValue(complement, out int value))
                {
                    return (value, index);
                }

                verified.TryAdd(sourceArray[index], index);
                index++;
            }

            return (-1, -1);
        }

        //Problem 15
        //
        //  Given an array of integers, the task is to find the Next Greater Element
        //  for each element of the array in order of their appearance in the array.
        //  Note: The Next Greater Element for an element x is the first greater element on
        //  the right side of x in the array.Elements for which no greater element exist,
        //  consider the next greater element as -1.

        public static IEnumerable<int> GetNextGreaterElement(IEnumerable<int> source)
        {
            int[] sourceArray = source.ToArray();

            if (sourceArray.Length == 0)
            {
                return [];
            }

            List<int> result = [];
            result.Add(-1);

            int[] greater = new int[sourceArray.Length];
            int greaterIndex = 0;
            greater[greaterIndex] = -1;

            for (int i = sourceArray.Length - 1; i > 0; i--)
            {
                if (sourceArray[i - 1] < sourceArray[i])
                {
                    result.Add(sourceArray[i]);
                    greaterIndex++;
                    greater[greaterIndex] = sourceArray[i];
                    continue;
                }

                if (greater[greaterIndex] == -1)
                {
                    result.Add(greater[greaterIndex]);
                    continue;
                }

                if (sourceArray[i - 1] < greater[greaterIndex])
                {
                    result.Add(greater[greaterIndex]);
                    continue;
                }

                while (sourceArray[i - 1] > greater[greaterIndex] && greater[greaterIndex] != -1)
                {
                    greaterIndex--;
                }

                result.Add(greater[greaterIndex]);
                greaterIndex++;
                greater[greaterIndex] = sourceArray[i - 1];
            }

            result.Reverse();

            return result;
        }

        //  Problem 16
        //
        //  Given an array of integers, find the maximum element in every subarray of size k

        public static IEnumerable<int> GetMaximumElements(IEnumerable<int> source, int size)
        {
            int[] sourceArray = source.ToArray();

            if (sourceArray.Length == 0)
            {
                return [];
            }

            List<int> result = [];
            LinkedList<int> max = [];

            int start = 0;
            int end = 0;

            while (end < sourceArray.Length)
            {
                while (max.Count != 0 && sourceArray[end] > max.Last.Value)
                {
                    max.RemoveLast();
                }

                max.AddLast(sourceArray[end]);
                end++;

                if (end < size)
                {
                    continue;
                }

                result.Add(max.First.Value);

                if (sourceArray[start] == max.First.Value)
                {
                    max.RemoveFirst();
                }

                start++;
            }

            return result;
        }

        //  Problem 17
        //
        //  Find the first non-repeating character in a string

        public static char GetFirstUniqueCharacter(string input)
        {
            if(input.Length == 0)
            {
                return '\0';
            }

            return input[0];
        }
    }
}