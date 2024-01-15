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
    }
}
