namespace S03_E06
{
    internal static class F
    {
        public static void FMain()
        {
            Console.WriteLine("Ler um número inteiro N e calcular todos os seus divisores.");

            int? n = int.Parse(Console.ReadLine());

            for (int i = 1; i <= n; i++)
            {
                if (n % i == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}