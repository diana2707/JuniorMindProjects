using System.Security.Cryptography;

namespace ObjectsCollection
{
    public class CryptoStreamEncryptionProviderFacts
    {
        [Fact]
        public void ShouldReturnEncryptionCryptoStream()
        {
            using Stream stream = new MemoryStream();
            CryptoStreamEncryptionProvider encryptionProvider = new CryptoStreamEncryptionProvider();
            using Stream encryptedStream = encryptionProvider.Create(stream);
            Assert.IsType<CryptoStream>(encryptedStream);
        }
    }
}
