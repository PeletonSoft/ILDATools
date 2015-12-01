using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace TestView.Behaviors
{
    public class UpdatePropertyOnEnterPressBehavior : Behavior<UIElement>
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
                if (Element != null && Property != null)
                {
                    var bindingExpression = BindingOperations.GetBindingExpression(Element, Property);
                    if (bindingExpression != null)
                    {
                        bindingExpression.UpdateSource();
                    }

                    var multiBindingExpression = BindingOperations.GetMultiBindingExpression(Element, Property);
                    if(multiBindingExpression != null)
                    {
                        multiBindingExpression.UpdateSource();
                    }

                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewKeyDown -= AssociatedObjectOnPreviewKeyDown;
        }

        public DependencyObject Element
        {
            get
            {
                return (DependencyObject)this.GetValue(ElementProperty);
            }
            set
            {
                this.SetValue(ElementProperty, value);
            }
        }

        public static readonly DependencyProperty ElementProperty = DependencyProperty.Register(
          "Element", typeof(DependencyObject), typeof(UpdatePropertyOnEnterPressBehavior), new PropertyMetadata(null));

        public DependencyProperty Property
        {
            get
            {
                return (DependencyProperty)this.GetValue(PropertyProperty);
            }
            set
            {
                this.SetValue(PropertyProperty, value);
            }
        }

        public static readonly DependencyProperty PropertyProperty = DependencyProperty.Register(
          "Property", typeof(DependencyProperty), typeof(UpdatePropertyOnEnterPressBehavior), new PropertyMetadata(null));
    }

}
