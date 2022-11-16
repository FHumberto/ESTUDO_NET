using System.Globalization;

namespace S04_E02.E03
{
    internal static class C
    {
        public static void CMain()
        {
            Console.WriteLine("ler o nome de um aluno e as três notas que ele obteve nos três trimestres do ano (primeiro trimestre vale 30 e o segundo e terceiro valem 35 cada).");
            Console.WriteLine("Ao final, mostrar qual a nota final do aluno no ano. Dizer também se o aluno está APROVADO ou REPROVADO e, em caso negativo, quantos pontos faltam");
            Console.WriteLine("para o aluno obter o mínimo para ser aprovado (que é 60 pontos).");

            Aluno a = new();

            Console.Write("\nNome do aluno: ");
            a.Nome = Console.ReadLine();

            for (int i = 0; i <= 2; i++)
            {
                a.Notas[i] = float.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            }

            Console.WriteLine("NOTA FINAL = " + a.NotaFinal().ToString("F2", CultureInfo.InvariantCulture));
            if (a.NotaFinal() >= 60.0)
            {
                Console.WriteLine("APROVADO");
            }
            else
            {
                double Restam = 60.0 - a.NotaFinal();
                Console.WriteLine("REPROVADO");
                Console.WriteLine($"FALTAM {Restam.ToString("F2", CultureInfo.InvariantCulture)} PONTOS");
            }
        }
    }
}