using S04_E02.E01;
using S04_E02.E02;
using S04_E02.E03;

namespace S04_E02
{
    internal static class Index
    {
        private static void Main()
        {
            Console.WriteLine("Informe o número do problema: [1 a 3]");
            int n = int.Parse(Console.ReadLine());

            switch (n)
            {
                case 1:
                    A.AMain();
                    break;

                case 2:
                    B.BMain();
                    break;

                case 3:
                    C.CMain();
                    break;

                default:
                    Console.WriteLine("O problema informado não existe, tente novamente.");
                    break;
            }
        }
    }
}