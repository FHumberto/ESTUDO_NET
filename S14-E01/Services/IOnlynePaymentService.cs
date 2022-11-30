namespace S14_E01.Services
{
    internal interface IOnlynePaymentService
    {
        public double PaymentFee(double amount);

        public double Interest(double amount, int months);
    }
}