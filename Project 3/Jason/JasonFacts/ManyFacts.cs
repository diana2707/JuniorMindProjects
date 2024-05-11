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
            StringView stringView = new ("abc", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('b', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternIsCharacterAndMatchesMultipleTimes()
        {
            var a = new Many(new Character('a'));
            StringView stringView = new ("aaabc", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('b', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternIsCharacterAndDoesNotMatch()
        {
            var a = new Many(new Character('a'));
            StringView stringView = new ("bc", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('b', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenPatternIsCharacterTheTextIsEmptyString()
        {
            var a = new Many(new Character('a'));
            StringView stringView = new ("", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('\0', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndNullWhenPatternIsCharacterTheTextIsNull()
        {
            var a = new Many(new Character('a'));
            StringView stringView = new (null, 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('\0', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternIsRangeAndMatchesMultipleTimes()
        {
            var digits = new Many(new Range('0', '9'));
            StringView stringView = new ("12345ab123", 0);
            Assert.True(digits.Match(stringView).Success());
            Assert.Equal('a', digits.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternIsRangeAndDoesNotMatch()
        {
            var digits = new Many(new Range('0', '9'));
            StringView stringView = new ("ab", 0);
            Assert.True(digits.Match(stringView).Success());
            Assert.Equal('a', digits.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternIsPresentInTheRemainingText()
        {
            var a = new Many(new Character('a'));
            StringView stringView = new ("aaababa", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('b', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternIsPresentInTheRemainingText()
        {
            var a = new Many(new Character('a'));
            StringView stringView = new ("baba", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('b', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyTextWhenEntirePatternIsConsumed()
        {
            var a = new Many(new Character('a'));
            StringView stringView = new ("aaaaa", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('\0', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyTextWhenThePatternIsASequence()
        {
            var a = new Many(new Sequence(new Character('a'), new Character('b')));
            StringView stringView = new ("abab", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('\0', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenThePatternIsASequence()
        {
            var a = new Many(new Sequence(new Character('a'), new Character('b')));
            StringView stringView = new ("ababx", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('x', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyTextWhenThePatternIsASequenceAndContainsARange()
        {
            var a = new Many(new Sequence(new Character('a'), new Range('0', '9')));
            StringView stringView = new ("a1a2a3", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('\0', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenThePatternIsASequenceAndContainsARange()
        {
            var a = new Many(new Sequence(new Character('a'), new Range('0', '9')));
            StringView stringView = new ("a1a2a3xx", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('x', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenThePatternIsASequenceAndTheMatchIsInvalid()
        {
            var a = new Many(new Sequence(new Character('a'), new Range('0', '9')));
            StringView stringView = new ("xyz", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('x', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenThePatternIsASequenceAndTheMatchIsPartial()
        {
            var a = new Many(new Sequence(new Character('a'), new Range('0', '9')));
            StringView stringView = new ("axyz", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('a', a.Match(stringView).StringView().Peek());
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

            StringView stringView = new ("#5@d#6@fz", 0);

            Assert.True(a.Match(stringView).Success());
            Assert.Equal('z', a.Match(stringView).StringView().Peek());
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

            StringView stringView = new ("", 0);

            Assert.True(a.Match(stringView).Success());
            Assert.Equal('\0', a.Match(stringView).StringView().Peek());
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

            StringView stringView = new (null, 0);

            Assert.True(a.Match(stringView).Success());
            Assert.Equal('\0', a.Match(stringView).StringView().Peek());
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

            StringView stringView = new ("#5dz", 0);

            Assert.True(a.Match(stringView).Success());
            Assert.Equal('#', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyTextWhenThePatternIsAChoice()
        {
            var a = new Many(new Choice(new Character('a'), new Character('b')));
            StringView stringView = new ("abab", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('\0', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenThePatternIsAChoice()
        {
            var a = new Many(new Choice(new Character('a'), new Character('b')));
            StringView stringView = new ("ababxx", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('x', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenThePatternIsAChoiceAndContainsARange()
        {
            var a = new Many(new Choice(new Character('a'), new Range('1', '5')));
            StringView stringView = new ("a12aa4y", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('y', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenThePatternIsAChoiceAndTheMatchIsInvalid()
        {
            var a = new Many(new Choice(new Character('a'), new Range('1', '5')));
            StringView stringView = new ("za12aa4y", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('z', a.Match(stringView).StringView().Peek());
        }
    }
}
