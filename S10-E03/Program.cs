using S10_E03.Entities;

using System.Globalization;

namespace S10_E03
{
    internal static class Program
    {
        private static void Main()
        {
            Console.Write("Enter the number of tax payers: ");
            int n = int.Parse(Console.ReadLine());

            List<TaxPayer> payer = new();

            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine($"Tax payer #{i} data:");
                Console.Write("Individual or company (i/c)? ");
                string? type = Console.ReadLine().ToLower();
                Console.Write("Name: ");
                string? name = Console.ReadLine();
                Console.Write("Yearly income: ");
                double income = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                if (type == "i")
                {
                    Console.Write("Health expenditures: ");
                    double expenditures = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    payer.Add(new Individual(name, income, expenditures));
                }
                else
                {
                    Console.Write("Number of employees: ");
                    int employess = int.Parse(Console.ReadLine());
                    payer.Add(new Company(name, income, employess));
                }
            }

            Console.WriteLine();
            Console.WriteLine("TAXES PAID:");
            double sum = 0.0;
            foreach (TaxPayer obj in payer)
            {
                Console.WriteLine($"{obj.Name}: $ {obj.Tax().ToString("F2", CultureInfo.InvariantCulture)}");
                sum += obj.Tax();
            }

            Console.WriteLine();
            Console.Write($"TOTAL TAXES: {sum.ToString("F2", CultureInfo.InvariantCulture)}");
        }
    }
}