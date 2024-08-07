using System;
using System.Security.Cryptography;

namespace ObjectsCollection
{
    public class EncryptionProvider : IEncryptionProvider
    {
        readonly Aes aes;

        public EncryptionProvider()
        {
            aes = Aes.Create();
        }

        public CryptoStream Create(Stream stream)
        {
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            return new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
        }
    }
}
