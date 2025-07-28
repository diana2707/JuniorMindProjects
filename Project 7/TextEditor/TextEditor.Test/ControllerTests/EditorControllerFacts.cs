using System;
using TextEditor.Domain.Controller;

namespace TextEditor.Test.ControllerTests
{
    public class EditorControllerFacts
    {
        [Fact]
        public void EditorControllerObjectExists()
        {
            var editorController = new EditorController();
            Assert.IsType<EditorController>(editorController);
        }
    }
}
