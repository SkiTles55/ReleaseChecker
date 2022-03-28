using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ReleaseCheckerDemo
{
    internal class BindableEntity
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}