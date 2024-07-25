using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ObjectsCollection
{
    public class StreamManager
    {
        readonly Stream stream;

        public StreamManager(Stream stream)
        {
            this.stream = stream;
        }

        public void WriteToStream(string text)
        {
            byte[] data = ConvertToByteArray(text);

            stream.Write(data, 0, data.Length);
            stream.Dispose();
        }

        private static byte[] ConvertToByteArray(string text)
        {
            byte[] data = new byte[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                data[i] = (byte)text[i];
            }

            return data;
        }
    }
}
