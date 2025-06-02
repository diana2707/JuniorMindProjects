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

        public string[] Text => buffer;

        public int RowsCount => buffer.Length;

        public int GetRowLength(int index)
        {
            return buffer[index].Length;
        }

        //mut logica de display in alta clasa?!!!!

        //ia in considerare pozitia initaila
        //muta logica in constructor pt buffer
        public string ReadWindowText(int width, int heigth, int startingRow)
        {
            string result = string.Empty;

            heigth = RowsCount < heigth - startingRow ? RowsCount : heigth - startingRow;

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
