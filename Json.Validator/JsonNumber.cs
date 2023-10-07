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
            return !HasLetters(input) && !IntegerPartStartsWith0(input);
        }

        private static bool IntegerPartStartsWith0(string input)
        {
            return input.Length > 1 && input[0] == '0' && char.IsAsciiDigit(input[1]);
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

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

    }
}
