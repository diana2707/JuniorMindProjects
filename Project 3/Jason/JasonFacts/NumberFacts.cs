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
            StringView currentIndex = new ("0", 0);
            StringView failedIndex = new ("0", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNmberStartsWith0()
        {
            var n = new Number();
            StringView currentIndex = new ("09", 0);
            StringView failedIndex = new ("09", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('9', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('9', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenTextIsASingleDigit()
        {
            var n = new Number();
            StringView currentIndex = new ("7", 0);
            StringView failedIndex = new ("7", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenTextIsLetter()
        {
            var n = new Number();
            StringView currentIndex = new ("a", 0);
            StringView failedIndex = new ("a", 0);

            Assert.False(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('a', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenTextHasMultipleDigits()
        {
            var n = new Number();
            StringView currentIndex = new ("50", 0);
            StringView failedIndex = new ("50", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenTextContainsLetters()
        {
            var n = new Number();
            StringView currentIndex = new ("50a3", 0);
            StringView failedIndex = new ("50a3", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('a', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenTextIsNull()
        {
            var n = new Number();
            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);

            Assert.False(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenTextIsEmptyString()
        {
            var n = new Number();
            StringView currentIndex = new ("", 0);
            StringView failedIndex = new ("", 0);

            Assert.False(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenNmberIsNegative()
        {
            var n = new Number();
            StringView currentIndex = new ("-97", 0);
            StringView failedIndex = new ("-97", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenNmberIsNegativeZero()
        {
            var n = new Number();
            StringView currentIndex = new ("-0", 0);
            StringView failedIndex = new ("-0", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNmberIsFractional()
        {
            var n = new Number();
            StringView currentIndex = new ("12.34", 0);
            StringView failedIndex = new ("12.34", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNmberIsFractionalAndHasLeadingZeroes()
        {
            var n = new Number();
            StringView currentIndex = new ("0.00000001", 0);
            StringView failedIndex = new ("0.00000001", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNmberEndsWithDot()
        {
            var n = new Number();
            StringView currentIndex = new ("12.", 0);
            StringView failedIndex = new ("12.", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('.', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNmberHasTwoFractionalParts()
        {
            var n = new Number();
            StringView currentIndex = new ("12.10.45", 0);
            StringView failedIndex = new ("12.10.45", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('.', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('.', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenFractionalPartHasLetters()
        {
            var n = new Number();
            StringView currentIndex = new ("12.10x7", 0);
            StringView failedIndex = new ("12.10x7", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('x', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('x', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenNumberHasExponent()
        {
            var n = new Number();
            StringView currentIndex = new ("12e7", 0);
            StringView failedIndex = new ("12e7", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenExponentStartsWithCapitalE()
        {
            var n = new Number();
            StringView currentIndex = new ("12E7", 0);
            StringView failedIndex = new ("12E7", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenExponentIsPositive()
        {
            var n = new Number();
            StringView currentIndex = new ("12e+7", 0);
            StringView failedIndex = new ("12e+7", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenExponentIsNegative()
        {
            var n = new Number();
            StringView currentIndex = new ("12e-7", 0);
            StringView failedIndex = new ("12e-7", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenNumberIsFractionatAndHasExponent()
        {
            var n = new Number();
            StringView currentIndex = new ("12.5e7", 0);
            StringView failedIndex = new ("12.5e7", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenExponentContainsletters()
        {
            var n = new Number();
            StringView currentIndex = new ("15e3x3", 0);
            StringView failedIndex = new ("15e3x3", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('x', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('x', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNumberHasTwoExponents()
        {
            var n = new Number();
            StringView currentIndex = new ("15e34e25", 0);
            StringView failedIndex = new ("15e34e25", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('e', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('e', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenExponentIsIncomplete()
        {
            var n = new Number();
            StringView currentIndex = new ("15e-", 0);
            StringView failedIndex = new ("15e-", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('e', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenExponentIsBeforeFaraction()
        {
            var n = new Number();
            StringView currentIndex = new ("15e3.3", 0);
            StringView failedIndex = new ("15e3.3", 0);

            Assert.True(n.Match(currentIndex, failedIndex).Success());
            Assert.Equal('.', n.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('.', n.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }
    }
}