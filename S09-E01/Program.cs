using System.Globalization;

using S09_E01.Entities;
using S09_E01.Entities.Enums;

namespace S09_E01
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Enter client data:");
            Console.Write("Name: ");
            string? name = Console.ReadLine();
            Console.Write("Email: ");
            string? email = Console.ReadLine();
            Console.Write("Birth Date (DD/MM/YYYY): ");
            DateTime birthDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter order data:");
            Console.Write("Status: ");
            OrderStatus status = Enum.Parse<OrderStatus>(Console.ReadLine());

            Client client = new(name, email, birthDate);
            Order order = new(DateTime.Now, status, client);

            Console.Write("How many items to this order? ");
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Enter #{i + 1} item data:");
                Console.Write("Product Name: ");
                string? pName = Console.ReadLine();
                Console.Write("Product Price: ");
                double pPrice = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                Product product = new Product(pName, pPrice);

                Console.Write("Quantity: ");
                int pQuantity = int.Parse(Console.ReadLine());

                OrderItem orderItem = new(pQuantity, pPrice, product);

                order.AddItem(orderItem);
            }

            Console.WriteLine("\nORDER SUMMARY:");
            Console.WriteLine(order);
        }
    }
}