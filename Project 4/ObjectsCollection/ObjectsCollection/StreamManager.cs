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

        public void WriteToStream(string text, GZipStreamCompressProvider compressProvider = null)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);

            if (compressProvider != null)
            {
                using GZipStream compressedStream = compressProvider.Create(stream);
                compressedStream.Write(data, 0, data.Length);
            }
            else
            {
                stream.Write(data, 0, data.Length);
            }

            stream.Flush();
        }

        public string ReadFromStream(GZipStreamDecompressProvider decompressProvider = null)
        {
            using MemoryStream resultStream = new MemoryStream();

            stream.Position = 0;
            using Stream decompressedStream = decompressProvider != null ? decompressProvider.Create(stream) : stream;
            decompressedStream.CopyTo(resultStream);

            byte[] data = resultStream.ToArray();
            return Encoding.UTF8.GetString(data);
        }
    }
}
