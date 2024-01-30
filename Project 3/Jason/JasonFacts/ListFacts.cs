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

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenThePatternDoesNotMatch()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            Assert.True(a.Match("abc").Success());
            Assert.Equal("abc", a.Match("abc").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenTheTextIsEmptyString()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            Assert.True(a.Match("").Success());
            Assert.Equal("", a.Match("").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndNullWhenTheTextIsNull()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            Assert.True(a.Match(null).Success());
            Assert.Null(a.Match(null).RemainingText());
        }
    }
}
