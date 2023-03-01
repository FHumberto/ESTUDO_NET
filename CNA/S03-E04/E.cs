using System.Globalization;

namespace S03_E04
{
    internal static class E
    {
        public static void EMain()
        {
            Console.WriteLine("escreva um programa que leia o código de um item e a quantidade deste item. A seguir, calcule e mostre o valor da conta a pagar");
            string[] valores = Console.ReadLine().Split(' ');
            int cod = int.Parse(valores[0]);
            int qtd = int.Parse(valores[1]);
            double total = 0;
            switch (cod)
            {
                case 1:
                    total = qtd * 4.00;
                    break;

                case 2:
                    total = qtd * 4.50;
                    break;

                case 3:
                    total = qtd * 5.00;
                    break;

                case 4:
                    total = qtd * 2.00;
                    break;

                case 5:
                    total = qtd * 1.50;
                    break;
            }
            Console.WriteLine("Total: " + total.ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}