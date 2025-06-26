using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class InputHandlerFacts
    {
        [Fact]
        public void InputHandlerClassExists()
        {
            FakeConsoleController controller = new();
            InputHandler inputHandler = new(controller);
            Assert.IsType<InputHandler>(inputHandler);
        }
    }
}
