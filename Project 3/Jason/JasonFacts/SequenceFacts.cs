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
        public void ShouldReturnFalseAndRemainingTextWhenTheMatchIsPartiallyInvalid()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            const string text = "ax";

            Assert.False(ab.Match(text).Success());
            Assert.Equal("ax", ab.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndRemainingTextWhentheTextIsInvalid()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            const string text = "def";

            Assert.False(ab.Match(text).Success());
            Assert.Equal("def", ab.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseWhenTheTextIsNull()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            const string text = null;

            Assert.False(ab.Match(text).Success());
            Assert.Equal(text, ab.Match(text).RemainingText());
        }
    }
}
