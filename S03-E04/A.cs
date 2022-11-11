namespace S03_E04
{
    static class A
    {
        public static void AMain()
        {
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