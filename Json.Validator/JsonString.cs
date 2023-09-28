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
            char[] validHexValues = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'A', 'B', 'C', 'D', 'E', 'F' };
            foreach (char hexValue in validHexValues)
            {
                if (value == hexValue)
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
