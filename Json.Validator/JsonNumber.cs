using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return double.TryParse(input, out var number) && !StartsWith0(input) && !EndsWithDot(input);
        }

        private static bool EndsWithDot(string input)
        {
            return input.EndsWith(".");
        }

        private static bool StartsWith0(string input)
        {
            return input.Length > 1 && input[0] == '0' && int.TryParse(input[1].ToString(), out int number);
        }
    }
}
