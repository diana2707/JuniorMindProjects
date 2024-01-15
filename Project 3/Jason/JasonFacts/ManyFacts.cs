using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class ManyFacts
    {
        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternMatchesOnce()
        {
            var a = new Many(new Character('a'));
            Assert.True(a.Match("abc").Success());
            Assert.Equal("bc", a.Match("abc").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternMatchesMultipleTimes()
        {
            var a = new Many(new Character('a'));
            Assert.True(a.Match("aaabc").Success());
            Assert.Equal("bc", a.Match("aaabc").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternDoNotMatch()
        {
            var a = new Many(new Character('a'));
            Assert.True(a.Match("bc").Success());
            Assert.Equal("bc", a.Match("bc").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenTehTextIsEmptyString()
        {
            var a = new Many(new Character('a'));
            Assert.True(a.Match("").Success());
            Assert.Equal("", a.Match("").RemainingText());
        }
    }
}
