namespace S03_E04
{
    internal static class Index
    {
        public static void Main()
        {
            Console.WriteLine("Informe a letra do problema: [a, b, c, d, e, f]");

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

                case 'd':
                    D.DMain();
                    break;

                case 'e':
                    E.EMain();
                    break;

                case 'f':
                    F.FMain();
                    break;

                case 'g':
                    G.GMain();
                    break;

                case 'H':
                    H.HMain();
                    break;

                default:
                    Console.WriteLine("O problema informado não existe, tente novamente.");
                    break;
            }
        }
    }
}