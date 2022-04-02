using System.Windows.Controls;
using System.Windows.Navigation;

namespace ReleaseCheckerDemo.Views
{
    /// <summary>
    /// Логика взаимодействия для ReleaseFile.xaml
    /// </summary>
    public partial class ReleaseFile : UserControl
    {
        public ReleaseFile()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ExtMethods.OpenUrl(e.Uri.AbsoluteUri);
            e.Handled = true;
        }
    }
}