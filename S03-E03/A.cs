namespace S03_E03
{
    internal static class A
    {
        public static void AMain()
        {
            Console.WriteLine("ler dois valores inteiros, e depois mostrar na tela a soma desses números com uma mensagem explicativa:");
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());
            Console.WriteLine("SOMA = " + (a + b));
        }
    }
}