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
            int sign = 1;
            int index = 0;

            if (source[0] == '-' || source[0] == '+')
            {
                if (source[0] == '-')
                {
                    sign = -1;
                }

                index++;
            }

            for (int i = index; i < source.Length; i++)
            {
                if (!char.IsDigit(source[i]))
                {
                    throw new ArgumentException("The string is not integer");
                }
            }

            int orders = source.Length - index;

            for (int i = index; i < source.Length; i++)
            {
                int digit = source[i] - '0';
                double order = Math.Pow(10, --orders);
                result += digit * (int)order;
            }

            return result * sign;
        }
    }
}
