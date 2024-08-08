using System.Security.Cryptography;

namespace ObjectsCollection
{
    public class CryptoStreamEncryptionProviderFacts
    {
        [Fact]
        public void ShouldReturnEncryptionCryptoStream()
        {
            using Stream stream = new MemoryStream();
            EncryptorDecryptorProvider encryptor = new EncryptorDecryptorProvider();
            EncryptionProvider encryptionProvider = new EncryptionProvider(encryptor);
            using Stream encryptedStream = encryptionProvider.Create(stream);
            Assert.IsType<CryptoStream>(encryptedStream);
        }
    }
}
