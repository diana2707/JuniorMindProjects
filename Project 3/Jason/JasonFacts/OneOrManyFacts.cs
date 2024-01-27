using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class OneOrManyFacts
    {
        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenPatternDoesNotMatch()
        {
            var a = new OneOrMany(new Range('0', '9'));
            Assert.False(a.Match("bc").Success());
            Assert.Equal("bc", a.Match("bc").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAnEmptyStringWhenPatternConsumesWholeText()
        {
            var a = new OneOrMany(new Range('0', '9'));
            Assert.True(a.Match("123").Success());
            Assert.Equal("", a.Match("123").RemainingText());
        }

    }
}
