using Microsoft.CodeAnalysis;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Json
{
    public static partial class JsonString
    {
        public static bool IsJsonString(string input)
        {
            return !string.IsNullOrEmpty(input) &&
                HasValidCharacterPositioning(input) &&
                HasValidCharacterSequences(input);
        }

        private static bool HasValidCharacterPositioning(string input)
        {
            return HasDoubleQuotes(input) &&
                !EndsWithReversedSolidus(input);
        }

        private static bool HasValidCharacterSequences(string input)
        {
            return !HasControlCharacters(input) &&
               !HasUnrecognizedEscapedSequences(input);
        }

        private static bool EndsWithReversedSolidus(string input)
        {
            const int PenultimateCharIndex = 2;
            return input[^PenultimateCharIndex] == '\\';
        }

        private static bool HasUnrecognizedEscapedSequences(string input)
        {
            MatchCollection recognisedMatches = RecognisedPattern().Matches(input);
            MatchCollection allMatches = GeneralPattern().Matches(input);

            return recognisedMatches.Count != allMatches.Count;
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

        [GeneratedRegex("\\\\[\\\'\\\"\\\\/bfnrt]|u[0-9A-Fa-f]{4}")]
        private static partial Regex RecognisedPattern();

        [GeneratedRegex("\\\\.+?")]
        private static partial Regex GeneralPattern();
    }
}
