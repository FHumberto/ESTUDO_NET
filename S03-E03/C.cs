namespace S03_E03
{
    internal static class C
    {
        public static void CMain()
        {
            Console.WriteLine("ler quatro valores inteiros A, B, C e D. A seguir, calcule e mostre a diferença do produto de A e B pelo produto de C e D segundo a fórmula: DIFERENCA = (A * B - C * D)");
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());
            int c = int.Parse(Console.ReadLine());
            int d = int.Parse(Console.ReadLine());

            Console.WriteLine("DIFERENCA = " + (a * b - c * d));
        }
    }
}