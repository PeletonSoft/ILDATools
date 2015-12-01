using TestTools.Geometry;

namespace TestModel.Transformation
{
    public class PerspectiveTransformation : ITransformation
    {
        public Point Arrow { get; private set; }

        public PerspectiveTransformation(Point arrow)
        {
            Arrow = arrow;
        }

        public Point Transform(Point point)
        {
            var w = (point.X - 1) * (Arrow.Y - 1) + (point.Y - 1) * (Arrow.X - 1) - 1;
            return
                new Point
                {
                    X = -point.X * Arrow.X / w,
                    Y = -point.Y * Arrow.Y / w
                };
        }

        public Point ReverseTransform(Point point)
        {
            var w = (point.Y * Arrow.X * (Arrow.X - 1) + point.X * Arrow.Y * (Arrow.Y - 1) + Arrow.X * Arrow.Y) /
                    (Arrow.X + Arrow.Y - 1);
            return
                new Point
                {
                    X = point.X * Arrow.Y / w,
                    Y = point.Y * Arrow.X / w
                };
        }

    }
}