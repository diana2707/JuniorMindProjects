using System;
using System.Security.Cryptography;

namespace ObjectsCollection
{
    public class DecryptionProvider : IDecryptionProvider
    {
        public CryptoStream Create(Stream stream, AESParameters aesParameters)
        {
            using Aes aes = Aes.Create();
            byte[] key = aes.Key;
            byte[] iv = aes.IV;
            ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
            return new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
        }
    }
}
