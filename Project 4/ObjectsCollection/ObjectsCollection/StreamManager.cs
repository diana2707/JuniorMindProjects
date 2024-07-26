using System.IO;
using System.IO.Compression;
using System.Text;

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
            stream.Flush();
        }

        public string ReadFromStream()
        {
            byte[] data = new byte[stream.Length];
            stream.Position = 0;
            int bytesRead = stream.Read(data, 0, data.Length);
            return Encoding.UTF8.GetString(data);
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
