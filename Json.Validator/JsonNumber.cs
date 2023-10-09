using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return HasContent(input) && (IsInteger(input) || IsFractional(input) || IsExponential(input));
        }

        private static bool IsExponential(string input)
        {
            input = input.ToLower();
            string[] exponentialNumber = input.Split('e');
            if (exponentialNumber.Length != 2)
            {
                return false;
            }

            return (HasOnlyDigits(exponentialNumber[0]) || IsFractional(exponentialNumber[0])) && HasOnlyDigits(exponentialNumber[1]);
        }

        private static bool IsFractional(string input)
        {
            string[] fractionalNumber = input.Split('.');

            if (fractionalNumber.Length != 2 || fractionalNumber[0].Length == 0 || fractionalNumber[1].Length == 0)
            {
                return false;
            }

            return HasOnlyDigits(fractionalNumber[0]) && HasOnlyDigits(fractionalNumber[1]);
        }

        private static bool IsInteger(string input)
        {
            if (input.Length > 1)
            {
                string substring = input[1..];
                return HasOnlyDigits(substring) && !IntegerPartStartsWith0(input) && CanBeNegative(input[0]);
            }

            return HasOnlyDigits(input);
        }

        private static bool CanBeNegative(char firstChar)
        {
            return char.IsAsciiDigit(firstChar) || firstChar == '-';
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

            return counter == input.Length;
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }
    }
}
