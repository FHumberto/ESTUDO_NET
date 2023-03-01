namespace S03_E04
{
    internal static class C
    {
        public static void CMain()
        {
            Console.WriteLine("Leia 2 valores inteiros (A e B). Após, o programa deve mostrar uma mensagem Sao Multiplos ou Nao sao Multiplos, indicando se os valores lidos são múltiplos entre si. Atenção: os números devem poder ser digitados em ordem crescente ou decrescente");
            string[] valores = Console.ReadLine().Split(' ');
            int a = int.Parse(valores[0]);
            int b = int.Parse(valores[1]);
            if (a % b == 0 || b % a == 0)
            {
                Console.WriteLine("Sao Multiplos");
            }
            else
            {
                Console.WriteLine("Nao sao Multiplos");
            }
        }
    }
}