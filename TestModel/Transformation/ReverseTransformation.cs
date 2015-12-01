using TestTools.Geometry;

namespace TestModel.Transformation
{
    public class ReverseTransformation : ITransformation
    {
        public ITransformation InnerTransformation { get; private set; }

        public ReverseTransformation(ITransformation transformation)
        {
            InnerTransformation = transformation;
        }

        public Point Transform(Point point)
        {
            return InnerTransformation.ReverseTransform(point);
        }

        public Point ReverseTransform(Point point)
        {
            return InnerTransformation.Transform(point);
        }
    }
}