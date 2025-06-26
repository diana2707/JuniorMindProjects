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

        //[Fact]
        //public void ListenMethodReadsKeysInALoop()
        //{
        //    FakeConsoleController controller = new();
        //    InputHandler inputHandler = new(controller);

        //    ConsoleKeyInfo downArrow = new('\u2193', ConsoleKey.DownArrow, false, false, false);
        //    ConsoleKeyInfo upArrow = new('\u2191', ConsoleKey.UpArrow, false, false, false);
        //    ConsoleKeyInfo leftArrow = new('\u2190', ConsoleKey.LeftArrow, false, false, false);
        //    ConsoleKeyInfo rightArrow = new('\u2192', ConsoleKey.RightArrow, false, false, false);

        //    controller.KeysToRead = [downArrow, upArrow, leftArrow, rightArrow];
        //    inputHandler.Listen();

        //    Assert.Equal(, inputHandler.Listen());
        //}

        [Fact]
        public void ListenMethodRaisesEventArrowKeyPressedWhenAnArrowKeyIsRead()
        {

            FakeConsoleController controller = new();
            InputHandler inputHandler = new(controller);

            ConsoleKeyInfo? result = null;
            ConsoleKeyInfo downArrow = new('\u2193', ConsoleKey.DownArrow, false, false, false);
            ConsoleKeyInfo escape = new('\u001B', ConsoleKey.Escape, false, false, false);

            controller.KeysToRead.Add(downArrow);
            controller.KeysToRead.Add(escape);

            inputHandler.ArrowKeyPressed += downArrow => result = downArrow;
            inputHandler.Listen();

            Assert.Equal(downArrow, result);
        }
    }
}
