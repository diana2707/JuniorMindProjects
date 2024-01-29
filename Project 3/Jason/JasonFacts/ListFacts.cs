using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class ListFacts
    {
        [Fact]
        public void ShouldReturnTrueAndRemainigText()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            Assert.True(a.Match("1,2,3").Success());
            Assert.Equal("", a.Match("1,2,3").RemainingText());
        }
    }
}
