using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using TestTools.Helper;

namespace TestViewModel.Outline
{
    public sealed class QuadrangleViewModel : INotifyPropertyChanged
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

        private LocationViewModel _topLeft;
        public LocationViewModel TopLeft
        {
            get { return _topLeft; }
            set
            {
                if (SetField(ref _topLeft, value) && value != null)
                {
                    value.PropertyChanged +=
                        (sender, arg) => LocationOnPropertyChanged(arg.PropertyName);
                }
            }
        }
        private LocationViewModel _topRight;
        public LocationViewModel TopRight
        {
            get { return _topRight; }
            set
            {
                if (SetField(ref _topRight, value) && value != null)
                {
                    value.PropertyChanged += 
                        (sender,arg) => LocationOnPropertyChanged(arg.PropertyName);
                }
            }
        }
        private LocationViewModel _bottomLeft;
        public LocationViewModel BottomLeft
        {
            get { return _bottomLeft; }
            set
            {
                if (SetField(ref _bottomLeft, value) && value != null)
                {
                    value.PropertyChanged += 
                        (sender,arg) => LocationOnPropertyChanged(arg.PropertyName);
                } }
        }
        private LocationViewModel _bottomRight;
        public LocationViewModel BottomRight
        {
            get { return _bottomRight; }
            set
            {
                if (SetField(ref _bottomRight, value) && value != null)
                {
                    value.PropertyChanged += 
                        (sender,arg) => LocationOnPropertyChanged(arg.PropertyName);
                } }
        }

        private void LocationOnPropertyChanged(string changedPropertyName, 
            [CallerMemberName] string propertyName = null)
        {
            var list = new[] { "X", "Y" };
            if (list.Contains(changedPropertyName))
            {
                OnPropertyChanged(propertyName);
            }
        }
    }
}
