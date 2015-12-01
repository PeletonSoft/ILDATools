using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestModel.Outline;
using TestTools.Helper;
using TestViewModel.FileContainer;

namespace TestViewModel.Source
{
    public sealed class FrameSourceViewModel : INotifyPropertyChanged, ILoadable
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

        private FrameViewModel _frame;
        public FrameViewModel Frame
        {
            get { return _frame; }
            set { SetField(ref _frame, value); }
        }

        public OpenFileContainerViewModel OpenFileContainer { get; private set; }
        public IHostTransformation HostTransformation { get; private set; }

        public IFrameSaver FrameSaver { get; private set; }
        public IFactory<IFrameable> FrameFactory { get; private set; }

        public void LoadFile(string fileName)
        {
            if (fileName == null)
            {
                Frame = new FrameViewModel(new NullFramable(), HostTransformation, null);
                return;
            }

            var frameable = FrameFactory.Create(fileName);
            Frame = new FrameViewModel(frameable, HostTransformation, FrameSaver);
        }

        public FrameSourceViewModel(IHostTransformation hostTransformation, IFactory<IFrameable> frameFactory, IFrameSaver frameSaver)
        {
            FrameFactory = frameFactory;
            FrameSaver = frameSaver;
            HostTransformation = hostTransformation;
            OpenFileContainer = new OpenFileContainerViewModel(this, frameFactory.GetType());
            Frame = new FrameViewModel(new NullFramable(), HostTransformation, null);
        }
    }
}