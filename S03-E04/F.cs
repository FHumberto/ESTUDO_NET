using System.Globalization;

namespace S03_E04
{
    static class F
    {
        public static void FMain()
        {
            double ponto = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            if (ponto >= 0 && ponto <= 25)
            {
                Console.WriteLine("Intervalo [0,25]");
            }
            else if (ponto > 25 && ponto <= 50)
            {
                Console.WriteLine("Intervalo (25,50]");
            }
            else if (ponto > 50 && ponto <= 75)
            {
                Console.WriteLine("Intervalo (50,75]");
            }
            else if (ponto > 75 && ponto <= 100)
            {
                Console.WriteLine("Intervalo (75,100]");
            }
            else
            {
                Console.WriteLine("Fora de Intervalo");
            }
        }
    }
}
