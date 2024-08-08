using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
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

        public void WriteToStream(string text, CompressionProvider compressionProvider = null, EncryptionProvider encryptionProvider = null)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);

            if (compressionProvider != null)
            {
                using MemoryStream compressedStream = new MemoryStream();
                using GZipStream gzipStream = compressionProvider.Create(stream);
                gzipStream.Write(data, 0, data.Length);
                data = compressedStream.ToArray();
            }

            if (encryptionProvider != null)
            {
                using MemoryStream encryptedStream = new MemoryStream();
                using CryptoStream cryptoStream = encryptionProvider.Create(encryptedStream);
                cryptoStream.Write(data, 0, data.Length);
                data = encryptedStream.ToArray();
            }

            stream.Write(data, 0, data.Length);
            stream.Flush();
        }

        public string ReadFromStream(DecompressionProvider decompressionProvider = null)
        {
            using MemoryStream resStream = new MemoryStream();

            stream.Position = 0;
            using Stream decompressedStream = decompressionProvider != null ? decompressionProvider.Create(stream) : stream;
            decompressedStream.CopyTo(resStream);

            byte[] data = resStream.ToArray();
            return Encoding.UTF8.GetString(data);
        }
    }
}
