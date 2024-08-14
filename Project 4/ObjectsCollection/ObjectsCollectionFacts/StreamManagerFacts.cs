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

        [Fact]
        public void WriteToStreamShouldWriteCompressedFileStreamWhenParameterIsNotNull()
        {
            const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
            using Stream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            StreamManager streamManager = new StreamManager(fileStream);
            const string text = "This is a text.";

            streamManager.WriteToStream(text, true);

            using GZipStream decompressedStream = new GZipStream(fileStream, CompressionMode.Decompress);
            using StreamReader reader = new StreamReader(decompressedStream);
            Assert.Equal(text, reader.ReadToEnd());
        }

        [Fact]
        public void ReadFromStreamShouldDecompressAndReadMemoryStreamWhenParameterIsNotNull()
        {
            using Stream stream = new MemoryStream();
            StreamManager streamManager = new StreamManager(stream);
            const string text = "This is a text.";
            streamManager.WriteToStream(text, true);

            Assert.Equal(text, streamManager.ReadFromStream(true));
        }

        [Fact]
        public void ReadFromStreamShouldDecompressAndReadFileStreamWhenParameterIsNotNull()
        {
            const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
            using Stream fileStream = new FileStream(path, FileMode.Create);
            StreamManager streamManager = new StreamManager(fileStream);
            const string text = "This is a text.";
            streamManager.WriteToStream(text, true);

            Assert.Equal(text, streamManager.ReadFromStream(true));
        }

        [Fact]
        public void WriteToStreamShouldWriteTextToAMemoryStreamAndEncriptWhenParameterIsNotNull()
        {
            using Stream memoryStream = new MemoryStream();
            StreamManager streamManager = new StreamManager(memoryStream);
            const string text = "This is an example.";

            streamManager.WriteToStream(text, encrypt: true);

            Assert.Equal(text, streamManager.ReadFromStream(decrypt: true));
        }

        [Fact]
        public void WriteToStreamShouldWriteTextToAFileStreamAndEncriptWhenParameterIsNotNull()
        {
            const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
            using Stream fileStream = new FileStream(path, FileMode.Create);
            StreamManager streamManager = new StreamManager(fileStream);
            const string text = "This is an example.";

            streamManager.WriteToStream(text, encrypt: true);

            Assert.Equal(text, streamManager.ReadFromStream(decrypt: true));
        }

        [Fact]
        public void ReadFromStreamShouldDecryptAndReadFileStreamWhenParameterIsNotNull()
        {
            const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
            using Stream fileStream = new FileStream(path, FileMode.Create);
            StreamManager streamManager = new StreamManager(fileStream);
            const string text = "This is an encripted example.";

            streamManager.WriteToStream(text, encrypt: true);

            Assert.Equal(text, streamManager.ReadFromStream(decrypt: true));
        }

        [Fact]
        public void WriteToStreamShouldWriteTextToAMemoryStreamAndCompressAndEncriptWhenParametersAreNotNull()
        {
            using Stream memoryStream = new MemoryStream();
            StreamManager streamManager = new StreamManager(memoryStream);
            const string text = "This is an example.";

            streamManager.WriteToStream(text, true, true);

            Assert.Equal(text, streamManager.ReadFromStream(true, true));
        }

        [Fact]
        public void WriteToStreamShouldWriteTextToAFileStreamAndCompressAndEncriptWhenParametersAreNotNull()
        {
            const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
            using Stream fileStream = new FileStream(path, FileMode.Create);
            StreamManager streamManager = new StreamManager(fileStream);
            const string text = "This is an example.";

            streamManager.WriteToStream(text, true, true);

            Assert.Equal(text, streamManager.ReadFromStream(true, true));
        }

        [Fact]
        public void StreamManagerShouldReturnArgumentNullExceptionWhenStreamIsNull()
        {
            using MemoryStream memoryStream = null;

            Assert.Throws<ArgumentNullException>(() => new StreamManager(memoryStream));
        }

        [Fact]
        public void WriteToStreamShouldWriteTextToAMemoryStreamAndCompressAndEncriptWhenParametersAreNotNullmmmm()
        {
            using Stream memoryStream = new MemoryStream();
            StreamManager streamManager = new StreamManager(memoryStream);

            Assert.Throws<ArgumentNullException>(() => streamManager.WriteToStream(null));
        }

        [Fact]
        public void StreamManagerShouldReturnObjectDisposedExceptionWhenStreamIsDisposed()
        {
            using MemoryStream memoryStream = new ();
            memoryStream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => new StreamManager(memoryStream));
        }

        [Fact]
        public void StreamManagerShouldReturnObjectDisposedExceptionWhenStreamIsClosed()
        {
            using MemoryStream memoryStream = new ();
            memoryStream.Close();

            Assert.Throws<ObjectDisposedException>(() => new StreamManager(memoryStream));
        }

        [Fact]
        public void StreamManagerShouldReturnNotSupportedExceptionWhenCanSeekIsFalse()
        {
            using GZipStream gzipStream = new (new MemoryStream(), CompressionLevel.Optimal);

            Assert.Throws<NotSupportedException>(() => new StreamManager(gzipStream));
        }

        [Fact]
        public void ReadFromStreamShouldThrowNotSupportedExceptionWhenCanReadIsFalse()
        {
            const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
            using Stream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamManager streamManager = new StreamManager(fileStream);
            const string text = "This is an example.";
            streamManager.WriteToStream(text);

            Assert.Throws<NotSupportedException>(() => streamManager.ReadFromStream());
        }

        [Fact]
        public void WriteToStreamShouldThrowNotSupportedExceptionWhenCanWriteIsFalse()
        {
            const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
            using Stream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            StreamManager streamManager = new StreamManager(fileStream);
            const string text = "This is an example.";

            Assert.Throws<NotSupportedException>(() => streamManager.WriteToStream(text));
        }
    }
}
