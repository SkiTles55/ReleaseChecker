using ReleaseChecker;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ReleaseCheckerDemo.Models
{
    internal class ReleaseCheckerModel<TOne, TTWo> : BindableEntity where TOne : ReleaseChecker<TTWo> where TTWo : Release
    {
        private ObservableCollection<TTWo> releases = new();

        public TOne? Checker { get; set; } = null;

        public ObservableCollection<TTWo> Releases
        {
            get => releases;
            set
            {
                releases = value;
                OnPropertyChanged("Releases");
            }
        }

        public async Task GetReleases(int page, int pageSize, bool includePreRelease)
        {
            Releases.Clear();
            var result = await Checker!.GetReleasesAsync(page, pageSize, includePreRelease);
            foreach (var release in result)
                Releases.Add(release);
        }

        public async Task GetLatestRelease(bool includePreRelease)
        {
            Releases.Clear();
            var result = await Checker!.GetLatestReleaseAsync(includePreRelease);
            if (result != null)
                Releases.Add(result);
        }
    }
}