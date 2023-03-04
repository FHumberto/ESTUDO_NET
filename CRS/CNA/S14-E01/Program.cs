using S14_E01.Entities;
using S14_E01.Services;

using System.Globalization;

namespace S14_E01
{
    internal static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Enter contract data:");
            Console.Write("Number: ");
            int number = int.Parse(Console.ReadLine());
            Console.Write("Date (dd/MM/yyyy): ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            Console.Write("Contract value: ");
            double contractValue = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.Write("Enter number of installments: ");
            int months = int.Parse(Console.ReadLine());

            Contract myContract = new(number, date, contractValue);

            ContractService contractService = new(new PaypalService());
            contractService.ProcessContract(myContract, months);

            Console.WriteLine();
            Console.WriteLine("Installments:");
            foreach (Installment installment in myContract.Installments)
            {
                Console.WriteLine(installment);
            }
        }
    }
}