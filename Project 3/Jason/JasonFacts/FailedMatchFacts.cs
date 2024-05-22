using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class FailedMatchFacts
    {
        [Fact]
        public void ShouldReturnFalseAndRemainingText()
        {
            StringView currentIndex = new StringView("abc", 0);
            StringView failedIndex = new StringView("abc", 0);
            FailedMatch match = new FailedMatch(currentIndex, failedIndex);
            Assert.False(match.Success());
            Assert.Equal('a', match.CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyString()
        {
            StringView currentIndex = new StringView("", 0);
            StringView failedIndex = new StringView("", 0);
            FailedMatch match = new FailedMatch(currentIndex, failedIndex);
            Assert.False(match.Success());
            Assert.Equal('\0', match.CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNull()
        {
            StringView currentIndex = new StringView(null, 0);
            StringView failedIndex = new StringView(null, 0);
            FailedMatch match = new FailedMatch(currentIndex, failedIndex);
            Assert.False(match.Success());
            Assert.Equal('\0', match.CurrentIndex().Peek());
        }
    }
}
