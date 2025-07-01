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

        [Fact]
        public void ReadLineInputMethodReadsSingleLine()
        {
            FakeConsoleController controller = new();
            InputHandler inputHandler = new(controller);

            controller.LineToRead = "test";

            Assert.Equal("test", inputHandler.ReadInputLine());
        }

        [Fact]
        public void ReadInputKeyMethodReadsSingleKey()
        {
            FakeConsoleController controller = new();
            InputHandler inputHandler = new(controller);

            ConsoleKeyInfo inputKey = new ('\u2193', ConsoleKey.DownArrow, false, false, false);

            controller.KeysToRead.Add(inputKey);

            Assert.Equal(inputKey, inputHandler.ReadKeyWithoutDisplay());
        }

        [Fact]
        public void ListenMethodRaisesEventArrowKeyPressedWhenAnArrowKeyIsRead()
        {

            FakeConsoleController controller = new();
            InputHandler inputHandler = new(controller);

            Direction result = 0;
            ConsoleKeyInfo downArrow = new('\u2193', ConsoleKey.DownArrow, false, false, false);
            ConsoleKeyInfo escape = new('\u001B', ConsoleKey.Escape, false, false, false);

            controller.KeysToRead.Add(downArrow);
            controller.KeysToRead.Add(escape);

            inputHandler.ArrowKeyPressed += downArrow => result = Direction.Down;
            inputHandler.Listen();

            Assert.Equal(Direction.Down, result);
        }

        [Fact]
        public void ListenMethodRaisesEventEscapeKeyPressedWhenEscapeIsRead()
        {

            FakeConsoleController controller = new();
            InputHandler inputHandler = new(controller);

            bool result = false;
            ConsoleKeyInfo escape = new('\u001B', ConsoleKey.Escape, false, false, false);

            controller.KeysToRead.Add(escape);

            inputHandler.EscapeKeyPressed += () => result = true;
            inputHandler.Listen();

            Assert.True(result);
        }
    }
}
