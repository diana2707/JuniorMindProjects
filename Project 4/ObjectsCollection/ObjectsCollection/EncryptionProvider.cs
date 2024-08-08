using System;
using System.Security.Cryptography;

namespace ObjectsCollection
{
    public class EncryptionProvider : ICryptoStreamProvider
    {
        readonly EncryptorDecryptorProvider encryptorProvider;

        public EncryptionProvider(EncryptorDecryptorProvider encryptorProvider)
        {
            this.encryptorProvider = encryptorProvider;
        }

        public CryptoStream Create(Stream stream)
        {
            return new CryptoStream(stream, encryptorProvider.CreateEncryptor(), CryptoStreamMode.Write);
        }
    }
}
