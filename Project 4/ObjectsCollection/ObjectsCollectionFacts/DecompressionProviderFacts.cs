using System;
using System.IO.Compression;

namespace ObjectsCollection
{
    public class DecompressionProviderFacts
    {
        [Fact]
        public void ShouldReturnDecompressedGZipStream()
        {
            using Stream stream = new MemoryStream();
            DecompressionProvider gzipStreamDecompressProvider = new DecompressionProvider();
            using Stream decompressedStream = gzipStreamDecompressProvider.Create(stream);
            Assert.IsType<GZipStream>(decompressedStream);
        }
    }
}
