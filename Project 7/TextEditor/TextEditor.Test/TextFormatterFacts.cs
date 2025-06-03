using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class TextFormatterFacts
    {
        [Fact]
        public void CreateValidTextDisplayerObject()
        {
            string path = GetTestFilePath("singleRow.txt");
            FileManager manager = new(path);
            Assert.IsType<TextFormatter>(new TextFormatter(manager.Text, 120, 30, 0));
        }

        [Fact]
        public void FormatWithinWindowReturnsTextLimitedToTheWidthOfWindow()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            FileManager fileManager = new(path);
            TextFormatter formatter = new(fileManager.Text, 120, 30, 0);

            Assert.Equal("This is a very long line that exceeds the console width of 130 characters. " +
                "This is a very long line that exceeds the con\r\n", formatter.FormatWithinWindow());
        }

        [Fact]
        public void LastRowPositionReturnsTheConsoleRowositionOfLastDisplayedRow()
        {
            var path = GetTestFilePath("SingleRow.txt");
            FileManager fileManager = new(path);
            TextFormatter formatter = new(fileManager.Text, 120, 30, 0);

            Assert.Equal(0, formatter.LastRowPosition);
        }

        [Fact]
        public void GetDisplayedRowLengthReturnsTheDisplayedLengthOfARow()
        {
            var path = GetTestFilePath("SingleRow.txt");
            FileManager fileManager = new(path);
            TextFormatter formatter = new(fileManager.Text, 120, 30, 0);

            Assert.Equal(15, formatter.GetRowDisplayedLength(0));
        }

        [Fact]
        public void ReadWindowTextReturnsTheVisibleTextFowConsoleWhenRowsAreLongerThenWindowHeigth()
        {
            var path = GetTestFilePath("ExceedsWindowHeigth.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 0);

            Assert.Equal("Line 1\r\nLine 2\r\nLine 3\r\nLine 4\r\nLine 5\r\nLine 6\r\nLine 7\r\nLine 8" +
                "\r\nLine 9\r\nLine 10\r\nLine 11\r\nLine 12\r\nLine 13\r\nLine 14\r\nLine 15\r\nLine 16" +
                "\r\nLine 17\r\nLine 18\r\nLine 19\r\nLine 20\r\nLine 21\r\nLine 22\r\nLine 23\r\nLine 24" +
                "\r\nLine 25\r\nLine 26\r\nLine 27\r\nLine 28\r\nLine 29\r\nLine 30\r\n", formatter.FormatWithinWindow());
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
