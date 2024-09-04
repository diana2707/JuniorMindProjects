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
            this.stream = stream;

            aes = Aes.Create();
            key = aes.Key;
            iv = aes.IV;
        }

        public void WriteToStream(string text, bool compress = false, bool encrypt = false)
        {
            ValidateForWriting(text);

            Stream decoratedStream = stream;

            if (compress)
            {
                decoratedStream = new GZipStream(decoratedStream, CompressionMode.Compress, true);
            }

            if (encrypt)
            {
                decoratedStream = new CryptoStream(decoratedStream, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write, true);
            }

            StreamWriter writer = new StreamWriter(decoratedStream);
            writer.Write(text);
            writer.Flush();

            Flush(decoratedStream);
            stream.Position = 0;
        }

        public string ReadFromStream(bool decompress = false, bool decrypt = false)
        {
            ValidateForReading();
            stream.Position = 0;
            Stream decoratedStream = stream;
            MemoryStream outputStream = new MemoryStream();

            if (decompress)
            {
                decoratedStream = new GZipStream(decoratedStream, CompressionMode.Decompress, true);
            }

            if (decrypt)
            {
                decoratedStream = new CryptoStream(decoratedStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read, true);
            }

            decoratedStream.CopyTo(outputStream);

            return Encoding.UTF8.GetString(outputStream.ToArray());
        }

        private static void Flush(Stream decoratedStream)
        {
            if (decoratedStream is CryptoStream cryptoStream)
            {
                cryptoStream.FlushFinalBlock();
            }

            if (decoratedStream is GZipStream gzipStream)
            {
                gzipStream.Flush();
            }
        }

        private void ValidateForWriting(string text)
        {
            ArgumentNullException.ThrowIfNull(text);

            if (stream.CanWrite)
            {
                return;
            }

            throw new NotSupportedException("Stream does not support reading.");
        }

        private void ValidateForReading()
        {
            if (stream.CanRead)
            {
                return;
            }

            throw new NotSupportedException("Stream does not support reading.");
        }
    }
}
