namespace TextEditor.Domain
{
    public class FileManager
    {
        readonly string path;

        public FileManager(string path)
        {
            ValidatePath(path);

            this.path = path;
        }

        public string ReadFile()
        {
            return File.ReadAllText(path);
        }

        private static void ValidatePath(string path)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(path);

            if (!File.Exists(path))
            {
                throw new ArgumentException("The path is not valid.");
            }
        }
    }
}
