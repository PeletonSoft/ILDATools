using TestTools.Geometry;

namespace TestModel.Transformation
{
    public class TranslateTransformation : ITransformation
    {
        public Point Offest { get; private set; }
        public TranslateTransformation(Point offest)
        {
            Offest = offest;
        }

        public Point Transform(Point point)
        {
            return new Point(point.X + Offest.X, point.Y + Offest.Y);
        }

        public Point ReverseTransform(Point point)
        {
            return new Point(point.X - Offest.X, point.Y - Offest.Y);
        }
    }
}