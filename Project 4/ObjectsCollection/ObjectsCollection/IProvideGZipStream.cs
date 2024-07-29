using System;
using System.IO.Compression;

namespace ObjectsCollection
{
    public interface IProvideGZipStream
    {
        GZipStream Create(Stream stream);
    }
}
