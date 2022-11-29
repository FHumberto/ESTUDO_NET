using System.Globalization;

namespace S13_E01.Entities
{
    internal class Product
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Product(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public double TotalPrice()
        {
            return Price * Quantity;
        }

        public override string ToString()
        {
            return $"{Name},{TotalPrice().ToString("F2", CultureInfo.InvariantCulture)}";
        }
    }
}