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
            Assert.True(n.Match("0").Success());
            Assert.Equal("", n.Match("0").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNmberStartsWith0()
        {
            var n = new Number();
            Assert.True(n.Match("09").Success());
            Assert.Equal("9", n.Match("09").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenTextIsASingleDigit()
        {
            var n = new Number();
            Assert.True(n.Match("7").Success());
            Assert.Equal("", n.Match("7").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenTextIsLetter()
        {
            var n = new Number();
            Assert.False(n.Match("a").Success());
            Assert.Equal("a", n.Match("a").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenTextHasMultipleDigits()
        {
            var n = new Number();
            Assert.True(n.Match("50").Success());
            Assert.Equal("", n.Match("50").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenTextContainsLetters()
        {
            var n = new Number();
            Assert.True(n.Match("50a3").Success());
            Assert.Equal("a3", n.Match("50a3").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenTextIsNull()
        {
            var n = new Number();
            Assert.False(n.Match(null).Success());
            Assert.Null(n.Match(null).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenTextIsEmptyString()
        {
            var n = new Number();
            Assert.False(n.Match("").Success());
            Assert.Equal("", n.Match("").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenNmberIsNegative()
        {
            var n = new Number();
            Assert.True(n.Match("-97").Success());
            Assert.Equal("", n.Match("-97").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenNmberIsNegativeZero()
        {
            var n = new Number();
            Assert.True(n.Match("-0").Success());
            Assert.Equal("", n.Match("-0").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNmberIsFractional()
        {
            var n = new Number();
            Assert.True(n.Match("12.34").Success());
            Assert.Equal("", n.Match("12.34").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNmberIsFractionalAndHasLeadingZeroes()
        {
            var n = new Number();
            Assert.True(n.Match("0.00000001").Success());
            Assert.Equal("", n.Match("0.00000001").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNmberEndsWithDot()
        {
            var n = new Number();
            Assert.True(n.Match("12.").Success());
            Assert.Equal(".", n.Match("12.").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNmberHasTwoFractionalParts()
        {
            var n = new Number();
            Assert.True(n.Match("12.10.45").Success());
            Assert.Equal(".45", n.Match("12.10.45").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenFractionalPartHasLetters()
        {
            var n = new Number();
            Assert.True(n.Match("12.10x7").Success());
            Assert.Equal("x7", n.Match("12.10x7").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenNumberHasExponent()
        {
            var n = new Number();
            Assert.True(n.Match("12e7").Success());
            Assert.Equal("", n.Match("12e7").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenExponentStartsWithCapitalE()
        {
            var n = new Number();
            Assert.True(n.Match("12E7").Success());
            Assert.Equal("", n.Match("12E7").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenExponentIsPositive()
        {
            var n = new Number();
            Assert.True(n.Match("12e+7").Success());
            Assert.Equal("", n.Match("12e+7").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenExponentIsNegative()
        {
            var n = new Number();
            Assert.True(n.Match("12e-7").Success());
            Assert.Equal("", n.Match("12e-7").RemainingText());
        }


    }



   

    //[Fact]
    //public void CanHaveFractionAndExponent()
    //{
    //    Assert.True(IsJsonNumber("12.34E3"));
    //}

    //[Fact]
    //public void TheExponentDoesNotAllowLetters()
    //{
    //    Assert.False(IsJsonNumber("22e3x3"));
    //}

    //[Fact]
    //public void DoesNotHaveTwoExponents()
    //{
    //    Assert.False(IsJsonNumber("22e323e33"));
    //}

    //[Fact]
    //public void TheExponentIsAlwaysComplete()
    //{
    //    Assert.False(IsJsonNumber("22e"));
    //    Assert.False(IsJsonNumber("22e+"));
    //    Assert.False(IsJsonNumber("23E-"));
    //}

    //[Fact]
    //public void TheExponentIsAfterTheFraction()
    //{
    //    Assert.False(IsJsonNumber("22e3.3"));
    //}

    //[Fact]
    //public void TheExponentStartWithSymbol()
    //{
    //    Assert.False(IsJsonNumber("22e&5"));
    //}
}