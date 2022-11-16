using System.Globalization;

namespace S05_E01
{
    internal class ContaBancaria
    {
        public int Conta { get; private set; }
        public string Titular { get; set; }
        public double Saldo { get; private set; }

        public ContaBancaria(int conta, string titular)
        {
            Conta = conta;
            Titular = titular;
        }

        public ContaBancaria(int conta, string titular, double depositoInicial) : this(conta, titular)
        {
            Deposito(depositoInicial);
        }

        public void Deposito(double saldo)
        {
            Saldo += saldo;
        }

        public void Saque(double quantia)
        {
            Saldo = Saldo - quantia - 5.0;
        }

        public override string ToString()
        {
            return $"Conta {Conta}, Titular: {Titular}, Saldo: $ {Saldo.ToString("F2", CultureInfo.InvariantCulture)}";
        }
    }
}