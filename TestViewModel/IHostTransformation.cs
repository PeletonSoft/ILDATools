using System.ComponentModel;
using TestModel.Transformation;
using TestViewModel.Outline;

namespace TestViewModel
{
    public interface IHostTransformation : INotifyPropertyChanged
    {
        ITransformation Transformation { get; }
        QuadrangleViewModel Quadrangle { get; }
    }
}
