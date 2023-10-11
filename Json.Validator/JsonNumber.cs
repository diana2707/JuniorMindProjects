using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            if (!HasContent(input))
            {
                return false;
            }

            input = input.ToLower();
            int dotIndex = input.IndexOf('.');
            int exponentIndex = input.IndexOf('e');

            return IsInteger(Integer(input, dotIndex, exponentIndex))
                && IsFraction(Fraction(input, dotIndex, exponentIndex))
                && IsExponent(Exponent(input, exponentIndex));
        }

        private static bool IsExponent(string exponent)
        {
            const int ExponentIndex = 2;

            if (exponent == "")
            {
                return true;
            }

            if (exponent.Length == 1)
            {
                return false;
            }

            return exponent.Length == ExponentIndex ? char.IsAsciiDigit(exponent[1]) : HasOnlyDigits(exponent[ExponentIndex..]) && (CanBePositive(exponent[1]) || CanBeNegative(exponent[1]));
        }

        private static string Exponent(string input, int exponentIndex)
        {
            return exponentIndex == -1 ? "" : input[exponentIndex..];
        }

        private static bool IsFraction(string fraction)
        {
            if (fraction == "")
            {
                return true;
            }

            return fraction.StartsWith('.') && HasOnlyDigits(fraction[1..]);
        }

        private static string Fraction(string input, int dotIndex, int exponentIndex)
        {
            if (dotIndex == -1)
            {
                return string.Empty;
            }

            if (exponentIndex == -1)
            {
                return input[dotIndex..];
            }

            return input[dotIndex..exponentIndex];
        }

        private static bool IsInteger(string integer)
        {
            if (integer.Length > 1)
            {
                string substring = integer[1..];
                return HasOnlyDigits(substring) && !IntegerPartStartsWith0(integer) && CanBeNegative(integer[0]);
            }

            return HasOnlyDigits(integer);
        }

        private static bool CanBeNegative(char firstChar)
        {
            return char.IsAsciiDigit(firstChar) || firstChar == '-';
        }

        private static bool CanBePositive(char firstChar)
        {
            return char.IsAsciiDigit(firstChar) || firstChar == '+';
        }

        private static bool IntegerPartStartsWith0(string input)
        {
            return input[0] == '0' && char.IsAsciiDigit(input[1]);
        }

        private static bool HasOnlyDigits(string input)
        {
            int counter = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] >= '0' && input[i] <= '9')
                {
                    counter++;
                }
            }

            return counter == input.Length && input != "";
        }

        private static string Integer(string input, int dotIndex, int exponentIndex)
        {
            if (dotIndex == -1 && exponentIndex == -1)
            {
                return input;
            }

            if (dotIndex == -1)
            {
                return input[..exponentIndex];
            }

            return input[..dotIndex];
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }
    }
}
