namespace S03_E06
{
    internal static class E
    {
        public static void EMain()
        {
            Console.WriteLine("Ler um valor N. Calcular e escrever seu respectivo fatorial.");
            Console.WriteLine("Fatorial de N = N * (N-1) * (N-2) * (N-3) * ... * 1.");
            Console.WriteLine("Lembrando que, por definição, fatorial de 0 é 1");

            int? n = int.Parse(Console.ReadLine());

            int fatorial = 1;

            for (int i = 1; i <= n; i++)
            {
                fatorial *= i;
            }

            Console.WriteLine(fatorial);
        }
    }
}