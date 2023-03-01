namespace S03_E04
{
    internal static class B
    {
        public static void BMain()
        {
            Console.WriteLine("ler um número inteiro e dizer se este número é par ou ímpar");
            int x = int.Parse(Console.ReadLine());
            if (x % 2 == 0)
            {
                Console.WriteLine("PAR");
            }
            else
            {
                Console.WriteLine("IMPAR");
            }
        }
    }
}