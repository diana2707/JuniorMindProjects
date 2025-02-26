using System;

namespace Extension_methods.Extension_methods
{
    public class Product
    {
        public string Name { get; internal set; }

        public int Quantity { get; internal set; }

        public ICollection<Feature> Features { get; internal set; }
    }
}
