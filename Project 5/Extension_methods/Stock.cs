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

        public void Add(Product product)
        {
            products.Add(product);
        }

        public void DecreaseQuantity(Product product, int units)
        {
            product.Quantity -= units;
            this.CheckThresholds(product);
        }

        public void IncreaseQuantity(Product product, int units) => product.Quantity += units;

        private void CheckThresholds(Product product)
        {
            var threshold = this.thresholds
                .Where(threshold => product.Quantity < threshold.Value)
                .FirstOrDefault();

            if (threshold.Value != 0)
            {
                this.NotifyLowStock.Invoke(product.Name, product.Quantity);
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
