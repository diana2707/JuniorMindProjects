using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class OptionalFacts
    {
        [Fact]
        public void ShouldreturnTrueAndRemainingText()
        {
            var a = new Optional(new Character('a'));
            Assert.True(a.Match("abc").Success());
            Assert.Equal("bc", a.Match("abc").RemainingText());
        }
    }
}
