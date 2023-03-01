using System.Globalization;

namespace S03_E03
{
    internal static class B
    {
        public static void BMain()
        {
            Console.WriteLine("ler o valor do raio de um círculo, e depois mostrar o valor da área deste círculo com quatro casas decimais:");
            double raio = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            double area = 3.14159 * (Math.Pow(raio, 2));
            Console.WriteLine("A=" + area.ToString("F4", CultureInfo.InvariantCulture));
        }
    }
}