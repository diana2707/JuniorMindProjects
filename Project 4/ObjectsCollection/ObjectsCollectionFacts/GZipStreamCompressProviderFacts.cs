using System;
using System.IO.Compression;

namespace ObjectsCollection
{
    public class GZipStreamCompressProviderFacts
    {
        [Fact]
        public void ShouldReturnCompressedGZipStream()
        {
            using Stream stream = new MemoryStream();
            CompressionProvider gzipStreamCompressProvider = new CompressionProvider();
            using Stream compressedStream = gzipStreamCompressProvider.Create(stream);
            Assert.IsType<GZipStream>(compressedStream);
        }
    }
}
