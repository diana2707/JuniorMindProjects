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

        //ia in considerare pozitia initaila
        public string ReadWindowText(int width, int heigth, int initialRowPosition)
        {
            string result = string.Empty;

            heigth = LinesCount < heigth - initialRowPosition ? LinesCount : heigth - initialRowPosition;

            for(int i = 0; i < heigth; i++)
            {
                result += (buffer[i].Length > width ? buffer[i][..width] : buffer[i]) + "\r\n";
            }

            return result;
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
