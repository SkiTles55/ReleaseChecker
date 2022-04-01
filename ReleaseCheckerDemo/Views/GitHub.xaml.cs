using ReleaseCheckerDemo.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is ScrollViewer scrollViewer)
            {
                if (e.Delta > 0)
                    scrollViewer.LineUp();
                else
                    scrollViewer.LineDown();
                e.Handled = true;
            }
        }
    }
}