using System;

namespace Extension_methods
{
    public static class StringExtensions
    {
        public static int CountVowels(this string source)
        {
            const string vowels = "aeiouAEIOU";
            return source.Where(e => vowels.Contains(e))
                         .Count();
        }
    }
}
