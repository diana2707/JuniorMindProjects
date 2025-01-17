namespace Extension_methods
{
    public class StockFacts
    {
        [Fact]
        public void StockClassExtists()
        {
            var stock = new Stock("Jacket", 100);
            Assert.IsType<Stock>(stock);
        }

        [Fact]
        public void QuantityReturnsStockQuantity()
        {
            var stock = new Stock("Jacket", 100);
            Assert.Equal(100, stock.Quantity);
        }

        [Fact]
        public void DecreaseQuantityShouldReturnDecresedQuantity()
        {
            var stock = new Stock("Jacket", 100);
            stock.DecreaseQuantity(25);
            Assert.Equal(75, stock.Quantity);
        }
    }
}
