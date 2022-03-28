using System.Windows.Controls;

namespace ReleaseCheckerDemo.Views
{
    /// <summary>
    /// Логика взаимодействия для GitHub.xaml
    /// </summary>
    public partial class GitHub : UserControl
    {
        public GitHub()
        {
            InitializeComponent();
            DataContext = new GitHubViewModel();
        }
    }
}