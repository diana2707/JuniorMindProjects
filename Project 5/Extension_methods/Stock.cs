using System.Net.Http.Headers;

namespace Extension_methods
{
    public class Stock
    {
        readonly string product;

        public Stock(string product, int quantity)
        {
            this.product = product;
            this.Quantity = quantity;
        }

        public int Quantity { get; private set; }

        public void DecreaseQuantity(int units) => Quantity -= units;

        public void IncreaseQuantoty(int units) => Quantity += units;
    }
}
