namespace S03_E06
{
    internal static class G
    {
        public static void GMain()
        {
            Console.WriteLine("Fazer um programa para ler um número inteiro positivo N. O programa deve então mostrar na tela N linhas,");
            Console.WriteLine("começando de 1 até N. Para cada linha, mostrar o número da linha, depois o quadrado e o cubo do valor");

            int? n = int.Parse(Console.ReadLine());

            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine($"{i} {(Math.Pow(i, 2))} {(Math.Pow(i, 3))}");
            }
        }
    }
}