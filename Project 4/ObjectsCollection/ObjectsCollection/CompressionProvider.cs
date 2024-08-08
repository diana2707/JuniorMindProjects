using System;
using System.IO;
using System.IO.Compression;

namespace ObjectsCollection
{
    public class CompressionProvider : IGZipStreamProvider
    {
        public GZipStream Create(Stream stream)
        {
            return new GZipStream(stream, CompressionMode.Compress, true);
        }
    }
}
