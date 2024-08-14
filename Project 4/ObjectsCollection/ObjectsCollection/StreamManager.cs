using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace ObjectsCollection
{
    public class StreamManager
    {
        readonly Stream stream;
        readonly Aes aes;
        readonly byte[] key;
        readonly byte[] iv;
        private byte[] data;

        public StreamManager(Stream stream)
        {
            Validate(stream);

            this.stream = stream;

            aes = Aes.Create();
            key = aes.Key;
            iv = aes.IV;
        }

        public void WriteToStream(string text, bool compress = false, bool encrypt = false)
        {
            ValidateForWriting(text, stream);

            data = ConvertStringToBytes(text);

            if (compress)
            {
                CompressStream();
            }

            if (encrypt)
            {
               EncryptStream();
            }

            stream.Write(data, 0, data.Length);
            stream.Position = 0;
        }

        public string ReadFromStream(bool decompress = false, bool decrypt = false)
        {
            ValidateForReading(stream);

            stream.Position = 0;
            using (MemoryStream dataStream = new ())
            {
                stream.CopyTo(dataStream);
                data = dataStream.ToArray();
            }

            if (decrypt)
            {
               DecryptStream();
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

        private static byte[] ConvertStringToBytes(string text)
        {
            return Encoding.UTF8.GetBytes(text);
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

        private void EncryptStream()
        {
            using MemoryStream encryptedStream = new ();
            using (CryptoStream cryptoStream =
                new CryptoStream(encryptedStream, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write))
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

        private void DecryptStream()
        {
            using MemoryStream resultStream = new ();
            using (MemoryStream decryptedStream = new (data))
            {
                using CryptoStream cryptoStream =
                    new CryptoStream(decryptedStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read);
                cryptoStream.CopyTo(resultStream);
            }

            data = resultStream.ToArray();
        }
    }
}
