using Xunit;

namespace Jason.JasonFacts
{
    public class RangeFacts
    {
        [Fact]
        public void MatchShouldReturnFalseWhenStringIsNull()
        {
            Range digit = new ('a', 'f');
            const string? text = null;
            Assert.False(digit.Match(text));
        }

        [Fact]
        public void MatchShouldReturnFalseWhenStringIsEmpty()
        {
            Range digit = new ('a', 'f');
            const string text = "";
            Assert.False(digit.Match(text));
        }

        [Fact]
        public void MatchShouldReturnFalseWhenStringContainsInvalidCharacters()
        {
            Range digit = new ('a', 'f');
            const string text = "1acfd";
            Assert.False(digit.Match(text));
        }

        [Fact]
        public void MatchShouldReturnTrueWhenStringContainsValidCharacters()
        {
            Range digit = new ('a', 'f');
            const string text = "acfdb";
            Assert.True(digit.Match(text));
        }
    }
}
