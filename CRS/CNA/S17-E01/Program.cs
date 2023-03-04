using System.Globalization;

using S17_E01.Entities;

namespace S17_E01
{
    internal static class Program
    {
        private static void Main()
        {
            string? defaultPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\File.csv";
            string? sourcePath = null;
            List<Employee> employees = new();

            Console.WriteLine("Chose an option:");
            Console.WriteLine();
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
                        string[] line = sr.ReadLine().Split(",");
                        string employeeName = line[0];
                        string employeeEmail = line[1];
                        double employeeSalary = double.Parse(line[2], CultureInfo.InvariantCulture);
                        employees.Add(new Employee(employeeName, employeeEmail, employeeSalary));
                    }

                    Console.Write("Enter salary: ");
                    double salary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                    var empHighterThen = employees.Where(x => x.Salary > salary).OrderBy(p => p.Email);

                    foreach (Employee employee in empHighterThen)
                    {
                        Console.WriteLine(employee.Email);
                    }

                    var empSumSalary = employees.Where(x => x.Name.StartsWith('M')).Sum(x => x.Salary);

                    Console.WriteLine($"Sum of salary of people whose name starts with 'M': {empSumSalary.ToString("F2", CultureInfo.InvariantCulture)}");
                }
            }
            catch (ArgumentException error)
            {
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