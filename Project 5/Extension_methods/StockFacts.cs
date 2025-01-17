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
    }
}
