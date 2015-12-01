using TestTools.Geometry;

namespace TestModel.Transformation
{
    public static class TransformationHelper
    {
        public static Line Transform(this ITransformation transformation, Line line)
        {
            return new Line
            {
                Start = transformation.Transform(line.Start),
                Finish = transformation.Transform(line.Finish)
            };
        }

        public static Line ReverseTransform(this ITransformation transformation, Line line)
        {
            return new Line
            {
                Start = transformation.ReverseTransform(line.Start),
                Finish = transformation.ReverseTransform(line.Finish)
            };
        }
    }
}
