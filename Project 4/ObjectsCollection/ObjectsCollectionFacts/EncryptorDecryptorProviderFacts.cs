using System;
using System.Security.Cryptography;

namespace ObjectsCollection
{
    public class EncryptorDecryptorProviderFacts
    {
        [Fact]
        public void CreateEncryptorMethodReturnsICryptoTransformObject()
        {
            EncryptorDecryptorProvider provider = new EncryptorDecryptorProvider();
            Assert.IsAssignableFrom<ICryptoTransform>(provider.CreateEncryptor());
        }

        [Fact]
        public void CreateDecryptorMethodReturnsICryptoTransformObject()
        {
            EncryptorDecryptorProvider provider = new EncryptorDecryptorProvider();
            Assert.IsAssignableFrom<ICryptoTransform>(provider.CreateDecryptor());
        }
    }
}
