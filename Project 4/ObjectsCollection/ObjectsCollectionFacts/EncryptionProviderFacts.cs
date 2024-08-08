using System.Security.Cryptography;

namespace ObjectsCollection
{
    public class EncryptionProviderFacts
    {
        [Fact]
        public void ShouldReturnEncryptionCryptoStream()
        {
            using Stream stream = new MemoryStream();
            EncryptorDecryptorProvider encryptorProvider = new EncryptorDecryptorProvider();
            EncryptionProvider encryptionProvider = new EncryptionProvider(encryptorProvider);
            using Stream encryptedStream = encryptionProvider.Create(stream);
            Assert.IsType<CryptoStream>(encryptedStream);
        }
    }
}
