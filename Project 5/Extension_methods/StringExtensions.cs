using System;

namespace Extension_methods
{
    public static class StringExtensions
    {
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
            int max = 0;
            char maxChar = '\0';

            foreach (char c in source)
            {
                int count = source.Count(e => e == c);

                if (count > max)
                {
                    max = count;
                    maxChar = c;
                }
            }

            return maxChar;
        }
    }
}
