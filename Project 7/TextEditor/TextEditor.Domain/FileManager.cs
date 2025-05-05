namespace TextEditor.Domain
{
    public class FileManager
    {
        readonly string path;

        public FileManager(string path)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(path);

            this.path = path;
        }

        public string OpenFile()
        {
            return File.ReadAllText(path);
        }
    }
}
