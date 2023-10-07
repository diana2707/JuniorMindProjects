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
            return HasOnlyDigits(input) && !IntegerPartStartsWith0(input);
        }

        private static bool IntegerPartStartsWith0(string input)
        {
            return input.Length > 1 && input[0] == '0' && char.IsAsciiDigit(input[1]);
        }

        private static bool HasOnlyDigits(string input)
        {
            int counter = 0;
            foreach (char c in input)
            {
                if (c >= '0' && c <= '9')
                {
                    counter++;
                }
            }

            return counter == input.Length;
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

    }
}
