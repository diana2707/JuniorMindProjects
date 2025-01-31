using System;

namespace Extension_methods
{
    public static class StringExtensions
    {
        //refa metodele sa nu fie extensii de string
        public static int CountVowels(this string source)
        {
            const string vowels = "aeiouAEIOU";
            return source.Count(e => vowels.Contains(e));
        }

        public static int CountConsonants(this string source)
        {
            return source.Length - source.CountVowels();
        }

        public static char FirstUnique(this string source)
        {
            return source.Where(e => source.Count(x => x == e) == 1).First();
        }

        public static int ConvertToInt(this string source)
        {
            int result = 0;
            int sign = source[0] == '-' ? -1 : 1;

            IEnumerable<char> number = source[0] == '-' || source[0] == '+' ? source.Skip(1) : source;
            int position = number.Count();

            if (number.Any(e => !char.IsDigit(e)))
            {
                throw new ArgumentException("The string is not integer");
            }

            result = number.Aggregate(0, (result, e) => result + ((e - '0') * (int)Math.Pow(10, --position)));

            return result * sign;
        }

        public static char MostFrequentChar(this string source)
        {
            return source.GroupBy(c => c)
                .MaxBy(e => e.Count())
                .Key;
        }

        public static List<IEnumerable<char>> GetAllPalindroms(this string source)
        {
            return Enumerable.Range(0, source.Length)
                .SelectMany(i => Enumerable.Range(1, source.Length - i)
                                  .Select(j => source.Skip(i).Take(j)))
                .Where(e => IsPalindrom(e))
                .ToList();
        }

        private static bool IsPalindrom(IEnumerable<char> source)
        {
            return source.SequenceEqual(source.Reverse());
        }

        public static List<List<int>> GetSubarrays(this List<int> source, int maxSum)
        {
            return Enumerable.Range(0, source.Count)
                .SelectMany(i => Enumerable.Range(1, source.Count - i)
                           .Select(j => source.Skip(i).Take(j)))
                .Where(e => e.Aggregate(0, (sum, e) => sum + e) <= maxSum)
                .Select(e => e.ToList())
                .ToList();
        }

        public static List<string> GetSignCombinations(int num)
        {
            return Enumerable.Range(0, (int)Math.Pow(2, num))
                .Select(e => Convert.ToString(e, 2).PadLeft(num))
                .Select(e => Enumerable.Range(1, num + 1)
                       .Zip(e, (n, k) => k == '0' ? $"-{n}" : $"+{n}")
                           .Aggregate((combination, elm) => combination += elm))
                .ToList();
        }

        public static List<List<int>> GetPythagoreanTriplets(List<int> source)
        {
            source = source.Order().ToList();

            return Enumerable.Range(0, source.Count - 2)
                .SelectMany(i => Enumerable.Range(i + 1, source.Count - i - 1)
                    .SelectMany(j => Enumerable.Range(j + 1, source.Count - j - 1)
                        .Select(k => new List<int> { source[i], source[j], source[k] })))
                .Where(comb => (int)Math.Pow(comb[0], 2) + (int)Math.Pow(comb[1], 2) == (int)Math.Pow(comb[2], 2))
                .ToList();
        }
    }
}
