using TestTools.Geometry;

namespace TestModel.Transformation
{
    public class RouteTransformationFactory
    {
        public ITransformation CreateFromRoute(ITransformation start, ITransformation finish)
        {
            return new CompositeTransformation(
                new ReverseTransformation(start), finish);
        }

        public ITransformation CreateFromOnePoint(Point start, Point finish)
        {
            var factory = new StandardTransformationFactory();
            return CreateFromRoute(factory.CreateFromOnePoint(start),
                factory.CreateFromOnePoint(finish));
        }

        public ITransformation CreateFromTwoPoints(Point[] start, Point[] finish)
        {
            var factory = new StandardTransformationFactory();
            return CreateFromRoute(factory.CreateFromTwoPoints(start),
                factory.CreateFromTwoPoints(finish));
        }

        public ITransformation CreateFromThreePoints(Point[] start, Point[] finish)
        {
            var factory = new StandardTransformationFactory();
            return CreateFromRoute(factory.CreateFromThreePoints(start),
                factory.CreateFromThreePoints(finish));
        }

        public ITransformation CreateFromFourPoints(Point[] start, Point[] finish)
        {
            var factory = new StandardTransformationFactory();
            return CreateFromRoute(factory.CreateFromFourPoints(start),
                factory.CreateFromFourPoints(finish));
        }
    }
}