using System.Globalization;

namespace S10_E01
{
    internal static class Program
    {
        private static void Main()
        {
            Console.Write("Enter the number of products: ");
            int n = int.Parse(Console.ReadLine());

            List<Product> prod = new();

            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine($"\nProduct #{i} data:");
                Console.Write("Common, used or imported (c/u/i)? ");
                string opt = Console.ReadLine().ToLower();
                Console.Write("Name: ");
                string? name = Console.ReadLine();
                Console.Write("Price: ");
                double price = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                switch (opt)
                {
                    case "c":
                        prod.Add(new Product(name, price));
                        break;

                    case "u":
                        Console.Write("Manufacture date (DD/MM/YYYY): ");
                        DateOnly date = DateOnly.Parse(Console.ReadLine());
                        prod.Add(new UsedProduct(name, price, date));
                        break;

                    case "i":
                        Console.Write("Customs fee: ");
                        double fee = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                        prod.Add(new ImportedProduct(name, price, fee));
                        break;
                }
            }

            Console.WriteLine("\nPRICE TAGS:");
            foreach (Product p in prod)
            {
                Console.WriteLine(p.PriceTag());
            }
        }
    }
}