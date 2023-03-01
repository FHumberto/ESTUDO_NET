namespace S03_E04
{
    internal static class A
    {
        public static void AMain()
        {
            Console.WriteLine("ler um número inteiro, e depois dizer se este número é negativo ou não");
            int x = int.Parse(Console.ReadLine());
            if (x >= 0)
            {
                Console.WriteLine("NAO NEGATIVO");
            }
            else
            {
                Console.WriteLine("NEGATIVO");
            }
        }
    }
}