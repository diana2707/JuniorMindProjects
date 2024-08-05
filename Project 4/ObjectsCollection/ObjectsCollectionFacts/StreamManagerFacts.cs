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

        //[Fact]
        //public void ReadFromStreamShouldReadTextFromAMemoryStream()
        //{
        //    using Stream memoryStream = new MemoryStream();
        //    StreamManager streamManager = new StreamManager(memoryStream);
        //    const string text = "This is an example.";
        //    streamManager.WriteToStream(text);

        //    Assert.Equal(text, streamManager.ReadFromStream());
        //}

        //[Fact]
        //public void ReadFromStreamShouldReadTextFromAFileStream()
        //{
        //    const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
        //    using Stream fileStream = new FileStream(path, FileMode.OpenOrCreate);
        //    StreamManager streamManager = new StreamManager(fileStream);
        //    const string text = "This is an example.";
        //    streamManager.WriteToStream(text);

        //    Assert.Equal(text, streamManager.ReadFromStream());
        //}

        [Fact]
        public void WriteToStreamShouldWriteCompressedMemoryStreamWhenParameterIsNotNull()
        {
            using Stream stream = new MemoryStream();
            GZipStreamCompressProvider compressProvider = new GZipStreamCompressProvider();
            StreamManager streamManager = new StreamManager(stream);
            const string text = "This is a text.";

            streamManager.WriteToStream(text, compressProvider);

            stream.Position = 0;
            GZipStreamDecompressProvider decompressProvider = new GZipStreamDecompressProvider();
            using StreamReader reader = new StreamReader(decompressProvider.Create(stream));
            Assert.Equal(text, reader.ReadToEnd());
        }

        [Fact]
        public void WriteToStreamShouldWriteCompressedFileStreamWhenParameterIsNotNull()
        {
            const string path = "C:\\Users\\Diana\\OneDrive\\Desktop\\my_file.txt";
            using Stream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            GZipStreamCompressProvider compressProvider = new GZipStreamCompressProvider();
            StreamManager streamManager = new StreamManager(fileStream);
            const string text = "This is a text.";

            streamManager.WriteToStream(text, compressProvider);

            fileStream.Position = 0;
            GZipStreamDecompressProvider decompressProvider = new GZipStreamDecompressProvider();
            using StreamReader reader = new StreamReader(decompressProvider.Create(fileStream));
            Assert.Equal(text, reader.ReadToEnd());
        }

        //[Fact]
        //public void ReadFromStreamShouldDecompressTheCompressedStream()
        //{
        //    using Stream stream = new MemoryStream();

        //    GZipStreamCompressProvider compressProvider = new GZipStreamCompressProvider();
        //    StreamManager streamManager = new StreamManager(stream);
        //    const string text = "This is a text.";
        //    streamManager.WriteToStream(text, compressProvider);

        //    GZipStreamDecompressProvider decompressProvider = new GZipStreamDecompressProvider();
        //    StreamManager streamManager1 = new StreamManager(stream);
        //    Assert.Equal(text, streamManager1.ReadFromStream(decompressProvider));
        //}
    }
}
