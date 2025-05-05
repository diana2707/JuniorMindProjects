using TextEditor.Domain;

namespace TextEditor.Test
{
    public class FileManagerFacts
    {
        [Fact]
        public void ManagerObjectIsTypeFileManager()
        {
            var path = GetTestFilePath("short_text.txt");
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
        public void OpenFileReturnsTextInFileAsString()
        {
            var path = GetTestFilePath("short_text.txt");
            FileManager manager = new FileManager(path);
            Assert.Equal("This is a test.", manager.OpenFile());
        }

        private static string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
