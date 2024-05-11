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

            StringView stringView = new ("abcd", 0);

            Assert.True(ab.Match(stringView).Success());
            Assert.Equal('c', ab.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndRemainingTextWhenTheMatchIsPartiallyInvalid()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            StringView stringView = new ("ax", 0);

            Assert.False(ab.Match(stringView).Success());
            Assert.Equal('a', ab.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndRemainingTextWhenTheStringIsInvalid()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            StringView stringView = new ("def", 0);

            Assert.False(ab.Match(stringView).Success());
            Assert.Equal('d', ab.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseWhenTheStringIsNull()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            StringView stringView = new (null, 0);

            Assert.False(ab.Match(stringView).Success());
            Assert.Equal('\0', ab.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseWhenTheStringIsEmpty()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            StringView stringView = new ("", 0);

            Assert.False(ab.Match(stringView).Success());
            Assert.Equal('\0', ab.Match(stringView).StringView().Peek());
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

            StringView stringView = new ("abcdcd", 0);

            Assert.True(abc.Match(stringView).Success());
            Assert.Equal('d', abc.Match(stringView).StringView().Peek());
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

            StringView stringView = new ("def", 0);

            Assert.False(abc.Match(stringView).Success());
            Assert.Equal('d', abc.Match(stringView).StringView().Peek());
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

            StringView stringView = new ("abf", 0);

            Assert.False(abc.Match(stringView).Success());
            Assert.Equal('a', abc.Match(stringView).StringView().Peek());
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

            StringView stringView = new (null, 0);

            Assert.False(abc.Match(stringView).Success());
            Assert.Equal('\0', abc.Match(stringView).StringView().Peek());
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

            StringView stringView = new ("", 0);

            Assert.False(abc.Match(stringView).Success());
            Assert.Equal('\0', abc.Match(stringView).StringView().Peek());
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

            StringView stringView = new ("u1234", 0);

            Assert.True(hexSeq.Match(stringView).Success());
            Assert.Equal('\0', hexSeq.Match(stringView).StringView().Peek());
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

            StringView stringView = new ("uabcdef", 0);

            Assert.True(hexSeq.Match(stringView).Success());
            Assert.Equal('e', hexSeq.Match(stringView).StringView().Peek());
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

            StringView stringView = new ("uB005 ab", 0);

            Assert.True(hexSeq.Match(stringView).Success());
            Assert.Equal(' ', hexSeq.Match(stringView).StringView().Peek());
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

            StringView stringView = new ("abc", 0);

            Assert.False(hexSeq.Match(stringView).Success());
            Assert.Equal('a', hexSeq.Match(stringView).StringView().Peek());
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

            StringView stringView = new (null, 0);

            Assert.False(hexSeq.Match(stringView).Success());
            Assert.Equal('\0', hexSeq.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenStringIsEmpty()
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

            StringView stringView = new ("", 0);

            Assert.False(hexSeq.Match(stringView).Success());
            Assert.Equal('\0', hexSeq.Match(stringView).StringView().Peek());
        }
    }
}
