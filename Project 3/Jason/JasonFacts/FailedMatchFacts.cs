using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class FailedMatchFacts
    {
        [Fact]
        public void ShouldReturnFalseAndRemainingText()
        {
            StringView stringView = new StringView("abc", 0);
            FailedMatch match = new FailedMatch(stringView, 2);
            Assert.False(match.Success());
            Assert.Equal('a', match.StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyString()
        {
            StringView stringView = new StringView("", 0);
            FailedMatch match = new FailedMatch(stringView, 2);
            Assert.False(match.Success());
            Assert.Equal('\0', match.StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNull()
        {
            StringView stringView = new StringView(null, 0);
            FailedMatch match = new FailedMatch(stringView, 2);
            Assert.False(match.Success());
            Assert.Equal('\0', match.StringView().Peek());
        }

        [Fact]
        public void ShouldReturnCorrectFailedIndex()
        {
            StringView stringView = new StringView(null, 0);
            FailedMatch match = new FailedMatch(stringView, 2);
            Assert.Equal(2, match.FailedIndex());
        }
    }
}
