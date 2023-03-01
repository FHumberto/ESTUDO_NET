namespace S03_E06
{
    internal static class B
    {
        public static void BMain()
        {
            Console.WriteLine("Leia um valor inteiro N. Este valor será a quantidade de valores inteiros X que serão lidos.");
            Console.WriteLine("Mostre quantos destes valores X estão dentro do intervalo [10,20] e quantos estão fora do ");
            Console.WriteLine("intervalo, mostrando essas informações (use a palavra \"in\" para dentro do intervalo,");
            Console.WriteLine(" e \"out\" para fora do intervalo).");

            int n = int.Parse(Console.ReadLine());
            int valIn = 0, valOut = 0;
            for (int i = 0; i < n; i++)
            {
                int x = int.Parse(Console.ReadLine());
                if (x >= 10 && x <= 20)
                {
                    valIn++;
                }
                else
                {
                    valOut++;
                }
            }
            Console.WriteLine(valIn + " in");
            Console.WriteLine(valOut + " out");
        }
    }
}