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

        private List<Product> products = [];

        public Stock(Action<string, int> notifyLowStock)
        {
            NotifyLowStock = notifyLowStock;
        }

        public Action<string, int> NotifyLowStock { get; private set; }

        public void Add(Product product) => products.Add(product);

        public void DecreaseQuantity(Product product, int units)
        {
            product.Quantity -= units;
            CheckThresholds(product);
        }

        public void IncreaseQuantity(Product product, int units) => product.Quantity += units;

        private void CheckThresholds(Product product)
        {
            var threshold = thresholds
                .FirstOrDefault(threshold => product.Quantity < threshold.Value);

            if (threshold.Value != 0)
            {
                NotifyLowStock.Invoke(product.Name, product.Quantity);
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

        public string Name { get; private set; }

        public int Quantity { get; internal set; }
    }
}
