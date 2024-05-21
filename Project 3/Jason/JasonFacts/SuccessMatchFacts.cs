using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class SuccessMatchFacts
    {
        [Fact]
        public void ShouldReturnTrueAndRemainingText()
        {
            StringView stringView = new StringView("abc", 0);
            SuccessMatch match = new (stringView, stringView);
            Assert.True(match.Success());
            Assert.Equal('a', match.StringView().Peek());
        }
    }
}
