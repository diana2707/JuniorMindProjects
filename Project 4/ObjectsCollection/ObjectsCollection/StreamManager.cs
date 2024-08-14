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

        public void WriteToStream(string text, bool compress = false, EncryptionProvider encryptionProvider = null)
        {
            ValidateForWriting(text, stream);

            data = Encoding.UTF8.GetBytes(text);

            if (compress)
            {
                CompressStream();
            }

            if (encryptionProvider != null)
            {
               EncryptStream(encryptionProvider);
            }

            stream.Write(data, 0, data.Length);
            stream.Position = 0;
        }

        public string ReadFromStream(bool decompress = false, DecryptionProvider decryptionProvider = null)
        {
            ValidateForReading(stream);
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

            if (decompress)
            {
                DecompressStream();
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
            catch (NotSupportedException)
            {
                throw new NotSupportedException("The stream is not seekable");
            }
            catch (IOException)
            {
                throw new IOException("The stream can not be managed correctly.");
            }
        }

        private static void ValidateForWriting(string text, Stream stream)
        {
            ArgumentNullException.ThrowIfNull(text);

            if (stream.CanWrite)
            {
                return;
            }

            throw new NotSupportedException("Stream does not support reading.");
        }

        private static void ValidateForReading(Stream stream)
        {
            if (stream.CanRead)
            {
                return;
            }

            throw new NotSupportedException("Stream does not support reading.");
        }

        private void CompressStream()
        {
            using MemoryStream compressedStream = new ();
            using (GZipStream gzipStream = new GZipStream(compressedStream, CompressionMode.Compress))
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

        private void DecompressStream()
        {
            using MemoryStream resultStream = new ();
            using (MemoryStream decompressedStream = new (data))
            {
                using GZipStream gzipStream = new GZipStream(decompressedStream, CompressionMode.Decompress);
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
