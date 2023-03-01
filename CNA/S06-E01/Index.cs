namespace S06_E01
{
    internal static class Index
    {
        private static void Main()
        {
            Console.WriteLine("A dona de um pensionato possui dez quartos para alugar para estudantes, sendo esses quartos identificados pelos números 0 a 9");
            Console.WriteLine("Quando um estudante deseja alugar um quarto, deve-se registrar o nome e email deste estudante.");
            Console.WriteLine("inicie com todos os dez quartos vazios, e depois leia uma quantidade N representando o número de estudantes que vão alugar quartos (N pode ser de 1 a 10).");
            Console.WriteLine("Em seguida, registre o aluguel dos N estudantes. Para cada registro de aluguel, informar o nome e email do estudante, bem como qual dos quartos ele escolheu (de 0 a 9).");
            Console.WriteLine("Suponha que seja escolhido um quarto vago. Ao final, seu programa deve imprimir um relatório de todas ocupações do pensionato, por ordem de quarto, conforme exemplo");
            Console.WriteLine("[considere que todos os quartos estão livres, e desconsidere verificação]");

            Console.Write("\n\nQuantos quartos serão alugados? ");
            int n = int.Parse(Console.ReadLine());
            Estudante[] quartos = new Estudante[10];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nAluguel #{i + 1}");
                Console.Write("Nome: ");
                string? nome = Console.ReadLine();
                Console.Write("Email: ");
                string? email = Console.ReadLine();
                Console.Write("Quarto: ");
                int quarto = int.Parse(Console.ReadLine());

                quartos[quarto] = new Estudante(nome, email);
            }

            Console.WriteLine("\nQuartos Ocupados:");

            for (int i = 0; i < quartos.Length; i++)
            {
                if (quartos[i] != null)
                {
                    Console.WriteLine($"{i}: {quartos[i]}");
                }
            }
        }
    }
}