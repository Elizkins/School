using System.Windows;
using System.Windows.Controls;

namespace School
{
    public class TextBoxExtended : TextBox
    {
        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }
            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
            nameof(Placeholder), typeof(string), typeof(TextBoxExtended), new PropertyMetadata(""));

        public TextBoxExtended()
        {
            DefaultStyleKey = typeof(TextBoxExtended);
        }
    }
}
