using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class SuccessMatch
    {
        [Fact]
        public void ShouldReturnTrueWhenSuccessIsTrue()
        {
            Match match = new Match("abc", 'a', 'a');
            Assert.True(match.Success());
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
