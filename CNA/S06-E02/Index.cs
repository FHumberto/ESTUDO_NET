using System.Globalization;

namespace S06_E02
{
    internal static class Index
    {
        private static void Main()
        {
            Console.WriteLine("Ler um número inteiro N e depois os dados (id, nome e salario) de N funcionários. Não deve haver repetição de id");
            Console.WriteLine("Em seguida, efetuar o aumento de X por cento no salário de um determinado funcionário.");
            Console.WriteLine("o programa deve ler um id e o valor X. Se o id informado não existir, mostrar uma mensagem e abortar a operação.");
            Console.WriteLine("Ao final, mostrar a listagem atualizada dos funcionários.\n\n");

            Console.Write("How many employess will be registered? ");
            int n = int.Parse(Console.ReadLine());

            List<Employee> emp = new();

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nEmploye #{i + 1}");
                Console.Write("Id: ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("Name: ");
                string? name = Console.ReadLine();
                Console.Write("Salary: ");
                double salary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                emp.Add(new Employee(id, name, salary));
            }

            Console.Write("\nEnter the employee id that will have salary increase : ");
            int searchId = int.Parse(Console.ReadLine());

            Employee? findEmploye = emp.Find(x => x.Id == searchId);

            if (findEmploye != null)
            {
                Console.Write("Enter the percentage: ");
                findEmploye.IncreaseSalary(double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture));
            }
            else
            {
                Console.WriteLine("This id does not exist!");
            }

            Console.WriteLine("\nUpdated list of employess:");
            foreach (Employee obj in emp)
            {
                Console.WriteLine(obj);
            }
        }
    }
}