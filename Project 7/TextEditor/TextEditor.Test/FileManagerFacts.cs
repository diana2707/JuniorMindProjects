using System.Diagnostics;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class FileManagerFacts
    {
        [Fact]
        public void ManagerObjectIsTypeFileManager()
        {
            var path = GetTestFilePath("singleRow.txt");
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
            var path = GetTestFilePath("singleRow.txt");
            FileManager manager = new(path);
            Assert.Equal("This is a test.", manager.ReadFile());
        }

        [Fact]
        public void LoadFileLoadsFileInBuffer()
        {
            var path = GetTestFilePath("multipleRows.txt");
            FileManager manager = new(path);
            Assert.Equal("This is a test.This is a test.This is a test.This is the last line of test.", manager.ReadFile());
        }

        [Fact]
        public void LineCountReturnsTheNumberOfLinesLodedInTheBuffer()
        {

            var path = GetTestFilePath("multipleRows.txt");
            FileManager manager = new(path);
            Assert.Equal(4, manager.LinesCount);
        }

        [Fact]
        public void GetLineLengthReturnsTheNumberOfLinesLodedInTheBuffer()
        {

            var path = GetTestFilePath("multipleRows.txt");
            FileManager manager = new(path);
            int linesCount = manager.LinesCount;
            Assert.Equal(30, manager.GetLineLength(linesCount - 1));
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
