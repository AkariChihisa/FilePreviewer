using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;


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
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "选择包含 .txt 和 .log 文件的文件夹";
                dialog.ShowNewFolderButton = false;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedPath = dialog.SelectedPath;
                    var files = Directory.GetFiles(selectedPath, "*.*", SearchOption.AllDirectories)
                                         .Where(f => f.EndsWith(".txt", StringComparison.OrdinalIgnoreCase) ||
                                                     f.EndsWith(".log", StringComparison.OrdinalIgnoreCase));

                    FileListBox.Items.Clear();
                    foreach (var file in files)
                    {
                        FileListBox.Items.Add(file);
                    }
                }
            }
        }
        private void FileListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FileListBox.SelectedItem is string selectedFile)
            {
                try
                {
                    string content = File.ReadAllText(selectedFile,Encoding.UTF8);
                    if (content.Contains("�"))
                    {
                        // 出现乱码，尝试使用GB2312编码读取
                        content = File.ReadAllText(selectedFile, Encoding.GetEncoding("GB2312"));
                    }
                    Counter_String.Content = content.Length.ToString()+"个字符";
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
