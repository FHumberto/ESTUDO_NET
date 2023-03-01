using System.Globalization;

namespace S03_E03
{
    internal static class F
    {
        public static void FMain()
        {
            Console.WriteLine("Fazer um programa que leia três valores com ponto flutuante de dupla precisão: A, B e C. Em seguida, calcule e mostre:");
            Console.WriteLine("a) a área do triângulo retângulo que tem A por base e C por altura.");
            Console.WriteLine("b) a área do círculo de raio C. (pi = 3.14159)");
            Console.WriteLine("a área do trapézio que tem A e B por bases e C por altura");
            Console.WriteLine("a área do quadrado que tem lado B.");
            Console.WriteLine("a área do retângulo que tem lados A e B.");
            string[] linha = Console.ReadLine().Split(' ');
            float a = float.Parse(linha[0], CultureInfo.InvariantCulture);
            float b = float.Parse(linha[1], CultureInfo.InvariantCulture);
            float c = float.Parse(linha[2], CultureInfo.InvariantCulture);

            Console.WriteLine("TRIANGULO: " + (a * c / 2.0).ToString("F3", CultureInfo.InvariantCulture));
            Console.WriteLine("CIRCULO: " + (3.14159 * (c * c)).ToString("F3", CultureInfo.InvariantCulture));
            Console.WriteLine("TRAPEZIO: " + (((a + b) * c) / 2.0).ToString("F3", CultureInfo.InvariantCulture));
            Console.WriteLine("QUADRADO: " + (b * b).ToString("F3", CultureInfo.InvariantCulture));
            Console.WriteLine("RETANGULO: " + (a * b).ToString("F3", CultureInfo.InvariantCulture));
        }
    }
}