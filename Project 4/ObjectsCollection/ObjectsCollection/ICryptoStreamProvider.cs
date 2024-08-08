using System;
using System.Security.Cryptography;

namespace ObjectsCollection
{
    public interface ICryptoStreamProvider
    {
        CryptoStream Create(Stream stream);
    }
}
