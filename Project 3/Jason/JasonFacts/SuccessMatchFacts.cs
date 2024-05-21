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
            SuccessMatch match = new (stringView, 2);
            Assert.True(match.Success());
            Assert.Equal('a', match.StringView().Peek());
        }

        [Fact]
        public void ShouldReturnCorrectFailedIndex()
        {
            StringView stringView = new StringView("abc", 0);
            SuccessMatch match = new (stringView, 2);
            Assert.Equal(2, match.FailedIndex());
        }
    }
}
