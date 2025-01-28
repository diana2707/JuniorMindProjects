using System.Net.Http.Headers;

namespace Extension_methods
{
    public class Stock
    {
        public static class Trashold
        {
            public const int Moderate = 10;
            public const int Low = 5;
            public const int Critical = 2;
        }

        readonly string product;
        private int quantity;

        private Func<string, int, string> notifyLowStock;

        public Stock(string product, int quantity, Func<string, int, string> lowStockNotification)
        {
            this.product = product;
            this.quantity = quantity;
            this.notifyLowStock = lowStockNotification;
        }

        public int Quantity
        {
            get => this.quantity;
            private set
            {
                quantity = value;
                CheckTrasholds();
            }
        }

        public void DecreaseQuantity(int units) => Quantity -= units;

        public void IncreaseQuantity(int units) => Quantity += units;

        private void CheckTrasholds()
        {
            if (quantity <= Trashold.Moderate)
            {
                notifyLowStock.Invoke(product, quantity);
            }
        }
    }
}
