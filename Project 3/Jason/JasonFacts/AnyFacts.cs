using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class AnyFacts
    {
        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternMatches()
        {
            var e = new Any("eE");
            Assert.True(e.Match("ea").Success());
            Assert.Equal("a", e.Match("ea").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingText()
        {
            var e = new Any("eE");
            Assert.True(e.Match("Ea").Success());
            Assert.Equal("a", e.Match("Ea").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenPatternDoesNotMatch()
        {
            var e = new Any("eE");
            Assert.False(e.Match("a").Success());
            Assert.Equal("a", e.Match("a").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenTextIsEmptyString()
        {
            var e = new Any("eE");
            Assert.False(e.Match("").Success());
            Assert.Equal("", e.Match("").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenTextIsNull()
        {
            var e = new Any("eE");
            Assert.False(e.Match(null).Success());
            Assert.Null(e.Match(null).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenVerifyPositiveSign()
        {
            var sign = new Any("-+");
            Assert.True(sign.Match("+3").Success());
            Assert.Equal("3", sign.Match("+3").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenVerifyNegativeSign()
        {
            var sign = new Any("-+");
            Assert.True(sign.Match("-2").Success());
            Assert.Equal("2", sign.Match("-2").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenPatternDoesNotMatchForSigns()
        {
            var sign = new Any("-+");
            Assert.False(sign.Match("2").Success());
            Assert.Equal("2", sign.Match("2").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenTheTextIsEmptyString()
        {
            var sign = new Any("-+");
            Assert.False(sign.Match("").Success());
            Assert.Equal("", sign.Match("").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenTheTextIsNull()
        {
            var sign = new Any("-+");
            Assert.False(sign.Match(null).Success());
            Assert.Null(sign.Match(null).RemainingText());
        }
    }
}
