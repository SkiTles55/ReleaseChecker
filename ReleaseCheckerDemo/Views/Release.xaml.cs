using System.Windows.Controls;
using System.Windows.Navigation;

namespace ReleaseCheckerDemo.Views
{
    /// <summary>
    /// Логика взаимодействия для Release.xaml
    /// </summary>
    public partial class Release : UserControl
    {
        public Release()
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
