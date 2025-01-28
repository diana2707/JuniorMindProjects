using System;

namespace Extension_methods
{
    public class Stock
    {
        private readonly Dictionary<string, int> tresholds = new ()
        {
            { "Critical", 2 },
            { "Low", 5 },
            { "Moderate", 10 },
        };

        private readonly List<int> notifiedTresholds = new ();
        private readonly Func<string, int, string> notifyLowStock;
        private readonly string product;
        private int quantity;

        public Stock(string product, int quantity, Func<string, int, string> notifyLowStock)
        {
            this.product = product;
            this.quantity = quantity;
            this.notifyLowStock = notifyLowStock;
        }

        public int Quantity
        {
            get => quantity;
            private set
            {
                quantity = value;
                ResetNotifiedTresholds();
                CheckThresholds();
            }
        }

        public void DecreaseQuantity(int units) => Quantity -= units;

        public void IncreaseQuantity(int units) => Quantity += units;

        private void ResetNotifiedTresholds()
        {
            foreach (var treshold in tresholds)
            {
                if (quantity >= treshold.Value && notifiedTresholds.Contains(treshold.Value))
                {
                    notifiedTresholds.Remove(treshold.Value);
                }
            }
        }

        private void CheckThresholds()
        {
            foreach (var treshold in tresholds)
            {
                if (quantity < treshold.Value && !notifiedTresholds.Contains(treshold.Value))
                {
                    notifyLowStock.Invoke(product, quantity);
                    notifiedTresholds.Add(treshold.Value);
                    break;
                }
            }
        }
    }
}
