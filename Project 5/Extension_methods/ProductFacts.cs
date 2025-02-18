using System;

namespace Extension_methods
{
    public class ProductFacts
    {
        [Fact]
        public void ProductClassExists()
        {
            var product = new Product("Jacket", 5);
            Assert.IsType<Product>(product);
        }

        [Fact]
        public void ProductClassGetsPropertiesNameAndQuantity()
        {
            var product = new Product("Jacket", 5);
            Assert.Equal("Jacket", product.Name);
            Assert.Equal(5, product.Quantity);
        }

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
