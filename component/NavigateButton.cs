using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FilePreviewer
{
    public class NavigateButton : Button
    {
        public CornerRadius ButtonCornerRadius
        {
            get { return (CornerRadius)GetValue(ButtonCornerRadiusProperty); }
            set { SetValue(ButtonCornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty ButtonCornerRadiusProperty =
            DependencyProperty.Register("ButtonCornerRadius", typeof(CornerRadius), typeof(NavigateButton), new PropertyMetadata(new CornerRadius(0)));

        public Brush BackgroundHover
        {
            get { return (Brush)GetValue(BackgroundHoverProperty); }
            set { SetValue(BackgroundHoverProperty, value); }
        }

        public static readonly DependencyProperty BackgroundHoverProperty =
            DependencyProperty.Register("BackgroundHover", typeof(string), typeof(NavigateButton), new PropertyMetadata("#FF3A3A3A"));

        public Brush BackgroundPressed
        {
            get { return (Brush)GetValue(BackgroundPressedProperty); }
            set { SetValue(BackgroundPressedProperty, value); }
        }

        public static readonly DependencyProperty BackgroundPressedProperty =
            DependencyProperty.Register("BackgroundPressed", typeof(string), typeof(NavigateButton), new PropertyMetadata("#FF555555"));
    }
}
