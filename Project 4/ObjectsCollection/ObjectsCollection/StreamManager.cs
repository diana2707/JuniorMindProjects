using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using Xunit.Sdk;

namespace ObjectsCollection
{
    public class StreamManager
    {
        readonly Stream stream;
        private byte[] data;

        public StreamManager(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);
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
               DecryptStream(decryptionProvider);
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
            using (GZipStream gzipStream = compressionProvider.Create(compressedStream))
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
            using MemoryStream decompressedStream = new ();
            using (MemoryStream compressedStream = new (data))
            {
                using GZipStream gzipStream = decompressionProvider.Create(compressedStream);
                gzipStream.CopyTo(decompressedStream);
            }

            data = decompressedStream.ToArray();
        }

        private void DecryptStream(DecryptionProvider decryptionProvider)
        {
            using MemoryStream decryptedStream = new ();
            using (MemoryStream encryptedStream = new (data))
            {
                using CryptoStream cryptoStream = decryptionProvider.Create(encryptedStream);
                cryptoStream.CopyTo(decryptedStream);
            }

            data = decryptedStream.ToArray();
        }
    }
}
