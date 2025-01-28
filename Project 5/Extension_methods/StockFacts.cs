namespace Extension_methods
{
    public class StockFacts
    {
        [Fact]
        public void StockClassExtists()
        {
            var stock = new Stock(
                "Jacket",
                100,
                (string product, int quantity) =>
                    $"There are only {quantity} units of {product} in stock.");

            Assert.IsType<Stock>(stock);
        }

        [Fact]
        public void QuantityReturnsStockQuantity()
        {
            var stock = new Stock(
                "Jacket",
                100,
                (string product, int quantity) =>
                    $"There are only {quantity} units of {product} in stock.");
            Assert.Equal(100, stock.Quantity);
        }

        [Fact]
        public void DecreaseQuantityShouldReturnDecresedQuantity()
        {
            var stock = new Stock(
                "Jacket",
                100,
                (string product, int quantity) =>
                    $"There are only {quantity} units of {product} in stock.");
            stock.DecreaseQuantity(25);
            Assert.Equal(75, stock.Quantity);
        }

        [Fact]
        public void IncreaseQuantityShouldReturnIncresedQuantity()
        {
            var stock = new Stock(
                "Jacket",
                100,
                (string product, int quantity) =>
                    $"There are only {quantity} units of {product} in stock.");
            stock.IncreaseQuantity(25);
            Assert.Equal(125, stock.Quantity);
        }

        [Fact]
        public void DecreaseQuantityShouldSetNewQuantityAndInvokeNotificationWhenQuantityIsBellow10()
        {
            string notification = string.Empty;

            var stock = new Stock(
                "Jacket",
                20,
                (string product, int quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");
            stock.DecreaseQuantity(11);
            Assert.Equal("There are only 9 units of Jacket in stock.", notification);
        }

        [Fact]
        public void DecreaseQuantityShouldSetNewQuantityAndInvokeNotificationOnceWhenIsBellow10()
        {
            string notification = string.Empty;

            var stock = new Stock(
                "Jacket",
                20,
                (string product, int quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");
            stock.DecreaseQuantity(11);
            stock.DecreaseQuantity(2);
            Assert.Equal("There are only 9 units of Jacket in stock.", notification);
        }

        [Fact]
        public void DecreaseQuantityShouldSetNewQuantityAndInvokeNotificationWhenIsBellow5()
        {
            string notification = string.Empty;

            var stock = new Stock(
                "Jacket",
                20,
                (string product, int quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");
            stock.DecreaseQuantity(11);
            stock.DecreaseQuantity(5);

            Assert.Equal("There are only 4 units of Jacket in stock.", notification);
        }

        [Fact]
        public void DecreaseQuantityShouldSetNewQuantityAndInvokeNotificationWhenIsBellow2()
        {
            string notification = string.Empty;

            var stock = new Stock(
                "Jacket",
                20,
                (string product, int quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");
            stock.DecreaseQuantity(11);
            stock.DecreaseQuantity(5);
            stock.DecreaseQuantity(3);

            Assert.Equal("There are only 1 units of Jacket in stock.", notification);
        }

        [Fact]
        public void IncreaseQuantityShouldSetQuantityAndResetThresholdIfQuantityIncresesAboveThrashold()
        {
            string notification = string.Empty;

            var stock = new Stock(
                "Jacket",
                20,
                (string product, int quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");
            stock.DecreaseQuantity(11);

            Assert.Equal("There are only 9 units of Jacket in stock.", notification);

            stock.IncreaseQuantity(2);
            stock.DecreaseQuantity(5);

            Assert.Equal("There are only 6 units of Jacket in stock.", notification);
        }
    }
}
