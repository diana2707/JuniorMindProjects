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
