using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class SequenceFacts
    {
        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenTheMatchIsValid()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            const string text = "abcd";

            Assert.True(ab.Match(text).Success());
            Assert.Equal("cd", ab.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndRemainingTextWhenTheMatchIsInvalid()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            const string text = "ax";

            Assert.False(ab.Match(text).Success());
            Assert.Equal("ax", ab.Match(text).RemainingText());
        }

    }
}
