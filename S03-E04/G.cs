using System.Globalization;

namespace S03_E04
{
    static class G
    {
        public static void GMain()
        {
            string[] valores = Console.ReadLine().Split(' ');
            float x = float.Parse(valores[0], CultureInfo.InvariantCulture);
            float y = float.Parse(valores[1], CultureInfo.InvariantCulture);

            if (x > 0 && y > 0)
            {
                Console.WriteLine("Q1");
            }
            else if (x < 0 && y > 0)
            {
                Console.WriteLine("Q2");
            }
            else if (x < 0 && y < 0)
            {
                Console.WriteLine("Q3");
            }
            else if (x > 0 && y < 0)
            {
                Console.WriteLine("Q4");
            }
            else if (x == 0 && y > 0)
            {
                Console.WriteLine("Eixo Y");
            }
            else if (x > 0 && y == 0)
            {
                Console.WriteLine("Eixo X");
            }
            else
            {
                Console.WriteLine("Origem");
            }
        }
    }
}
