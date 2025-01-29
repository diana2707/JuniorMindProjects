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
    }
}
