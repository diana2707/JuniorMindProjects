using System;
using System.IO.Compression;

namespace ObjectsCollection
{
    public interface IGZipStreamProvider
    {
        GZipStream Create(Stream stream);
    }
}
