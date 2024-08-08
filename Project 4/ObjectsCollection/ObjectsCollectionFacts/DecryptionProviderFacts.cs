using System.IO.Compression;
using System.Security.Cryptography;

namespace ObjectsCollection
{
    public class DecryptionProviderFacts
    {
        [Fact]
        public void ShouldReturDecryptedCryptoStream()
        {
            using Stream stream = new MemoryStream();
            EncryptorDecryptorProvider decryptorProvider = new EncryptorDecryptorProvider();
            DecryptionProvider decryptionProvider = new DecryptionProvider(decryptorProvider);
            using Stream decryptedStream = decryptionProvider.Create(stream);
            Assert.IsType<CryptoStream>(decryptedStream);
        }
    }
}
