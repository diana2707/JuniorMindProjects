using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class SuccessMatchFacts
    {
        [Fact]
        public void ShouldReturnTrueAndRemainingText()
        {
            SuccessMatch match = new SuccessMatch("abc");
            Assert.True(match.Success());
            Assert.Equal("abc", match.RemainingText());
        }
    }
}
