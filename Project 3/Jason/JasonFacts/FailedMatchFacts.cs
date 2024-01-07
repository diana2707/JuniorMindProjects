using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class FailedMatch
    {
        [Fact]
        public void ShouldReturnFalse()
        {
            FailedMatch match = new FailedMatch("ab");
            Assert.Equal("ab", match.RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseWhenSuccessIsFalse()
        {
            Match match = new Match("abc", 'b', 'b');
            Assert.False(match.Success());
        }

        [Fact]
        public void ShouldReturnRemainingText()
        {
            Match match = new Match("abc", 'a', 'a');
            Assert.Equal("bc", match.RemainingText());
        }
    }
}
