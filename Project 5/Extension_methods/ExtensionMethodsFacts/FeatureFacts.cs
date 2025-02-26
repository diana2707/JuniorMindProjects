using Extension_methods.Extension_methods;
using System;

namespace Extension_methods.ExtensionMethodsFacts
{
    public class FeatureFacts
    {
        [Fact]
        public void FeatureClassExists()
        {
            var feature = new Feature();
            Assert.IsType<Feature>(feature);
        }

        [Fact]
        public void SetIdForFeature()
        {
            var feature = new Feature();
            feature.Id = 1;

            Assert.Equal(1, feature.Id);
        }
    }
}
