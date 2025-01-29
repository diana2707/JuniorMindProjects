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
            foreach (char c in source)
            {
                result = c - '0';
            }

            return result;

            //int index = 0;
            //int sign = 1;

            //if (source[0] == '-' || source[0] == '+')
            //{
            //    if (source[0] == '-')
            //    {
            //        sign = -1;
            //    }

            //    index++;
            //}

            //foreach (char c in source)
            //{
            //    if (!char.IsDigit(c))
            //    {
            //        throw new ArgumentException("The string is not integer");
            //    }
            //}
        }
    }
}
