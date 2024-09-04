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
            data = Encoding.UTF8.GetBytes(text);
            Stream decoratedStream = stream;

            if (compress)
            {
                decoratedStream = new GZipStream(decoratedStream, CompressionMode.Compress, true);
            }

            if (encrypt)
            {
                decoratedStream = new CryptoStream(decoratedStream, aes.CreateEncryptor(), CryptoStreamMode.Write, true);
            }

            decoratedStream.Write(data, 0, data.Length);
        }

        public string ReadFromStream(bool decompress = false, bool decrypt = false)
        {
            Stream decoratedStream = stream;
            MemoryStream outputStream = new MemoryStream();

            if (decompress)
            {
                decoratedStream = new GZipStream(decoratedStream, CompressionMode.Decompress, true);
            }

            if (decrypt)
            {
                decoratedStream = new CryptoStream(decoratedStream, aes.CreateDecryptor(), CryptoStreamMode.Read, true);
            }

            decoratedStream.CopyTo(outputStream);
            return Encoding.UTF8.GetString(outputStream.ToArray());
        }

            //public void WriteToStream(string text, bool compress = false, bool encrypt = false)
            //{
            //    data = Encoding.UTF8.GetBytes(text);

            //    if (compress && encrypt)
            //    {
            //        WriteCompressedAndEncryptedData();
            //    }

            //    if (compress && !encrypt)
            //    {
            //        WriteCompressedData();
            //    }

            //    if (encrypt && !compress)
            //    {
            //        WriteEncryptedData();
            //    }

            //    if (!compress && !encrypt)
            //    {
            //        WriteData();
            //    }

            //    stream.Position = 0;
            //}

            //    public string ReadFromStream(bool decompress = false, bool decrypt = false)
            //{
            //    stream.Position = 0;
            //    if (decompress && decrypt)
            //    {
            //        return ReadDecompressedAndDecryptedData();
            //    }

            //    if (decompress)
            //    {
            //        return ReadDecompressedData();
            //    }

            //    if (decrypt)
            //    {
            //        return ReadDecryptedData();
            //    }

            //    return ReadData();
            //}

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

        private void WriteCompressedAndEncryptedData()
        {
            using CryptoStream cryptoStream = new CryptoStream(stream, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write, true);
            using GZipStream gzipStream = new GZipStream(cryptoStream, CompressionMode.Compress, true);
            gzipStream.Write(data, 0, data.Length);
        }

        private void WriteCompressedData()
        {
            using GZipStream gzipStream = new GZipStream(stream, CompressionMode.Compress, true);
            gzipStream.Write(data, 0, data.Length);
        }

        private void WriteEncryptedData()
        {
            using CryptoStream cryptoStream = new CryptoStream(stream, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write, true);
            cryptoStream.Write(data, 0, data.Length);
        }

        private void WriteData()
        {
            stream.Write(data, 0, data.Length);
        }

        private string ReadDecompressedAndDecryptedData()
        {
            using MemoryStream outputStream = new MemoryStream();

            using CryptoStream cryptoStream = new CryptoStream(stream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read, true);
            using GZipStream gzipStream = new GZipStream(cryptoStream, CompressionMode.Decompress, true);

            gzipStream.CopyTo(outputStream);
            data = outputStream.ToArray();
            return Encoding.UTF8.GetString(outputStream.ToArray());
        }

        private string ReadDecompressedData()
        {
            using MemoryStream outputStream = new MemoryStream();
            using GZipStream gzipStream = new GZipStream(stream, CompressionMode.Decompress, true);

            gzipStream.CopyTo(outputStream);
            return Encoding.UTF8.GetString(outputStream.ToArray());
        }

        private string ReadDecryptedData()
        {
            using MemoryStream tempStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(stream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read, true);

            cryptoStream.CopyTo(tempStream);
            return Encoding.UTF8.GetString(tempStream.ToArray());
        }

        private string ReadData()
        {
            using MemoryStream outputStream = new MemoryStream();
            stream.CopyTo(outputStream);
            return Encoding.UTF8.GetString(outputStream.ToArray());
        }
    }
}
