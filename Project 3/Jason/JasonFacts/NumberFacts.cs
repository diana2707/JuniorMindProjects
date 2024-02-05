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
        public void ShouldReturnFalseAndWholeTextWhenNmberStartsWith0()
        {
            var n = new Number();
            Assert.False(n.Match("09").Success());
            Assert.Equal("09", n.Match("09").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenNmberIsNegative()
        {
            var n = new Number();
            Assert.True(n.Match("-97").Success());
            Assert.Equal("", n.Match("-97").RemainingText());
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
        public void ShouldReturnFalseAndWholeTextWhenNmberIsFractionalAndEndsWithDot()
        {
            var n = new Number();
            Assert.False(n.Match("12.").Success());
            Assert.Equal("12.", n.Match("12.").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenNmberHasTwoFractionalParts()
        {
            var n = new Number();
            Assert.False(n.Match("12.10.45").Success());
            Assert.Equal("12.10.45.", n.Match("12.10.45.").RemainingText());
        }
    }




    //[Fact]
    //public void ShouldReturnFalseAndWholeTextWhenTextHasMultipleDigitsAndLetters()
    //{
    //    var n = new Number();
    //    Assert.False(n.Match("5467rd").Success());
    //    //Assert.Equal("5467rd", n.Match("5467rd").RemainingText());
    //}
    //verify if match method from class Number returns false and whole text when text has multiple digits and letters

    


    //[Fact]
    //public void DoesNotStartWithZero()
    //{
    //    Assert.False(IsJsonNumber("07"));
    //}

    //[Fact]
    //public void CanBeNegative()
    //{
    //    Assert.True(IsJsonNumber("-26"));
    //}

    //[Fact]
    //public void CanBeMinusZero()
    //{
    //    Assert.True(IsJsonNumber("-0"));
    //}

    //[Fact]
    //public void CanBeFractional()
    //{
    //    Assert.True(IsJsonNumber("12.34"));
    //}

    //[Fact]
    //public void TheFractionCanHaveLeadingZeros()
    //{
    //    Assert.True(IsJsonNumber("0.00000001"));
    //    Assert.True(IsJsonNumber("10.00000001"));
    //}

    //[Fact]
    //public void DoesNotEndWithADot()
    //{
    //    Assert.False(IsJsonNumber("12."));
    //}

    //[Fact]
    //public void DoesNotHaveTwoFractionParts()
    //{
    //    Assert.False(IsJsonNumber("12.34.56"));
    //}

    //[Fact]
    //public void TheDecimalPartDoesNotAllowLetters()
    //{
    //    Assert.False(IsJsonNumber("12.3x"));
    //}

    //[Fact]
    //public void CanHaveAnExponent()
    //{
    //    Assert.True(IsJsonNumber("12e3"));
    //}

    //[Fact]
    //public void TheExponentCanStartWithCapitalE()
    //{
    //    Assert.True(IsJsonNumber("12E3"));
    //}

    //[Fact]
    //public void TheExponentCanHavePositive()
    //{
    //    Assert.True(IsJsonNumber("12e+3"));
    //}

    //[Fact]
    //public void TheExponentCanBeNegative()
    //{
    //    Assert.True(IsJsonNumber("61e-9"));
    //}

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