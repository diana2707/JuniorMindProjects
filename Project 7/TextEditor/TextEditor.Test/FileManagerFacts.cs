using System.Diagnostics;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class FileManagerFacts
    {
        [Fact]
        public void ManagerObjectIsTypeFileManager()
        {
            var path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new FileManager(path);
            Assert.IsType<FileManager>(manager);
        }

        [Fact]
        public void ManagerObjectThrowsArgumentNullExceptionWhenPathIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new FileManager(null));
        }

        [Fact]
        public void ManagerObjectThrowsArgumentNullExceptionWhenPathIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => new FileManager(string.Empty));
        }

        [Fact]
        public void ManagerObjectThrowsArgumentNullExceptionWhenPathIsInvalid()
        {
            var path = GetTestFilePath("text.txt");
            Assert.Throws<ArgumentException>(() => new FileManager(path));
        }

        [Fact]
        public void ReadFileReturnsTextInFileAsString()
        {
            var path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new(path);
            Assert.Equal("This is a test.\r\n", manager.ReadWindowText(120, 30, 0));
        }

        [Fact]
        public void LoadFileLoadsFileInBuffer()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            Assert.Equal("This is a test.\r\nThis is the second test.\r\nThis is another test.\r\n" +
                "This is the last line of test.\r\n", manager.ReadWindowText(120, 30, 0));
        }

        [Fact]
        public void LineCountReturnsTheNumberOfLinesLodedInTheBuffer()
        {

            var path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            Assert.Equal(4, manager.RowsCount);
        }

        [Fact]
        public void GetLineLengthReturnsTheNumberOfLinesLodedInTheBuffer()
        {

            var path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            int linesCount = manager.RowsCount;
            Assert.Equal(30, manager.GetRowLength(linesCount - 1));
        }

        [Fact]
        public void ReadWindowTextReturnsTheVisibleTextFowConsoleWhenRowsAreLongerThenWindowWidth()
        {
            var path = GetTestFilePath("ExceedsWindowWidth.txt");
            FileManager manager = new(path);

            Assert.Equal("This is a very long line that exceeds the console width of 130 characters. " +
                "This is a very long line that exceeds the con\r\n", manager.ReadWindowText(120, 30, 0));
        }

        [Fact]
        public void ReadWindowTextReturnsTheVisibleTextFowConsoleWhenRowsAreLongerThenWindowHeigth()
        {
            var path = GetTestFilePath("ExceedsWindowHeigth.txt");
            FileManager manager = new(path);

            Assert.Equal("Line 1\r\nLine 2\r\nLine 3\r\nLine 4\r\nLine 5\r\nLine 6\r\nLine 7\r\nLine 8" +
                "\r\nLine 9\r\nLine 10\r\nLine 11\r\nLine 12\r\nLine 13\r\nLine 14\r\nLine 15\r\nLine 16" +
                "\r\nLine 17\r\nLine 18\r\nLine 19\r\nLine 20\r\nLine 21\r\nLine 22\r\nLine 23\r\nLine 24" +
                "\r\nLine 25\r\nLine 26\r\nLine 27\r\nLine 28\r\nLine 29\r\nLine 30\r\n", manager.ReadWindowText(120, 30, 0));
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
