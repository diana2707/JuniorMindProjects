using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class ManyFacts
    {
        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPrefixMatchesOnce()
        {
            var a = new Many(new Character('a'));
            Assert.True(a.Match("abc").Success());
        }
    }
}
