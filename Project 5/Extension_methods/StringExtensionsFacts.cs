using System;

namespace Extension_methods
{
    public class StringExtensionsFacts
    {
        [Fact]
        public void CountVowelsReturnsNumberOfVowelsInString()
        {
            string source = "Alexandra";
            Assert.Equal(4, source.CountVowels());
        }

        [Fact]
        public void CountConsonantsReturnsNumberOfVowelsInString()
        {
            string source = "Alexandra";
            Assert.Equal(5, source.CountConsonants());
        }
    }
}
