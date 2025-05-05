namespace TextEditor.Domain
{
    public class FileManager
    {
        string path;

        public FileManager(string path)
        {
            this.path = path;
        }

        public string OpenFile()
        {
            return File.ReadAllText(path);
        }
    }
}
