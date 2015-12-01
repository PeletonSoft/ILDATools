using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace TestView.Behaviors
{
    public class SelectAllTextOnPressEnterBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewKeyDown += AssociatedObjectOnPreviewKeyDown;
        }

        private void AssociatedObjectOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Action finish =
                    () => AssociatedObject.SelectAll();

                ThreadStart start =
                    () =>
                    {
                        Thread.Sleep(50);
                        Dispatcher.Invoke(finish);
                    };

                var thread = new Thread(start);
                thread.Start();

            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewKeyDown -= AssociatedObjectOnPreviewKeyDown;
        }

    }

}
