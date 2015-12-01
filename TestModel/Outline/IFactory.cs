using TestModel.Transformation;

namespace TestModel.Outline
{
    public interface IFactory<out T>
    {
        ITransformation Transformation { get; }
        T Create(string fileName);
    }
}