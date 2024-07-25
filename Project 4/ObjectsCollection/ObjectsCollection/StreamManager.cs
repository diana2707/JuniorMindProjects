using System.IO;

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
            byte[] data = new byte[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                data[i] = (byte)text[i];
            }

            stream.Write(data, 0, data.Length);
            stream.Dispose();
        }
    }
}
