namespace S10_E03.Entities
{
    internal class Individual : TaxPayer
    {
        public double HeathExpenditures { get; set; }

        public Individual(string? name, double yearlyIncome, double heathExpenditures) : base(name, yearlyIncome)
        {
            HeathExpenditures = heathExpenditures;
        }

        public override double Tax()
        {
            double tax = (YearlyIncome < 20000.00) ? 0.15 : 0.25;

            return (YearlyIncome * tax) - (HeathExpenditures * 0.5);
        }
    }
}