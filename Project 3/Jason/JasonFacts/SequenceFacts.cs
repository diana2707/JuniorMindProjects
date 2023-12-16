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
        public void ShouldReturnTrueAndRemainingTex()
        {
            var ab = new Sequence(
                new Range('a', 'g'),
                new Range('a', 'g'));

            const string text = "abcd";

            Assert.True(ab.Match(text).Success());
            Assert.Equal("cd", ab.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTexjhhjg()
        {
            var x = new Sequence(
                new Character('a'),
                new Character('b'));

            var ab = new Sequence(
                new Range('a', 'g'),
                new Range('a', 'g'),
                x);

            const string text = "abcd";

            Assert.True(ab.Match(text).Success());
            Assert.Equal("cd", ab.Match(text).RemainingText());
        }

    }
}
