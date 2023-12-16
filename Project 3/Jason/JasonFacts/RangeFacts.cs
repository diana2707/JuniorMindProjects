using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class RangeFacts
    {
        [Fact]
        public void MatchShouldReturnFalseWhenStringIsNull()
        {
            Range digit = new ('a', 'f');
            const string? text = "";

            Assert.False(digit.Match(text).Success());
            Assert.Equal("", digit.Match(text).RemainingText());
        }

        [Fact]
        public void MatchShouldReturnFalseWhenStringIsEmpty()
        {
            Range digit = new ('a', 'f');
            const string text = "";

            Match match = new (text, false);

            Assert.False(digit.Match(text).Success());
            Assert.Equal(text, digit.Match(text).RemainingText());
        }

        [Fact]
        public void MatchShouldReturnFalseWhenStringStartsWithInvalidCharacters()
        {
            Range digit = new ('a', 'f');
            const string text = "1acfd";
            Match match = new (text, false);

            Assert.False(digit.Match(text).Success());
            Assert.Equal(text, digit.Match(text).RemainingText());
        }

        [Fact]
        public void MatchShouldReturnTrueWhenStringStartsWithValidCharacters()
        {
            Range digit = new ('a', 'f');
            const string text = "acfdb";
            Match match = new (text, true);

            Assert.True(digit.Match(text).Success());
            Assert.Equal("cfdb", digit.Match(text).RemainingText());
        }
    }
}
