using System.Globalization;

namespace S03_E03
{
    internal class F
    {
        public static void FMain()
        {
            string[] linha = Console.ReadLine().Split(' ');
            float a = float.Parse(linha[0], CultureInfo.InvariantCulture);
            float b = float.Parse(linha[1], CultureInfo.InvariantCulture);
            float c = float.Parse(linha[2], CultureInfo.InvariantCulture);
            
            Console.WriteLine("TRIANGULO: " + (a * c / 2.0).ToString("F3", CultureInfo.InvariantCulture));
            Console.WriteLine("CIRCULO: " + (3.14159 * (c * c)).ToString("F3", CultureInfo.InvariantCulture));
            Console.WriteLine("TRAPEZIO: " + (((a + b) * c) / 2.0).ToString("F3", CultureInfo.InvariantCulture));
            Console.WriteLine("QUADRADO: " + (b * b).ToString("F3", CultureInfo.InvariantCulture));
            Console.WriteLine("RETANGULO: " + (a * b).ToString("F3", CultureInfo.InvariantCulture));
        }
    }
}
