using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return HasContent(input) && IsInteger(input);
        }

        private static bool IsInteger(string input)
        {
            return !HasLetters(input);
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        private static bool HasLetters(string input)
        {
            foreach (char c in input)
            {
                if (c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z')
                {
                    return true;
                }
            }

            return false;
        }
    }
}
