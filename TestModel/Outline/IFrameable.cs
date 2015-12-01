using TestModel.Transformation;
using TestTools.Geometry;

namespace TestModel.Outline
{
    public interface IFrameable
    {
        TracePoint[] TracePoints { get; }
        ITransformation Transformation { get; }
        string Name { get; }
    }
}
