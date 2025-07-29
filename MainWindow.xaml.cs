using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FilePreviewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // 存储文件路径的列表
        private List<string> filePaths = new List<string>();

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

        private void OpenFileButton_Click(Object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            dlg.Filter = "文本文件 (*.txt;*.log)|*.txt;*.log";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                FileListBox.Items.Clear();
                foreach (var filename in dlg.FileNames)
                {
                    FileListBox.Items.Add(filename);
                }
            }
        }
        private void FileListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FileListBox.SelectedItem is string selectedFile)
            {
                try
                {
                    string content = File.ReadAllText(selectedFile);
                    FileContentBox.Text = content;
                }
                catch (IOException ex)
                {
                    MessageBox.Show("读取文件出错: " + ex.Message);
                }
            }
        }
    }
}
