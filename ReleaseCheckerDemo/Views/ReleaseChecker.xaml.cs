using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReleaseCheckerDemo.Views
{
    /// <summary>
    /// Логика взаимодействия для ReleaseChecker.xaml
    /// </summary>
    public partial class ReleaseChecker : UserControl
    {
        public ReleaseChecker()
        {
            InitializeComponent();
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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}