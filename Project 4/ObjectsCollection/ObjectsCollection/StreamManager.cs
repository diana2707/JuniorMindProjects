using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ObjectsCollection
{
    public class StreamManager
    {
        readonly Stream stream;
        private byte[] data;

        public StreamManager(Stream stream)
        {
            this.stream = stream;
        }

        public void WriteToStream(string text, CompressionProvider compressionProvider = null, EncryptionProvider encryptionProvider = null)
        {
            data = Encoding.UTF8.GetBytes(text);

            if (compressionProvider != null)
            {
                CompressStream(compressionProvider);
            }

            if (encryptionProvider != null)
            {
               EncryptStream(encryptionProvider);
            }

            stream.Write(data, 0, data.Length);
            stream.Position = 0;
        }

        public string ReadFromStream(DecompressionProvider decompressionProvider = null, DecryptionProvider decryptionProvider = null)
        {
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
                DecompressStream(decompressionProvider);
            }

            return Encoding.UTF8.GetString(data);
        }

        private void CompressStream(CompressionProvider compressionProvider)
        {
            using MemoryStream compressedStream = new ();
            using (Stream gzipStream = compressionProvider.Create(compressedStream))
            {
                gzipStream.Write(data, 0, data.Length);
            }

            data = compressedStream.ToArray();
        }

        private void EncryptStream(EncryptionProvider encryptionProvider)
        {
            using MemoryStream encryptedStream = new ();
            using (CryptoStream cryptoStream = encryptionProvider.Create(encryptedStream))
            {
                cryptoStream.Write(data, 0, data.Length);
            }

            data = encryptedStream.ToArray();
        }

        private void DecompressStream(DecompressionProvider decompressionProvider)
        {
            using MemoryStream decompressedStream = new MemoryStream();
            using (MemoryStream compressedStream = new MemoryStream(data))
            {
                using GZipStream gzipStream = decompressionProvider.Create(compressedStream);
                gzipStream.CopyTo(decompressedStream);
            }

            data = decompressedStream.ToArray();
        }
    }
}
