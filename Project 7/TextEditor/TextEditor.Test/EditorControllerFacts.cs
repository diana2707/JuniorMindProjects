using System;
using TextEditor.Domain;

namespace TextEditor.Test
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
