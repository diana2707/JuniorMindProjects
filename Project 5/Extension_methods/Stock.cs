using System;

namespace Extension_methods
{
    public class Stock
    {
        private readonly Dictionary<string, int> thresholds = new ()
        {
            { "Critical", 2 },
            { "Low", 5 },
            { "Moderate", 10 },
        };

        private readonly List<int> notifiedThresholds = new ();
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
                ResetNotifiedThresholds();
                CheckThresholds();
            }
        }

        public void DecreaseQuantity(int units) => Quantity -= units;

        public void IncreaseQuantity(int units) => Quantity += units;

        private void ResetNotifiedThresholds()
        {
            notifiedThresholds.RemoveAll(threshold => threshold < quantity);
        }

        private void CheckThresholds()
        {
            foreach (var threshold in thresholds)
            {
                if (quantity < threshold.Value && !notifiedThresholds.Contains(threshold.Value))
                {
                    notifyLowStock.Invoke(product, quantity);
                    notifiedThresholds.Add(threshold.Value);
                    break;
                }
            }
        }
    }
}
