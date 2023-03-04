using S10_E02.Entities.Enums;

namespace S10_E02.Entities
{
    internal abstract class Shape
    {
        public Color Color { get; set; }

        protected Shape(Color color)
        {
            Color = color;
        }

        public abstract double Area();
    }
}