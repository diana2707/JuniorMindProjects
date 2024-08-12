using System;
using System.IO.Compression;

namespace ObjectsCollection
{
    public class CompressionProviderFacts
    {
        [Fact]
        public void ShouldReturnCompressedGZipStream()
        {
            using Stream stream = new MemoryStream();
            CompressionProvider compressionProvider = new CompressionProvider();
            using Stream compressedStream = compressionProvider.Create(stream);
            Assert.IsType<GZipStream>(compressedStream);
        }
    }
}
