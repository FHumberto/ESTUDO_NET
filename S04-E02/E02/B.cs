using System.Globalization;

namespace S04_E02.E02
{
    internal static class B
    {
        public static void BMain()
        {
            Console.WriteLine("ler os dados de um funcionário (nome, salário bruto e imposto)");
            Console.WriteLine("Mostrar os dados do funcionário (nome e salário líquido). Em seguida, aumentar o salário do funcionário com base em uma porcentagem dada");
            Console.WriteLine("(somente o salário bruto é afetado pela porcentagem) e mostrar novamente os dados do funcionário.");

            Funcionario f = new();

            Console.Write("\nNome: ");
            f.Nome = Console.ReadLine();
            Console.Write("Salário Bruto: ");
            f.SalarioBruto = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.Write("Imposto: ");
            f.Imposto = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            Console.WriteLine("\nFuncionário: " + f);

            Console.Write("\nDigite a porcentagem para aumentar o salário: ");
            f.AumentarSalario(double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture));

            Console.WriteLine("\nDados Atualizados: " + f);
        }
    }
}