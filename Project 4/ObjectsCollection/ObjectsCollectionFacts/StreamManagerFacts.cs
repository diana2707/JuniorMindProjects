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
            StreamManager streamManager = new StreamManager(stream);
            const string text = "This is a text.";

            streamManager.WriteToStream(text, true);

            using GZipStream decompressedStream = new GZipStream(stream, CompressionMode.Decompress);
            using StreamReader reader = new StreamReader(decompressedStream);
            string decompressedText = reader.ReadToEnd();
            Assert.Equal(text, decompressedText);
        }

        //[Fact]
        //public void WriteToStreamShouldWriteCompressedFileStreamWhenParameterIsNotNull()
        //{
        //    const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
        //    using Stream fileStream = new FileStream(path, FileMode.OpenOrCreate);
        //    CompressionProvider compressProvider = new CompressionProvider();
        //    StreamManager streamManager = new StreamManager(fileStream);
        //    const string text = "This is a text.";

        //    streamManager.WriteToStream(text, compressProvider);

        //    DecompressionProvider decompressProvider = new DecompressionProvider();
        //    using StreamReader reader = new StreamReader(decompressProvider.Create(fileStream));
        //    Assert.Equal(text, reader.ReadToEnd());
        //}

        //[Fact]
        //public void ReadFromStreamShouldDecompressAndReadMemoryStreamWhenParameterIsNotNull()
        //{
        //    using Stream stream = new MemoryStream();

        //    CompressionProvider compressProvider = new CompressionProvider();
        //    StreamManager streamManager = new StreamManager(stream);
        //    const string text = "This is a text.";
        //    streamManager.WriteToStream(text, compressProvider);

        //    DecompressionProvider decompressProvider = new DecompressionProvider();
        //    Assert.Equal(text, streamManager.ReadFromStream(decompressProvider));
        //}

        //[Fact]
        //public void ReadFromStreamShouldDecompressAndReadFileStreamWhenParameterIsNotNull()
        //{
        //    const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
        //    using Stream fileStream = new FileStream(path, FileMode.Create);

        //    CompressionProvider compressProvider = new CompressionProvider();
        //    StreamManager streamManager = new StreamManager(fileStream);
        //    const string text = "This is a text.";
        //    streamManager.WriteToStream(text, compressProvider);

        //    DecompressionProvider decompressProvider = new DecompressionProvider();
        //    Assert.Equal(text, streamManager.ReadFromStream(decompressProvider));
        //}

        //[Fact]
        //public void WriteToStreamShouldWriteTextToAMemoryStreamAndEncriptWhenParameterIsNotNull()
        //{
        //    using Stream memoryStream = new MemoryStream();
        //    StreamManager streamManager = new StreamManager(memoryStream);
        //    const string text = "This is an example.";
        //    AESProvider aesProvider = new AESProvider();
        //    EncryptionProvider encryptionProvider = new EncryptionProvider(aesProvider);

        //    streamManager.WriteToStream(text, encryptionProvider: encryptionProvider);

        //    DecryptionProvider decryptionProvider = new DecryptionProvider(aesProvider);
        //    Assert.Equal(text, streamManager.ReadFromStream(decryptionProvider: decryptionProvider));
        //}

        //[Fact]
        //public void WriteToStreamShouldWriteTextToAFileStreamAndEncriptWhenParameterIsNotNull()
        //{
        //    const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
        //    using Stream fileStream = new FileStream(path, FileMode.Create);
        //    StreamManager streamManager = new StreamManager(fileStream);
        //    const string text = "This is an example.";
        //    AESProvider aesProvider = new AESProvider();
        //    EncryptionProvider encryptionProvider = new EncryptionProvider(aesProvider);

        //    streamManager.WriteToStream(text, encryptionProvider: encryptionProvider);

        //    DecryptionProvider decryptionProvider = new DecryptionProvider(aesProvider);
        //    Assert.Equal(text, streamManager.ReadFromStream(decryptionProvider: decryptionProvider));
        //}

        //[Fact]
        //public void ReadFromStreamShouldDecryptAndReadMemoryStreamWhenParameterIsNotNull()
        //{
        //    using Stream memoryStream = new MemoryStream();
        //    StreamManager streamManager = new StreamManager(memoryStream);
        //    const string text = "This is an example.";
        //    AESProvider encriptorDecriptorProvider = new AESProvider();
        //    EncryptionProvider encryptionProvider = new EncryptionProvider(encriptorDecriptorProvider);

        //    streamManager.WriteToStream(text, encryptionProvider: encryptionProvider);

        //    DecryptionProvider decryptionProvider = new DecryptionProvider(encriptorDecriptorProvider);
        //    Assert.Equal(text, streamManager.ReadFromStream(decryptionProvider: decryptionProvider));
        //}

        //[Fact]
        //public void ReadFromStreamShouldDecryptAndReadFileStreamWhenParameterIsNotNull()
        //{
        //    const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
        //    using Stream fileStream = new FileStream(path, FileMode.Create);
        //    StreamManager streamManager = new StreamManager(fileStream);
        //    const string text = "This is an encripted example.";
        //    AESProvider encriptorDecriptorProvider = new AESProvider();
        //    EncryptionProvider encryptionProvider = new EncryptionProvider(encriptorDecriptorProvider);

        //    streamManager.WriteToStream(text, encryptionProvider: encryptionProvider);

        //    DecryptionProvider decryptionProvider = new DecryptionProvider(encriptorDecriptorProvider);
        //    Assert.Equal(text, streamManager.ReadFromStream(decryptionProvider: decryptionProvider));
        //}

        //[Fact]
        //public void WriteToStreamShouldWriteTextToAMemoryStreamAndCompressAndEncriptWhenParametersAreNotNull()
        //{
        //    using Stream memoryStream = new MemoryStream();
        //    StreamManager streamManager = new StreamManager(memoryStream);
        //    const string text = "This is an example.";

        //    CompressionProvider compressionProvider = new CompressionProvider();

        //    AESProvider aesProvider = new AESProvider();
        //    EncryptionProvider encryptionProvider = new EncryptionProvider(aesProvider);

        //    streamManager.WriteToStream(text, compressionProvider, encryptionProvider);

        //    DecryptionProvider decryptionProvider = new DecryptionProvider(aesProvider);
        //    DecompressionProvider decompressionProvider = new DecompressionProvider();
        //    Assert.Equal(text, streamManager.ReadFromStream(decompressionProvider, decryptionProvider));
        //}

        //[Fact]
        //public void WriteToStreamShouldWriteTextToAFileStreamAndCompressAndEncriptWhenParametersAreNotNull()
        //{
        //    const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
        //    using Stream fileStream = new FileStream(path, FileMode.Create);
        //    StreamManager streamManager = new StreamManager(fileStream);
        //    const string text = "This is an example.";

        //    CompressionProvider compressionProvider = new CompressionProvider();

        //    AESProvider aesProvider = new AESProvider();
        //    EncryptionProvider encryptionProvider = new EncryptionProvider(aesProvider);

        //    streamManager.WriteToStream(text, compressionProvider, encryptionProvider);

        //    DecryptionProvider decryptionProvider = new DecryptionProvider(aesProvider);
        //    DecompressionProvider decompressionProvider = new DecompressionProvider();
        //    Assert.Equal(text, streamManager.ReadFromStream(decompressionProvider, decryptionProvider));
        //}

        //[Fact]
        //public void StreamManagerShouldReturnArgumentNullExceptionWhenStreamIsNull()
        //{
        //    using MemoryStream memoryStream = null;

        //    Assert.Throws<ArgumentNullException>(() => new StreamManager(memoryStream));
        //}

        //[Fact]
        //public void WriteToStreamShouldWriteTextToAMemoryStreamAndCompressAndEncriptWhenParametersAreNotNullmmmm()
        //{
        //    using Stream memoryStream = new MemoryStream();
        //    StreamManager streamManager = new StreamManager(memoryStream);

        //    Assert.Throws<ArgumentNullException>(() => streamManager.WriteToStream(null));
        //}

        //[Fact]
        //public void StreamManagerShouldReturnObjectDisposedExceptionWhenStreamIsDisposed()
        //{
        //    using MemoryStream memoryStream = new ();
        //    memoryStream.Dispose();

        //    Assert.Throws<ObjectDisposedException>(() => new StreamManager(memoryStream));
        //}

        //[Fact]
        //public void StreamManagerShouldReturnObjectDisposedExceptionWhenStreamIsClosed()
        //{
        //    using MemoryStream memoryStream = new ();
        //    memoryStream.Close();

        //    Assert.Throws<ObjectDisposedException>(() => new StreamManager(memoryStream));
        //}

        //[Fact]
        //public void StreamManagerShouldReturnNotSupportedExceptionWhenCanSeekIsFalse()
        //{
        //    using GZipStream gzipStream = new (new MemoryStream(), CompressionLevel.Optimal);

        //    Assert.Throws<NotSupportedException>(() => new StreamManager(gzipStream));
        //}

        //[Fact]
        //public void ReadFromStreamShouldThrowNotSupportedExceptionWhenCanReadIsFalse()
        //{
        //    const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
        //    using Stream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
        //    StreamManager streamManager = new StreamManager(fileStream);
        //    const string text = "This is an example.";
        //    streamManager.WriteToStream(text);

        //    Assert.Throws<NotSupportedException>(() => streamManager.ReadFromStream());
        //}

        //[Fact]
        //public void WriteToStreamShouldThrowNotSupportedExceptionWhenCanWriteIsFalse()
        //{
        //    const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
        //    using Stream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
        //    StreamManager streamManager = new StreamManager(fileStream);
        //    const string text = "This is an example.";

        //    Assert.Throws<NotSupportedException>(() => streamManager.WriteToStream(text));
        //}
    }
}
