using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TestModel.Outline;
using TestTools.Helper;
using TestViewModel.FileContainer;

namespace TestViewModel.Source
{
    public sealed class MultiFrameSourceViewModel : INotifyPropertyChanged, ILoadable
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

        public void LoadFile(string fileName)
        {
            SelectedIndex = -1;

            if (fileName == null)
            {
                Frames = null;
                return;
            }

            var frames = FramesFactory.Create(fileName);

            Frames = frames
                .Select(frame => new FrameViewModel(frame, HostTransformation, FrameSaver))
                .ToList();
        }

        private IList<FrameViewModel> _frames;
        public IList<FrameViewModel> Frames
        {
            get { return _frames; }
            private set { SetField(ref _frames, value); }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (SetField(ref _selectedIndex, value))
                {
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        public FrameViewModel SelectedItem
        {
            get
            {
                return SelectedIndex == -1 || Frames == null
                    ? new FrameViewModel(new NullFramable(), HostTransformation, null)
                    : Frames[SelectedIndex];
            }
        }

        public MultiFrameSourceViewModel(IHostTransformation hostTransformation, 
            IFactory<IEnumerable<IFrameable>> framesFactory, IFrameSaver frameSaver)
        {
            FramesFactory = framesFactory;
            FrameSaver = frameSaver;
            HostTransformation = hostTransformation;
            OpenFileContainer = new OpenFileContainerViewModel(this, framesFactory.GetType());
        }

        public IFrameSaver FrameSaver { get; private set; }

        public IFactory<IEnumerable<IFrameable>> FramesFactory { get; private set; }

        public OpenFileContainerViewModel OpenFileContainer { get; private set; }

        public IHostTransformation HostTransformation { get; private set; }
    }
}
