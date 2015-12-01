using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestTools.Geometry;
using TestTools.Helper;


namespace TestViewModel.Outline
{
    public sealed class LocationViewModel : INotifyPropertyChanged
    {

        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            notificator.SetField(ref field, value);
        }

        #endregion

        private double _x;
        public double X
        {
            get { return _x; }
            set { SetField(ref _x, value); }
        }

        private double _y;
        public double Y
        {
            get { return _y; }
            set { SetField(ref _y, value); }
        }

        public LocationViewModel(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public LocationViewModel()
        {
        }

        public Point ToPoint()
        {
            return  new Point(X,Y);
        }
    }
}
