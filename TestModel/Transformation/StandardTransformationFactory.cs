using TestTools.Geometry;

namespace TestModel.Transformation
{
    public class StandardTransformationFactory
    {
        public ITransformation CreateFromOnePoint(Point point)
        {
            return new TranslateTransformation(point);
        }

        public ITransformation CreateFromTwoPoints(Point[] points)
        {
            var translate = new TranslateTransformation(points[0]);
            var finish = translate.ReverseTransform(points[1]);
            var distort = new DistortTransformation(finish, new Point(-finish.Y, finish.X));
            return new CompositeTransformation(distort, translate);
        }

        public ITransformation CreateFromThreePoints(Point[] points)
        {
            var translate = new TranslateTransformation(points[0]);
            var axisX = translate.ReverseTransform(points[1]);
            var axisY = translate.ReverseTransform(points[2]);
            var distort = new DistortTransformation(axisX, axisY);

            return new CompositeTransformation(distort, translate);
        }

        public ITransformation CreateFromFourPoints(Point[] points)
        {
            var translate = new TranslateTransformation(points[0]);
            var axisX = translate.ReverseTransform(points[1]);
            var axisY = translate.ReverseTransform(points[2]);
            var distort = new DistortTransformation(axisX, axisY);
            var arrow = distort.ReverseTransform(
                translate.ReverseTransform(points[3]));
            var perspective = new PerspectiveTransformation(arrow);
            return new CompositeTransformation(perspective, distort, translate);
        }
    }
}