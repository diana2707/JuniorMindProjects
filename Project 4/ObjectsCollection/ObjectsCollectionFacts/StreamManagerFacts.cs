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
            using Stream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            StreamManager streamManager = new StreamManager(fileStream);
            const string text = "This is an example.";

            streamManager.WriteToStream(text);

            fileStream.Position = 0;
            using StreamReader reader = new StreamReader(fileStream);
            Assert.Equal(text, reader.ReadToEnd());
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
            using Stream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            StreamManager streamManager = new StreamManager(fileStream);
            const string text = "This is an example.";
            streamManager.WriteToStream(text);

            Assert.Equal(text, streamManager.ReadFromStream());
        }
    }
}
