using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TestTools.DialogAttribute;
using TestTools.DialogsOptions;
using TestTools.Helper;
using TestViewModel.Tools;

namespace TestViewModel.FileContainer
{
    public sealed class OpenFileContainerViewModel : INotifyPropertyChanged, IOpenDialogOptions
    {
        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator= () => OnPropertyChanged(propertyName);
            notificator.SetField(ref field, value);
        }

        #endregion

        public string Filter { get; set; }
        public string DefaultExt { get; set; }
        public OpenFileContainerViewModel(ILoadable loadable, Type factoryType)
        {
            Loadable = loadable;
            OpenFileCommand = new RelayCommand(OpenFile);

            var attrs = Attribute.GetCustomAttributes(factoryType);
            var openDialogOptions = attrs
                .OfType<OpenDialogOptionsAttribute>()
                .FirstOrDefault();

            if (openDialogOptions != null)
            {
                Filter = openDialogOptions.Filter;
                DefaultExt = openDialogOptions.DefaultExt;
            }
        }

        public ILoadable Loadable { get; private set; }

        public ICommand OpenFileCommand { get; private set; }

        private void OpenFile(object parameter)
        {
            FileName = (string) parameter;
        }

        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set
            {
                SetField(ref _fileName, value);
                Loadable.LoadFile(value);
            }
        }

    }
}
