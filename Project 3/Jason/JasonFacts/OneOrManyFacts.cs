using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class OneOrManyFacts
    {
        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenPatternDoesNotMatch()
        {
            var a = new OneOrMany(new Range('0', '9'));
            Assert.False(a.Match("bc").Success());
            Assert.Equal("bc", a.Match("bc").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenPatternConsumesWholeText()
        {
            var a = new OneOrMany(new Range('0', '9'));
            Assert.True(a.Match("123").Success());
            Assert.Equal("", a.Match("123").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternIsConsumed()
        {
            var a = new OneOrMany(new Range('0', '9'));
            Assert.True(a.Match("12a").Success());
            Assert.Equal("a", a.Match("12a").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenTextIsEmptyString()
        {
            var a = new OneOrMany(new Range('0', '9'));
            Assert.False(a.Match("").Success());
            Assert.Equal("", a.Match("").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenTextIsNull()
        {
            var a = new OneOrMany(new Range('0', '9'));
            Assert.False(a.Match(null).Success());
            Assert.Null(a.Match(null).RemainingText());
        }

    }
}
