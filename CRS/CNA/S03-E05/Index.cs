namespace S03_E05
{
    internal static class Index
    {
        public static void Main()
        {
            Console.WriteLine("Informe a letra do problema: [a, b, c]");
            char? a = char.Parse(Console.ReadLine().ToLower());

            switch (a)
            {
                case 'a':
                    A.AMain();
                    break;

                case 'b':
                    B.BMain();
                    break;

                case 'c':
                    C.CMain();
                    break;

                default:
                    Console.WriteLine("O problema informado não existe, tente novamente.");
                    break;
            }
        }
    }
}