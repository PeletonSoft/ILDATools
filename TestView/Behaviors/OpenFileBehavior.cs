using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;
using Microsoft.Win32;
using TestTools.DialogsOptions;

namespace TestView.Behaviors
{
    public class OpenFileBehavior : Behavior<ButtonBase>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Click += AssociatedObjectOnClick;
        }

        private void AssociatedObjectOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (Command != null && !Command.CanExecute(null))
            {
                return;
            }

            var fileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                AddExtension = false
            };

            var options = AssociatedObject.DataContext as IOpenDialogOptions;
            if (options != null)
            {
                fileDialog.Filter = options.Filter;
                fileDialog.DefaultExt = options.DefaultExt;
            }

            if (fileDialog.ShowDialog() == true && Command != null)
            {
                var parameter = fileDialog.FileName;
                Command.Execute(parameter);
            }
        }

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
          "Command", typeof(ICommand), typeof(OpenFileBehavior), new PropertyMetadata(null));

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= AssociatedObjectOnClick;
        }
    }

}
