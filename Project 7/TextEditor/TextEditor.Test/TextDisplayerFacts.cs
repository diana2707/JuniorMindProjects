using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class TextDisplayerFacts
    {
        [Fact]
        public void CreateValidTextDisplayerObject()
        {
            Assert.IsType<TextDisplayer>(new TextDisplayer());
        }
    }
}
