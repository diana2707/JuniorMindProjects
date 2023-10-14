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

            if (exponent.Length == ExponentIndex)
            {
                return char.IsAsciiDigit(exponent[1]);
            }

            return HasOnlyDigits(exponent[ExponentIndex..]) && (exponent[1..].StartsWith('-') || exponent[1..].StartsWith('+') || char.IsAsciiDigit(exponent[1]));
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
            if (integer.StartsWith("-"))
            {
                return HasOnlyDigits(integer[1..]);
            }

            return HasOnlyDigits(integer) && !IntegerPartStartsWith0(integer);
        }

        private static bool IntegerPartStartsWith0(string integer)
        {
            return integer.StartsWith("0") && integer.Length > 1;
        }

        private static bool HasOnlyDigits(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!(input[i] >= '0' && input[i] <= '9'))
                {
                    return false;
                }
            }

            return input.Length != 0;
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
