using TestTools.Geometry;

namespace TestModel.Transformation
{
    public interface ITransformation
    {
        Point Transform(Point point);
        Point ReverseTransform(Point point);
    }

    
}
