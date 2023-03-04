namespace S04_E03
{
    public static class ConversorDeMoeda
    {
        public const double Iof = 6.0;

        public static double DolarParaReal(double cotacao, double quantia)
        {
            double total = quantia * cotacao;
            return total + total * Iof / 100.0;
        }
    }
}