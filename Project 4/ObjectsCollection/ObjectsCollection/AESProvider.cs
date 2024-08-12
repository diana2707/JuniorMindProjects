using System;
using System.Security.Cryptography;

namespace ObjectsCollection
{
    public class AESProvider
    {
        readonly Aes aes;
        readonly byte[] key;
        readonly byte[] iv;

        public AESProvider()
        {
            aes = Aes.Create();
            key = aes.Key;
            iv = aes.IV;
        }

        public ICryptoTransform CreateEncryptor()
        {
            return aes.CreateEncryptor(key, iv);
        }

        public ICryptoTransform CreateDecryptor()
        {
            return aes.CreateDecryptor(key, iv);
        }
    }
}
