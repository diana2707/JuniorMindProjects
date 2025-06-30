using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class ViewportFacts
    {
        [Fact]
        public void ViewportObjectExists()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text, 120, 30, 0);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, 0, textInfo);
            Assert.IsType<Viewport>(viewport);
        }

        [Fact]
        public void GetLinesCountReturnsTheCountOfDisplayedLinesWhenAreLessTheWindowHight()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text, 120, 30, 0);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, 0, textInfo);

            Assert.Equal(4, viewport.GetLinesCount());
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
