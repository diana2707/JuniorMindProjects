using System;

namespace Extension_methods
{
    public class Stock
    {
        private static class Trashold
        {
            public const int Moderate = 10;
            public const int Low = 5;
            public const int Critical = 2;
        }

        private readonly Func<string, int, string> notifyLowStock;
        private readonly string product;
        private int quantity;
        private List<int> notified = new List<int>();

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
                this.quantity = value;
                CheckTrasholds();
            }
        }

        public void DecreaseQuantity(int units) => Quantity -= units;

        public void IncreaseQuantity(int units) => Quantity += units;

        private void CheckTrasholds()
        {
            if (quantity < Trashold.Moderate && !notified.Contains(Trashold.Moderate))
            {
                notifyLowStock.Invoke(product, quantity);
                notified.Add(Trashold.Moderate);
            }

            if (quantity < Trashold.Low && !notified.Contains(Trashold.Low))
            {
                notifyLowStock.Invoke(product, quantity);
                notified.Add(Trashold.Low);
            }

            if (quantity < Trashold.Critical && !notified.Contains(Trashold.Critical))
            {
                notifyLowStock.Invoke(product, quantity);
                notified.Add(Trashold.Critical);
            }

            if (quantity >= Trashold.Moderate && notified.Contains(Trashold.Moderate))
            {
                notified.Remove(Trashold.Moderate);
            }

            if (quantity >= Trashold.Low && notified.Contains(Trashold.Low))
            {
                notified.Remove(Trashold.Low);
            }

            if (quantity >= Trashold.Critical && notified.Contains(Trashold.Critical))
            {
                notified.Remove(Trashold.Critical);
            }
        }
    }
}
