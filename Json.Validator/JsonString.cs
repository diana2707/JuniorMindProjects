using System;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            return !string.IsNullOrEmpty(input) &&
                HasDoubleQuotes(input) &&
                !HasControlCharacters(input) &&
                !ContainUnrecognizedEscapedCharacters(input);
        }

        private static bool ContainUnrecognizedEscapedCharacters(string input)
        {
            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == '\\' && !char.IsWhiteSpace(input[i + 1]))
                {
                    return true;
                }
            }

            return false;
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

        private static bool HasDoubleQuotes(string input)
        {
            return input[0] == '"' && input[^1] == '"';
        }
    }
}
