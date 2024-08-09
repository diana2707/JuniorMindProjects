using System;
using System.Security.Cryptography;

namespace ObjectsCollection
{
    public class DecryptionProvider : ICryptoStreamProvider
    {
        readonly EncryptorDecryptorProvider decryptorProvider;

        public DecryptionProvider(EncryptorDecryptorProvider decryptorProvider)
        {
            this.decryptorProvider = decryptorProvider;
        }

        public CryptoStream Create(Stream stream)
        {
            return new CryptoStream(stream, decryptorProvider.CreateDecryptor(), CryptoStreamMode.Read);
        }
    }
}
