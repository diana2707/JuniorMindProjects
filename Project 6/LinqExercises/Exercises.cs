using System.Linq;

namespace LinqExercises
{
    public static class Exercises
    {

        //  Exercise 1
        //
        //  Given a collection return the longest subarray with distinct elements

        public static IEnumerable<int> GetLongestDistinctSubarray(IEnumerable<int> input)
        {
            return input.Distinct();
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
    }
}