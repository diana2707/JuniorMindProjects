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
        public void ShouldReturnFalseAndRemainingTextWhenTheStringIsInvalid()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            const string text = "def";

            Assert.False(ab.Match(text).Success());
            Assert.Equal("def", ab.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseWhenTheStringIsNull()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            const string text = null;

            Assert.False(ab.Match(text).Success());
            Assert.Equal(text, ab.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseWhenTheStringIsEmpty()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            const string text = "";

            Assert.False(ab.Match(text).Success());
            Assert.Equal(text, ab.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueWhenSequenceContainsASequenceAndMatchIsValid()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'),
                new Character('c'),
                new Character('d'));

            var abc = new Sequence(
                ab,
                new Character('c'));

            const string text = "abcdcd";

            Assert.True(abc.Match(text).Success());
            Assert.Equal("d", abc.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseWhenSequenceContainsASequenceAndMatchIsInvalid()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            var abc = new Sequence(
                ab,
                new Character('c'));

            const string text = "def";

            Assert.False(abc.Match(text).Success());
            Assert.Equal("def", abc.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseWhenSequenceContainsASequenceAndMatchIsPartiallyInvalid()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            var abc = new Sequence(
                ab,
                new Character('c'));

            const string text = "abf";

            Assert.False(abc.Match(text).Success());
            Assert.Equal("abf", abc.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseWhenSequenceContainsASequenceAndStringIsNull()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            var abc = new Sequence(
                ab,
                new Character('c'));

            const string text = null;

            Assert.False(abc.Match(text).Success());
            Assert.Equal(text, abc.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseWhenSequenceContainsASequenceAndStringIsEmpty()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            var abc = new Sequence(
                ab,
                new Character('c'));

            const string text = "";

            Assert.False(abc.Match(text).Success());
            Assert.Equal(text, abc.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenSequenceContainsAChoiceAndMatchIsValid()
        {
            var hex = new Choice(
               new Range('0', '9'),
               new Range('a', 'f'),
               new Range('A', 'F'));

            var hexSeq = new Sequence(
                new Character('u'),
                new Sequence(
                    hex,
                    hex,
                    hex,
                    hex));

            const string text = "u1234";

            Assert.True(hexSeq.Match(text).Success());
            Assert.Equal("", hexSeq.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenSequenceContainsAChoiceAndMatchIsValid()
        {
            var hex = new Choice(
                new Range('0', '9'),
                new Range('a', 'f'),
                new Range('A', 'F'));

            var hexSeq = new Sequence(
                new Character('u'),
                new Sequence(
                    hex,
                    hex,
                    hex,
                    hex));

            const string text = "uabcdef";

            Assert.True(hexSeq.Match(text).Success());
            Assert.Equal("ef", hexSeq.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenSequenceContainsAChoiceAndStringContainsSpace()
        {
            var hex = new Choice(
                new Range('0', '9'),
                new Range('a', 'f'),
                new Range('A', 'F'));

            var hexSeq = new Sequence(
                new Character('u'),
                new Sequence(
                    hex,
                    hex,
                    hex,
                    hex));

            const string text = "uB005 ab";

            Assert.True(hexSeq.Match(text).Success());
            Assert.Equal(" ab", hexSeq.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenMatchIsInvalid()
        {
            var hex = new Choice(
                new Range('0', '9'),
                new Range('a', 'f'),
                new Range('A', 'F'));

            var hexSeq = new Sequence(
                new Character('u'),
                new Sequence(
                    hex,
                    hex,
                    hex,
                    hex));

            const string text = "abc";

            Assert.False(hexSeq.Match(text).Success());
            Assert.Equal(text, hexSeq.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenStringIsNull()
        {
            var hex = new Choice(
                new Range('0', '9'),
                new Range('a', 'f'),
                new Range('A', 'F'));

            var hexSeq = new Sequence(
                new Character('u'),
                new Sequence(
                    hex,
                    hex,
                    hex,
                    hex));

            const string text = null;

            Assert.False(hexSeq.Match(text).Success());
            Assert.Equal(text, hexSeq.Match(text).RemainingText());
        }
    }
}
