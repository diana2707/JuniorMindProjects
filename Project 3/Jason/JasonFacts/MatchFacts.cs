using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class MatchFacts
    {
        [Fact]
        public void ShouldReturnTrueWhenSuccessIsTrue()
        {
            Match match = new Match("abc", true);
            Assert.True(match.Success());
        }

        [Fact]
        public void ShouldReturnFalseWhenSuccessIsFalse()
        {
            Match match = new Match("abc", false);
            Assert.False(match.Success());
        }

        [Fact]
        public void ShouldReturnRemainingText()
        {
            Match match = new Match("abc", true);
            Assert.Equal("bc", match.RemainingText());
        }
    }
}
