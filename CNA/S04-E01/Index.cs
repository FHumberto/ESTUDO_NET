using S04_E01.E01;
using S04_E01.E02;

namespace S04_E01
{
    internal static class Index
    {
        public static void Main()
        {
            Console.WriteLine("Informe o número do problema: [1 ou 2]");
            int? n = int.Parse(Console.ReadLine());

            switch (n)
            {
                case 1:
                    A.AMain();
                    break;

                case 2:
                    B.BMain();
                    break;

                default:
                    Console.WriteLine("O problema informado não existe, tente novamente.");
                    break;
            }
        }
    }
}