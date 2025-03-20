using Xunit.Sdk;

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
            int maxLength = 0;

            return inputArray.SelectMany((_, start) => Enumerable.Range(start, input.Count() - start)
                                    .Select(end => inputArray[start..(end + 1)]))
                             .Last(subarray =>
                             {
                                 int subarrayLength = subarray.Length;

                                 if (subarrayLength != subarray.Distinct().Count()) //can use order or any?
                                 {
                                     return false;
                                 }

                                 if (subarrayLength <= maxLength)
                                 {
                                     return false;
                                 }

                                 maxLength = subarrayLength;
                                 return true;
                             });
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
            return input.GroupBy(word => new string(word.Order().ToArray()));
        }

        //  Exercise 8
        //
        //  Find substrings of a string that are anagram of each other

        public static IEnumerable<IEnumerable<string>> GetSubstringAnagrams(string input)
        {
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
            if (!input.Any())
            {
                return [];
            }

            var inputArray = input.ToArray();

            return inputArray.SelectMany((_, start) => Enumerable.Range(start, inputArray.Length - start)
                                .Select(end => inputArray[start.. (end + 1)]))
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
    }
}