namespace S03_E06
{
    internal static class A
    {
        public static void AMain()
        {
            Console.WriteLine("Leia um valor inteiro X (1 <= X <= 1000).");
            Console.WriteLine("Em seguida mostre os ímpares de 1 até X, um valor por linha, inclusive o X, se for o caso");

            int x = int.Parse(Console.ReadLine());

            for (int i = 1; i <= x; i++)
            {
                if (i % 2 != 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}