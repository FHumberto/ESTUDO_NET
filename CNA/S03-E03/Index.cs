namespace S03_E03
{
    internal static class Index
    {
        private static void Main()
        {
            Console.WriteLine("Informe a letra do problema: [a, b, c, d, e, f]");
            char? e = char.Parse(Console.ReadLine().ToLower());

            switch (e)
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

                case 'd':
                    D.DMain();
                    break;

                case 'e':
                    E.EMain();
                    break;

                case 'f':
                    F.FMain();
                    break;

                default:
                    Console.WriteLine("O problema informado não existe, tente novamente.");
                    break;
            }
        }
    }
}