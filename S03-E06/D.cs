using System.Globalization;

namespace S03_E06
{
    internal static class D
    {
        public static void DMain()
        {
            Console.WriteLine("Ler um número N. Depois leia N pares de números e mostre a divisão do primeiro pelo segundo. ");
            Console.WriteLine("Se o denominador for igual a zero, mostrar a mensagem \"divisão impossível\"");

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] numeros = Console.ReadLine().Split(' ');
                double x = double.Parse(numeros[0], CultureInfo.InvariantCulture);
                double y = double.Parse(numeros[1], CultureInfo.InvariantCulture);
                if (y == 0)
                {
                    Console.WriteLine("divisão impossível");
                }
                else
                {
                    Console.WriteLine((x / y).ToString("F1", CultureInfo.InvariantCulture));
                }
            }
        }
    }
}