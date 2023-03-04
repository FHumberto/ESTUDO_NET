namespace S14_E01.Services
{
    internal class PaypalService : IOnlynePaymentService
    {
        private const double _feePercentage = 0.02;
        private const double _monthlyInterest = 0.01;

        public double PaymentFee(double amount)
        {
            return amount * _feePercentage;
        }

        public double Interest(double amount, int months)
        {
            return amount * _monthlyInterest * months;
        }
    }
}