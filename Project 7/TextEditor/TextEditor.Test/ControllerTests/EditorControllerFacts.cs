using System;
using TextEditor.Domain.Controller;
using TextEditor.Domain.CursorLogic;
using TextEditor.Domain.Display;
using TextEditor.Domain.Editor;
using TextEditor.Domain.Input;
using TextEditor.Test.Shared;

namespace TextEditor.Test.ControllerTests
{
    public class EditorControllerFacts
    {
        [Fact]
        public void EditorControllerObjectExists()
        {
            var editorController = CreateEditorController();
            Assert.IsType<EditorController>(editorController);
        }

        [Fact]
        public void RunMethodDisplaysInitialTextCorrectlly()
        {
            var path = GetTestFilePath("SingleChar.txt");

            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);
            FakeConsoleController consoleController = new();
            InputHandler inputHandler = new(consoleController);
            Displayer displayer = new (consoleController);
            Viewport viewport = new (120, 30, 0, textManager);
            Cursor cursor = new (viewport);

            EditorController editorController = new(
                fileManager,
                textManager,
                inputHandler,
                displayer,
                viewport,
                cursor);

            consoleController.KeysToRead.Add(new ConsoleKeyInfo('\u001B', ConsoleKey.Escape, false, false, false));
            editorController.Run();

            Assert.Equal(["a\r\n"], consoleController.WrittenLines);
        }

        [Fact]
        public void RunMethodMovesCursorAfterTextOnEscape()
        {
            var path = GetTestFilePath("MultipleRows.txt");

            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text);
            FakeConsoleController consoleController = new();
            InputHandler inputHandler = new(consoleController);
            Displayer displayer = new(consoleController);
            Viewport viewport = new(120, 30, 0, textManager);
            Cursor cursor = new(viewport);

            EditorController editorController = new(
                fileManager,
                textManager,
                inputHandler,
                displayer,
                viewport,
                cursor);

            consoleController.KeysToRead.Add(new ConsoleKeyInfo('\u001B', ConsoleKey.Escape, false, false, false));
            editorController.Run();
            Assert.Equal(4, cursor.Row);
        }

        private static EditorController CreateEditorController(int startingRow = 0)
        {
            var path = GetTestFilePath("EmptyRow.txt");

            FileManager fileManager = new (path);
            TextManager textManager = new (fileManager.Text);
            FakeConsoleController consoleController = new ();
            InputHandler inputHandler = new (consoleController);
            Displayer displayer = new Displayer(consoleController);
            Viewport viewport = new Viewport(120, 30, startingRow, textManager);
            Cursor cursor = new Cursor(viewport);

            return new EditorController(fileManager, textManager, inputHandler, displayer, viewport, cursor);
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
