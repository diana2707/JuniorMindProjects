using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class FailedMatchFacts
    {
        [Fact]
        public void ShouldReturnFalseAndRemainingText()
        {
            FailedMatch match = new FailedMatch("abc");
            Assert.False(match.Success());
            Assert.Equal("abc", match.RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyString()
        {
            FailedMatch match = new FailedMatch("");
            Assert.False(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndNull()
        {
            FailedMatch match = new FailedMatch(null);
            Assert.False(match.Success());
            Assert.Null(match.RemainingText());
        }
    }
}
