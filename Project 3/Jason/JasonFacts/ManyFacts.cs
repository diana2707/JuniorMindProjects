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
            Assert.True(a.Match("abc").Success());
            Assert.Equal("bc", a.Match("abc").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternIsCharacterAndMatchesMultipleTimes()
        {
            var a = new Many(new Character('a'));
            Assert.True(a.Match("aaabc").Success());
            Assert.Equal("bc", a.Match("aaabc").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternIsCharacterAndDoesNotMatch()
        {
            var a = new Many(new Character('a'));
            Assert.True(a.Match("bc").Success());
            Assert.Equal("bc", a.Match("bc").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenPatternIsCharacterTheTextIsEmptyString()
        {
            var a = new Many(new Character('a'));
            Assert.True(a.Match("").Success());
            Assert.Equal("", a.Match("").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndNullWhenPatternIsCharacterTheTextIsNull()
        {
            var a = new Many(new Character('a'));
            Assert.True(a.Match(null).Success());
            Assert.Null(a.Match(null).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternIsRangeAndMatchesMultipleTimes()
        {
            var digits = new Many(new Range('0', '9'));
            Assert.True(digits.Match("12345ab123").Success());
            Assert.Equal("ab123", digits.Match("12345ab123").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternIsRangeAndDoesNotMatch()
        {
            var digits = new Many(new Range('0', '9'));
            Assert.True(digits.Match("ab").Success());
            Assert.Equal("ab", digits.Match("ab").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternIsPresentInTheRemainingText()
        {
            var a = new Many(new Character('a'));
            Assert.True(a.Match("aaababa").Success());
            Assert.Equal("baba", a.Match("aaababa").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternIsPresentInTheRemainingText()
        {
            var a = new Many(new Character('a'));
            Assert.True(a.Match("baba").Success());
            Assert.Equal("baba", a.Match("baba").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyTextWhenEntirePatternIsConsumed()
        {
            var a = new Many(new Character('a'));
            Assert.True(a.Match("aaaaa").Success());
            Assert.Equal("", a.Match("aaaaa").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyTextWhenThePatternIsASequence()
        {
            var a = new Many(new Sequence(new Character('a'), new Character('b')));
            Assert.True(a.Match("abab").Success());
            Assert.Equal("", a.Match("abab").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenThePatternIsASequence()
        {
            var a = new Many(new Sequence(new Character('a'), new Character('b')));
            Assert.True(a.Match("ababx").Success());
            Assert.Equal("x", a.Match("ababx").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyTextWhenThePatternIsASequenceAndContainsARange()
        {
            var a = new Many(new Sequence(new Character('a'), new Range('0', '9')));
            Assert.True(a.Match("a1a2a3").Success());
            Assert.Equal("", a.Match("a1a2a3").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenThePatternIsASequenceAndContainsARange()
        {
            var a = new Many(new Sequence(new Character('a'), new Range('0', '9')));
            Assert.True(a.Match("a1a2a3xx").Success());
            Assert.Equal("xx", a.Match("a1a2a3xx").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenThePatternIsASequenceAndTheMatchIsInvalid()
        {
            var a = new Many(new Sequence(new Character('a'), new Range('0', '9')));
            Assert.True(a.Match("xyz").Success());
            Assert.Equal("xyz", a.Match("xyz").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenThePatternIsASequenceAndTheMatchIsPartial()
        {
            var a = new Many(new Sequence(new Character('a'), new Range('0', '9')));
            Assert.True(a.Match("axyz").Success());
            Assert.Equal("axyz", a.Match("axyz").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenThePatterIsASequenceAndContainsCharactersRangeChoice()
        {
            var a = new Many(new Sequence(
                new Choice(
                    new Sequence(new Character('#'), new Range('1', '5')),
                    new Sequence(new Character('#'), new Range('6', '9'))),
                new Character('@'),
                new Range('a', 'f')));

            Assert.True(a.Match("#5@dz").Success());
            Assert.Equal("z", a.Match("#5@dz").RemainingText());
        }

    }
}
