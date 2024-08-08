using System;
using System.Security.Cryptography;

namespace ObjectsCollection
{
    public class EncryptorDecryptorProvider
    {
        readonly Aes aes = Aes.Create();

        public EncryptorDecryptorProvider()
        {
        }

        public ICryptoTransform CreateEncryptor()
        {
            return aes.CreateEncryptor(aes.Key, aes.IV);
        }

        public ICryptoTransform CreateDecryptor()
        {
            return aes.CreateDecryptor(aes.Key, aes.IV);
        }
    }
}
