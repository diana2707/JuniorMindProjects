using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class DisplayerFacts
    {
        [Fact]
        public void DisplayerObjectExists()
        {
            var displayer = new Displayer();
            Assert.IsType<Displayer>(displayer);
        }
    }
}
