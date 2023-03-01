namespace S10_E03.Entities
{
    internal abstract class TaxPayer
    {
        public string? Name { get; set; }
        public double YearlyIncome { get; set; }

        protected TaxPayer(string? name, double yearlyIncome)
        {
            Name = name;
            YearlyIncome = yearlyIncome;
        }

        public abstract double Tax();
    }
}