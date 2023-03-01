using S10_E02.Entities;
using S10_E02.Entities.Enums;

using System.Globalization;

namespace S10_E02
{
    internal static class Program
    {
        private static void Main()
        {
            Console.Write("Enter the number of shapes: ");
            int n = int.Parse(Console.ReadLine());

            List<Shape> shapes = new();

            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine($"Shape {i} data:");
                Console.Write("Rectangle or Circle (r/c)? ");
                string opt = Console.ReadLine().ToLower();
                Console.Write("Color (Black/Blue/Red): ");
                Color color = Enum.Parse<Color>(Console.ReadLine());
                if (opt == "r")
                {
                    Console.Write("Width: ");
                    double width = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    Console.Write("Height: ");
                    double height = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    shapes.Add(new Rectangle(color, width, height));
                }
                else
                {
                    Console.Write("Radius: ");
                    double radius = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    shapes.Add(new Circle(color, radius));
                }
            }

            Console.WriteLine();
            Console.WriteLine("SHAPE AREAS:");
            foreach (Shape shape in shapes)
            {
                Console.WriteLine(shape.Area().ToString("F2", CultureInfo.InvariantCulture));
            }
        }
    }
}