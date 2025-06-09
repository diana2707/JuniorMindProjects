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

            Assert.Equal("This is a very long line that exceeds the console width of 130 characters. " +//75
                "This is a very long line that exceeds the co\r\n", formatter.FormatWithinWindow());
        }

        [Fact]
        public void LastRowPositionReturnsTheConsoleRowPositionOfLastDisplayedRow()
        {
            var path = GetTestFilePath("SingleRow.txt");
            FileManager fileManager = new(path);
            TextFormatter formatter = new(fileManager.Text, 120, 30, 0);

            Assert.Equal(0, formatter.LastDisplayedRowPosition);
        }

        [Fact]
        public void LastRowPositionReturnsTheConsoleRowPositionOfLastDisplayedRowWhenStartingRowIsLargerThen0()
        {
            var path = GetTestFilePath("SingleRow.txt");
            FileManager fileManager = new(path);
            TextFormatter formatter = new(fileManager.Text, 120, 30, 5);

            Assert.Equal(5, formatter.LastDisplayedRowPosition);
        }

        [Fact]
        public void LastRowPositionReturnsTheConsoleRowPositionOfLastDisplayedRowWhenTextExceedsWindow()
        {
            var path = GetTestFilePath("ExceedsWindowHeigth.txt");
            FileManager fileManager = new(path);
            TextFormatter formatter = new(fileManager.Text, 120, 30, 0);

            Assert.Equal(29, formatter.LastDisplayedRowPosition);
        }

        [Fact]
        public void FirstRowPositionReturnsTheStartingConsoleRowPositionOfDisplayedText()
        {
            var path = GetTestFilePath("SingleRow.txt");
            FileManager fileManager = new(path);
            TextFormatter formatter = new(fileManager.Text, 120, 30, 5);

            Assert.Equal(5, formatter.FirstDisplayedRowPosition);
        }

        [Fact]
        public void GetDisplayedRowLengthReturnsTheDisplayedLengthOfARow()
        {
            var path = GetTestFilePath("SingleRow.txt");
            FileManager fileManager = new(path);
            TextFormatter formatter = new(fileManager.Text, 120, 30, 0);

            Assert.Equal(15, formatter.GetDisplayedRowLength(0));
        }

        [Fact]
        public void GetDisplayedRowLengthReturnsTheDisplayedLengthOfARowWhenStartingRowIsLargerThen0()
        {
            var path = GetTestFilePath("SingleRow.txt");
            FileManager fileManager = new(path);
            TextFormatter formatter = new(fileManager.Text, 120, 30, 5);

            Assert.Equal(15, formatter.GetDisplayedRowLength(5));
        }

        [Fact]
        public void GetDisplayedRowLengthReturnsTheDisplayedLengthOfARowWhenRealLengthIsLargerThenWindowWidth()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            FileManager fileManager = new(path);
            TextFormatter formatter = new(fileManager.Text, 120, 30, 0);

            Assert.Equal(119, formatter.GetDisplayedRowLength(0));
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

        [Fact]
        public void ReadWindowTextReturnsTheVisibleTextFowConsoleWhenTextExeedsWindowHeigthAndWidth()
        {
            var path = GetTestFilePath("ExceedsWindowHeigthAndWidth.txt");
            FileManager manager = new(path);
            TextFormatter formatter = new(manager.Text, 120, 30, 0);

            Assert.Equal("01. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "02. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "03. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "04. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "05. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "06. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "07. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "08. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "09. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "10. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "11. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "12. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "13. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "14. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "15. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "16. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "17. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "18. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "19. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "20. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "21. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "22. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "23. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "24. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "25. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "26. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "27. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "28. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "29. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n" +
                "30. This is a very long line that exceeds the console width of 120 characters. This is a very long line that exceeds th\r\n", formatter.FormatWithinWindow());
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
