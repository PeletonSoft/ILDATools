namespace TestModel.Outline
{
    public interface IFrameSaver
    {
        void Save(string fileName, IFrameable frameable);
    }
}