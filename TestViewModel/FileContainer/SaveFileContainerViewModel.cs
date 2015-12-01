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
    public sealed class SaveFileContainerViewModel : INotifyPropertyChanged, ISaveDialogOptions
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

        public string Filter { get; set; }
        public string DefaultExt { get; set; }
        public SaveFileContainerViewModel(ISavable savable, Type factoryType)
        {
            Savable = savable;
            SaveFileCommand = new RelayCommand(SaveFile);

            var attrs = Attribute.GetCustomAttributes(factoryType);
            var options = attrs
                .OfType<SaveDialogOptionsAttribute>()
                .FirstOrDefault();

            if (options != null)
            {
                Filter = options.Filter;
                DefaultExt = options.DefaultExt;
            }
        }

        public ISavable Savable { get; private set; }
        public ICommand SaveFileCommand { get; private set; }

        private void SaveFile(object parameter)
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
                Savable.SaveFile(value);
            }
        }

    }
}
