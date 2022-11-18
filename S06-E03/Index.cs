namespace S06_E03
{
    internal static class Index
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Ler dois números inteiros M e N, e depois ler uma matriz de M linhas por N colunas contendo números inteiros");
            Console.WriteLine("podendo haver repetições. Em seguida, ler um número inteiro X que pertence à matriz. Para cada ocorrência de X");
            Console.WriteLine("mostrar os valores à esquerda, acima, à direita e abaixo de X\n\n");

            string[] def = Console.ReadLine().Split(' ');
            int l = int.Parse(def[0]);
            int c = int.Parse(def[1]);

            int[,] matriz = new int[l, c];

            for (int i = 0; i < l; i++)
            {
                string[] valores = Console.ReadLine().Split(' ');
                for (int j = 0; j < c; j++)
                {
                    matriz[i, j] = int.Parse(valores[j]);
                }
            }

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    if (matriz[i, j] == n)
                    {
                        Console.WriteLine($"Position {i},{j}:");
                        if (j > 0)
                        {
                            Console.WriteLine($"Left: {matriz[i, j - 1]}");
                        }
                        if (i > 0)
                        {
                            Console.WriteLine($"Up: {matriz[i - 1, j]}");
                        }
                        if (j < l - 1)
                        {
                            Console.WriteLine($"Right: {matriz[i, j + 1]}");
                        }
                        if (i < c - 1)
                        {
                            Console.WriteLine($"Down: {matriz[i + 1, j]}");
                        }
                    }
                }
            }
        }
    }
}