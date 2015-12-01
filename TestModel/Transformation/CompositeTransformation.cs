using System.Collections.Generic;
using System.Linq;
using TestTools.Geometry;

namespace TestModel.Transformation
{
    public class CompositeTransformation : ITransformation
    {
        public IEnumerable<ITransformation> Transformations { get; private set; }

        public Point Transform(Point point)
        {
            var result = point;
            foreach (var transformation in Transformations)
            {
                result = transformation.Transform(result);
            }
            return result;
        }

        public Point ReverseTransform(Point point)
        {
            var result = point;
            foreach (var transformation in Transformations.Reverse())
            {
                result = transformation.ReverseTransform(result);
            }
            return result;
        }

        public CompositeTransformation(IEnumerable<ITransformation> transformations)
        {
            Transformations = transformations;
        }

        public CompositeTransformation(params ITransformation[] transformations)
        {
            Transformations = transformations;
        }
    }
}