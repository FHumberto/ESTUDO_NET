namespace S10_E03.Entities
{
    internal class Company : TaxPayer
    {
        public int NumberOfEmployees { get; set; }

        public Company(string? name, double yearlyIncome, int numberOfEmployees) : base(name, yearlyIncome)
        {
            NumberOfEmployees = numberOfEmployees;
        }

        public override double Tax()
        {
            double tax = (NumberOfEmployees > 10) ? 0.14 : 0.16;
            return YearlyIncome * tax;
        }
    }
}