namespace TextEditor.Domain
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

        public int LinesCount => buffer.Length;

        public int GetLineLength(int index)
        {
            return buffer[index].Length;
        }

        public string ReadFile()
        {
            //return String.Concat(buffer);
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
