using System;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            return !IsNull(input) &&
                !IsEmpty(input) &&
                HasDoubleQuotes(input) &&
                !HasControlCharacters(input);
        }

        private static bool HasControlCharacters(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsControl(input[i]))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsEmpty(string input)
        {
            return input == string.Empty;
        }

        private static bool IsNull(string input)
        {
            return input == null;
        }

        private static bool HasDoubleQuotes(string input)
        {
            return input[0] == '"' && input[^1] == '"';
        }
    }
}
