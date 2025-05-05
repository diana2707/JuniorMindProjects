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

        //[Fact]
        //public void OpenFileReturns()
        //{
        //    FileManager manager = new FileManager();
        //    Assert.Equal("This is a test.", manager.OpenFile());
        //}

        private string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);
        }
    }
}
