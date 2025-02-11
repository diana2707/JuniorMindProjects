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

        //private readonly List<int> notifiedThresholds = new ();
        //private readonly Func<string, int, string> notifyLowStock;
        private readonly string product;
        private int quantity;

        public Stock(string product, int quantity, Action<string, int> notifyLowStock)
        {
            this.product = product;
            this.quantity = quantity;
            this.NotifyLowStock = notifyLowStock;
        }

        public Action<string, int> NotifyLowStock { get; private set; }

        public int Quantity
        {
            get => this.quantity;
            private set => this.quantity = value;
                //ResetNotifiedThresholds();
                //CheckThresholds();
        }

        public void DecreaseQuantity(int units)
        {
            this.Quantity -= units;
            this.CheckThresholds();
        }

        public void IncreaseQuantity(int units) => Quantity += units;

        //private void ResetNotifiedThresholds()
        //{
        //    notifiedThresholds.RemoveAll(threshold => threshold < quantity);
        //}

        private void CheckThresholds()
        {
            var threshold = this.thresholds
                .Where(threshold => this.quantity < threshold.Value /*&& !notifiedThresholds.Contains(threshold.Value)*/)
                .FirstOrDefault();

            if (threshold.Value != 0)
            {
                this.NotifyLowStock.Invoke(this.product, this.quantity);
                //notifiedThresholds.Add(threshold.Value);
            }
        }
    }
}
