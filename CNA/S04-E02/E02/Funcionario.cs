using System.Globalization;

namespace S04_E02.E02
{
    internal class Funcionario
    {
        public string? Nome;
        public double SalarioBruto;
        public double Imposto;

        public double SalarioLiquido()
        {
            return SalarioBruto - Imposto;
        }

        public double AumentarSalario(double porcentagem)
        {
            return SalarioBruto += SalarioBruto * porcentagem / 100.0;
        }

        public override string ToString()
        {
            return $"{Nome}, ${SalarioLiquido().ToString("F2", CultureInfo.InvariantCulture)}";
        }
    }
}