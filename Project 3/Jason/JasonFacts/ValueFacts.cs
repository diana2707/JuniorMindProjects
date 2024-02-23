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

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsEmptyArray()
        {
            var value = new Value();

            Assert.True(value.Match(" [ ] ").Success());
            Assert.Equal("", value.Match(" [ ] ").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsAnArrayOfNumbers()
        {
            var value = new Value();

            Assert.True(value.Match(" [ 1 , 2 , 3 ] ").Success());
            Assert.Equal("", value.Match(" [ 1 , 2 , 3 ] ").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsAnArrayOfStrings()
        {
            var value = new Value();

            Assert.True(value.Match(" [ \"a\" , \"b\" , \"c\" ] ").Success());
            Assert.Equal("", value.Match(" [ \"a\" , \"b\" , \"c\" ] ").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsAnArrayOfBoolean()
        {
            var value = new Value();

            Assert.True(value.Match(" [ true ] ").Success());
            Assert.Equal("", value.Match(" [ true ] ").RemainingText());
        }
    }
}
