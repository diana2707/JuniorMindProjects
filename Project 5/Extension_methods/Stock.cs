using System;

namespace Extension_methods
{
    public class Stock
    {
        private Dictionary<int, bool> thresholds = new()
        {
            { 2, false },
            { 5, false },
            { 10, false },
        };

        private List<Product> products = [];

        public Stock(Action<string, int> notifyLowStock)
        {
            NotifyLowStock = notifyLowStock;
        }

        public Action<string, int> NotifyLowStock { get; }

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
                .FirstOrDefault(threshold => product.Quantity < threshold.Key);

            if (threshold.Key != 0 && !thresholds[threshold.Key])
            {
                thresholds[threshold.Key] = true;
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
