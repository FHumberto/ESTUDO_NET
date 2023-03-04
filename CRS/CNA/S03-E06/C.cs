using System.Globalization;

namespace S03_E06
{
    internal static class C
    {
        public static void CMain()
        {
            Console.WriteLine("Leia 1 valor inteiro N, que representa o número de casos de teste que vem a seguir.");
            Console.WriteLine("Cada caso de teste consiste de 3 valores reais, cada um deles com uma casa decimal.");
            Console.WriteLine("Apresente a média ponderada para cada um destes conjuntos de 3 valores, sendo que o");
            Console.WriteLine("primeiro valor tem peso 2, o segundo valor tem peso 3 e o terceiro valor tem peso 5");

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] casos = Console.ReadLine().Split(' ');
                double a = double.Parse(casos[0], CultureInfo.InvariantCulture);
                double b = double.Parse(casos[1], CultureInfo.InvariantCulture);
                double c = double.Parse(casos[2], CultureInfo.InvariantCulture);
                Console.WriteLine((((a * 2) + (b * 3) + (c * 5)) / 10.0).ToString("F1", CultureInfo.InvariantCulture));
            }
        }
    }
}