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
            const string? text = null;

            Match match = new (text, false);

            Assert.Equivalent(match, digit.Match(text));
        }

        [Fact]
        public void MatchShouldReturnFalseWhenStringIsEmpty()
        {
            Range digit = new ('a', 'f');
            const string text = "";

            Match match = new (text, false);

            Assert.Equivalent(match, digit.Match(text));
        }

        [Fact]
        public void MatchShouldReturnFalseWhenStringStartsWithInvalidCharacters()
        {
            Range digit = new ('a', 'f');
            const string text = "1acfd";
            Match match = new (text, false);

            Assert.Equivalent(match, digit.Match(text));
        }

        [Fact]
        public void MatchShouldReturnTrueWhenStringStartsWithValidCharacters()
        {
            Range digit = new ('a', 'f');
            const string text = "acfdb";
            Match match = new (text, true);

            Assert.Equivalent(match, digit.Match(text));
        }
    }
}
