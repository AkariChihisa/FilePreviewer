using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using TextBox = System.Windows.Controls.TextBox;



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
            this.PreviewKeyDown += MainWindow_PreviewKeyDown;
        }
        private void MainWindow_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                e.Handled = true;
                SaveFileMenuItem_Click(null, null);
            }
            else if (e.Key == Key.O && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                e.Handled = true;
                OpenFileMenuItem_Click(null, null);
            }
            else if (e.Key == Key.E && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                e.Handled = true;
                ExitMenuItem_Click(null, null);
            }
        }

        private void OpenFileMenuItem_Click(Object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "选择包含 .txt 和 .log 文件的文件夹";
                dialog.ShowNewFolderButton = false;

                FileListBox.Items.Clear();
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedPath = dialog.SelectedPath;
                    var files = Directory.GetFiles(selectedPath, "*.*", SearchOption.AllDirectories)
                                         .Where(f => f.EndsWith(".txt", StringComparison.OrdinalIgnoreCase) ||
                                                     f.EndsWith(".log", StringComparison.OrdinalIgnoreCase));

                    FileListBox.Items.Clear();
                    foreach (var file in files)
                    {
                        // 前台文件名和后台全路径
                        FileListBox.Items.Add(new FileItem
                        {
                            FileName = Path.GetFileName(file),
                            FullPath = file
                        });
                    }
                    FileListBox.DisplayMemberPath = "FileName";
                }
            }
        }
        private void FileListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FileListBox.SelectedItem is FileItem selectedFile)
            {
                try
                {
                    string content = File.ReadAllText(selectedFile.FullPath, Encoding.UTF8);
                    Content_Unicode.Content = "UTF-8";
                    if (content.Contains("�"))
                    {
                        // 出现乱码，尝试使用GB2312编码读取
                        content = File.ReadAllText(selectedFile.FullPath, Encoding.GetEncoding("GB2312"));
                        Content_Unicode.Content = "GB2312";
                    }
                    Counter_String.Content = content.Length.ToString() + "个字符";
                    FileContentBox.Text = content;
                }
                catch (IOException ex)
                {
                    MessageBox.Show("读取文件出错: " + ex.Message);
                    Content_Unicode.Content = "未知编码";
                }
            }
        }
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 关闭应用程序
            System.Windows.Application.Current.Shutdown();
        }
        private void FileContentBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb == null) return;
            //获取光标的字符索引
            int caretIndex = tb.SelectionStart;
            //获取该索引所在行数
            int line = tb.GetLineIndexFromCharacterIndex(caretIndex);
            //获取该行第一个字符的索引
            int lineStartIndex = tb.GetCharacterIndexFromLineIndex(line);
            //计算光标字符索引与该行第一个字符的偏移量 + 1 -> 列位置
            int column = caretIndex - lineStartIndex + 1;
            CursorPositionTextBlock.Text = $"行{line + 1},列{column}";
            // 更新选中文本的字符计数
            if (FileContentBox.SelectionLength > 0)
            {
                Counter_String.Content = FileContentBox.SelectedText.Length.ToString() + "个字符";
            }
            else
            {
                Counter_String.Content = FileContentBox.Text.Length.ToString() + "个字符";
            }
        }
        private void SaveFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (FileListBox.SelectedItem is FileItem selectedFile)
            {
                try
                {
                    // 获取文本框内容
                    string contentToSave = FileContentBox.Text;
                    // 获取编码格式
                    string encodingType = Content_Unicode.Content?.ToString() ?? string.Empty;
                    if (encodingType == "GB2312")
                    {
                        File.WriteAllText(selectedFile.FullPath, contentToSave, Encoding.GetEncoding("GB2312"));
                    }
                    else
                    {
                        File.WriteAllText(selectedFile.FullPath, contentToSave, Encoding.UTF8);
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show("保存文件出错: " + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("请先选择一个文件进行保存。", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


    }
}
