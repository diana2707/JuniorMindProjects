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
                using MemoryStream compressedStream = new ();
                using (Stream gzipStream = compressionProvider.Create(compressedStream))
                {
                    gzipStream.Write(data, 0, data.Length);
                }

                data = compressedStream.ToArray();
            }

            if (encryptionProvider != null)
            {
                using MemoryStream encryptedStream = new ();
                using (CryptoStream cryptoStream = encryptionProvider.Create(encryptedStream))
                {
                    cryptoStream.Write(data, 0, data.Length);
                }

                data = encryptedStream.ToArray();
            }

            stream.Write(data, 0, data.Length);
            stream.Position = 0;
        }

        public string ReadFromStream(DecompressionProvider decompressionProvider = null, DecryptionProvider decryptionProvider = null)
        {
            byte[] data;
            stream.Position = 0;
            using (MemoryStream dataStream = new ())
            {
                stream.CopyTo(dataStream);
                data = dataStream.ToArray();
            }

            if (decryptionProvider != null)
            {
                using MemoryStream decryptedStream = new ();
                using (MemoryStream encryptedStream = new (data))
                {
                    using CryptoStream cryptoStream = decryptionProvider.Create(encryptedStream);
                    cryptoStream.CopyTo(decryptedStream);
                }

                data = decryptedStream.ToArray();
            }

            if (decompressionProvider != null)
            {
                using MemoryStream decompressedStream = new MemoryStream();
                using (MemoryStream compressedStream = new MemoryStream(data))
                {
                    using GZipStream gzipStream = decompressionProvider.Create(compressedStream);
                    gzipStream.CopyTo(decompressedStream);
                }

                data = decompressedStream.ToArray();
            }

            return Encoding.UTF8.GetString(data);
        }
    }
}
