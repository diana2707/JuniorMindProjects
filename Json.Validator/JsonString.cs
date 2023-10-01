using Newtonsoft.Json.Linq;
using System;

namespace Json
{
    public static class JsonString
    {
        const int HexNumberIndex = 5;

        public static bool IsJsonString(string input)
        {
            return HasContent(input) &&
                !HasControlCharacters(input) &&
                !HasUnrecognizedEscapedSequences(input);
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input) &&
                HasDoubleQuotes(input);
        }

        private static bool HasUnrecognizedEscapedSequences(string input)
        {
            if (ContainsUnrecognizedEscapedCharacters(input))
            {
                return true;
            }

            return input.Contains(@"\u") && ContainsUnfinishedHexNumbers(input);
        }

        private static bool ContainsUnrecognizedEscapedCharacters(string input)
        {
            string[] recognisedEscapedCharacters = { "\\\\", "\\\"", "\\\'", "\\/", "\\b", "\\f", "\\n", "\\r", "\\t", "\\u" };
            const string EscapedSolidus = "\\";

            int indexEscapedSolidus = input.IndexOf(EscapedSolidus);

            while (indexEscapedSolidus != -1)
            {
                bool isRecognised = false;
                string substring = "";

                if (EscapedSolidus.Length + 1 > input.Length - 1 - indexEscapedSolidus)
                {
                    return true;
                }

                for (int i = 0; i < recognisedEscapedCharacters.Length; i++)
                {
                    substring = input.Substring(indexEscapedSolidus, recognisedEscapedCharacters[i].Length);

                    if (substring.Equals(recognisedEscapedCharacters[i]))
                    {
                        isRecognised = true;
                        break;
                    }
                }

                if (!isRecognised)
                {
                    return true;
                }

                indexEscapedSolidus = input.IndexOf(EscapedSolidus, indexEscapedSolidus + substring.Length);
            }

            return false;
        }

        private static bool ContainsUnfinishedHexNumbers(string input)
        {
            if (!CanContainHexNumber(input))
            {
                return true;
            }

            for (int i = 0; i < input.Length - HexNumberIndex; i++)
            {
                if (input[i] == '\\' && input[i + 1] == 'u')
                {
                    int counter = 0;
                    int j = 0;
                    int k = i + 1;
                    while (j < HexNumberIndex - 1)
                    {
                        counter = IsHexValue(input[k + 1]) ? counter + 1 : counter;
                        j++;
                        k++;
                    }

                    if (counter < HexNumberIndex - 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool CanContainHexNumber(string input)
        {
            return input.Length > HexNumberIndex && input.LastIndexOf('u') <= input.Length - HexNumberIndex;
        }

        private static bool IsHexValue(char value)
        {
            return char.IsAsciiDigit(value) || IsHexValidLetter(value);
        }

        private static bool IsHexValidLetter(char value)
        {
            const int LowerLimitForUppercase = 64;
            const int UpperLimitForUppercase = 71;
            const int LowerLimitForLowercase = 96;
            const int UpperLimitForLowercase = 103;

            return value > LowerLimitForUppercase && value < UpperLimitForUppercase || value > LowerLimitForLowercase && value < UpperLimitForLowercase;
        }

        private static bool HasControlCharacters(string input)
        {
            const int UpperLimitForControlChars = 32;

            foreach (char c in input)
            {
                if (c >= 0 && c < UpperLimitForControlChars)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool HasDoubleQuotes(string input)
        {
            return input[0] == '"' && input[^1] == '"' && input.Length > 1;
        }
    }
}
