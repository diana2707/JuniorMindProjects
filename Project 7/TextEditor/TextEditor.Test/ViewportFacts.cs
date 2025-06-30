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
        public void LastRowPositionReturnsThePositionOfLastRowInViewport()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text, 120, 30, 0);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, 0, textInfo);

            Assert.Equal(3, viewport.LastRowPosition);
        }

        [Fact]
        public void LastRowPositionReturnsThePositionOfLastRowInViewportWhenStartPositionIsGreaterThen0()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text, 120, 30, 0);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, 5, textInfo);

            Assert.Equal(8, viewport.LastRowPosition);
        }

        [Fact]
        public void GetRowLengthReturnsTheRowLengthWhenRowIsSmallerThenWindowWidth()
        {
            var path = GetTestFilePath("SingleRow.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text, 120, 30, 0);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, 0, textInfo);

            Assert.Equal(15, viewport.GetRowLength(0));
        }

        [Fact]
        public void GetRowLengthReturnsTheRowLengthWhenRowIsLongerThenWindowWidth()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            FileManager fileManager = new(path);
            TextManager textManager = new(fileManager.Text, 120, 30, 0);
            ITextInfo textInfo = textManager;
            Viewport viewport = new(120, 30, 0, textInfo);

            Assert.Equal(119, viewport.GetRowLength(0));
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
