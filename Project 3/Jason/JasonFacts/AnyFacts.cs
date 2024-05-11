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
            StringView stringView = new ("ea", 0);
            Assert.True(e.Match(stringView).Success());
            Assert.Equal('a', e.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingText()
        {
            var e = new Any("eE");
            StringView stringView = new ("Ea", 0);
            Assert.True(e.Match(stringView).Success());
            Assert.Equal('a', e.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenPatternDoesNotMatch()
        {
            var e = new Any("eE");
            StringView stringView = new ("a", 0);
            Assert.False(e.Match(stringView).Success());
            Assert.Equal('a', e.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenTextIsEmptyString()
        {
            var e = new Any("eE");
            StringView stringView = new ("", 0);
            Assert.False(e.Match(stringView).Success());
            Assert.Equal('\0', e.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenTextIsNull()
        {
            var e = new Any("eE");
            StringView stringView = new (null, 0);
            Assert.False(e.Match(stringView).Success());
            Assert.Equal('\0', e.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenVerifyPositiveSign()
        {
            var sign = new Any("-+");
            StringView stringView = new ("+3", 0);
            Assert.True(sign.Match(stringView).Success());
            Assert.Equal('3', sign.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenVerifyNegativeSign()
        {
            var sign = new Any("-+");
            StringView stringView = new ("-2", 0);
            Assert.True(sign.Match(stringView).Success());
            Assert.Equal('2', sign.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenPatternDoesNotMatchForSigns()
        {
            var sign = new Any("-+");
            StringView stringView = new ("2", 0);
            Assert.False(sign.Match(stringView).Success());
            Assert.Equal('2', sign.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenTheTextIsEmptyString()
        {
            var sign = new Any("-+");
            StringView stringView = new ("", 0);
            Assert.False(sign.Match(stringView).Success());
            Assert.Equal('\0', sign.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenTheTextIsNull()
        {
            var sign = new Any("-+");
            StringView stringView = new (null, 0);
            Assert.False(sign.Match(stringView).Success());
            Assert.Equal('\0', sign.Match(stringView).StringView().Peek());
        }
    }
}
