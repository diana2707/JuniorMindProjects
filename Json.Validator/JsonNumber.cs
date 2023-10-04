using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return double.TryParse(input, out var number) && (input.Length <= 1 || input[0] != '0');
        }
    }
}
