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

            if (compress && encrypt)
            {
                WriteCompressedAndEncrypted();
                return;
            }

            if (compress)
            {
                WriteCompressed();
                return;
            }

            if (encrypt)
            {
                WriteEncrypted();
                return;
            }

            stream.Write(data, 0, data.Length);
        }

        public string ReadFromStream(bool decompress = false, bool decrypt = false)
        {
            stream.Position = 0;
            if (decompress && decrypt)
            {
                return ReadDecompressedAndDecrypted();
            }

            if (decompress)
            {
                return ReadDecompressed();
            }

            if (decrypt)
            {
                return ReadDecrypted();
            }

            using MemoryStream outputStream = new MemoryStream();
            stream.CopyTo(outputStream);
            return Encoding.UTF8.GetString(outputStream.ToArray());
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

        private void WriteCompressedAndEncrypted()
        {
            using CryptoStream cryptoStream = new CryptoStream(stream, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write, true);
            using GZipStream gzipStream = new GZipStream(cryptoStream, CompressionMode.Compress, true);
            gzipStream.Write(data, 0, data.Length);
        }

        private void WriteCompressed()
        {
            using GZipStream gzipStream = new GZipStream(stream, CompressionMode.Compress, true);
            gzipStream.Write(data, 0, data.Length);
        }

        private void WriteEncrypted()
        {
            using CryptoStream cryptoStream = new CryptoStream(stream, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write, true);
            cryptoStream.Write(data, 0, data.Length);
        }

        private string ReadDecompressedAndDecrypted()
        {
            using MemoryStream outputStream = new MemoryStream();

            using CryptoStream cryptoStream = new CryptoStream(stream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read, true);
            using GZipStream gzipStream = new GZipStream(cryptoStream, CompressionMode.Decompress, true);

            gzipStream.CopyTo(outputStream);
            data = outputStream.ToArray();
            return Encoding.UTF8.GetString(outputStream.ToArray());
        }

        private string ReadDecompressed()
        {
            using MemoryStream outputStream = new MemoryStream();
            using GZipStream gzipStream = new GZipStream(stream, CompressionMode.Decompress, true);

            gzipStream.CopyTo(outputStream);
            return Encoding.UTF8.GetString(outputStream.ToArray());
        }

        private string ReadDecrypted()
        {
            using MemoryStream tempStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(stream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read, true);

            cryptoStream.CopyTo(tempStream);
            return Encoding.UTF8.GetString(tempStream.ToArray());
        }



        //private byte[] ConvertStreamToBytes()
        //{
        //    using MemoryStream dataStream = new ();
        //    stream.CopyTo(dataStream);
        //    return dataStream.ToArray();
        //}

        //private void CompressStream()
        //{
        //    using MemoryStream compressedStream = new ();
        //    using (GZipStream gzipStream = new (compressedStream, CompressionMode.Compress))
        //    {
        //        gzipStream.Write(data, 0, data.Length);
        //    }

        //    data = compressedStream.ToArray();
        //}

        //private void EncryptStream()
        //{
        //    using MemoryStream encryptedStream = new ();
        //    using (CryptoStream cryptoStream =
        //        new (encryptedStream, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write))
        //    {
        //        cryptoStream.Write(data, 0, data.Length);
        //    }

        //    data = encryptedStream.ToArray();
        //}

        //private void DecompressStream()
        //{
        //    using MemoryStream resultStream = new ();
        //    using (MemoryStream decompressedStream = new (data))
        //    {
        //        using GZipStream gzipStream = new (decompressedStream, CompressionMode.Decompress);
        //        gzipStream.CopyTo(resultStream);
        //    }

        //    data = resultStream.ToArray();
        //}

        //private void DecryptStream()
        //{
        //    using MemoryStream resultStream = new ();
        //    using (MemoryStream decryptedStream = new (data))
        //    {
        //        using CryptoStream cryptoStream =
        //            new (decryptedStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read);
        //        cryptoStream.CopyTo(resultStream);
        //    }

        //    data = resultStream.ToArray();
        //}
    }
}
