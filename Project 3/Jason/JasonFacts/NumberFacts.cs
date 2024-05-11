using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class NumberFacts
    {
        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenTextIs0()
        {
            var n = new Number();
            StringView stringView = new ("0", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('\0', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNmberStartsWith0()
        {
            var n = new Number();
            StringView stringView = new ("09", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('9', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenTextIsASingleDigit()
        {
            var n = new Number();
            StringView stringView = new ("7", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('\0', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenTextIsLetter()
        {
            var n = new Number();
            StringView stringView = new ("a", 0);
            Assert.False(n.Match(stringView).Success());
            Assert.Equal('a', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenTextHasMultipleDigits()
        {
            var n = new Number();
            StringView stringView = new ("50", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('\0', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenTextContainsLetters()
        {
            var n = new Number();
            StringView stringView = new ("50a3", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('a', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenTextIsNull()
        {
            var n = new Number();
            StringView stringView = new (null, 0);
            Assert.False(n.Match(stringView).Success());
            Assert.Equal('\0', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenTextIsEmptyString()
        {
            var n = new Number();
            StringView stringView = new ("", 0);
            Assert.False(n.Match(stringView).Success());
            Assert.Equal(0, n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenNmberIsNegative()
        {
            var n = new Number();
            StringView stringView = new ("-97", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('\0', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenNmberIsNegativeZero()
        {
            var n = new Number();
            StringView stringView = new ("-0", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('\0', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNmberIsFractional()
        {
            var n = new Number();
            StringView stringView = new ("12.34", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('\0', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNmberIsFractionalAndHasLeadingZeroes()
        {
            var n = new Number();
            StringView stringView = new ("0.00000001", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('\0', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNmberEndsWithDot()
        {
            var n = new Number();
            StringView stringView = new ("12.", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('.', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNmberHasTwoFractionalParts()
        {
            var n = new Number();
            StringView stringView = new ("12.10.45", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('.', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenFractionalPartHasLetters()
        {
            var n = new Number();
            StringView stringView = new ("12.10x7", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('x', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenNumberHasExponent()
        {
            var n = new Number();
            StringView stringView = new ("12e7", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('\0', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenExponentStartsWithCapitalE()
        {
            var n = new Number();
            StringView stringView = new ("12E7", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('\0', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenExponentIsPositive()
        {
            var n = new Number();
            StringView stringView = new ("12e+7", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('\0', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenExponentIsNegative()
        {
            var n = new Number();
            StringView stringView = new ("12e-7", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('\0', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenNumberIsFractionatAndHasExponent()
        {
            var n = new Number();
            StringView stringView = new ("12.5e7", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('\0', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenExponentContainsletters()
        {
            var n = new Number();
            StringView stringView = new ("15e3x3", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('x', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNumberHasTwoExponents()
        {
            var n = new Number();
            StringView stringView = new ("15e34e25", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('e', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenExponentIsIncomplete()
        {
            var n = new Number();
            StringView stringView = new ("15e-", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('e', n.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenExponentIsBeforeFaraction()
        {
            var n = new Number();
            StringView stringView = new ("15e3.3", 0);
            Assert.True(n.Match(stringView).Success());
            Assert.Equal('.', n.Match(stringView).StringView().Peek());
        }
    }
}