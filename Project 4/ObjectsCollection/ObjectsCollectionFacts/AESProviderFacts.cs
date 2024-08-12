using System;
using System.Security.Cryptography;

namespace ObjectsCollection
{
    public class AESProviderFacts
    {
        [Fact]
        public void CreateEncryptorMethodReturnsICryptoTransformObject()
        {
            AESProvider provider = new AESProvider();
            Assert.IsAssignableFrom<ICryptoTransform>(provider.CreateEncryptor());
        }

        [Fact]
        public void CreateDecryptorMethodReturnsICryptoTransformObject()
        {
            AESProvider provider = new AESProvider();
            Assert.IsAssignableFrom<ICryptoTransform>(provider.CreateDecryptor());
        }
    }
}
