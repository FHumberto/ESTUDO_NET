using System.Globalization;

namespace S05_E01
{
    internal static class Index
    {
        private static void Main()
        {
            ContaBancaria conta;

            Console.Write("Entre com o número da conta: ");
            int numero = int.Parse(Console.ReadLine());
            Console.Write("Entre o titular da conta: ");
            string? titular = Console.ReadLine();
            Console.Write("Haverá depósito inicial (s/n)? ");
            char resp = char.Parse(Console.ReadLine().ToLower());

            if (resp == 's')
            {
                Console.Write("Entre o valor para depósito inicial: ");
                double saldo = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                conta = new(numero, titular, saldo);
            }
            else
            {
                conta = new(numero, titular);
            }

            int operacao = 0;
            while (operacao != 3)
            {
                Console.WriteLine("\nQue tipo de operação deseja fazer? [0: Dados da Conta, 1: Depósito, 2: Saque, 3: Finalizar]");
                operacao = int.Parse(Console.ReadLine());

                switch (operacao)
                {
                    case 0:
                        Console.WriteLine();
                        Console.WriteLine(conta);
                        break;

                    case 1:
                        Console.Write("\nEntre um valor para depósito: ");
                        conta.Deposito(double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture));
                        Console.WriteLine("Dados da conta atualizados:");
                        Console.WriteLine(conta);
                        break;

                    case 2:
                        Console.Write("\nEntre um valor para saque: ");
                        conta.Saque(double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture));
                        Console.WriteLine("Dados da conta atualizados:");
                        Console.WriteLine(conta);
                        break;
                }
            }
        }
    }
}