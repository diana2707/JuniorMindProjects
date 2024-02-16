using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class StringsFacts
    {
        public static string Quoted(string text)
        {
            return $"\"{text}\"";
        }

        [Fact]
        public void ShouldReturnTrueAndAndEmptyStringWhenStringIsWrappedInDoubleQuotes()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted("abc")).Success());
            Assert.Equal("", s.Match(Quoted("abc")).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndAndWholeTextWhenStringMissesStartingQuote()
        {
            var s = new Strings();
            Assert.False(s.Match("abc\"").Success());
            Assert.Equal("abc\"", s.Match("abc\"").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndAndWholeTextWhenStringMissesEndingQuote()
        {
            var s = new Strings();
            Assert.False(s.Match("\"abc").Success());
            Assert.Equal("\"abc", s.Match("\"abc").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndAndNullWhenTextIsNull()
        {
            var s = new Strings();
            Assert.False(s.Match(null).Success());
            Assert.Null(s.Match(null).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndAndEmptyStringWhenTextIsEmptyString()
        {
            var s = new Strings();
            Assert.False(s.Match("").Success());
            Assert.Equal("", s.Match("").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndAndEmptyStringWhenTextIsQuotedEmptyString()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted("")).Success());
            Assert.Equal("", s.Match(Quoted("")).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndAndWholeTextWhenContainsControlCharacters()
        {
            var s = new Strings();
            Assert.False(s.Match(Quoted("a\nb\rc")).Success());
            Assert.Equal(Quoted("a\nb\rc"), s.Match(Quoted("a\nb\rc")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndAndEmptyStringWhenContainsEscapedQuotationMarks()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted(@"\""a\"" b")).Success());
            Assert.Equal("", s.Match(Quoted(@"\""a\"" b")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndAndEmptyStringWhenContainsEscapedBackslash()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted(@"a \\ b")).Success());
            Assert.Equal("", s.Match(Quoted(@"a \\ b")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndAndEmptyStringWhenContainsLargeUnicodeCharacters()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted("⛅⚾")).Success());
            Assert.Equal("", s.Match(Quoted("⛅⚾")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndAndEmptyStringWhenContainsEscapedSlash()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted(@"a \/ b")).Success());
            Assert.Equal("", s.Match(Quoted(@"a \/ b")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndAndEmptyStringWhenContainsEscapedBackspace()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted(@"a \b b")).Success());
            Assert.Equal("", s.Match(Quoted(@"a \b b")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndAndEmptyStringWhenContainsEscapedLinefeed()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted(@"a \n b")).Success());
            Assert.Equal("", s.Match(Quoted(@"a \n b")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndAndEmptyStringWhenContainsEscapedCarrigeReturn()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted(@"a \r b")).Success());
            Assert.Equal("", s.Match(Quoted(@"a \r b")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndAndEmptyStringWhenContainsEscapedHorizontalTab()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted(@"a \t b")).Success());
            Assert.Equal("", s.Match(Quoted(@"a \t b")).RemainingText());
        }
    }


    //[Fact]
    //public void CanContainEscapedHorizontalTab()
    //{
    //    Assert.True(IsJsonString(Quoted(@"a \t b")));
    //}

    //[Fact]
    //public void CanContainEscapedUnicodeCharacters()
    //{
    //    Assert.True(IsJsonString(Quoted(@"a \u26Be b")));
    //}

    //[Fact]
    //public void CanContainAnyMultipleEscapeSequences()
    //{
    //    Assert.True(IsJsonString(Quoted(@"\\\u1212\n\t\r\\\b")));
    //}

    //[Fact]
    //public void DoesNotContainUnrecognizedEscapeCharacters()
    //{
    //    Assert.False(IsJsonString(Quoted(@"a\x")));
    //}

    //[Fact]
    //public void DoesNotEndWithReverseSolidus()
    //{
    //    Assert.False(IsJsonString(Quoted(@"a\")));
    //}

    //[Fact]
    //public void DoesNotEndWithAnUnfinishedHexNumber()
    //{
    //    Assert.False(IsJsonString(Quoted(@"a\u")));
    //    Assert.False(IsJsonString(Quoted(@"a\u123")));
    //}

    //[Fact]
    //public void CanContainNonBMPUnicodeCharacters()
    //{
    //    Assert.True(IsJsonString(Quoted(@"🙉")));
    //}

    //[Fact]
    //public void CanContainEscapedApostrophe()
    //{
    //    Assert.True(IsJsonString(Quoted(@"\'")));
    //}

    //[Fact]
    //public void ReturnsFalseWhenContainRecognisedAndUnrecognisedEscapedSequences()
    //{
    //    Assert.False(IsJsonString(Quoted(@"\u2634 \u11")));
    //}

    //public static string Quoted(string text)
    //    => $"\"{text}\"";
}
