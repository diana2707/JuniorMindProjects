using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class TextDisplayerFacts
    {
        [Fact]
        public void CreateValidTextDisplayerObject()
        {
            string path = GetTestFilePath("singleRow.txt");
            FileManager manager = new(path);
            Assert.IsType<TextDisplayer>(new TextDisplayer(manager));
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
