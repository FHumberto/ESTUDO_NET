using System.Globalization;

namespace S10_E00
{
    internal static class Program
    {
        private static void Main()
        {
            Console.Write("Enter the number of employees: ");
            int n = int.Parse(Console.ReadLine());
            List<Employee> list = new();

            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine($"Employee #{i}");
                Console.Write("Outsourced (y/n)? ");
                char? o = char.Parse(Console.ReadLine());
                Console.Write("Name: ");
                string? name = Console.ReadLine();
                Console.Write("Hours: ");
                int hours = int.Parse(Console.ReadLine());
                Console.Write("Value per hour: ");
                double value = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                if (o.ToString().ToLower() == "y")
                {
                    Console.WriteLine("Additional Charge: ");
                    float adicional = float.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    list.Add(new OutsourcedEmployee(name, hours, value, adicional));
                }
                else
                {
                    list.Add(new Employee(name, hours, value));
                }
            }

            Console.WriteLine("\nPAYMENTS:");
            foreach (Employee emp in list)
            {
                Console.WriteLine(emp);
            }
        }
    }
}