using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class SuccessMatchFacts
    {
        [Fact]
        public void ShouldReturnTrueAndRemainingText()
        {
            StringView currentIndex = new StringView("abc", 0);
            StringView failedIndex = new StringView("abc", 0);
            SuccessMatch match = new (currentIndex, failedIndex);
            Assert.True(match.Success());
            Assert.Equal('a', match.CurrentIndex().Peek());
        }
    }
}
