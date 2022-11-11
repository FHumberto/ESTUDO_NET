using System.Globalization;

namespace S03_E04
{
    internal static class G
    {
        public static void GMain()
        {
            Console.WriteLine("Leia 2 valores com uma casa decimal (x e y), que devem representar as coordenadas de um ponto em um plano. A seguir, determine qual o quadrante ao qual pertence o ponto, ou se está sobre um dos eixos cartesianos ou na origem (x = y = 0). Se o ponto estiver na origem, escreva a mensagem “Origem”. Se o ponto estiver sobre um dos eixos escreva “Eixo X” ou “Eixo Y”, conforme for a situação");
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