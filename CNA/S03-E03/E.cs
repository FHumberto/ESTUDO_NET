using System.Globalization;

namespace S03_E03
{
    internal static class E
    {
        public static void EMain()
        {
            Console.WriteLine("ler o código de uma peça 1, o número de peças 1, o valor unitário de cada peça 1, o  código de uma peça 2, o número de peças 2 e o valor unitário de cada peça 2. Calcule e mostre o valor a ser pago:");
            int[] nPeca = new int[2];
            float[] vPeca = new float[2];

            string[] valores = Console.ReadLine().Split(' ');
            _ = int.Parse(valores[0]);
            nPeca[0] = int.Parse(valores[1]);
            vPeca[0] = float.Parse(valores[2], CultureInfo.InvariantCulture);

            valores = Console.ReadLine().Split(" ");
            _ = int.Parse(valores[0]);
            nPeca[1] = int.Parse(valores[1]);
            vPeca[1] = float.Parse(valores[2], CultureInfo.InvariantCulture);

            float total = (nPeca[0] * vPeca[0]) + (nPeca[1] * vPeca[1]);

            Console.WriteLine("VALOR A PAGAR: R$ " + total.ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}