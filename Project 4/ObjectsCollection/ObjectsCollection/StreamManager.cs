using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace ObjectsCollection
{
    public class StreamManager
    {
        readonly Stream stream;
        private byte[] data;

        public StreamManager(Stream stream)
        {
            Validate(stream);

            this.stream = stream;
        }

        public void WriteToStream(string text, CompressionProvider compressionProvider = null, EncryptionProvider encryptionProvider = null)
        {
            ArgumentNullException.ThrowIfNull(text);

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

        private static void Validate(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);

            try
            {
                stream.Position = 0;
            }
            catch (ObjectDisposedException)
            {
                throw new ObjectDisposedException("Object was disposed");
            }
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
            using MemoryStream resultStream = new ();
            using (MemoryStream decompressedStream = new (data))
            {
                using GZipStream gzipStream = decompressionProvider.Create(decompressedStream);
                gzipStream.CopyTo(resultStream);
            }

            data = resultStream.ToArray();
        }

        private void DecryptStream(DecryptionProvider decryptionProvider)
        {
            using MemoryStream resultStream = new ();
            using (MemoryStream decryptedStream = new (data))
            {
                using CryptoStream cryptoStream = decryptionProvider.Create(decryptedStream);
                cryptoStream.CopyTo(resultStream);
            }

            data = resultStream.ToArray();
        }
    }
}
