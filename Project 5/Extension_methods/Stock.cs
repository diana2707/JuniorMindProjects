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
            int oldQuantity = product.Quantity;
            product.Quantity -= units;

            CheckThresholds(product, oldQuantity);
        }

        public void IncreaseQuantity(Product product, int units)
        {
            product.Quantity += units;
        }

        private void CheckThresholds(Product product, int oldQuantity)
        {
            int threshold = thresholds
                .FirstOrDefault(threshold => product.Quantity < threshold);

            if (threshold != 0 && oldQuantity > threshold)
            {
                NotifyLowStock.Invoke(product.Name, product.Quantity);
            }
        }
    }
}
