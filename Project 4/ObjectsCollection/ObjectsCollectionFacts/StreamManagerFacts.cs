using System.IO;

namespace ObjectsCollection
{
    public class StreamManagerFacts
    {
        [Fact]
        public static void StreamFacts1()
        {
            Stream memoryStream = new MemoryStream();
            StreamManager streamManager = new StreamManager(memoryStream);
            streamManager.WriteToStream("This is an example.");
            Assert.NotNull(memoryStream);
        }
    }
}
