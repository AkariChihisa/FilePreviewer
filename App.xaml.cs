using System.Configuration;
using System.Data;
using System.Text;
using System.Windows;

namespace FilePreviewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 添加GBK, GB2312字符集
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }

}
