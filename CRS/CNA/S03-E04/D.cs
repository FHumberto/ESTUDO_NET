namespace S03_E04
{
    internal static class D
    {
        public static void DMain()
        {
            Console.WriteLine("Leia a hora inicial e a hora final de um jogo. A seguir calcule a duração do jogo, sabendo que o mesmo pode\r\ncomeçar em um dia e terminar em outro, tendo uma duração mínima de 1 hora e máxima de 24 horas");
            string[] valores = Console.ReadLine().Split(' ');
            int hInicial = int.Parse(valores[0]);
            int hFinal = int.Parse(valores[1]);
            int duracao;

            if (hInicial < hFinal)
            {
                duracao = hFinal - hInicial;
            }
            else
            {
                duracao = 24 - hInicial + hFinal;
            }

            Console.WriteLine("O JOGO DUROU " + duracao + " HORA(S)");
        }
    }
}