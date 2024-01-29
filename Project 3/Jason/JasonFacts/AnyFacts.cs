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
    }
}
