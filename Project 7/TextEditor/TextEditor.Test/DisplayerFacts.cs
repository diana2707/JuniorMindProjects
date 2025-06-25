using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class DisplayerFacts
    {
        [Fact]
        public void DisplayerObjectExists()
        {
            StringWriter writer = new();
            var displayer = new Displayer(writer);
            Assert.IsType<Displayer>(displayer);
        }
    }
}
