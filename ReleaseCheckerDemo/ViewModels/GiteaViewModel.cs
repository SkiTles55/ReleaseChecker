using ReleaseChecker.Gitea;
using ReleaseCheckerDemo.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReleaseCheckerDemo.ViewModels
{
    internal class GiteaViewModel : ReleaseCheckerViewModel
    {
        private readonly ReleaseCheckerModel<GiteaChecker, GiteaRelease> _giteaModel = new();

        public override RelayCommand GetReleases => new(async obj => { await GetGiteaReleases(); });

        public override RelayCommand GetLatestRelease => new(async obj => { await GetGiteaLatestRelease(); });

        public override RelayCommand GetNextPage => new(async obj => { await GetGiteaReleasesNextPage(); });

        public override RelayCommand GetPreviousPage => new(async obj => { await GetGiteaReleasesPreviousPage(); });

        public override bool IsServerFieldEnabled => true;

        public ObservableCollection<GiteaRelease> Releases => _giteaModel.Releases;

        public new string ServerUrl { get; set; } = "https://dev.sp-tarkov.com/";

        public new string RepoAuthor { get; set; } = "SPT-AKI";

        public new string RepoName { get; set; } = "Stable-releases";

        private async Task GetGiteaReleasesNextPage()
        {
            Page += 1;
            await GetGiteaReleases();
        }

        private async Task GetGiteaReleasesPreviousPage()
        {
            Page -= 1;
            await GetGiteaReleases();
        }

        private async Task GetGiteaLatestRelease()
        {
            PrepareGiteaChecker();
            try
            {
                await _giteaModel.GetLatestRelease(IncludePreRelease);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            UpdatePageButtons();
        }

        private void UpdatePageButtons()
        {
            IsNextPageButtonEnabled = _giteaModel.Checker?.HasNextPage ?? false;
            IsPreviousPageButtonEnabled = Page > 1;
        }

        private async Task GetGiteaReleases()
        {
            PrepareGiteaChecker();
            try
            {
                await _giteaModel.GetReleases(Page, PageSize, IncludePreRelease);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            UpdatePageButtons();
        }

        private void PrepareGiteaChecker()
        {
            if (_giteaModel.Checker == null)
            {
                SetupGiteaChecker();
            }
            else if (repoAuthor != RepoAuthor || repoName != RepoName)
            {
                _giteaModel.Checker = null;
                SetupGiteaChecker();
            }
        }

        private void SetupGiteaChecker()
        {
            repoAuthor = RepoAuthor;
            repoName = RepoName;
            _giteaModel.Checker = new(ServerUrl, RepoAuthor, RepoName);
        }
    }
}