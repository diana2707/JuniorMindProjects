using System;

namespace Extension_methods
{
    public class Stock
    {
        private List<int> thresholds = [2, 5, 10];

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
            CheckThresholds(product, units);
        }

        public void IncreaseQuantity(Product product, int units)
        {
            product.Quantity += units;
        }

        private void CheckThresholds(Product product, int units)
        {
            int threshold = thresholds
                .FirstOrDefault(threshold => product.Quantity < threshold);

            if (threshold != 0 && product.Quantity + units > threshold)
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
