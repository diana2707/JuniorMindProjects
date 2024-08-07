using System.IO.Compression;
using System.Security.Cryptography;

namespace ObjectsCollection
{
    public class CryptoStreamDecryptionProviderFacts
    {
        [Fact]
        public void ShouldReturncryptionCryptoStream()
        {
            using Stream stream = new MemoryStream();
            DecryptionProvider decryptionProvider = new DecryptionProvider();
            using Stream decryptedStream = decryptionProvider.Create(stream);
            Assert.IsType<CryptoStream>(decryptedStream);
        }
    }
}
