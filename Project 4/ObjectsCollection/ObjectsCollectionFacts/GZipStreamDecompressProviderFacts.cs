using System;
using System.IO.Compression;

namespace ObjectsCollection
{
    public class GZipStreamDecompressProviderFacts
    {
        [Fact]
        public void ShouldReturnDecompressedGZipStream()
        {
            using Stream stream = new MemoryStream();
            GZipStreamDecompressProvider gzipStreamDecompressProvider = new GZipStreamDecompressProvider();
            using Stream decompressedStream = gzipStreamDecompressProvider.Create(stream);
            Assert.IsType<GZipStream>(decompressedStream);
        }
    }
}
