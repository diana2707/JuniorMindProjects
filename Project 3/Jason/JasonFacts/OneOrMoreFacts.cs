using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class OneOrMoreFacts
    {
        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenPatternDoesNotMatch()
        {
            var a = new OneOrMore(new Range('0', '9'));
            StringView stringView = new ("bc", 0);
            Assert.False(a.Match(stringView).Success());
            Assert.Equal('b', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenPatternConsumesWholeText()
        {
            var a = new OneOrMore(new Range('0', '9'));
            StringView stringView = new ("123", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('\0', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternIsConsumed()
        {
            var a = new OneOrMore(new Range('0', '9'));
            StringView stringView = new ("12a", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('a', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenTextIsEmptyString()
        {
            var a = new OneOrMore(new Range('0', '9'));
            StringView stringView = new ("", 0);
            Assert.False(a.Match(stringView).Success());
            Assert.Equal('\0', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenTextIsNull()
        {
            var a = new OneOrMore(new Range('0', '9'));
            StringView stringView = new (null, 0);
            Assert.False(a.Match(stringView).Success());
            Assert.Equal('\0', a.Match(stringView).StringView().Peek());
        }
    }
}
