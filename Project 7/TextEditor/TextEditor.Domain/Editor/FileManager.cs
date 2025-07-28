namespace TextEditor.Domain.Editor
{
    public class FileManager
    {
        readonly string path;
        readonly string[] buffer;

        public FileManager(string path)
        {
            ValidatePath(path);

            this.path = path;
            buffer = File.ReadAllLines(path);
        }

        public string[] Text => buffer;

        //ar trebui sters, exista deja in TextManager
        public int RowsCount => buffer.Length;

        //ar trebui sters, exista deja in TextManager
        public int GetRowLength(int index)
        {
            return buffer[index].Length;
        }

        public string ReadFile()
        {
            return File.ReadAllText(path);
        }

        private static void ValidatePath(string path)
        {
            ArgumentException.ThrowIfNullOrEmpty(path);

            if (!File.Exists(path))
            {
                throw new ArgumentException("The path is not valid.");
            }
        }
    }
}
