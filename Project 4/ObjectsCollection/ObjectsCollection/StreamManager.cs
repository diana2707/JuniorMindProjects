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
            byte[] data = Encoding.UTF8.GetBytes(text);
            stream.Write(data, 0, data.Length);
            stream.Flush();
        }

        public string ReadFromStream()
        {
            byte[] data = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(data, 0, data.Length);
            return Encoding.UTF8.GetString(data);
        }
    }
}
