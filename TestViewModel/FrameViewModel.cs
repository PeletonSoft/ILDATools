using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TestModel.Outline;
using TestModel.Transformation;
using TestTools.Geometry;
using TestTools.Helper;
using TestViewModel.FileContainer;
using TestViewModel.Geomentry;
using TestViewModel.Outline;

namespace TestViewModel
{
    public sealed class FrameViewModel : INotifyPropertyChanged, ISaveable
    {

        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }
        #endregion

        public FrameViewModel(IFrameable frameable, 
            IHostTransformation hostTransformation, IFrameSaver frameSaver)
        {
            Frameable = frameable;
            HostTransformation = hostTransformation;
            FrameSaver = frameSaver;

            hostTransformation.PropertyChanged +=
                (sender, args) =>
                {
                    switch (args.PropertyName)
                    {
                        case "Quadrangle":
                            OnPropertyChanged("Quadrangle");
                            break;
                        case "Transformation":
                            OnPropertyChanged("Transformation");
                            OnPropertyChanged("Lines");
                            OnPropertyChanged("Points");
                            OnPropertyChanged("Elements");
                            break;
                    }
                };
            if (frameSaver != null)
            {
                SaveFileContainer = new SaveFileContainerViewModel(this, frameSaver.GetType());
            }
        }

        public SaveFileContainerViewModel SaveFileContainer { get; private set; }
        private IHostTransformation HostTransformation { get; set; }
        public IFrameSaver FrameSaver { get; set; }

        public string Name
        {
            get { return Frameable.Name; }
        }

        public IFrameable Frameable { get; set; }

        public ITransformation Transformation
        {
            get { return HostTransformation.Transformation; }
        }

        public QuadrangleViewModel Quadrangle
        {
            get { return HostTransformation.Quadrangle; }
        }

        public IEnumerable<PointViewModel> Points
        {
            get
            {
                return Frameable.GetPoints()
                    .Select(Transformation.Transform)
                    .Select(point => new PointViewModel(point));
            }
        }

        public IEnumerable<LineViewModel> Lines
        {
            get
            {
                return Frameable.GetLines()
                    .Select(line => new Line
                    {
                        Start = Transformation.Transform(line.Start),
                        Finish = Transformation.Transform(line.Finish)
                    })
                    .Select(line => new LineViewModel(line));
            }
        }

        public IEnumerable<IDrawViewModel> Elements
        {
            get
            {
                return ((IEnumerable<IDrawViewModel>)Points).Union(Lines);
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public void SaveFile(string fileName)
        {
            FrameSaver.Save(fileName, Frameable);
        }
    }
}
