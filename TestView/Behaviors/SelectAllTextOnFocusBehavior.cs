using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace TestView.Behaviors
{
    public class SelectAllTextOnFocusBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotKeyboardFocus += AssociatedObjectGotKeyboardFocus;
            AssociatedObject.GotMouseCapture += AssociatedObjectGotMouseCapture;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotKeyboardFocus -= AssociatedObjectGotKeyboardFocus;
            AssociatedObject.GotMouseCapture -= AssociatedObjectGotMouseCapture;
        }

        private void AssociatedObjectGotKeyboardFocus(object sender,
        System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            DoSelectAll();
        }

        private void AssociatedObjectGotMouseCapture(object sender,
        System.Windows.Input.MouseEventArgs e)
        {
            DoSelectAll();
        }

        private void DoSelectAll()
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

}
