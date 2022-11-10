using System.Globalization;

namespace S03_E02
{
    static class Index
    {
        static void Main()
        {
            Console.WriteLine("Entre com o seu nome completo:");
            string? nomeCompleto = Console.ReadLine();
            Console.WriteLine("Quantos quartos tem na sua casa?");
            int quartos = int.Parse(Console.ReadLine());
            Console.WriteLine("Entre com o preço de um produto:");
            float preco = float.Parse(Console.ReadLine());
            Console.WriteLine("Entre seu último nome, idade e altura (mesma linha):");
            string[] l = Console.ReadLine().Split(' ');
            string nome = l[0];
            int idade = int.Parse(l[1]);
            float altura = float.Parse(l[2], CultureInfo.InvariantCulture);

            Console.Write("\n");
            Console.WriteLine(nomeCompleto);
            Console.WriteLine(quartos);
            Console.WriteLine(preco.ToString("F2", CultureInfo.InvariantCulture));
            Console.WriteLine(nome);
            Console.WriteLine(idade);
            Console.WriteLine(altura.ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}