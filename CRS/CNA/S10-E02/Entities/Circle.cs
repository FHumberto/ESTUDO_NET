using S10_E02.Entities.Enums;

namespace S10_E02.Entities
{
    internal class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(Color color, double radius) : base(color)
        {
            Radius = radius;
        }

        public override double Area()
        {
            return 3.14159265359 * Math.Pow(Radius, 2);
        }
    }
}