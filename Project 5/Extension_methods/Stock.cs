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
        private readonly Product product;
        //private int quantity;

        public Stock(Product product, Action<string, int> notifyLowStock)
        {
            this.product = product;
            this.NotifyLowStock = notifyLowStock;
        }

        public Action<string, int> NotifyLowStock { get; private set; }

        //public int Quantity
        //{
        //    get => this.quantity;
        //    private set => this.quantity = value;
        //        //ResetNotifiedThresholds();
        //        //CheckThresholds();
        //}

        public void DecreaseQuantity(Product product, int units)
        {
            product.Quantity -= units;
            this.CheckThresholds(product);
        }

        public void IncreaseQuantity(Product product, int units) => product.Quantity += units;

        //private void ResetNotifiedThresholds()
        //{
        //    notifiedThresholds.RemoveAll(threshold => threshold < quantity);
        //}

        private void CheckThresholds(Product product)
        {
            var threshold = this.thresholds
                .Where(threshold => product.Quantity < threshold.Value /*&& !notifiedThresholds.Contains(threshold.Value)*/)
                .FirstOrDefault();

            if (threshold.Value != 0)
            {
                this.NotifyLowStock.Invoke(this.product.Name, product.Quantity);
                //notifiedThresholds.Add(threshold.Value);
            }
        }
    }

    public class Product
    {
        public Product(string name, int quantity)
        {
            this.Name = name;
            this.Quantity = quantity;
        }

        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}
