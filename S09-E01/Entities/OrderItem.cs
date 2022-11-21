using System.Globalization;

namespace S09_E01.Entities
{
    internal class OrderItem
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Product? Product { get; set; }

        public OrderItem()
        {
        }

        public OrderItem(int quantity, double price, Product product)
        {
            Quantity = quantity;
            Price = price;
            Product = product;
        }

        public double SubTotal()
        {
            return Quantity * Price;
        }

        public override string ToString()
        {
            return $"{Product.Name}, Quantity: {Quantity}, SubTotal: ${SubTotal().ToString("F2", CultureInfo.InvariantCulture)}";
        }
    }
}