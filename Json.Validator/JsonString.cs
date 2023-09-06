using System;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            return !string.IsNullOrEmpty(input) &&
                HasDoubleQuotes(input);
        }

        private static bool HasDoubleQuotes(string input)
        {
            return input[0] == '"' && input[^1] == '"';
        }
    }
}
