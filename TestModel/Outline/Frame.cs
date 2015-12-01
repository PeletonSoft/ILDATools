using TestModel.Transformation;
using TestTools.Geometry;

namespace TestModel.Outline
{
    public class Frame : IFrameable
    {
        public TracePoint[] TracePoints { get; private set; }
        public ITransformation Transformation { get; private set; }
        public string Name { get; private set; }

        public Frame(TracePoint[] tracePoints, ITransformation transformation, string name)
        {
            TracePoints = tracePoints;
            Transformation = transformation;
            Name = name;

        }
    }
}
