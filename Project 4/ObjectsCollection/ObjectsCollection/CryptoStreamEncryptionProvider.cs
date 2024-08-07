using System;
using System.Security.Cryptography;

namespace ObjectsCollection
{
    public class CryptoStreamEncryptionProvider : ICryptoStreamProvider
    {
        public CryptoStream Create(Stream stream)
        {
            using Aes aes = Aes.Create();
            byte[] key = aes.Key;
            byte[] iv = aes.IV;
            ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
            return new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
        }
    }
}
