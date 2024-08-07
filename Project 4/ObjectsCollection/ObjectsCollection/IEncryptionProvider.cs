using System;
using System.Security.Cryptography;

namespace ObjectsCollection
{
    public interface IEncryptionProvider
    {
        CryptoStream Create(Stream stream);
    }
}
