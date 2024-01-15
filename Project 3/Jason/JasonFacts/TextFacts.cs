using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class TextFacts
    {
        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenTextIsTheSameAsPrefix()
        {
            var trueText = new Text("true");
            Assert.True(trueText.Match("true").Success());
            Assert.Equal("", trueText.Match("true").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPrefixIsValid()
        {
            var trueText = new Text("true");
            Assert.True(trueText.Match("trueX").Success());
            Assert.Equal("X", trueText.Match("trueX").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenPrefixIsInvalid()
        {
            var trueText = new Text("true");
            Assert.False(trueText.Match("false").Success());
            Assert.Equal("false", trueText.Match("false").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenStringIsEmptyString()
        {
            var trueText = new Text("true");
            Assert.False(trueText.Match("").Success());
            Assert.Equal("", trueText.Match("").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenStringIsNull()
        {
            var trueText = new Text("true");
            Assert.False(trueText.Match(null).Success());
            Assert.Null(trueText.Match(null).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyString()
        {
            var falseText = new Text("false");
            Assert.True(falseText.Match("false").Success());
            Assert.Equal("", falseText.Match("false").RemainingText());
        }
    }
}
