using System;

namespace LinqExercises
{
    public static class Exercises
    {

        //  Exercise 1
        //
        //  Given a collection return the longest subarray with distinct elements

        public static IEnumerable<int> GetLongestDistinctSubarray(IEnumerable<int> input)
        {
            return input.SelectMany((_, start) => Enumerable.Range(start, input.Count() - start).Select(end => input.Skip(start).Take(end - start + 1)))
                .Where(subarray => subarray.Count() == subarray.Distinct().Count())
                .MaxBy(subarray => subarray.Count());
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
            return Enumerable.Range(0, input.Count())
                        .SelectMany(start => Enumerable.Range(start, input.Count() - start)
                            .Select(end => input.Skip(start).Take(end - start + 1)))
                .Where(subarray => subarray.Distinct().Count() == 1 && subarray.First() == 0)
                .MaxBy(subarray => subarray.Count())
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
                .Where(hand => hand.Where(c => char.IsDigit(c)).Distinct().Count() == 2)
                .Aggregate("", (acc, fullHand) => acc + (acc.Length > 0 ? ";" : "") + fullHand);
        }

        //  Exercise 5
        //
        //What day of the week is Christmas day (25th December) on for the next 10 years (starting with 2025)?
        //The answer should be a string (or sequence of strings)

        public static string GetChristmasDays(int years, int startYear)
        {
            return Enumerable.Range(0, years)
                .Select(n => new DateTime(startYear + n, 12, 25).DayOfWeek.ToString())
                .Aggregate("", (acc, day) => acc + (acc.Length > 0 ? ", " : "") + day);
        }

        //  Exercise 6
        //
        //  From the following dictionary of words,
        //  "parts,traps,arts,rats,starts,tarts,rat,art,tar,tars,stars,stray"
        //  return all words that are an anagram of star(no leftover letters allowed).

        public static string SelectAnagramsOf(string input, string dictionary)
        {

            IEnumerable<char> orderedInput = input.Order();

            return dictionary.Split(',').Where(word => word.Trim().Order().SequenceEqual(orderedInput))
                .Aggregate("", (acc, word) => acc + (acc.Length > 0 ? ", " : "") + word);
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

            return input.SelectMany((_, index) => GetAllAnagrams(input.Substring(0, index) + input.Substring(index + 1, input.Length - index - 1))
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
        //Find substrings of a string that are anagram of each other

        public static IEnumerable<IEnumerable<string>> GetSubstringAnagrams(string input)
        {
            return [];
        }
    }
}