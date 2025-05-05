namespace TextEditor.Domain
{
    public class FileManager
    {
        readonly string path;

        public FileManager(string path)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(path);

            if (!File.Exists(path))
            {
                throw new ArgumentException(path);
            }

            this.path = path;
        }

        public string OpenFile()
        {
            return File.ReadAllText(path);
        }
    }
}
