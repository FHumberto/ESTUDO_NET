using System.Globalization;

namespace S03_E03
{
    internal static class D
    {
        public static void DMain()
        {
            Console.WriteLine("leia o número de um funcionário, seu número de horas trabalhadas, o valor que recebe por hora e calcula o salário desse funcionário. A seguir, mostre o número e o salário do funcionário, com duas casas decimais");
            int cod = int.Parse(Console.ReadLine());
            int hora = int.Parse(Console.ReadLine());
            float valor = float.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            Console.WriteLine("NUMBER = " + cod);
            Console.WriteLine("SALARY = U$ " + (hora * valor).ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}