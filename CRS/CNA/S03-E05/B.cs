namespace S03_E05
{
    internal static class B
    {
        public static void BMain()
        {
            Console.WriteLine("Escreva um programa para ler as coordenadas (X,Y) de uma quantidade indeterminada de pontos no sistema");
            Console.WriteLine("cartesiano. Para cada ponto escrever o quadrante a que ele pertence. O algoritmo será encerrado quando");
            Console.WriteLine("pelo menos uma de duas coordenadas for NULA (nesta situação sem escrever mensagem alguma)");

            int aux = 0;
            while (aux < 1)
            {
                string[] valores = Console.ReadLine().Split(' ');
                int x = int.Parse(valores[0]);
                int y = int.Parse(valores[1]);
                if (x > 0 && y > 0)
                {
                    Console.WriteLine("primeiro");
                }
                else if (x < 0 && y > 0)
                {
                    Console.WriteLine("segundo");
                }
                else if (x < 0 && y < 0)
                {
                    Console.WriteLine("terceiro");
                }
                else if (x > 0 && y < 0)
                {
                    Console.WriteLine("quarto");
                }
                else
                {
                    aux++;
                }
            }
        }
    }
}