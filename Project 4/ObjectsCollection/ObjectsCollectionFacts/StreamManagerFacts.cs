using System.IO;
using System.IO.Compression;

namespace ObjectsCollection
{
    public class StreamManagerFacts
    {
        [Fact]
        public void WriteToStreamShouldWriteTextToAMemoryStream()
        {
            using Stream memoryStream = new MemoryStream();
            StreamManager streamManager = new StreamManager(memoryStream);
            const string text = "This is an example.";

            streamManager.WriteToStream(text);

            memoryStream.Position = 0;
            using StreamReader reader = new StreamReader(memoryStream);
            Assert.Equal(text, reader.ReadToEnd());
        }

        [Fact]
        public void WriteToStreamShouldWriteTextToAFileStream()
        {
            const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
            using Stream fileStream = new FileStream(path, FileMode.Create);
            StreamManager streamManager = new StreamManager(fileStream);
            const string text = "This is an example.";

            streamManager.WriteToStream(text);

            fileStream.Position = 0;
            using StreamReader reader = new StreamReader(fileStream);
            string result = reader.ReadToEnd();
            Assert.Equal(text, result);
        }

        [Fact]
        public void ReadFromStreamShouldReadTextFromAMemoryStream()
        {
            using Stream memoryStream = new MemoryStream();
            StreamManager streamManager = new StreamManager(memoryStream);
            const string text = "This is an example.";
            streamManager.WriteToStream(text);

            Assert.Equal(text, streamManager.ReadFromStream());
        }

        [Fact]
        public void ReadFromStreamShouldReadTextFromAFileStream()
        {
            const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
            using Stream fileStream = new FileStream(path, FileMode.Create);
            StreamManager streamManager = new StreamManager(fileStream);
            const string text = "This is an example.";
            streamManager.WriteToStream(text);

            Assert.Equal(text, streamManager.ReadFromStream());
        }

        [Fact]
        public void WriteToStreamShouldWriteCompressedMemoryStreamWhenParameterIsNotNull()
        {
            using Stream stream = new MemoryStream();
            CompressionProvider compressProvider = new CompressionProvider();
            StreamManager streamManager = new StreamManager(stream);
            const string text = "This is a text.";

            streamManager.WriteToStream(text, compressProvider);

            stream.Position = 0;
            DecompressionProvider decompressProvider = new DecompressionProvider();
            using StreamReader reader = new StreamReader(decompressProvider.Create(stream));
            Assert.Equal(text, reader.ReadToEnd());
        }

        [Fact]
        public void WriteToStreamShouldWriteCompressedFileStreamWhenParameterIsNotNull()
        {
            const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
            using Stream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            CompressionProvider compressProvider = new CompressionProvider();
            StreamManager streamManager = new StreamManager(fileStream);
            const string text = "This is a text.";

            streamManager.WriteToStream(text, compressProvider);

            fileStream.Position = 0;
            DecompressionProvider decompressProvider = new DecompressionProvider();
            using StreamReader reader = new StreamReader(decompressProvider.Create(fileStream));
            Assert.Equal(text, reader.ReadToEnd());
        }

        [Fact]
        public void ReadFromStreamShouldDecompressAndReadMemoryStreamWhenParameterIsNotNull()
        {
            using Stream stream = new MemoryStream();

            CompressionProvider compressProvider = new CompressionProvider();
            StreamManager streamManager = new StreamManager(stream);
            const string text = "This is a text.";
            streamManager.WriteToStream(text, compressProvider);

            DecompressionProvider decompressProvider = new DecompressionProvider();
            Assert.Equal(text, streamManager.ReadFromStream(decompressProvider));
        }

        [Fact]
        public void ReadFromStreamShouldDecompressAndReadFileStreamWhenParameterIsNotNull()
        {
            const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
            using Stream fileStream = new FileStream(path, FileMode.Create);

            CompressionProvider compressProvider = new CompressionProvider();
            StreamManager streamManager = new StreamManager(fileStream);
            const string text = "This is a text.";
            streamManager.WriteToStream(text, compressProvider);

            DecompressionProvider decompressProvider = new DecompressionProvider();
            Assert.Equal(text, streamManager.ReadFromStream(decompressProvider));
        }

        [Fact]
        public void WriteToStreamShouldWriteTextToAMemoryStreamAndEncriptWhenParameterIsNotNull()
        {
            using Stream memoryStream = new MemoryStream();
            StreamManager streamManager = new StreamManager(memoryStream);
            const string text = "This is an example.";
            EncryptorDecryptorProvider encryptor = new EncryptorDecryptorProvider();
            EncryptionProvider encryptionProvider = new EncryptionProvider(encryptor);

            streamManager.WriteToStream(text, encryptionProvider: encryptionProvider);

            memoryStream.Position = 0;
            using StreamReader reader = new StreamReader(memoryStream);

            Assert.True(memoryStream.Length > 0);
            Assert.NotEqual(text, reader.ReadToEnd());
        }

        [Fact]
        public void WriteToStreamShouldWriteTextToAFileStreamAndEncriptWhenParameterIsNotNull()
        {
            const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
            using Stream fileStream = new FileStream(path, FileMode.Create);
            StreamManager streamManager = new StreamManager(fileStream);
            const string text = "This is an example.";
            EncryptorDecryptorProvider encryptor = new EncryptorDecryptorProvider();
            EncryptionProvider encryptionProvider = new EncryptionProvider(encryptor);

            streamManager.WriteToStream(text, encryptionProvider: encryptionProvider);

            fileStream.Position = 0;
            using StreamReader reader = new StreamReader(fileStream);

            Assert.True(fileStream.Length > 0);
            Assert.NotEqual(text, reader.ReadToEnd());
        }

        //[Fact]
        //public void ReadFromStreamShouldDecryptAndReadMemoryStreamWhenParameterIsNotNull()
        //{
        //    using Stream memoryStream = new MemoryStream();
        //    StreamManager streamManager = new StreamManager(memoryStream);
        //    const string text = "This is an example.";
        //    CryptoStreamEncryptionProvider encryptionProvider = new CryptoStreamEncryptionProvider();
        //    streamManager.WriteToStream(text, encryptionProvider: encryptionProvider);

        //    CryptoStreamDecryptionProvider decryptionProvider = new CryptoStreamDecryptionProvider();
        //    Assert.Equal(text, streamManager.ReadFromStream(decryptionProvider));
        //}
    }
}
