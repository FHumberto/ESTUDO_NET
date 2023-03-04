using System.Globalization;

namespace S04_E03
{
    internal static class Index
    {
        public static void Main()
        {
            Console.WriteLine("Faça um programa para ler a cotação do dólar, e depois um valor em dólares a ser comprado por uma pessoa em reais.");
            Console.WriteLine("Informar quantos reais a pessoa vai pagar pelos dólares, considerando ainda que a pessoa terá que pagar 6% de IOF");
            Console.WriteLine("sobre o valor em dólar. Criar uma classe ConversorDeMoeda para ser responsável pelos cálculos\n");

            Console.WriteLine("Qual é a cotação do dólar? ");
            double cotacao = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine("Quantos dólares você vai comprar? ");
            double quantia = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            double valorConvertido = ConversorDeMoeda.DolarParaReal(cotacao, quantia);
            Console.WriteLine("Valor a ser pago em reais = " + valorConvertido.ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}