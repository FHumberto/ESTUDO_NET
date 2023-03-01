namespace S04_E01.E01
{
    internal static class A
    {
        public static void AMain()
        {
            Console.WriteLine("Fazer um programa para ler os dados de duas pessoas, depois mostrar o nome da pessoa mais velha:\n\n");

            Pessoa p1 = new();
            Pessoa p2 = new();

            Console.WriteLine("Dados da primeira pessoa:");
            Console.Write("Nome: ");
            p1.Nome = Console.ReadLine();
            Console.Write("Idade: ");
            p1.Idade = int.Parse(Console.ReadLine());

            Console.WriteLine("Dados da segunda pessoa:");
            Console.Write("Nome: ");
            p2.Nome = Console.ReadLine();
            Console.Write("Idade: ");
            p2.Idade = int.Parse(Console.ReadLine());

            Console.WriteLine("Pessoa mais velha: " + MaisVelho(p1, p2));
        }

        private static string? MaisVelho(Pessoa PrimeiraPessoa, Pessoa SegundaPessoa)
        {
            return (PrimeiraPessoa.Idade > SegundaPessoa.Idade) ? PrimeiraPessoa.Nome : SegundaPessoa.Nome;
        }
    }
}