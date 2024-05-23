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

            StringView currentIndex = new ("abcd", 0);
            StringView failedIndex = new ("abcd", 0);

            Assert.True(ab.Match(currentIndex, failedIndex).Success());
            Assert.Equal('c', ab.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('a', ab.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndRemainingTextWhenTheMatchIsPartiallyInvalid()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            StringView currentIndex = new ("ax", 0);
            StringView failedIndex = new ("ax", 0);

            Assert.False(ab.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', ab.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('x', ab.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndRemainingTextWhenTheStringIsInvalid()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            StringView currentIndex = new ("def", 0);
            StringView failedIndex = new ("def", 0);

            Assert.False(ab.Match(currentIndex, failedIndex).Success());
            Assert.Equal('d', ab.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('d', ab.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseWhenTheStringIsNull()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);

            Assert.False(ab.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', ab.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', ab.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseWhenTheStringIsEmpty()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            StringView currentIndex = new ("", 0);
            StringView failedIndex = new ("", 0);

            Assert.False(ab.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', ab.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', ab.Match(currentIndex, failedIndex).FailedIndex().Peek());
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

            StringView currentIndex = new ("abcdcd", 0);
            StringView failedIndex = new ("abcdcd", 0);

            Assert.True(abc.Match(currentIndex, failedIndex).Success());
            Assert.Equal('d', abc.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('a', abc.Match(currentIndex, failedIndex).FailedIndex().Peek());
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

            StringView currentIndex = new ("def", 0);
            StringView failedIndex = new ("def", 0);

            Assert.False(abc.Match(currentIndex, failedIndex).Success());
            Assert.Equal('d', abc.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('d', abc.Match(currentIndex, failedIndex).FailedIndex().Peek());
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

            StringView currentIndex = new ("abf", 0);
            StringView failedIndex = new ("abf", 0);

            Assert.False(abc.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', abc.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('f', abc.Match(currentIndex, failedIndex).FailedIndex().Peek());
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

            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);

            Assert.False(abc.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', abc.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', abc.Match(currentIndex, failedIndex).FailedIndex().Peek());
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

            StringView failedIndex = new ("", 0);
            StringView currentIndex = new ("", 0);

            Assert.False(abc.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', abc.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', abc.Match(currentIndex, failedIndex).FailedIndex().Peek());
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

            StringView currentIndex = new ("u1234", 0);
            StringView failedIndex = new ("u1234", 0);

            Assert.True(hexSeq.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', hexSeq.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('u', hexSeq.Match(currentIndex, failedIndex).FailedIndex().Peek());
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

            StringView currentIndex = new ("uabcdef", 0);
            StringView failedIndex = new ("uabcdef", 0);

            Assert.True(hexSeq.Match(currentIndex, failedIndex).Success());
            Assert.Equal('e', hexSeq.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('d', hexSeq.Match(currentIndex, failedIndex).FailedIndex().Peek());
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

            StringView currentIndex = new ("uB005 ab", 0);
            StringView failedIndex = new ("uB005 ab", 0);

            Assert.True(hexSeq.Match(currentIndex, failedIndex).Success());
            Assert.Equal(' ', hexSeq.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('B', hexSeq.Match(currentIndex, failedIndex).FailedIndex().Peek());
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

            StringView currentIndex = new ("uzabc", 0);
            StringView failedIndex = new ("uzabc", 0);

            Assert.False(hexSeq.Match(currentIndex, failedIndex).Success());
            Assert.Equal('u', hexSeq.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('z', hexSeq.Match(currentIndex, failedIndex).FailedIndex().Peek());
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

            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);

            Assert.False(hexSeq.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', hexSeq.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', hexSeq.Match(currentIndex, failedIndex).FailedIndex().Peek());
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

            StringView currentIndex = new ("", 0);
            StringView failedIndex = new ("", 0);

            Assert.False(hexSeq.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', hexSeq.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', hexSeq.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }
    }
}
