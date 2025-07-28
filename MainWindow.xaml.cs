using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FilePreviewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            FileMenuPopup.IsOpen = true;
        }

        private void FileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            FileButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3D3D3"));
        }

        private void FileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            FileButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E1E1E"));
        }

        private void FileMenuPopup_MouseEnter(object sender, MouseEventArgs e)
        {
            // 鼠标移到Popup上不关闭
        }

        private void FileMenuPopup_MouseLeave(object sender, MouseEventArgs e)
        {
            FileMenuPopup.IsOpen = false;
        }
    }
}
