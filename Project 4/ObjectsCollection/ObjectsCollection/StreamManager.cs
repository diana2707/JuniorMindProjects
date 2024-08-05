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

        //public string ReadFromStream(GZipStreamDecompressProvider decompressProvider = null)
        //{
        //    stream.Position = 0;
        //    Stream decompressedStream = decompressProvider != null ? decompressProvider.Create(stream) : stream;
        //    resultStream = new MemoryStream();
        //    byte[] buffer = new byte[4096];
        //    int bytesRead;

        //    while ((bytesRead = decompressedStream.Read(buffer, 0, buffer.Length)) > 0)
        //    {
        //        resultStream.Write(buffer, 0, bytesRead);
        //    }

        //    byte[] data = resultStream.ToArray();

        //    decompressedStream.Dispose();
        //    return Encoding.UTF8.GetString(data);
        //}
    }
}
