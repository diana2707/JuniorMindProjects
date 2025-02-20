using System;

namespace Extension_methods
{
    public class ProductFacts
    {
        [Fact]
        public void ProductClassExists()
        {
            Product product = new Product
            {
                Name = "Jacket",
                Quantity = 5
            };
            Assert.IsType<Product>(product);
        }

        [Fact]
        public void ProductClassGetsPropertiesNameAndQuantity()
        {
            Product product = new Product
            {
                Name = "Jacket",
                Quantity = 5
            };
            Assert.Equal("Jacket", product.Name);
            Assert.Equal(5, product.Quantity);
        }

        [Fact]
        public void FeaturesPropertySetsCollectionOfFeatures()
        {
            Product product = new Product
            {
                Name = "Jacket",
                Quantity = 5
            };

            var first = new Feature();
            first.Id = 1;

            var second = new Feature();
            second.Id = 2;

            List<int> expected = [1, 2];

            product.Features = new Feature[] { first, second };

            int count = 0;
            foreach (var feature in product.Features)
            {
                Assert.Equal(expected[count++], feature.Id);
            }
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
