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
            GZipStreamCompressProvider gzipStreamCompressProvider = new GZipStreamCompressProvider();
            using Stream compressedStream = gzipStreamCompressProvider.Create(stream);
            Assert.IsType<GZipStream>(compressedStream);
        }
    }
}
