using TextEditor.Domain.Editor;

namespace TextEditor.Test.EditorTests
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
            Assert.Equal("This is a test.", manager.ReadFile());
        }

        [Fact]
        public void LoadFileLoadsFileInBuffer()
        {
            var path = GetTestFilePath("MultipleRows.txt");
            FileManager manager = new(path);
            Assert.Equal("This is a test.\r\nThis is the second test.\r\nThis is another test.\r\n" +
                "This is the last line of test.", manager.ReadFile());
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
        public void TextReturnTheTextLoadedInTheBuffer()
        {
            var path = GetTestFilePath("SingleRow.txt");
            FileManager manager = new(path);
            Assert.Equal(["This is a test."], manager.Text);
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
