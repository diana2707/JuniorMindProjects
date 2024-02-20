using Xunit;

namespace Jason.JasonFacts
{
    public class ValueFacts
    {
        [Fact]
        public void ShouldReturnTrueAndRemainingText()
        {
            var value = new Value();

            Assert.True(value.Match())
        }
    }
}
