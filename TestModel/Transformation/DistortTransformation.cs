using TestTools.Geometry;

namespace TestModel.Transformation
{
    public class DistortTransformation : ITransformation
    {
        public Point AxisX { get; private set; }
        public Point AxisY { get; private set; }

        public DistortTransformation(Point axisX, Point axisY)
        {
            AxisX = axisX;
            AxisY = axisY;
        }

        public Point Transform(Point point)
        {
            return
                new Point
                {
                    X = point.X * AxisX.X + point.Y * AxisY.X,
                    Y = point.X * AxisX.Y + point.Y * AxisY.Y
                };
        }

        public Point ReverseTransform(Point point)
        {
            var det = AxisX.X * AxisY.Y - AxisX.Y * AxisY.X;
            return
                new Point
                {
                    X = (+point.X * AxisY.Y - point.Y * AxisY.X) / det,
                    Y = (-point.X * AxisX.Y + point.Y * AxisX.X) / det
                };
        }

    }
}