using System;
using System.Security.Cryptography;

namespace ObjectsCollection
{
    public interface IDecryptionProvider
    {
        CryptoStream Create(Stream stream, AESParameters aesParameters);
    }
}
