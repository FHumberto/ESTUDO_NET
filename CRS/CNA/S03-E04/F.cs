using System.Globalization;

namespace S03_E04
{
    internal static class F
    {
        public static void FMain()
        {
            Console.WriteLine("ler um valor qualquer e apresente uma mensagem dizendo em qual dos seguintes intervalos ([0,25], (25,50], (50,75], (75,100]) este valor se encontra. Obviamente se o valor não estiver em nenhum destes intervalos, deverá ser impressa a mensagem “Fora de intervalo");
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