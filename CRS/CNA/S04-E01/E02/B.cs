using System.Globalization;

namespace S04_E01.E02
{
    internal static class B
    {
        public static void BMain()
        {
            Console.WriteLine("Fazer um programa para ler nome e salário de dois funcionários. Depois, mostrar o salário médio dos funcionários.\n\n");

            Funcionario a = new();
            Funcionario b = new();

            Console.WriteLine("Dados do primeiro funcionário:");
            Console.Write("Nome: ");
            a.Nome = Console.ReadLine();
            Console.Write("Salário: ");
            a.Salario = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            Console.WriteLine("Dados do segundo funcionário:");
            Console.Write("Nome: ");
            b.Nome = Console.ReadLine();
            Console.Write("Salário: ");
            b.Salario = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            Console.WriteLine("Salário médio = " + MediaSalario(a.Salario, b.Salario).ToString("F2", CultureInfo.InvariantCulture));
        }

        private static double MediaSalario(double salarioA, double salarioB)
        {
            return (salarioA + salarioB) / 2.0;
        }
    }
}