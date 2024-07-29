using System;
using System.IO;
using System.IO.Compression;

namespace ObjectsCollection
{
    public class GZipStreamDecompressProvider : IGZipStreamProvider
    {
        public GZipStream Create(Stream stream)
        {
            return new GZipStream(stream, CompressionMode.Decompress);
        }
    }
}
