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
    }
}
