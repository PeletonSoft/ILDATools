using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TestModel.Source;
using TestModel.Source.Ild;
using TestModel.Transformation;
using TestTools.Helper;
using TestViewModel.Outline;
using TestViewModel.Source;

namespace TestViewModel
{
    public sealed class WorkspaceViewModel : IHostTransformation
    {
        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }
        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            return notificator.SetField(ref field, value);
        }
        #endregion

        private QuadrangleViewModel _quadrangle;

        public QuadrangleViewModel Quadrangle
        {
            get { return _quadrangle; }
            set
            {
                if (SetField(ref _quadrangle, value) && value != null)
                {
                    value.PropertyChanged += (sender, args) =>
                        QuadranglePropertyChanged(args.PropertyName);
                }
            }
        }

        private void QuadranglePropertyChanged(string propertyName)
        {
            var list = new[] {"TopLeft", "TopRight", "BottomLeft", "BottomRight"};
            if (list.Contains(propertyName))
            {
                OnPropertyChanged("Quadrangle");
            }
        }

        private ITransformation _transformation;

        public ITransformation Transformation
        {
            get { return _transformation; }
            private set { SetField(ref _transformation, value); }
        }

        public void CalculateTransformation()
        {
            var polygon = new[]
            {
                Quadrangle.BottomLeft.ToPoint(),
                Quadrangle.BottomRight.ToPoint(),
                Quadrangle.TopLeft.ToPoint(),
                Quadrangle.TopRight.ToPoint()
            };

            var transformationFactory = new StandardTransformationFactory();
            Transformation = transformationFactory.CreateFromFourPoints(polygon);
        }

        public MultiFrameSourceViewModel IldMultiFrameSource { get; private set; }
        public FrameSourceViewModel XmlFrameSource { get; private set; }
        public FrameSourceViewModel PltFrameSource { get; private set; }
        public FrameSourceViewModel DatFrameSource { get; private set; }

        public WorkspaceViewModel()
        {
            IldMultiFrameSource = new MultiFrameSourceViewModel(this, new IldMultiFrameFactory(), new IldMultiFrameFactory());
            XmlFrameSource = new FrameSourceViewModel(this, new XmlFrameFactory(), new IldMultiFrameFactory());
            PltFrameSource = new FrameSourceViewModel(this, new PltFrameFactory(), new IldMultiFrameFactory());
            DatFrameSource = new FrameSourceViewModel(this, new DatFrameFactory(), new IldMultiFrameFactory());

            PropertyChanged +=
                (sender, args) =>
                {
                    switch (args.PropertyName)
                    {
                        case "Quadrangle":
                            CalculateTransformation();
                            break;
                    }
                };
        }

    }
}
