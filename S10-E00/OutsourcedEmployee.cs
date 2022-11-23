namespace S10_E00
{
    internal class OutsourcedEmployee : Employee
    {
        public double AddicionalCharge { get; set; }

        public OutsourcedEmployee()
        { }

        public OutsourcedEmployee(string? name, int hours, double valuePerHour, double adicionalCharge) : base(name, hours, valuePerHour)
        {
            AddicionalCharge = adicionalCharge;
        }

        public override sealed double Payment()
        {
            return base.Payment() + 1.1 * AddicionalCharge;
        }
    }
}