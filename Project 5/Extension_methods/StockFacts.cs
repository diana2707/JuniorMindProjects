namespace Extension_methods
{
    public class StockFacts
    {
        [Fact]
        public void StockClassExtists()
        {
            string notification = string.Empty;

            var stock = new Stock(
                "Jacket",
                100,
                (string product, int quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");

            Assert.IsType<Stock>(stock);
        }

        [Fact]
        public void QuantityReturnsStockQuantity()
        {
            string notification = string.Empty;

            var stock = new Stock(
                "Jacket",
                100,
                (string product, int quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");
            Assert.Equal(100, stock.Quantity);
        }

        [Fact]
        public void DecreaseQuantityShouldReturnDecresedQuantity()
        {
            string notification = string.Empty;

            var stock = new Stock(
                "Jacket",
                100,
                (string product, int quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");
            stock.DecreaseQuantity(25);
            Assert.Equal(75, stock.Quantity);
        }

        [Fact]
        public void IncreaseQuantityShouldReturnIncresedQuantity()
        {
            string notification = string.Empty;

            var stock = new Stock(
                "Jacket",
                100,
                (string product, int quantity) => notification =
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
        public void IncreaseQuantityShouldSetQuantityAndResetModerateThresholdIfQuantityIncresesAboveThrashold()
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

        [Fact]
        public void IncreaseQuantityShouldSetQuantityAndResetLowThresholdIfQuantityIncresesAboveThrashold()
        {
            string notification = string.Empty;

            var stock = new Stock(
                "Jacket",
                20,
                (string product, int quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");
            stock.DecreaseQuantity(16);

            Assert.Equal("There are only 4 units of Jacket in stock.", notification);

            stock.IncreaseQuantity(2);
            stock.DecreaseQuantity(3);

            Assert.Equal("There are only 3 units of Jacket in stock.", notification);
        }

        [Fact]
        public void IncreaseQuantityShouldSetQuantityAndResetCriticalThresholdIfQuantityIncresesAboveThrashold()
        {
            string notification = string.Empty;

            var stock = new Stock(
                "Jacket",
                20,
                (string product, int quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");
            stock.DecreaseQuantity(19);

            Assert.Equal("There are only 1 units of Jacket in stock.", notification);

            stock.IncreaseQuantity(2);
            stock.DecreaseQuantity(3);

            Assert.Equal("There are only 0 units of Jacket in stock.", notification);
        }

        [Fact]
        public void NotificationShouldBeSentWhenIncreasingQuantityAboveATrasholdButStillBelowAnother()
        {
            string notification = string.Empty;

            var stock = new Stock(
                "Jacket",
                20,
                (string product, int quantity) => notification =
                    $"There are only {quantity} units of {product} in stock.");
            stock.DecreaseQuantity(19);

            Assert.Equal("There are only 1 units of Jacket in stock.", notification);

            stock.IncreaseQuantity(2);

            Assert.Equal("There are only 3 units of Jacket in stock.", notification);
        }
    }
}
