using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class ValueFacts
    {
        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsString()
        {
            var value = new Value();

            Assert.True(value.Match(" \"abc\" ").Success());
            Assert.Equal("", value.Match(" \"abc\" ").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsNumber()
        {
            var value = new Value();

            Assert.True(value.Match(" 12 ").Success());
            Assert.Equal("", value.Match(" 12 ").RemainingText());
        }
    }
}
