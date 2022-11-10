using System.Globalization;

namespace S03_E03
{
    static class B
    {
        public static void BMain()
        {
            double raio = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            double area = 3.14159 * (Math.Pow(raio, 2));
            Console.WriteLine("A=" + area.ToString("F4", CultureInfo.InvariantCulture));
        }
    }
}
