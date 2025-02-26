using Extension_methods.Extension_methods;

namespace Extension_methods.ExtensionMethodsFacts
{
    public class StockFacts
    {
        [Fact]
        public void StockClassExtists()
        {
            string notification = string.Empty;
            var stock = new Stock(
                (product, quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");

            Assert.IsType<Stock>(stock);
        }

        [Fact]
        public void DecreaseQuantityShouldReturnDecresedQuantity()
        {
            string notification = string.Empty;
            Product product = new Product
            {
                Name = "Jacket",
                Quantity = 100
            };
            var stock = new Stock(
                (product, quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");

            stock.Add(product);
            stock.DecreaseQuantity(product, 25);

            Assert.Equal(75, product.Quantity);
        }

        [Fact]
        public void IncreaseQuantityShouldReturnIncresedQuantity()
        {
            string notification = string.Empty;
            Product product = new Product
            {
                Name = "Jacket",
                Quantity = 100
            };
            var stock = new Stock(
                (product, quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");

            stock.Add(product);
            stock.IncreaseQuantity(product, 25);

            Assert.Equal(125, product.Quantity);
        }

        [Fact]
        public void DecreaseQuantityShouldSetNewQuantityAndInvokeNotificationWhenQuantityIsBellow10()
        {
            string notification = string.Empty;
            Product product = new Product
            {
                Name = "Jacket",
                Quantity = 20
            };
            var stock = new Stock(
                (product, quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");

            stock.Add(product);
            stock.DecreaseQuantity(product, 11);

            Assert.Equal("There are only 9 units of Jacket in stock.", notification);
        }

        [Fact]
        public void DecreaseQuantityShouldSetNewQuantityAndInvokeNotificationAtOnceWhenIsBellow10()
        {
            string notification = string.Empty;
            Product product = new Product
            {
                Name = "Jacket",
                Quantity = 20
            };
            var stock = new Stock(
                (product, quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");

            stock.Add(product);
            stock.DecreaseQuantity(product, 11);
            stock.DecreaseQuantity(product, 2);

            Assert.Equal("There are only 9 units of Jacket in stock.", notification);
        }

        [Fact]
        public void DecreaseQuantityShouldSetNewQuantityAndInvokeNotificationWhenIsBellow5()
        {
            string notification = string.Empty;
            Product product = new Product
            {
                Name = "Jacket",
                Quantity = 20
            };
            var stock = new Stock(
                (product, quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");

            stock.Add(product);
            stock.DecreaseQuantity(product, 11);
            stock.DecreaseQuantity(product, 5);

            Assert.Equal("There are only 4 units of Jacket in stock.", notification);
        }

        [Fact]
        public void DecreaseQuantityShouldSetNewQuantityAndInvokeNotificationWhenIsBellow2()
        {
            string notification = string.Empty;
            Product product = new Product
            {
                Name = "Jacket",
                Quantity = 20
            };
            var stock = new Stock(
                (product, quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");

            stock.Add(product);
            stock.DecreaseQuantity(product, 11);
            stock.DecreaseQuantity(product, 5);
            stock.DecreaseQuantity(product, 3);

            Assert.Equal("There are only 1 units of Jacket in stock.", notification);
        }

        [Fact]
        public void IncreaseQuantityShouldSetQuantityAndReset10ThresholdIfQuantityIncresesAboveThrashold()
        {
            string notification = string.Empty;
            Product product = new Product
            {
                Name = "Jacket",
                Quantity = 20
            };

            var stock = new Stock(
                (product, quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");

            stock.Add(product);
            stock.DecreaseQuantity(product, 11);

            Assert.Equal("There are only 9 units of Jacket in stock.", notification);

            stock.IncreaseQuantity(product, 2);
            stock.DecreaseQuantity(product, 5);

            Assert.Equal("There are only 6 units of Jacket in stock.", notification);
        }

        [Fact]
        public void IncreaseQuantityShouldSetQuantityAndReset5ThresholdIfQuantityIncresesAboveThrashold()
        {
            string notification = string.Empty;
            Product product = new Product
            {
                Name = "Jacket",
                Quantity = 20
            };
            var stock = new Stock(
                (product, quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");

            stock.Add(product);
            stock.DecreaseQuantity(product, 16);

            Assert.Equal("There are only 4 units of Jacket in stock.", notification);

            stock.IncreaseQuantity(product, 2);
            stock.DecreaseQuantity(product, 3);

            Assert.Equal("There are only 3 units of Jacket in stock.", notification);
        }

        [Fact]
        public void IncreaseQuantityShouldSetQuantityAndReset2ThresholdIfQuantityIncresesAboveThrashold()
        {
            string notification = string.Empty;
            Product product = new Product
            {
                Name = "Jacket",
                Quantity = 20
            };
            var stock = new Stock(
                (product, quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");

            stock.Add(product);
            stock.DecreaseQuantity(product, 19);

            Assert.Equal("There are only 1 units of Jacket in stock.", notification);

            stock.IncreaseQuantity(product, 2);
            stock.DecreaseQuantity(product, 3);

            Assert.Equal("There are only 0 units of Jacket in stock.", notification);
        }

        [Fact]
        public void ProductClassExists()
        {
            Product product = new Product
            {
                Name = "Jacket",
                Quantity = 20
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
        public void StockClassUpdatesQuantityOnSpecificProductWhenMoreProductsAreStored()
        {
            string notification = string.Empty;
            Product jacket = new Product
            {
                Name = "Jacket",
                Quantity = 20
            };
            Product coat = new Product
            {
                Name = "Jacket",
                Quantity = 20
            };
            Product shirt = new Product
            {
                Name = "Jacket",
                Quantity = 20
            };
            var stock = new Stock(
                (product, quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");

            stock.Add(jacket);
            stock.Add(coat);
            stock.Add(shirt);

            stock.DecreaseQuantity(shirt, 5);

            Assert.Equal(15, shirt.Quantity);
            Assert.Equal(20, jacket.Quantity);
            Assert.Equal(20, coat.Quantity);
        }
    }
}
