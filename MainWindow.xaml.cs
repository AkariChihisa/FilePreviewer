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
    }
    private void FileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            FileMenuPopup.IsOpen = true;
        }

        private void FileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            // 延迟关闭逻辑由Popup决定，不立即关闭
        }
    }