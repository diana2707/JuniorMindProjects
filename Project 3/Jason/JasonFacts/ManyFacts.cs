using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class ManyFacts
    {
        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternIsCharacterAndMatchesOnce()
        {
            var a = new Many(new Character('a'));
            StringView currentIndex = new ("abc", 0);
            StringView failedIndex = new ("abc", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('b', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('b', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternIsCharacterAndMatchesMultipleTimes()
        {
            var a = new Many(new Character('a'));
            StringView currentIndex = new ("aaabc", 0);
            StringView failedIndex = new ("aaabc", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('b', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('b', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternIsCharacterAndDoesNotMatch()
        {
            var a = new Many(new Character('a'));
            StringView currentIndex = new ("bc", 0);
            StringView failedIndex = new ("bc", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('b', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('b', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenPatternIsCharacterTheTextIsEmptyString()
        {
            var a = new Many(new Character('a'));
            StringView currentIndex = new ("", 0);
            StringView failedIndex = new ("", 0);
            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndNullWhenPatternIsCharacterTheTextIsNull()
        {
            var a = new Many(new Character('a'));
            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);
            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternIsRangeAndMatchesMultipleTimes()
        {
            var digits = new Many(new Range('0', '9'));
            StringView currentIndex = new ("12345ab123", 0);
            StringView failedIndex = new ("12345ab123", 0);
            Assert.True(digits.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', digits.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('a', digits.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternIsRangeAndDoesNotMatch()
        {
            var digits = new Many(new Range('0', '9'));
            StringView currentIndex = new ("ab", 0);
            StringView failedIndex = new ("ab", 0);
            Assert.True(digits.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', digits.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('a', digits.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternIsPresentInTheRemainingText()
        {
            var a = new Many(new Character('a'));
            StringView currentIndex = new ("aaababa", 0);
            StringView failedIndex = new ("aaababa", 0);
            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('b', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('b', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternIsPresentInTheRemainingText()
        {
            var a = new Many(new Character('a'));
            StringView currentIndex = new ("baba", 0);
            StringView failedIndex = new ("baba", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('b', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('b', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyTextWhenEntirePatternIsConsumed()
        {
            var a = new Many(new Character('a'));
            StringView currentIndex = new ("aaaaa", 0);
            StringView failedIndex = new ("aaaaa", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyTextWhenThePatternIsASequence()
        {
            var a = new Many(new Sequence(new Character('a'), new Character('b')));
            StringView currentIndex = new ("abab", 0);
            StringView failedIndex = new ("abab", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenThePatternIsASequence()
        {
            var a = new Many(new Sequence(new Character('a'), new Character('b')));
            StringView currentIndex = new ("ababx", 0);
            StringView failedIndex = new ("ababx", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('x', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('x', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyTextWhenThePatternIsASequenceAndContainsARange()
        {
            var a = new Many(new Sequence(new Character('a'), new Range('0', '9')));
            StringView currentIndex = new ("a1a2a3", 0);
            StringView failedIndex = new ("a1a2a3", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenThePatternIsASequenceAndContainsARange()
        {
            var a = new Many(new Sequence(new Character('a'), new Range('0', '9')));
            StringView currentIndex = new ("a1a2a3xx", 0);
            StringView failedIndex = new ("a1a2a3xx", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('x', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('x', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenThePatternIsASequenceAndTheMatchIsInvalid()
        {
            var a = new Many(new Sequence(new Character('a'), new Range('0', '9')));
            StringView currentIndex = new ("xyz", 0);
            StringView failedIndex = new ("xyz", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('x', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('x', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenThePatternIsASequenceAndTheMatchIsPartial()
        {
            var a = new Many(new Sequence(new Character('a'), new Range('0', '9')));
            StringView currentIndex = new ("axyz", 0);
            StringView failedIndex = new ("axyz", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('x', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenThePatternIsASequenceAndContainsCharactersRangeChoice()
        {
            var a = new Many(new Sequence(
                new Choice(
                    new Sequence(new Character('#'), new Range('1', '5')),
                    new Sequence(new Character('#'), new Range('6', '9'))),
                new Character('@'),
                new Range('a', 'f')));

            StringView currentIndex = new ("#5@d#6@fz", 0);
            StringView failedIndex = new ("#5@d#6@fz", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('z', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('z', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenThePatternIsASequenceAndTheTextIsEmptyString()
        {
            var a = new Many(new Sequence(
                new Choice(
                    new Sequence(new Character('#'), new Range('1', '5')),
                    new Sequence(new Character('#'), new Range('6', '9'))),
                new Character('@'),
                new Range('a', 'f')));

            StringView currentIndex = new ("", 0);
            StringView failedIndex = new ("", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenThePatternIsASequenceAndTheTextIsNull()
        {
            var a = new Many(new Sequence(
                new Choice(
                    new Sequence(new Character('#'), new Range('1', '5')),
                    new Sequence(new Character('#'), new Range('6', '9'))),
                new Character('@'),
                new Range('a', 'f')));

            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenThePatternIsASequenceAndTheMatchIsIncomplete()
        {
            var a = new Many(new Sequence(
                new Choice(
                    new Sequence(new Character('#'), new Range('1', '5')),
                    new Sequence(new Character('#'), new Range('6', '9'))),
                new Character('@'),
                new Range('a', 'f')));

            StringView currentIndex = new ("#5dz", 0);
            StringView failedIndex = new ("#5dz", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('#', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('d', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyTextWhenThePatternIsAChoice()
        {
            var a = new Many(new Choice(new Character('a'), new Character('b')));
            StringView currentIndex = new ("abab", 0);
            StringView failedIndex = new ("abab", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenThePatternIsAChoice()
        {
            var a = new Many(new Choice(new Character('a'), new Character('b')));
            StringView currentIndex = new ("ababxx", 0);
            StringView failedIndex = new ("ababxx", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('x', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('x', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenThePatternIsAChoiceAndContainsARange()
        {
            var a = new Many(new Choice(new Character('a'), new Range('1', '5')));
            StringView currentIndex = new ("a12aa4y", 0);
            StringView failedIndex = new ("a12aa4y", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('y', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('y', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenThePatternIsAChoiceAndTheMatchIsInvalid()
        {
            var a = new Many(new Choice(new Character('a'), new Range('1', '5')));
            StringView currentIndex = new ("za12aa4y", 0);
            StringView failedIndex = new ("za12aa4y", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('z', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('z', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }
    }
}
