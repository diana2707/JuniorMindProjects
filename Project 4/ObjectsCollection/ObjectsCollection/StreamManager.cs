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
            ValidateStream();

            aes = Aes.Create();
            key = aes.Key;
            iv = aes.IV;
        }

        public void WriteToStream(string text, bool compress = false, bool encrypt = false)
        {
            ValidateForWriting(text);
            data = Encoding.UTF8.GetBytes(text);
            Stream decoratedStream = stream;

            if (compress)
            {
                decoratedStream = new GZipStream(decoratedStream, CompressionMode.Compress, true);
            }

            if (encrypt)
            {
                decoratedStream = new CryptoStream(decoratedStream, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write, true);
            }

            decoratedStream.Write(data, 0, data.Length);

            Dispose(decoratedStream);
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
            Dispose(decoratedStream);

            return Encoding.UTF8.GetString(outputStream.ToArray());
        }

        private static void Dispose(Stream decoratedStream)
        {
            if (decoratedStream is CryptoStream cryptoStream)
            {
                cryptoStream.Dispose();
            }

            if (decoratedStream is GZipStream gzipStream)
            {
                gzipStream.Dispose();
            }
        }

        private void ValidateStream()
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
