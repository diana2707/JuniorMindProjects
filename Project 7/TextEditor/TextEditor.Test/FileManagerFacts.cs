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
            Assert.Equal("This is a test.\r\n", manager.ReadWindowText(120, 30));
        }

        [Fact]
        public void LoadFileLoadsFileInBuffer()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            Assert.Equal("This is a test.\r\nThis is the second test.\r\nThis is another test.\r\nThis is the last line of test.\r\n", manager.ReadWindowText(120, 30));
        }

        [Fact]
        public void LineCountReturnsTheNumberOfLinesLodedInTheBuffer()
        {

            var path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            Assert.Equal(4, manager.LinesCount);
        }

        [Fact]
        public void GetLineLengthReturnsTheNumberOfLinesLodedInTheBuffer()
        {

            var path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            int linesCount = manager.LinesCount;
            Assert.Equal(30, manager.GetLineLength(linesCount - 1));
        }

        [Fact]
        public void ReadWindowTextReturnsTheVisibleTextFowConsoleWhenRowsAreLongerThenWindowWidth()
        {
            var path = GetTestFilePath("ExceedsWindowLength.txt");
            FileManager manager = new(path);

            Assert.Equal("This is a very long line that exceeds the console width of 130 characters. " +
                "This is a very long line that exceeds the con\r\n", manager.ReadWindowText(120, 30));
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
