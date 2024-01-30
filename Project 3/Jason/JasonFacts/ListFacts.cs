using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class ListFacts
    {
        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenPatternsConsumesWholeText()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            Assert.True(a.Match("1,2,3").Success());
            Assert.Equal("", a.Match("1,2,3").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndSeparatorWhenTextEndsWithSeparator()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            Assert.True(a.Match("1,2,3,").Success());
            Assert.Equal(",", a.Match("1,2,3,").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemaininText()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            Assert.True(a.Match("1a").Success());
            Assert.Equal("a", a.Match("1a").RemainingText());
        }
    }
}
