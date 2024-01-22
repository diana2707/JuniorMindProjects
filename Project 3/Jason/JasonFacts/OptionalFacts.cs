using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class OptionalFacts
    {
        [Fact]
        public void ShouldReturnTrueAndRemainingText()
        {
            var a = new Optional(new Character('a'));
            Assert.True(a.Match("abc").Success());
            Assert.Equal("bc", a.Match("abc").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternRepeats()
        {
            var a = new Optional(new Character('a'));
            Assert.True(a.Match("aabc").Success());
            Assert.Equal("abc", a.Match("aabc").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternDoNotMatch()
        {
            var a = new Optional(new Character('a'));
            Assert.True(a.Match("aabc").Success());
            Assert.Equal("abc", a.Match("aabc").RemainingText());
        }
    }
}
