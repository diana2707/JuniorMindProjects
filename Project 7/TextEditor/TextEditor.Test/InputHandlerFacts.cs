using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class InputHandlerFacts
    {
        [Fact]
        public void InputHandlerClassExists()
        {
            InputHandler inputHandler = new();
            Assert.IsType<InputHandler>(inputHandler);
        }
    }
}
