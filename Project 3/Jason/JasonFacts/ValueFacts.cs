using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class ValueFacts
    {
        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsString()
        {
            var value = new Value();

            Assert.True(value.Match(" \"abc\" ").Success());
            Assert.Equal("", value.Match(" \"abc\" ").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsNumber()
        {
            var value = new Value();

            Assert.True(value.Match(" 12 ").Success());
            Assert.Equal("", value.Match(" 12 ").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsEmptyArray()
        {
            var value = new Value();

            Assert.True(value.Match(" [ ] ").Success());
            Assert.Equal("", value.Match(" [ ] ").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsAnArrayOfNumbers()
        {
            var value = new Value();

            Assert.True(value.Match(" [ 1 , 2 , 3 ] ").Success());
            Assert.Equal("", value.Match(" [ 1 , 2 , 3 ] ").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsAnArrayOfStrings()
        {
            var value = new Value();

            Assert.True(value.Match(" [ \"a\" , \"b\" , \"c\" ] ").Success());
            Assert.Equal("", value.Match(" [ \"a\" , \"b\" , \"c\" ] ").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsAnArrayOfBoolean()
        {
            var value = new Value();

            Assert.True(value.Match(" [ true ] ").Success());
            Assert.Equal("", value.Match(" [ true ] ").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsAnArrayOfArrays()
        {
            var value = new Value();

            Assert.True(value.Match(" [ [ 1 ] , [ 2 ] , [ 3 , 4 ] ] ").Success());
            Assert.Equal("", value.Match(" [ [ 1 ] , [ 2 ] , [ 3 , 4 ] ] ").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsEmptyObject()
        {
            var value = new Value();

            Assert.True(value.Match(" { } ").Success());
            Assert.Equal("", value.Match(" { } ").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsObjectWithKeyValues()
        {
            var value = new Value();

            const string obj = " { \"Name\" : \"Ana\" , \"Age\" : 25 , \"Married\" : true , " +
                "\"Children\" : [ \"Andreea\" , \"Ioana\" ] , \"Pets\" : { \"Cat\" : \"Kitty\" , \"Dog\" : \"Marley\" } }";

            Assert.True(value.Match(obj).Success());
            Assert.Equal("", value.Match(obj).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsInvalidObject()
        {
            var value = new Value();

            const string obj = " { \"Name\" : \"Ana\" , \"Age\" : 25 , \"Married\" : true , " +
                "\"Children\"  [ \"Andreea\" , \"Ioana\" ] , \"Pets\" : { \"Cat\" : \"Kitty\" , \"Dog\" : \"Marley\" } }";

            Assert.False(value.Match(obj).Success());
            Assert.Equal(obj, value.Match(obj).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsObjectWithKeyValues1()
        {
            var value = new Value();

            const string obj = "{\r\n    \"glossary\": {\r\n        \"title\": \"example glossary\",\r\n\t\t\"GlossDiv\": {\r\n            \"title\": \"S\",\r\n\t\t\t" +
                "\"GlossList\": {\r\n                \"GlossEntry\": {\r\n                    \"ID\": \"SGML\",\r\n\t\t\t\t\t\"SortAs\": \"SGML\",\r\n\t\t\t\t\t\"" +
                "GlossTerm\": \"Standard Generalized Markup Language\",\r\n\t\t\t\t\t\"Acronym\": \"SGML\",\r\n\t\t\t\t\t\"Abbrev\": \"ISO 8879:1986\",\r\n\t\t\t\t\t\"" +
                "GlossDef\": {\r\n                        \"para\": \"A meta-markup language, used to create markup languages such as DocBook.\",\r\n\t\t\t\t\t\t\"" +
                "GlossSeeAlso\": [\"GML\", \"XML\"]\r\n                    },\r\n\t\t\t\t\t\"GlossSee\": \"markup\"\r\n                }\r\n            }\r\n        }\r\n    }\r\n}";

            Assert.True(value.Match(obj).Success());
            Assert.Equal("", value.Match(obj).RemainingText());
        }
    }
}