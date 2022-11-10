using System.Globalization;

namespace S03_E03
{
    static class D
    {
        public static void DMain()
        {
            int cod = int.Parse(Console.ReadLine());
            int hora = int.Parse(Console.ReadLine());
            float valor = float.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            Console.WriteLine("NUMBER = " + cod);
            Console.WriteLine("SALARY = U$ " + (hora * valor).ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}
