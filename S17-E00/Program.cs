using System.Globalization;

using S17_E00.Entities;

namespace S17_E00
{
    internal static class Program
    {
        private static void Main()
        {
            string? defaultPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\File.csv";
            string? sourcePath = null;

            List<Product> products = new();

            Console.WriteLine("Chose an option:");
            Console.WriteLine("0: to write custom path where the CSV file is located.");
            Console.WriteLine("1: to use the default location.");
            Console.WriteLine();

            byte opt = byte.Parse(Console.ReadLine());

            switch (opt)
            {
                case 0:
                    Console.WriteLine("Inform the folder file path.");
                    sourcePath = Console.ReadLine();
                    break;

                case 1:
                    sourcePath = defaultPath;
                    break;

                default:
                    Console.WriteLine("ERROR: invalid optiton. Please, try again.");
                    break;
            }

            try
            {
                if (sourcePath == null)
                {
                    throw new ArgumentException("The file path canot be null");
                }
                else
                {
                    using StreamReader sr = File.OpenText(sourcePath);

                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        string productName = line[0];
                        double productPrice = double.Parse(line[1], CultureInfo.InvariantCulture);
                        products.Add(new Product(productName, productPrice));
                    }

                    // FILTRA OS ARQUIVOS
                    var averagePrice = products.Select(p => p.Price).DefaultIfEmpty(0.0).Average();
                    var productsBelowAverage = products.Where(x => x.Price < averagePrice);

                    Console.WriteLine($"Average Price: {averagePrice.ToString("F2", CultureInfo.InvariantCulture)}");

                    Console.WriteLine();
                    foreach (Product product in productsBelowAverage.Reverse())
                    {
                        Console.WriteLine(product.Name);
                    }
                }
            }
            catch (ArgumentException error)
            {
                Console.WriteLine();
                Console.WriteLine($"ERROR: {error.Message}");
            }
            catch (FileNotFoundException error)
            {
                Console.WriteLine();
                Console.WriteLine($"ERROR: {error.Message}");
            }
        }
    }
}