using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ReleaseCheckerDemo
{
    internal class BindableEntity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}