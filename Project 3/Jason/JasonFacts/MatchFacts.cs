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
    }
}
